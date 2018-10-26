<%@ Page Title="PizzaDetails" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PizzaDetails.aspx.cs" Inherits="Client.PizzaDetails" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="row">
            <div class="col-md-6">
                <div class="table-responsive">
                    <asp:GridView ID = "GvListIngredients"
                                  runat="server"
                                  AutoGenerateColumns ="False" 
                                  CssClass="table table-bordered table-condensed">
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Składnik" 
                                ReadOnly="True" SortExpression="CategoryName" />
                            <asp:BoundField DataField="Price" DataFormatString="{0:c}" 
                                HeaderText="Cena" HtmlEncode="False" 
                                SortExpression="UnitPrice" />
                            <asp:CheckBoxField DataField="Status" HeaderText="" SortExpression="contract" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="col-md-2">

            </div>

            <div class="col-md-4 p-0">
                <div class="row mt-4">
                    <div class="col-md-12">
                        <asp:Label ID="LbTitle" runat="server" Text="Tytuł"></asp:Label>
                    </div>
                </div>

                <div class="row mt-4">
                    <div class="col-md-12">
                        <asp:Label ID="LbPrice" runat="server" Text="0.00"></asp:Label> 
                        <asp:Label ID="LbCurrency" runat="server" Text="PLN"></asp:Label>
                    </div>
                </div>

                <div class="row mt-4 mb-4">

                </div>

                <div class="row mb-4 p-0">
                    <div class="col-md-12 p-0">
                        <asp:Button ID="BtnOrder" runat="server" Text="Zamów" OnClick="BtnOrder_Click" CssClass="btn-order bg-green"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
