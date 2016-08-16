using TesseractDemo.ClassAndInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TesseractDemo.Pages
{
    public partial class Search : ContentPage
    {
        public DataBase MyDB;
        public Search(DataBase _db)
        {
            InitializeComponent();
            MyDB = _db;
            stack.Padding = new Thickness(0, 20, 0, 0);
            button1.Clicked += Button1_Clicked;
            button2.Clicked += Button2_Clicked;

        }

        private async void Button2_Clicked(object sender, EventArgs e)
        {
            if (Name.Text == null || Name.Text.Equals(""))
            {
                await ((View)sender).FadeTo(0, 500, Easing.Linear);
                await ((View)sender).FadeTo(1, 250, Easing.CubicIn);
            }
            else
            {
                var list = MyDB.GetRecordsName(Name.Text.Trim());
                if (list.Count() == 0)
                {
                    Name.Text = "";
                    fail.Text = "No Records for this Name";
                    fail.IsVisible = true;
                    await ((View)fail).FadeTo(0, 2000, Easing.Linear);
                    fail.IsVisible = false;
                    await ((View)fail).FadeTo(1, 1, Easing.Linear);
                }
                else
                {
                   
                    await Navigation.PushAsync(new RecordsPage(list, MyDB));
                    Name.Text = "";
                }
            }
        }

        private async void Button1_Clicked(object sender, EventArgs e)
        {
            var list = MyDB.GetRecords(Dates.Date);
            if (list.Count() == 0)
            {
                fail.Text = "No Records for this Date";
                fail.IsVisible = true;
                await ((View)fail).FadeTo(0, 2000, Easing.Linear);
                fail.IsVisible = false;
                await ((View)fail).FadeTo(1, 1, Easing.Linear);
            }
            else
            {
                await Navigation.PushAsync(new RecordsPage(list, MyDB));
            }
        }

       
    }
}
