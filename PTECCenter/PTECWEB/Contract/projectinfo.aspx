<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="projectinfo.aspx.vb" Inherits="PTECCENTER.projectinfo" %>

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
                           <i class="fa fa-tasks" aria-hidden="true"></i>ข้อมูลโครงการ
                  </li>
                </ol>
                <p></p>
                <div class="row">
                    <div class="col-6">
                        <asp:Button ID="btnNewProject" class="btn btn-sm  btn-primary" runat="server" Text=" New " />
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text=" Save " />
                        <asp:Button ID="btnConfirm" class="btn btn-sm  btn-success" runat="server" Text=" Confirm " OnClientClick="return confirm_data();" />
                        <asp:Button ID="btnCancel" class="btn btn-sm  btn-danger" runat="server" Text=" Cancel " OnClientClick="return cancel_data();"/>
                    </div>
                    <div class="col-6 text-right">
                        <asp:Button ID="btnChange" class="btn btn-sm  btn-primary" runat="server" Text=" Change " />
                        <asp:Button ID="btnApprove" class="btn btn-sm  btn-success" runat="server" Text=" Approve " />
                    </div>
                </div>
    <p></p>    
    <div class="row">
        <div class="col-4">
            <div class="input-group sm-3">
                <div class="input-group-prepend">
                <span class="input-group-text">เลขที่โครงการ</span>
                </div>
                <asp:TextBox class="form-control" ID="txtprojectno" placeholder="please save first" ReadOnly="true" runat="server" ></asp:TextBox>    

            </div>
        </div>
        <div class="col-4">
            <div class="input-group sm-3">
                <div class="input-group-prepend">
                <span class="input-group-text">รหัสสาขา</span>
                </div>
                <asp:TextBox class="form-control" ID="txtbranch" placeholder="ตัวอย่าง 116" runat="server"></asp:TextBox>    

            </div>
        </div>
        <div class="col-4" style="text-align:center" >
                <asp:label class="form-control" ID="lblStatus"  runat="server"></asp:label>    
        </div>
    </div><br />
 <div class="row">
        <div class="col-12">
            <div class="input-group sm-3">
                <div class="input-group-prepend">
                <span class="input-group-text">Note</span>
                </div>
                <asp:TextBox class="form-control" ID="txtRemark"  TextMode="MultiLine" runat="server"></asp:TextBox>    

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
                                            รายการสัญญา                                           
                                        </div>
                                        <div class="col-4" style="text-align:right">
                                            <asp:Button ID="btnNewAgree" class="btn btn-sm  btn-primary" runat="server" Text=" + เพิ่มสัญญา" />
                                        </div>
                                    </div>
                                <asp:GridView ID="gvData"  
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    emptydatatext=" ไม่มีข้อมูล " 
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#CCCCFF" />

                                      <Columns>
	                                    <asp:TemplateField HeaderText="Agree">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblagno" runat="server" Text='<%#Eval("agno")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Date">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblagdate" runat="server" Text='<%#Eval("agdate")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Type">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblagtype" runat="server" Text='<%#Eval("agtype")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Status">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblagstatus" runat="server" Text='<%#Eval("agstatus")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Approver">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblapproveby" runat="server" Text='<%#Eval("approveby")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Approve Date">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblapprovedate" runat="server" Text='<%#Eval("approvedate")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>                          
                                                <asp:HyperLink ID="HyperLink" runat="server" NavigateUrl='<%#Eval("link")%>' Text="" data-toggle="tooltip" data-placement="right" title="View Detail"><i class="fa fa-eye" aria-hidden="true"></i></asp:HyperLink>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                              </asp:GridView>
                                               
                            </div>
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


    </div>
    <!-- /#wrapper -->

  <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>

    <script type="text/javascript">
        jQuery('[id$=txtProjectDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format:'d/m/Y'
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
