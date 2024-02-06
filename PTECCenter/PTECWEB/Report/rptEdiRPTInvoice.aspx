<%@ Page Title="rptEdiRPTInvoice" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="rptEdiRPTInvoice.aspx.vb" Inherits="PTECCENTER.rptEdiRPTInvoice" %>

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
                        <a href="#">EDI Invoice</a>
                    </li>
                </ol>
                <div class="row">
                    <div class="col-md-12 mb-3">
                        <asp:Button ID="btnSearch" class="btn btn-sm btn-success" runat="server" Text="Search" />&nbsp;   
                         <asp:Button ID="btnClear" class="btn btn-sm btn-secondary" runat="server" Text="Clear" />&nbsp;
                        <asp:Button ID="btnExport" class="btn btn-sm btn-success" runat="server" Text="Export" />
                        &nbsp;           
                    </div>
                </div>

                <div class="row">
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
                <div class="card-body">
                    <div class="table-responsive overflow-auto" style="font-size: 0.9rem">
                        <asp:GridView ID="gvRemind"
                            class="table table-striped table-bordered"
                            AutoGenerateColumns="false"
                            EmptyDataText="No data available."
                            PageSize="20"
                            AllowPaging="true"
                            runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="billing_type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcode" runat="server" Text='<%#Eval("billing_type")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="invoice_no">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranch" runat="server" Text='<%#Eval("invoice_no")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="shipment">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranch" runat="server" Text='<%#Eval("shipment")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="invoice_date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbljobdate" runat="server" Text='<%#Eval("invoice_date")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="loading_date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbljobtype" runat="server" Text='<%#Eval("loading_date")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="due_date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldetails" runat="server" Text='<%#Eval("due_date")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="net_value">
                                    <ItemTemplate>
                                        <asp:Label ID="lblastupdate" runat="server" Text='<%#Eval("net_value")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="vat">
                                    <ItemTemplate>
                                        <asp:Label ID="lbdetailFollow" runat="server" Text='<%#Eval("vat")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="total_value">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("total_value")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
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
