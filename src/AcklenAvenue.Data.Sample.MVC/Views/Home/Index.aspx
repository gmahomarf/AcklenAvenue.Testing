<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h2>AcklenAvenue.Data Sample</h2>
    <p><a href='<%=Url.Action("View", "Account", new{Id = 1}) %>'>Account #1</a></p>
    <p><a href='<%=Url.Action("View", "Account", new{Id = 2}) %>'>Account #2</a></p>
    <p><a href='<%=Url.Action("View", "Account", new{Id = 3}) %>'>Account #3</a></p>
</body>
</html>
