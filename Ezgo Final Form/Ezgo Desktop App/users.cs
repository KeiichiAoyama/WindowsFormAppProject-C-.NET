using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;
using Section = CrystalDecisions.CrystalReports.Engine.Section;

namespace Ezgo_Desktop_App
{
    public class employee {
        public string id;
        public string key;
        public string name;
        public string branch;
        public string phone;
        public string email;
        public DateTime birthday;
        public int salary;

        DB db = new DB();
        Methods mtd = new Methods();

        public employee() { }

        public void login(string id, string password) {
            using (SHA256 hash = SHA256.Create()) {
                byte[] byteHash = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                string passwordHash = BitConverter.ToString(byteHash).Replace("-", "").ToLower();

                string[] table = { "Employee" };
                string[] fields = {"*"};
                string[] keys = { "EmployeeID" };
                object[] values = { id };
                Dictionary<string, object> parameter = mtd.ArrayMaker(keys, values);
                string[] oprtr = {"="};

                DataTable result = DB.Select(fields, table, oprtr, parameter); 

                if (result.Rows.Count > 0)
                {
                    this.id = (string)result.Rows[0]["employeeID"];
                    string pw = (string)result.Rows[0]["password"];
                    if (pw == passwordHash)
                    {
                        load(result);
                    }
                    else {
                        MessageBox.Show("Password anda salah");
                    }
                }
                else {
                    MessageBox.Show("NIK anda salah.");
                }
            }
        }

        public void load(DataTable user) {
            this.key = (string)user.Rows[0]["roleID"];
            this.name = (string)user.Rows[0]["eName"];
            this.branch = (string)user.Rows[0]["destID"];
            this.phone = (string)user.Rows[0]["ePhone"];
            this.email = (string)user.Rows[0]["eEmail"];
            this.birthday = (DateTime)user.Rows[0]["eBirthDate"];
            this.salary = (int)user.Rows[0]["eSalary"];

            switch (this.key) {
                case "trg":
                    TourGuide trg = new TourGuide(this);
                    trg.Show();
                    break;
                case "sls":
                    Sales sls = new Sales(this);
                    sls.Show();
                    break;
                case "mng":
                    Manager mng = new Manager(this);
                    mng.Show();
                    break;
            }
        }

        public bool InsertReport(string[] keys, object[] values)
        {
            string table = "Reports";
            Dictionary<string, object> parameter = mtd.ArrayMaker(keys, values);
            int x = DB.Insert(table, parameter);

            if (x > 0)
            {
                return true;
            }
            return false;
        }

        public bool UpdateReport(string[] keys, object[] values, string[] field, object[] where)
        {
            string table = "Reports";
            Dictionary<string, object> updates = mtd.ArrayMaker(keys, values);
            Dictionary<string, object> parameter = mtd.ArrayMaker(field, where);
            int x = DB.Update(table, parameter, updates);

            if (x > 0)
            {
                return true;
            }
            return false;
        }
    }

    public class manager : employee {
        DB db = new DB();
        Methods mtd = new Methods();

        public manager(employee e) {
            this.id = e.id;
            this.name = e.name;
            this.birthday = e.birthday;
            this.phone = e.phone;
            this.email = e.email;
            this.branch = e.branch;
            this.salary = e.salary;
            this.key = e.key;
        }

        public DataTable ViewStock(int code, int key = 0) {
            DataTable result = new DataTable();
            string[] table = new string[1];
            string type = "";

            switch (code) {
                case 1:
                    table[0] = "Tickets";

                    if (key != 0) {
                        switch (key) {
                            case 1:
                                type = "krt";
                                break;
                            case 2:
                                type = "fer";
                                break;
                            case 3:
                                type = "pln";
                                break;
                        }
                    }

                    break;
                case 2:
                    table[0] = "Hotel";
                    break;
                case 3:
                    table[0] = "Tour";
                    break;
            }

            string[] fields = { "*" };
            string[] keys = { "tcType" };
            string[] values = { type };
            Dictionary<string, object> parameter = (key != 0 ? mtd.ArrayMaker(keys, values) : new Dictionary<string, object>());
            string[] oprtr = { "=" };
            result = DB.Select(fields, table, oprtr, parameter);
           
            return result;
        }

        public bool InsertProduct(int code, string vendor, string id)
        {
            string table = "Products";
            string[] key = new string[0];
            object[] values = new string[0];

            switch (code)
            {
                case 1:
                    key = new string[] { "productID", "keyID", "vendorID", "destID" };
                    values = new string[] { id, "tkt", vendor, this.branch };
                    break;
                case 2:
                    key = new string[] { "productID", "keyID", "vendorID", "destID" };
                    values = new string[] { id, "htl", vendor, this.branch };
                    break;
                case 3:
                    key = new string[] { "productID", "keyID", "vendorID", "destID" };
                    values = new string[] { id, "trp", vendor, this.branch };
                    break;
            }

            Dictionary<string, object> parameter = mtd.ArrayMaker(key, values);
            int x = DB.Insert(table, parameter);

            if (x > 0)
            {
                return true;
            }
            return false;
        }

