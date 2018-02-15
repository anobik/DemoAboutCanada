/* Filename: MainPage.Xaml.cs
 * Description: Coe behind for view for the main page
 * */

using Xamarin.Forms;

namespace AboutCanada
{
    /// <summary>
    /// Class for the main page
    /// </summary>
    public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        /// <summary>
        /// List view items scrolled event
        /// </summary>
        /// <param name="sender">Listview</param>
        /// <param name="e">Item parameter</param>
        private void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            (this.BindingContext as MainPageViewModel).PopulateListImages(e.Item);
        }
    }
}
