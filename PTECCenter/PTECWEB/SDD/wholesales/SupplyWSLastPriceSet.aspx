<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="SupplyWSLastPriceSet.aspx.vb" Inherits="PTECCENTER.SupplyWSLastPriceSet" %>

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
                           <i class="fa fa-tasks" aria-hidden="true"></i>ตรวจสอบตั้งราคาขายส่ง ย้อนหลัง
                  </li>
                </ol>
                <div class="row">  
                    <div class="col-md-6 mb-3">
             <%--           <asp:Button ID="btnNew" class="btn btn-sm  btn-primary" runat="server" Text=" New " />
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text=" Save " />
                        <asp:Button ID="btnConfirm" class="btn btn-sm  btn-success" runat="server" Text=" Confirm " />
                        <asp:Button ID="btnLastPrice" class="btn btn-sm  btn-success" runat="server" Text=" Last Price " />--%>
                    </div>                    
                    <div class="col-md-6 mb-3" style="text-align: right">
                        <asp:Button ID="btnBack" class="btn btn-sm  btn-danger" runat="server" Text=" กลับ " />
                    </div>
                </div> 
                                
            </div>            <!-- /.container-fluid t> -->

                    <div class="row" style="padding-top: 0.2rem;">
                        <div class="col-md-4 mb-3">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">วันที่</span>
                              
                                <asp:textbox class="form-control" ID="txtBeginPriceDate" style="background-color:white" runat="server" ></asp:textbox>
                                 </div>
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ถึงวันที่</span>
                                </div>
                                <asp:textbox class="form-control" ID="txtEndPriceDate" style="background-color:white" runat="server" ></asp:textbox>
                                <asp:Button ID="btnFind" class="btn btn-sm  btn-danger" runat="server" Text=" Find " />
                            </div>
                        </div>

                      <%--  <div class="col-2">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                <span class="input-group-text">สัญญามีผลวันที่</span>
                                </div>
                                <asp:TextBox class="form-control" ID="txtBegindate" style="background-color:white" runat="server"></asp:TextBox>    

                            </div>
                        </div>--%>
                        
                    </div>
            <br />
            <div class="table-responsive">
            
                <asp:gridview id="gvPrice" 
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    emptydatatext="No data available." 
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#CCCCFF" />
                    <Columns>                                                                        
	                                    <asp:TemplateField HeaderText="Date" >
		                                    <ItemTemplate>
			                                    <asp:Label id="lblpricedate" runat="server" Text='<%#Eval("pricedate")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Name">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblName" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Status">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="DIESEL B5">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblb5price" runat="server" Text='<%#Eval("b5price")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="DIESEL B7">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblb7price" runat="server" Text='<%#Eval("b7price")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="DIESEL B10">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblb10price" runat="server" Text='<%#Eval("b10price")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="GAS 95">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblg95price" runat="server" Text='<%#Eval("g95price")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="GAS 91" >
		                                    <ItemTemplate>
			                                    <asp:Label id="lblg91price" runat="server" Text='<%#Eval("g91price")%>' ></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="E20">
		                                    <ItemTemplate>
			                                    <asp:Label id="lble20price" runat="server" Text='<%#Eval("e20price")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="E85">
		                                    <ItemTemplate>
			                                    <asp:Label id="lble85price" runat="server" Text='<%#Eval("e85price")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="remark">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblremark" runat="server" Text='<%#Eval("remark")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
                                    

                    </Columns>
                </asp:gridview>     
                
            </div>

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
        <!-- Modal -->
        <div class="modal fade" id="exampleModalLong" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
          <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLongTitle">Modal title</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                ...
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                <button type="button" class="btn btn-primary">Save changes</button>
              </div>
            </div>
          </div>
        </div>

    </div>
    <!-- /#wrapper -->
  <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>

    <script type="text/javascript">
        jQuery('[id$=txtBeginPriceDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: true,
            scrollInput: true,
            format:'d/m/Y'
        });
    </script>
    <script type="text/javascript">
        jQuery('[id$=txtEndPriceDate]').datetimepicker({
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

     <script src="//ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
     <script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.8.18/jquery-ui.min.js"></script>

</asp:Content>
