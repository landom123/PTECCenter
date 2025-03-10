﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="SupplyWSQuotation.aspx.vb" Inherits="PTECCENTER.SupplyWSQuotation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div id="wrapper" class="h-100">

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
                <div class="row">
                    <div class="col-6">
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-primary" runat="server" Text=" New " />
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text=" Save " />
                        <asp:Button ID="btnConfirm" class="btn btn-sm  btn-success" runat="server" Text=" Confirm " />
                        <asp:Button ID="btnPrint" class="btn btn-sm  btn-success" runat="server" Text=" Print " />
                    </div>
                    <div class="col-6" style="text-align:right">
                        <asp:Button ID="btnCancel" class="btn btn-sm  btn-danger" runat="server" Text=" Cancel " OnClientClick="return cancel_data();"/>
                    </div>
                </div> 
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
			                      <asp:Label id="lblvolume" runat="server" Text='<%#Eval("volume", "{0:n0}")%>'></asp:Label>
		                        </ItemTemplate>
                               <FooterTemplate>
                                  <asp:Label id="lblvolumetotal" runat="server" Text="0"></asp:Label>
                               </FooterTemplate>
	                        </asp:TemplateField>	           
	                        <asp:TemplateField HeaderText="ราคา">
		                        <ItemTemplate>
			                        <asp:Label id="lblprice" runat="server" Text='<%#Eval("price", "{0:n}")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>	
	                        <asp:TemplateField HeaderText="+ค่าคลัง"  Visible="false">
		                        <ItemTemplate>
			                        <asp:Label id="lblterminalmarkup" runat="server" Text='<%#Eval("terminalmarkup")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>	                            
	                        <asp:TemplateField HeaderText="Markup"  Visible="false">
		                        <ItemTemplate>
			                        <asp:Label id="lblmarkup" runat="server" Text='<%#Eval("markup")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>	    
	                        <asp:TemplateField HeaderText="รวม">
		                        <ItemTemplate>
			                        <asp:Label id="lbltotal" runat="server" Text='<%#Eval("total", "{0:n}")%>'></asp:Label>
		                        </ItemTemplate>
                               <FooterTemplate>
                                  <asp:Label id="lblnettotal" runat="server" Text="0"></asp:Label>
                               </FooterTemplate>
	                        </asp:TemplateField>	      
	                        <asp:TemplateField HeaderText="">
		                        <ItemTemplate>
			                      <asp:Button class="btn btn-sm  btn-danger" ID="btnDelRow" runat="server" Text="Del" OnClick="BtnDelRow" />
		                        </ItemTemplate>
	                        </asp:TemplateField>	                             
                        </Columns>
                    </asp:GridView>
                                               
                </div><!-- end Table detail-->

                </div>

                    <div class="row">
                        <div class="col-md-3">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">เลือกผลิตภัณฑ์</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboProduct" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">+ เพิ่ม</span>
                                </div>
                                <asp:textbox class="form-control bg-warning text-dark" ID="txtAdd" runat="server" AutoPostBack="false" ></asp:textbox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ปริมาณสั่งซื้อ (ลิตร)</span>
                                </div>
                                <asp:textbox class="form-control bg-warning text-dark" ID="txtVolume"  runat="server" AutoPostBack="false"></asp:textbox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12" style="text-align:right">
                                <asp:Button ID="btnAddtoTable" class="btn btn-sm  btn-success" runat="server" Text="เพิ่มในตาราง" />  
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
                    <div class="row" runat="server" visible="false">
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
                    <div class="row" runat="server" visible="false">
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
                                <asp:textbox class="form-control bg-warning text-dark" ID="txtremark"  runat="server" AutoPostBack="true"></asp:textbox>
                            </div>
                        </div>
                    </div>
                    <br />
                   <div class="table-responsive">
                        <asp:GridView ID="gvImage"  

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
	                        <asp:TemplateField HeaderText="รูปภาพ">
		                        <ItemTemplate>
                                <a href="<%#Eval("url")%>" target="_blank">
                                    <%#Eval("imagepath")%>
                                </a>

		                        </ItemTemplate>
	                        </asp:TemplateField>	  
	                        <asp:TemplateField HeaderText="">
		                        <ItemTemplate>
			                        <asp:Button class="btn btn-sm  btn-danger" ID="btnDelRow" runat="server" Text="Del" OnClick="BtnDelRow" />
		                        </ItemTemplate>
	                        </asp:TemplateField>	
                        
                        </Columns>
                    </asp:GridView>
                                               
                </div><!-- end Table image detail-->
                <div class="row">
                    <asp:FileUpload ID="FileUpload1" class="btn btn-sm  btn-secondary files" runat="server" text="เลือกไฟล์ " accept="image/*,.pdf" />                                    
                    <asp:Button ID="btnAddImage" class="btn btn-sm  btn-success" runat="server" Text="Add File" />
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

    </div>   <!-- /#wrapper -->
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
