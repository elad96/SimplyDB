using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
using System.Data;
using Microsoft.Data.Sqlite;

namespace SimplyDB.Model
{
    public class DatabaseModel
    {
        private SQLiteConnection myConnection;
        private SQLiteCommand cmd;
        private SQLiteDataReader result;

        private string connString;
        public string ConnString // set by db path
        {
            get { return connString; }
            set { connString = $"Data Source={value}; Version=3"; }
        }

        public void OpenConnection()
        {
            if(ConnString!=null)
            {
                myConnection = new SQLiteConnection(connString);
                myConnection.Open();
            }
            
        }

        public void CloseConnection()
        {
            if (myConnection!=null)
            {
                myConnection.Close();
                myConnection.Dispose();
            }
        }

        public void executeNonQuery(string query)
        {
            this.OpenConnection();
            this.cmd = new SQLiteCommand(query, myConnection);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("NonQuery executed");
            }
            catch
            {
                MessageBox.Show("NonQuery not executed");
            }
            finally
            {
                cmd.Dispose();
                this.CloseConnection();
            }
        }

        public void executeQuery(string query, ref DataTable tableData, ref SQLiteDataAdapter sda)
        {
            this.OpenConnection();
            this.cmd = new SQLiteCommand(query, myConnection);
            sda = new SQLiteDataAdapter(cmd);
            tableData = new DataTable();
            
            try
            {
                sda.Fill(tableData);
            }
            catch
            {
                MessageBox.Show("Query not executed");
            }
            finally
            {
                cmd.Dispose();
                this.CloseConnection();
            }
        }

        public void executeQueryList(string query, ref List<string> strList)
        {
            strList.Clear();
            this.OpenConnection();
            SQLiteCommand cmd = new SQLiteCommand(query, myConnection);
            try
            {
                this.result = cmd.ExecuteReader();
                while (this.result.Read())
                {
                    strList.Add(this.result["name"].ToString());
                }
                result.Close();
            }
            catch
            {
                MessageBox.Show("Could not load tables list");
            }
            finally
            {
                cmd.Dispose();
                this.CloseConnection();
            }
        }

        public void ContentSave(string tableName, ref DataTable tableData, ref SQLiteDataAdapter sda)
        {
            if (tableName != null)
            {
                SQLiteCommandBuilder sqCommandBuilder = new SQLiteCommandBuilder(sda);
                this.OpenConnection();
                sda.SelectCommand = new SQLiteCommand($"SELECT * FROM {tableName} ", myConnection);
                try
                {
                    sda.Update(tableData);
                }
                catch
                {
                    MessageBox.Show("Couldn't save table content");
                }
                this.CloseConnection();
            }
        }

        public void CreateTable(string query)
        {
            this.OpenConnection();
            SQLiteCommand cmd = new SQLiteCommand(query, myConnection);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Table Successfully created");
            }
            catch
            {
                MessageBox.Show("Table could not be created");
            }
            finally
            {
                cmd.Dispose();
                this.CloseConnection();
            }
        }

        public void DeleteTableColumn(string tableName, string columnToRemove) // Delete Table Column Support for SQLite
        {
            try
            {
                this.OpenConnection();
                // Reads all columns definitions from table
                List<string> columnDefinition = new List<string>();
                var mSql = $"SELECT type, sql FROM sqlite_master WHERE tbl_name='{tableName}'";
                var mSqliteCommand = new SQLiteCommand(mSql, myConnection);
                string sqlScript = "";
                SQLiteDataReader mSqliteReader;
                using (mSqliteReader = mSqliteCommand.ExecuteReader())
                {
                    while (mSqliteReader.Read())
                    {
                        sqlScript = mSqliteReader["sql"].ToString();
                        break;
                    }
                }
                if (!string.IsNullOrEmpty(sqlScript))
                {
                    // Gets string within first '(' and last ')' characters
                    int firstIndex = sqlScript.IndexOf("(");
                    int lastIndex = sqlScript.LastIndexOf(")");
                    if (firstIndex >= 0 && lastIndex <= sqlScript.Length - 1)
                    {
                        sqlScript = sqlScript.Substring(firstIndex, lastIndex - firstIndex + 1);
                    }
                    string[] scriptParts = sqlScript.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string s in scriptParts)
                    {
                        if (!s.Contains(columnToRemove))
                        {
                            columnDefinition.Add(s);
                        }
                    }
                }
                string columnDefinitionString = string.Join(",", columnDefinition);
                // Reads all columns from table
                List<string> columns = new List<string>();
                mSql = $"PRAGMA table_info({tableName})";
                mSqliteCommand = new SQLiteCommand(mSql, myConnection);
                using (mSqliteReader = mSqliteCommand.ExecuteReader())
                {
                    while (mSqliteReader.Read()) columns.Add(mSqliteReader["name"].ToString());
                }
                columns.Remove(columnToRemove);
                string columnString = string.Join(",", columns);
                mSql = "PRAGMA foreign_keys=OFF";
                mSqliteCommand = new SQLiteCommand(mSql, myConnection);
                int n = mSqliteCommand.ExecuteNonQuery();
                // Removes a column from the table
                using (SQLiteTransaction tr = myConnection.BeginTransaction())
                {
                    using (SQLiteCommand cmd = myConnection.CreateCommand())
                    {
                        cmd.Transaction = tr;
                        string query = $"CREATE TEMPORARY TABLE {tableName}_backup {columnDefinitionString}";
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = $"INSERT INTO {tableName}_backup SELECT {columnString} FROM {tableName}";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = $"DROP TABLE {tableName}";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = $"CREATE TABLE {tableName} {columnDefinitionString}";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = $"INSERT INTO {tableName} SELECT {columnString} FROM {tableName}_backup;";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = $"DROP TABLE {tableName}_backup";
                        cmd.ExecuteNonQuery();
                    }
                    tr.Commit();
                }
                mSql = "PRAGMA foreign_keys=ON";
                mSqliteCommand = new SQLiteCommand(mSql, myConnection);
                n = mSqliteCommand.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show(columnToRemove + " could not be removed.");
            }
        }

    }
}
