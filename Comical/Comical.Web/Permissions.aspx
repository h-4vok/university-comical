<%@ Page Title="Lista de Permisos" 
    Language="C#" 
    MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" 
    CodeBehind="Permissions.aspx.cs" 
    Inherits="Comical.Web.Permissions" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="text-center"><%: Title %></h2>

    <div class="d-flex justify-content-center p-2 flex-column">

        <div class="center p-2">
            <asp:Label CssClass="error-label" ID="lblError" Visible="false" runat="server"></asp:Label>
        </div>

        <asp:DataGrid
            runat="server"
            ID="Grid"
            AutoGenerateColumns="false"
            >

            <Columns>
                <asp:BoundColumn HeaderText="ID" DataField="Id"></asp:BoundColumn>
                <asp:BoundColumn HeaderText="Código" DataField="Code"></asp:BoundColumn>
            </Columns>

        </asp:DataGrid>

    </div>

</asp:Content>
