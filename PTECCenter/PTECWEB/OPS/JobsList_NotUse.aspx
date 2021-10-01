<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="JobsList_NotUse.aspx.vb" Inherits="PTECCENTER.JobsList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div id="wrapper">

    <!-- #include virtual ="/include/menu.inc" --> <!-- add side menu -->

    <!-- เนื้อหาเว็บ -->
    <div id="content-wrapper">

      <div class="container-fluid">

        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
          <li class="breadcrumb-item">
            <a href="#">Job List</a>
          </li>
        </ol>

                <div class="row">
                    <div class="col-12">
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-primary" runat="server" Text="New" />&nbsp;
                    </div>
                </div>
                          <div class="card-body">
                              <div class="table-responsive">
                              <asp:GridView ID="gvRemind"
                                  class="table table-striped table-bordered"
                                  AutoGenerateColumns="false"
                                  EmptyDataText="No data available."
                                  PageSize="20"
                                  AllowPaging="true"
                                  runat="server">
                                      <Columns>
	                                    <asp:TemplateField HeaderText="Code">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblcode" runat="server" Text='<%#Eval("jobcode")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Branch">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblBranch" runat="server" Text='<%#Eval("branch")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Date">
		                                    <ItemTemplate>
			                                    <asp:Label id="lbljobdate" runat="server" Text='<%#Eval("jobdate")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Type">
		                                    <ItemTemplate>
			                                    <asp:Label id="lbljobtype" runat="server" Text='<%#Eval("jobtype")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Detail" >
		                                    <ItemTemplate>
			                                    <asp:label id="lbldetails" runat="server" text='<%#Eval("details")%>' ></asp:label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Status" >
		                                    <ItemTemplate>
			                                    <asp:label id="lblstatus" runat="server" text='<%#Eval("status")%>' ></asp:label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>                          
                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Eval("link")%>' Text=""><img src="../icon/addnote.png" title="รายละเอียด" style="width:20px" /></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                              </asp:GridView>
                                               
                            </div>
                          </div>


      </div>
      <!-- /.container-fluid -->


      <!-- Sticky Footer -->
      <footer class="sticky-footer">
        <div class="container my-auto">
          <div class="copyright text-center my-auto">
            <span>Copyright © Your Website 2019</span>
          </div>
        </div>
      </footer>

    </div>
    <!-- /.content-wrapper -->
    <!-- end เนื้อหาเว็บ -->


  </div>
  <!-- /#wrapper -->

</asp:Content>
