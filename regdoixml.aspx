<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="regdoixml.aspx.cs" Inherits="resolver.regdoixml" validateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DOI Registation GUI</title>
    <link rel="stylesheet" href="bootstrap.css" />
</head>
<body>
    <form id="form1" action="regdoi.aspx" method="post" enctype="multipart/form-data">
        <br />
        <div class="container" >
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">RegDOI GUI</h3>
                </div>
                <div class="panel-body">
                    XML<br />
                    <textarea class="form-control" name="xml" rows="10" ><%=Request["xml"] %></textarea><br />
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
