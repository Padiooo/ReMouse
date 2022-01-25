using ReMouse.Phone.Core.Application;
using ReMouse.Phone.Core.Helpers;
using ReMouse.Phone.Core.RepositoryData;
using ReMouse.Phone.MVVM.Models;
using ReMouse.Phone.MVVM.Views;
using ReMouse.Phone.Resources;
using Xamarin.Forms;

namespace ReMouse.Phone
{
    public partial class App : Application
    {
        private readonly ApplicationLifeCycleSource source = new ApplicationLifeCycleSource();

        public App()
        {
            InitializeComponent();

            DependencyService.Register<ButtonRepository>();
            DependencyService.Register<ApplicationDataRepository>();

            Resources.Add(SocketModel.ResourceKey, new SocketModel());
            Resources.Add(ApplicationColorModel.ResourceKey, new ApplicationColorModel());
            Resources.Add(PageHolder.ResourceKey, new PageHolder());
            Resources.MergedDictionaries.Add(new Styles());

            DependencyService.RegisterSingleton(source);

            MainPage mainPage = new MainPage();
            MainPage = new NavigationPage(mainPage);
        }

        protected override void OnStart()
        {
            source.Start();
        }

        protected override void OnSleep()
        {
            source.Sleep();
        }

        protected override void OnResume()
        {
            source.Resume();
        }
    }
}
