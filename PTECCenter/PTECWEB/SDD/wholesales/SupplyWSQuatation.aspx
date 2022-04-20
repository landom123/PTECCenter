<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="SupplyWSQuatation.aspx.vb" Inherits="PTECCENTER.SupplyWSQuatation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div id="wrapper">

    <!-- #include virtual ="/include/menu.inc" --> <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">    
                <!-- Breadcrumbs-->
                <ol class="breadcrumb"style="background-color:deeppink;color:white">
                  <li class="breadcrumb-item" >
                           <i class="fa fa-tasks" aria-hidden="true"></i> คำนวนราคาขายส่ง
                  </li>
                </ol>

                <div class="container">

                    <div class="row">
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">วันที่ขาย</span>
                                </div>
                                <asp:textbox class="form-control" ID="txtSaleDate" runat="server" AutoPostBack="true"></asp:textbox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">วันที่ เวลา ราคามีผล</span>
                                </div>
                                <asp:textbox class="form-control" ID="txtPriceDate" runat="server" AutoPostBack="true"></asp:textbox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">เลือกคลัง</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboTerminal" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">เลือกลูกค้า</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboCustomer" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">เลือกผลิตภัณฑ์</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboProduct" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>

                    </div>
                    <br />
                    <div class="row">
                        <div class="col-12">
                            <asp:Button ID="btnClear" class="btn btn-sm  btn-primary" runat="server" Text="Clear" />&nbsp;
                            <asp:Button ID="btnCalc" class="btn btn-sm  btn-success" runat="server" Text="คำนวน" />  
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ราคาตามตาราง/ลิตร</span>
                                </div>
                                <asp:label class="form-control" ID="lblPrice" runat="server" AutoPostBack="true"></asp:label>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">+ เพิ่ม</span>
                                </div>
                                <asp:textbox class="form-control bg-warning text-dark" ID="txtAdd" runat="server" AutoPostBack="false" ></asp:textbox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ระยะทาง</span>
                                </div>
                                <asp:label class="form-control" ID="lblDistanct" runat="server" AutoPostBack="true"></asp:label>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ค่าขนส่ง/ลิตร</span>
                                </div>
                                <asp:label class="form-control" ID="lblTTCost" runat="server" AutoPostBack="true"></asp:label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ค่าคอมมิสชั่น/ลิตร</span>
                                </div>
                                <asp:label class="form-control" ID="lblCommission" runat="server" AutoPostBack="true"></asp:label>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ปริมาณสั่งซื้อ (ลิตร)</span>
                                </div>
                                <asp:textbox class="form-control bg-warning text-dark" ID="txtVolume"  runat="server" AutoPostBack="true"></asp:textbox>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-12">
                            <asp:Button ID="btnCalcPrice" class="btn btn-sm  btn-success" runat="server" Text="คำนวนค่าน้ำมัน" />  
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ค่าน้ำมันสุทธิ</span>
                                </div>
                                <asp:label class="form-control" ID="lblOilPrice" runat="server" AutoPostBack="true"></asp:label>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ค่าขนส่งสุทธิ</span>
                                </div>
                                <asp:label class="form-control" ID="lblNetTTCost" runat="server" AutoPostBack="true"></asp:label>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ค่าคอมมิสชั่นสุทธิ</span>
                                </div>
                                <asp:label class="form-control" ID="lblNetCommission" runat="server" AutoPostBack="true"></asp:label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ราคาขายสุทธิ/ลิตร</span>
                                </div>
                                <asp:label class="form-control" ID="lblTotalPerLitre" runat="server" AutoPostBack="true"></asp:label>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ราคาขายสุทธิ</span>
                                </div>
                                <asp:label class="form-control" ID="lblTotal" runat="server" AutoPostBack="true"></asp:label>
                            </div>
                        </div>
                    </div>
                </div>

            </div>            <!-- /.container-fluid -->
            <!-- Sticky Footer -->
            <footer class="sticky-footer">
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
    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>

    <script type="text/javascript">
        jQuery('[id$=txtPriceDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: true,
            scrollInput: false,
            format:'d/m/Y H:i'
        });
    </script>

    <script type="text/javascript">
        jQuery('[id$=txtSaleDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>
    
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

<%--    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtVolume").focusout(function () {
                $("lblTotal").val("test")
            });
        });


        function calcPrice() {
            let volume = document.getElementById("<%= txtVolume.ClientID%>").value;

            const total = document.getElementById("lblTotal");
            total.innerHTML = volume.value ;
        }
    </script>--%>



</asp:Content>
