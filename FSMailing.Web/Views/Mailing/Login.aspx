<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<FSMailing.Web.Models.LoginModel>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Login</title>
</head>
<body>
    <div>
        <% using (Html.BeginForm("Login", "Mailing", FormMethod.Post))
           {
          %> 
          <p><%: Html.DisplayTextFor(m => m.Message)%></p>
        <p><%: Html.LabelFor(m => m.Login) %><%: Html.TextBoxFor(m => m.Login) %>
        </p>
        <p><%: Html.LabelFor(m => m.Password)%><%: Html.PasswordFor(m => m.Password) %>
        </p>
        <p><input type="submit" value="logar"/></p>
        <%} %>
    </div>
</body>
</html>
