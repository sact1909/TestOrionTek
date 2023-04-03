using Polly.Fallback;
using Polly.Wrap;
using Polly;
using Refit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace TestOrionTek.ApiSettings
{
    public class PolicyBuilder<T>
    {
        public AsyncPolicyWrap<TResult> Build<TResult>()
        {
            return AuthorizationPolicy<TResult>().WrapAsync(RetryPolicy());
        }

        public AsyncPolicyWrap Build()
        {
            return Policy.WrapAsync(AuthorizationPolicy(), RetryPolicy());
        }

        private AsyncFallbackPolicy<TResult> AuthorizationPolicy<TResult>()
        {
            return Policy<TResult>.Handle<ApiException>()
                .FallbackAsync((token) =>
                {
                    Debug.WriteLine("Invalid credentials");

                    return Task.FromResult(default(TResult));
                });
        }

        private AsyncFallbackPolicy AuthorizationPolicy()
        {
            return Policy.Handle<ApiException>()
                .FallbackAsync((token) =>
                {
                    Debug.WriteLine("Invalid credentials");

                    return Task.CompletedTask;
                });


        }

        private AsyncPolicy RetryPolicy()
        {
            var numberOfRetries = 2;

            return Policy.Handle<ApiException>(a => a.StatusCode != HttpStatusCode.NotFound &&
                                                    a.StatusCode != HttpStatusCode.BadRequest &&
                                                    a.StatusCode != HttpStatusCode.Forbidden).WaitAndRetryAsync(
                numberOfRetries,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2.5, retryAttempt)),
                (exception, timeSpan, attempt, context) =>
                {
                    Debug.WriteLine("=================== Exception ==========================");
                    Debug.WriteLine($"Retry attempt {attempt} of {numberOfRetries}");
                    Debug.WriteLine(string.Empty);
                    Debug.WriteLine(($"Exception: {exception}."));
                    Debug.WriteLine("========================================================");
#if !DEBUG
                    if (attempt == numberOfRetries) LogHttpException(exception as ApiException);                    
#endif
                });
        }

        private void LogHttpException(ApiException apiException)
        {
            Debug.WriteLine($"==============={apiException.Uri}===============");
            Debug.WriteLine($"==============={apiException.StatusCode}=============");

        }
    }
}
