<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="OilPriceUpdate.aspx.vb" Inherits="PTECCENTER.OilPriceUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper" class="h-100">

    <!-- #include virtual ="/include/menu.inc" --> <!-- add side menu -->



        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">    
                <!-- Breadcrumbs-->
                <ol class="breadcrumb"style="background-color:deeppink;color:white">
                  <li class="breadcrumb-item" >
                           <i class="fa fa-tasks" aria-hidden="true"></i> ปรับราคาน้ำมัน กทม.
                  </li>
                </ol>
                <p></p>
                <div class="row">
                    <div class="col-lg-8 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">วันที่ปรับราคา</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtCloseDate" runat="server"></asp:TextBox>
                          &nbsp;<asp:Button ID="btnFind" class="btn btn-sm  btn-primary" runat="server" Text="Find"   />&nbsp;
                        </div>
                    </div>
                </div>
                                <div class="row">
                                    <div class="col-lg-4 mb-3">
                                        <div class="input-group sm-3">
                                          <div class="input-group-prepend">
                                            <span class="input-group-text">กะที่มีผล</span>
                                          </div>
                                          <asp:DropDownList class="form-control" ID="cboShift" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-lg-8 mb-3">
                                        <div class="input-group sm-3">
                                            <div style="position:absolute;right:0;">
                                                <asp:Button ID="btnsave" class="btn btn-sm  btn-success" runat="server" Text="บันทึกเปลี่ยนแปลงราคา"   />&nbsp;
                                            </div>
                                        </div>
                                    </div>
                                </div>
                          <div class="card-body">
                        
                              <div class="table-responsive">
                                <asp:GridView ID="gvData"  
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    emptydatatext="No data available." 
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#CCCCFF" />

                                      <Columns>
	                                    <asp:TemplateField HeaderText="กะที่มีผล">
                                            <itemstyle Width="100px" />
		                                    <ItemTemplate>
			                                    <asp:Label id="lblshiftno" runat="server" Text='<%#Eval("shiftno")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Code">
                                            <itemstyle Width="100px" />
		                                    <ItemTemplate>
			                                    <asp:Label id="lblname" runat="server" Text='<%#Eval("product_name")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="วันที่ปรับล่าสุด">
                                            <itemstyle Width="100px" />
		                                    <ItemTemplate>
			                                    <asp:Label id="lbleffectivedate" runat="server" Text='<%#Eval("effectivedate")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ราคาก่อนหน้า">
                                            <itemstyle Width="100px" />
		                                    <ItemTemplate>
			                                    <asp:Label id="lbllastprice" runat="server" Text='<%#Eval("lastprice")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="การปรับ" >
                                            <itemstyle Width="100px" />
		                                    <ItemTemplate>
			                                    <asp:label id="lblmovevalue" runat="server" text='<%#Eval("movevalue")%>' ></asp:label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ราคาที่มีผล" >
                                            <itemstyle Width="100px" />
		                                    <ItemTemplate>
			                                    <asp:label id="lblprice" runat="server" text='<%#Eval("price")%>' ></asp:label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="มูลค่าที่ปรับ">
                                            <itemstyle Width="100px" />
                                            <ItemTemplate>                   
                                                <asp:textbox id="txtadjust" runat="server" text='<%#Eval("adjust")%>'  AutoPostBack="true" OnTextChanged="txtadjust_TextChanged">></asp:textbox>
                                                <%--<asp:HyperLink ID="HyperLink" runat="server" NavigateUrl='<%#Eval("link")%>' Text="" data-toggle="tooltip" data-placement="right" title="View Detail"><i class="fa fa-eye" aria-hidden="true"></i></asp:HyperLink>--%>
                                                <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Eval("Send")%>' Text="" data-toggle="tooltip" data-placement="left" title="Send to D365"><i class="fa fa-paper-plane" aria-hidden="true"></i></asp:HyperLink>--%>
                                                <%--<asp:checkbox id="chk" runat="server" checked='<%#Eval("chk")%>'></asp:checkbox>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ราคาใหม่">
                                            <itemstyle Width="100px" />
                                            <ItemTemplate>                   
                                                <asp:label id="lblactual" runat="server" text='<%#Eval("actual")%>' ></asp:label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                              </asp:GridView>
                                               
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <div class="input-group sm-3">
                                        <div style="position:absolute;right:0;">
                                          <asp:Button ID="btnConfirm" class="btn btn-sm  btn-primary" 
                                              runat="server" Text="ยืนยัน และส่งข้อมูลการปรับราคา"  OnClientClick="(myClosure())().canyouseeIt()"/>&nbsp;
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
