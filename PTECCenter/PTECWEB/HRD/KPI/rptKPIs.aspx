<%@ Page Title="rptKPIs" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="rptKPIs.aspx.vb" Inherits="PTECCENTER.rptKPIs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
    <style>

        #content-wrapper {
            min-height: 600px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper">

        <!-- #include virtual ="/include/menu.inc" -->
        <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">KPIs Report</a>
                    </li>
                </ol>
                <div class="row">
                    <div class="col-md-3 mb-3">
                        <asp:Button ID="btnExport" class="btn btn-sm btn-success" runat="server" Text="Export CSV" />
                        &nbsp;           
                    </div>
                </div>
                <div class="row">
                    <div class="col mb-3">
                        <asp:DropDownList ID="cboPeriod" class="form-control" runat="server"></asp:DropDownList>
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
