using Prism;
using Prism.Ioc;
using TestOrionTek.ViewModels;
using TestOrionTek.Views;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using TestOrionTek.ApiSettings;

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
            containerRegistry.RegisterForNavigation<UpdateEmployee, UpdateEmployeeViewModel>();
            containerRegistry.RegisterForNavigation<EmployeeDetail, EmployeeDetailViewModel>();

            /*API Service*/
            containerRegistry.RegisterSingleton(typeof(IBackendClient<>),typeof(BackendClient<>));
        }
    }
}
