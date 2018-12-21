<%@ Page Title="Basket" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmptyBasket.aspx.cs" Inherits="Client.EmptyBasket" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <asp:Label ID="LbTitle" runat="server" Text="Koszyk jest pusty!" CssClass="lb-title"></asp:Label> 
    </div>
     
    <div class="mt-5 mb-5">
    </div>
</asp:Content>