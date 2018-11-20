using AppXamarin.Models;
using AppXamarin.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace AppXamarin.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private readonly JSONPlaceholder jSONPlaceholder;
        public ObservableCollection<Posts> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public AboutViewModel()
        {
            Title = "Service";
            jSONPlaceholder = new JSONPlaceholder();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

          //  ExecuteLoadItemsCommand();
            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }

       async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                //  var jSONPlaceholder = DependencyService.Get<IJSONPlaceholder>();
                var data = await DataStore.GetItemsAsync(true);
                  var items = jSONPlaceholder.GetPosts();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        public ICommand OpenWebCommand { get; }
    }
}