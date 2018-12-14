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
                                        <asp:Label ID="Label1" runat="server" Text=<%# string.Format("{0:0.00}", Eval("Price"))%> CssClass="lb-price-basket-order"></asp:Label> 
                                        <asp:Label ID="Label2" runat="server" Text=" PLN"></asp:Label>
                                    </div>

                                </div>
                             </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-sm-10">
                                        <h2 class="card-title lb-title-menu"></h2>
                                        <p class="card-text lb-description-menu">
                                            <%# Eval("IngredientsStr")%>
                                        </p>
                                    </div>
                                    <div class="col-sm-2 text-right">
                                        <asp:Button ID="BtnCancelOrder" runat="server" Text="Anuluj" CssClass="btn btn-cancel-order" OnClick="BtnCancelOrder_Click" commandArgument='<%# Container.DataItemIndex%>'/>
                                    </div>
                                </div>
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
                <asp:Label ID="LbPartialSum" runat="server" CssClass="lb-price-basket-partial-summary"></asp:Label> 
                <asp:Label runat="server" Text=" PLN" CssClass="lb-currency-basket-partial-summary"></asp:Label>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-8"></div>
        
            <div class="col-sm-2">
                <asp:Label runat="server" Text="Dostawa i obsługa" CssClass="lb-label-basket-partial-summary"></asp:Label> 
            </div>
        
            <div class="col-sm-2 text-right">                               
                <asp:Label ID="LbDeliveryAndService" runat="server" Text="10"  CssClass="lb-price-basket-partial-summary"></asp:Label> 
                <asp:Label runat="server" Text=" PLN" CssClass="lb-currency-basket-partial-summary"></asp:Label>
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
                <asp:Label runat="server" Text=" PLN" CssClass="lb-currency-basket-summary"></asp:Label>
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
            <div class="col-sm-6">
                <asp:textbox runat="server" ID="Name" CssClass="form-control w-100" placeholder="Imię" required/>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                                                CssClass="error"
                                                runat="server"
                                                ControlToValidate="Name" 
                                                ErrorMessage="Niepoprawne!"
                                                ValidationExpression="^[a-zA-Z]+$">
                </asp:RegularExpressionValidator>
            </div>

            <div class="col-sm-6">
                <asp:textbox runat="server" ID="Surname" CssClass="form-control w-100" placeholder="Nazwisko" required/>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                                                CssClass="error"
                                                runat="server"
                                                ControlToValidate="Surname" 
                                                ErrorMessage="Niepoprawne!"
                                                ValidationExpression="^[a-zA-Z]+$">
                </asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-sm-8">
                <asp:textbox runat="server" ID="Street" CssClass="form-control w-100" placeholder="Ulica" required/>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" 
                                                CssClass="error"
                                                runat="server"
                                                ControlToValidate="Street" 
                                                ErrorMessage="Niepoprawna!"
                                                ValidationExpression="^[a-zA-Z]+$">
                </asp:RegularExpressionValidator>
            </div>

            <div class="col-sm-4">
                <asp:textbox runat="server" ID="HouseNumber" CssClass="form-control w-100" placeholder="Numer domu" required/>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" 
                                                CssClass="error"
                                                runat="server"
                                                ControlToValidate="HouseNumber" 
                                                ErrorMessage="Niepoprawny!"
                                                ValidationExpression="\d{1,3}">
                </asp:RegularExpressionValidator>
            </div>
        </div>
         <div class="row mb-3">
            <div class="col-sm-4">
                <asp:textbox runat="server" ID="PostCode" CssClass="form-control w-100" placeholder="Kod pocztowy" required/>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" 
                                CssClass="error"
                                runat="server"
                                ControlToValidate="PostCode" 
                                ErrorMessage="Niepoprawny!"
                                ValidationExpression="\d{2}-\d{3}">
                </asp:RegularExpressionValidator>
            </div>

            <div class="col-sm-8">
                <asp:textbox runat="server" ID="City" CssClass="form-control w-100" placeholder="Poczta" required/>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" 
                                                CssClass="error"
                                                runat="server"
                                                ControlToValidate="City" 
                                                ErrorMessage="Niepoprawna poczta"
                                                ValidationExpression="^[a-zA-Z]+$">
                </asp:RegularExpressionValidator>
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