<%@ Page Title="PizzaDetails" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OfferDetails.aspx.cs" Inherits="Client.PizzaDetails" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <asp:Label ID="Label1" runat="server" Text="Szczegóły zamówienia" CssClass="lb-title"></asp:Label> 

        <div class="mb-3"></div>
        <div class="row">
            <div class="col-md-8">
                <div class="table-responsive pt-4">
                    <asp:GridView ID = "GvListIngredients"
                                  runat="server"
                                  AutoGenerateColumns ="False" 
                                  CssClass="table table-bordered table-condensed">
                        <HeaderStyle CssClass="bg-gray" />
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Składnik" 
                                ReadOnly="True"  />
                            <asp:BoundField DataField="Price" DataFormatString="{0:c}" 
                                HeaderText="Cena" HtmlEncode="False" 
                                SortExpression="UnitPrice" />
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="CbStatus" runat="server" Checked='<%# Bind("Status") %>' Enabled="true" AutoPostBack="True" OnCliOnCheckedChanged="CbStatus_CheckedChanged" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CbStatus" runat="server" Checked='<%# Bind("Status") %>' Enabled="true" AutoPostBack="True" OnCheckedChanged="CbStatus_CheckedChanged"/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <div class="col-md-4">
                <div class="row mt-4">
                    <div class="col-md-12">
                        <asp:Label ID="LbTitle" runat="server" Text="" CssClass="lb-title-order"></asp:Label>
                    </div>
                </div>

                <div class="row mt-4">
                    <div class="col-md-12">
                        <asp:Label ID="LbPrice" runat="server" Text="0.00" CssClass="lb-price-order"></asp:Label> 
                        <asp:Label ID="LbCurrency" runat="server" Text=" PLN" CssClass="lb-currency-order"></asp:Label>
                    </div>
                </div>

                 <div class="row mt-2 mb-4">
                    <div class="col-md-12">
                        <hr/>
                    </div>
                </div>

                <div class="row mb-4">
                    <div class="col-md-12">
                        <asp:Button ID="BtnOrder" runat="server" Text="Zamów" OnClick="BtnOrder_Click" CssClass="btn btn-order bg-green"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
