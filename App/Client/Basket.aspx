<%@ Page Title="Basket" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Basket.aspx.cs" Inherits="Client.Basket" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div class="container mt-4">
            <asp:Label ID="LbTitle" runat="server" Text="Koszyk" CssClass="lb-title"></asp:Label> 

            <div class="mb-3"></div>

            <asp:ListView ID="LvListOrdersPizza" ItemPlaceholderID="itemPlaceHolder" GroupPlaceholderID="groupPlaceHolder" GroupItemCount="1" runat="server">
                <LayoutTemplate>
                    <asp:PlaceHolder ID="groupPlaceHolder" runat="server"></asp:PlaceHolder>
                </LayoutTemplate>

                <GroupTemplate>
                    <div class="row">
                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server" ></asp:PlaceHolder>
                    </div>
                </GroupTemplate>

                <ItemTemplate>
                    <div class="col-md-12 mb-4">
                        <div class="card h-100">
                             <div class="card-header">
                                <div class="row">
                                    <div class="col-sm-2">
                                        <asp:Label runat="server" Text="Pizza" CssClass="lb-title-basket-order"></asp:Label> 
                                        <asp:Label ID="lbCountPizza" runat="server" Text=<%# Container.DataItemIndex + 1%> CssClass="lb-title-basket-order"></asp:Label>
                                    </div>
                                    <div class="col-sm-10 text-right">
                                        <asp:Label ID="Label1" runat="server" Text=<%# Eval("Price") %> CssClass="lb-price-basket-order"></asp:Label> 
                                        <asp:Label ID="Label2" runat="server" Text=",00 PLN"></asp:Label>
                                    </div>
                                </div>
                             </div>
                            <div class="card-body">
                                <h2 class="card-title lb-title-menu"></h2>
                                <p class="card-text lb-description-menu">ciasto, sos pomidorowy, ser, szynka, kabanosy, boczek wędzony, salami,</p>
                            </div>
                        </div>
                    </div>
            </ItemTemplate>
        </asp:ListView>   

        <div class="row">
            <div class="col-sm-8"></div>
        
            <div class="col-sm-2">
                <asp:Label runat="server" Text="Suma częściowa" CssClass="lb-label-basket-partial-summary"></asp:Label> 
            </div>
        
            <div class="col-sm-2 text-right">                               
                <asp:Label ID="LbPartialSum" runat="server" Text="10" CssClass="lb-price-basket-partial-summary"></asp:Label> 
                <asp:Label runat="server" Text=",00 PLN" CssClass="lb-currency-basket-partial-summary"></asp:Label>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-8"></div>
        
            <div class="col-sm-2">
                <asp:Label runat="server" Text="Dostawa i obsługa" CssClass="lb-label-basket-partial-summary"></asp:Label> 
            </div>
        
            <div class="col-sm-2 text-right">                               
                <asp:Label ID="LbDeliveryAndService" runat="server" Text="10"  CssClass="lb-price-basket-partial-summary"></asp:Label> 
                <asp:Label runat="server" Text=",00 PLN" CssClass="lb-currency-basket-partial-summary"></asp:Label>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-8"></div>
        
            <div class="col-sm-4">
                <hr class="mb-3"/>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-8"></div>
        
            <div class="col-sm-2">
                <asp:Label runat="server" Text="ŁĄCZNIE" CssClass="lb-label-basket-summary"></asp:Label> 
            </div>
        
            <div class="col-sm-2 text-right">                               
                <asp:Label ID="LbTotal" runat="server" Text="10" CssClass="lb-price-basket-summary"></asp:Label> 
                <asp:Label runat="server" Text=",00 PLN" CssClass="lb-currency-basket-summary"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-8"></div>
        
            <div class="col-sm-4 mt-3">
        <button type="button" class="btn btn-register bg-green" data-toggle="modal" data-target="#exampleModal">
  Przejdź do kasy
</button>
            </div>
        </div>



<!-- Modal -->
<div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header bg-red-light">
        <h5 class="modal-title text-white" id="exampleModalLabel">Adres dostawy</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
         <div class="row mb-3">
            <div class="col-sm-12">
                <asp:textbox runat="server" ID="Text7" CssClass="form-control w-100" placeholder="Miejscowość"/>
            </div>
        </div>
         <div class="row mb-3">
            <div class="col-sm-12">
                <asp:textbox runat="server" ID="Text2" CssClass="form-control w-100" placeholder="Ulica"/>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-sm-6">
                <asp:textbox runat="server" ID="Text3" CssClass="form-control w-100" placeholder="Numer domu"/>
            </div>

            <div class="col-sm-6">
                <asp:textbox runat="server" ID="Text4" CssClass="form-control w-100" placeholder="Numer mieszkania"/>
            </div>
        </div>
         <div class="row mb-3">
            <div class="col-sm-12">
                <asp:textbox runat="server" ID="Text5" CssClass="form-control w-100" placeholder="Kod pocztowy"/>
            </div>
        </div>

         <div class="row">
            <div class="col-sm-12">
                <asp:textbox runat="server" ID="Textbox8" CssClass="form-control w-100" placeholder="Poczta"/>
            </div>
        </div>
          
      </div>
      <div class="modal-footer">
        <asp:Button ID="BtnOfferDetails" runat="server" Text="Anuluj" CssClass="btn btn-cancel-address text-secondary" data-dismiss="modal"/>
        <asp:Button ID="AcceptOrder" runat="server" Text="Zamawiaj" CssClass="btn bg-green btn-accept-address pl-4 pr-4" OnClick="AcceptOrder_Click"/>
      </div>
    </div>
  </div>
</div>

    </div>
</asp:Content>