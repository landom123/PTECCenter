<%@ Page Title="rptPCCO" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="rptPCCO.aspx.vb" Inherits="PTECCENTER.rptPCCO" %>

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
                        <a href="#">EDI Invoice</a>
                    </li>
                </ol>
                <div class="row">
                    <div class="col-md-12 mb-3">
                        <asp:Button ID="btnClear" class="btn btn-sm btn-secondary" runat="server" Text="Clear" />&nbsp;
                        <asp:Button ID="btnExport" class="btn btn-sm btn-info" runat="server" Text="Export" />
                        &nbsp;           
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3 mb-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">เริ่ม</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtduedate" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col">
                        <div class="form-group pl-5">
                            <input class="form-check-input chk-img-after" type="checkbox" id="chkGroupVAT" runat="server">
                            <asp:Label ID="Label8" CssClass="form-check-label" AssociatedControlID="chkGroupVAT" runat="server" Text="รวบ VAT" />
                        </div>
                        <div class="form-group pl-5">
                            <input class="form-check-input chk-img-after" type="checkbox" id="chkGroupVendor" runat="server">
                            <asp:Label ID="Label10" CssClass="form-check-label" AssociatedControlID="chkGroupVendor" runat="server" Text="รวบ Vendor" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.container-fluid -->
        </div>
        <!-- end content-wrapper -->
        <!-- end เนื้อหาเว็บ -->
    </div>
    <!-- /#wrapper -->
    <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>

    <script type="text/javascript">
        jQuery('[id$=txtduedate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08'
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
