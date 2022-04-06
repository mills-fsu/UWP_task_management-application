using ListManagement.models;
using Newtonsoft.Json;
using System;
using System.IO;
using UWPListManagement.Dialogs;
using UWPListManagement.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Runtime.CompilerServices;
using System.ComponentModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPListManagement
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        private string persistencePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\SaveData.json";

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public MainPage()
        {
            this.InitializeComponent();

            DataContext = new MainViewModel(persistencePath);

        }

        private async void AddToDoClick(object sender, RoutedEventArgs e)
        {
            var dialog = new ToDoDialog();
            await dialog.ShowAsync();
        }

        private async void EditToDoClick(object sender, RoutedEventArgs e)
        {
            var dialog = new ToDoDialog((DataContext as MainViewModel).SelectedItem);
            await dialog.ShowAsync();
        }

        private void RemoveToDoClick(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).Remove();
        }
/*
        private void CompleteToDoClick(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).Complete();
            NotifyPropertyChanged();
        }
*/
        private async void AddAppointmentClick(object sender, RoutedEventArgs e)
        {
            var dialog = new AppointmentDialog();
            await dialog.ShowAsync();
        }

        private async void EditAppointmentClick(object sender, RoutedEventArgs e)
        {
            var dialog = new AppointmentDialog((DataContext as MainViewModel).SelectedItem);
            await dialog.ShowAsync();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).save();
        }
        private void LoadClick(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).Load();
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).Search(nameInput.Text);
        }

        private void SortClick(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).Sort();
        }

    }
}
