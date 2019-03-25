<%@ Page Title="" Language="C#" MasterPageFile="~/SarahByXehar.Master" AutoEventWireup="true" CodeBehind="sarahByXehar.aspx.cs" Inherits="xehardiversity.sarahByXehar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Products">
    <div class="row" id="productRow" runat="server">

                            
    </div>
        <div class="cart">
        <a href="cart.aspx" title="Cart"><i class="ion-ios-cart"></i></a>
    </div>

    <form action="#" method="post" runat="server">
        <div id="quirks" style="display: none;" runat="server"></div>
        <asp:HiddenField runat="server" ID="pros"/>
        <asp:HiddenField runat="server" ID="cSKU"/>
        <asp:HiddenField runat="server" ID="quan"/>
        <asp:HiddenField runat="server" ID="size"/>
        <div style="display: none">
            <asp:Button runat="server" ID="Runner" OnClick="AddToCart" />
        </div>
    </form>

    <%--<div id="addCart" runat="server"></div>--%>
</asp:Content>