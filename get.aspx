<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="get.aspx.cs" Inherits="resolver.get" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RSA Resolver</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
</head>
<body>
1
    <form id="form1" runat="server">
        <br />
        <div class="container" >
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">RSA Resolver</h3>
                </div>
                <div class="panel-body">
                    <br />
                    <% if (view != "") { %>
                    URL:<br />
                    <a href="<%=view%>"><%=view%></a>
                    <% } else {
                            //Response.Write(message);
                            Response.Redirect(view);
                        }
                    %>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
