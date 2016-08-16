using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

using System.IO;
using SQLite.Net;
using TesseractDemo.ClassAndInterface;
using TesseractDemo.Droid;

[assembly: Dependency(typeof(DBConnection))]

namespace TesseractDemo.Droid
{
    class DBConnection : DBInterface

    {

        public SQLiteConnection GetConnection()
        {
            

            string dbPath = Path.Combine(
                        System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                         "TesseractDemoRecords.db3");
          
            return new SQLiteConnection(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), dbPath);
           
        }
    }
}