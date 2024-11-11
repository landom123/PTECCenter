<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="UpdateSmileSheetID.aspx.vb" Inherits="PTECCENTER.UpdateSmileSheetID" %>

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
                           <i class="fa fa-tasks" aria-hidden="true"></i> ข้อมูล Google Sheet
                  </li>
                </ol>
                <p></p>
                          <div class="card-body">
                        
                              <div class="table-responsive">
                                <asp:GridView ID="gvData"  
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    ShowFooter="True" ShowHeader="true"
                                    OnRowCommand="gvData_OnRowCommand"
                                    emptydatatext="No data available." 
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#CCCCFF" />

                                      <Columns>
	                                    <asp:TemplateField HeaderText="เดือน">
                                            <itemstyle Width="100px" />
		                                    <ItemTemplate>
			                                    <asp:Label id="lblmonthly" runat="server" Text='<%#Eval("monthly")%>'></asp:Label>
		                                    </ItemTemplate>

	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Google Sheet">
                                            <itemstyle Width="300px" />
		                                    <ItemTemplate>
                                                <asp:textbox id="txtsheetid" class="form-control" runat="server" text='<%#Eval("sheetid")%>' ></asp:textbox>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>

                                    </Columns>

                              </asp:GridView>
                <div class="row">
                    <div class="col-lg-4 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">เดือน ปี (yyyymm : 202106) </span>
                          </div>
                            <asp:textbox id="txtaddmonthly" class="form-control" runat="server" ></asp:textbox>
                        </div>
                    </div>
                    <div class="col-lg-4 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">เลขที่ google sheet </span>
                          </div>
                            <asp:textbox id="txtaddsheetid" class="form-control" runat="server" ></asp:textbox>
                        </div>
                    </div>
                    <div class="col-lg-4 mb-3">
                        <div class="input-group sm-3">
                            <asp:Button ID="btnadd" class="btn btn-sm  btn-success" runat="server"  Text="Add" />
                        </div>
                    </div>

                </div>                                                  
                            </div>

                              <br />
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
    </script>


    <script type="text/javascript">
        myClosure = function () {
            var canyousee = "here I'm ";
            return (function () {
                return { canyouseeIt: function () { return confirm(canyousee) } };
            });
        }
    </script>
</asp:Content>
