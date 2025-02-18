<%@ Page Title="rptKPIsSummaryScore" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="rptKPIsSummaryScore.aspx.vb" Inherits="PTECCENTER.rptKPIsSummaryScore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper" class="h-100">

        <!-- #include virtual ="/include/menu.inc" -->
        <!-- add side menu -->

        <!-- เนื้อหาเว็บ -->
        <div id="content-wrapper">

            <div class="container-fluid">

                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">KPIs Main (ประเมินประจำปี)</a>
                    </li>
                </ol>
                <div class="row">
                    <div class="col-md-3 mb-3">
                        <asp:Button ID="btnExport" class="btn btn-sm btn-success" runat="server" Text="Export CSV"   />
                        &nbsp;           
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 mb-3">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Period</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboPeriod" runat="server" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.container-fluid -->
        </div>
    </div>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <CompositeScript>
            <Scripts>
                <asp:ScriptReference Path="~/datetimepicker/jquery.js" />
                <asp:ScriptReference Path="~/datetimepicker/build/jquery.datetimepicker.full.min.js" />
                <asp:ScriptReference Path="~/js/NonPO.js" />
            </Scripts>
        </CompositeScript>
    </asp:ScriptManager>
    <!-- datetimepicker ต้องไปทั้งชุด-->
<%--    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/NonPO.js")%>"></script>--%>
    <script>
        $(document).ready(function () {
            $('.form-control').selectpicker({
                noneSelectedText: '-',
                liveSearch: true,
                maxOptions: 1
            });
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
