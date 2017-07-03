using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ApplManga.ViewModels {
    public class DbManager {
        public string ConnString { get; protected set; }

        public DbManager(string connString) {
            this.ConnString = connString;
        }

        public void ConnectToDb(string dbName) {

        }
    }
}
