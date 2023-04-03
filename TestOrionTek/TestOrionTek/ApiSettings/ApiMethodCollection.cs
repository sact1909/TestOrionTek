using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestOrionTek.Constants;
using TestOrionTek.Models;

namespace TestOrionTek.ApiSettings
{
    [Url("http://www.xaritestapp.somee.com/api")]
    [Headers("Accept: application/json", "API-KEY-USER: " + AppConstants.ApiKey)]
    public interface ApiMethodCollection
    {
        [Get("/Employees")]
        Task<List<Employees>> GetAllEmployees();

        [Get("/Employees/GetWithAddress")]
        Task<List<Employees>> GetAllEmployeesWithAddress();

        [Get("/Employees/GetWithAddressById/{Id}")]
        Task<Employees> GetEmployeeWithAddressById(string Id);

        [Delete("/Employees/DeleteWithAddressById/{Id}")]
        Task<string> DeleteWithAddressById(string Id);

        [Post("/Employees")]
        Task<string> PostEmployee([Body] Employees employees);

        [Put("/Employees")]
        Task<string> PutEmployee([Body] Employees employees);

        [Delete("/Employees")]
        Task<string> DeleteEmployee([Body]Employees employees);
    }
}
