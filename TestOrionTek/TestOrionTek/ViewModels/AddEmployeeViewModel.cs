using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using PropertyChanged;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using TestOrionTek.Models;
using TestOrionTek.Extensions;
using TestOrionTek.ApiSettings;

namespace TestOrionTek.ViewModels
{
    [AddINotifyPropertyChangedInterface]
	public class AddEmployeeViewModel : ViewModelBase
    {
        private readonly IBackendClient<ApiMethodCollection> _api;

        public string ErrorMessage { get; set; }
        public Employees Employee { get; set; } = new Employees();
        public ObservableCollection<AddressEntries> Address { get; set; } = new ObservableCollection<AddressEntries>();

        public ICommand AddNewAddresCommand => new Command(() => AddNewAddres());
        public ICommand RemoveAddressCommand => new Command<AddressEntries>(RemoveAddress);
        public ICommand CreateUserCommand => new Command(async() => await CreateUser());

        public ICommand CancelCommand => new Command(async () =>
        {
            await NavigationService.GoBackAsync();
        });
        public AddEmployeeViewModel(INavigationService navigationService, IBackendClient<ApiMethodCollection> api)
            : base(navigationService)
        {
            Title = "Add Employee";
            _api = api;
            Address.Add(new AddressEntries { AddressName = "" });
        }

        async Task CreateUser()
        {

            foreach (var address in Address.ToList()) {
                Employee.Address.Add(new Address{ AddressName = address.AddressName}); 
            }

            if (Employee.HasAPropertyEmpty())
            {
                ErrorMessage = "There is a field empty or wrong filled, please check your data and try again";
                return;
            }

            await _api.CallAsync(ep=>ep.PostEmployee(Employee));
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
            Address.Add(new AddressEntries { AddressName = ""});
        }
    }
}
