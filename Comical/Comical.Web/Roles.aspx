<%@ Page Title="Lista de Permisos" 
    Language="C#" 
    MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" 
    CodeBehind="Roles.aspx.cs" 
    Inherits="Comical.Web.Roles" %>

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
            OnDeleteCommand="Grid_Delete"
            OnUpdateCommand="Grid_Update"
            DataKeyField="Id"
            >

            <Columns>
                <asp:BoundColumn ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" HeaderText="ID" DataField="Id"></asp:BoundColumn>
                <asp:BoundColumn HeaderStyle-CssClass="text-center"  HeaderText="Código" DataField="Code"></asp:BoundColumn>
                <asp:BoundColumn HeaderStyle-CssClass="text-center" HeaderText="Descripción" DataField="Description"></asp:BoundColumn>
                <asp:BoundColumn ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" HeaderText="Habilitado" DataField="Enabled"></asp:BoundColumn>
                <asp:HyperLinkColumn ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" DataNavigateUrlFormatString="Role.aspx?ID={0}" DataNavigateUrlField="Id" Text="Actualizar" HeaderText="Actualizar"></asp:HyperLinkColumn>
                <asp:ButtonColumn ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" CommandName="Delete" HeaderText="Eliminar" Text="Eliminar"></asp:ButtonColumn>
            </Columns>

        </asp:DataGrid>

        <asp:Button 
            class="btn btn-primary btn-lg btn-block center p-2" 
            runat="server" 
            OnClick="NewRoleButton_Click" 
            ID="NewRoleButton" 
            Text="Crear Nuevo Rol" />

    </div>

</asp:Content>
