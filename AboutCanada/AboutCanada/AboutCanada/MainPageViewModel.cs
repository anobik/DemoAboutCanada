/* Filename: MainPageViewModel.cs
 * Description: ViewModel for the main page to hold buisness logic 
 * */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace AboutCanada
{
    /// <summary>
    /// Main page view model class
    /// </summary>
    public class MainPageViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Title of the app
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// List to store data
        /// </summary>
        public List<Row> ListData = null;

        /// <summary>
        /// Command for gettign data
        /// </summary>
        public ICommand GetCanadianData { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MainPageViewModel()
        {
            GetCanadianData = new Command(ExecuteGetRequest);
        }

        /// <summary>
        /// Api call for getting data
        /// </summary>
        public async void ExecuteGetRequest()
        {
            bool isSuccess = await GetData();
        }

        /// <summary>
        /// Method to get api data
        /// </summary>
        /// <returns>If value recieved or not</returns>
        private async Task<bool> GetData()
        {
            try
            {
                HttpClient downloadClient = new HttpClient();
                string jsonData = await downloadClient.GetStringAsync("https://dl.dropboxusercontent.com/s/2iodh4vg0eortkl/facts.json");
                ItemData data = JsonConvert.DeserializeObject<ItemData>(jsonData);
                Title = data.title;
                ListData = data.rows;
                this.OnPropertyChanged("ListData");
                return true;
            }
            catch (Exception ex)
            {
                Debugger.Log(0, "HttpCall", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Fires the Property changed event
        /// </summary>
        /// <param name="propertyName">name of the property</param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
