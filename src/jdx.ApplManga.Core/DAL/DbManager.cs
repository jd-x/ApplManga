using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace jdx.ApplManga.Core.DAL {
    public class DbManager {
        private string _dbPath = string.Empty;
        public string DatabasePath {
            get {
                if (string.IsNullOrEmpty(_dbPath)) {
                    // TODO: Use relative path
                    _dbPath = Path.Combine("D:/Dev/Github/ApplManga/src/jdx.ApplManga/bin/Debug", "ApplMangaDB.db");
                }

                return _dbPath;
            }

            set {
                _dbPath = value;
            }
        }

        private string _connectionString = string.Empty;
        public string ConnectionString {
            get {
                if (string.IsNullOrEmpty(_connectionString)) {
                    _connectionString = string.Format("Data Source={0}; Version=3;", DatabasePath);
                }

                return _connectionString;
            }
        }

        private SQLiteConnection _dbConnection;
        public SQLiteConnection DbConnection {
            get {
                return _dbConnection = new SQLiteConnection(ConnectionString);
            }
        }

        public DbManager() {
        }

        public DbManager(string dbPath) {
            DatabasePath = dbPath;
        }

        public void CreateDb() {
            if (!File.Exists(DatabasePath)) {
                SQLiteConnection.CreateFile(DatabasePath);
            }
        }

        public bool IsDbConnectionOpen() {
            using (SQLiteConnection dbConnection = new SQLiteConnection(ConnectionString)) {
                dbConnection.Open();

                if (dbConnection != null && dbConnection.State == ConnectionState.Closed) {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Executes provided SQL query
        /// </summary>
        /// <param name="query">SQL query to be executed</param>
        /// <param name="parameters">Query parameters with values</param>
        /// <returns>Number of rows affected</returns>
        public int ExecuteCommand(string query, Dictionary<string, object> parameters = null) {
            return ExecuteCommand(ConnectionString, query, parameters);
        }

        /// <summary>
        /// Executes provided SQL query
        /// </summary>
        /// <param name="connectionString">Database connection string</param>
        /// <param name="query">SQL query to be executed</param>
        /// <param name="parameters">Query parameters with values</param>
        /// <returns>Number of rows affected</returns>
        private int ExecuteCommand(string connectionString, string query, Dictionary<string, object> parameters = null) {
            using (SQLiteConnection dbConnection = new SQLiteConnection(ConnectionString)) {
                using (SQLiteCommand dbCommand = new SQLiteCommand(query)) {
                    // Add parameters
                    if (parameters != null) {
                        foreach (KeyValuePair<string, object> kvp in parameters) {
                            SQLiteParameter dbParameter = new SQLiteParameter();
                            dbParameter.ParameterName = kvp.Key;
                            dbParameter.Value = kvp.Value;
                            dbCommand.Parameters.Add(dbParameter);
                        }
                    }

                    // Execute query
                    dbConnection.Open();
                    return dbCommand.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Returns queried data
        /// </summary>
        /// <param name="query">SELECT query</param>
        /// <param name="parameters">Query parameters with values</param>
        /// <returns>Query results as a DataTable object</returns>
        public DataTable Select(string query, Dictionary<string, object> parameters = null) {
            return Select(ConnectionString, query, parameters);
        }

        /// <summary>
        /// Returns queried data
        /// </summary>
        /// <param name="connectionString">Database connection string</param>
        /// <param name="query">SELECT query</param>
        /// <param name="parameters">Query parameters with values</param>
        /// <returns>Query results as a DataTable object</returns>
        private DataTable Select(string connectionString, string query, Dictionary<string, object> parameters = null) {
            DataTable dt = new DataTable();

            using (SQLiteConnection dbConnection = new SQLiteConnection(ConnectionString)) {
                using (SQLiteDataAdapter dbDataAdapter = new SQLiteDataAdapter(query, dbConnection))
                using (SQLiteCommand dbCommand = new SQLiteCommand(query)) {
                    // Add parameters
                    if (parameters != null) {
                        foreach (KeyValuePair<string, object> kvp in parameters) {
                            SQLiteParameter dbParameter = new SQLiteParameter();
                            dbParameter.ParameterName = kvp.Key;
                            dbParameter.Value = kvp.Value;
                            dbCommand.Parameters.Add(dbParameter);
                        }
                    }

                    // Execute query
                    dbConnection.Open();
                    dbDataAdapter.Fill(dt);
                    return dt;
                }
            }
        }
    }
}
