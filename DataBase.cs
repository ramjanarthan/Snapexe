using SQLite;
using SQLite.Net;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TesseractDemo.ClassAndInterface
{
   
    public class DataBase
    {
        
        private SQLiteConnection _connection;
       
        private string IToMonth(string num)
        {
            switch (num)
            {
                case "01": return "January";
                case "02": return "February";
                case "03": return "March";
                case "04": return "April";
                case "05": return "May";
                case "06": return "June";
                case "07": return "July";
                case "08": return "August";
                case "09": return "September";
                case "10": return "October";
                case "11": return "November";
                case "12": return "December";
                default: return "Invalid";
            }
        }

       
        public DataBase()
        {
            _connection = DependencyService.Get<DBInterface>().GetConnection();
            _connection.CreateTable<DBClass>();
        }

        public IEnumerable<DBClass> GetCategories()
        {
            IEnumerable<DBClass> list = (from record in _connection.Table<DBClass>() where record.ParentID.Equals(-1) select record);
            list = list.Where(x => GetRecords(x.ID).Count() != 0);
            
            if(list.Count() > 0)
            {
                return list;
            }else
            {
                return null;
            }
           
           
        }

        public List<string> GetMonths()
        {
            List<DBClass> list = new List<DBClass>(GetAll());
            List<string> months = new List<string>();

            for(int i = 0; i < list.Count(); i++)
            {
                DBClass x = list.ElementAt(i);
                if(!(x.ParentID == -1))
                {
                    string I = "" + "" + x.Date[3] + x.Date[4];
                    string mAndy =  IToMonth(I) +  ", " + x.Date.Substring(6);
                    if (months.Contains(mAndy))
                    {
                        continue;
                    }else
                    {
                        months.Add(mAndy);
                    }

                }

            }

            if(months.Count() > 0)
            {
                return months;
            }else
            {
                return null;
            }
            
        }

        public IEnumerable<DBClass> GetAll()
        {
            return (from record in _connection.Table<DBClass>() select record);

        }

        public IEnumerable<DBClass> GetRecords(int parentID)
        {
            var i = parentID;
            return(from record in _connection.Table<DBClass>() where record.ParentID.Equals(parentID) select record);
         
        }

        public IEnumerable<DBClass> GetRecordsName(string name)
        {
            IEnumerable<DBClass> list = (from record in _connection.Table<DBClass>() where record.ParentID != -1 select record);
            list = list.Where(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            return list;

        }

        public List<DBClass> GetRecords(string MAndY)
        {

            List<DBClass> list = new List<DBClass>(from record in _connection.Table<DBClass>() where record.ParentID != -1 select record);

            for (int i = 0; i < list.Count(); i++)
            {
                DBClass x = list.ElementAt(i);
                    string I = "" + "" + x.Date[3] + x.Date[4];
                    string mAndy = IToMonth(I) + ", " + x.Date.Substring(6);
                    if (MAndY.Equals(mAndy))
                    {
                        continue;
                    }
                    else
                    {
                        list.Remove(list.ElementAt(i));
                        i--;
                    }
              
            }

            return list;
            

        }




        public void DeleteRecord(int id)
        {
            _connection.Delete<DBClass>(id);
        }

        public void Clear()
        {
            _connection.DeleteAll<DBClass>();
        }

        public IEnumerable<DBClass> GetRecords(DateTime date)
        {
            string Date1 = date.ToString("dd/MM/yyyy");
            return (from record in _connection.Table<DBClass>() where record.Date.Equals(Date1) select record);

        }


        public void AddRecord(int parentID, string name, double amount, DateTime date)
        {
            var record = new DBClass()
            {
                Name = name,
                Amount = amount,
                ParentID = parentID,
                Date = date.ToString("dd/MM/yyyy")

            };
            _connection.Insert(record);
        }

        public DBClass FindRecord(string name)
        {
            var rec = from record in _connection.Table<DBClass>() where record.Name.Equals(name) select record;
            if(rec.Count() == 0)
            {
                return null;
            }else
            {
                return rec.First();
            }
          
        }

        
    }
}
