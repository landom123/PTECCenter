<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="SupplyImportWSPrice.aspx.vb" Inherits="PTECCENTER.SupplyImportWSPrice" %>

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
                           <i class="fa fa-tasks" aria-hidden="true"></i> Import Price
                  </li>
                </ol>
                <p></p>
                <div class="row">
                    <div class="col-4" style="text-align:right;color:red">
                        เลือกไฟล์ราคา
                    </div>
                    <div class="col-4">
                      <asp:FileUpload ID="FileUploadPrice" class="form-control" runat="server" accept="GNDITEM.DBF" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-12 ">
                        <asp:Button ID="btnOpen" class="btn btn-sm  btn-primary" runat="server" Text="Preview" />&nbsp;
                        <asp:Button ID="btnImport" class="btn btn-sm  btn-success" runat="server" Text="Import" />  
                    </div>
                </div>
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
	                                    <asp:TemplateField HeaderText="SaleDate">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblsaledate" runat="server" Text='<%#Eval("saledate")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="PriceDate">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblpricedate" runat="server" Text='<%#Eval("pricedate")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Supply">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblsupply" runat="server" Text='<%#Eval("supply")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="G91 Price">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblg91price" runat="server" Text='<%#Eval("g91price")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="G91 Gap">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblg91gap" runat="server" Text='<%#Eval("g91gap")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
		                                <asp:TemplateField HeaderText="G95 Price">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblg95price" runat="server" Text='<%#Eval("g95price")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="G95 Gap">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblg95gap" runat="server" Text='<%#Eval("g95gap")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>      
		                                <asp:TemplateField HeaderText="E20 Price">
		                                    <ItemTemplate>
			                                    <asp:Label id="lble20price" runat="server" Text='<%#Eval("e20price")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="E20 Gap">
		                                    <ItemTemplate>
			                                    <asp:Label id="lble20gap" runat="server" Text='<%#Eval("e20gap")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>         
		                                <asp:TemplateField HeaderText="E85 Price">
		                                    <ItemTemplate>
			                                    <asp:Label id="lble85price" runat="server" Text='<%#Eval("e85price")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="E85 Gap">
		                                    <ItemTemplate>
			                                    <asp:Label id="lble85gap" runat="server" Text='<%#Eval("e85gap")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>      
		                                <asp:TemplateField HeaderText="B5 Price">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblb5price" runat="server" Text='<%#Eval("b5price")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="B5 Gap">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblb5gap" runat="server" Text='<%#Eval("b5gap")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>        
		                                <asp:TemplateField HeaderText="B7 Price">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblb7price" runat="server" Text='<%#Eval("b7price")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="B7 Gap">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblb7gap" runat="server" Text='<%#Eval("b7gap")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>       
		                                <asp:TemplateField HeaderText="B10 Price">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblb10price" runat="server" Text='<%#Eval("b10price")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="B10 Gap">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblb10gap" runat="server" Text='<%#Eval("b10gap")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>    										  
                                    </Columns>
                              </asp:GridView>
                                               
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

</asp:Content>
