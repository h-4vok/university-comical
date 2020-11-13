<%@ Page Title="Bitácora de Excepciones" 
    Language="C#" 
    MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" 
    CodeBehind="Errors.aspx.cs" 
    Inherits="Comical.Web.Errors" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="text-center"><%: Title %></h2>

    <div class="d-flex justify-content-center p-2 flex-column">

        <div class="center p-2">
            <asp:Label CssClass="error-label" ID="lblError" Visible="false" runat="server"></asp:Label>
        </div>

        <asp:DataGrid
            runat="server"
            ID="Grid">

        </asp:DataGrid>

    </div>

</asp:Content>
