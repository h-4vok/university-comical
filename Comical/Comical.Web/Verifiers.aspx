<%@ Page Title="Dígitos Verificadores" 
    Language="C#" 
    MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" 
    CodeBehind="Verifiers.aspx.cs" 
    Inherits="Comical.Web.Verifiers" %>

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

        <asp:Button 
            class="btn btn-primary btn-lg btn-block center" 
            runat="server" 
            OnClick="RecalculateButton_Click" 
            ID="RecalculateButton" 
            Text="Recalcular Todos" />
       
    </div>

</asp:Content>
