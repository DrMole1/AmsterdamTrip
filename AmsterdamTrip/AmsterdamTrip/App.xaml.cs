using AmsterdamTrip.Repositories;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AmsterdamTrip
{
    public partial class App : Application
    {
        private string dbPath = Path.Combine(FileSystem.AppDataDirectory, "dataBase.db3");
        public static MuseumsRepository MuseumsRepository { get; private set; }
        public static object UserRepository { get; internal set; }

        public App()
        {
            InitializeComponent();

            MuseumsRepository = new MuseumsRepository(dbPath);

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
