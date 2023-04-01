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
	public class EmployeeDetailViewModel : ViewModelBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public string ErrorMessage { get; set; }

        private string ID { get; set; }
        public Employees Employee { get; set; } = new Employees();
        public ObservableCollection<AddressEntries> Address { get; set; } = new ObservableCollection<AddressEntries>();

        public ICommand CancelCommand => new Command(async () =>
        {
            await NavigationService.GoBackAsync();
        });
        public EmployeeDetailViewModel(INavigationService navigationService, IUnitOfWork unitOfWork)
            : base(navigationService)
        {
            Title = "Detail Employee";
            _unitOfWork = unitOfWork;
        }

        async Task GetEmployeeData()
        {
            Guid id = Guid.Parse(ID);
            var result = await _unitOfWork.Employees.GetSingleAsync(a => a.ID == id);
            Employee = result;
            foreach (var address in Employee.Address)
            {
                Address.Add(new AddressEntries { AddressName = address });
            }
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
