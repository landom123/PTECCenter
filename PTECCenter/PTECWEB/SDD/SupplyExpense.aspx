<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="SupplyExpense.aspx.vb" Inherits="PTECCENTER.SupplyExpense" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .input-group {
            margin-bottom: 1rem;
        }
    </style>
    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper" class="h-100">

        <!-- #include virtual ="/include/menu.inc" -->
        <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">
                <ol class="breadcrumb" style="background-color: deeppink; color: white">
                    <li class="breadcrumb-item">บันทึกค่าใช้จ่ายอื่น (Supply)
                    </li>
                </ol>

                <div class="row">
                    <div class="col-md-8 mb-3">
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-primary" runat="server" Text=" New " />
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text=" Save " />
                        <asp:Button ID="btnConfirm" class="btn btn-sm  btn-success" runat="server" Text=" Confirm " />
                        <asp:Button ID="btnCancel" class="btn btn-sm  btn-danger" runat="server" Text=" Cancel " />
                    </div>
                    <div class="col-md-4 mb-3 text-right">
                        <asp:Label ID="lblstatus" class="btn btn-sm  btn-success" runat="server" Text="สถานะ"></asp:Label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-4 col-md-6">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">เลขที่เอกสาร</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtDocNo" runat="server"></asp:TextBox>
                            <div class="input-group-append">
                                <asp:Button ID="btnFind" class="btn btn-sm  btn-success" runat="server" Text=" Find " />
                            </div>
                        </div>

                    </div>
                    <div class="col-lg-4 col-md-6">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">วันที่</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtDocDate" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-6">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ผู้บันทึก</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtCreateBy" runat="server" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-6">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Supplier</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboSupplier"
                                runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>

                </div>

                <div class="card-body">
                    <div class="table-responsive">

                        <table class="table table-bordered " id="dataTable">
                            <thead class="table-info">
                                <tr>
                                    <th style="width: 200px;">Sale Order</th>
                                    <th style="width: 300px;">รายการ</th>
                                    <th style="width: 200px;">จำนวนเงิน</th>
                                    <th style="width: 120px;"></th>
                                </tr>
                            </thead>
                            <tbody>
                                <% For i = 0 To details.Rows.Count - 1 %>
                                <tr>
                                    <td><% =details.Rows(i).Item("saleorder") %></td>
                                    <td><% =details.Rows(i).Item("expense") %></td>
                                    <td><% =Double.Parse(details.Rows(i).Item("amount")).ToString("N02") %></td>
                                    <td>
                                        <asp:Button ID="btnDel" class="btn btn-sm  btn-danger" runat="server" Text=" - " />
                                    </td>
                                </tr>
                                <% Next i %>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="cboSaleOrder" class="form-control" runat="server"></asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboExpense" class="form-control" runat="server"></asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox class="form-control" ID="txtAmount" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAdd" class="btn btn-sm  btn-success" runat="server" Text=" + " OnClientClick="validateData()" />


                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <%--   status,detail--%>
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
        jQuery('[id$=txtDocDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
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
    <!--end datetimepicker ต้องไปทั้งชุด-->


    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>
    <script type="text/javascript">
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
    <script type="text/javascript">
        $(document).ready(function () {
            $('.form-control').selectpicker({
                liveSearch: true,
                maxOptions: 1
            });
        });
    </script>
</asp:Content>
