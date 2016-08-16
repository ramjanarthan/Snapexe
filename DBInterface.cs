
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesseractDemo.ClassAndInterface
{
    public interface DBInterface
    {
        SQLiteConnection GetConnection();
    }
}
