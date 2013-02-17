using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // http://stackoverflow.com/questions/9344650/asp-net-use-variable-in-aspx-page
        if (!IsPostBack)
            DataBind();
    }

    public string NameValues
    {
        get
        {
            // ##jpl: don't forget to put your AssemblyHelperNameValues.cs files in the App_Code directory, or VS2010 will nog recognize them
            string result = AssemblyHelperHelper.AssemblyHelperNameValues.NameValuesXHtml;
            return result;
        }
    }
}
