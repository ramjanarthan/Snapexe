using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesseractDemo.ClassAndInterface
{
    public class DBClass
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public int ParentID { get; set; }
        public double Amount { get; set; }
        public string Date { get; set; }

        public DBClass()
        {
            ID = -1;
            Name = "Default";
            ParentID = -1;
            Amount = 0;
            Date = (new DateTime(1997, 07, 20)).ToString();
           
        }

        public DBClass(int id, string name, int parentid, double amount, DateTime date)
        {
            ID = id;
            Name = name;
            ParentID = parentid;
            Amount = amount;
            Date = date.ToString("dd MM, yyyy");

        }

    }
}
