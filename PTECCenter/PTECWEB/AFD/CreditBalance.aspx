<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="CreditBalance.aspx.vb" Inherits="PTECCENTER.CreditBalance" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="btn btn-lg btn-block btn-success">
        <h3>รายงานยอดลูกหนี้เงินเชื่อ</h3>
    </div>
   <br />
    <div class="row">
        <div class="col-md-12" >

            <asp:DropDownList ID="listDate" runat="server" Height="25px" Width="236px">
            </asp:DropDownList>&nbsp;
            <asp:Button ID="btnView" runat="server" Text="View" cssclass="btn btn-success btn-sm" />
&nbsp;</div>
    </div>

    <div class="row">        
        <div class="col-md-12">   

                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ReuseParameterValuesOnRefresh="True" ToolPanelView="None" BestFitPage="False" PrintMode="ActiveX" />

        </div>
    </div>

</asp:Content>
