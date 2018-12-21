<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Client.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <asp:Label ID="LbTitle" runat="server" Text="O nas" CssClass="lb-title"></asp:Label> 

        <div class="mt-5 mb-5">
            <p><b>Dane firmy:</b></p>
            <p>
                Pełen Brzuszek<br/>
                ul. Bojemskiego 25<br/>
                42-202 Częstochowa<br/>
                NIP: 9492107026, REGON: 241123546<br/>
                KRS 0000429838: Sąd Rejonowy w Częstochowie<br/>
                XVII Wydział Gospodarczy Krajowego Rejestru Sądowego<br/>
            </p>
        </div>
    </div>
</asp:Content>