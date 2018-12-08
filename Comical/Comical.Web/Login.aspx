<%@ Page Title="Autenticación" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Comical.Web.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="text-center"><%: Title %></h2>

    <div class="d-flex justify-content-center p-2 flex-column">

        <div id="divUser" runat="server" visible="true" class="form-group center text-center mtb-20">

            <label for="loginInput">Usuario</label>
            <input style="width: 600px;" runat="server" class="form-control" id="loginInput" aria-describedby="loginHelp" placeholder="Ingrese su usuario" type="text">
            <small id="loginHelp" class="form-text text-muted">Nunca compartiremos tus datos con nadie.</small>

        </div>

        <div id="divPassword" runat="server" visible="true" class="form-group center text-center mtb-20">
            
            <label for="passwordInput">Password</label>
            <input style="width: 600px;" runat="server" class="form-control" id="passwordInput" placeholder="Ingrese su contraseña" type="password">

        </div>

        <div id="divSuccess" runat="server" visible="false" class="form-group center text-center mtb-20">
            
            <asp:Label CssClass="success-label" ID="lblSuccess" Visible="false" runat="server">Bienvenido</asp:Label>

        </div>

        <asp:Button class="btn btn-primary btn-lg btn-block center" runat="server" OnClick="LoginButton_Click" ID="LoginButton" Text="Autenticarse" />

        <div class="center p-2">
            <asp:Label CssClass="error-label" ID="lblError" Visible="false" runat="server"></asp:Label>
        </div>
    </div>

</asp:Content>
