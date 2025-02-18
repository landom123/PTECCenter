<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="OilPriceFormula.aspx.vb" Inherits="PTECCENTER.OilPriceFormula" %>

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
                           <i class="fa fa-tasks" aria-hidden="true"></i> ข้อมูล สูตรการคำนวนราคาหน้าปั้ม ตามสาขา
                  </li>
                </ol>
                <p></p>
<%--                <div class="row">
                    <div class="col-8">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">วันที่ปรับราคา</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtCloseDate" runat="server"></asp:TextBox>
                          &nbsp;<asp:Button ID="btnFind" class="btn btn-sm  btn-primary" runat="server" Text="Find"   />&nbsp;
                        </div>
                    </div>
                </div>--%>

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
	                                    <asp:TemplateField HeaderText="สาขา">
                                            <itemstyle Width="100px" />
		                                    <ItemTemplate>
			                                    <asp:Label id="lblbranch" runat="server" Text='<%#Eval("branchid")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ส่วนต่างฯ">
                                            <itemstyle Width="100px" />
		                                    <ItemTemplate>
			                                    <asp:Label id="lbldiffbkk" runat="server" Text='<%#Eval("diffbkk")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ภาษี">
                                            <itemstyle Width="50px" />
		                                    <ItemTemplate>
			                                    <asp:Label id="lbltax" runat="server" Text='<%#Eval("tax")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="B7 เพิ่ม/ลด" >
                                            <itemstyle Width="50px" />
		                                    <ItemTemplate>
			                                    <asp:label id="lblb7" runat="server" text='<%#Eval("b7add")%>' ></asp:label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="B10 เพิ่ม/ลด" >
                                            <itemstyle Width="50px" />
		                                    <ItemTemplate>
			                                    <asp:label id="lblb10" runat="server" text='<%#Eval("b10add")%>' ></asp:label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>	
	                                    <asp:TemplateField HeaderText="G91 เพิ่ม/ลด" >
                                            <itemstyle Width="50px" />
		                                    <ItemTemplate>
			                                    <asp:label id="lblg91" runat="server" text='<%#Eval("g91add")%>' ></asp:label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="G95 เพิ่ม/ลด" >
                                            <itemstyle Width="50px" />
		                                    <ItemTemplate>
			                                    <asp:label id="lblg95" runat="server" text='<%#Eval("g95add")%>' ></asp:label>
		                                    </ItemTemplate>
                                        </asp:TemplateField>	
	                                    <asp:TemplateField HeaderText="E20 เพิ่ม/ลด" >
                                            <itemstyle Width="50px" />
		                                    <ItemTemplate>
			                                    <asp:label id="lble20" runat="server" text='<%#Eval("e20add")%>' ></asp:label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="PADO เพิ่ม/ลด" >
                                            <itemstyle Width="50px" />
		                                    <ItemTemplate>
			                                    <asp:label id="lblpado" runat="server" text='<%#Eval("padoadd")%>' ></asp:label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>        
	                                    <asp:TemplateField HeaderText="PG เพิ่ม/ลด" >
                                            <itemstyle Width="50px" />
		                                    <ItemTemplate>
			                                    <asp:label id="lblpg95" runat="server" text='<%#Eval("pg95add")%>' ></asp:label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>  
	                                    <asp:TemplateField HeaderText="วันที่เริ่มใช้" >
                                            <itemstyle Width="100px" />
		                                    <ItemTemplate>
			                                    <asp:label id="lblbegindate" runat="server" text='<%#Eval("begindate")%>' ></asp:label>
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


</asp:Content>
