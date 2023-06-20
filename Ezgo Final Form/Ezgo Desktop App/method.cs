using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;

namespace Ezgo_Desktop_App
{
    public class Methods {
        public Dictionary<string, object> ArrayMaker(string[] keys, object[] values) {
            Dictionary<string, object> dictionary = keys
                .Zip(values, (k, v) => new { Key = k, Value = v })
                .ToDictionary(x => x.Key, x => x.Value);
            return dictionary;
        }

        public bool Insert(System.Windows.Forms.Label[] labels, object[] inputs, employee empl, int kode = 0)
        {
            string[] keys = new string[labels.Length];
            object[] values = new object[labels.Length];

            int i = 0;

            foreach (System.Windows.Forms.Label label in labels)
            {
                keys[i] = label.Text;
                i++;
            }

            i = 0;
            int x = 0;
            bool skip = false;
            string temp = "";
            int time = 0;
            TimeSpan waktu = new TimeSpan();

            foreach (object input in inputs)
            {
                switch (kode)
                {
                    case 1:
                        if (i == 5)
                        {
                            if (input is NumericUpDown num)
                            {
                                time = (int)num.Value;
                            }

                            skip = true;
                        }
                        else if (i == 6)
                        {
                            if (input is NumericUpDown num)
                            {
                                waktu = new TimeSpan(time, (int)num.Value, 0);
                                time = 0;
                            }

                            skip = false;
                            x++;
                        }
                        else if (i == 9)
                        {
                            if (input is NumericUpDown num)
                            {
                                time = (int)num.Value;
                            }

                            skip = true;
                        }
                        else if (i == 10) {
                            if (input is NumericUpDown num)
                            {
                                temp = $"{time}.{num.Value}";
                                time = 0;
                            }

                            skip = false;
                            x++;
                        }

                        break;
                    case 3:
                        if (i == 4)
                        {
                            if (input is NumericUpDown num)
                            {
                                time = (int)num.Value;
                            }

                            skip = true;
                        }
                        else if (i == 5)
                        {
                            if (input is NumericUpDown num)
                            {
                                waktu = new TimeSpan(time, (int)num.Value, 0);
                                time = 0;
                            }

                            skip = false;
                            x++;
                        }

                        break;
                }

                if (!skip)
                {
                    if (input is TextBox tb)
                    {
                        switch (kode)
                        {
                            case 0:
                                if (i == 7)
                                {
                                    values[i - x] = int.Parse(tb.Text);
                                }
                                else {
                                    values[i - x] = tb.Text;
                                }
                                break;
                            case 1:
                                if (i == 11 || i == 12 || i == 13)
                                {
                                    values[i - x] = int.Parse(tb.Text);
                                }
                                else
                                {
                                    values[i - x] = tb.Text;
                                }

                                break;
                            case 2:
                                if (i == 6 || i == 7 || i == 8)
                                {
                                    values[i - x] = int.Parse(tb.Text);
                                }
                                else
                                {
                                    values[i - x] = tb.Text;
                                }

                                break;
                            case 3:
                                if (i == 7|| i == 8 || i == 9)
                                {
                                    values[i - x] = int.Parse(tb.Text);
                                }
                                else
                                {
                                    values[i - x] = tb.Text;
                                }

                                break;
                            case 4:
                                values[i - x] = tb.Text;
                                break;
                        }
                    }
                    else if (input is NumericUpDown)
                    {
                        if (kode == 3)
                        {
                            values[i - x] = waktu;
                            waktu = new TimeSpan();
                        }
                        else if (kode == 1 && i == 6)
                        {
                            values[i - x] = waktu;
                            waktu = new TimeSpan();
                        }
                        else {
                            values[i - x] = temp;
                            temp = "";
                        }
                    }
                    else if (input is DateTimePicker dtp)
                    {
                        values[i - x] = dtp.Value;
                    }
                    else if (input is ComboBox cb)
                    {
                        if (cb.SelectedIndex != -1)
                        {
                            string[] code = cb.Items[cb.SelectedIndex].ToString().Split(new[] { " - " }, StringSplitOptions.None);
                            values[i - x] = code[0];
                        }
                        else {
                            values[i - x] = "";
                        }
                    }
                    else if (input is string str)
                    {
                        values[i - x] = str;
                    }
                }
                i++;
            }

            if (empl is manager mng)
            {
                if (kode == 0)
                {
                    if (mng.InsertStaff(keys, values))
                    {
                        x = 0;
                        return true;
                    }
                }
                else if (kode >= 1 && kode <= 3)
                {
                    int y = 0;
                    foreach (string key in keys)
                    {
                        y++;
                    }
                    if (mng.InsertStock(kode, keys, values))
                    {
                        x = 0;
                        return true;
                    }
                }
                else if (kode == 4)
                {
                    if (mng.InsertVendor(keys, values))
                    {
                        x = 0;
                        return true;
                    }
                }
            }
            else if (empl is sales sls) {
                if (kode == 0)
                {
                    if (sls.InsertStaff(keys, values))
                    {
                        x = 0;
                        return true;
                    }
                }
                else if (kode >= 1 && kode <= 3)
                {
                    int y = 0;
                    foreach (string key in keys)
                    {
                        y++;
                    }
                    if (sls.InsertStock(kode, keys, values))
                    {
                        x = 0;
                        return true;
                    }
                }
            }

            return false;
        }

