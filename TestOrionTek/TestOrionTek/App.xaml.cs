using Prism;
using Prism.Ioc;
using TestOrionTek.DatabaseSettings.DbServices.Abstract;
using TestOrionTek.DatabaseSettings.DbServices;
using TestOrionTek.ViewModels;
using TestOrionTek.Views;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace TestOrionTek
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<AddEmployee, AddEmployeeViewModel>();

            /*UnitOfWork*/
            containerRegistry.RegisterSingleton<IUnitOfWork, UnitOfWork>();
            containerRegistry.RegisterForNavigation<UpdateEmployee, UpdateEmployeeViewModel>();
            containerRegistry.RegisterForNavigation<EmployeeDetail, EmployeeDetailViewModel>();
        }
    }
}