        public bool InsertStock(int code, string[] keys, object[] values)
        {
            string table = "";
            string vendor = "";
            string id = "";

            switch (code)
            {
                case 1:
                    table = "Tickets";
                    id = values[0].ToString();
                    vendor = values[13].ToString();
                    break;
                case 2:
                    id = values[0].ToString();
                    vendor = values[10].ToString();
                    table = "Hotel";
                    break;
                case 3:
                    id = values[0].ToString();
                    vendor = "vd000";
                    table = "Tour";
                    break;
            }

            if (InsertProduct(code, vendor, id))
            {
                Dictionary<string, object> parameter = mtd.ArrayMaker(keys, values);
                int x = DB.Insert(table, parameter);

                if (x > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool UpdateStock(int code, string[] keys, object[] values, string[] field, object[] where)
        {
            string table = "";

            switch (code)
            {
                case 1:
                    table = "Tickets";
                    break;
                case 2:
                    table = "Hotel";
                    break;
                case 3:
                    table = "Tour";
                    break;
            }

            Dictionary<string, object> updates = mtd.ArrayMaker(keys, values);
            Dictionary<string, object> parameter = mtd.ArrayMaker(field, where);
            int x = DB.Update(table,parameter ,updates);

            if (x > 0)
            {
                return true;
            }
            return false;
        }

        public bool DeleteStock(int code, string[] keys, object[] values, string[]field, object[]content)
        {
            string table = "";

            switch (code)
            {
                case 1:
                    table = "Tickets";
                    break;
                case 2:
                    table = "Hotel";
                    break;
                case 3:
                    table = "Tour";
                    break;
            }

            Dictionary<string, object> parameter = mtd.ArrayMaker(keys, values);
            int x = DB.Delete(table, parameter);

            if (x > 0)
            {
                parameter = mtd.ArrayMaker(field, content);
                int y = DB.Delete("Products", parameter);
                if (y > 0) {
                    return true;
                }
            }
            return false;
        }

        public DataTable ViewStaff(int roleCode, int branch = 0)
        {
            string role = "";

            switch (roleCode) {
                case 1:
                    role = "mng";
                    break;
                case 2:
                    role = "sls";
                    break;
                case 3:
                    role = "trg";
                    break;
            }

            string[] keys = new string[(branch != 0 ? 2 : 1)];
            string[] values = new string[(branch != 0 ? 2 : 1)];

            keys[0] = "roleID";
            values[0] = role;

            switch (branch) {
                case 1:
                    keys[1] = "destID";
                    values[1] = "jkt";
                    break;
                case 2:
                    keys[1] = "destID";
                    values[1] = "bdg";
                    break;
                case 3:
                    keys[1] = "destID";
                    values[1] = "sby";
                    break;
                case 4:
                    keys[1] = "destID";
                    values[1] = "dps";
                    break;
            }

            string[] table = { "Employee" };
            string[] fields = { "*" };
            Dictionary<string, object> parameter = mtd.ArrayMaker(keys, values);
            string[] oprtr = { "=", "=" };

            DataTable result = DB.Select(fields, table, oprtr, parameter);
            return result;
        }

        public bool InsertStaff(string[] keys, object[] values)
        {
            string table = "Employee";
            Dictionary<string, object> parameter = mtd.ArrayMaker(keys, values);
            foreach (var pair in parameter) {
                MessageBox.Show($"{pair.Key} {pair.Value}");
            }
            int x = DB.Insert(table, parameter);

            if (x > 0)
            {
                return true;
            }
            return false;
        }

        public bool UpdateStaff(string[] keys, object[] values, string[] field, object[] where)
        {
            string table = "Employee";
            Dictionary<string, object> updates = mtd.ArrayMaker(keys, values);
            Dictionary<string, object> parameter = mtd.ArrayMaker(field, where);
            int x = DB.Update(table, parameter, updates);

            if (x > 0)
            {
                return true;
            }
            return false;
        }

        public bool DeleteStaff(int code, string[] keys, object[] values)
        {
            string table = "Employee";
            Dictionary<string, object> parameter = mtd.ArrayMaker(keys, values);
            int x = DB.Delete(table, parameter);

            if (x > 0)
            {
                return true;
            }
            return false;
        }

        public DataTable ViewReport(int dest = 0) {
            string[] keys = new string[(dest != 0 && dest != 5 ? 2 : 1)];
            string[] values = new string[(dest != 0 && dest != 5 ? 2 : 1)];

            keys[0] = "recipientEmployeeID";
            values[0] = this.id;

            switch (dest)
            {
                case 1:
                    keys[1] = "destID";
                    values[1] = "jkt";
                    break;
                case 2:
                    keys[1] = "destID";
                    values[1] = "bdg";
                    break;
                case 3:
                    keys[1] = "destID";
                    values[1] = "sby";
                    break;
                case 4:
                    keys[1] = "destID";
                    values[1] = "dps";
                    break;
                case 5:
                    keys[0] = "writerEmployeeID";
                    break;
            }

            string[] table = { "Reports" };
            string[] fields = { "reportID", "rName", "statusID", "writerEmployeeID" };
            Dictionary<string, object> parameter = mtd.ArrayMaker(keys, values);
            string[] oprtr = { "=", "=", "=" };

            DataTable result = DB.Select(fields, table, oprtr, parameter);
            return result;
        }

        public CrystalReport3 CreateReport1(int type, int destination, int header, ref DataTable ds, string id = null)
        {
            string[] fields;
            string[] table;
            string[] keys;
            string[] oprtrs;
            object[] values;

            switch (type)
            {
                case 1:
                    fields = new string[] { "*" };
                    table = new string[] { "Tour" };
                    keys = new string[] { "tourID" };
                    oprtrs = new string[] { "=" };
                    values = new object[] { id };
                    break;
                default:
                    throw new ArgumentException("Invalid type provided.");
            }

            CrystalReport3 rpt = new CrystalReport3();
            rpt.Load($"C:\\Users\\DELL\\Documents\\VSCODE\\Ezgo Final Form\\Ezgo Desktop App\\Reports\\CrystalReport3.rpt");

            Section section1 = rpt.ReportDefinition.Sections[0];
            Section section2 = rpt.ReportDefinition.Sections[1];
            TextObject textObject1 = section1.ReportObjects["Text1"] as TextObject;
            TextObject textObject2 = section2.ReportObjects["Text5"] as TextObject;

            switch (header)
            {
                case 1:
                    textObject1.Text = "Create a Tour Group";
                    textObject2.Text = $"I, {this.name} would like for this division to create a Tour Group for the Tour Package listed below.";
                    break;
                case 2:
                    textObject1.Text = "Promote Product";
                    textObject2.Text = $"I, {this.name} would like for this division to market or increase marketing for the Product listed below.";
                    break;
                default:
                    throw new ArgumentException("Invalid header provided.");
            }

            switch (destination)
            {
                case 1:
                    keys[0] = "destID";
                    values[0] = "jkt";
                    break;
                case 2:
                    keys[0] = "destID";
                    values[0] = "bdg";
                    break;
                case 3:
                    keys[0] = "destID";
                    values[0] = "sby";
                    break;
                case 4:
                    keys[0] = "destID";
                    values[0] = "dps";
                    break;
                default:
                    throw new ArgumentException("Invalid destination provided.");
            }

            ds = mtd.BuildReport(fields, table, oprtrs, keys, values, ref rpt);

            return rpt;
        }

        public CrystalReport5 CreateReport3(int type, int destination, int header, ref DataTable ds, string id = null)
        {
            string[] fields;
            string[] table;
            string[] keys;
            string[] oprtrs;
            object[] values;

            bool tr = false;

            if (id != null)
            {
                keys = new string[2];
                values = new string[2];
                oprtrs = new string[] { "=", "=" };
                tr = true;
                MessageBox.Show("yes");
            }
            else
            {
                keys = new string[1];
                values = new string[1];
                oprtrs = new string[] { "=" };
                tr = false;
            }

            switch (type)
            {
                case 3:
                    fields = new string[] { "*" };
                    table = new string[] { "Hotel" };
                    if (tr == true)
                    {
                        keys[1] = "hotelID";
                        values[1] = id;
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid type provided.");
            }

            CrystalReport5 rpt = new CrystalReport5();
            rpt.Load($"C:\\Users\\DELL\\Documents\\VSCODE\\Ezgo Final Form\\Ezgo Desktop App\\Reports\\CrystalReport5.rpt");

            Section section1 = rpt.ReportDefinition.Sections[0];
            Section section2 = rpt.ReportDefinition.Sections[1];
            TextObject textObject1 = section1.ReportObjects["Text1"] as TextObject;
            TextObject textObject2 = section2.ReportObjects["Text5"] as TextObject;

            switch (header)
            {
                case 2:
                    textObject1.Text = "Promote Product";
                    textObject2.Text = $"I, {this.name} would like for this division to market or increase marketing for the Product listed below.";
                    break;
                default:
                    throw new ArgumentException("Invalid header provided.");
            }

            switch (destination)
            {
                case 1:
                    keys[0] = "destID";
                    values[0] = "jkt";
                    break;
                case 2:
                    keys[0] = "destID";
                    values[0] = "bdg";
                    break;
                case 3:
                    keys[0] = "destID";
                    values[0] = "sby";
                    break;
                case 4:
                    keys[0] = "destID";
                    values[0] = "dps";
                    break;
                default:
                    throw new ArgumentException("Invalid destination provided.");
            }

            ds = mtd.BuildReport(fields, table, oprtrs, keys, values, ref rpt);

            return rpt;
        }

        public CrystalReport4 CreateReport2(int type, int destination, int header, ref DataTable ds, string id = null)
        {
            string[] fields;
            string[] table;
            string[] keys;
            string[] oprtrs;
            object[] values;

            switch (type)
            {
                case 2:
                    fields = new string[] { "*" };
                    table = new string[] { "Tickets" };
                    keys = new string[] { "ticketID" };
                    oprtrs = new string[] { "=" };
                    values = new object[] { id };
                    break;
                default:
                    throw new ArgumentException("Invalid type provided.");
            }

            CrystalReport4 rpt = new CrystalReport4();
            rpt.Load($"C:\\Users\\DELL\\Documents\\VSCODE\\Ezgo Final Form\\Ezgo Desktop App\\Reports\\CrystalReport4.rpt");

            Section section1 = rpt.ReportDefinition.Sections[0];
            Section section2 = rpt.ReportDefinition.Sections[1];
            TextObject textObject1 = section1.ReportObjects["Text1"] as TextObject;
            TextObject textObject2 = section2.ReportObjects["Text5"] as TextObject;

            switch (header)
            {
                case 2:
                    textObject1.Text = "Promote Product";
                    textObject2.Text = $"I, {this.name} would like for this division to market or increase marketing for the Product listed below.";
                    break;
                default:
                    throw new ArgumentException("Invalid header provided.");
            }

            switch (destination)
            {
                case 1:
                    keys[0] = "destID";
                    values[0] = "jkt";
                    break;
                case 2:
                    keys[0] = "destID";
                    values[0] = "bdg";
                    break;
                case 3:
                    keys[0] = "destID";
                    values[0] = "sby";
                    break;
                case 4:
                    keys[0] = "destID";
                    values[0] = "dps";
                    break;
                default:
                    throw new ArgumentException("Invalid destination provided.");
            }

            ds = mtd.BuildReport(fields, table, oprtrs, keys, values, ref rpt);

            return rpt;
        }

        public CrystalReport6 CreateReport4(int type, int destination, int header, ref DataTable ds, string id = null)
        {
            string[] fields;
            string[] table;
            string[] keys;
            string[] oprtrs;
            object[] values;

            bool tr = false;

            if (id != null)
            {
                keys = new string[2];
                values = new string[2];
                oprtrs = new string[] { "=", "=" };
                tr = true;
                MessageBox.Show("yes");
            }
            else
            {
                keys = new string[0];
                values = new string[0];
                oprtrs = new string[0];
                tr = false;
            }

            switch (type)
            {
                case 4:
                    fields = new string[] { "*" };
                    table = new string[] { "Vendor" };
                    if (tr == true)
                    {
                        keys[1] = "vendorID";
                        values[1] = id;
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid type provided.");
            }

            CrystalReport6 rpt = new CrystalReport6();
            rpt.Load($"C:\\Users\\DELL\\Documents\\VSCODE\\Ezgo Final Form\\Ezgo Desktop App\\Reports\\CrystalReport6.rpt");

            Section section1 = rpt.ReportDefinition.Sections[0];
            Section section2 = rpt.ReportDefinition.Sections[1];
            TextObject textObject1 = section1.ReportObjects["Text1"] as TextObject;
            TextObject textObject2 = section2.ReportObjects["Text5"] as TextObject;

            switch (header)
            {
                case 3:
                    textObject1.Text = "Promote Vendor";
                    textObject2.Text = $"I, {this.name} would like for this division to market or increase marketing for our Vendor listed below.";
                    break;
                default:
                    throw new ArgumentException("Invalid header provided.");
            }

            ds = mtd.BuildReport(fields, table, oprtrs, keys, values, ref rpt);

            return rpt;
        }

        public CrystalReport7 CreateReport5(int type, int destination, int header, ref DataTable ds, string id = null)
        {
            string[] fields;
            string[] table;
            string[] keys;
            string[] oprtrs;
            object[] values;

            bool tr = false;

            if (id != null)
            {
                keys = new string[2];
                values = new string[2];
                oprtrs = new string[] { "=", "=" };
                tr = true;
                MessageBox.Show("yes");
            }
            else
            {
                keys = new string[0];
                values = new string[0];
                oprtrs = new string[0];
                tr = false;
            }

            switch (type)
            {
                case 5:
                    fields = new string[] { "*" };
                    table = new string[] { "ProductKey" };
                    if (tr == true)
                    {
                        keys = new string[] { "keyID" };
                        values = new string[] { id };
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid type provided.");
            }

            CrystalReport7 rpt = new CrystalReport7();
            rpt.Load($"C:\\Users\\DELL\\Documents\\VSCODE\\Ezgo Final Form\\Ezgo Desktop App\\Reports\\CrystalReport7.rpt");

            Section section1 = rpt.ReportDefinition.Sections[0];
            Section section2 = rpt.ReportDefinition.Sections[1];
            TextObject textObject1 = section1.ReportObjects["Text1"] as TextObject;
            TextObject textObject2 = section2.ReportObjects["Text5"] as TextObject;

            switch (header)
            {
                case 4:
                    textObject1.Text = "Find New Product";
                    textObject2.Text = $"I, {this.name} would like for this division to search for more products of the type below from our existing vendors.";
                    break;
                default:
                    throw new ArgumentException("Invalid header provided.");
            }

            ds = mtd.BuildReport(fields, table, oprtrs, keys, values, ref rpt);

            return rpt;
        }

        public CrystalReport1 CreateReport6(int type, int destination, int header, ref DataTable ds, string id = null)
        {
            string[] fields;
            string[] table;
            string[] keys;
            string[] oprtrs;
            object[] values;

            string addon = "";

            switch (type)
            {
                case 6:
                    fields = new string[] { "P.productID", "TC.tcName", "H.hName", "T.tpName", "TC.tcAmount", "H.hAmount", "T.tpSlot", "V.vName" };
                    table = new string[] { "Products P", "Tickets TC", "Hotel H", "Tour T", "Vendor V" };
                    oprtrs = new string[] { "=" };
                    keys = new string[0];
                    values = new string[0];
                    if (destination != 0) {
                        keys = new string[1];
                        values = new string[1];
                    }
                    addon = "P.";
                    break;
                default:
                    throw new ArgumentException("Invalid type provided.");
            }

            CrystalReport1 rpt = new CrystalReport1();
            rpt.Load($"C:\\Users\\DELL\\Documents\\VSCODE\\Ezgo Final Form\\Ezgo Desktop App\\Reports\\CrystalReport1.rpt");

            Section section1 = rpt.ReportDefinition.Sections[0];
            Section section2 = rpt.ReportDefinition.Sections[1];
            TextObject textObject1 = section1.ReportObjects["Text1"] as TextObject;
            TextObject textObject2 = section2.ReportObjects["Text5"] as TextObject;

            switch (header)
            {
                case 5:
                    textObject1.Text = "Current Stocks";
                    textObject2.Text = $"A list of the currently available products and it's available amount or slots.";
                    break;
                default:
                    throw new ArgumentException("Invalid header provided.");
            }

            switch (destination)
            {
                case 1:
                    keys[0] = $"{addon}destID";
                    values[0] = "jkt";
                    break;
                case 2:
                    keys[0] = $"{addon}destID";
                    values[0] = "bdg";
                    break;
                case 3:
                    keys[0] = $"{addon}destID";
                    values[0] = "sby";
                    break;
                case 4:
                    keys[0] = $"{addon}destID";
                    values[0] = "dps";
                    break;
            }

            string[] join = { "P.productID = TC.ticketID", "P.productID = H.hotelID", "P.productID = T.tourID", "P.vendorID = V.vendorID" };
            ds = mtd.BuildReport(fields, table, oprtrs, keys, values, ref rpt, join);

            return rpt;
        }

        public CrystalReport2 CreateReport7(int type, int destination, int header, ref DataTable ds, string id = null)
        {
            string[] fields;
            string[] table;
            string[] keys;
            string[] oprtrs;
            object[] values;

            string addon = "";

            switch (type)
            {
                case 7:
                    fields = new string[] { "O.orderID", "TC.tcName", "H.hName", "T.tpName", "OD.total", "OD.totalPrice", "V.vName" };
                    table = new string[] { "Orders O", "OrderDetails OD", "Products P", "Tickets TC", "Hotel H", "Tour T", "Vendor V" };
                    oprtrs = new string[] { "=" };
                    keys = new string[0];
                    values = new string[0];
                    if (destination != 0)
                    {
                        keys = new string[1];
                        values = new string[1];
                    }
                    addon = "P.";
                    break;
                default:
                    throw new ArgumentException("Invalid type provided.");
            }

            CrystalReport2 rpt = new CrystalReport2();
            rpt.Load($"C:\\Users\\DELL\\Documents\\VSCODE\\Ezgo Final Form\\Ezgo Desktop App\\Reports\\CrystalReport2.rpt");

            Section section1 = rpt.ReportDefinition.Sections[0];
            Section section2 = rpt.ReportDefinition.Sections[1];
            TextObject textObject1 = section1.ReportObjects["Text1"] as TextObject;
            TextObject textObject2 = section2.ReportObjects["Text5"] as TextObject;

            switch (header)
            {
                case 6:
                    textObject1.Text = "Current Sales";
                    textObject2.Text = $"Sales that has been generated and it's amounts and prices per product.";
                    break;
                default:
                    throw new ArgumentException("Invalid header provided.");
            }

            switch (destination)
            {
                case 1:
                    keys[0] = $"{addon}destID";
                    values[0] = "jkt";
                    break;
                case 2:
                    keys[0] = $"{addon}destID";
                    values[0] = "bdg";
                    break;
                case 3:
                    keys[0] = $"{addon}destID";
                    values[0] = "sby";
                    break;
                case 4:
                    keys[0] = $"{addon}destID";
                    values[0] = "dps";
                    break;
            }

            string[] join = { "O.orderID = OD.orderID", "OD.productID = P.productID", "P.productID = TC.ticketID", "P.productID = H.hotelID", "P.productID = T.tourID", "P.vendorID = V.vendorID" };
            ds = mtd.BuildReport(fields, table, oprtrs, keys, values, ref rpt, join);

            return rpt;
        }

        public DataTable ViewCustomer(int dest = 0)
        {
            string[] fields = new string[0];
            string[] table = new string[0];
            string[] keys = new string[0];
            string[] values = new string[0];
            string[] oprtr = new string[0];
            DataTable result = new DataTable();

            if (dest != 0)
            {
                switch (dest)
                {
                    case 1:   
                        values = new string[] { "jkt" };
                        break;
                    case 2:
                        values = new string[] { "bdg" };
                        break;
                    case 3:
                        values = new string[] { "dps" };
                        break;
                    case 4:
                        values = new string[] { "sby" };
                        break;
                }

                fields = new string[] { "Customer.*" };
                table = new string[] { "Orders", "Customer" };
                keys = new string[] { "Orders.destID" };
                oprtr = new string[] { "=" };
                string[] join = { "Orders.custID = Customer.custID" };
                Dictionary<string, object> parameter = mtd.ArrayMaker(keys, values);

                result = DB.Select(fields, table, oprtr, parameter, join);
            }
            else {
                fields = new string[] { "*" };
                table = new string[] { "Customer" };
                Dictionary<string, object> parameter = new Dictionary<string, object>();

                result = DB.Select(fields, table, oprtr, parameter);
            }

            return result;
        }

        public DataTable ViewVendor() {
            string[] fields = { "*" };
            string[] table = { "Vendor" };
            string[] oprtr = new string[0];
            Dictionary<string, object> parameter = new Dictionary<string, object>();

            DataTable result = DB.Select(fields, table, oprtr, parameter);
            return result;
        }

        public bool InsertVendor(string[] keys, object[] values)
        {
            string table = "Vendor";
            Dictionary<string, object> parameter = mtd.ArrayMaker(keys, values);
            int x = DB.Insert(table, parameter);

            if (x > 0)
            {
                return true;
            }
            return false;
        }

        public bool UpdateVendor(string[] keys, object[] values, string[] field, object[] where) {
            string table = "Vendor";
            Dictionary<string, object> updates = mtd.ArrayMaker(keys, values);
            Dictionary<string, object> parameter = mtd.ArrayMaker(field, where);
            int x = DB.Update(table, parameter, updates);

            if (x > 0)
            {
                return true;
            }
            return false;
        }

        public bool DeleteVendor(string[] keys, object[] values)
        {
            string table = "Vendor";
            Dictionary<string, object> parameter = mtd.ArrayMaker(keys, values);
            int x = DB.Delete(table, parameter);

            if (x > 0)
            {
                return true;
            }
            return false;
        }
    }

    public class sales : employee
    {
        DB db = new DB();
        Methods mtd = new Methods();

        public sales(employee e)
        {
            this.id = e.id;
            this.name = e.name;
            this.birthday = e.birthday;
            this.phone = e.phone;
            this.email = e.email;
            this.branch = e.branch;
            this.salary = e.salary;
            this.key = e.key;
        }

        public DataTable ViewStock(int code, int key = 0)
        {
            DataTable result = new DataTable();
            string[] table = new string[1];
            string type = "";

            switch (code)
            {
                case 1:
                    table[0] = "Tickets";

                    if (key != 0)
                    {
                        switch (key)
                        {
                            case 1:
                                type = "krt";
                                break;
                            case 2:
                                type = "fer";
                                break;
                            case 3:
                                type = "pln";
                                break;
                        }
                    }

                    break;
                case 2:
                    table[0] = "Hotel";
                    break;
                case 3:
                    table[0] = "Tour";
                    break;
            }

            string[] fields = { "*" };
            string[] keys = { "tcType", "destID" };
            string[] values = { type, this.branch };
            Dictionary<string, object> parameter = (key != 0 ? mtd.ArrayMaker(keys, values) : new Dictionary<string, object>());
            string[] oprtr = { "=", "=" };
            result = DB.Select(fields, table, oprtr, parameter);

            return result;
        }

        public bool InsertProduct(int code, string vendor, string id) {
            string table = "Products";
            string[] key = new string[0];
            object[] values = new string[0];

            switch (code)
            {
                case 1:
                    key = new string[] { "productID", "keyID", "vendorID", "destID" };
                    values = new string[] { id, "tkt", vendor, this.branch };
                    break;
                case 2:
                    key = new string[] { "productID", "keyID", "vendorID", "destID" };
                    values = new string[] { id, "htl", vendor, this.branch };
                    break;
                case 3:
                    key = new string[] { "productID", "keyID", "vendorID", "destID" };
                    values = new string[] { id, "trp", vendor, this.branch };
                    break;
            }

            Dictionary<string, object> parameter = mtd.ArrayMaker(key, values);
            int x = DB.Insert(table, parameter);

            if (x > 0)
            {
                return true;
            }
            return false;
        }

        public bool InsertStock(int code, string[] keys, object[] values)
        {
            string table = "";
            string vendor = "";
            string id = "";

            switch (code)
            {
                case 1:
                    table = "Tickets";
                    id = values[0].ToString();
                    vendor = values[13].ToString();
                    break;
                case 2:
                    table = "Hotel";
                    break;
                case 3:
                    table = "Tour";
                    break;
            }

            if (InsertProduct(code, vendor, id)) {
                Dictionary<string, object> parameter = mtd.ArrayMaker(keys, values);
                int x = DB.Insert(table, parameter);

                if (x > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool UpdateStock(int code, string[] keys, object[] values, string[] field, object[] where)
        {
            string table = "";

            switch (code)
            {
                case 1:
                    table = "Tickets";
                    break;
                case 2:
                    table = "Hotel";
                    break;
                case 3:
                    table = "Tour";
                    break;
            }

            Dictionary<string, object> updates = mtd.ArrayMaker(keys, values);
            Dictionary<string, object> parameter = mtd.ArrayMaker(field, where);
            int x = DB.Update(table, parameter, updates);

            if (x > 0)
            {
                return true;
            }
            return false;
        }

        public bool DeleteStock(int code, string[] keys, object[] values)
        {
            string table = "";

            switch (code)
            {
                case 1:
                    table = "Tickets";
                    break;
                case 2:
                    table = "Hotel";
                    break;
                case 3:
                    table = "Tour";
                    break;
            }

            Dictionary<string, object> parameter = mtd.ArrayMaker(keys, values);
            int x = DB.Delete(table, parameter);

            if (x > 0)
            {
                return true;
            }
            return false;
        }

        public DataTable ViewStaff(int roleCode, int branch = 0)
        {
            string role = "";

            switch (roleCode)
            {
                case 1:
                    role = "mng";
                    break;
                case 2:
                    role = "sls";
                    break;
                case 3:
                    role = "trg";
                    break;
            }

            string[] keys = new string[(branch != 0 ? 2 : 1)];
            string[] values = new string[(branch != 0 ? 2 : 1)];

            keys[0] = "roleID";
            values[0] = role;

            switch (branch)
            {
                case 1:
                    keys[1] = "destID";
                    values[1] = "jkt";
                    break;
                case 2:
                    keys[1] = "destID";
                    values[1] = "bdg";
                    break;
                case 3:
                    keys[1] = "destID";
                    values[1] = "sby";
                    break;
                case 4:
                    keys[1] = "destID";
                    values[1] = "dps";
                    break;
            }

            string[] table = { "Employee" };
            string[] fields = { "*" };
            Dictionary<string, object> parameter = mtd.ArrayMaker(keys, values);
            string[] oprtr = { "=", "=" };

            DataTable result = DB.Select(fields, table, oprtr, parameter);
            return result;
        }

        public bool InsertStaff(string[] keys, object[] values)
        {
            string table = "Employee";
            Dictionary<string, object> parameter = mtd.ArrayMaker(keys, values);
            int x = DB.Insert(table, parameter);

            if (x > 0)
            {
                return true;
            }
            return false;
        }

        public bool UpdateStaff(string[] keys, object[] values, string[] field, object[] where)
        {
            string table = "Employee";
            Dictionary<string, object> updates = mtd.ArrayMaker(keys, values);
            Dictionary<string, object> parameter = mtd.ArrayMaker(field, where);
            int x = DB.Update(table, updates, parameter);

            if (x > 0)
            {
                return true;
            }
            return false;
        }

        public bool DeleteStaff(int code, string[] keys, object[] values)
        {
            string table = "Employee";
            Dictionary<string, object> parameter = mtd.ArrayMaker(keys, values);
            int x = DB.Delete(table, parameter);

            if (x > 0)
            {
                return true;
            }
            return false;
        }

        public DataTable ViewReport(int dest = 0)
        {

            string[] keys = new string[(dest != 0 && dest != 5 ? 2 : 1)];
            string[] values = new string[(dest != 0 && dest != 5 ? 2 : 1)];

            keys[0] = "recipientEmployeeID";
            values[0] = this.id;

            switch (dest)
            {
                case 1:
                    keys[1] = "destID";
                    values[1] = "jkt";
                    break;
                case 2:
                    keys[1] = "destID";
                    values[1] = "bdg";
                    break;
                case 3:
                    keys[1] = "destID";
                    values[1] = "sby";
                    break;
                case 4:
                    keys[1] = "destID";
                    values[1] = "dps";
                    break;
                case 5:
                    keys[0] = "writerEmployeeID";
                    break;
            }

            string[] table = { "Reports" };
            string[] fields = { "reportID", "rName", "statusID", "writerEmployeeID" };
            Dictionary<string, object> parameter = mtd.ArrayMaker(keys, values);
            string[] oprtr = { "=", "=" };

            DataTable result = DB.Select(fields, table, oprtr, parameter);
            return result;
        }


        public CrystalReport1 CreateReport6(int type, int destination, int header, ref DataTable ds, string id = null)
        {
            string[] fields;
            string[] table;
            string[] keys;
            string[] oprtrs;
            object[] values;

            string addon = "";

            switch (type)
            {
                case 6:
                    fields = new string[] { "P.productID", "TC.tcAmount", "H.hAmount", "T.tpSlot", "V.vName" };
                    table = new string[] { "Products P", "Tickets TC", "Hotel H", "Tour T", "Vendor V" };
                    oprtrs = new string[] { "=" };
                    keys = new string[1];
                    values = new string[1];
                    addon = "P.";
                    break;
                default:
                    throw new ArgumentException("Invalid type provided.");
            }

            CrystalReport1 rpt = new CrystalReport1();
            rpt.Load($"C:\\Users\\DELL\\Documents\\VSCODE\\Ezgo Final Form\\Ezgo Desktop App\\Reports\\CrystalReport1.rpt");

            Section section1 = rpt.ReportDefinition.Sections[0];
            Section section2 = rpt.ReportDefinition.Sections[1];
            TextObject textObject1 = section1.ReportObjects["Text6"] as TextObject;
            TextObject textObject2 = section2.ReportObjects["Text7"] as TextObject;

            switch (header)
            {
                case 3:
                    textObject1.Text = "Current Stocks";
                    textObject2.Text = $"A list of the currently available products and it's available amount or slots in {branch} branch.";
                    break;
                default:
                    throw new ArgumentException("Invalid header provided.");
            }

            switch (destination)
            {
                case 1:
                    keys[0] = $"{addon}destID";
                    values[0] = "jkt";
                    break;
                case 2:
                    keys[0] = $"{addon}destID";
                    values[0] = "bdg";
                    break;
                case 3:
                    keys[0] = $"{addon}destID";
                    values[0] = "sby";
                    break;
                case 4:
                    keys[0] = $"{addon}destID";
                    values[0] = "dps";
                    break;
                default:
                    throw new ArgumentException("Invalid destination provided.");
            }

            string[] join = { "P.productID = TC.productID", "P.productID = H.productID", "P.productID = T.productID", "P.vendorID   = V.vendorID" };
            ds = mtd.BuildReport(fields, table, oprtrs, keys, values, ref rpt, join);

            return rpt;
        }

        public CrystalReport2 CreateReport7(int type, int destination, int header, ref DataTable ds, string id = null)
        {
            string[] fields;
            string[] table;
            string[] keys;
            string[] oprtrs;
            object[] values;

            string addon = "";

            switch (type)
            {
                case 7:
                    fields = new string[] { "O.orderID", "TC.tcName", "H.hName", "T.tpName", "OD.total", "OD.totalPrice", "V.vName" };
                    table = new string[] { "Orders O", "OrderDetails OD", "Products P", "Tickets TC", "Hotel H", "Tour T", "Vendor V" };
                    oprtrs = new string[] { "=" };
                    keys = new string[1];
                    values = new string[1];
                    addon = "P.";
                    break;
                default:
                    throw new ArgumentException("Invalid type provided.");
            }

            CrystalReport2 rpt = new CrystalReport2();
            rpt.Load($"C:\\Users\\DELL\\Documents\\VSCODE\\Ezgo Final Form\\Ezgo Desktop App\\Reports\\CrystalReport2.rpt");

            Section section1 = rpt.ReportDefinition.Sections[0];
            Section section2 = rpt.ReportDefinition.Sections[1];
            TextObject textObject1 = section1.ReportObjects["Text1"] as TextObject;
            TextObject textObject2 = section2.ReportObjects["Text5"] as TextObject;

            switch (header)
            {
                case 6:
                    textObject1.Text = "Current Sales";
                    textObject2.Text = $"Sales that has been generated and it's amounts and prices per product.";
                    break;
                default:
                    throw new ArgumentException("Invalid header provided.");
            }

            switch (destination)
            {
                case 1:
                    keys[0] = $"{addon}destID";
                    values[0] = "jkt";
                    break;
                case 2:
                    keys[0] = $"{addon}destID";
                    values[0] = "bdg";
                    break;
                case 3:
                    keys[0] = $"{addon}destID";
                    values[0] = "sby";
                    break;
                case 4:
                    keys[0] = $"{addon}destID";
                    values[0] = "dps";
                    break;
                default:
                    throw new ArgumentException("Invalid destination provided.");
            }

            string[] join = { "O.orderID = OD.orderID", "OD.productID = P.productID", "P.productID = TC.ticketID", "P.productID = H.hotelID", "P.productID = T.tourID", "P.vendorID = V.vendorID" };
            ds = mtd.BuildReport(fields, table, oprtrs, keys, values, ref rpt, join);

            return rpt;
        }

        public CrystalReport3 CreateReport1(int type, int destination, int header, ref DataTable ds, string id = null)
        {
            string[] fields;
            string[] table;
            string[] keys;
            string[] oprtrs;
            object[] values;

            switch (type)
            {
                case 1:
                    fields = new string[] { "*" };
                    table = new string[] { "Tour" };
                    keys = new string[] { "tourID" };
                    oprtrs = new string[] { "=" };
                    values = new object[] { id };
                    break;
                default:
                    throw new ArgumentException("Invalid type provided.");
            }

            CrystalReport3 rpt = new CrystalReport3();
            rpt.Load($"C:\\Users\\DELL\\Documents\\VSCODE\\Ezgo Final Form\\Ezgo Desktop App\\Reports\\CrystalReport3.rpt");

            Section section1 = rpt.ReportDefinition.Sections[0];
            Section section2 = rpt.ReportDefinition.Sections[1];
            TextObject textObject1 = section1.ReportObjects["Text1"] as TextObject;
            TextObject textObject2 = section2.ReportObjects["Text5"] as TextObject;

            switch (header)
            {
                case 1:
                    textObject1.Text = "Guide This Tour Package";
                    textObject2.Text = $"I, {this.name} allow for you to be a Tour Guide for the Tour Package listed below.";
                    break;
                case 2:
                    textObject1.Text = "Guide This Tour Package";
                    textObject2.Text = $"I, {this.name} would like for you to be a Tour Guide for the Tour Package listed below.";
                    break;
                default:
                    throw new ArgumentException("Invalid header provided.");
            }

            switch (destination)
            {
                case 1:
                    keys[0] = "destID";
                    values[0] = "jkt";
                    break;
                case 2:
                    keys[0] = "destID";
                    values[0] = "bdg";
                    break;
                case 3:
                    keys[0] = "destID";
                    values[0] = "sby";
                    break;
                case 4:
                    keys[0] = "destID";
                    values[0] = "dps";
                    break;
                default:
                    throw new ArgumentException("Invalid destination provided.");
            }

            ds = mtd.BuildReport(fields, table, oprtrs, keys, values, ref rpt);

            return rpt;
        }

        public CrystalReport8 CreateReport8(int type, int destination, int header, ref DataTable ds, string[] arr = null)
        {
            CrystalReport8 rpt = new CrystalReport8();
            rpt.Load($"C:\\Users\\DELL\\Documents\\VSCODE\\Ezgo Final Form\\Ezgo Desktop App\\Reports\\CrystalReport8.rpt");

            Section section1 = rpt.ReportDefinition.Sections[0];
            Section section2 = rpt.ReportDefinition.Sections[1];
            TextObject textObject1 = section1.ReportObjects["Text1"] as TextObject;
            TextObject textObject2 = section2.ReportObjects["Text5"] as TextObject;

            switch (header)
            {
                 case 5:
                    textObject1.Text = "Proposal for a New Product";
                    textObject2.Text = $"The {branch} branch is proposing of the addition of a new {arr[0]} called {arr[1]}.";
                    break;
                case 6:
                    textObject1.Text = "Proposal for a New Tour Package";
                    textObject2.Text = $"The {branch} branch is proposing of the addition of a new Tour Package for the destination city of {arr[0]} which will have a total of {arr[1]} destinations.";
                    break;
                case 7:
                    textObject1.Text = "Proposal for a New Marketing Plan";
                    textObject2.Text = $"The {branch} branch is proposing of the permission to begin a new marketing for the product {arr[0]} for the {arr[1]} branch.";
                    break;
                default:
                    throw new ArgumentException("Invalid header provided.");
            }

            return rpt;
        }

        public DataTable ViewGuides(int branch, int kode = 0)
        {
            string[] keys = new string[(kode != 3 ? 2 : 1)];
            string[] values = new string[(kode != 3 ? 2 : 1)];

            switch (branch)
            {
                case 1:
                    keys[0] = "destID";
                    values[0] = "jkt";
                    break;
                case 2:
                    keys[0] = "destID";
                    values[0] = "bdg";
                    break;
                case 3:
                    keys[0] = "destID";
                    values[0] = "sby";
                    break;
                case 4:
                    keys[0] = "destID";
                    values[0] = "dps";
                    break;
            }

            string[] table = { "Tour" };
            string[] fields = { "*" };
            string[] oprtr = new string[0];

            switch (kode)
            {
                case 1:
                    keys[1] = "COALESCE(employeeID, '')";
                    values[1] = "";
                    oprtr = new string[] { "=", "=" };
                    break;
                case 2:
                    keys[1] = "COALESCE(employeeID, '')";
                    values[1] = "";
                    oprtr = new string[] { "=", "!=" };
                    break;
                case 3:
                    oprtr = new string[] { "=" };
                    break;
            }

            Dictionary<string, object> parameter = mtd.ArrayMaker(keys, values);

            DataTable result = DB.Select(fields, table, oprtr, parameter);
            return result;
        }

        public DataTable ViewCustomer()
        {
            string[] fields = { "*" };
            string[] table = { "Customer" };
            string[] oprtr = new string[0];
            Dictionary<string, object> parameter = new Dictionary<string, object>();

            DataTable result = DB.Select(fields, table, oprtr, parameter);
            return result;
        }

        public bool AssignGuide(string id, string tour, string pid)
        {
            string table = "Tour";
            string[] keys = { "employeeID" };
            string[] values = { id };
            string[] field = { "tourID" };
            string[] where = { tour };
            Dictionary<string, object> updates = mtd.ArrayMaker(keys, values);
            Dictionary<string, object> parameter = mtd.ArrayMaker(field, where);
            int x = DB.Update(table, parameter, updates);

            string table2 = "TourGroup";
            string[] keys2 = { "tgID", "tourID", "employeeID" };
            string[] values2 = { tour+id, tour, id };
            Dictionary<string, object> parameter2 = mtd.ArrayMaker(keys2, values2);
            int y = DB.Insert(table2, parameter2);

            string[] table3 = { "Orders O", "OrderDetails OD" };
            string[] fields3 = { "O.custID" };
            string[] oprtr = { "=" };
            string[] keys3 = { "OD.productID" };
            string[] values3 = { pid };
            string[] join = { "O.orderID = OD.orderID" };
            Dictionary<string, object> parameter3 = mtd.ArrayMaker(keys3, values3);
            DataTable result = DB.Select(fields3, table3, oprtr, parameter3, join);

            string[] arr = mtd.ArrayFromTable(result, "custID");
            int i = 0;

            foreach (string cust in arr) {
                string table4 = "TourGroupMember";
                string[] keys4 = { "tgmID", "tgID", "custID" };
                string[] values4 = { tour + id + i, tour + id, cust };
                Dictionary<string, object> parameter4 = mtd.ArrayMaker(keys4, values4);
                int z = DB.Insert(table4, parameter4);
                if (z > 0)
                {
                    i++;
                }
            }

            if (x > 0 && y > 0)
            {
                return true;
            }
            return false;
        }

        public bool InsertCustomer(string[] keys, object[] values)
        {
            string table = "Customer";
            Dictionary<string, object> parameter = mtd.ArrayMaker(keys, values);
            int x = DB.Insert(table, parameter);

            if (x > 0)
            {
                return true;
            }
            return false;
        }

        public Dictionary<string, object> SelectItem(int kode, string id) {
            string[] field = { "*" };
            string[] table = new string[1];
            string[] key = {"productID"};

            switch (kode) {
                case 1:
                    table[0] = "Tickets";
                    break;
                case 2:
                    table[0] = "Hotel";
                    break;
                case 3:
                    table[0] = "Tour";
                    break;
            }

            string[] oprtr = { "=" };
            string[] value = { id };
            Dictionary<string, object> where = mtd.ArrayMaker(key, value);

            DataTable dt = DB.Select(field, table, oprtr, where);
            DataRow firstRow = dt.Rows[0];
            object[] rowArray = firstRow.ItemArray;
            string[] columns = new string[dt.Columns.Count];
            int i = 0;

            foreach (DataColumn column in dt.Columns)
            {
                columns[i] = column.ColumnName;
                i++;
            }

            Dictionary<string, object> ret = mtd.ArrayMaker(columns, rowArray);
            return ret;
        }

        public int GetLastOrderID()
        {
            string[] fields = { "orderID" };
            string[] table = { "Orders" };
            string[] oprtr = new string[0];
            Dictionary<string, object> parameter = new Dictionary<string, object>();

            DataTable result = DB.Select(fields, table, oprtr, parameter);

            if (result.Rows.Count > 0)
            {
                int lastOrderID = Convert.ToInt32(result.Rows[result.Rows.Count - 1]["orderID"]);
                return lastOrderID;
            }

            return -1; 
        }


        public bool InsertOrders(string[] keys, object[] values, string table)
        {
            Dictionary<string, object> parameter = mtd.ArrayMaker(keys, values);
            int x = DB.Insert(table, parameter);

            if (x > 0)
            {
                return true;
            }
            return false;
        }
    }

    public class tour : employee
    {
        DB db = new DB();
        Methods mtd = new Methods();

        public tour(employee e)
        {
            this.id = e.id;
            this.name = e.name;
            this.birthday = e.birthday;
            this.phone = e.phone;
            this.email = e.email;
            this.branch = e.branch;
            this.salary = e.salary;
            this.key = e.key;
        }

        public DataTable viewSchedule() {
            string[] table = { "Tour" };
            string[] fields = { "*" };
            string[] oprtr = { "=" };
            string[] keys = { "employeeID" };
            string[] values = { id };
            Dictionary<string, object> parameter = mtd.ArrayMaker(keys, values);

            DataTable result = DB.Select(fields, table, oprtr, parameter);

            return result;
        }

        public DataTable applySchedule()
        {
            string[] table = { "Tour" };
            string[] fields = { "*" };
            string[] oprtr = { "=", "=" };
            string[] keys = { "COALESCE(employeeID, '')", "destID" };
            string[] values = { "", branch };
            Dictionary<string, object> parameter = mtd.ArrayMaker(keys, values);

            DataTable result = DB.Select(fields, table, oprtr, parameter);

            return result;
        }

        public CrystalReport3 CreateReport1(int type, int destination, int header, ref DataTable ds, string[] add = null)
        {
            string[] fields;
            string[] table;
            string[] keys = new string[2];
            string[] oprtrs;
            object[] values = new string[2];

            string addon = "";

            switch (type)
            {
                case 1:
                    fields = new string[] { "*" };
                    table = new string[] { "Tour" };
                    oprtrs = new string[] { "=", "=" };
                    keys[0] = "tourID";
                    values[0] = add[0];
                    break;
                case 2:
                    fields = new string[] { "*" };
                    table = new string[] { "Tour" };
                    oprtrs = new string[] { "=", "=" };
                    keys[0] = "employeeID";
                    values[0] = this.id;
                    break;
                default:
                    throw new ArgumentException("Invalid type provided.");
            }

            CrystalReport3 rpt = new CrystalReport3();
            rpt.Load($"C:\\Users\\DELL\\Documents\\VSCODE\\Ezgo\\Ezgo Desktop App\\Reports\\CrystalReport3.rpt");

            Section section1 = rpt.ReportDefinition.Sections[0];
            Section section2 = rpt.ReportDefinition.Sections[1];
            TextObject textObject1 = section1.ReportObjects["Text1"] as TextObject;
            TextObject textObject2 = section2.ReportObjects["Text5"] as TextObject;

            switch (header)
            {
                case 1:
                    textObject1.Text = "Request For Assignment";
                    textObject2.Text = $"I, {this.name} would like to be the Tour Guide for the Tour Package listed below.";
                    break;
                case 2:
                    textObject1.Text = "After Work Report";
                    textObject2.Text = $"I, {this.name} have completed my assignment as the Tour Guide of the Tour Package listed below at {DateTime.Today:yyyy-MM-dd}.";
                    break;
                default:
                    throw new ArgumentException("Invalid header provided.");
            }

            switch (destination)
            {
                case 1:
                    keys[1] = $"{addon}destID";
                    values[1] = "jkt";
                    break;
                case 2:
                    keys[1] = $"{addon}destID";
                    values[1] = "bdg";
                    break;
                case 3:
                    keys[1] = $"{addon}destID";
                    values[1] = "sby";
                    break;
                case 4:
                    keys[1] = $"{addon}destID";
                    values[1] = "dps";
                    break;
            }
            
            ds = mtd.BuildReport(fields, table, oprtrs, keys, values, ref rpt);

            return rpt;
        }

        public CrystalReport11 CreateReport11(int type, int destination, int header, ref DataTable ds, string[] add = null)
        {
            string[] fields;
            string[] table;
            string[] keys = new string[2];
            string[] oprtrs;
            object[] values = new string[2];

            string addon = "";

            switch (type)
            {
                case 3:
                    fields = new string[] { "TGM.tgmID", "TG.tgID", "C.custID", "C.cName", "T.tourID" };
                    table = new string[] { "Tour T", "TourGroup TG", "TourGroupMember TGM", "Customer C" };
                    oprtrs = new string[] { "=", "=" };
                    keys[0] = "T.employeeID";
                    values[0] = this.id;
                    addon = "T.";
                    break;
                default:
                    throw new ArgumentException("Invalid type provided.");
            }

            CrystalReport11 rpt = new CrystalReport11();
            rpt.Load($"C:\\Users\\DELL\\Documents\\VSCODE\\Ezgo Final Form\\Ezgo Desktop App\\Reports\\CrystalReport11.rpt");

            Section section1 = rpt.ReportDefinition.Sections[0];
            Section section2 = rpt.ReportDefinition.Sections[1];
            TextObject textObject1 = section1.ReportObjects["Text1"] as TextObject;
            TextObject textObject2 = section2.ReportObjects["Text5"] as TextObject;

            switch (header)
            {
                case 3:
                    textObject1.Text = "Tour Group Member List";
                    textObject2.Text = $"Customers that are within today's Tour Group:";
                    break;
                default:
                    throw new ArgumentException("Invalid header provided.");
            }

            switch (destination)
            {
                case 1:
                    keys[1] = $"{addon}destID";
                    values[1] = "jkt";
                    break;
                case 2:
                    keys[1] = $"{addon}destID";
                    values[1] = "bdg";
                    break;
                case 3:
                    keys[1] = $"{addon}destID";
                    values[1] = "sby";
                    break;
                case 4:
                    keys[1] = $"{addon}destID";
                    values[1] = "dps";
                    break;
            }

            string[] join = { "T.tourID = TG.tourID", "TG.tgID = TGM.tgID", "TGM.custID = C.custID" };
            ds = mtd.BuildReport(fields, table, oprtrs, keys, values, ref rpt, join);

            return rpt;
        }

        public DataTable ViewReport(int dest = 0)
        {

            string[] keys = new string[(dest != 0 && dest != 5 ? 2 : 1)];
            string[] values = new string[(dest != 0 && dest != 5 ? 2 : 1)];

            keys[0] = "recipientEmployeeID";
            values[0] = this.id;

            switch (dest)
            {
                case 1:
                    keys[1] = "destID";
                    values[1] = "jkt";
                    break;
                case 2:
                    keys[1] = "destID";
                    values[1] = "bdg";
                    break;
                case 3:
                    keys[1] = "destID";
                    values[1] = "sby";
                    break;
                case 4:
                    keys[1] = "destID";
                    values[1] = "dps";
                    break;
                case 5:
                    keys[0] = "writerEmployeeID";
                    break;
            }

            string[] table = { "Reports" };
            string[] fields = { "reportID", "rName", "statusID", "writerEmployeeID" };
            Dictionary<string, object> parameter = mtd.ArrayMaker(keys, values);
            string[] oprtr = { "=", "=", "=" };

            DataTable result = DB.Select(fields, table, oprtr, parameter);
            return result;
        }

        public DataTable getTourGroup()
        {
            string[] table = { "Tour T", "TourGroup TG" };
            string[] fields = { "T.tourID", "TG.tgID", "T.tpName" };
            string[] oprtr = { "=" };
            string[] keys = { "T.employeeID" };
            string[] values = { this.id };
            string[] join = { "T.tourID = TG.tourID" };
            Dictionary<string, object> parameter = mtd.ArrayMaker(keys, values);

            DataTable result = DB.Select(fields, table, oprtr, parameter,join);

            return result;
        }
    }
}

