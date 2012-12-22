using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace WebpushersScriptMarket
{
	public partial class Packager : System.Web.UI.Page
	{
		protected string[] _dirs = Directory.GetDirectories(HttpContext.Current.Server.MapPath("~/market-apps/"));

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(Request["repackage"]))
			{
				string folder = Request["repackage"] as String;
				string directory = _dirs.Where((x) => x.Split(Path.DirectorySeparatorChar).Last() == folder).SingleOrDefault();

				webpushers.MicroApp repackaged_app = new webpushers.MicroApp(directory);

				Response.ClearHeaders();
				Response.ClearContent();
				Response.AddHeader("Content-Type", "text/javascript");
				Response.Write("market_load(" + repackaged_app.JSONOutput + ");");
				Response.Flush();
				Response.End();
				return;
			}
		}

		public string[] AppNames
		{
			get
			{
				return _dirs.Select((x) => x.Split(Path.DirectorySeparatorChar).Last()).ToArray();
			}
		}
	}
}