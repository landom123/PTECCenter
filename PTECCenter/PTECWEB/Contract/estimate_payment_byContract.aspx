<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="estimate_payment_byContract.aspx.vb" Inherits="PTECCENTER.estimate_payment_byContract" %>

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
                           <a href="projectlist.aspx"><i class="fa fa-tasks" aria-hidden="true"></i></a> ประมาณการค่าใช้จ่าย ตลอดอายุสัญญา
                  </li>
                </ol>
                <p></p>
                <div class="card-body">
                    <div class="row">
                        <div class="col-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                <span class="input-group-text">เลขที่สัญญาในระบบ</span>
                                </div>
                                <asp:TextBox class="form-control" ID="txtContractNo" placeholder="please save first" ReadOnly="true" runat="server" ></asp:TextBox>    

                            </div>
                        </div>
                        <div class="col-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                <span class="input-group-text">ประมาณการยอดขายลิตร</span>
                                </div>
                                <asp:TextBox class="form-control" ID="txtVolume"  runat="server" text="0"></asp:TextBox>    

                            </div>
                        </div>                      
                    </div>
                    
                </div>
                <div class="card-body">
<%--                    <div class="col-12 text-right">
                            <asp:Button ID="btnselectall" class="btn btn-sm  btn-success" runat="server" Text="Select All" />&nbsp;  
                            <asp:Button ID="btnunselect" class="btn btn-sm  btn-danger" runat="server" Text="Un Select All" />  
                    </div>--%>

                              <div class="table-responsive">
                                   <div class="row">
                                        <div class="col-8" style="font-size:large;">
                                            ค่าใช้จ่ายแบบจ่ายครั้งเดียว                                          
                                        </div>
                                    </div>
                                <asp:GridView ID="gvOneTime"  
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    emptydatatext=" ไม่มีข้อมูล " 
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#CCCCFF" />
                                      <Columns>
	                                    <asp:TemplateField HeaderText="Type">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblpaymenttype" runat="server" Text='<%#Eval("paymenttype")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Due">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblduedate" runat="server" Text='<%#Eval("duedate")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Amount">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblamount" runat="server" Text='<%#Eval("amount")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
                                    </Columns>
                              </asp:GridView>
                                               
                            </div><!-- end Table paymen flexible-->


                              <div class="table-responsive">
                                   <div class="row">
                                        <div class="col-8" style="font-size:large;">
                                            ผลตอบแทนแบบ Flexible                                           
                                        </div>
                                    </div>
                                <asp:GridView ID="gvPaymentFlex"  
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    emptydatatext=" ไม่มีข้อมูล " 
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#00cc66" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#ccffcc" />

                                      <Columns>
	                                    <asp:TemplateField HeaderText="Type">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblpaymenttype" runat="server" Text='<%#Eval("paymenttype")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Due">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblduedate" runat="server" Text='<%#Eval("duedate")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Amount">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblamount" runat="server" Text='<%#Eval("amount")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
                                    </Columns>
                              </asp:GridView>
                                               
                            </div><!-- end Table paymen flexible-->

                              <div class="table-responsive">
                                   <div class="row">
                                        <div class="col-4" style="font-size:large;">
                                            ผลตอบแทนแบบ Fix                                           
                                        </div>
                                    </div>
                                <asp:GridView ID="gvPaymentFix"  
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    emptydatatext=" ไม่มีข้อมูล " 
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#CCCCFF" />
                                    <Columns>
	                                    <asp:TemplateField HeaderText="Type">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblpaymenttype" runat="server" Text='<%#Eval("paymenttype")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Due">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblduedate" runat="server" Text='<%#Eval("duedate")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Amount">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblamount" runat="server" Text='<%#Eval("amount")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
                                    </Columns>
                              </asp:GridView>
                                               
                            </div><!-- end Table payment fix-->

                          </div><!-- end card-body-->

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
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>

    <script type="text/javascript">
        jQuery('[id$=txtContractActiveDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format:'d/m/Y'
        });
    </script>

    <script type="text/javascript">
        jQuery('[id$=txtContractDate]').datetimepicker({
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


    <script type="text/javascript" language="javascript"> 
        function alertWarning(massage) {
            Swal.fire(
                massage,
                '',
                'warning'
            )
        }

    </script>

    <script type="text/javascript" language="javascript"> 
        function confirm_data() {
            if (confirm("Confirm Data (yes/no) ?") == true)
                return true;
            else
                return false;
        }
    </script>

    <script type="text/javascript" language="javascript"> 
        function cancel_data() {
            if (confirm("Cancel Data (yes/no) ?") == true)
                return true;
            else
                return false;
        }
    </script>

</asp:Content>
