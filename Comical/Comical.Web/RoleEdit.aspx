﻿<%@ Page Title="Rol" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RoleEdit.aspx.cs" Inherits="Comical.Web.RoleEdit" %>

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

        <div class="form-group center text-center mtb-20">

            <label class="display-block">Permisos</label>
            <asp:ListBox
                ID="permissionsList"
                runat="server"
                SelectionMode="Multiple"
                CssClass="listbox-medium"
                >

            </asp:ListBox>

        </div>

        <div class="form-group center text-center mtb-20">
            <asp:Button class="btn btn-primary btn-lg btn-block center" runat="server" OnClick="ActionButton_Click" ID="ActionButton" Text="Finalizar" />
        </div>

        <div class="center p-2">
            <asp:Label CssClass="error-label" ID="lblError" Visible="false" runat="server"></asp:Label>
        </div>
    </div>

</asp:Content>
