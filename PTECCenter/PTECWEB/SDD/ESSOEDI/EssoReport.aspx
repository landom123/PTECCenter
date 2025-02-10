<%@ Page Title="SDD Web Application : Upload Order" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="EssoReport.aspx.vb" Inherits="PTECCENTER.EssoReport" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <!-- datetimepicker-->
  <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper" class="h-100">

    <!-- #include virtual ="/include/menu.inc" --> <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">    
                <!-- Breadcrumbs-->
                <ol class="breadcrumb">
                  <li class="breadcrumb-item">
                    <a href="#">SDD\รายงานสถานะ PO
                    </a>
                  </li>
                </ol>


                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                            <span class="input-group-text">วันที่</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtBeginDate" runat="server" autocomplete="off"></asp:TextBox>
                        </div>                                                
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                            <span class="input-group-text">วันที่</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtEndDate" runat="server" autocomplete="off"></asp:TextBox>
                        </div>                                                
                    </div>   
                    <div class="col-4">
                        <asp:Button ID="btnReport"  class="btn btn-sm  btn-success" runat="server" Text="รายงาน" />
                    </div>
                    
                </div>
    <p></p>
        <style>
            .my_text
            {
                font-family:    'TH Sarabun New';
            }
        </style>
    <div class="my_text">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ReuseParameterValuesOnRefresh="True" ToolPanelView="None" BestFitPage="False" PrintMode="ActiveX" HyperlinkTarget="_blank"/>
    </div>



            </div>            <!-- /.container-fluid -->
            <!-- Sticky Footer -->
            <footer class="sticky-footer d-none">
                <div class="container my-auto">
                    <div class="copyright text-center my-auto">
                    <span>Copyright © Your Website 2019</span>
                    </div>
                </div>
            </footer>
        </div>        <!-- end content-wrapper -->


        <!-- end เนื้อหาเว็บ -->


    </div>
    <!-- /#wrapper -->
  <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>

    <script type="text/javascript">
        jQuery('[id$=txtBeginDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format:'d/m/yy'
        });
    </script>
    <script type="text/javascript">
        jQuery('[id$=txtEndDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format:'d/m/yy'
        });
    </script>
</asp:Content>
