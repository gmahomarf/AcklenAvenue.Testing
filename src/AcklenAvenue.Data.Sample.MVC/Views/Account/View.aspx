<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<AcklenAvenue.Data.Sample.MVC.Models.AccountModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <dl>
        <dt>Id</dt>
        <dd><%=Model.Id %></dd>
        <dt>Name</dt>
        <dd><%=Model.Name %></dd>
    </dl>
</body>
</html>
