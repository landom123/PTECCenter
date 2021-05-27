<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="TTCosttoD365.aspx.vb" Inherits="PTECCENTER.TTCostToD365" %>

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
                           <i class="fa fa-tasks" aria-hidden="true"></i> TT Cost to D365
                  </li>
                </ol>

                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">วันที่ Invoice </span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtCloseDate" runat="server"></asp:TextBox>
                          &nbsp;<asp:Button ID="btnFind" class="btn btn-sm  btn-primary" runat="server" Text="Find" />&nbsp;                          
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">ระบุ Due ชำระ</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtDueDate" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-4 text-right">
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text="Save and Download" />  
                    </div>
                </div>

                <div class="card-body">
<%--                    <div class="col-12 text-right">
                            <asp:Button ID="btnselectall" class="btn btn-sm  btn-success" runat="server" Text="Select All" />&nbsp;  
                            <asp:Button ID="btnunselect" class="btn btn-sm  btn-danger" runat="server" Text="Un Select All" />  
                    </div>--%>

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
<%--	                                    <asp:TemplateField HeaderText="Type">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblbillingtype" runat="server" Text='<%#Eval("billing_type")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>--%>
	                                    <asp:TemplateField HeaderText="ขนส่ง">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblsupplier" runat="server" Text='<%#Eval("suppliername")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Invoice No">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblinvoiceno" runat="server" Text='<%#Eval("invoice_no")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Shipment">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblshipment" runat="server" Text='<%#Eval("shipment")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
<%--	                                    <asp:TemplateField HeaderText="Invoice Date">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblinvoicedate" runat="server" Text='<%#Eval("invoice_date", "{0: dd/MM/yyyy}")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>--%>
	                                    <asp:TemplateField HeaderText="Due Date">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblduedate" runat="server" Text='<%#Eval("due_date", "{0:dd/MM/yyyy}")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ต้นทาง">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblterminal" runat="server" Text='<%#Eval("terminal")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ปลายทาง">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblshipto" runat="server" Text='<%#Eval("shipto")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ทะเบียนรถ">
		                                    <ItemTemplate>
			                                    <asp:Label id="lbltruck" runat="server" Text='<%#Eval("truck")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ระยะทาง">
		                                    <ItemTemplate>
			                                    <asp:Label id="lbldistance" runat="server" Text='<%#Eval("distance")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Rate">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblrate" runat="server" Text='<%#Eval("tt_cost_rate", "{0:N4}")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Volume">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblvolume" runat="server" Text='<%#Eval("net_volume", "{0:N2}")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ค่าขนส่ง">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblamount" runat="server" Text='<%#Eval("ttcost", "{0:N2}")  %>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
<%--                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>                          
                                                <asp:HyperLink ID="HyperLink" runat="server" NavigateUrl='<%#Eval("link")%>' Text="" data-toggle="tooltip" data-placement="right" title="View Detail"><i class="fa fa-eye" aria-hidden="true"></i></asp:HyperLink>
                                                <asp:checkbox id="chk" runat="server" checked='<%#Eval("chk")%>'></asp:checkbox>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                              </asp:GridView>
                                               
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
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>

    <script type="text/javascript">
        jQuery('[id$=txtCloseDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format:'d/m/Y'
        });
    </script>
    <script type="text/javascript">
        jQuery('[id$=txtDueDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>

</asp:Content>
