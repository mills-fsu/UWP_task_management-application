using ListManagement.models;
using ListManagement.services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace UWPListManagement.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ItemService itemService = ItemService.Current;
        private JsonSerializerSettings serializerSettings
            = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        private string persistencePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\SaveData.json";
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       
        public MainViewModel()
        {
           
     
        }

        public MainViewModel(string path)
        {
            Load();
        }

        public ObservableCollection<Item> Items
        {
            get
            {
                return itemService.Items;
            }
        }

        public Item SelectedItem
        {
            get; set;
        }

        public void Add(Item item)
        {
            itemService.Add(item);

        }

        public void Remove()
        {
            itemService.Remove(SelectedItem);
        }

 /*       public void Complete()
        {
            var temp = SelectedItem as ToDo;
            temp.IsCompleted = true;
            //(SelectedItem as ToDo).IsCompleted = true;
     
            NotifyPropertyChanged();
        }
 */
        public void Refresh()
        {
            NotifyPropertyChanged("Items");
        }

        public void save()
        {

            var listJson = JsonConvert.SerializeObject(Items, serializerSettings);
            if (File.Exists(persistencePath))
            {
                File.Delete(persistencePath);
            }
            File.WriteAllText(persistencePath, listJson);
        }
        public void Load()
        {
            MainViewModel mvm;
            if (File.Exists(persistencePath))
            {
                try
                {
                    mvm = JsonConvert
                    .DeserializeObject<MainViewModel>(File.ReadAllText(persistencePath));
                    foreach(Item i in mvm.Items)
                        itemService.Add(i);

                    SelectedItem = mvm.SelectedItem;
                    NotifyPropertyChanged("Items");

                }
                catch (Exception)
                {
                    File.Delete(persistencePath);
                }

            }
            
        }
        
        public void Search(string searchstr)
        {
            var temp = Items
                       .Where(i => ((i as Appointment)?.Attendees.AsEnumerable().Contains(searchstr) == true) || (i as Item)?.Name == searchstr || (i as Item)?.Description == searchstr);

            foreach (Item i in temp)
            {
                SelectedItem = i;
                break;
            }
            NotifyPropertyChanged("SelectedItem");
        }

        public void Sort()
        {
            ObservableCollection<Item> temp;
            temp = new ObservableCollection<Item>(Items.OrderByDescending(Items => Items.Priority));
            Items.Clear();
            foreach (Item i in temp)
                itemService.Add(i);
            NotifyPropertyChanged("Items");
        }
    }
}
