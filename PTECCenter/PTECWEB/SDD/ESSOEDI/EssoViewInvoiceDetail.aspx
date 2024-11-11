<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="EssoViewInvoiceDetail.aspx.vb" Inherits="PTECCENTER.EssoViewInvoiceDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
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
                           <i class="fa fa-tasks" aria-hidden="true"></i> Invoice
                  </li>
                </ol>
                <p></p>
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">Billing Type</span>
                          </div>
                            <asp:Label class="form-control" ID="lblbillingtype" runat="server" ReadOnly="true"></asp:Label>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">Invoice No</span>
                          </div>
                            <asp:Label class="form-control" ID="lblinvoiceno" runat="server" ReadOnly="true"></asp:Label>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">Invoice Date</span>
                          </div>
                            <asp:Label class="form-control" ID="lblinvoicedate" runat="server" ReadOnly="true"></asp:Label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">Due Date</span>
                          </div>
                            <asp:Label class="form-control" ID="lblduedate" runat="server" ReadOnly="true"></asp:Label>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">Sale Order No</span>
                          </div>
                            <asp:Label class="form-control" ID="lblsono" runat="server" ReadOnly="true"></asp:Label>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">Delivery No</span>
                          </div>
                            <asp:Label class="form-control" ID="lbldeliveryno" runat="server" ReadOnly="true"></asp:Label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">Ship From</span>
                          </div>
                            <asp:Label class="form-control" ID="lblshipfrom" runat="server" ReadOnly="true"></asp:Label>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">Amount (Inc. VAT)</span>
                          </div>
                            <asp:Label class="form-control" ID="lblinvoiceamount" runat="server" ReadOnly="true"></asp:Label>
                        </div>
                    </div>

                </div>

                          <div class="card-body">
                              <div class="table-responsive">
                                <asp:GridView ID="gvData"  
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    emptydatatext="No data available." 
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#CCCCFF" />

                                      <Columns>
	                                    <asp:TemplateField HeaderText="Ship to">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblshipto" runat="server" Text='<%#Eval("shipto")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Sale Order">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblsaleorder" runat="server" Text='<%#Eval("so_no")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Mat Code">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblmatcode" runat="server" Text='<%#Eval("mat_code")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Product">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblproduct" runat="server" Text='<%#Eval("product")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Volume">
		                                    <ItemTemplate>
			                                    <asp:Label id="lbldovolume" runat="server" Text='<%#Eval("do_volume")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Unit Price">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblunitprice" runat="server" Text='<%#Eval("unit_price")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Amount (Exc. VAT">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblitemnetvalue" runat="server" Text='<%#Eval("item_net_value")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Ref">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblref" runat="server" Text='<%#Eval("ref_invoice_no")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
                                    </Columns>
                              </asp:GridView>
                                               
                            </div>
                          </div>

                <ol class="breadcrumb"style="background-color:deeppink;color:white">
                  <li class="breadcrumb-item" >
                           <i class="fa fa-tasks" aria-hidden="true"></i> การแก้ไขข้อมูล
                  </li>
                </ol>
                <p></p>
                <asp:Button ID="btnremove" class="btn btn-sm  btn-success" runat="server" Text="ลบรายการที่เลือก" />
                <div class="card-body">
                    <div class="table-responsive">
                    <asp:GridView ID="dgv2"  
                        class="table table-striped table-bordered" 
                        AllowSorting="True" 
                        allowpaging="false"
                        AutoGenerateColumns="false" 
                        emptydatatext="No data available." 
                        runat="server" CssClass="table table-striped">
                        <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                        <AlternatingRowStyle BackColor="#CCCCFF" />

                            <Columns>
	                        <asp:TemplateField HeaderText="ID">
		                        <ItemTemplate>
			                        <asp:Label id="lblid" runat="server" Text='<%#Eval("id")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>
	                        <asp:TemplateField HeaderText="Mat Code">
		                        <ItemTemplate>
			                        <asp:Label id="lblmatcode" runat="server" Text='<%#Eval("matcode")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>
	                        <asp:TemplateField HeaderText="Sale Order">
		                        <ItemTemplate>
			                        <asp:Label id="lblsono" runat="server" Text='<%#Eval("sono")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>
	                        <asp:TemplateField HeaderText="Ship to">
		                        <ItemTemplate>
			                        <asp:Label id="lblshipto" runat="server" Text='<%#Eval("shipto")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>
	                        <asp:TemplateField HeaderText="Ship to">
		                        <ItemTemplate>
			                        <asp:Label id="lblshipname" runat="server" Text='<%#Eval("shipname")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>
	                        <asp:TemplateField HeaderText="Volume">
		                        <ItemTemplate>
			                        <asp:Label id="lbldovolume" runat="server" Text='<%#Eval("volume")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>
	                        <asp:TemplateField HeaderText="Volume">
		                        <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" />			                        
		                        </ItemTemplate>
	                        </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                        <br />
                <div class="row">
                    <div class="col">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">Product</span>
                          </div>
                            <asp:dropdownlist class="form-control" ID="cbomatcode" runat="server"></asp:dropdownlist>
                        </div>
                    </div>
                    <div class="col">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">Sale Order</span>
                          </div>
                            <asp:dropdownlist class="form-control" ID="cboSono" runat="server"></asp:dropdownlist>
                        </div>
                    </div>
                    <div class="col">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">Ship to</span>
                          </div>
                            <asp:dropdownlist class="form-control" ID="cboshipto" runat="server"></asp:dropdownlist>
                        </div>
                    </div>
                    <div class="col">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">ปริมาณ</span>
                          </div>
                            <asp:textbox class="form-control" ID="txtvolume" runat="server"></asp:textbox>
                            <asp:Button ID="btnAdd" class="btn btn-sm  btn-success" runat="server" Text="Add" />  
                        </div>
                    </div>
                </div>    
                         
                </div>
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
    <!-- /#wrapper -->



</asp:Content>
