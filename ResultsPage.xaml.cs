using TesseractDemo.ClassAndInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TesseractDemo.Pages
{
    public partial class ResultsPage : ContentPage
    {
        public DataBase _db;
        public ResultsPage(DataBase MyDB)
        {
            InitializeComponent();
            _db = MyDB;
            listview.ItemsSource = MyDB.GetCategories();
            if (listview.ItemsSource == null)
            {
                noRecords.IsVisible = true;
            }
            else
            {
                noRecords.IsVisible = false;
            }
            listview.ItemSelected += async (s, e) =>
            {
                if (e.SelectedItem != null)
                {
                    DBClass temp = (DBClass)e.SelectedItem;
                    var page = new RecordsPage(MyDB.GetRecords(temp.ID), _db);
                    await Navigation.PushAsync(page);
                    listview.SelectedItem = null;


                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            listview.ItemsSource = _db.GetCategories();
            if (listview.ItemsSource == null)
            {
                noRecords.IsVisible = true;
            }
            else
            {
                noRecords.IsVisible = false;
            }
        }
    }
}
