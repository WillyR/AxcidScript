<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Packager.aspx.cs" Inherits="WebpushersScriptMarket.Packager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Webpushers Script Packager</h1>
    <form id="form1" runat="server">
        Package List: <br />
        <div>
            <% foreach (string name in AppNames)
               { %>
               <div class="app-row" style="border-bottom: 1px solid #000;">
                Application: <%=name %><br />
                <a href="?repackage=<%=name %>">Repackage Local Folder</a>
               </div>               
            <% } %>
        </div>
        <div align="center"><font style="font-size: 11px;">&copy; Webpushers 2012-2013</font></div>
    </form>
</body>
</html>
