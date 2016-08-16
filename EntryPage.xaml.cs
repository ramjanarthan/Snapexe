using TesseractDemo.ClassAndInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TesseractDemo.Pages
{
    public partial class EntryPage : ContentPage
    {

        public DataBase _db;
        public EntryPage(DataBase DB)
        {
            InitializeComponent();
            _db = DB;
            IEnumerable<DBClass> cats = _db.GetCategories();
            CategPicker.Items.Add("Choose a Category");
            CategPicker.SelectedIndex = 0;
            if (cats != null)
            {
                for (int i = 0; i < cats.Count(); i++)
                {
                    CategPicker.Items.Add(cats.ElementAt(i).Name);
                }
            }

            CategPicker.SelectedIndexChanged += CategPicker_SelectedIndexChanged;

            button.Clicked += Button_Clicked;
            button2.Clicked += Button2_Clicked;

            

            MessagingCenter.Subscribe<CameraPage, string>(this, "textDecoded", (sender, arg) =>
            {
                string line1 = arg.Split(new[] { '\r', '\n' }).FirstOrDefault();
                string line2 = arg.Split(new[] { '\r', '\n' }).Last();
                string numbers = "0123456789";
                List<char> number = new List<char>();
                bool isfound = false;
                for(int i = 0; i <line2.Length; i ++)
                {
                    if (isfound)
                    {
                        if(numbers.Contains(line2[i].ToString()) || line2[i].Equals('.') || line2[i].Equals(','))
                        {
                            number.Add(line2[i]);
                        }
                        else
                        {
                            break;
                        }
                        
                    }else
                    {
                        if (numbers.Contains(line2[i].ToString())){
                            isfound = true;
                            number.Add(line2[i]);
                        }
                        
                    }


                }
                numbers = "";
                for(int i = 0; i < number.Count(); i++)
                {
                    numbers += number[i].ToString();
                }
                Name.Text = line1;
                Amount.Text = numbers;

            });
        }

        private void CategPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(CategPicker.SelectedIndex <= 0))
            {
                Category.Text = CategPicker.Items.ElementAt(CategPicker.SelectedIndex);
                CategPicker.SelectedIndex = 0;
            }

        }

        private void Button2_Clicked(object sender, EventArgs e)
        {
            Name.Text = null;
            Amount.Text = null;
            Category.Text = null;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string _name = Name.Text, _amount = Amount.Text, _cat = Category.Text;

            if (_name == null || _amount == null || _cat == null || _name.Trim() == "" || _amount.Trim() == "" || _cat.Trim() == "")
            {
                await ((View)sender).FadeTo(0, 500, Easing.Linear);
                await ((View)sender).FadeTo(1, 250, Easing.CubicIn);
            }else
            {
                double am = 0;
                bool canParse = double.TryParse(_amount, out am);

                if (canParse)
                {

                    var category = _db.FindRecord(_cat);
                    if (category != null)
                    {
                        _db.AddRecord(category.ID, Name.Text.Trim(), double.Parse(Amount.Text), Dates.Date);
                    }
                    else
                    {
                        _db.AddRecord(-1, Category.Text, 0, new DateTime(100000));
                        category = _db.FindRecord(Category.Text);
                        _db.AddRecord(category.ID, Name.Text.Trim(), double.Parse(Amount.Text), Dates.Date);
                    }


                    await Navigation.PushAsync(new RecordsPage(_db.GetRecords(category.ID), _db));
                    Name.Text = null;
                    Amount.Text = null;
                    Category.Text = null;

                    IEnumerable<DBClass> cats = _db.GetCategories();
                    CategPicker.Items.Clear();
                    CategPicker.Items.Add("Choose a Category");
                    CategPicker.SelectedIndex = 0;
                    if(cats != null)
                    {
                        for (int i = 0; i < cats.Count(); i++)
                        {
                            CategPicker.Items.Add(cats.ElementAt(i).Name);
                        }

                    }
                    
                    CategPicker.SelectedIndex = 0;
                }else
                {
                    await DisplayAlert("Error", "Please enter a valid Amount", "OK");
                }

               
            }
        }

       
    }
}
