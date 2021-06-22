using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using SimplyDB.Model;

namespace SimplyDB.ViewModel
{
    public class DatabaseViewModel : INotifyPropertyChanged
    {
        //--------------------------------------- Data Members ---------------------------------------
        private DatabaseModel DatabaseModelObject { get; set; } // Model Instance
        public DatabaseViewModel()
        {
            DatabaseModelObject = new DatabaseModel();
        }

        private string dbName; // DB Name
        public string DbName
        {
            get { return dbName; }
            set { dbName = value; OnPropertyChanged("DbName"); }
        }

        private string dbPath; // DB Path
        public string DbPath
        {
            get { return dbPath; }
            set { dbPath = value; OnPropertyChanged("DbPath"); DatabaseModelObject.ConnString=value; }
        }

        private string selectedTable; // Selected Table
        public string SelectedTable
        {
            get { return selectedTable; }
            set { selectedTable = value; }
        }

        private string selectedView; // Selected View
        public string SelectedView
        {
            get { return selectedView; }
            set { selectedView = value; }
        }

        private List<string> tableNames; // Table Names
        public List<string> TableNames
        {
            get { return tableNames; }
            set { tableNames = value; OnPropertyChanged("TableNames"); }
        }
        
        private List<string> viewNames; // View Names
        public List<string> ViewNames
        {
            get { return viewNames; }
            set { viewNames = value; OnPropertyChanged("ViewNames"); }
        }
        
        private DataTable tableInfoData; // DATA TABLE - TABLE INFO
        public DataTable TableInfoData
        {
            get { return tableInfoData; }
            set { tableInfoData = value; OnPropertyChanged("TableInfoData"); }
        }

        private DataTable tableContentData; // DATA TABLE - TABLE CONTENT
        public DataTable TableContentData
        {
            get { return tableContentData; }
            set { tableContentData = value; OnPropertyChanged("TableContentData"); }
        }

        private DataTable createTableInfo; // DATA TABLE - FOR NEW TABLE CREATION
        public DataTable CreateTableInfo
        {
            get { return createTableInfo; }
            set { createTableInfo = value; OnPropertyChanged("CreateTableInfo"); }
        }

        private List<string> selectList; // selectList for Select Menu
        public List<string> SelectList
        {
            get { return selectList; }
            set { selectList = value; }
        }

        private List<string> fromList; // fromList for Select Menu
        public List<string> FromList
        {
            get { return fromList; }
            set { fromList = value; }
        }

        private bool isLocked = false; // Locking flag
        public bool IsLocked
        {
            get { return isLocked; }
            set { isLocked = value; OnPropertyChanged("IsLocked"); }
        }

        private FileStream fileStream; // Locking object
        public FileStream FileStream
        {
            get { return fileStream; }
            set { fileStream = value; }
        }

        private string customQuery; // Holds Custom Query
        public string CustomQuery
        {
            get { return customQuery; }
            set { customQuery = value; }
        }

        private SQLiteDataAdapter sdaInfo; // Data adapters
        private SQLiteDataAdapter sdaContent;


        // --------------------------------------- Methods ---------------------------------------

