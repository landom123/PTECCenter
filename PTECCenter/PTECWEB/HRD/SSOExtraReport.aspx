<%@ Page Title="SDD Web Application : Upload Order" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="SSOExtraReport.aspx.vb" Inherits="PTECCENTER.SSOExtraReport" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper">

    <!-- #include virtual ="/include/menu.inc" --> <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">    
                <!-- Breadcrumbs-->
                <ol class="breadcrumb">
                  <li class="breadcrumb-item">
                    <a href="#">HRD\รายงานส่งเงินสบทบ เฉพาะกิจ
                    </a>
                  </li>
                </ol>


                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                            <span class="input-group-text">เดือน yyyymm (ตย 201601)</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtMonthly" runat="server" autocomplete="off"></asp:TextBox>
                        </div>                                                
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                            <span class="input-group-text">รหัสสาขา (ตย 000000)</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtBranch" runat="server" autocomplete="off"></asp:TextBox>
                        </div>                                                
                    </div>   
                    <div class="col-4">
                        <asp:Button ID="btnReport"  class="btn btn-sm  btn-success" runat="server" Text="รายงาน" />
                    </div>
                    
                </div>
    <p></p>
    <div class="row">
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

</asp:Content>
