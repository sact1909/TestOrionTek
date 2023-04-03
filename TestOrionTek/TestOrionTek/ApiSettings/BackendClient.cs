using DryIoc;
using Fusillade;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace TestOrionTek.ApiSettings
{
    public class BackendClient<T> : IBackendClient<T>
    {
        private readonly string _apiBaseAddress;
        private readonly HttpClientHandler _httpClientHandler;
        private readonly PolicyBuilder<T> _policy;

        public BackendClient()
        {
            _policy = new PolicyBuilder<T>();
            _apiBaseAddress = GetUrl();
#if DEBUG
            _httpClientHandler = new LoggedHttpClientHandler();
#else
            _httpClientHandler = new HttpClientHandler();
#endif
        }

        public async Task<TResult> CallAsync<TResult>(Func<T, Task<TResult>> apiCall, Priority priority = Priority.Speculative)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet) return default(TResult);

            var client = CreateClient(new RateLimitedHttpMessageHandler(_httpClientHandler, priority.ToFusilladePriority()));

            return await _policy.Build<TResult>().ExecuteAsync(async () => await apiCall(client));
        }

        public async Task CallAsync(Func<T, Task> apiCall, Priority priority = Priority.Speculative)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet) return;

            var client = CreateClient(new RateLimitedHttpMessageHandler(_httpClientHandler, priority.ToFusilladePriority()));

            await _policy.Build().ExecuteAsync(async () => await apiCall(client));
        }

        public TResult Call<TResult>(Func<T, Task<TResult>> apiCall, Priority priority = Priority.Speculative)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet) return default(TResult);

            var client = CreateClient(new RateLimitedHttpMessageHandler(_httpClientHandler, priority.ToFusilladePriority()));

            return _policy.Build<TResult>().ExecuteAsync(() => apiCall(client)).Result;
        }

        public async Task Call(Func<T, Task> apiCall, Priority priority = Priority.Speculative)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet) return;

            var client = CreateClient(new RateLimitedHttpMessageHandler(_httpClientHandler, priority.ToFusilladePriority()));

            await _policy.Build().ExecuteAsync(async () => await apiCall(client));
        }

        private T CreateClient(HttpMessageHandler messageHandler, string apiBaseAddress = null)
        {
            /*var refitSettings = new RefitSettings()
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(App.ApiToken),
                HttpMessageHandlerFactory = () => messageHandler
            };*/

            var client = new HttpClient(messageHandler)
            {
                BaseAddress = new Uri(_apiBaseAddress)
            };
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("API-KEY-USER", App.ApiKey);


            var service = RestService.For<T>(client);
            return service;
        }

        private string GetUrl()
        {
            var urlAttribute = (UrlAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(UrlAttribute));

            return urlAttribute.Url;
        }
    }
}
