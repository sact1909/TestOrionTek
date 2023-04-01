using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TestOrionTek.DatabaseSettings.DbServices.Abstract;
using TestOrionTek.DatabaseSettings.Entities;
using PropertyChanged;
using System.Windows.Input;
using Xamarin.Forms;
using TestOrionTek.Views;
using Prism.AppModel;
using Xamarin.Essentials;

namespace TestOrionTek.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageViewModel : ViewModelBase, IPageLifecycleAware
    {
        private readonly IUnitOfWork _unitOfWork;

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
            await _unitOfWork.Employees.DeleteItemAsync(employee);
            await LoadEmployees();
        });

        public MainPageViewModel(INavigationService navigationService, IUnitOfWork unitOfWork)
            : base(navigationService)
        {
            Title = "Main Page";
            _unitOfWork = unitOfWork;

            new Action(async()=> await LoadEmployees())();
        }

        async Task LoadEmployees()
        {
            var result = await _unitOfWork.Employees.GetAllAsync();

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
