using Xamarin.Forms;

namespace wifiAnalysis
{
    public partial class App : Application
    {
        private static Database database;
        public static Database ScanDatabase
        {
            get
            {
                if (database == null)
                {
                    database = new Database();
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            //Call to DB to pre-populate
            ScanDatabase.GetRooms();
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
