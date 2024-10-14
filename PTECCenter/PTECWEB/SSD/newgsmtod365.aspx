<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="newgsmtod365.aspx.vb" Inherits="PTECCENTER.newgsmtod365" %>

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
                           <i class="fa fa-tasks" aria-hidden="true"></i> NEW GSM to D365
                  </li>
                </ol>
                <p>
                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
                </p>
                <div class="row">
                    <div class="col-8">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">วันที่ปิดวัน</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtCloseDate" runat="server"></asp:TextBox>
                          <asp:dropdownlist class="form-control" ID="cbobranch" runat="server"></asp:dropdownlist>
                          &nbsp;<asp:Button ID="btnFind" class="btn btn-sm  btn-primary" runat="server" Text="Find" />&nbsp;
                          
                        </div>
                    </div>
                </div>

                          <div class="card-body">
                              <div class="table-responsive">
                                <asp:GridView ID="gvData"  
                                    OnRowCommand="GridView1_RowCommand"
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    emptydatatext="No data available." 
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#CCCCFF" />

                                      <Columns>
	                                    <asp:TemplateField HeaderText="Branch">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblbranch" runat="server" Text='<%#Eval("br_code")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Name">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblname" runat="server" Text='<%#Eval("br_name")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ยอดขายรวม">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblincome" runat="server" Text='<%#Eval("income", "{0:N2}")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ยอดรับชำระ">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblreceive" runat="server" Text='<%#Eval("receive", "{0:N2}")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Diff" >
		                                    <ItemTemplate>
			                                    <asp:label id="lbldiff" runat="server" text='<%#Eval("diff", "{0:N2}")%>' ></asp:label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>                          
                                                <asp:HyperLink ID="HyperLink" runat="server" NavigateUrl='<%#Eval("link")%>' Text="" data-toggle="tooltip" data-placement="right" title="View Detail"><i class="fa fa-eye" aria-hidden="true"></i></asp:HyperLink>
                                                <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Eval("Send")%>' Text="" data-toggle="tooltip" data-placement="left" title="Send to D365"><i class="fa fa-paper-plane" aria-hidden="true"></i></asp:HyperLink>--%>
                                                <%--<asp:checkbox id="chk" runat="server" checked='<%#Eval("chk")%>'></asp:checkbox>--%>
                                                <%--<asp:Button ID="btnCreate" class="btn btn-sm  btn-primary" runat="server" Text="Create" CommandName="Create" CommandArgument="<%# Container.DataItemIndex %>"/>                                                --%>
                                                <asp:Button ID="btnDownload" class="btn btn-sm  btn-warning" runat="server" Text="Create and Download" CommandName="Download" CommandArgument="<%# Container.DataItemIndex %>"/>                                                
                                                <%--<asp:Button ID="btnSend" class="btn btn-sm  btn-success" runat="server" Text="Send" CommandName="Send" CommandArgument="<%# Container.DataItemIndex %>"/>                                                --%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                              </asp:GridView>
                                               
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
        function alertSuccessToast(massage) {

            const Toast = Swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 1000,
                timerProgressBar: true,
                didOpen: (toast) => {
                    toast.addEventListener('mouseenter', Swal.stopTimer)
                    toast.addEventListener('mouseleave', Swal.resumeTimer)
                }
            })

            Toast.fire({
                icon: 'success',
                title: massage
            })
        }
    </script>


</asp:Content>
