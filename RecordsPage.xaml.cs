using TesseractDemo.ClassAndInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.Diagnostics;

namespace TesseractDemo.Pages
{
    public partial class RecordsPage : ContentPage
    {
        DataBase _db;
        public RecordsPage(IEnumerable<DBClass> list, DataBase MyDB)
        {
            InitializeComponent();
            _db = MyDB;
            stack.Padding = new Thickness(20, 20, 20, 20);
            liststack.Padding = new Thickness(20);

            listview.ItemsSource = list;
            double count = 0;
            for (int i = 0; i < list.Count(); i++)
            {
                count += list.ElementAt(i).Amount;
            };

            listview.ItemSelected += Listview_ItemSelected;

            Sum.Text = count.ToString();

           
        }

        private void Listview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            listview.SelectedItem = null;
        }

        public async void OnDelete(object sender, EventArgs e)
        {
            //var mi = ((MenuItem)sender);
            //DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
            var x = (MenuItem) sender;
            var y = (DBClass)x.CommandParameter;
            _db.DeleteRecord(y.ID);
            await DisplayAlert("Confirmation", "Record Deleted", "OK");
            await Navigation.PopAsync();
            
        }
    }
}
