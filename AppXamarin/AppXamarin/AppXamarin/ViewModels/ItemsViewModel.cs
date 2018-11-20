using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using AppXamarin.Models;
using AppXamarin.Views;
using AppXamarin.Services;

namespace AppXamarin.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        #region Property
        public ObservableCollection<Posts> Items { get; set; }

        private JSONPlaceholder jSONPlaceholder;
        public Command LoadItemsCommand { get; set; }
        #endregion


        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Posts>();
            jSONPlaceholder = new JSONPlaceholder();

              LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var _item = item as Item;
            //    Items.Add(_item);
            //    await DataStore.AddItemAsync(_item);
            //});
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
                var itemsTwo = await DataStore.GetItemsAsync(true);
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
    }
}