        public bool Update(System.Windows.Forms.Label[] labels, object[] inputs, string[] codes, object[] param, int kode, employee empl)
        {
            string[] keys = new string[labels.Length];
            object[] values = new object[labels.Length];

            int i = 0;

            foreach (System.Windows.Forms.Label label in labels)
            {
                keys[i] = label.Text;
                i++;
            }

            i = 0;
            int x = 0;
            bool skip = false;
            string temp = "";
            int time = 0;
            TimeSpan waktu = new TimeSpan();

            foreach (object input in inputs)
            {
                switch (kode)
                {
                    case 1:
                        if (i == 1)
                        {
                            if (input is NumericUpDown num)
                            {
                                time = (int)num.Value;
                            }

                            skip = true;
                        }
                        else if (i == 2)
                        {
                            if (input is NumericUpDown num)
                            {
                                waktu = new TimeSpan(time, (int)num.Value, 0);
                                time = 0;
                            }

                            skip = false;
                            x++;
                        }
                        else if (i == 5)
                        {
                            if (input is NumericUpDown num)
                            {
                                time = (int)num.Value;
                            }

                            skip = true;
                        }
                        else if (i == 6)
                        {
                            if (input is NumericUpDown num)
                            {
                                temp = $"{time}.{num.Value}";
                                time = 0;
                            }

                            skip = false;
                            x++;
                        }

                        break;
                    case 3:
                        if (i == 2)
                        {
                            if (input is NumericUpDown num)
                            {
                                time = (int)num.Value;
                            }

                            skip = true;
                        }
                        else if (i == 3)
                        {
                            if (input is NumericUpDown num)
                            {
                                waktu = new TimeSpan(time, (int)num.Value, 0);
                                time = 0;
                            }

                            skip = false;
                            x++;
                        }

                        break;
                }

                if (!skip)
                {
                    if (input is TextBox tb)
                    {
                        switch (kode)
                        {
                            case 0:
                                if (i == 5)
                                {
                                    values[i - x] = int.Parse(tb.Text);
                                }
                                else
                                {
                                    values[i - x] = tb.Text;
                                }
                                break;
                            case 1:
                                if (i == 8 || i == 9 || i == 10)
                                {
                                    values[i - x] = int.Parse(tb.Text);
                                }
                                else
                                {
                                    values[i - x] = tb.Text;
                                }

                                break;
                            case 2:
                                if (i == 6 || i == 7 || i == 8)
                                {
                                    values[i - x] = int.Parse(tb.Text);
                                }
                                else
                                {
                                    values[i - x] = tb.Text;
                                }

                                break;
                            case 3:
                                if (i == 5 || i == 6 || i == 7)
                                {
                                    values[i - x] = int.Parse(tb.Text);
                                }
                                else
                                {
                                    values[i - x] = tb.Text;
                                }

                                break;
                            case 4:
                                values[i - x] = tb.Text;
                                break;
                            case 5:
                                values[i - x] = tb.Text;
                                break;
                        }
                    }
                    else if (input is NumericUpDown)
                    {
                        if (kode == 3)
                        {
                            values[i - x] = waktu;
                            waktu = new TimeSpan();
                        }
                        else if (kode == 1 && i == 2)
                        {
                            values[i - x] = waktu;
                            waktu = new TimeSpan();
                        }
                        else
                        {
                            values[i - x] = temp;
                            temp = "";
                        }
                    }
                    else if (input is DateTimePicker dtp)
                    {
                        values[i - x] = dtp.Value;
                    }
                    else if (input is ComboBox cb)
                    {
                        string[] code = cb.Items[cb.SelectedIndex].ToString().Split(new[] { " - " }, StringSplitOptions.None);
                        values[i - x] = code[0];
                    }
                    else if (input is string str)
                    {
                        values[i - x] = str;
                    }
                }
                i++;
            }

            if (empl is manager mng)
            {
                if (kode == 0)
                {
                    if (mng.UpdateStaff(keys, values, codes, param))
                    {
                        x = 0;
                        return true;
                    }
                }
                else if (kode >= 1 && kode <= 3)
                {
                    if (mng.UpdateStock(kode, keys, values, codes, param))
                    {
                        x = 0;
                        return true;
                    }
                }
                else if (kode == 4)
                {
                    if (mng.UpdateReport(keys, values, codes, param))
                    {
                        x = 0;
                        return true;
                    }
                }
                else if (kode == 5) {
                    if (mng.UpdateVendor(keys, values, codes, param))
                    {
                        x = 0;
                        return true;
                    }
                }
            }

            if (empl is sales sls)
            {
                if (kode == 0)
                {
                    if (sls.UpdateStaff(keys, values, codes, param))
                    {
                        x = 0;
                        return true;
                    }
                }
                else if (kode >= 1 && kode <= 3)
                {
                    if (sls.UpdateStock(kode, keys, values, codes, param))
                    {
                        x = 0;
                        return true;
                    }
                }
                else if (kode == 4)
                {
                    if (sls.UpdateReport(keys, values, codes, param))
                    {
                        x = 0;
                        return true;
                    }
                }
            }

            return false;
        }

