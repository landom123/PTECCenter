<%@ Page Title="Budget" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="frmCN.aspx.vb" Inherits="PTECCENTER.frmCN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
    <style>

        #content-wrapper {
            min-height: 600px;
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
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">E-tax ใบลดหนี้ (CN)</a>
                    </li>
                </ol>
                <div class="row">
                    <div class="col-md-12 mb-3">
                        <asp:Button ID="btnSearch" class="btn btn-sm d-none btn-success" runat="server" Text="Search"   />&nbsp;   
                         <asp:Button ID="btnClear" class="btn btn-sm  btn-secondary" runat="server" Text="Clear"   />&nbsp;
                        <asp:Button ID="btnExport" class="btn btn-sm  btn-success" runat="server" Text="Export CSV"   />
                        &nbsp;           
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3 mb-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Zone</span>
                            </div>
                            <asp:DropDownList class="form-control " ID="cboZone" runat="server" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3 mb-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Branch</span>
                            </div>
                            <asp:DropDownList class="form-control " ID="cboBranch" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3 mb-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">เริ่ม</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtbegindate" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3 mb-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ถึง</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtenddate" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col mb-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Email for test (ถ้ามี)</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtEmail" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <asp:Label ID="note" CssClass="text-danger text-right" runat="server" Text="( Email for test ตัวอย่าง  :  abc@rpcthai.com,def@rpcthai.com )" />
                    </div>
                </div>
            </div>
            <!-- /.container-fluid -->
        </div>
        <!-- end content-wrapper -->
        <!-- end เนื้อหาเว็บ -->
    </div>
    <!-- /#wrapper -->
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <CompositeScript>
            <Scripts>
                <asp:ScriptReference Path="~/datetimepicker/jquery.js" />
                <asp:ScriptReference Path="~/datetimepicker/build/jquery.datetimepicker.full.min.js" />
            </Scripts>
        </CompositeScript>
    </asp:ScriptManager>
    <!-- datetimepicker ต้องไปทั้งชุด-->
    <%--<script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>--%>

    <script type="text/javascript">
        jQuery('[id$=txtbegindate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08'
            onShow: function (ct) {
                this.setOptions({
                    maxDate: jQuery('[id$=txtenddate]').val() ? jQuery('[id$=txtenddate]').val() : false, formatDate: 'd.m.Y'
                })
            },
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });

        jQuery('[id$=txtenddate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            onShow: function (ct) {
                this.setOptions({
                    minDate: jQuery('[id$=txtbegindate]').val() ? jQuery('[id$=txtbegindate]').val() : false, formatDate: 'd.m.Y'
                })
            },
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
        $(document).ready(function () {

            $('.form-control').selectpicker({
                noneSelectedText: '-',
                liveSearch: true
            });


            $('.form-control').selectpicker('refresh');


        });
        function alertSuccess() {
            Swal.fire(
                'สำเร็จ',
                '',
                'success'
            )
        }

        function alertWarning(massage) {
            Swal.fire(
                massage,
                '',
                'warning'
            )
        }
    </script>
</asp:Content>
