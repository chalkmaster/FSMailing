<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<FSMailing.Web.Models.MailingModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Sens NewsLetter Mail</title>
        <link rel="StyleSheet" href="<%: Url.Content("~/Content/Site.css") %>" type="text/css"/>
    </head>
    <body>
        <% using (Html.BeginForm("Send", "Mailing"))
           {%>
           <%: Html.ValidationSummary() %>
           <% if ( Model != null && !String.IsNullOrWhiteSpace(Model.SendReturn))
              {%>
                <h1><center><b><font color="red"><%: Model.SendReturn %></font></b></center></h1>
              <%} %>
           <table>
               <tr>
                   <td>Destinatários (separar com ";" ou ","):<br />
                   <%: Html.TextBoxFor(m => m.To) %>
                   </td>
                </tr>
                <tr>
                   <td>Assunto:<br />
                   <%: Html.TextBoxFor(m => m.Subject) %>
                   </td>
                </tr>
                <tr>
                   <td>Conteúdo (colar o código HTML do e-mail):<br />
                   <%: Html.TextAreaFor(m => m.Body) %>
                   </td>
               </tr>
           </table>
           <input type="submit" value="Enviar Email"/>
        <% } %>
    </body>
</html>