        // Orange
        public bool LockFile()
        {
            if (IsLocked)
            {
                FileStream.Close();
                MessageBox.Show("Database has been Unlocked");
            }
            else
            {
                FileStream = new FileStream(dbPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
                MessageBox.Show("Database has been Locked");
            }
            IsLocked = !IsLocked;
            return IsLocked;
        }

        public void Refresh()
        {
            if (DbPath != null)
            {
                this.GetTableNames();
                this.GetViewNames();
                this.GetTableInfo();
                this.GetTableData();
            }
        }

        // Green Methods
        public void CreateWindow()
        {
            SaveFileDialog fileSaveObject = new SaveFileDialog();
            fileSaveObject.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // Initial Path: Desktop
            fileSaveObject.Title = "Create DB Dialog";
            fileSaveObject.Filter = "Database File|*.db";
            if (fileSaveObject.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (File.Exists(fileSaveObject.FileName)) // in case overwrite saving
                {
                    File.Delete(fileSaveObject.FileName);
                }
                this.DbName = Path.GetFileName(fileSaveObject.FileName);
                this.DbPath = fileSaveObject.FileName;
            }
            else { MessageBox.Show("The database hasn't been created."); }
        }

        public void BackupWindow()
        {
            if (this.DbPath != null)
            {
                SaveFileDialog fileSaveObject = new SaveFileDialog();
                fileSaveObject.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // Initial Path: Desktop
                fileSaveObject.Title = "Backup DB Dialog";
                fileSaveObject.Filter = "Database File|*.db";
                if (fileSaveObject.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    System.IO.File.Copy(this.DbPath, fileSaveObject.FileName);
                    MessageBox.Show("Backup Done");
                }
                else { MessageBox.Show("Backup Cancelled"); }
            }
            else { MessageBox.Show("Backup Unavailable"); }
        }

        public void DeleteWindow()
        {
            if (this.dbPath != null)
            {
                var result = MessageBox.Show("Are you sure you want to delete the selected Database?", "Deletion Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    File.Delete(this.DbPath);
                    MessageBox.Show("Database has been deleted");
                }
                else
                {
                    MessageBox.Show("No action performed");
                }
            }
        }

        public void LoadWindow()
        {
            OpenFileDialog fileOpenObject = new OpenFileDialog();
            fileOpenObject.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // Initial Path: Desktop
            fileOpenObject.Filter = "Database File|*.db";
            fileOpenObject.Title = "Load DB Dialog";
            if (fileOpenObject.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DbName = Path.GetFileName(fileOpenObject.FileName);
                this.DbPath = fileOpenObject.FileName;
                MessageBox.Show("Database has been loaded");
            }
        }

        // Yellow
        public void ClearCreateTable()
        {
            this.CreateTableInfo = new DataTable(); // reset list
            this.CreateTableInfo.Columns.Add("name");
            this.CreateTableInfo.Columns.Add("type");
            this.CreateTableInfo.Columns.Add("pk").DefaultValue = "False";
            this.CreateTableInfo.Columns.Add("notnull").DefaultValue = "False";
            this.CreateTableInfo.Columns.Add("dfltvalue").DefaultValue = "";
        }

        public void CreateTable(string tableName)
        {
            string query = "";
            bool cFlag = true; // flag for , (comma)

            query += "CREATE TABLE IF NOT EXISTS " + tableName + "("; // BUILD CREATE TABLE QUERY
            foreach (DataRow row in CreateTableInfo.Rows)
            {
                if (cFlag)
                    cFlag = false;
                else
                    query += ",";
                query += row["name"] + " ";
                query += row["type"] + " ";
                if ((string)row["pk"] == "True")
                    query += "PRIMARY KEY ";
                if ((string)row["notnull"] == "True")
                    query += "NOT NULL ";
                if ((string)row["dfltvalue"] != "")
                    query += "DEFAULT '" + row["dfltvalue"] + "'";
            }
            query += ")";
            MessageBox.Show(query);
            DatabaseModelObject.CreateTable(query);
            OnPropertyChanged("TableNames");
        }

        public void DeleteTable()
        {
            if (this.SelectedTable != null)
            {
                var result = MessageBox.Show("Are you sure you want to delete the selected Table?", "Deletion Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string query = $"DROP TABLE IF EXISTS {this.SelectedTable}";
                    DatabaseModelObject.executeNonQuery(query);
                    MessageBox.Show("Table has been deleted");
                    this.SelectedTable = null;
                }
                else
                {
                    MessageBox.Show("No action performed");
                }
            }
        }

        public void CreateView(string viewName, string viewQuery)
        {
            string query = $"CREATE VIEW {viewName} AS {viewQuery}";
            DatabaseModelObject.executeNonQuery(query);
            this.Refresh();
        }

        public void DeleteView()
        {
            // are you sure you want to delete? yes / no
            if (this.SelectedView != null)
            {
                var result = MessageBox.Show("Are you sure you want to delete the selected View?", "Deletion Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string query = $"DROP VIEW IF EXISTS {this.SelectedView}";
                    DatabaseModelObject.executeNonQuery(query);
                    MessageBox.Show("View has been deleted");
                    this.SelectedView = null;
                }
                else
                {
                    MessageBox.Show("No action performed");
                }
            }
        }

        // Tables functions
        public void GetTableNames()
        {
            string query = "SELECT name FROM sqlite_master WHERE type = 'table' ORDER BY 1";
            tableNames = new List<string>();
            DatabaseModelObject.executeQueryList(query, ref tableNames);
            OnPropertyChanged("TableNames");
        }

        public void GetViewNames()
        {
            string query = "SELECT name FROM sqlite_master WHERE type = 'view' ORDER BY 1";
            viewNames = new List<string>();
            DatabaseModelObject.executeQueryList(query, ref viewNames);
            OnPropertyChanged("ViewNames");
        }

        public void GetTableInfo() // Info Table
        {
            if (this.SelectedTable!=null)
            {
                string query = $"SELECT * FROM PRAGMA_TABLE_INFO('{this.SelectedTable}')";
                DatabaseModelObject.executeQuery(query, ref tableInfoData, ref sdaInfo);
                OnPropertyChanged("TableInfo");
            }
        }

        public void GetTableData() // Content Table
        {
            if (this.SelectedTable != null)
            {
                string query = $"SELECT * FROM {this.SelectedTable}";
                DatabaseModelObject.executeQuery(query, ref tableContentData, ref sdaContent);
                OnPropertyChanged("TableContentData");
            }
        }

        public void GetViewTableData() // Content Table
        {
            string query = $"SELECT * FROM [{this.SelectedView}]";
            DatabaseModelObject.executeQuery(query, ref tableContentData, ref sdaContent);
            OnPropertyChanged("TableContentData");
        }


        public void EditTableName(string newTable)
        {
            string query = $"ALTER TABLE {this.SelectedTable} RENAME TO {newTable};";
            DatabaseModelObject.executeNonQuery(query);
            this.SelectedTable = newTable;
            this.Refresh();
        }

        public void DeleteTableColumn(string colName) // Delete Column
        {
            DatabaseModelObject.DeleteTableColumn(this.SelectedTable, colName);
            this.Refresh();
        }

        public void ApplyCellUpdate(string oldName, string newName) // Rename Column
        {
                string query = $"ALTER TABLE {this.SelectedTable} RENAME COLUMN {oldName} TO {newName} ";
                DatabaseModelObject.executeNonQuery(query);
        }

        public void CreateColumn(string colName, string colType, bool colNotNull, string colDefaultValue) // Create Column
        {
            if (this.SelectedTable!=null)
            {
                string query = ""; // Create Query
                if (colNotNull == false && colDefaultValue == "")
                    query = $"ALTER TABLE {this.SelectedTable} ADD {colName} {colType}";
                else if (colNotNull == false && colDefaultValue != "")
                    query = $"ALTER TABLE {this.SelectedTable} ADD {colName} {colType} DEFAULT '{colDefaultValue}'";
                else
                    query = $"ALTER TABLE {this.SelectedTable} ADD {colName} {colType} NOT NULL DEFAULT '{colDefaultValue}'";

                MessageBox.Show(query);
                DatabaseModelObject.executeNonQuery(query);
            }
        }

        public void ContentSave() // Save Changes for Content Table
        {
            DatabaseModelObject.ContentSave(this.SelectedTable, ref tableContentData, ref sdaContent);
        }

        public void SubmitCustomQuery() // Content Table
        {
            DatabaseModelObject.executeQuery(customQuery, ref tableContentData, ref sdaContent);
            OnPropertyChanged("TableContentData");
        }

        // Select functions
        public void GetFromList()
        {
            fromList = new List<string>();
            string query = "SELECT name FROM sqlite_master WHERE type = 'table' ORDER BY 1";
            DatabaseModelObject.executeQueryList(query, ref fromList);
        }
        
        public void GetSelectList(List<string> fromObj)
        {
            selectList = new List<string>();
            string query = "";
            foreach (string obj in fromObj)
            {
                SelectList.Add(obj);
            }
            query = "PRAGMA table_info({tableName})";
            DatabaseModelObject.executeQueryList(query, ref selectList);
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}