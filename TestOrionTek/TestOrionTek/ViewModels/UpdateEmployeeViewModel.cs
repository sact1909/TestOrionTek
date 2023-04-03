using DryIoc;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TestOrionTek.ApiSettings;
using TestOrionTek.Extensions;
using TestOrionTek.Models;
using Xamarin.Forms;

namespace TestOrionTek.ViewModels
{
	public class UpdateEmployeeViewModel : ViewModelBase
    {
        private readonly IBackendClient<ApiMethodCollection> _api;

        public string ErrorMessage { get; set; }

        private string ID { get; set; }
        public Employees Employee { get; set; } = new Employees();
        public ObservableCollection<AddressEntries> Address { get; set; } = new ObservableCollection<AddressEntries>();

        public ICommand AddNewAddresCommand => new Command(() => AddNewAddres());
        public ICommand RemoveAddressCommand => new Command<AddressEntries>(RemoveAddress);
        public ICommand UpdateUserCommand => new Command(async () => await UpdateUser());

        public ICommand CancelCommand => new Command(async () =>
        {
            await NavigationService.GoBackAsync();
        });
        public UpdateEmployeeViewModel(INavigationService navigationService, IBackendClient<ApiMethodCollection> api)
            : base(navigationService)
        {
            Title = "Update Employee";
            _api = api;
        }

        async Task GetEmployeeData()
        {
            var result = await _api.CallAsync(ep=>ep.GetEmployeeWithAddressById(ID));
            Employee = result;
            foreach (var address in Employee.Address)
            {
                Address.Add(new AddressEntries { AddressName = address.AddressName });
            }
        }

        async Task UpdateUser()
        {
            foreach (var address in Address.ToList())
            {
                Employee.Address.Add(new Address { AddressName = address.AddressName });
            }

            if (Employee.HasAPropertyEmpty())
            {
                ErrorMessage = "There is a field empty or wrong filled, please check your data and try again";
                return;
            }

            await _api.CallAsync(ep => ep.PutEmployee(Employee));
            await NavigationService.GoBackAsync();
        }

        private void RemoveAddress(AddressEntries address)
        {
            if (Address.Count() > 1)
            {
                Address.Remove(address);
            }
        }

        void AddNewAddres()
        {
            Address.Add(new AddressEntries { AddressName = "" });
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            //get a single typed parameter
            string Id = parameters.GetValue<string>("Id");
            ID = Id;
            new Action(async () => await GetEmployeeData())();
        }
    }
}
