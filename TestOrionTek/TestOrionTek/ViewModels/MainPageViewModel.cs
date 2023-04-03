using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TestOrionTek.Models;
using PropertyChanged;
using System.Windows.Input;
using Xamarin.Forms;
using TestOrionTek.Views;
using Prism.AppModel;
using Xamarin.Essentials;
using TestOrionTek.ApiSettings;

namespace TestOrionTek.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageViewModel : ViewModelBase, IPageLifecycleAware
    {
        private readonly IBackendClient<ApiMethodCollection> _api;

        public List<Employees> EmployeesList { get; set; }

        public ICommand GoToAddNewCommand => new Command(async () => {
            await NavigationService.NavigateAsync($"{nameof(AddEmployee)}");
        });

        public ICommand GoToUpdateCommand => new Command<Guid>(async(Guid ID) => {
            var navigationParams = new NavigationParameters
            {
                { "Id",  ID.ToString()}
            };

            await NavigationService.NavigateAsync($"{nameof(UpdateEmployee)}", navigationParams);
        });

        public ICommand GoToDetailCommand => new Command<Guid>(async (Guid ID) => {
            var navigationParams = new NavigationParameters
            {
                { "Id",  ID.ToString()}
            };

            await NavigationService.NavigateAsync($"{nameof(EmployeeDetail)}", navigationParams);
        });

        public ICommand RemoveEmployeeCommand => new Command<Employees>(async (Employees employee) => {
            await _api.CallAsync(ep => ep.DeleteWithAddressById(employee.Id.ToString()));
            await LoadEmployees();
        });

        public MainPageViewModel(INavigationService navigationService, IBackendClient<ApiMethodCollection> api)
            : base(navigationService)
        {
            Title = "Main Page";
            _api = api;

            new Action(async()=> await LoadEmployees())();
        }

        async Task LoadEmployees()
        {
            var result = await _api.CallAsync(ep=>ep.GetAllEmployeesWithAddress());

            EmployeesList = result;
        }

        public void OnAppearing()
        {
            new Action(async () => await LoadEmployees())();
        }

        public void OnDisappearing()
        {
            //throw new NotImplementedException();
        }
    }
}
