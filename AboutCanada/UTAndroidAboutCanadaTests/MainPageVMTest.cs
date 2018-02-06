/* Filename: MainPageVMTest.cs
 * Description: UT for the main page vm 
 * */

using AboutCanada;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace UTAndroidAboutCanada.Tests
{
    /// <summary>
    /// Unit test class
    /// </summary>
    [TestClass]
    public class MainPageVMTest
    {
        /// <summary>
        /// Method to test if api call successfull
        /// </summary>
        /// <returns>Task</returns>
        [TestMethod]
        public async Task Test_GetData()
        {
            MainPageViewModel mainPageVm = new MainPageViewModel();
            mainPageVm.ExecuteGetRequest();

            while (mainPageVm.IsRequestMade)
            {
                await Task.Delay(1000);
            }

            Assert.IsTrue(!string.IsNullOrEmpty(mainPageVm.Title));
        }

        /// <summary>
        /// Test to check proper sorting execution
        /// </summary>
        /// <returns>Task</returns>
        [TestMethod]
        public async Task Test_ExecuteSort()
        {
            MainPageViewModel mainPageVm = new MainPageViewModel();
            mainPageVm.ExecuteGetRequest();
            while (mainPageVm.IsRequestMade)
            {
                await Task.Delay(1000);
            }

            var lastListElementTitle = mainPageVm.ListData[mainPageVm.ListData.Count - 1].title;
            mainPageVm.ExecuteSort();
            await Task.Delay(1000);
            Assert.AreEqual(mainPageVm.ListData[0].title, lastListElementTitle);
        }
    }
}