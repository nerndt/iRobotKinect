
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
//using Acrobat;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace iRobotKinect
{
    public static class IOTools
    {
        // Loads all known types into memory
        public static List<string> DataTypes = new List<string>()
        {
            "All",
            "Plates",
            "Wells",
            "Channels",
            "PlateTemplates"
        };

        public static DialogResult OpenCSVFileDlg(string FileName)
        {
            DialogResult result = DialogResult.OK;
            if (FileName == "")
            {
                // Bring Up Save Dialog and Export the DataGridView Info
                OpenFileDialog dlg;

                string OutputExportTableFilename = AppDomain.CurrentDomain.BaseDirectory + "Setup\\" + Path.GetFileNameWithoutExtension(FileName) + ".csv";

                // Configure save file dialog box
                dlg = new OpenFileDialog();
                dlg.Title = "Export Table";

                dlg.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "Analysis\\"; //  System.IO.Path.GetDirectoryName(richTextBoxPlateProjectName.Text);

                // Set the default to the current plate filename with the csv extension.
                dlg.FileName = Path.GetFileNameWithoutExtension(OutputExportTableFilename); // string.Empty;

                dlg.DefaultExt = ".csv";
                dlg.Filter = "Text CSV Files|*.csv";

                dlg.FilterIndex = 1;
                dlg.RestoreDirectory = false; // true

                // Show save file dialog box
                result = dlg.ShowDialog();
                FileName = dlg.FileName;
            }
            return result;
        }

        public static DialogResult SaveCSVFileDlg(string FileName)
        {
            DialogResult result = DialogResult.OK;

            if (FileName == "")
            {
                // Bring Up Save Dialog and Export the DataGridView Info
                SaveFileDialog dlg;

                string OutputExportTableFilename = AppDomain.CurrentDomain.BaseDirectory + "Setup\\" + Path.GetFileNameWithoutExtension(FileName) + ".csv";

                // Configure save file dialog box
                dlg = new SaveFileDialog();
                dlg.Title = "Export Table";

                dlg.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "Analysis\\"; //  System.IO.Path.GetDirectoryName(richTextBoxPlateProjectName.Text);

                // Set the default to the current plate filename with the csv extension.
                dlg.FileName = Path.GetFileNameWithoutExtension(OutputExportTableFilename); // string.Empty;

                dlg.DefaultExt = ".csv";
                dlg.Filter = "Text CSV Files|*.csv";

                // Show save file dialog box
                result = dlg.ShowDialog();
                FileName = dlg.FileName;
            }

            return result;
        }

        public static bool MoveFile(string inputFile, string outputFile)
        {
            // Move the backupFileName to finalBackupFileName
            try
            {
                System.IO.File.Move(inputFile, outputFile);
            }
            catch (Exception ex)
            {
                string exception = ex.ToString();
                try
                {
                    System.IO.File.Copy(inputFile, outputFile, true); // overwrite = true
                    // Try to Copy the file and delete original!
                    System.IO.File.Delete(inputFile);
                }
                catch (Exception ex2)
                {
                    string exception2 = ex2.ToString();
                    //string message = "Unable to save plate " + backupPlateProjectFilename + "\nCheck to see if file is in use and try again";
                    //DialogResult dlgRes2 = MessageBox.Show(this, message, "Save Plate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        // Check Disk Space
        public static bool CheckDiskSpace(string Drive, long MinimumAvailableDiskSpaceInBytes, ref long AvailableDiskSpaceInBytes)
        {
            try
            {
                DriveInfo[] allDrives = DriveInfo.GetDrives();

                foreach (DriveInfo d in allDrives)
                {
                    if (d.Name.StartsWith(Drive) == false)
                        continue;

                    AvailableDiskSpaceInBytes = d.AvailableFreeSpace;

                    if (AvailableDiskSpaceInBytes < MinimumAvailableDiskSpaceInBytes)
                        return false;

                    return true;
                }
            }
            catch
            {
            }

            AvailableDiskSpaceInBytes = -1;

            return false;
        }

        public const long OneTerraByte = 1L * 1024L * 1024L * 1024L * 1024L;
        public const long OneGigaByte = 1L * 1024L * 1024L * 1024L;
        public const long OneMegaByte = 1L * 1024L * 1024L;
        public const long OneKiloByte = 1L * 1024L;

        public const string PixelError = "Pixel Error";

        public static string DiskSpaceString(long DiskSpaceValue)
        {
            // TerraByte check, return if equal or over 1 TeraByte
            if (DiskSpaceValue >= OneTerraByte)
                return string.Format("{0} TB", DisplayTools.TruncateToMaxDecimalDigits((double)DiskSpaceValue / (double)OneTerraByte, 2));

            // GigaByte check, return if equal or over 1 GigaByte
            if (DiskSpaceValue >= OneGigaByte)
                return string.Format("{0} GB", DisplayTools.TruncateToMaxDecimalDigits((double)DiskSpaceValue / (double)OneGigaByte, 2));

            // MegaByte check, return if equal or over 1 MegaByte
            if (DiskSpaceValue >= OneMegaByte)
                return string.Format("{0} MB", DisplayTools.TruncateToMaxDecimalDigits((double)DiskSpaceValue / (double)OneMegaByte, 2));

            // KiloByte check, return if equal or over 1 KiloByte
            if (DiskSpaceValue >= OneKiloByte)
                return string.Format("{0} KB", DisplayTools.TruncateToMaxDecimalDigits((double)DiskSpaceValue / (double)OneKiloByte, 2));

            // Return number of Bytes
            return string.Format("{0} Bytes", DiskSpaceValue);
        }

        public static List<string> UserDefinedParametersHeader = new List<string>();

        /*
        private void ImportSetupData()
        {
            string PlateSetupImportFileOut;
            if (ImportCSVSetupData("", out PlateSetupImportFileOut, UserDefinedParametersHeader) == false)
                return;
        }
        */

        /*
        public static bool ImportAndExportCSVSetupData(string PlateSetupImportTemplateFile, string PlateSetupImportFileOut, List<string> SamplNames, List<string> WellNames)
        {
            DataTable dataTable;
            if (ImportCSVSetupData(PlateSetupImportTemplateFile, PlateSetupImportFileOut, SamplNames, WellNames, out dataTable) == true)
            {
                if (ExportSetupData(PlateSetupImportFileOut, dataTable) == true)
                {
                    // Copy the .csv file to the Template directory
                    File.Copy(MainForm.CRMainForm.SetupInfo.TemplateDirectory + "\\" + Path.GetFileNameWithoutExtension(PlateSetupImportFileOut) + ".csv", MainForm.CRMainForm.SetupInfo.TemplateDirectory + "\\" + Path.GetFileNameWithoutExtension(PlateSetupImportFileOut) + ".csv", true);
                    return true;
                }
            }
            return false;
        }

        public static bool ImportAndExportCSVSetupData(string PlateSetupImportTemplateFile, string PlateSetupImportFileOut, string[] SamplNames)
        {
            DataTable dataTable;
            if (ImportCSVSetupData(PlateSetupImportTemplateFile, PlateSetupImportFileOut, SamplNames, out dataTable) == true)
            {
                if (ExportSetupData(PlateSetupImportFileOut, dataTable) == true)
                {
                    // Copy the .csv file to the Template directory
                    File.Copy(MainForm.CRMainForm.SetupInfo.TemplateDirectory + "\\" + Path.GetFileNameWithoutExtension(PlateSetupImportFileOut) + ".csv", MainForm.CRMainForm.SetupInfo.TemplateDirectory + "\\" + Path.GetFileNameWithoutExtension(PlateSetupImportFileOut) + ".csv", true);
                    return true;
                }
            }
            return false;
        }
        */

        public static bool WriteDataTable(StreamWriter sw, DataTable table)
        {
            try
            {
                string columnNames = "";
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    if (i == 0)
                    {
                        columnNames += table.Columns[i].ColumnName;
                    }
                    else
                    {
                        columnNames += "," + table.Columns[i].ColumnName;
                    }
                }
                sw.WriteLine(columnNames);

                foreach (DataRow row in table.Rows)
                {
                    sw.WriteLine(String.Join(",", row.ItemArray));
                    //foreach (DataColumn column in table.Columns)
                    //{
                    //    sw.WriteLine(row[column]);
                    //}
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        /*
        public static bool WriteDataTable(StreamWriter sw, DataTable dt)
        {
            try
            {
                int[] maxLengths = new int[dt.Columns.Count];

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    maxLengths[i] = dt.Columns[i].ColumnName.Length;

                    foreach (DataRow row in dt.Rows)
                    {
                        if (!row.IsNull(i))
                        {
                            int length = row[i].ToString().Length;

                            if (length > maxLengths[i])
                            {
                                maxLengths[i] = length;
                            }
                        }
                    }
                }

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i == dt.Columns.Count - 1)
                    {
                        sw.Write(dt.Columns[i].ColumnName.PadRight(maxLengths[i] + 2));
                    }
                    else
                    {
                        sw.Write(dt.Columns[i].ColumnName.PadRight(maxLengths[i] + 2) + ",");
                    }
                }

                sw.WriteLine();

                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (!row.IsNull(i))
                        {
                            if (i == dt.Columns.Count - 1)
                            {
                                sw.Write(dt.Columns[i].ColumnName.PadRight(maxLengths[i] + 2));
                            }
                            else
                            {
                                sw.Write(dt.Columns[i].ColumnName.PadRight(maxLengths[i] + 2) + ",");
                            }
                            sw.Write(row[i].ToString().PadRight(maxLengths[i] + 2));
                        }
                        else
                        {
                            sw.Write(new string(',', maxLengths[i] + 2));
                        }
                    }

                    sw.WriteLine();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        */

        public static bool CSVToList(string FileName, List<string> list, out string rowHeaderName, bool includeHeader = false)
        {
            rowHeaderName = "";

            try
            {
                // Read the file line-wise into List
                using (var streamReader = new StreamReader(FileName, Encoding.Default))
                {
                    if (includeHeader == true)
                    {
                        rowHeaderName = streamReader.ReadLine();
                    }
                    while (!streamReader.EndOfStream)
                    {
                        list.Add(streamReader.ReadLine());
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool ListToCSVFile(string FileName, List<string> list, string rowHeaderName = "", bool includeHeader = false)
        {
            try
            {
                using (var streamWriter = new StreamWriter(FileName)) // using (var streamWriter = new StreamWriter(filePath, false, Encoding.Default))
                {
                    if (includeHeader == true)
                    {
                        streamWriter.WriteLine(rowHeaderName);
                    }
                    foreach (string line in list)
                    {
                        streamWriter.WriteLine(line);
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool DataGridViewToCSVFile(string FileName, List<string> ObjectTypes, DataGridView dgv, string rowHeaderName = "", bool showRowHeader = true) // Write to CSV File - later can use database
        {
            bool ShowApplication = MainForm.ShowApplication;
            DialogResult result = IOTools.SaveCSVFileDlg(FileName);

            // Export the table data to an output file
            if (result == DialogResult.OK)
            {
                if (System.IO.File.Exists(FileName) == true)
                {
                    FileAttributes fileAttributes = File.GetAttributes(FileName);
                    FileStream currentWriteableFile = null;
                    if ((fileAttributes & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
                    { // Make sure that the file is NOT read-only 
                        try
                        {
                            currentWriteableFile = File.OpenWrite(FileName);
                        }
                        catch
                        {
                            if (ShowApplication == true)
                            {
                                string message = "Could not export data to file '" + FileName + "'. Possibly in use";
                                MessageBox.Show(message, "Export CSV", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            return false;
                        }
                    }
                    currentWriteableFile.Close();
                }

                StreamWriter sw = null;
                try
                {
                    sw = new StreamWriter(FileName, false);
                }
                catch
                {
                    if (ShowApplication == true)
                    {
                        string message = "Could not write to file '" + FileName + "'. Possibly in use";
                        DialogResult dlgRes = MessageBox.Show(message, "Export CSV", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return false;
                }

                try
                {
                    DataTable dataTable = IOTools.DataGridViewToDataTable(dgv, rowHeaderName, showRowHeader); //  GetDataTableFromDGV(dataGridView1); // GetDataTableFromDataGridView(dataGridView1); // ExtractDataTable(dataGridView1);

                    IOTools.WriteDataTable(sw, dataTable);

                }
                catch
                {
                    sw.Close();
                    if (ShowApplication == true)
                    {
                        string message = "Could not write to file '" + FileName + "'. Possibly in use";
                        DialogResult dlgRes = MessageBox.Show(message, "Export CSV", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return false;
                }

                sw.Close();

            }
            return true;
        }

        public static bool CSVFileToDataGridView(string FileName, DataGridView dgv)
        {
            bool ShowApplication = MainForm.ShowApplication;

            DialogResult result = IOTools.OpenCSVFileDlg(FileName);

            DataTable dataTable = null;
            // Export the table data to an output file
            if (result == DialogResult.OK)
            {
                if (System.IO.File.Exists(FileName) == true)
                {

                    FileAttributes fileAttributes = File.GetAttributes(FileName);
                    FileStream currentWriteableFile = null;
                    if ((fileAttributes & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
                    { // Make sure that the file is NOT read-only 
                        try
                        {
                            dataTable = CSVReader.ReadCSVFile(FileName, true, 0);
                        }
                        catch
                        {
                            if (ShowApplication == true)
                            {
                                string message = "Could not import data to file '" + FileName + "'. Possibly in use";
                                MessageBox.Show(message, "Import CSV", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            return false;
                        }
                    }
                    currentWriteableFile.Close();
                }

                try // Load data table into datagridview
                {
                    dgv.RowHeadersVisible = false;
                    dgv.ColumnCount = dataTable.Columns.Count;
                    int i, j;
                    for (i = 0; i < dgv.ColumnCount; i++)
                    {
                        dgv.Columns[i].ValueType = typeof(String);
                        dgv.Columns[i].HeaderText = dataTable.Rows[0][i].ToString();
                    }
                    for (i = 1; i < dataTable.Rows.Count; i++)
                    {
                        for (j = 0; j < dataTable.Columns.Count; j++)
                        {
                            dgv[i - 1, j].Value = dataTable.Rows[i][j].ToString();
                        }
                    }
                }
                catch
                {
                    if (ShowApplication == true)
                    {
                        string message = "Could load data";
                        DialogResult dlgRes = MessageBox.Show(message, "Import CSV", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return false;
                }
            }

            return true;
        }

        public static object[,] DataTableTo2DObjectArray(DataTable dt)
        {
            var rows = dt.Rows;
            int rowCount = rows.Count;
            int colCount = dt.Columns.Count;
            var result = new object[rowCount, colCount];

            for (int i = 0; i < rowCount; i++)
            {
                var row = rows[i];
                for (int j = 0; j < colCount; j++)
                {
                    result[i, j] = row[j];
                }
            }

            return result;
        }

        /// <summary>
        /// Convert ArrayList to List.
        /// </summary>
        public static List<T> ArrayListToList<T>(this ArrayList arrayList)
        {
            List<T> list = new List<T>(arrayList.Count);
            foreach (T instance in arrayList)
            {
                list.Add(instance);
            }
            return list;
        }

        // Puts a List in a DataTable
        public static DataTable ListToDataTable<T>(IList<T> data)
        {
            DataTable table = new DataTable();

            //special handling for value types and string
            if (typeof(T).IsValueType || typeof(T).Equals(typeof(string)))
            {

                DataColumn dc = new DataColumn("Value");
                table.Columns.Add(dc);
                foreach (T item in data)
                {
                    DataRow dr = table.NewRow();
                    dr[0] = item;
                    table.Rows.Add(dr);
                }
            }
            else
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
                foreach (PropertyDescriptor prop in properties)
                {
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
                foreach (T item in data)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                    {
                        try
                        {
                            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                        }
                        catch (Exception /* ex */)
                        {
                            row[prop.Name] = DBNull.Value;
                        }
                    }
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        // Puts a String List in a DataTable
        public static DataTable ListToDataTable(List<string[]> list, bool headerRow)
        {
            if (list == null && list.Count > 0)
                return null;
            if (list != null && list.Count == 0)
                return null;

            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 0;
            foreach (var array in list)
            {
                if (array.Length > columns)
                {
                    columns = array.Length;
                }
            }

            List<string> columnNames = new List<string>();

            // Read the column names from the header row (if there is one)
            if (headerRow)
                foreach (object name in list[0])
                    columnNames.Add(name.ToString());

            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
                if (i < columnNames.Count)
                    table.Columns[i].ColumnName = columnNames[i];
            }

            // Add rows.
            foreach (var array in list)
            {
                table.Rows.Add(array);
            }

            return table;
        }

        // Puts a String List in a DataTable
        public static DataTable ListToDataTable(List<List<string>> list, bool headerRow, string rowHeaderTextName, bool headerColumn)
        {
            if (list == null && list.Count > 0)
                return null;
            if (list != null && list.Count == 0)
                return null;

            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 0;
            foreach (var array in list)
            {
                if (array.Count > columns)
                {
                    columns = array.Count;
                }
            }

            List<string> columnNames = new List<string>();

            // Read the column names from the header row (if there is one)
            if (headerRow)
            {
                if (headerColumn == true)
                {
                    columnNames.Add(rowHeaderTextName); //This column is used to save the Row Header Text
                }
                foreach (object name in list[0])
                    columnNames.Add(name.ToString());
            }

            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                if (i < columnNames.Count)
                {
                    table.Columns.Add(columnNames[i], typeof(string));
                }
            }

            // Add rows.
            int count = 0;
            foreach (List<string> stringList in list)
            {
                if (count == 0 && headerRow == true)
                {
                    count++;
                    continue; // Do not need to add the header row since alreay added in the Column names
                }
                if (stringList.Count > columnNames.Count)
                {
                    table.Rows.Add(stringList.GetRange(0, table.Columns.Count - 1).ToArray()); // NGE09052013 
                }
                else
                {
                    table.Rows.Add(stringList.ToArray());
                }
            }

            return table;
        }

        public static DataTable DataGridViewToDataTable(DataGridView dataGridView1, string rowHeaderName, bool showRowHeader)
        {
            DataTable table = new DataTable("Table");
            int i, j;

            DataRow row;
            if (showRowHeader == true)
            {
                table.Columns.Add(rowHeaderName); //This column is used to save the Row Header Text
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    table.Columns.Add(column.HeaderText, column.ValueType); //Copy all the Columns
                }

                row = table.NewRow();
                row[0] = rowHeaderName;
                for (j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    row[j + 1] = dataGridView1.Columns[j].HeaderText;
                }
                table.Rows.Add(row);

                for (i = 0; i < dataGridView1.Rows.Count - 1; i++) //Save the values one by one
                {
                    row = table.NewRow();
                    row[0] = dataGridView1.Rows[i].HeaderCell.Value;
                    for (j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        row[j + 1] = dataGridView1.Rows[i].Cells[j].Value;
                    }
                    table.Rows.Add(row);
                }
            }
            else
            {
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    table.Columns.Add(column.HeaderText, column.ValueType); //Copy all the Columns
                }

                row = table.NewRow();
                for (j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    row[j] = dataGridView1.Columns[j].HeaderText;
                }
                table.Rows.Add(row);


                for (i = 0; i < dataGridView1.Rows.Count - 1; i++) //Save the values one by one
                {
                    row = table.NewRow();
                    for (j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        row[j] = dataGridView1.Rows[i].Cells[j].Value;
                    }
                    table.Rows.Add(row);
                }
            }

            return table;
        }

        public static DataTable DataGridViewToDataTableOther(DataGridView dgv, string rowHeaderName, bool showRowHeader)
        {
            DataTable dtSrc = dgv.DataSource as DataTable;
            DataTable table = new DataTable();
            if (dtSrc != null)
            {
                table.Merge(dtSrc);
            }
            else // Put in info manually
            {
                table = DataGridViewToDataTable(dgv, rowHeaderName, showRowHeader);
            }
            return table;
        }

        public static ArrayList DataGridViewToArrayList(DataGridView dgv)
        {
            Object ds = dgv.DataSource;
            IList il = null;
            if (ds is IListSource)
                il = (ds as IListSource).GetList();
            else
                il = (ds as IList);
            return new ArrayList(il);
        }

        public static List<string> DataGridViewToList(DataGridView dgv)
        {
            return DataGridViewToArrayList(dgv).ArrayListToList<string>();
        }

        public static DataTable DataGridViewToDataTableNew(DataGridView dgv, string rowHeaderName, bool showRowHeader)
        {
            DataView dv = (DataView)(dgv.DataSource);
            return dv.ToTable();
        }

        //public static List<T> DataTableToList<T>(DataTable datatable) where T : new()
        //{
        //    List<T> Temp = new List<T>();
        //    try
        //    {
        //        List<string> columnsNames = new List<string>();
        //        foreach (DataColumn DataColumn in datatable.Columns)
        //            columnsNames.Add(DataColumn.ColumnName);
        //        Temp = datatable.AsEnumerable().ToList().ConvertAll<T>(row => DataRowToObject<T>(row, columnsNames));
        //        return Temp;
        //    }
        //    catch
        //    {
        //        return Temp;
        //    }

        //}

        public static DataTable IEnumerableToDataTable<T>(IEnumerable<T> collection)
        {
            DataTable newDataTable = new DataTable();
            Type impliedType = typeof(T);
            PropertyInfo[] _propInfo = impliedType.GetProperties();
            foreach (PropertyInfo pi in _propInfo)
                newDataTable.Columns.Add(pi.Name, pi.PropertyType);

            foreach (T item in collection)
            {
                DataRow newDataRow = newDataTable.NewRow();
                newDataRow.BeginEdit();
                foreach (PropertyInfo pi in _propInfo)
                    newDataRow[pi.Name] = pi.GetValue(item, null);
                newDataRow.EndEdit();
                newDataTable.Rows.Add(newDataRow);
            }
            return newDataTable;
        }

        public static T DataRowToObject<T>(DataRow row, List<string> columnsName) where T : new()
        {
            T obj = new T();
            try
            {
                string columnname = "";
                string value = "";
                PropertyInfo[] Properties;
                Properties = typeof(T).GetProperties();
                foreach (PropertyInfo objProperty in Properties)
                {
                    columnname = columnsName.Find(name => name.ToLower() == objProperty.Name.ToLower());
                    if (!string.IsNullOrEmpty(columnname))
                    {
                        value = row[columnname].ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            if (Nullable.GetUnderlyingType(objProperty.PropertyType) != null)
                            {
                                value = row[columnname].ToString().Replace("$", "").Replace(",", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(Nullable.GetUnderlyingType(objProperty.PropertyType).ToString())), null);
                            }
                            else
                            {
                                value = row[columnname].ToString().Replace("%", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(objProperty.PropertyType.ToString())), null);
                            }
                        }
                    }
                }
                return obj;
            }
            catch
            {
                return obj;
            }
        }

        //public static bool ExportSetupData(string OutputFilename, DataTable dataTable)
        //{
        //    if (OutputFilename == "") // use the default template
        //    {
        //        OutputFilename = MainForm.CRMainForm.SetupInfo.TemplateDirectory + "/TestPlate";
        //    }
        //    else
        //    {
        //        OutputFilename = MainForm.CRMainForm.SetupInfo.TemplateDirectory + "\\" + Path.GetFileNameWithoutExtension(OutputFilename) + ".csv";
        //    }
        //    //if (UserDefinedParametersHeader != null && UserDefinedParametersHeader.Count == 0)
        //    //{
        //    //    UserDefinedParametersHeader.Add("IsControl");
        //    //}

        //    StreamWriter sw = null;
        //    try
        //    {
        //        sw = new StreamWriter(OutputFilename, false);
        //    }
        //    catch
        //    {
        //        if (MainForm.ShowApplication == true)
        //        {
        //            string message = "Could not write plate setup data to file '" + OutputFilename + "'. Possibly in use";
        //            DialogResult dlgRes = MessageBox.Show(message, "Export CSV", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        }
        //        return false;
        //    }

        //    try
        //    {
        //        #region Version 1.0
        //        {
        //            sw.AutoFlush = true;

        //            string tempHeaderNewAssayNamesVersion1_0 = "\"Well\",\"Sample\",\"Ch1 Target\",\"Ch1 Assay Type\",\"Ch2 Target\",\"Ch2 Assay Type\",\"Comments\"";
        //            int w = 0;

        //            if (UserDefinedParametersHeader != null && UserDefinedParametersHeader.Count > 0)
        //            {
        //                string tempHeaderColumn;
        //                for (w = 0; w < UserDefinedParametersHeader.Count; w++)
        //                {
        //                    tempHeaderColumn = ",\"" + UserDefinedParametersHeader[w] + "\"";
        //                    tempHeaderNewAssayNamesVersion1_0 += tempHeaderColumn;
        //                }
        //            }

        //            //sw.WriteLine(tempHeaderNewAssayNames); //  (tempHeaderNewer); // temp for old format
        //            string fileString = IOTools.ToCSV(dataTable, ",", true, true, true, 10, 15); // Does not work because of RefCopyNumber etc are not quoted!

        //            sw.Write(fileString);
        //            sw.Close();
        //        }
        //        #endregion Version 1.0
        //    }
        //    catch
        //    {
        //        if (MainForm.ShowApplication == true)
        //        {
        //            string message = "Could not write plate setup data to file '" + OutputFilename + "'. Possibly in use";
        //            DialogResult dlgRes = MessageBox.Show(message, "Export CSV", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        }
        //        return false;
        //    }
        //    return true;
        //}

        //public static bool ImportCSVSetupData(string PlateSetupImportTemplateFile, string PlateSetupImportFileOut, List<string> SamplNames, List<string> WellNames, out DataTable dataTable)
        //{
        //    dataTable = null;
        //    try
        //    {
        //        dataTable = CSVReader.ReadCSVFile(PlateSetupImportTemplateFile, true, 0); // Read the file
        //    }
        //    catch
        //    {
        //        if (MainForm.ShowApplication)
        //        {
        //            MainForm.CRMainForm.ShowMessageBox(string.Format("Input template file: " + PlateSetupImportTemplateFile + "\nis either open by another program (maybe Excel) or in the wrong format.\nMake sure the template file is available for reading.", "File Error"));
        //            return false;

        //        }
        //        return false;
        //    }

        //    string WellName;
        //    int WellNumber;
        //    // Change data with new Sample Names
        //    if (dataTable != null && dataTable.Rows.Count > 0)
        //    {
        //        for (int j = 0; j < SamplNames.Count(); j++)
        //        {
        //            for (int i = 0; i < dataTable.Rows.Count; i++)
        //            {
        //                WellName = dataTable.Rows[i][0].ToString();
        //                WellNumber = Well.ConvertToInt16(WellName);
        //                if (WellNumber < 0 || WellNumber > 15)
        //                {
        //                    return ReportCSVParsingError("Template CSV File not in the correct format - Field:WellName Row:" + ((int)(i + 1)).ToString() + " incorrect.");
        //                }
        //                if (WellName == WellNames[j]) // matching well so change SampleName
        //                {
        //                    dataTable.Rows[i][1] = SamplNames[j];
        //                    // If SampleName is blank, change the Channel Types to Unused
        //                    if (SamplNames[j] == string.Empty)
        //                    {
        //                        dataTable.Rows[i][3] = Well.Type_NotUsed;
        //                        dataTable.Rows[i][5] = Well.Type_NotUsed;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return true;
        //}

        //public static bool ImportCSVSetupData(string PlateSetupImportTemplateFile, string PlateSetupImportFileOut, string[] SamplNames, out DataTable dataTable)
        //{
        //    dataTable = null;
        //    try
        //    {
        //        dataTable = CSVReader.ReadCSVFile(PlateSetupImportTemplateFile, true, 0); // Read the file
        //    }
        //    catch
        //    {
        //        if (MainForm.ShowApplication)
        //        {
        //            MainForm.CRMainForm.ShowMessageBox(string.Format("Input template file: " + PlateSetupImportTemplateFile + "\nis either open by another program (maybe Excel) or in the wrong format.\nMake sure the template file is available for reading.", "File Error"));
        //            return false;

        //        }
        //        return false;
        //    }

        //    string WellName;
        //    int WellNumber;
        //    // Change data with new Sample Names
        //    if (dataTable != null && dataTable.Rows.Count > 0)
        //    {
        //        for (int j = 0; j < SamplNames.Count(); j++)
        //        {
        //            for (int i = 0; i < dataTable.Rows.Count; i++)
        //            {
        //                WellName = dataTable.Rows[i][0].ToString();
        //                WellNumber = Well.ConvertToInt16(WellName);
        //                if (WellNumber < 0 || WellNumber > 15)
        //                {
        //                    return ReportCSVParsingError("Template CSV File not in the correct format - Field:WellName Row:" + ((int)(i + 1)).ToString() + " incorrect.");
        //                }

        //                if (j >= 10)
        //                {
        //                    if (WellName.Substring(1, 2) == ((j + 2).ToString()))
        //                    {
        //                        dataTable.Rows[i][1] = SamplNames[j];
        //                        // If SampleName is blank, change the Channel Types to Unused
        //                        if (SamplNames[j] == string.Empty)
        //                        {
        //                            dataTable.Rows[i][3] = Well.Type_NotUsed;
        //                            dataTable.Rows[i][5] = Well.Type_NotUsed;
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    if (WellName.Substring(1, 2) == ("0" + (j + 2).ToString()))
        //                    {
        //                        dataTable.Rows[i][1] = SamplNames[j];
        //                        // If SampleName is blank, change the Channel Types to Unused
        //                        if (SamplNames[j] == string.Empty)
        //                        {
        //                            dataTable.Rows[i][3] = Well.Type_NotUsed;
        //                            dataTable.Rows[i][5] = Well.Type_NotUsed;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return true;
        //}

        public static bool MyTryParse(DataTable dataTable, int row, int col, float defaultValue, out float value)
        {
            float tempValue;
            value = defaultValue;

            try
            {
                if (float.TryParse(dataTable.Rows[row][col].ToString(), out tempValue) == true)
                {
                    value = tempValue;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        //public static bool ImportCSVSetupData(string PlateSetupImportCSVTemplateFile, PlateTemplate plate)
        //{
        //    DataTable dataTable;
        //    if (PlateSetupImportCSVTemplateFile == "")
        //    {
        //        return false;
        //    }
        //    else if (!File.Exists(PlateSetupImportCSVTemplateFile)) // try to copy the original
        //    {
        //        PlateSetupImportCSVTemplateFile = MainForm.CRMainForm.SetupInfo.TemplateDirectory + "\\" + Path.GetFileName(PlateSetupImportCSVTemplateFile);
        //        if (!File.Exists(PlateSetupImportCSVTemplateFile)) // Try the local Template folder
        //            return false;
        //    }


        //    try
        //    {
        //        dataTable = CSVReader.ReadCSVFile(PlateSetupImportCSVTemplateFile, true, 0); // Read the file

        //        string WellName;
        //        int WellNumber;
        //        Well w;

        //        // Put the relavent info in the plates and wells

        //        if (dataTable != null && dataTable.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dataTable.Rows.Count; i++)
        //            {

        //                WellName = dataTable.Rows[i][0].ToString();
        //                WellNumber = Well.ConvertToInt16(WellName);
        //                if (WellNumber < 0 || WellNumber > 15)
        //                {
        //                    return ReportCSVParsingError("Template CSV File not in the correct format - Field:WellName Row:" + ((int)(i + 1)).ToString() + " incorrect.");
        //                }

        //                w = plate.WellList[WellNumber];
        //                if (w.ChannelData == null || (w.ChannelData != null && w.ChannelData.Count == 0))
        //                {
        //                    Channel channelTemp;
        //                    int nChannels = 2;
        //                    if (w.ChannelData == null)
        //                    {
        //                        w.ChannelData = new List<Channel>();
        //                    }
        //                    for (i = 0; i < nChannels; i++)
        //                    {
        //                        channelTemp = new Channel();
        //                        w.ChannelData.Add(channelTemp);
        //                    }
        //                }

        //                // Well	Sample	Ch1 Target	Ch1 Assay Type	Ch2 Target	Ch2 Assay Type	Experiment	Expt Type	Expt FG Color	Expt BG Color	ReferenceCopyNumber	TargetCopyNumber	ReferenceAssayNumber	TargetAssayNumber	ReactionVolume	DilutionFactor	SuperMix    Cartridge   Expt Comments
        //                w.SampleName = (string)dataTable.Rows[i][1];
        //                w.ChannelData[0].AssayName = (string)dataTable.Rows[i][2];
        //                w.ChannelData[0].AssayType = (string)dataTable.Rows[i][3];
        //                w.ChannelData[1].AssayName = (string)dataTable.Rows[i][4];
        //                w.ChannelData[1].AssayType = (string)dataTable.Rows[i][5];
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //    return true;
        //}

//        public static bool ImportCSVSetupData(string PlateSetupImportCSVTemplateFile, Plate plate)
//        {
//            DataTable dataTable;
//            if (PlateSetupImportCSVTemplateFile == "")
//            {
//                return false;
//            }

//            if (!File.Exists(PlateSetupImportCSVTemplateFile))
//                return false;

//            try
//            {
//                dataTable = CSVReader.ReadCSVFile(PlateSetupImportCSVTemplateFile, true, 0); // Read the file

//                string WellName;
//                int WellNumber;
//                Well w;

//                // Put the relavent info in the plates and wells

//                if (dataTable != null && dataTable.Rows.Count > 0)
//                {
//                    for (int i = 0; i < dataTable.Rows.Count; i++)
//                    {

//                        WellName = dataTable.Rows[i][0].ToString();
//                        WellNumber = Well.ConvertToInt16(WellName);
//                        if (WellNumber < 0 || WellNumber > 15)
//                        {
//                            return ReportCSVParsingError("Template CSV File not in the correct format - Field:WellName Row:" + ((int)(i + 1)).ToString() + " incorrect.");
//                        }

//                        w = plate.WellList[WellNumber];
//                        if (w.ChannelData == null || (w.ChannelData != null && w.ChannelData.Count == 0))
//                        {
//                            Channel channelTemp;
//                            int nChannels = 2;
//                            if (w.ChannelData == null)
//                            {
//                                w.ChannelData = new List<Channel>();
//                            }
//                            for (i = 0; i < nChannels; i++)
//                            {
//                                channelTemp = new Channel();
//                                w.ChannelData.Add(channelTemp);
//                            }
//                        }

//                        // Well	Sample	Ch1 Target	Ch1 Assay Type	Ch2 Target	Ch2 Assay Type	Experiment	Expt Type	Expt FG Color	Expt BG Color	ReferenceCopyNumber	TargetCopyNumber	ReferenceAssayNumber	TargetAssayNumber	ReactionVolume	DilutionFactor	Supermix    Cartridge   Expt Comments
//                        w.SampleName = (string)dataTable.Rows[i][1];
//                        w.ChannelData[0].AssayName = (string)dataTable.Rows[i][2];
//                        w.ChannelData[0].AssayType = (string)dataTable.Rows[i][3];
//                        w.ChannelData[1].AssayName = (string)dataTable.Rows[i][4];
//                        w.ChannelData[1].AssayType = (string)dataTable.Rows[i][5];

//                        #region Skip Login when in Debug Mode
//#if DEBUG // Skip login when in debug mode
//                        if (w.WellName == "G02")
//                        {
//                            int foo = 0;
//                            foo = foo + 1;
//                        }
//#endif
//                        #endregion Skip Login when in Debug Mode

//                        // NOTE!!! If data in last row is null, then set to empty string!!!
//                        try
//                        {
//                            //w.Comments = (string)dataTable.Rows[i][18];
//                        }
//                        catch
//                        {
//                            //w.Comments = "";
//                        }

//                    }
//                }
//            }
//            catch
//            {
//                return false;
//            }
//            return true;
//        }

        /*
        private bool ImportCSVSetupData(string PlateSetupImportFile, out string PlateSetupImportFileOut, List<string> UserDefinedParametersHeader)
        {
            PlateSetupImportFileOut = "";

            if (PlateSetupImportFile == "")
            {
                // Configure open file dialog box
                System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
                dlg.Title = "Import Plate Setup File";
                dlg.DefaultExt = ".csv";
                dlg.Filter = "QS CSV File|*.csv";

                dlg.FilterIndex = 1;
                dlg.RestoreDirectory = false; // true
                dlg.InitialDirectory =  SetupInfo.TemplateDirectory; //  @"C:\\Templates";  

                // Show open file dialog box
                // Process open file dialog box results
                DialogResult result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    PlateSetupImportFile = dlg.FileName;
                }
            }

            if (PlateSetupImportFile != "")
            {
                System.IO.StreamReader file = null;
                try
                {
                    file =
                    new System.IO.StreamReader(PlateSetupImportFile);
                }
                catch
                {
                    return ReportCSVParsingError("Unable to read Template CSV File.\nCheck to see if it is open by another program.");
                }

                // string temp, tempHeader = "Experiment,Type,ForegroundColor,BackgroundColor,Comments";

                string temp, tempHeader = "\"Well\",\"Sample\",\"FAM Target\",\"FAM Assay Type\",\"VIC Target\",\"VIC Assay Type\",\"Experiment\",\"Expt Type\",\"Expt FG Color\",\"Expt BG Color\",\"ReferenceCopyNumber\",\"Expt Comments\"";
                string tempHeader2 = "Well,Sample,FAM Target,FAM Assay Type,VIC Target,VIC Assay Type,Experiment,Expt Type,Expt FG Color,Expt BG Color,ReferenceCopyNumber,Expt Comments";
                string tempHeaderNew = "\"Well\",\"Sample\",\"FAM Target\",\"FAM Assay Type\",\"VIC Target\",\"VIC Assay Type\",\"Experiment\",\"Expt Type\",\"Expt FG Color\",\"Expt BG Color\",\"ReferenceCopyNumber\",\"TargetCopyNumber\",\"ReferenceAssayNumber\",\"TargetAssayNumber\",\"reactionVolume\",\"dilutionFactor\",\"Expt Comments\"";
                string tempHeader2New = "Well,Sample,FAM Target,FAM Assay Type,VIC Target,VIC Assay Type,Experiment,Expt Type,Expt FG Color,Expt BG Color,ReferenceCopyNumber,TargetCopyNumber,ReferenceAssayNumber,TargetAssayNumber,reactionVolume,dilutionFactor,Expt Comments";
                string tempHeaderNewAssayNames = "\"Well\",\"Sample\",\"Ch1 Target\",\"Ch1 Assay Type\",\"Ch2 Target\",\"Ch2 Assay Type\",\"Experiment\",\"Expt Type\",\"Expt FG Color\",\"Expt BG Color\",\"ReferenceCopyNumber\",\"TargetCopyNumber\",\"ReferenceAssayNumber\",\"TargetAssayNumber\",\"ReactionVolume\",\"DilutionFactor\",\"Expt Comments\"";
                string tempHeader2NewAssayNames = "Well,Sample,Ch1 Target,Ch1 Assay Type,Ch2 Target,Ch2 Assay Type,Experiment,Expt Type,Expt FG Color,Expt BG Color,ReferenceCopyNumber,TargetCopyNumber,ReferenceAssayNumber,TargetAssayNumber,ReactionVolume,DilutionFactor,Expt Comments";
                bool CorrectHeader = false;
                bool NewHeaderStyle = false;
                int MinimumColumns = 12; // tempHeader

                temp = file.ReadLine();
                file.Close();
                if (temp.Length < tempHeader.Length || string.Equals(temp.Substring(0, tempHeader.Length), tempHeader, StringComparison.CurrentCultureIgnoreCase) == false)
                {
                    CorrectHeader = false;
                }
                else
                {
                    CorrectHeader = true;
                    MinimumColumns = 12;
                }

                if (CorrectHeader == false)
                {
                    if (temp.Length < tempHeader2.Length || string.Equals(temp.Substring(0, tempHeader2.Length), tempHeader2, StringComparison.CurrentCultureIgnoreCase) == false)
                    {
                        CorrectHeader = false;
                    }
                    else
                    {
                        CorrectHeader = true;
                        MinimumColumns = 12;
                    }
                }

                if (CorrectHeader == false)
                {
                    if (temp.Length < tempHeaderNewAssayNames.Length || string.Equals(temp.Substring(0, tempHeaderNewAssayNames.Length), tempHeaderNewAssayNames, StringComparison.CurrentCultureIgnoreCase) == false)
                    {
                        CorrectHeader = false;
                    }
                    else
                    {
                        CorrectHeader = true;
                        NewHeaderStyle = true;
                        MinimumColumns = 17;
                    }
                }

                if (CorrectHeader == false)
                {
                    if (temp.Length < tempHeader2NewAssayNames.Length || string.Equals(temp.Substring(0, tempHeader2NewAssayNames.Length), tempHeader2NewAssayNames, StringComparison.CurrentCultureIgnoreCase) == false)
                    {
                        CorrectHeader = false;
                    }
                    else
                    {
                        CorrectHeader = true;
                        NewHeaderStyle = true;
                        MinimumColumns = 17;
                    }
                }

                if (CorrectHeader == false)
                {
                    if (temp.Length < tempHeaderNew.Length || string.Equals(temp.Substring(0, tempHeaderNew.Length), tempHeaderNew, StringComparison.CurrentCultureIgnoreCase) == false)
                    {
                        CorrectHeader = false;
                    }
                    else
                    {
                        CorrectHeader = true;
                        NewHeaderStyle = true;
                        MinimumColumns = 17;
                    }
                }

                if (CorrectHeader == false)
                {
                    if (temp.Length < tempHeader2New.Length || string.Equals(temp.Substring(0, tempHeader2New.Length), tempHeader2New, StringComparison.CurrentCultureIgnoreCase) == false)
                    {
                        CorrectHeader = false;
                    }
                    else
                    {
                        CorrectHeader = true;
                        NewHeaderStyle = true;
                        MinimumColumns = 17;
                    }
                }

                if (CorrectHeader == false)
                {
                    return ReportCSVParsingError("Template CSV File not in the correct format - Header incorrect.");
                }

                DataTable dataTable = CSVReader.ReadCSVFile(PlateSetupImportFile, true, 0);

                Experiment NewExperiment = null;
                int ExperimentIndex = -1;
                Experiment.ExperimentType ExptType = Experiment.ExperimentType.AbsoluteQuantitation;
                string WellName, SampleName, FAMName, FAMType, VICName, VICType, Name, Type, ForegroundColor, BackgroundColor, Comments, comments, referenceCopyNumber, targetCopyNumber, referenceCopyNumberRatio, targetCopyNumberRatio, reactionVolume, dilutionFactor;
                Color bgColor, fgColor;
                uint RefCopyNumber = 2;
                uint TargetCopyNumber = 1;
                uint RefCopyNumberRatio = 1;
                uint TargetCopyNumberRatio = 1;
                float? ReactionVolume = null;
                float DilutionFactor = 1;
                //bool FileReadingComplete = false;
                bool ExperimentTypeFound = false;
                int WellNumber = -1;
                Point pos;
                byte g, b, r, a;

                if (dataTable.Columns.Count < MinimumColumns - 1) // When Excel saves a cell commented CSV, it removes all of the quotes on the cells.  If there are no comments, the columns is now one less than the row headers
                {
                    return ReportCSVParsingError("Template CSV File not in the correct format.");
                }
                else
                {
                    string[] headerColumns = temp.Split(',');

                    if (headerColumns.Count() > 17)
                    {
                        UserDefinedParametersHeader = new List<string>();
                        for (int j = 17; j < headerColumns.Count(); j++)
                        {
                            UserDefinedParametersHeader.Add(headerColumns[j]);
                        }
                    }
                    else
                    {
                        if (UserDefinedParametersHeader != null && UserDefinedParametersHeader.Count == 0)
                        {
                            UserDefinedParametersHeader.Add("IsControl");
                            UserDefinedParametersHeader.Add("TimeDuration");
                            UserDefinedParametersHeader.Add("QuantityTitration");
                        }
                    }
                }

                List<string> UserDefinedParametersList = new List<string>();

                int i = 0;

                for (i = 0; i < dataTable.Rows.Count; i++)
                {
                    WellName = "";
                    SampleName = "";
                    FAMName = "";
                    FAMType = "";
                    VICName = "";
                    VICType = "";
                    Name = "";
                    referenceCopyNumber = "2"; // Default
                    targetCopyNumber = "1"; // Default
                    referenceCopyNumberRatio = "1"; // Default
                    targetCopyNumberRatio = "1"; // Default
                    RefCopyNumber = 2;
                    Comments = "";
                    fgColor = Color.Transparent;
                    bgColor = Color.Transparent;
                    Type = "";
                    ExperimentTypeFound = false;

                    WellName = dataTable.Rows[i][0].ToString();
                    WellNumber = Well.ConvertToInt(WellName);
                    if (WellNumber < 0 || WellNumber > 15)
                    {
                        return ReportCSVParsingError("Template CSV File not in the correct format - Field:WellName Row:" + ((int)(i + 1)).ToString() + " incorrect.");
                    }
                    SampleName = dataTable.Rows[i][1].ToString();
                    FAMName = dataTable.Rows[i][2].ToString();
                    FAMType = dataTable.Rows[i][3].ToString();
                    if (!(FAMType == Well.Type_NotUsed || FAMType == Well.Type_Unknown || FAMType == Well.Type_Reference || FAMType == Well.Type_PositiveControl || FAMType == Well.Type_NegativeControl || FAMType == Well.Type_NTC || FAMType == Well.Type_Blank))
                    {
                        return ReportCSVParsingError("Template CSV File not in the correct format - Field:AssayType[BioConstants.Channel.Channel1]  Row:" + ((int)(i + 1)).ToString() + " incorrect.");
                    }
                    VICName = dataTable.Rows[i][4].ToString();
                    VICType = dataTable.Rows[i][5].ToString();
                    if (!(VICType == Well.Type_NotUsed || VICType == Well.Type_Unknown || VICType == Well.Type_Reference || VICType == Well.Type_PositiveControl || VICType == Well.Type_NegativeControl || VICType == Well.Type_NTC || VICType == Well.Type_Blank))
                    {
                        return ReportCSVParsingError("Template CSV File not in the correct format - Field:AssayType[BioConstants.Channel.Channel2]  Row:" + ((int)(i + 1)).ToString() + " incorrect.");
                    }
                    Name = dataTable.Rows[i][6].ToString();
                    Type = dataTable.Rows[i][7].ToString();
                    if (Type == Well.ExperimentType_AbsoluteQuantitation || Type == Well.ExperimentType_CopyNumberVariation || Type == Well.ExperimentType_RareEventDetection || Type == Well.ExperimentType_Ratio || Type == Well.ExperimentType_Ratio2 || Type == Well.ExperimentType_GeneExpression || Type == Well.ExperimentType_Genotype)
                    {
                        if (Type == Well.ExperimentType_AbsoluteQuantitation)
                            ExptType = Experiment.ExperimentType.AbsoluteQuantitation;
                        else if (Type == Well.ExperimentType_CopyNumberVariation)
                            ExptType = Experiment.ExperimentType.CopyNumberVariation;
                        else if (Type == Well.ExperimentType_RareEventDetection)
                            ExptType = Experiment.ExperimentType.RareEventDetection;
                        else if (Type == Well.ExperimentType_GeneExpression)
                            ExptType = Experiment.ExperimentType.GeneExpression;
                        else if (Type == Well.ExperimentType_Genotype)
                            ExptType = Experiment.ExperimentType.GenoType;
                        else if (Type == Well.ExperimentType_Ratio)
                            ExptType = Experiment.ExperimentType.Ratio;
                        else if (Type == Well.ExperimentType_Ratio2)
                            ExptType = Experiment.ExperimentType.Ratio2;
                        ExperimentTypeFound = true;
                    }
                    if (ExperimentTypeFound == false)
                    {
                        return ReportCSVParsingError("Template CSV File not in the correct format - Field:Expt Name  Row:" + ((int)(i + 1)).ToString() + " incorrect.");
                    }
                    ForegroundColor = dataTable.Rows[i][8].ToString();
                    fgColor = ConvertFromNameOrValueToColor(ForegroundColor);
                    g = fgColor.G;
                    b = fgColor.B;
                    r = fgColor.R;
                    a = fgColor.A;
                    if (g == 0 && b == 0 && r == 0 && a == 0)
                    {
                        fgColor = Color.White;
                    }
                    BackgroundColor = dataTable.Rows[i][9].ToString();
                    bgColor = ConvertFromNameOrValueToColor(BackgroundColor);
                    g = bgColor.G;
                    b = bgColor.B;
                    r = bgColor.R;
                    a = bgColor.A;
                    if (g == 0 && b == 0 && r == 0 && a == 0)
                    {
                        bgColor = Color.Maroon;
                    }

                    referenceCopyNumber = dataTable.Rows[i][10].ToString();
                    if (referenceCopyNumber != "")
                    {
                        if (uint.TryParse(referenceCopyNumber, out RefCopyNumber) == false)
                        {
                            return ReportCSVParsingError("Template CSV File not in the correct format - Field:ReferenceCopyNumber  Row:" + ((int)(i + 1)).ToString() + " incorrect.");
                        }
                    }
                    else
                    {
                        if (ExptType == Experiment.ExperimentType.CopyNumberVariation)
                            return ReportCSVParsingError("Template CSV File not in the correct format - Field:ReferenceCopyNumber  Row:" + ((int)(i + 1)).ToString() + " incorrect.");
                        else
                            referenceCopyNumber = "2";
                    }

                    if (NewHeaderStyle == false)
                    {
                        comments = "";
                        try
                        {
                            comments = dataTable.Rows[i][11].ToString();
                        }
                        catch // Comments not entered
                        {
                        }
                        Comments = comments.Replace("^", "\n"); // replace ^ with Carriage returns
                        Comments = Comments.Replace("\"", ""); // replace "" with a space
                        Comments = Comments.Replace("\'", ""); // replace ' with a space
                    }
                    else
                    {
                        targetCopyNumber = dataTable.Rows[i][11].ToString();
                        if (targetCopyNumber != "")
                        {
                            if (uint.TryParse(targetCopyNumber, out TargetCopyNumber) == false)
                            {
                                return ReportCSVParsingError("Template CSV File not in the correct format - Field:TargetCopyNumber  Row:" + ((int)(i + 1)).ToString() + " incorrect.");
                            }
                        }
                        else
                        {
                            if (ExptType == Experiment.ExperimentType.CopyNumberVariation)
                                return ReportCSVParsingError("Template CSV File not in the correct format - Field:TargetCopyNumber  Row:" + ((int)(i + 1)).ToString() + " incorrect.");
                            else
                                targetCopyNumber = "1";
                        }

                        referenceCopyNumberRatio = dataTable.Rows[i][12].ToString();
                        if (referenceCopyNumberRatio != "")
                        {
                            if (uint.TryParse(referenceCopyNumberRatio, out RefCopyNumberRatio) == false)
                            {
                                return ReportCSVParsingError("Template CSV File not in the correct format - Field:ReferenceAssayNumber  Row:" + ((int)(i + 1)).ToString() + " incorrect.");
                            }
                        }
                        else
                        {
                            if (ExptType == Experiment.ExperimentType.Ratio)
                                return ReportCSVParsingError("Template CSV File not in the correct format - Field:ReferenceAssayNumber  Row:" + ((int)(i + 1)).ToString() + " incorrect.");
                            else
                                referenceCopyNumberRatio = "1";
                        }

                        targetCopyNumberRatio = dataTable.Rows[i][13].ToString();
                        if (targetCopyNumberRatio != "")
                        {
                            if (uint.TryParse(targetCopyNumberRatio, out TargetCopyNumberRatio) == false)
                            {
                                return ReportCSVParsingError("Template CSV File not in the correct format - Field:TargetAssayNumber  Row:" + ((int)(i + 1)).ToString() + " incorrect.");
                            }
                        }
                        else
                        {
                            if (ExptType == Experiment.ExperimentType.Ratio)
                                return ReportCSVParsingError("Template CSV File not in the correct format - Field:TargetAssayNumber  Row:" + ((int)(i + 1)).ToString() + " incorrect.");
                            else
                                targetCopyNumberRatio = "1";
                        }

                        //referenceCopyNumberRatio2 = dataTable.Rows[i][14].ToString();
                        //if (referenceCopyNumberRatio2 != "")
                        //{
                        //    if (uint.TryParse(referenceCopyNumberRatio2, out RefCopyNumberRatio2) == false)
                        //    {
                        //        return ReportCSVParsingError("Template CSV File not in the correct format - Field:ReferenceCopyNumberRatio2  Row:" + ((int)(i + 1)).ToString() + " incorrect.");
                        //    }
                        //}
                        //else
                        //{
                        //    if (ExptType == Experiment.ExperimentType.Ratio2)
                        //        return ReportCSVParsingError("Template CSV File not in the correct format - Field:ReferenceCopyNumberRatio2  Row:" + ((int)(i + 1)).ToString() + " incorrect.");
                        //    else
                        //        referenceCopyNumberRatio2 = "1";
                        //}

                        //targetCopyNumberRatio2 = dataTable.Rows[i][15].ToString();
                        //if (targetCopyNumberRatio2 != "")
                        //{
                        //    if (uint.TryParse(targetCopyNumberRatio2, out TargetCopyNumberRatio2) == false)
                        //    {
                        //        return ReportCSVParsingError("Template CSV File not in the correct format - Field:TargetCopyNumberRatio2  Row:" + ((int)(i + 1)).ToString() + " incorrect.");
                        //    }
                        //}
                        //else
                        //{
                        //    if (ExptType == Experiment.ExperimentType.Ratio2)
                        //        return ReportCSVParsingError("Template CSV File not in the correct format - Field:TargetCopyNumberRatio2  Row:" + ((int)(i + 1)).ToString() + " incorrect.");
                        //    else
                        //        targetCopyNumberRatio2 = "1";
                        //}

                        reactionVolume = dataTable.Rows[i][14].ToString();
                        float wellRactionVolume;
                        if (reactionVolume != "")
                        {
                            if (float.TryParse(reactionVolume, out wellRactionVolume) == false)
                            {
                                return ReportCSVParsingError("Template CSV File not in the correct format - Field:reactionVolume  Row:" + ((int)(i + 1)).ToString() + " incorrect.");
                            }
                            ReactionVolume = wellRactionVolume;
                        }
                        else
                        {
                            ReactionVolume = null;
                        }

                        dilutionFactor = dataTable.Rows[i][15].ToString();
                        if (dilutionFactor != "")
                        {
                            if (float.TryParse(dilutionFactor, out DilutionFactor) == false)
                            {
                                return ReportCSVParsingError("Template CSV File not in the correct format - Field:dilutionFactor  Row:" + ((int)(i + 1)).ToString() + " incorrect.");
                            }
                        }
                        else
                        {
                            DilutionFactor = 1;
                        }

                        comments = "";
                        try
                        {
                            comments = dataTable.Rows[i][16].ToString();
                        }
                        catch // Comments not entered
                        {
                        }
                        Comments = comments.Replace("^", "\n"); // replace ^ with Carriage returns
                        Comments = Comments.Replace("\"", ""); // replace "" with a space
                        Comments = Comments.Replace("\'", ""); // replace ' with a space
                        if (dataTable.Columns.Count > 17)
                        {
                            for (int j = 17; j < dataTable.Columns.Count; j++)
                            {
                                UserDefinedParametersList.Add(dataTable.Rows[i][j].ToString());
                            }
                        }
                    }

                }

                PlateSetupImportFileOut = PlateSetupImportFile;
            }

            return true;
        }
        */

        public static bool GetKnownColor(int iARGBValue, out string strKnownColor, out Color someColor)
        {
            someColor = Color.Transparent;

            Array aListofKnownColors = Enum.GetValues(typeof(KnownColor));
            foreach (KnownColor eKnownColor in aListofKnownColors)
            {
                someColor = Color.FromKnownColor(eKnownColor);
                if (iARGBValue == someColor.ToArgb() && !someColor.IsSystemColor)
                {
                    strKnownColor = someColor.Name;
                    return true;
                }
            }
            strKnownColor = "";
            return false;
        }

        public static Color ConvertFromNameOrValueToColor(string name)
        {
            Color newColor = Color.FromArgb(0, 0, 0, 0);
            newColor = Color.FromName(name);
            byte g = 0;
            byte b = 0;
            byte r = 0;
            byte a = 0;
            if (newColor.IsKnownColor == true)
            {
                g = newColor.G;
                b = newColor.B;
                r = newColor.R;
                a = newColor.A;
            }
            else
            {
                string[] values = name.Split('_');
                int TotalValues = values.Length;
                if (TotalValues != 4)
                {
                    newColor = Color.FromArgb(0, 0, 0, 0);
                    return newColor;
                }
                else
                {
                    // Parse string for 4 values
                    int alpha = 0, red = 0, blue = 0, green = 0;
                    if (int.TryParse(values[0], out alpha) == true && int.TryParse(values[1], out red) == true && int.TryParse(values[2], out blue) == true && int.TryParse(values[3], out green) == true)
                    {
                        newColor = Color.FromArgb(alpha, red, blue, green);
                        g = newColor.G;
                        b = newColor.B;
                        r = newColor.R;
                        a = newColor.A;
                    }
                    else
                    {
                        newColor = Color.FromArgb(0, 0, 0, 0);
                        return newColor;
                    }
                }
            }
            Color color = newColor;
            string colorName;
            if (IOTools.GetKnownColor(newColor.ToArgb(), out colorName, out color) == true)
            {
                if (newColor != color)
                    newColor = color;
            }

            return newColor;
        }

        public static bool WriteColorToString(Color color, out string colorString)
        {
            colorString = color.Name;
            int ARGB;
            byte g;
            byte b;
            byte r;
            byte a;
            if (color.IsNamedColor == true)
            {
                colorString = color.Name;
            }
            else if (color.IsKnownColor == true)
            {
                colorString = color.Name;
            }
            else if (color.IsSystemColor == true)
            {
                colorString = color.Name;
            }
            else
            {
                ARGB = color.ToArgb();
                g = color.G;
                b = color.B;
                r = color.R;
                a = color.A;
                colorString = a.ToString() + "_" + r.ToString() + "_" + g.ToString() + "_" + b.ToString();
                if (colorString == "0_0_0_0")
                {
                    int EmptyColorBug = 0;
                    EmptyColorBug += 1;
                    colorString = "255_255_255_255";
                }
            }

            color = ConvertFromNameOrValueToColor(colorString);
            g = color.G;
            b = color.B;
            r = color.R;
            a = color.A;
            if (g == 0 && b == 0 && r == 0 && a == 0)
            {
                color = Color.White;
                return false;
            }
            return true;
        }

        public static bool ReadColorFromString(string ColorString, out Color color)
        {
            color = Color.White;
            try
            {
                color = ConvertFromNameOrValueToColor(ColorString);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool ReportCSVParsingError(string ErrorInfo)
        {
            if (MainForm.ShowApplication == true)
            {
                MessageBox.Show(ErrorInfo, PixelError, MessageBoxButtons.OK); // , MessageBoxImage.Error);
            }
            return false;
        }

        /*
        // ExportCSVSetupData - All data is surrounded by double quotes so that commas can be included in the fields.  No double quotes are allowing in a data element
        private bool ExportCSVSetupData(string PlateSetupExportFile, out string PlateSetupExportFileOut, List<string> userDefinedParametersHeader)
        {
            PlateSetupExportFileOut = "";

            // Export the plate setup data to an output file
            if (PlateSetupExportFile == "")
                return false;

            // Configure save file dialog box
            System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog();
            dlg.Title = "Export Plate Setup Data";

            dlg.InitialDirectory =  SetupInfo.TemplateDirectory; //  System.IO.Path.GetDirectoryName(richTextBoxPlateProjectName.Text);

            // Set the default to the current QLP filename with the csv extension.
            string pathDir =  SetupInfo.TemplateDirectory + "\\"; //Path.GetDirectoryName(PlateSetupExportFile) + "\\";
            string defaultCSVFilename = pathDir + Path.GetFileNameWithoutExtension(PlateSetupExportFile) + ".csv"; // "Setup.csv";
            dlg.FileName = defaultCSVFilename; // string.Empty;

            dlg.DefaultExt = ".csv";
            dlg.Filter = "Text CSV Files|*.csv";

            DialogResult result;

            if (MainForm.ShowApplication == true)
            {
                // Show save file dialog box
                if (PlateSetupExportFile == "")
                {
                    result = dlg.ShowDialog();
                }
                else
                {
                    dlg.FileName = PlateSetupExportFile;
                    result = DialogResult.OK;
                }
            }
            else
            {
                if (PlateSetupExportFile == "")
                {
                    dlg.FileName = defaultCSVFilename;
                }
                else
                {
                    dlg.FileName = PlateSetupExportFile;
                }
                result = DialogResult.OK;
            }
            if (result == DialogResult.OK)
            {
                if (System.IO.File.Exists(dlg.FileName) == true)
                {
                    FileAttributes fileAttributes = File.GetAttributes(dlg.FileName);
                    FileStream currentWriteableFile = null;
                    if ((fileAttributes & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
                    { // Make sure that the file is NOT read-only 
                        try
                        {
                            currentWriteableFile = File.OpenWrite(dlg.FileName);
                        }
                        catch
                        {
                            if (MainForm.ShowApplication == true)
                            {
                                string message = "Could not export plate setup data to file '" + dlg.FileName + "'. Possibly in use";
                                MessageBox.Show(message, "Export CSV", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                borderlessButtonOptions_Click(null, null);
                            }
                            return false;
                        }
                    }
                    currentWriteableFile.Close();
                }

                StreamWriter sw = null;
                try
                {
                    sw = new StreamWriter(dlg.FileName, false);
                }
                catch
                {
                    if (MainForm.ShowApplication == true)
                    {
                        string message = "Could not write plate setup data to file '" + dlg.FileName + "'. Possibly in use";
                        DialogResult dlgRes = MessageBox.Show(message, "Export CSV", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        borderlessButtonOptions_Click(null, null);
                    }
                    return false;
                }

                sw.AutoFlush = true;

                string temp = "\"Well\",\"Sample\",\"FAM Target\",\"FAM Assay Type\",\"VIC Target\",\"VIC Assay Type\",\"Experiment\",\"Expt Type\",\"Expt FG Color\",\"Expt BG Color\",\"ReferenceCopyNumber\",\"Expt Comments\"";
                //string tempHeaderNew = "\"Well\",\"Sample\",\"FAM Target\",\"FAM Assay Type\",\"VIC Target\",\"VIC Assay Type\",\"Experiment\",\"Expt Type\",\"Expt FG Color\",\"Expt BG Color\",\"ReferenceCopyNumber\",\"TargetCopyNumber\",\"ReferenceAssayNumber\",\"TargetAssayNumber\",\"Expt Comments\"";
                // string tempHeaderNewer = "\"Well\",\"Sample\",\"FAM Target\",\"FAM Assay Type\",\"VIC Target\",\"VIC Assay Type\",\"Experiment\",\"Expt Type\",\"Expt FG Color\",\"Expt BG Color\",\"ReferenceCopyNumber\",\"TargetCopyNumber\",\"ReferenceAssayNumber\",\"TargetAssayNumber\",\"reactionVolume\",\"dilutionFactor\",\"Expt Comments\"";
                string tempHeaderNewAssayNames = "\"Well\",\"Sample\",\"Ch1 Target\",\"Ch1 Assay Type\",\"Ch2 Target\",\"Ch2 Assay Type\",\"Experiment\",\"Expt Type\",\"Expt FG Color\",\"Expt BG Color\",\"ReferenceCopyNumber\",\"TargetCopyNumber\",\"ReferenceAssayNumber\",\"TargetAssayNumber\",\"ReactionVolume\",\"DilutionFactor\",\"Expt Comments\"";
                int w = 0;
                string tempHeaderValue;

                if (userDefinedParametersHeader != null && userDefinedParametersHeader.Count > 0)
                {
                    string tempHeaderColumn;
                    for (w = 0; w < userDefinedParametersHeader.Count; w++)
                    {
                        tempHeaderColumn = ",\"" + userDefinedParametersHeader[w] + "\"";
                        tempHeaderNewAssayNames += tempHeaderColumn;
                    }
                }

                sw.WriteLine(tempHeaderNewAssayNames); //  (tempHeaderNewer); // temp for old format

                QSWellInfoCtrl tempWellCtrl = null;
                //Experiment.ExperimentType Type = Experiment.ExperimentType.AbsoluteQuantitation;
                string fgColor, bgColor, ExptType = Well.ExperimentType_AbsoluteQuantitation, Comments, comments, name, famTargetName, vicTargetName, experimentName; //, tempName;
                int i = 0, j = 0;
                for (i = 1; i <= 8; i++) // row
                {
                    for (j = 1; j <= 12; j++) // column
                    {
                        tempWellCtrl = (QSWellInfoCtrl)qsPlateCtrl1.tableLayoutPanelPlateLayoutData.GetControlFromPosition(j, i);
                        fgColor = tempWellCtrl.w.ExperimentForegroundColor.Name;
                        bgColor = tempWellCtrl.w.ExperimentBackgroundColor.Name;
                        if (tempWellCtrl.w.ExperimentForegroundColor.IsNamedColor == true)
                        {
                            fgColor = tempWellCtrl.w.ExperimentForegroundColor.Name;
                        }
                        else if (tempWellCtrl.w.ExperimentForegroundColor.IsKnownColor == true)
                        {
                            fgColor = tempWellCtrl.w.ExperimentForegroundColor.Name;
                        }
                        else if (tempWellCtrl.w.ExperimentForegroundColor.IsSystemColor == true)
                        {
                            fgColor = tempWellCtrl.w.ExperimentForegroundColor.Name;
                        }
                        else
                        {
                            int ARGB = tempWellCtrl.w.ExperimentForegroundColor.ToArgb();
                            byte g = tempWellCtrl.w.ExperimentForegroundColor.G;
                            byte b = tempWellCtrl.w.ExperimentForegroundColor.B;
                            byte r = tempWellCtrl.w.ExperimentForegroundColor.R;
                            byte a = tempWellCtrl.w.ExperimentForegroundColor.A;
                            fgColor = a.ToString() + "_" + r.ToString() + "_" + g.ToString() + "_" + b.ToString();
                        }
                        if (tempWellCtrl.w.ExperimentBackgroundColor.IsNamedColor == true)
                        {
                            bgColor = tempWellCtrl.w.ExperimentBackgroundColor.Name;
                        }
                        else if (tempWellCtrl.w.ExperimentBackgroundColor.IsKnownColor == true)
                        {
                            bgColor = tempWellCtrl.w.ExperimentBackgroundColor.Name;
                        }
                        else if (tempWellCtrl.w.ExperimentBackgroundColor.IsSystemColor == true)
                        {
                            bgColor = tempWellCtrl.w.ExperimentBackgroundColor.Name;
                        }
                        else
                        {
                            int ARGB = tempWellCtrl.w.ExperimentBackgroundColor.ToArgb();
                            byte g = tempWellCtrl.w.ExperimentBackgroundColor.G;
                            byte b = tempWellCtrl.w.ExperimentBackgroundColor.B;
                            byte r = tempWellCtrl.w.ExperimentBackgroundColor.R;
                            byte a = tempWellCtrl.w.ExperimentBackgroundColor.A;
                            bgColor = a.ToString() + "_" + r.ToString() + "_" + g.ToString() + "_" + b.ToString();
                        }

                        ExptType = tempWellCtrl.w.ExperimentType;
                        if (ExptType == Well.ExperimentType_AbsoluteQuantitationOld)
                            ExptType = Well.ExperimentType_AbsoluteQuantitation;

                        // If comment has carriage returns, replace them with ^ sign and fix it when reading it back in.
                        if (tempWellCtrl.w.ExperimentComments == null || tempWellCtrl.w.ExperimentComments == "")
                        {
                            Comments = "";
                            comments = "";
                        }
                        else
                        {
                            Comments = tempWellCtrl.w.ExperimentComments;
                            comments = Comments.Replace("\n", "^"); // Comments;
                            comments = comments.Replace("\"", ""); // replace "" with a space
                            comments = comments.Replace("\'", ""); // replace ' with a space
                        }
                        if (tempWellCtrl.w.SampleName == null || tempWellCtrl.w.SampleName == "")
                            name = "";
                        else
                        {
                            name = tempWellCtrl.w.SampleName; //
                        }
                        if (tempWellCtrl.w.ExperimentName == null || tempWellCtrl.w.ExperimentName == "")
                        {
                            experimentName = "";
                            fgColor = "White";
                            bgColor = "Maroon";
                        }
                        else
                        {
                            experimentName = tempWellCtrl.w.ExperimentName;
                        }
                        if (tempWellCtrl.w.AssayTarget[BioConstants.Channel.Channel1] == null || tempWellCtrl.w.AssayTarget[BioConstants.Channel.Channel1] == "")
                            famTargetName = "";
                        else
                        {
                            famTargetName = tempWellCtrl.w.AssayTarget[BioConstants.Channel.Channel1];
                        }
                        if (tempWellCtrl.w.AssayTarget[BioConstants.Channel.Channel2] == null || tempWellCtrl.w.AssayTarget[BioConstants.Channel.Channel2] == "")
                            vicTargetName = "";
                        else
                        {
                            vicTargetName = tempWellCtrl.w.AssayTarget[BioConstants.Channel.Channel2];
                        }

                        if (name == "" && famTargetName == "" && tempWellCtrl.w.AssayType[BioConstants.Channel.Channel1] == Well.Type_NotUsed && vicTargetName == "" && tempWellCtrl.w.AssayType[BioConstants.Channel.Channel2] == Well.Type_NotUsed &&
                            experimentName == "" && ExptType == Well.ExperimentType_AbsoluteQuantitation && comments == "")
                            continue;

                        temp = "\"" + tempWellCtrl.w.WellName + "\"" + "," +
                               "\"" + name + "\"" + "," +
                               "\"" + famTargetName + "\"" + "," +
                               "\"" + tempWellCtrl.w.AssayType[BioConstants.Channel.Channel1] + "\"" + "," +
                               "\"" + vicTargetName + "\"" + "," +
                               "\"" + tempWellCtrl.w.AssayType[BioConstants.Channel.Channel2] + "\"" + "," +
                               "\"" + experimentName + "\"" + "," +
                               "\"" + ExptType + "\"" + "," +
                               "\"" + fgColor + "\"" + "," +
                               "\"" + bgColor + "\"" + "," +
                               DisplayTools.RoundToSignificantFiguresForDisplay(tempWellCtrl.w.ReferenceCopyNumber, 3) + "," + // tempWellCtrl.w.ReferenceCopyNumber.ToString() + "," +
                               DisplayTools.RoundToSignificantFiguresForDisplay(tempWellCtrl.w.TargetCopyNumber, 3) + "," + // tempWellCtrl.w.TargetCopyNumber.ToString() + "," +
                               DisplayTools.RoundToSignificantFiguresForDisplay(tempWellCtrl.w.ReferenceAssayNumber, 3) + "," + // tempWellCtrl.w.ReferenceAssayNumber.ToString() + "," +
                               DisplayTools.RoundToSignificantFiguresForDisplay(tempWellCtrl.w.TargetAssayNumber, 3) + "," + // tempWellCtrl.w.TargetAssayNumber.ToString() + "," +                               
                               DisplayTools.RoundToSignificantFiguresForDisplay(tempWellCtrl.w.ReactionVolume, 3) + "," + // tempWellCtrl.w.reactionVolume.ToString() + "," +
                               DisplayTools.RoundToSignificantFiguresForDisplay(tempWellCtrl.w.DilutionFactor, 3) + "," + // tempWellCtrl.w.dilutionFactor.ToString() + "," +
                               "\"" + comments + "\"";

                        if (tempWellCtrl.w.UserDefinedParameters != null && tempWellCtrl.w.UserDefinedParameters.Count > 0)
                        {
                            for (w = 0; w < tempWellCtrl.w.UserDefinedParameters.Count; w++)
                            {
                                tempHeaderValue = ",\"" + tempWellCtrl.w.UserDefinedParameters[w] + "\"";
                                temp += tempHeaderValue;
                            }
                        }
                        else
                        {
                            if (UserDefinedParametersHeader != null && UserDefinedParametersHeader.Count > 0)
                            {
                                for (w = 0; w < UserDefinedParametersHeader.Count; w++)
                                {
                                    if (string.Equals(UserDefinedParametersHeader[w], "IsControl", StringComparison.CurrentCultureIgnoreCase) == true)
                                    {
                                        if (tempWellCtrl.w.IsControl == true)
                                        {
                                            tempHeaderValue = ",\"" + "TRUE" + "\"";
                                        }
                                        else
                                        {
                                            tempHeaderValue = ",\"" + "\"";
                                        }
                                    }
                                    else if (string.Equals(UserDefinedParametersHeader[w], "TimeDuration", StringComparison.CurrentCultureIgnoreCase) == true)
                                    {
                                        tempHeaderValue = ",\"" + tempWellCtrl.w.TimeDuration.ToString() + "\"";
                                    }
                                    else if (string.Equals(UserDefinedParametersHeader[w], "QuantityTitration", StringComparison.CurrentCultureIgnoreCase) == true)
                                    {
                                        tempHeaderValue = ",\"" + tempWellCtrl.w.QuantityTitration.ToString() + "\"";
                                    }
                                    else
                                    {
                                        tempHeaderValue = ",\"" + "\"";
                                    }
                                    temp += tempHeaderValue;
                                }
                            }
                        }
                        sw.WriteLine(temp);
                    }
                }
                sw.Close();

                PlateSetupExportFileOut = dlg.FileName;

                return true;
            }

            return false;
        }

        */

        private static void LoadDataFromDataTable(DataTable dataTable, ref float[,] floatArray)
        {
            floatArray = new float[2, dataTable.Rows.Count];
            int i;
            for (i = 0; i < dataTable.Rows.Count; i++)
            {
                floatArray[0, i] = (float)dataTable.Rows[i][0];
                floatArray[1, i] = (float)dataTable.Rows[i][1];
            }
        }

        // Example Data file format
        //Assay1 Amplitude,Assay2 Amplitude,Cluster
        //1714.60083,2362.76636,1
        //1718.72437,2361.08374,1
        //1722.01819,2334.31445,1
        //1732.354,2327.14355,1
        //1734.989,2264.60522,1
        //1745.1615,2378.01929,1

        //public static bool ImportAnalysisData(string PlateAnalysisImportTemplateFile, Plate plate, out DataTable dataTable)
        //{
        //    dataTable = null;

        //    if (PlateAnalysisImportTemplateFile == "") // use the default template
        //    {
        //        return false;
        //    }

        //    if (System.IO.File.Exists(PlateAnalysisImportTemplateFile) == false)
        //    {
        //        // See if file is in Template Directory
        //        PlateAnalysisImportTemplateFile = MainForm.CRMainForm.SetupInfo.TemplateDirectory + "\\" + Path.GetFileName(PlateAnalysisImportTemplateFile) + ".csv";
        //        if (System.IO.File.Exists(PlateAnalysisImportTemplateFile) == false)
        //        {
        //            return false;
        //        }
        //    }

        //    try
        //    {
        //        dataTable = CSVReader.ReadCSVFile(PlateAnalysisImportTemplateFile, true, 0); // Read the file
        //    }
        //    catch
        //    {
        //        if (MainForm.ShowApplication)
        //        {
        //            MainForm.CRMainForm.ShowMessageBox(string.Format("Analysis Table is either open by another program or in the wrong format.\nDid you use 'Alt-T' to show the extended table before saving the file?", "File Error"));
        //            return false;

        //        }
        //        return false;
        //    }

        //    string WellName;
        //    int WellNumber;
        //    string threshold = "Threshold";
        //    int thresholdColumn = 0;
        //    float thresholdValue;
        //    string wasThresholded = "WasThresholded";
        //    int wasThresholdedColumn = 0;
        //    //bool wasThresholdedValue;
        //    string useClusterAlgorithm = "UseClusterAlgorithm";
        //    int useClusterAlgorithmColumn = 0;
        //    //bool useClusterAlgorithmValue;
        //    string typeAssay = "TypeAssay";
        //    int typeAssayColumn = 0;
        //    //string assayType1 = "Ch1";
        //    //string assayType2 = "Ch2";
        //    Well w;

        //    // Change data with new Sample Names
        //    if (dataTable != null && dataTable.Rows.Count > 0) // For now just fill in the theshold info
        //    {
        //        int i;
        //        // Find columns that have the data we are interested in
        //        for (i = 0; i < dataTable.Columns.Count; i++)
        //        {
        //            if (dataTable.Columns[i].ColumnName == threshold)
        //            {
        //                thresholdColumn = i;
        //            }
        //            else if (dataTable.Columns[i].ColumnName == wasThresholded)
        //            {
        //                wasThresholdedColumn = i;
        //            }
        //            else if (dataTable.Columns[i].ColumnName == useClusterAlgorithm)
        //            {
        //                useClusterAlgorithmColumn = i;
        //            }
        //            else if (dataTable.Columns[i].ColumnName == typeAssay)
        //            {
        //                typeAssayColumn = i;
        //            }
        //        }
        //        if (typeAssayColumn == 0 || thresholdColumn == 0 || wasThresholdedColumn == 0) // Either file is bad or Alt T was never used
        //        {
        //            if (MainForm.ShowApplication)
        //            {
        //                MainForm.CRMainForm.ShowMessageBox(string.Format("Analysis Table is in the wrong format.\nDid you use 'Alt-T' to show the extended table before saving the file?", "File Error"));
        //                return false;

        //            }
        //        }
        //        for (i = 0; i < dataTable.Rows.Count; i++)
        //        {
        //            WellName = dataTable.Rows[i][0].ToString();
        //            WellNumber = Well.ConvertToInt16(WellName);
        //            if (WellNumber < 0 || WellNumber > 15)
        //            {
        //                return ReportCSVParsingError("Analysis CSV File not in the correct format - Field:WellName Row:" + ((int)(i + 1)).ToString() + " incorrect.");
        //            }

        //            w = plate.WellList[WellNumber];

        //            MyTryParse(dataTable, i, thresholdColumn, 0.0f, out thresholdValue);
        //        }
        //    }
        //    return true;
        //}

        /*
        public void getJaggedIntArray()
        {
            int i, j;
            int[][] a;
            a = new int[3][];

            for (i = 0; i < a.Length; i++)
            {
                Console.WriteLine("enter row's column size");
                a[i] = new int[Convert.ToInt32(Console.ReadLine())];
            }

            for (i = 0; i < a.Length; i++)
            {
                for (j = 0; j < a[i].Length; j++)
                {
                    Console.WriteLine("enter no.");
                    a[i][j] = Convert.ToInt32(Console.ReadLine());
                }
            }

            for (i = 0; i < a.Length; i++)
            {
                for (j = 0; j < a[i].Length; j++)
                {
                    Console.WriteLine(a[i][j]);
                }
            }
        }

        private void ExportAmplitudeData(string OutputDirectory = "")
        {
            // Export the plate setup data to an output file
            if (PlateProjectFilename == "")
                return;

            DialogResult dlgRes;
            if (OutputDirectory == "")
            {
                // See if current plate needs to be saved first.
                dlgRes = SaveCurrentPlateInfoCheck();
                if (dlgRes == DialogResult.Cancel)
                {
                    borderlessButtonOptions_Click(null, null);
                    panelMainOptions.Enabled = true;
                    return;
                }

                MainForm.ShowApplication = true;
            }
            else
                MainForm.ShowApplication = false;

            DialogResult result;
            // Set the default to the current QLP filename with the csv extension.
            string pathDir = Path.GetDirectoryName(PlateProjectFilename) + "\\";
            string Folder = OutputDirectory;
            VistaFolderBrowserDialog folderDialog = null;

            if (MainForm.ShowApplication == true)
            {
                if (qsPlateCtrl1.selectedPlateWellList.Count == 0)
                {
                    MessageBox.Show(this, "Please select the wells you wish to export", "Export Amplitude Data");
                    return;
                }

                folderDialog = new VistaFolderBrowserDialog();
                folderDialog.SelectedPath = pathDir; // DataDirectory + @"\";
                folderDialog.Description = "Select Amplitude Export Folder";
                folderDialog.UseDescriptionForTitle = true;

                result = folderDialog.ShowDialog();
            }
            else
            {
                result = DialogResult.OK;
            }

            if (result == DialogResult.OK)
            {
                int count;
                if (MainForm.ShowApplication == true)
                {
                    count = qsPlateCtrl1.selectedPlateWellList.Count;
                }
                else
                {
                    count = SetupList.Count;
                }

                QSWellInfoCtrl tempCtrl = null;
                int WellNumber = 0;
                Well w = null;
                StreamWriter sw = null;
                int j = 0;
                int TotalEvents = 0;
                string WellFilename;
                if (MainForm.ShowApplication == false)
                {
                    Folder = OutputDirectory;
                }
                else
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Folder = folderDialog.SelectedPath;
                }

                string PlateName = Path.GetFileNameWithoutExtension(PlateProjectFilename);
                string FAMAmplitudeData, VICAmplitudeData;
                float maxWidthGateFAM;
                float minWidthGateFAM;
                float maxWidthGateVIC;
                float minWidthGateVIC;
                float minQualityGateFAM;
                float minQualityGateVIC;
                string temp;

                ManagedQLEvent[] Events;
                uint[][] EventGatingFlags;
                byte[] EventClusterCalls;

                for (int i = 0; i < count; i++)
                {
                    if (MainForm.ShowApplication == true)
                    {
                        tempCtrl = qsPlateCtrl1.selectedPlateWellList[i];
                        w = tempCtrl.w;
                    }
                    else
                    {
                        w = (Well)SetupList[i];
                    }

                    WellNumber = Well.ConvertToInt(w.WellName);
                    WellFilename = Path.Combine(Folder, string.Format("{0}_{1}_Amplitude.csv", PlateName, w.WellName));
                    maxWidthGateFAM = w.MaxWidthGate[BioConstants.Channel.Channel1];
                    minWidthGateFAM = w.MinWidthGate[BioConstants.Channel.Channel1];
                    maxWidthGateVIC = w.MaxWidthGate[BioConstants.Channel.Channel2];
                    minWidthGateVIC = w.MinWidthGate[BioConstants.Channel.Channel2];
                    minQualityGateFAM = w.MinQualityGate[BioConstants.Channel.Channel1];
                    minQualityGateVIC = w.MinQualityGate[BioConstants.Channel.Channel2];

                    if (PeakDataSet[WellNumber] == null)
                        continue;

                    TotalEvents = PeakDataSet[WellNumber].Length;

                    if (PeakDataSet[WellNumber] != null && TotalEvents > 0)
                    {
                        // Open the File
                        try
                        {
                            sw = new StreamWriter(WellFilename, false);
                        }
                        catch
                        {
                            if (MainForm.ShowApplication == true)
                            {
                                string message = "Could not write well amplitude data to file '" + WellFilename + "'. Possibly in use";
                                dlgRes = MessageBox.Show(message, "Export Amplitude Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            return;
                        }

                        // Valid Min and Max Width Gates
                        bool ValidFAMGates = false, ValidVICGates = false;

                        if (maxWidthGateFAM > 0 && minWidthGateFAM < maxWidthGateFAM && double.IsNaN(minWidthGateFAM) == false && double.IsNaN(maxWidthGateFAM) == false)
                            ValidFAMGates = true;

                        if (maxWidthGateVIC > 0 && minWidthGateVIC < maxWidthGateVIC && double.IsNaN(minWidthGateVIC) == false && double.IsNaN(maxWidthGateVIC) == false)
                            ValidVICGates = true;

                        Events = PeakDataSet[WellNumber];
                        EventGatingFlags = EventGatingFlagsSet[WellNumber];
                        EventClusterCalls = EventClusterCallsSet[WellNumber];

                        sw.AutoFlush = true;

                        temp = "Assay1 Amplitude,Assay2 Amplitude,Cluster";
                        sw.WriteLine(temp);

                        bool SortByAmplitudes = true;
                        if (SortByAmplitudes == true)
                        {
                            // First collect all Amplitudes into an int array of long amplitude values with the event number tagged to the end
                            // and event array index
                            SortedDictionary<ulong, int> tableValueDictionary = new SortedDictionary<ulong, int>();
                            ulong SortingNumber = 0;

                            if (w.AssayType[BioConstants.Channel.Channel1] != Well.Type_NotUsed)
                            {
                                if (ValidFAMGates == true && ValidVICGates == true)
                                {
                                    for (j = 0; j < TotalEvents; j++)
                                    {
                                        if (EventGatingFlags[BioConstants.Channel.Channel1][j] != 0)
                                            continue;

                                        if (EventGatingFlags[BioConstants.Channel.Channel2][j] != 0)
                                            continue;

                                        SortingNumber = ((1000000000000UL * (ulong)(Events[j].Amplitude0)) + (ulong)j);

                                        // while (tableValueDictionary.ContainsKey(SortingNumber))
                                        //     SortingNumber += .000000001F;

                                        tableValueDictionary.Add(SortingNumber, j);
                                    }
                                }
                            }
                            else if (w.AssayType[BioConstants.Channel.Channel2] != Well.Type_NotUsed)
                            {
                                if (ValidFAMGates == true && ValidVICGates == true)
                                {
                                    for (j = 0; j < TotalEvents; j++)
                                    {
                                        if (EventGatingFlags[BioConstants.Channel.Channel1][j] != 0)
                                            continue;

                                        if (EventGatingFlags[BioConstants.Channel.Channel2][j] != 0)
                                            continue;

                                        // Float significant digits is 7 - most droplets in a well 1,000,000 so need to multiply by 10,000,000,000,000 13 significan figures
                                        SortingNumber = ((1000000000000UL * (ulong)(Events[j].Amplitude1)) + (ulong)j);

                                        //while (tableValueDictionary.ContainsKey(SortingNumber))
                                        //    SortingNumber += .000000001F;

                                        tableValueDictionary.Add(SortingNumber, j);
                                    }
                                }
                            }

                            foreach (int index in tableValueDictionary.Values)
                            {
                                if (w.AssayType[BioConstants.Channel.Channel1] != Well.Type_NotUsed)
                                    FAMAmplitudeData = Events[index].Amplitude0.ToString("R"); // MathHelper.RoundToSignificantFigures(((double)Events[index].Amplitude0), 5).ToString("g7");
                                else
                                    FAMAmplitudeData = "";
                                if (w.AssayType[BioConstants.Channel.Channel2] != Well.Type_NotUsed)
                                    VICAmplitudeData = Events[index].Amplitude1.ToString("R"); // MathHelper.RoundToSignificantFigures(((double)Events[index].Amplitude1), 5).ToString("g7");
                                else
                                    VICAmplitudeData = "";
                                temp = FAMAmplitudeData + "," + VICAmplitudeData + "," + (EventClusterCalls[index] >> 4).ToString();
                                sw.WriteLine(temp);
                            }

                        }
                        else
                        {
                            if (ValidFAMGates == true && ValidVICGates == true)
                            {
                                for (j = 0; j < TotalEvents; j++)
                                {
                                    if (EventGatingFlags[BioConstants.Channel.Channel1][j] != 0)
                                        continue;

                                    if (EventGatingFlags[BioConstants.Channel.Channel2][j] != 0)
                                        continue;

                                    if (w.AssayType[BioConstants.Channel.Channel1] != Well.Type_NotUsed)
                                        FAMAmplitudeData = Events[j].Amplitude0.ToString("R"); // MathHelper.RoundToSignificantFigures(((double)Events[j].Amplitude0), 5).ToString("g7");
                                    else
                                        FAMAmplitudeData = "";
                                    if (w.AssayType[BioConstants.Channel.Channel2] != Well.Type_NotUsed)
                                        VICAmplitudeData = Events[j].Amplitude1.ToString("R"); // MathHelper.RoundToSignificantFigures(((double)Events[j].Amplitude1), 5).ToString("g7");
                                    else
                                        VICAmplitudeData = "";
                                    temp = FAMAmplitudeData + "," + VICAmplitudeData + "," + (EventClusterCalls[j] >> 4).ToString();
                                    sw.WriteLine(temp);
                                }
                            }
                        }

                        sw.Close();
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
        */

        ///// <summary>
        ///// Util class to copy DataGridView data to the clipboard
        ///// </summary>
        ///// <param name="dgData"></param>
        //public static void CopyToClipboard(MultiEditDataGridView dgData)
        //{
        //    DataObject d = dgData.GetClipboardContent();
        //    Clipboard.SetDataObject(d);
        //}

        ///// <summary>
        ///// Util class that can service any paste into a DataGridView
        ///// </summary>
        ///// <param name="dgData"></param>
        //public static void PasteFromClipboard(MultiEditDataGridView dgData)
        //{
        //    try
        //    {
        //        string s = Clipboard.GetText();
        //        string[] lines = s.Split('\n');
        //        int iFail = 0, iRow = dgData.CurrentCell.RowIndex;
        //        int iCol = dgData.CurrentCell.ColumnIndex;
        //        DataGridViewCell oCell;
        //        foreach (string line in lines)
        //        {
        //            if (iRow < dgData.RowCount && line.Length > 0)
        //            {
        //                string[] sCells = line.Split('\t');
        //                for (int i = 0; i < sCells.GetLength(0); ++i)
        //                {
        //                    if (iCol + i < dgData.ColumnCount)
        //                    {
        //                        oCell = dgData[iCol + i, iRow];
        //                        if (!oCell.ReadOnly)
        //                        {
        //                            if (oCell.Value.ToString() != sCells[i])
        //                            {
        //                                oCell.Value = Convert.ChangeType(sCells[i], oCell.ValueType);
        //                                oCell.Style.BackColor = Color.Tomato;
        //                            }
        //                            else
        //                                iFail++;//only traps a fail if the data has changed and you are pasting into a read only cell
        //                        }
        //                    }
        //                    else
        //                    { break; }
        //                }
        //                iRow++;
        //            }
        //            else
        //            { break; }
        //            if (iFail > 0)
        //                MessageBox.Show(string.Format("{0} updates failed due to read only column setting", iFail));
        //        }
        //    }
        //    catch (FormatException)
        //    {
        //        MessageBox.Show("The data you pasted is in the wrong format for the cell");
        //        return;
        //    }
        //}

        public static bool ValidateDirectory(string DirectoryPath)
        {
            // Check for DirectoryPath, create it if it does not exist
            DirectoryInfo DirectoryInfo = Directory.CreateDirectory(DirectoryPath);

            if (DirectoryInfo.Exists == false)
            {
                if (MainForm.ShowApplication) MainForm.CRMainForm.ShowMessageBox(string.Format("Unable to create the \"{0}\" directory.", DirectoryPath), "Directory Error");
                return false;
            }

            if ((DirectoryInfo.Attributes & FileAttributes.Directory) != FileAttributes.Directory)
            {
                if (MainForm.ShowApplication) MainForm.CRMainForm.ShowMessageBox(string.Format("\"{0}\" is not a valid directory.  It may be a file!", DirectoryPath), "Directory error");
                return false;
            }

            if ((DirectoryInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                if (MainForm.ShowApplication) MainForm.CRMainForm.ShowMessageBox(string.Format("The \"{0}\" directory is Read-Only.", DirectoryPath), "Directory error");
                return false;
            }

            return true;
        }

        public static bool ValidateDirectoryNOGUI(string DirectoryPath) // NO GUI!!!
        {
            // Check for DirectoryPath, create it if it does not exist
            DirectoryInfo DirectoryInfo = Directory.CreateDirectory(DirectoryPath);

            if (DirectoryInfo.Exists == false)
            {
                return false;
            }

            if ((DirectoryInfo.Attributes & FileAttributes.Directory) != FileAttributes.Directory)
            {
                return false;
            }

            if ((DirectoryInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                return false;
            }

            return true;
        }

        //public static bool SaveTestResults(Plate plate, string name, DataTable dataTable)
        //{
        //    // Bring Up Save Dialog and Export the DataGridView Info
        //    string PlateDirectory = MainForm.CRMainForm.SetupInfo.ReportDirectory + "\\" + Path.GetFileNameWithoutExtension(plate.Name);
        //    ValidateDirectoryNOGUI(PlateDirectory);

        //    string FileName;
        //    if (string.Equals(name, Path.GetFileNameWithoutExtension(plate.Name)))
        //    {
        //        FileName = PlateDirectory + "\\" + name + "_TestResults.csv";
        //    }
        //    else
        //    {
        //        FileName = PlateDirectory + "\\" + name + "-" + Path.GetFileNameWithoutExtension(plate.Name) + "_TestResults.csv";
        //    }

        //    //string rowHeaderName = "";
        //    //bool showRowHeader = false;

        //    bool ShowApplication = MainForm.ShowApplication;
        //    //rowHeaderName = "Gene";
        //    //showRowHeader = true;

        //    {
        //        if (System.IO.File.Exists(FileName) == true)
        //        {
        //            FileAttributes fileAttributes = File.GetAttributes(FileName);
        //            FileStream currentWriteableFile = null;
        //            if ((fileAttributes & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
        //            { // Make sure that the file is NOT read-only 
        //                try
        //                {
        //                    currentWriteableFile = File.OpenWrite(FileName);
        //                }
        //                catch
        //                {
        //                    if (ShowApplication == true)
        //                    {
        //                        string message = "Could not export data to file '" + FileName + "'. Possibly in use";
        //                        MessageBox.Show(message, "Export CSV", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                    }
        //                    return false;
        //                }
        //            }
        //            currentWriteableFile.Close();
        //        }

        //        StreamWriter sw = null;
        //        try
        //        {
        //            sw = new StreamWriter(FileName, false);
        //        }
        //        catch
        //        {
        //            if (ShowApplication == true)
        //            {
        //                string message = "Could not write to file '" + FileName + "'. Possibly in use";
        //                DialogResult dlgRes = MessageBox.Show(message, "Export CSV", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            }
        //            return false;
        //        }

        //        try
        //        {
        //            IOTools.WriteDataTable(sw, dataTable);

        //        }
        //        catch
        //        {
        //            sw.Close();
        //            if (ShowApplication == true)
        //            {
        //                string message = "Could not write to file '" + FileName + "'. Possibly in use";
        //                DialogResult dlgRes = MessageBox.Show(message, "Export CSV", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            }
        //            return false;
        //        }

        //        sw.Close();

        //    }

        //    return true;
        //}

        public static bool IsQCPlate(string plateName)
        {
            if (plateName.Length >= 7 && plateName.Substring(plateName.Length - 7, 3) == "_QC")
            {
                return true;
            }
            return false;
        }

        //public static string GetLoggingString(User user, string what, string where, string why, string result)
        //{
        //    // Add Log Information
        //    // The string list below contains all actions (workflow tasks) performed on this plate
        //    // Who  What    When    Where   Why all separated by tabs (for readability tabs are better than commas)
        //    // User WorkFlowTask    Date    Place   Reason  Result

        //    string loggingInfo = "";
        //    if (user == null) // system operation Who
        //    {
        //        loggingInfo += "System" + "\t";
        //    }
        //    else
        //    {
        //        if (user.Username == "")
        //        {
        //            loggingInfo += "SuperUser" + "\t";
        //        }
        //        else
        //        {
        //            loggingInfo += user + "\t";
        //        }
        //    }
        //    loggingInfo += what + "\t"; // What
        //    loggingInfo += String.Format("{0:MM}/{0:dd}/{0:yyyy} {0:HH}:{0:mm}:{0:ss} ", DateTime.Now) + "\t"; // DateTime.Now.ToShortDateString() + "\t"; // When
        //    loggingInfo += where + "\t"; // Where
        //    loggingInfo += why + "\t"; // Why
        //    loggingInfo += result; // Result

        //    // Write the Logging string to a default log file in SetupDirectory called CRPixel.log
        //    // open trace log file
        //    Trace.Open(MainForm.CRMainForm.SetupInfo.SetupDirectory + "\\Pixel.log");
        //    Trace.Write(loggingInfo);

        //    return loggingInfo;
        //}

        public static string GetLoggingString(string user, string what, string where, string why, string result)
        {
            // Add Log Information
            // The string list below contains all actions (workflow tasks) performed on this plate
            // Who  What    When    Where   Why all separated by tabs (for readability tabs are better than commas)
            // User WorkFlowTask    Date    Place   Reason  Result

            string loggingInfo = "";
            if (user == null) // system operation Who
            {
                loggingInfo += "System" + "\t";
            }
            else
            {
                if (user == "")
                {
                    loggingInfo += "SuperUser" + "\t";
                }
                else
                {
                    loggingInfo += user + "\t";
                }
            }
            loggingInfo += what + "\t"; // What
            loggingInfo += String.Format("{0:MM}/{0:dd}/{0:yyyy} {0:HH}:{0:mm}:{0:ss} ", DateTime.Now) + "\t"; // DateTime.Now.ToShortDateString() + "\t"; // When
            loggingInfo += where + "\t"; // Where
            loggingInfo += why + "\t"; // Why
            loggingInfo += result; // Result

            // Write the Logging string to a default log file in SetupDirectory called CRPixel.log
            // open trace log file
            Trace.Open(AppDomain.CurrentDomain.BaseDirectory + "\\iRobotKinect.log");
            Trace.Write(loggingInfo);

            return loggingInfo;
        }

        private static Dictionary<Type, IList<PropertyInfo>> typeDictionary = new Dictionary<Type, IList<PropertyInfo>>();

        public static IList<PropertyInfo> GetPropertiesForType<T>()
        {
            var type = typeof(T);
            if (!typeDictionary.ContainsKey(typeof(T)))
            {
                typeDictionary.Add(type, type.GetProperties().ToList());
            }
            return typeDictionary[type];
        }

        public static IList<T> ToList<T>(this DataTable table) where T : new()
        {
            IList<PropertyInfo> properties = GetPropertiesForType<T>();
            IList<T> result = new List<T>();

            foreach (var row in table.Rows)
            {
                var item = CreateItemFromRow<T>((DataRow)row, properties);
                result.Add(item);
            }

            return result;
        }

        public static bool SetFileCreationDateTimeBasedOnAnotherFilesCreationDateTime(string fileToChange, string fileWithCorrectDateTime)
        {
            if (File.Exists(fileWithCorrectDateTime))
            {
                DateTime FileCreationDateTime = File.GetCreationTime(fileWithCorrectDateTime);
                if (File.Exists(fileToChange))
                {
                    File.SetCreationTime(fileToChange, FileCreationDateTime);
                    return true;
                }
            }

            return false;
        }

        private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        {
            T item = new T();
            foreach (var property in properties)
            {
                property.SetValue(item, row[property.Name], null);
            }
            return item;
        }

        /// <summary>
        /// Converts the passed in data table to a CSV-style string.      
        /// </summary>
        /// <param name="table">Table to convert</param>
        /// <returns>Resulting CSV-style string</returns>
        public static string ToCSV(this DataTable table)
        {
            return ToCSV(table, ",", true, false, false, 0, 0);
        }

        /// <summary>
        /// Converts the passed in data table to a CSV-style string.
        /// </summary>
        /// <param name="table">Table to convert</param>
        /// <param name="includeHeader">true - include headers<br/>
        /// false - do not include header column</param>
        /// <returns>Resulting CSV-style string</returns>
        public static string ToCSV(this DataTable table, bool includeHeader)
        {
            return ToCSV(table, ",", includeHeader, false, false, 0, 0);
        }

        /// <summary>
        /// Converts the passed in data table to a CSV-style string.
        /// </summary>
        /// <param name="table">Table to convert</param>
        /// <param name="includeHeader">true - include headers<br/>
        /// false - do not include header column</param>
        /// <returns>Resulting CSV-style string</returns>
        public static string ToCSV(this DataTable table, string delimiter, bool includeHeader, bool quoteHeader, bool doNotQuoteCertainColums, int nonQuotedColumnStart, int nonQuotedColumnEnd)
        {
            var result = new StringBuilder();

            if (includeHeader)
            {
                if (quoteHeader == false)
                {
                    foreach (DataColumn column in table.Columns)
                    {
                        result.Append(column.ColumnName);
                        result.Append(delimiter);
                    }
                }
                else
                {
                    foreach (DataColumn column in table.Columns)
                    {
                        result.Append("\"" + column.ColumnName + "\"");
                        result.Append(delimiter);
                    }
                }

                result.Remove(--result.Length, 0);
                result.Append(Environment.NewLine);
            }

            int col = 0;
            foreach (DataRow row in table.Rows)
            {
                col = 0;
                foreach (object item in row.ItemArray)
                {
                    if (item is DBNull)
                        result.Append(delimiter);
                    else
                    {
                        string itemAsString = item.ToString();
                        if (doNotQuoteCertainColums == true && col >= nonQuotedColumnStart && col <= nonQuotedColumnEnd)
                        {
                        }
                        else
                        {
                            // Double up all embedded double quotes
                            itemAsString = itemAsString.Replace("\"", "\"\"");

                            // To keep things simple, always delimit with double-quotes
                            // so we don't have to determine in which cases they're necessary
                            // and which cases they're not.
                            itemAsString = "\"" + itemAsString + "\"";
                        }

                        result.Append(itemAsString + delimiter);
                    }
                    col++;
                }

                result.Remove(--result.Length, 0);
                result.Append(Environment.NewLine);
            }

            return result.ToString();
        }

        public static DataTable ConvertListToDataTable(List<string[]> list)
        {
            // Example list.
            //List<string[]> list = new List<string[]>();
            //list.Add(new string[] { "Column 1", "Column 2", "Column 3" });
            //list.Add(new string[] { "Row 2", "Row 2" });
            //list.Add(new string[] { "Row 3" });

            //// Convert to DataTable.
            //DataTable table = ConvertListToDataTable(list);
            //dataGridView1.DataSource = table;

            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 0;
            foreach (var array in list)
            {
                if (array.Length > columns)
                {
                    columns = array.Length;
                }
            }

            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }

            // Add rows.
            foreach (var array in list)
            {
                table.Rows.Add(array);
            }

            return table;
        }

        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
                  BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }

        public static void DoubleBuffered(this Control ctrl, bool setting)
        {
            Type ctrlType = ctrl.GetType();
            PropertyInfo pi = ctrlType.GetProperty("DoubleBuffered",
                  BindingFlags.Instance | BindingFlags.NonPublic);
            if (pi != null)
                pi.SetValue(ctrl, setting, null);
        }

        public static object DeepClone(object obj)
        {
            object objResult = null;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);

                ms.Position = 0;
                objResult = bf.Deserialize(ms);
            }
            return objResult;
        }

        /// <summary>
        /// Function to get byte array from a file
        /// </summary>
        /// <param name="_FileName">File name to get byte array</param>
        /// <returns>Byte Array</returns>
        public static byte[] FileToByteArray(string _FileName)
        {
            byte[] _Buffer = null;

            try
            {
                // Open file for reading
                System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                // attach filestream to binary reader
                System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);

                // get total byte length of the file
                long _TotalBytes = new System.IO.FileInfo(_FileName).Length;

                // read entire file into buffer
                _Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);

                // close file reader
                _FileStream.Close();
                _FileStream.Dispose();
                _BinaryReader.Close();
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }

            return _Buffer;
        }

        #region Chart Scaling Utilities

        public static void CalculateLogsForRange(double minVal, double maxVal, ref double NumberOfLogs)
        {
            double block = 0.0;
            if (maxVal > minVal && minVal < 0)
                block = Math.Pow(10, (int)(Math.Log(Math.Max(minVal, maxVal - minVal), 10)));
            else
                block = Math.Pow(10, (int)(Math.Log(Math.Max(minVal, maxVal), 10)));

            if (block < 1.0)
                block = 1.0;
            NumberOfLogs = block;
        }

        /// <summary>
        /// Calculate a step size based on a data range.
        /// </summary>
        /// <remarks>
        /// This utility method
        /// will try to honor the <see cref="Default.TargetXSteps"/> and
        /// <see cref="Default.TargetYSteps"/> number of
        /// steps while using a rational increment (1, 2, or 5 -- which are
        /// even divisors of 10).  This method is used by <see cref="PickScale"/>.
        /// </remarks>
        /// <param name="range">The range of data in user scale units.  This can
        /// be a full range of the data for the major step size, or just the
        /// value of the major step size to calculate the minor step size</param>
        /// <param name="targetSteps">The desired "typical" number of steps
        /// to divide the range into</param>
        /// <returns>The calculated step size for the specified data range.</returns>
        public static double CalcStepSize(double range, double targetSteps)
        {
            // Calculate an initial guess at step size
            double tempStep = range / targetSteps;

            // Get the magnitude of the step size
            double mag = Math.Floor(Math.Log10(tempStep));
            double magPow = Math.Pow((double)10.0, mag);

            // Calculate most significant digit of the new step size
            double magMsd = ((int)(tempStep / magPow + .5));

            // promote the MSD to either 1, 2, or 5
            if (magMsd > 5.0)
                magMsd = 10.0;
            else if (magMsd > 2.0)
                magMsd = 5.0;
            else if (magMsd > 1.0)
                magMsd = 2.0;

            return magMsd * magPow;
        }

        /// <summary>
        /// Calculate the modulus (remainder) in a safe manner so that divide
        /// by zero errors are avoided
        /// </summary>
        /// <param name="x">The divisor</param>
        /// <param name="y">The dividend</param>
        /// <returns>the value of the modulus, or zero for the divide-by-zero
        /// case</returns>
        public static double MyMod(double x, double y)
        {
            double temp;

            if (y == 0)
                return 0;

            temp = x / y;
            return y * (temp - Math.Floor(temp));
        }

        public const double MinLogValue = 0.0000000001; // Used to fix bug where 0 data in X scale causes chart package to get an exception!
        public const double MinChartScaleLog = 0.0000000001; // min value a user can set for any log scale
        public const double MinChartScaleLogDefault = 0.01; // default min value a user can set for any log scale
        public const double MinChartScaleAmplitude = -1000000000; // min value a user can set for an amplitude scale
        public const double MaxChartScaleAmplitude = 1000000000; // max value a user can set for an amplitude scale

        public static void CalculateAxisInfo(double minVal, double maxVal, ref double firstLabel, ref double lastLabel, ref double step, double minRange = 10, bool integerSteps = false, bool IsLog = false, double targetMajorSteps = 7.0)
        {
            if (IsLog == true) // Multiply the max value by 10 so the display is nicer
            {
                // Get the magnitude of the step size
                double mag = Math.Floor(Math.Log10(maxVal));
                double magPow = Math.Pow((double)10.0, mag);
                maxVal = magPow * 100.0;
                mag = Math.Floor(Math.Log10(minVal));
                magPow = Math.Pow((double)10.0, mag);
                minVal = Math.Max(magPow / 100.0, MinChartScaleLog);
            }
            step = CalcStepSize(maxVal - minVal, targetMajorSteps);

            double targetMinorYSteps = 5.0;

            double _minorStep = CalcStepSize(step, targetMinorYSteps);

            // Calculate the scale minimum
            firstLabel = minVal - MyMod(minVal, step);

            // Calculate the scale maximum
            lastLabel = MyMod(maxVal, step) == 0.0 ? maxVal :
                    maxVal + step - MyMod(maxVal, step);

            if ((lastLabel - firstLabel) < minRange)
            {
                lastLabel = minRange; // lastLabel += 10.0;
                step = minRange / 5;
            }
        }

        //public void CalculateAndSetChartMaxMinInterValForX(System.Windows.Forms.DataVisualization.Charting.Chart chart, double xMin, double xMax, double minRange = 10)
        //{
        //    double firstLabel = 0.0, lastLabel = 0.0, step = 0.0;
        //    CalculateAxisInfo(xMin, xMax, ref firstLabel, ref lastLabel, ref step, minRange, false, chart.ChartAreas["Default"].AxisX.IsLogarithmic); // NGE08072012 Added chart.ChartAreas["Default"].AxisX.IsLogarithmic
        //    if (step > 10)
        //        step = ((int)Math.Round(step, MidpointRounding.AwayFromZero)); // step;
        //    chart.ChartAreas["Default"].AxisX.Minimum = firstLabel;
        //    if (xMax - xMin < minRange)
        //    {
        //        if (xMax == lastLabel)
        //        {
        //            xMax = minRange;
        //        }
        //        else
        //        {
        //            xMax = lastLabel;
        //        }
        //    }
        //    if (xMax > minRange)
        //    {
        //        while (xMax < lastLabel - step)
        //        {
        //            lastLabel -= step;
        //        }
        //    }
        //    chart.ChartAreas["Default"].AxisX.Maximum = lastLabel;
        //    chart.ChartAreas["Default"].AxisX.Interval = step;
        //}

        //public void CalculateAndSetChartMaxMinInterValForY(System.Windows.Forms.DataVisualization.Charting.Chart chart, double yMin, double yMax, double minRange = 10, bool IsY2Axis = false)
        //{
        //    double firstLabel = 0.0, lastLabel = 0.0, step = 0.0;
        //    bool IsLog = false;  
        //    if (IsY2Axis == true)
        //    {
        //        IsLog = chart.ChartAreas["Default"].AxisY2.IsLogarithmic;
        //    }
        //    else
        //    {
        //        IsLog = chart.ChartAreas["Default"].AxisY.IsLogarithmic;
        //    }
        //    CalculateAxisInfo(yMin, yMax, ref firstLabel, ref lastLabel, ref step, minRange, false, IsLog);
        //    if (step > 10)
        //        step = ((int)Math.Round(step, MidpointRounding.AwayFromZero)); // step;
        //    if (yMax - yMin < minRange)
        //    {
        //        if (yMax == lastLabel)
        //        {
        //            yMax = minRange;
        //        }
        //        else
        //        {
        //            yMax = lastLabel;
        //        }
        //    }
        //    if (yMax > minRange)
        //    {
        //        while (yMax < lastLabel - step)
        //        {
        //            lastLabel -= step;
        //        }
        //    }
        //    if (IsY2Axis == true)
        //    {
        //        chart.ChartAreas["Default"].AxisY2.Minimum = firstLabel;
        //        chart.ChartAreas["Default"].AxisY2.Maximum = lastLabel;
        //        chart.ChartAreas["Default"].AxisY2.Interval = step;
        //    }
        //    else
        //    {
        //        chart.ChartAreas["Default"].AxisY.Minimum = firstLabel;
        //        chart.ChartAreas["Default"].AxisY.Maximum = lastLabel;
        //        chart.ChartAreas["Default"].AxisY.Interval = step;
        //    }
        //}

        //public static void SetScatterChartScalesAndInterval(ZedGraph.ZedGraphControl chart, double xMax, double xMin, double yMax, double yMin)
        //{
        //    SetScatterChartScalesAndInterval(chart, xMax, xMin, yMax, yMin, true);
        //}

        //public static void SetScatterChartScaleAndInterval(ZedGraph.ZedGraphControl chart, ZedGraph.Axis axis, double valueMax, double valueMin, bool ClipScaleToHighestValue)
        //{
        //    SetScatterChartScale(chart, axis, valueMax, valueMin);
        //    if (axis.Scale.IsLog == false) // if (!checkBoxLogScale.Checked)
        //    {
        //        // axis.Minimum = 0.0;
        //        // Now already set axis.Scale.MajorStep = (axis.Scale.Max - axis.Scale.Min) / 5;
        //    }
        //    else
        //    {
        //        if (axis.Scale.Min == 0)
        //            axis.Scale.Min = MinChartScaleAmplitude;
        //        axis.Scale.Min = Math.Max(MinChartScaleAmplitude, axis.Scale.Min);

        //        if (axis.Scale.Max <= axis.Scale.Min)
        //        {
        //            axis.Scale.Max = axis.Scale.Min + 1;
        //        }

        //        axis.Scale.MajorStep = 0;
        //    }
        //    if (ClipScaleToHighestValue == true)
        //        axis.Scale.Max = valueMax + 1;
        //}

        //public static void SetScatterChartScalesAndInterval(ZedGraph.ZedGraphControl chart, double xMax, double xMin, double yMax, double yMin, bool ClipXScale)
        //{
        //    SetScatterChartScaleAndInterval(chart, chart.GraphPane.XAxis, xMax, xMin, ClipXScale);
        //    SetScatterChartScaleAndInterval(chart, chart.GraphPane.YAxis, yMax, yMin, false);
        //}

        //private static void SetScatterChartScale(ZedGraph.ZedGraphControl chart, ZedGraph.Axis axis, double valueMax, double valueMin)
        //{
        //    double firstLabel = 0.0, lastLabel = 0.0, step = 0.0;
        //    CalculateAxisInfo(valueMin, valueMax, ref firstLabel, ref lastLabel, ref step);
        //    axis.Scale.Min = firstLabel; // yMin;
        //    axis.Scale.MajorStep = step;
        //    if (valueMax - valueMin < 10)
        //    {
        //        if (valueMax == lastLabel)
        //        {
        //            valueMax = 10;
        //        }
        //        else
        //        {
        //            valueMax = lastLabel;
        //        }
        //    }
        //    if (valueMax > 10)
        //    {
        //        while (valueMax < lastLabel - step)
        //        {
        //            lastLabel -= step;
        //        }
        //    }
        //    axis.Scale.Max = lastLabel; // yMax;
        //}

        //private static void SetScatterChartScales(ZedGraph.ZedGraphControl chart, double xMax, double xMin, double yMax, double yMin)
        //{
        //    SetScatterChartScale(chart, chart.GraphPane.XAxis, xMax, xMin);
        //    SetScatterChartScale(chart, chart.GraphPane.YAxis, yMax, yMin);
        //}
        #endregion Chart Scaling Utilities

        public static void Quicksort(IComparable[] elements, int left, int right)
        {
            int i = left, j = right;
            IComparable pivot = elements[(left + right) / 2];

            while (i <= j)
            {
                while (elements[i].CompareTo(pivot) < 0)
                {
                    i++;
                }

                while (elements[j].CompareTo(pivot) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                    IComparable tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;

                    i++;
                    j--;
                }
            }

            // Recursive calls
            if (left < j)
            {
                Quicksort(elements, left, j);
            }

            if (i < right)
            {
                Quicksort(elements, i, right);
            }
        }

        /// <summary>
        /// Determines if int array is sorted from 0 -> Max
        /// </summary>
        public static bool IsSorted(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i - 1] > arr[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Determines if int array is sorted from 0 -> Max
        /// </summary>
        public static bool IsSorted(float[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i - 1] > arr[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Determines if string array is sorted from A -> Z
        /// </summary>
        public static bool IsSorted(string[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i - 1].CompareTo(arr[i]) > 0) // If previous is bigger, return false
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Determines if int array is sorted from Max -> 0
        /// </summary>
        public static bool IsSortedDescending(int[] arr)
        {
            for (int i = arr.Length - 2; i >= 0; i--)
            {
                if (arr[i] < arr[i + 1])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Determines if int array is sorted from Max -> 0
        /// </summary>
        public static bool IsSortedDescending(float[] arr)
        {
            for (int i = arr.Length - 2; i >= 0; i--)
            {
                if (arr[i] < arr[i + 1])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Determines if string array is sorted from Z -> A
        /// </summary>
        public static bool IsSortedDescending(string[] arr)
        {
            for (int i = arr.Length - 2; i >= 0; i--)
            {
                if (arr[i].CompareTo(arr[i + 1]) < 0) // If previous is smaller, return false
                {
                    return false;
                }
            }
            return true;
        }

        public static void DeleteDirectory(string target)
        {
            try
            {
                string[] files = Directory.GetFiles(target);
                string[] dir = Directory.GetDirectories(target);

                foreach (string file in files)
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                }

                foreach (string directory in dir)
                {
                    DeleteDirectory(directory);
                }

                Directory.Delete(target, true);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        public static void RemoveDirectory(string directoryName)
        {
            try
            {
                System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(directoryName);
                foreach (System.IO.FileInfo file in directory.GetFiles()) file.Delete();
                foreach (System.IO.DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
                File.Delete(directoryName);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        public static void FileCopyWhileKeepingFileCreationDateTimeStamp(string originalFile, string newFile)
        {
            System.IO.File.Copy(originalFile, newFile);

            string DateTimeFormat = "yyyy:MM:dd HH:mm:ss";

            DateTime FileCreationDateTime = System.IO.File.GetCreationTime(originalFile);
            string WindowsFileCreationDateTimeString = FileCreationDateTime.ToString(DateTimeFormat);

            int year = 0, month = 0, day = 0, hour = 0, minute = 0, second = 0, millisecond = 0;
            int start = 0, end = WindowsFileCreationDateTimeString.Length;
            int TimeStampLength = end - start;

            DateTime ParsedDateTime = FileCreationDateTime;

            if (TimeStampLength == 19 || TimeStampLength == 23)
            {
                year = Convert.ToInt32(WindowsFileCreationDateTimeString.Substring(start, 4));
                month = Convert.ToInt32(WindowsFileCreationDateTimeString.Substring(start + 5, 2));
                day = Convert.ToInt32(WindowsFileCreationDateTimeString.Substring(start + 8, 2));
                hour = Convert.ToInt32(WindowsFileCreationDateTimeString.Substring(start + 11, 2));
                minute = Convert.ToInt32(WindowsFileCreationDateTimeString.Substring(start + 14, 2));
                second = Convert.ToInt32(WindowsFileCreationDateTimeString.Substring(start + 17, 2));

                if (TimeStampLength == 23)
                    millisecond = Convert.ToInt32(WindowsFileCreationDateTimeString.Substring(start + 20, 3));

                ParsedDateTime = new DateTime(year, month, day, hour, minute, second, millisecond);
            }

            System.IO.File.SetCreationTime(newFile, ParsedDateTime);
        }

    }

}
