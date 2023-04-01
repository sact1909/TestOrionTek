using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using TestOrionTek.DatabaseSettings.DbServices.Abstract;
using TestOrionTek.DatabaseSettings.Entities;
using PropertyChanged;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using TestOrionTek.DatabaseSettings.DbServices;
using TestOrionTek.Models;
using TestOrionTek.Extensions;

namespace TestOrionTek.ViewModels
{
    [AddINotifyPropertyChangedInterface]
	public class AddEmployeeViewModel : ViewModelBase
    {
        private readonly IUnitOfWork _unitOfWork;

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
        public AddEmployeeViewModel(INavigationService navigationService, IUnitOfWork unitOfWork)
            : base(navigationService)
        {
            Title = "Add Employee";
            _unitOfWork = unitOfWork;
            Address.Add(new AddressEntries { AddressName = "" });
        }

        async Task CreateUser()
        {
            Employee.Address = Address.Select(a=>a.AddressName).ToList();

            if (Employee.HasAPropertyEmpty())
            {
                ErrorMessage = "There is a field empty or wrong filled, please check your data and try again";
                return;
            }

            await _unitOfWork.Employees.SaveItemAsync(Employee);
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
