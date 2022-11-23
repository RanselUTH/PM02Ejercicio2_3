using PM02Ejercicio2_3.Contollers;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PM02Ejercicio2_3.Contollers;
using Xamarin.Forms;

namespace PM02Ejercicio2_3
{
    public partial class App : Application
    {

        static BDAudio BDAudio;

        public static BDAudio BDAudios
        {
            get
            {
                if (BDAudio == null)
                {
                    BDAudio = new BDAudio(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Audios.db"));

                }
                return BDAudio;
            }
        }

        public App()
        {
            InitializeComponent();

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

