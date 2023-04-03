using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestOrionTek.ApiSettings
{
    public interface IBackendClient<T>
    {
        Task<TResult> CallAsync<TResult>(Func<T, Task<TResult>> apiCall, Priority priority = Priority.Speculative);

        Task CallAsync(Func<T, Task> apiCall, Priority priority = Priority.Speculative);

        TResult Call<TResult>(Func<T, Task<TResult>> apiCall, Priority priority = Priority.Speculative);

        Task Call(Func<T, Task> apiCall, Priority priority = Priority.Speculative);

    }
}
