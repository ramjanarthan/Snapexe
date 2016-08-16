using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesseractDemo.ClassAndInterface;
using Xamarin.Forms;

namespace TesseractDemo.Pages
{
    public partial class MonthPage : ContentPage
    {

        public DataBase _db;
        public MonthPage(DataBase MyDB)
        {
            InitializeComponent();
            _db = MyDB;

            listview.ItemsSource = _db.GetMonths();
            if(listview.ItemsSource == null)
            {
                noRecords.IsVisible = true;
            }else
            {
                noRecords.IsVisible = false;
            }

            listview.ItemSelected += async (s, e) =>
            {
                if (e.SelectedItem != null)
                {
                    string temp = (string)e.SelectedItem;
                    var page = new RecordsPage(MyDB.GetRecords(temp), _db);
                    await Navigation.PushAsync(page);
                    listview.SelectedItem = null;


                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            listview.ItemsSource = _db.GetMonths();
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
