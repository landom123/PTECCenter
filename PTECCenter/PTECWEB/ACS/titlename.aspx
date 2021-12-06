<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="titlename.aspx.vb" Inherits="PTECCENTER.titlename" %>

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
                           <i class="fa fa-tasks" aria-hidden="true"></i> Esso EDI Invoice to D365
                  </li>
                </ol>
                <p></p>

                          <div class="card-body">
<%--                    <div class="col-12 text-right">
                            <asp:Button ID="btnselectall" class="btn btn-sm  btn-success" runat="server" Text="Select All" />&nbsp;  
                            <asp:Button ID="btnunselect" class="btn btn-sm  btn-danger" runat="server" Text="Un Select All" />  
                    </div>--%>

                              <div class="table-responsive">
                                <asp:GridView ID="gvData"  
                                    OnRowCommand="GridView1_RowCommand"
                                    ShowFooter="true"
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    emptydatatext="No data available." 
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#CCCCFF" />
                                      <Columns>
	                                    <asp:TemplateField HeaderText="ID">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblID" runat="server" Text='<%#Eval("titleid")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="คำนำหน้า">
		                                    <ItemTemplate>
			                                    <asp:textbox id="txttitlename" runat="server" Text='<%#Eval("titlename")%>'></asp:textbox>
		                                    </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txttitlename_add" runat="server"></asp:TextBox>
                                            </FooterTemplate>
	                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>                          
                                                <%--<asp:Button ID="btnUpdate" class="btn btn-sm  " runat="server" Text="<i class='fas minus-circle' style='color:blue;font-size:20px'></i> "  CommandName="Save" CommandArgument="<%# Container.DataItemIndex %>"/>                                                --%>
                                                <asp:LinkButton runat="server" ID="btnUpdate" Text="<i class='fas fa-check-square' aria-hidden='false' style='color:green;font-size:20px'></i> " 
                                                ValidationGroup="edt"  ToolTip="บันทึก"  CommandName="Update" CommandArgument="<%# Container.DataItemIndex %>" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton runat="server" ID="btnSave" Text="<i class='fas fa-plus-circle' style='color:green;font-size:20px'></i> " 
                                                ValidationGroup="edt" OnClick="Save"  ToolTip="เพิ่ม"/>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                              </asp:GridView>
                                               
                            </div>
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
            format:'d/m/Y'
        });
    </script>


</asp:Content>