        public void PickImage(updateStock ups)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                string fileName = Path.GetFileName(filePath);
                string directoryPath = @"C:\Users\DELL\Documents\VSCODE\GitHub\testing\Web-Design-and-Development-Project\ezgo\public\img\";
                string newFilePath = Path.Combine(directoryPath, fileName);

                try
                {
                    if (File.Exists(newFilePath))
                    {
                        ups.image = fileName; 
                    }
                    else
                    {
                        File.Copy(filePath, newFilePath);
                        ups.image = fileName; 
                    }
                    MessageBox.Show("Image Uploaded");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }


        public void PickImage(updateStockChild upc)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                string fileName = Path.GetFileName(filePath);
                string directoryPath = @"C:\Users\DELL\Documents\VSCODE\GitHub\testing\Web-Design-and-Development-Project\ezgo\public\img\";
                string newFilePath = Path.Combine(directoryPath, fileName);

                try
                {
                    if (File.Exists(newFilePath))
                    {
                        upc.image = fileName;
                    }
                    else
                    {
                        File.Copy(filePath, newFilePath);
                        upc.image = fileName;
                    }
                    MessageBox.Show("Image Uploaded");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        public string getAddress(DataGridViewRow row) {
            string[] keys = { "reportID" };
            string[] values = { row.Cells["reportID"].Value.ToString() };

            string[] table = { "Reports" };
            string[] fields = { "rAddress" };
            Dictionary<string, object> parameter = ArrayMaker(keys, values);
            string[] oprtr = { "=" };

            DataTable result = DB.Select(fields, table, oprtr, parameter);
            return result.Rows[0]["rAddress"].ToString();
        }

        public DataTable getReport(int id) {
            string[] keys = { "reportID" };
            object[] values = { id };

            string[] table = { "Reports" };
            string[] fields = { "rReason" };
            Dictionary<string, object> parameter = ArrayMaker(keys, values);
            string[] oprtr = { "=" };

            DataTable result = DB.Select(fields, table, oprtr, parameter);
            return result;
        }

        public DataTable BuildReport(string[] fields, string[] table, string[] oprtrs, string[] keys, object[] values, ref CrystalReport1 rpt)
        {
            Dictionary<string, object> parameter = ArrayMaker(keys, values);
            DataTable dt = DB.Select(fields, table, oprtrs, parameter);
            rpt.SetDataSource(dt);
            return dt;
        }

        public DataTable BuildReport(string[] fields, string[] table, string[] oprtrs, string[] keys, object[] values, ref CrystalReport2 rpt)
        {
            Dictionary<string, object> parameter = ArrayMaker(keys, values);
            DataTable dt = DB.Select(fields, table, oprtrs, parameter);
            rpt.SetDataSource(dt);
            return dt;
        }

        public DataTable BuildReport(string[] fields, string[] table, string[] oprtrs, string[] keys, object[] values, ref CrystalReport4 rpt)
        {
            Dictionary<string, object> parameter = ArrayMaker(keys, values);
            DataTable dt = DB.Select(fields, table, oprtrs, parameter);
            rpt.SetDataSource(dt);
            return dt;
        }

        public DataTable BuildReport(string[] fields, string[] table, string[] oprtrs, string[] keys, object[] values, ref CrystalReport5 rpt)
        {
            Dictionary<string, object> parameter = ArrayMaker(keys, values);
            DataTable dt = DB.Select(fields, table, oprtrs, parameter);
            rpt.SetDataSource(dt);
            return dt;
        }

        public DataTable BuildReport(string[] fields, string[] table, string[] oprtrs, string[] keys, object[] values, ref CrystalReport3 rpt)
        {
            Dictionary<string, object> parameter = ArrayMaker(keys, values);
            DataTable dt = DB.Select(fields, table, oprtrs, parameter);
            rpt.SetDataSource(dt);
            return dt;
        }

        public DataTable BuildReport(string[] fields, string[] table, string[] oprtrs, string[] keys, object[] values, ref CrystalReport6 rpt)
        {
            Dictionary<string, object> parameter = ArrayMaker(keys, values);
            DataTable dt = DB.Select(fields, table, oprtrs, parameter);
            rpt.SetDataSource(dt);
            return dt;
        }

        public DataTable BuildReport(string[] fields, string[] table, string[] oprtrs, string[] keys, object[] values, ref CrystalReport7 rpt)
        {
            Dictionary<string, object> parameter = ArrayMaker(keys, values);
            DataTable dt = DB.Select(fields, table, oprtrs, parameter);
            rpt.SetDataSource(dt);
            return dt;
        }

        public DataTable BuildReport(string[] fields, string[] table, string[] oprtrs, string[] keys, object[] values, ref CrystalReport1 rpt, string[] join)
        {
            Dictionary<string, object> parameter = ArrayMaker(keys, values);
            DataTable dt = DB.Select(fields, table, oprtrs, parameter, join);
            rpt.SetDataSource(dt);
            return dt;
        }

        public DataTable BuildReport(string[] fields, string[] table, string[] oprtrs, string[] keys, object[] values, ref CrystalReport2 rpt, string[] join)
        {
            Dictionary<string, object> parameter = ArrayMaker(keys, values);
            DataTable dt = DB.Select(fields, table, oprtrs, parameter, join);
            rpt.SetDataSource(dt);
            return dt;
        }

        public DataTable BuildReport(string[] fields, string[] table, string[] oprtrs, string[] keys, object[] values, ref CrystalReport3 rpt, string[] join)
        {
            Dictionary<string, object> parameter = ArrayMaker(keys, values);
            DataTable dt = DB.Select(fields, table, oprtrs, parameter, join);
            rpt.SetDataSource(dt);
            return dt;
        }

        public DataTable BuildReport(string[] fields, string[] table, string[] oprtrs, string[] keys, object[] values, ref CrystalReport11 rpt, string[] join)
        {
            Dictionary<string, object> parameter = ArrayMaker(keys, values);
            DataTable dt = DB.Select(fields, table, oprtrs, parameter, join);
            rpt.SetDataSource(dt);
            return dt;
        }

        public string[] ArrayFromTable(DataTable dt, string column) {
            string[] columnArray = dt.AsEnumerable()
                               .Select(row => row.Field<object>(column).ToString())
                               .ToArray();
            return columnArray;

        }

        public string[] CombineArrayTwo(string[] arr1, string[] arr2) {
            string[] arr = new string[0];
            if (arr1.Length == arr2.Length) { 
                arr = new string[arr1.Length];
                for (int i = 0; i < arr1.Length; i++)
                {
                    arr[i] = arr1[i] + " - " + arr2[i];
                }
            }
            return arr;
        }

        public string[] VendorList()
        {
            string[] fields = { "*" };
            string[] table = { "Vendor" };
            string[] oprtr = new string[0];
            Dictionary<string, object> parameter = new Dictionary<string, object>();

            DataTable result = DB.Select(fields, table, oprtr, parameter);
            string[] ret = new string[result.Rows.Count];
            int i = 0;
            foreach (DataRow dr in result.Rows)
            {
               ret[i] = $"{dr[0]} - {dr[1]}";
               i++;
            }
            return ret;
        }

        public string[] EmployeeList()
        {
            string[] fields = { "*" };
            string[] table = { "Employee" };
            string[] oprtr = new string[0];
            Dictionary<string, object> parameter = new Dictionary<string, object>();

            DataTable result = DB.Select(fields, table, oprtr, parameter);
            string[] ret = new string[result.Rows.Count];
            int i = 0;
            foreach (DataRow dr in result.Rows)
            {
                ret[i] = $"{dr[0]} - {dr[3]}";
                i++;
            }
            return ret;
        }

        public string[] RolesList()
        {
            string[] fields = { "*" };
            string[] table = { "Roles" };
            string[] oprtr = new string[0];
            Dictionary<string, object> parameter = new Dictionary<string, object>();

            DataTable result = DB.Select(fields, table, oprtr, parameter);
            string[] ret = new string[result.Rows.Count];
            int i = 0;
            foreach (DataRow dr in result.Rows)
            {
                ret[i] = $"{dr[0]} - {dr[1]}";
                i++;
            }
            return ret;
        }

        public DataTable GetColumn(string column, string table) {
            string[] tables = { table };
            string[] fields = { column };
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            string[] oprtr = new string[0];

            DataTable result = DB.Select(fields, tables, oprtr, parameter);
            return result;
        }

        public DataTable GetGuidesWhere(string branch)
        {
            string[] fields = { "employeeID", "eName" };
            string[] table = { "Employee" };
            string[] oprtr = { "=" };
            string[] keys = { "destID" };
            string[] values = { branch };
            Dictionary<string, object> parameter = ArrayMaker(keys, values);

            DataTable result = DB.Select(fields, table, oprtr, parameter);
            return result;
        }
    }
}
