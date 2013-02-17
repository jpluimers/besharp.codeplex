using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for AssemblyHelperWebService
/// </summary>
[WebService(Namespace = "http://BeSharp.net/AssemblyHelperWebService/")] // ##jpl: always change the http://tempuri.org/ into something meaningful
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class AssemblyHelperWebService : System.Web.Services.WebService
{

    public AssemblyHelperWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        string result = AssemblyHelperHelper.AssemblyHelperNameValues.NameValuesXHtml;
        return result;
        // return "Hello World";
    }

}
