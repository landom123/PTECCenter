<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="PayinByHOSReport.aspx.vb" Inherits="PTECCENTER.PayinByHOSReport" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="btn btn-lg btn-block btn-success">
        <h3>รายงานยอดชำระ ลูกหนี้เงินเชื่อ (HO)</h3>
    </div>
   <br />
    <div class="row">
        <div class="col-md-2" style="text-align:right">
            ระบุรหัสลูกค้า
        </div>
        <div class="col-md-2">
            <asp:TextBox ID="txtcuscode" runat="server" type="text" class="form-control" placeholder="รหัสลูกค้า"></asp:TextBox>
        </div>
    </div>
    <div class="row" >
        <div class="col-md-2" style="text-align:right">
            ระบุช่วงวัน
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtbegindate" runat="server" type="text" class="form-control" placeholder="วันที่เริ่มต้น yyyymmdd"></asp:TextBox>
            <asp:TextBox ID="txtenddate" runat="server" type="text" class="form-control" placeholder="วันที่สิ้นสุด yyyymmdd"></asp:TextBox>            
        </div>
        <div class="col-md-4" >
            ถ้าไม่ระบุประเภทจะเท่ากับ 1000<asp:TextBox ID="txttype" runat="server" type="text" class="form-control" placeholder="ระบุประเภท 1,1000" ></asp:TextBox>            
        </div>
        <div class="col-md-2">
            <asp:Button ID="btnView" runat="server" Text="View" cssclass="btn btn-success btn-sm" />
        </div>
    </div>

    <div class="row">        
        <div class="col-md-12">   
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ReuseParameterValuesOnRefresh="True" ToolPanelView="None" BestFitPage="False" PrintMode="ActiveX" />
        </div>
    </div>

</asp:Content>
