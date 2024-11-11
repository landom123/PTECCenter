<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="SupplyWSQuotationListForAccountant.aspx.vb" Inherits="PTECCENTER.SupplyWSQuotationListForAccountant" %>

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
                           <i class="fa fa-tasks" aria-hidden="true"></i>รายการใบเสนอราคา (สำหรับแผนกบัญชี)
                  </li>
                </ol>
                <div class="row">
                    <div class="col-md-4 mb-3">วันที่เริ่มต้น
                        <asp:textbox class="form-control" ID="txtBegindate" runat="server" AutoPostBack="true"></asp:textbox>
                    </div>
                    <div class="col-md-4 mb-3">วันที่สิ้นสุด
                        <asp:textbox class="form-control" ID="txtEnddate" runat="server" AutoPostBack="true"></asp:textbox>
                    </div>
                    <div class="col-md-4 mb-3">
                        <asp:Button class="btn btn-sm  btn-success" ID="btnFind" runat="server" Text="Find" />
                    </div>
                </div> 
                <br />
                <div class="container">
                    <div class="table-responsive">
                        <asp:GridView ID="gvQuotation"  
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
	                        <asp:TemplateField HeaderText="วันที่">
		                        <ItemTemplate>
			                        <asp:Label id="lblsaledate" runat="server" Text='<%#Eval("saledate")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>	
	                        <asp:TemplateField HeaderText="เลขที่">
		                        <ItemTemplate>
			                        <asp:Label id="lbldocno" runat="server" Text='<%#Eval("docno")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>	           
	                        <asp:TemplateField HeaderText="คลัง">
		                        <ItemTemplate>
			                        <asp:Label id="lblterminal" runat="server" Text='<%#Eval("terminal")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>	    
	                        <asp:TemplateField HeaderText="ลูกค้า">
		                        <ItemTemplate>
			                        <asp:Label id="lblcustomer" runat="server" Text='<%#Eval("customer")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>	 
	                        <asp:TemplateField HeaderText="Volume">
		                        <ItemTemplate>
			                        <asp:Label id="lblvolume" runat="server" Text='<%#Eval("volume")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>	                            
	                        <asp:TemplateField HeaderText="สถานะ">
		                        <ItemTemplate>
			                        <asp:Label id="lblstatus" runat="server" Text='<%#Eval("status")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>	      
                           
	                        <asp:TemplateField HeaderText="">
		                        <ItemTemplate>
			                      <asp:HyperLink ID="HyperLink" runat="server" NavigateUrl='<%#Eval("link")%>' Text="" data-toggle="tooltip" data-placement="right" title="View Detail"><i class="fa fa-eye" aria-hidden="true"></i></asp:HyperLink>
		                        </ItemTemplate>
	                        </asp:TemplateField>	                             
                        </Columns>
                    </asp:GridView>
                                               
                </div><!-- end Table detail-->

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
  <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>

    <script type="text/javascript">
        jQuery('[id$=txtBegindate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: true,
            scrollInput: false,
            format:'d/m/Y'
        });
    </script>
    <script type="text/javascript">
        jQuery('[id$=txtEnddate]').datetimepicker({
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
