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
using TestOrionTek.DatabaseSettings.DbServices.Abstract;
using TestOrionTek.DatabaseSettings.Entities;
using TestOrionTek.Extensions;
using TestOrionTek.Models;
using Xamarin.Forms;

namespace TestOrionTek.ViewModels
{
	public class UpdateEmployeeViewModel : ViewModelBase
    {
        private readonly IUnitOfWork _unitOfWork;

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
        public UpdateEmployeeViewModel(INavigationService navigationService, IUnitOfWork unitOfWork)
            : base(navigationService)
        {
            Title = "Update Employee";
            _unitOfWork = unitOfWork;

            
        }

        async Task GetEmployeeData()
        {
            Guid id = Guid.Parse(ID);
            var result = await _unitOfWork.Employees.GetSingleAsync(a=>a.ID == id);
            Employee = result;
            foreach (var address in Employee.Address)
            {
                Address.Add(new AddressEntries { AddressName = address });
            }
        }

        async Task UpdateUser()
        {
            Employee.Address = Address.Select(a => a.AddressName).ToList();

            if (Employee.HasAPropertyEmpty())
            {
                ErrorMessage = "There is a field empty or wrong filled, please check your data and try again";
                return;
            }

            await _unitOfWork.Employees.UpdateItemAsync(Employee);
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
