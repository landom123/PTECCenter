<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="RemindUpdateDue.aspx.vb" Inherits="PTECCENTER.RemindUpdateDue" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <!-- datetimepicker-->
  <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">

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
                    <a href="#">OPS : ระบบการแจ้งเตือน / ปรับปรุงวันครบกำหนด  </a>
                  </li>
                </ol>
<p></p>
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">สาขา</span>
                          </div>
                          <asp:DropDownList class="custom-select" ID="cboBranch" runat="server"></asp:DropDownList>
                          &nbsp;<asp:Button ID="btnRefresh" class="btn btn-sm  btn-success" runat="server" Text="Refresh" />
                        </div>
                    </div>
                </div>    
<p></p>
                          <div class="card-body">
                              <div class="table-responsive">
                              <asp:GridView ID="gvRemind" 
                                  class="table table-striped table-bordered" 
                                  autogeneratecolumns="false"
                                  emptydatatext="No data available." 
                                  allowpaging="true" 
                                  runat="server">
                                 <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                 <AlternatingRowStyle BackColor="#CCCCFF" />
                                      <Columns>
	                                    <asp:TemplateField HeaderText="ID">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblID" runat="server" ></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="หัวข้อ">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblsubject" runat="server" ></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>

	                                    <asp:TemplateField HeaderText="วันที่แจ้งเตือน">
		                                    <ItemTemplate>
			                                    <asp:Label id="lbldate" runat="server"></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Actived" >
		                                    <ItemTemplate>
                                                <asp:CheckBox ID="chkitem" runat="server" AutoPostBack="true"/>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>

                                    </Columns>
                              </asp:GridView>
                                               
                            </div>
                          </div>

                        <!-- end display job detail -->
<p></p>
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">วันที่</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtDueDate" runat="server" autocomplete="off"></asp:TextBox>
                            &nbsp;<asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text="Save" />
                        </div>                                                
                    </div>
                </div>             
<p></p>


            </div>            <!-- /.container-fluid -->
        </div>        <!-- end content-wrapper -->


        <!-- end เนื้อหาเว็บ -->


    </div>
    <!-- /#wrapper -->
  <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>

    <script type="text/javascript">
        jQuery('[id$=txtDueDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            format:'d/m/Y'
        });
    </script>

</asp:Content>