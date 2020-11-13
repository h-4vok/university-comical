<%@ Page Title="Backups de Seguridad" 
    Language="C#" 
    MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" 
    CodeBehind="Backups.aspx.cs" 
    Inherits="Comical.Web.Backups" %>

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
            class="btn btn-primary btn-lg btn-block center p-2" 
            runat="server" 
            OnClick="DoBackup_Click" 
            ID="DoBackup" 
            Text="Realizar Backup" />

        <div class="form-group center text-center mtb-20">
            
            <label for="restoreID">Ingrese un ID para Restaurar</label>
            <input style="width: 600px;" runat="server" class="form-control" id="restoreID" placeholder="Ingrese un ID" type="text">

            <asp:Button class="btn btn-primary btn-lg btn-block center" runat="server" OnClick="DoRestore_Click" ID="DoRestoreButton" Text="Restaurar desde ID" />

        </div>

    </div>

</asp:Content>
