/* Filename: MainPageViewModel.cs
 * Description: ViewModel for the main page to hold buisness logic 
 * */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
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
        /// check progressbar visibility variable
        /// </summary>
        private bool isRequestMade;

        /// <summary>
        /// title variable
        /// </summary>
        private string title;

        /// <summary>
        /// Command text variable
        /// </summary>
        private string commandText;

        /// <summary>
        /// List data variable
        /// </summary>
        private List<Row> listData = null;

        /// <summary>
        /// initial data for list
        /// </summary>
        private List<Row> sortedData;

        /// <summary>
        /// Title of the app
        /// </summary>
        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
                OnPropertyChanged("Title");

            }
        }
        
        /// <summary>
        /// Title of the app
        /// </summary>
        public string CommandText
        {
            get
            {
                return commandText;
            }

            set
            {
                commandText = value;
                OnPropertyChanged("CommandText");

            }
        }

        /// <summary>
        /// List to store data
        /// </summary>
        public List<Row> ListData
        {
            get
            {
                return listData;
            }

            set
            {
                listData = value;
                OnPropertyChanged("ListData");
            }
        }

        /// <summary>
        /// Command for gettign data
        /// </summary>
        public ICommand GetCanadianData { get; private set; }

        /// <summary>
        /// Command for sorting data
        /// </summary>
        public ICommand SortCanadianData { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MainPageViewModel()
        {
            GetCanadianData = new Command(ExecuteGetRequest);
            SortCanadianData = new Command(ExecuteSort);
            CommandText = "Get Data";
        }

        /// <summary>
        /// call for sorting data
        /// </summary>
        public async void ExecuteSort()
        {
            if (!isRequestMade)
            {
                isRequestMade = true;
                await Task.Run(() =>
                {
                    if (ListData != null)
                    {
                        ListData.Clear();
                        ListData = null;
                        sortedData.Reverse();
                    }
                });

                ListData = (from obj in sortedData
                            select obj).ToList();
                isRequestMade = false;
            }
            else
            {
                // show alert that an operation under progress
            }
        }

        /// <summary>
        /// Api call for getting data
        /// </summary>
        public async void ExecuteGetRequest()
        {
            if (!isRequestMade)
            {
                isRequestMade = true;
                bool isSuccess = await GetData();
                CommandText = isSuccess ? "Refresh Data" : "Get Data";
                isRequestMade = false;
            }
            else
            {
                // one operation under process show alert
            }
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
                if (ListData != null)
                {
                    ListData.Clear();
                }

                sortedData = (from obj in data.rows
                              where obj.description != null || obj.imageHref != null || obj.title != null
                              orderby obj.title
                              select obj).ToList();

                ListData = (from obj in sortedData
                            select obj).ToList();
                ;
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

        /// <summary>
        /// Property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
