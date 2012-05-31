using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace SqlConnectionStringBuilderKeysConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // a few examples of ConnectionStrings from http://connectionstrings.com/sql-server-2008#p1 and their shortened forms.

            showConnectionStringAndShortestConnectionString("Data Source=myServerAddress;Initial Catalog=myDataBase;User Id=myUsername;Password=myPassword;");
            showConnectionStringAndShortestConnectionString("Data Source=myServerAddress;Initial Catalog=myDataBase;Integrated Security=SSPI;");
            showConnectionStringAndShortestConnectionString(@"Server=myServerName\theInstanceName;Database=myDataBase;Trusted_Connection=True;");
            showConnectionStringAndShortestConnectionString(@"Data Source=myServerAddress;Initial Catalog=myDataBase;Integrated Security=SSPI;User ID=myDomain\myUsername;Password=myPassword;");
            showConnectionStringAndShortestConnectionString("Data Source=190.190.200.100,1433;Network Library=DBMSSOCN;Initial Catalog=myDataBase;User ID=myUsername;Password=myPassword;");
            showConnectionStringAndShortestConnectionString("Server=myServerAddress;Database=myDataBase;Trusted_Connection=True; MultipleActiveResultSets=true;");
            showConnectionStringAndShortestConnectionString(@"Server=.\SQLExpress;AttachDbFilename=c:\asd\qwe\mydbfile.mdf;Database=dbname; Trusted_Connection=Yes;");
            showConnectionStringAndShortestConnectionString(@"Server=.\SQLExpress;AttachDbFilename=|DataDirectory|mydbfile.mdf; Database=dbname;Trusted_Connection=Yes;");
            showConnectionStringAndShortestConnectionString(@"Data Source=.\SQLExpress;Integrated Security=true; AttachDbFilename=|DataDirectory|\mydb.mdf;User Instance=true;");
            showConnectionStringAndShortestConnectionString("Data Source=myServerAddress;Failover Partner=myMirrorServerAddress;Initial Catalog=myDataBase;Integrated Security=True;");
            showConnectionStringAndShortestConnectionString("Server=myServerAddress;Database=myDataBase;Integrated Security=True;Asynchronous Processing=True;");

            // the HTML table of equivalent keys

            IEnumerable<string> keys = SqlConnectionStringBuilderHelper.Keys;
            IEnumerable<string> orderedKeys = keys.OrderBy(s => s); // http://stackoverflow.com/a/3630693/29290

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Key");
            dataTable.Columns.Add("ShortesEquivalentKey");
            dataTable.Columns.Add("EquivalentKeys");

            foreach (string key in orderedKeys)
            {
                string shortestEquivalentKey = SqlConnectionStringBuilderHelper.ShortestEquivalentKey(key);
                List<string> equivalentKeys = SqlConnectionStringBuilderHelper.EquivalentKeys(key);

                string equivalentKeysCSV = AsCSV(equivalentKeys);

                dataTable.Rows.Add(key, shortestEquivalentKey, equivalentKeysCSV);
            }

            string htmlTable = AsHtmlTable(dataTable);
            Console.WriteLine(htmlTable);
            Console.ReadLine();
        }

        private static void showConnectionStringAndShortestConnectionString(string connectionString)
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            connectionString = connectionStringBuilder.ConnectionString;
            string shortestConnectionString = SqlConnectionStringBuilderHelper.ShortestConnectionString(connectionStringBuilder);

            Console.WriteLine(connectionString);
            Console.WriteLine(shortestConnectionString);
            Console.WriteLine();
        }

        // http://stackoverflow.com/a/799454/29290
        static string AsCSV(IEnumerable<string> strings)
        {
            string[] stringsArray = strings.ToArray();

            string joined = string.Join(",", stringsArray);

            return joined;
        }

        // http://stackoverflow.com/a/1018811/29290
        static string AsHtmlTable(DataTable dataTable)
        {
            // HtmlTextWriter and DataGrid are in System.Web.dll which is not part of the .NET Framework 4 Client Profile; you need the full .NET Framework 4 profile for this
            DataGrid dataGrid = new DataGrid();
            dataGrid.DataSource = dataTable;
            dataGrid.DataBind();

            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);

            dataGrid.RenderControl(htmlTextWriter);

            string result = stringWriter.ToString();
            return result;
        }

    }
}
