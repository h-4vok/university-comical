<%@ Page Title="Rol" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RoleEdit.aspx.cs" Inherits="Comical.Web.RoleEdit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="text-center"><%: Title %></h2>

    <div class="d-flex justify-content-center p-2 flex-column">

        <div class="form-group center text-center mtb-20">

            <label for="codeInput">Código</label>
            <input style="width: 600px;" runat="server" class="form-control" id="codeInput" placeholder="Ingrese un código" type="text">

        </div>

        <div class="form-group center text-center mtb-20">

            <label for="descriptionInput">Descripción</label>
            <input style="width: 600px;" runat="server" class="form-control" id="descriptionInput" placeholder="Ingrese una descripción" type="text">

        </div>

        <asp:Button class="btn btn-primary btn-lg btn-block center" runat="server" OnClick="ActionButton_Click" ID="ActionButton" Text="Crear" />

        <div class="center p-2">
            <asp:Label CssClass="error-label" ID="lblError" Visible="false" runat="server"></asp:Label>
        </div>
    </div>

</asp:Content>
