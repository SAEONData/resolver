<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="regdoigui.aspx.cs" Inherits="resolver.regdoigui" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DOI Registation GUI</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
</head>
<body>
    <form id="form1" action="regdoi.aspx">
        <br />
        <div class="container" >
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">RegDOI GUI</h3>
                </div>
                <div class="panel-body">
                    URL to JSON Metadata Record:<br />
                    <input class="form-control" name="data" type="text" value="<%=Request["data"] %>" /><br />
                    DOI:<br />
                    <input class="form-control" name="doi" type="text"  value="<%=Request["doi"] %>" /><br />
                    URL for Metadata View:<br />
                    <input class="form-control" name="view" type="text"  value="<%=Request["view"] %>" /><br />
                    Portal Name:<br />
                    <input class="form-control" name="portal" type="text"  value="<%=Request["portal"] %>" /><br />
                    User Name:<br />
                    <input class="form-control" name="username" type="text"  value="<%=Request["username"] %>" /><br />
                    Password:<br />
                    <input class="form-control" name="password" type="text"  value="<%=Request["password"] %>" /><br />
                    <input class="btn btn-primary" id="Submit1" type="submit" value="Submit" />
                </div>
            </div>

        </div>
        
    </form>
</body>
</html>
