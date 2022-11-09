<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="SupplyWSQuotationForAccountant.aspx.vb" Inherits="PTECCENTER.SupplyWSQuotationForAccountant" %>

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
                           <a href="SupplyWSQuotationList.aspx"><i class="fa fa-tasks" aria-hidden="true"></i></a>&nbsp; ใบเสนอราคาขายส่ง
                  </li>
                </ol>
<%--                <div class="row">
                    <div class="col-6">
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-primary" runat="server" Text=" New " />
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text=" Save " />
                        <asp:Button ID="btnConfirm" class="btn btn-sm  btn-success" runat="server" Text=" Confirm " />
                    </div>
                    <div class="col-6" style="text-align:right">
                        <asp:Button ID="btnCancel" class="btn btn-sm  btn-danger" runat="server" Text=" Cancel " OnClientClick="return cancel_data();"/>
                    </div>
                </div> --%>
                <div class="container">
                    <div class="row">
                        <div class="col-md-4"><asp:label ID="lblstatus" class="btn btn-sm  btn-danger" runat="server"></asp:label></div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">เลขที่เอกสาร</span>
                                </div>
                                <asp:label class="form-control" ID="lblDocNo" runat="server"></asp:label>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">วันที่ทำรายการ</span>
                                </div>
                                <asp:textbox class="form-control" ID="txtDocDate" runat="server" readonly="true"></asp:textbox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">วันที่ขาย</span>
                                </div>
                                <asp:textbox class="form-control" ID="txtSaledate" runat="server" AutoPostBack="true"></asp:textbox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">คลัง</span>
                                </div>
                                <asp:label class="form-control" ID="lblTerminal" runat="server" AutoPostBack="true"></asp:label>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ลูกค้า</span>
                                </div>
                                <asp:label class="form-control" ID="lblcustomer" runat="server" AutoPostBack="true"></asp:label>
                            </div>
                        </div>


                    </div>
                    <br />
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
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ค่าคอมมิสชั่น/ลิตร</span>
                                </div>
                                <asp:label class="form-control" ID="lblCommission" runat="server" AutoPostBack="true"></asp:label>
                            </div>
                        </div>
                    </div>
                    <br />
                <div class="container">
                    <div class="table-responsive">
                        <asp:GridView ID="gvSaleitem"  
                            OnDataBound = "OnDataBound"
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                            showfooter="true"
                            ShowHeaderWhenEmpty="True"
                            ShowFooterWhenEmpty="true"
                                    runat="server" CssClass="table table-striped">
                        <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                        <AlternatingRowStyle BackColor="#CCCCFF" />

                        <Columns>
	                        <asp:TemplateField HeaderText="ผลิตภัณฑ์">
		                        <ItemTemplate>
			                        <asp:Label id="lblproduct" runat="server" Text='<%#Eval("product")%>'></asp:Label>
		                        </ItemTemplate>
                               <FooterTemplate>
                                  <asp:Label id="lblcaption" runat="server" Text="รวม"></asp:Label>
                               </FooterTemplate>
	                        </asp:TemplateField>	
	                        <asp:TemplateField HeaderText="ลิตร">
		                        <ItemTemplate>
			                        <asp:Label id="lblvolume" runat="server" Text='<%#Eval("volume")%>'></asp:Label>
		                        </ItemTemplate>
                               <FooterTemplate>
                                  <asp:Label id="lblvolumetotal" runat="server" Text=""></asp:Label>
                               </FooterTemplate>
	                        </asp:TemplateField>	           
	                        <asp:TemplateField HeaderText="ราคา">
		                        <ItemTemplate>
			                        <asp:Label id="lblprice" runat="server" Text='<%#Eval("price")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>	
	                        <asp:TemplateField HeaderText="+ค่าคลัง">
		                        <ItemTemplate>
			                        <asp:Label id="lblterminalmarkup" runat="server" Text='<%#Eval("terminalmarkup")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>	                            
	                        <asp:TemplateField HeaderText="Markup">
		                        <ItemTemplate>
			                        <asp:Label id="lblmarkup" runat="server" Text='<%#Eval("markup")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>	    
	                        <asp:TemplateField HeaderText="รวม">
		                        <ItemTemplate>
			                        <asp:Label id="lbltotal" runat="server" Text='<%#Eval("total")%>'></asp:Label>
		                        </ItemTemplate>
                               <FooterTemplate>
                                  <asp:Label id="lblnettotal" runat="server" Text=""></asp:Label>
                               </FooterTemplate>
	                        </asp:TemplateField>	      
                            
                        </Columns>
                    </asp:GridView>
                                               
                </div><!-- end Table detail-->

                </div>

                    <div class="row">
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ค่าน้ำมันสุทธิ</span>
                                </div>
                                <asp:label class="form-control" ID="lblOilPrice" runat="server" ></asp:label>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ค่าขนส่งสุทธิ</span>
                                </div>
                                <asp:label class="form-control" ID="lblNetTTCost" runat="server" ></asp:label>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ค่าคอมมิสชั่นสุทธิ</span>
                                </div>
                                <asp:label class="form-control" ID="lblNetCommission" runat="server" ></asp:label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ราคาขายสุทธิ/ลิตร</span>
                                </div>
                                <asp:label class="form-control" ID="lblTotalPerLitre" runat="server"></asp:label>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ราคาขายสุทธิ (ที่ต้องชำระ)</span>
                                </div>
                                <asp:label class="form-control" ID="lblTotal" runat="server" ></asp:label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">หมายเหตุ</span>
                                </div>
                                <asp:label class="form-control " ID="lblRemark"  runat="server" ></asp:label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card">
                    <div class="card-header" style="background-color: navy; color: white">
                        <i class="fas fa-table"></i>
                        สำหรับบัญชี
                    </div>
                    <div id="accountant" class="card-body">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">วันที่บันทึก</span>
                                    </div>
                                    <asp:label class="form-control " ID="lblFinanceDate"  runat="server"  ></asp:label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">ผู้บันทึก</span>
                                    </div>
                                    <asp:label class="form-control " ID="lblCreateBy"  runat="server"  ></asp:label>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">วันที่รับชำระ</span>
                                    </div>
                                    <asp:textbox class="form-control " ID="txtPaymentdate"  runat="server" style="background-color:cornsilk" ></asp:textbox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">ยอดเงิน Credit (ถ้ามี)</span>
                                    </div>
                                    <asp:textbox class="form-control " ID="txtCreditAmount"  runat="server" style="background-color:cornsilk" ></asp:textbox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">ยอดเงินรับชำระ</span>
                                    </div>
                                    <asp:textbox class="form-control " ID="txtPaymentAmount"  runat="server" style="background-color:cornsilk" ></asp:textbox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">หมายเหตุ</span>
                                    </div>
                                    <asp:textbox class="form-control " ID="txtRemark"  runat="server" TextMode="MultiLine" style="background-color:cornsilk" ></asp:textbox>
                                </div>
                            </div>
                        </div><br />
                        <div class="row">
                            <div class="col-6">
                                <asp:Button ID="btnSave" class="btn btn-sm  btn-primary" runat="server" Text=" Save " />
                                <asp:Button ID="btnConfirm" class="btn btn-sm  btn-success" runat="server" Text=" Confirm " />
                            </div>
                        </div>
                    </div>
                    <!-- end jobdetail -->

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
        jQuery('[id$=txtSaledate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: true,
            scrollInput: false,
            format:'d/m/Y H:i'
        });
    </script>

      <script type="text/javascript">
          jQuery('[id$=txtPaymentdate]').datetimepicker({
              startDate: '+1971/05/01',//or 1986/12/08
              timepicker: true,
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
