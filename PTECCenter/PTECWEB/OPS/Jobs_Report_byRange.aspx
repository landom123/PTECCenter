<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="Jobs_Report_byRange.aspx.vb" Inherits="PTECCENTER.Jobs_Report_byRange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
    <style>
        .input-group {
            margin-bottom: 1rem;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper" class="h-100">

        <!-- #include virtual ="/include/menu.inc" -->
        <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">
                <ol class="breadcrumb" style="background-color: deeppink; color: white">
                    <li class="breadcrumb-item">รายงาน OPS 
                    </li>
                </ol>
                <div class="row">
                    <div class="col-12">
                        <asp:Button class="btn btn-sm  btn-primary" ID="btnReport" runat="server" Text="เรียกดู" UseSubmitBehavior="false" />
                    </div>
                </div>
                <div class="row" style="padding-top: 1rem;">
                    <div class="col-md-auto">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ฝ่าย</span>
                                <asp:DropDownList ID="cboDep" class="form-control" runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-auto">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">หัวข้องาน</span>
                                <asp:DropDownList ID="cboJobType" class="form-control" runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-auto">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">สถานะงานย่อย</span>
                                <asp:DropDownList class="form-control" ID="cboStatusFollow"
                                    runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-auto">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ประเภทสาขา</span>
                                <asp:DropDownList class="form-control" ID="cboBranchGroup"
                                    runat="server" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-auto">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">สาขา</span>
                                <asp:DropDownList class="form-control" ID="cboBranch"
                                    runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">เริ่ม</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtBeginDate" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">สิ้นสุด</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtEndDate" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <!-- end content-wrapper -->


            <!-- end เนื้อหาเว็บ -->

            <!-- Sticky Footer -->
            <footer class="sticky-footer d-none">
                <div class="container my-auto">
                    <div class="copyright text-center my-auto">
                        <span>Copyright © Your Website 2019</span>
                    </div>
                </div>
            </footer>
        </div>
        <!-- /#wrapper -->

    </div>
    <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>

    <script type="text/javascript">
        jQuery('[id$=txtBeginDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            onShow: function (ct) {
                this.setOptions({
                    maxDate: jQuery('[id$=txtEndDate]').val() ? jQuery('[id$=txtEndDate]').val() : false, formatDate: 'd.m.Y'
                })
            },
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>

    <script type="text/javascript">
        jQuery('[id$=txtEndDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            onShow: function (ct) {
                this.setOptions({
                    minDate: jQuery('[id$=txtBeginDate]').val() ? jQuery('[id$=txtBeginDate]').val() : false, formatDate: 'd.m.Y'
                })
            },
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.form-control').selectpicker({
                liveSearch: true,
                maxOptions: 1
            });
        });
    </script>

</asp:Content>
