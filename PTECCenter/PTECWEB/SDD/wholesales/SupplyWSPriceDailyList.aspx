<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="SupplyWSPriceDailyList.aspx.vb" Inherits="PTECCENTER.SupplyWSPriceDailyList" %>

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
                           <i class="fa fa-tasks" aria-hidden="true"></i>รายการราคาขายส่งประจำวัน
                  </li>
                </ol>
                <div class="row">
                    <div class="col-4">เลือกวันที่
                        <asp:textbox class="form-control" ID="txtPriceDate" runat="server" AutoPostBack="true"></asp:textbox>
                    </div>
                    <div class="col-4">ถึงวันที่
                        <asp:textbox class="form-control" ID="txtEnddate" runat="server" AutoPostBack="true"></asp:textbox>
                    </div>
                    <div class="col-4">
                        <asp:Button class="btn btn-sm  btn-success" ID="btnFind" runat="server" Text="Find" />
                    </div>
                </div> 
                <br />
                <div class="container">
                    <div class="table-responsive">
                        <asp:GridView ID="gvPrice"  
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
			                        <asp:Label id="lblpricedate" runat="server" Text='<%#Eval("pricedate")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>	
	                        <asp:TemplateField HeaderText="คลัง">
		                        <ItemTemplate>
			                        <asp:Label id="lblTerminal" runat="server" Text='<%#Eval("terminal")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>	           
	                        <asp:TemplateField HeaderText="B5">
		                        <ItemTemplate>
			                        <asp:Label id="lblB5" runat="server" Text='<%#Eval("b5")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>	    
	                        <asp:TemplateField HeaderText="B7">
		                        <ItemTemplate>
			                        <asp:Label id="lblB7" runat="server" Text='<%#Eval("b7")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>	    
	                        <asp:TemplateField HeaderText="B10">
		                        <ItemTemplate>
			                        <asp:Label id="lblB10" runat="server" Text='<%#Eval("b10")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>	      
	                        <asp:TemplateField HeaderText="GAS91">
		                        <ItemTemplate>
			                        <asp:Label id="lblG91" runat="server" Text='<%#Eval("G91")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>	   
	                        <asp:TemplateField HeaderText="GAS95">
		                        <ItemTemplate>
			                        <asp:Label id="lblG95" runat="server" Text='<%#Eval("G95")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>	      
	                        <asp:TemplateField HeaderText="E20">
		                        <ItemTemplate>
			                        <asp:Label id="lblE20" runat="server" Text='<%#Eval("e20")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>	     
	                        <asp:TemplateField HeaderText="E85">
		                        <ItemTemplate>
			                        <asp:Label id="lblE85" runat="server" Text='<%#Eval("e85")%>'></asp:Label>
		                        </ItemTemplate>
	                        </asp:TemplateField>	   
	                        <asp:TemplateField HeaderText="Status">
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
