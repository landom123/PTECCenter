<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="monthly_payment.aspx.vb" Inherits="PTECCENTER.monthly_payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper" class="h-100">

        <!-- #include virtual ="/include/menu.inc" -->
        <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">
                <!-- Breadcrumbs-->
                <ol class="breadcrumb" style="background-color: deeppink; color: white">
                    <li class="breadcrumb-item">
                        <a href="client_list.aspx"><i class="fa fa-tasks" aria-hidden="true"></i></a>คำนวนการจ่ายเงินประจำเดือน
                    </li>
                </ol>
                <p></p>
                <div class="row">
                    <div class="col-3">
                        <asp:TextBox class="form-control" ID="txtCalcDate" runat="server" Style="background-color: white"></asp:TextBox>
                    </div>
                    <div class="col-4">
                        <asp:Button ID="btnCalc" class="btn btn-sm  btn-primary" runat="server" Text=" คำนวน " />
                        <asp:Button ID="btnExport" class="btn btn-sm  btn-primary" runat="server" Text=" Export to excel " />
                        <asp:HyperLink NavigateUrl="frmReportMontly.aspx" ID="Button1" class="btn btn-sm  btn-danger" runat="server" Text=" report " />
                    </div>

                </div>

                <div class="card-body">

                    <div class="table-responsive">
                        <asp:GridView ID="gvPayment"
                            class="table table-striped table-bordered"
                            AllowSorting="True"
                            AllowPaging="false"
                            AutoGenerateColumns="false"
                            EmptyDataText=" ไม่มีข้อมูล "
                            runat="server" CssClass="table table-striped">
                            <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-Size="Smaller" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#CCCCFF" />

                            <Columns>
                                <asp:BoundField DataField="lawcontractno" HeaderText="เลขที่เอกสาร" />
                                <asp:BoundField DataField="paymenttype" HeaderText="ประเภท" />
                                <asp:BoundField DataField="duedate" HeaderText="กำหนดชำระ" />
                                <asp:BoundField DataField="amount" HeaderText="จำนวนเงิน" />
                                <asp:BoundField DataField="sale_volume" HeaderText="ยอดขายลิตร" />

                                <asp:BoundField DataField="Branch" HeaderText="สาขา" />
                                <asp:BoundField DataField="PaidType" HeaderText="ประเภทการจ่าย" />
                                <asp:BoundField DataField="BankBranchName" HeaderText="ธนาคาร" />

                                <%--                 <asp:TemplateField HeaderText="เลขที่สัญญา">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblagno" runat="server" Text='<%#Eval("lawcontractno")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ประเภท">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblpaymenttype" runat="server" Text='<%#Eval("paymenttype")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="กำหนดชำระ">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblduedate" runat="server" Text='<%#Eval("duedate")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="จำนวนเงิน">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblamount" runat="server" Text='<%#Eval("amount")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ยอดขายลิตร">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblvolume" runat="server" Text='<%#Eval("sale_volume")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>                          
                                                <asp:HyperLink ID="HyperLink" runat="server" NavigateUrl='<%#Eval("link")%>' Text="" data-toggle="tooltip" data-placement="right" title="View Detail"><i class="fa fa-eye" aria-hidden="true"></i></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>

                    </div>
                    <!-- end Table payment-->

                </div>


            </div>
            <!-- /.container-fluid -->
            <!-- Sticky Footer -->
            <footer class="sticky-footer d-none">
                <div class="container my-auto">
                    <div class="copyright text-center my-auto">
                        <span>Copyright © Your Website 2019</span>
                    </div>
                </div>
            </footer>
        </div>
        <!-- end content-wrapper -->


        <!-- end เนื้อหาเว็บ -->


    </div>
    <!-- /#wrapper -->
    <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>

    <script type="text/javascript">
        jQuery('[id$=txtCalcDate]').datetimepicker({
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

</asp:Content>
