<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Client._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row my-4">
            <div class="col-lg-8">
                <img class="img-fluid rounded" src="Image/logo.jpg" alt="">
            </div>
                
            <div class="col-lg-4">
                <h1 class="lb-title">Zamów jedzenie online!</h1>
                <p class="lb-description">Jesteś głodny lub po prostu masz ochotę na coś pysznego? Może chcesz spróbować dobrego jedzenia, nie wychodząc z domu? PełenBrzuszek to serwis do zamawiania jedzenia online. Zamawiaj jedzenie z dowozem do domu tylko przez Internet!</p>
                <asp:Button ID="BtnBasket" runat="server" Text="Koszyk" CssClass="btn btn-register bg-green" OnClick="BtnBasket_Click"/>
            </div>
        </div>

        <hr class="mb-4"/>

        <asp:ListView ID="LvListOffersPizza" ItemPlaceholderID="itemPlaceHolder" GroupPlaceholderID="groupPlaceHolder" GroupItemCount="3" runat="server">
            <LayoutTemplate>
                <asp:PlaceHolder ID="groupPlaceHolder" runat="server"></asp:PlaceHolder>
            </LayoutTemplate>

            <GroupTemplate>
                <div class="row">
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" ></asp:PlaceHolder>
                </div>
            </GroupTemplate>

            <ItemTemplate>
                <div class="col-md-4 mb-4">
                    <div class="card h-100">
                            
                    <div class="card-header text-center"><img src="Image/<%# Container.DataItemIndex%>.png" alt=""></div>
                        <div class="card-body">
                            <h2 class="card-title lb-title-menu"><%# Eval("Name") %></h2>
                            <p class="card-text lb-description-menu">
                                <%# ((List<Model.Ingredient>)Eval("Ingredients"))[0].Name %>,
                                <%# ((List<Model.Ingredient>)Eval("Ingredients"))[1].Name %>,
                                <%# ((List<Model.Ingredient>)Eval("Ingredients"))[2].Name %>,
                                . . .
                            </p>
                        </div>  

                        <div class="card-footer bg-yellow">
                            <div class="row">
                                <div class="col-sm-6">
                                    <asp:Button ID="BtnOfferDetails" runat="server" Text="Zobacz" OnClick="BtnOfferDetails_Click" CssClass="btn bg-red-light btn-view" commandArgument='<%# Container.DataItemIndex%>'/>
                                </div>
                            
                                <div class="col-sm-6 text-right">
                                    <asp:Label ID="LbPrice" runat="server" Text=<%# string.Format("{0:0.00}", Eval("Price"))%> CssClass="lb-price-menu"></asp:Label> 
                                    <asp:Label ID="LbCurrency" runat="server" Text=" PLN"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>   
    </div>
</asp:Content>
