<%@ Page Title="Remind : ระบบการแจ้งเตือน" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="RemindFrm.aspx.vb" Inherits="PTECCENTER.FrmRemind" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper">

    <!-- #include virtual ="/include/menu.inc" --> <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">    
                <!-- Breadcrumbs-->
                <ol class="breadcrumb">
                  <li class="breadcrumb-item">
                    <a href="#">OPS : ระบบการแจ้งเตือน  </a>
                  </li>
                </ol>
                <div class="row">
                    <div class="col-12">
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-success" runat="server" Text="New" />  &nbsp;              
                    </div>
                </div>
                          <div class="card-body">
                              <div class="table-responsive">
                              <asp:GridView ID="gvRemind" 
                                  class="table table-striped table-bordered" 
                                  autogeneratecolumns="false"
                                  emptydatatext="No data available." 
                                  allowpaging="true"
                                  runat="server">
                                      <Columns>
	                                    <asp:TemplateField HeaderText="ID">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblID" runat="server" Text='<%#Eval("remindid")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="หัวข้อ">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblsubject" runat="server" Text='<%#Eval("emailmessage")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
<%--	                                    <asp:TemplateField HeaderText="email">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblemail" runat="server" Text='<%#Eval("email")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>--%>
	                                    <asp:TemplateField HeaderText="วันที่แจ้งเตือน">
		                                    <ItemTemplate>
			                                    <asp:Label id="lbldate" runat="server" Text='<%#Eval("reminddate")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Actived" >
		                                    <ItemTemplate>
			                                    <asp:checkbox id="lblactived" runat="server" checked='<%#Eval("actived")%>' Enabled="false"></asp:checkbox>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>                          
                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Eval("link") %>' Text=""><img src="../icon/addnote.png" title="รายละเอียด" style="width:20px" /></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                              </asp:GridView>
                                               
                            </div>
                          </div>
                        <!-- end display job detail -->


<%--                    </div>
                </div>--%>
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

</asp:Content>
