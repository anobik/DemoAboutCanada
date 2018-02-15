using Xamarin.Forms;

namespace AboutCanada
{
    public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new AboutCanada.MainPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
