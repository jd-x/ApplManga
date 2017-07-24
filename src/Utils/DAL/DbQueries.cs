using System.Data.SQLite;

namespace ApplManga.DAL.Utils {
    public class DbQueries {
        public void CreateTables() {
            DbManager dbManager = new DbManager();

            using (SQLiteConnection dbConnection = new SQLiteConnection(dbManager.ConnectionString)) {
                //dbManager.ExecuteCommand(@"CREATE TABLE If NOT EXISTS `MangaList` ( `Title` TEXT NOT NULL UNIQUE, `Site` TEXT NOT NULL, PRIMARY KEY(`Title`) )")
            }
        }
    }
}
