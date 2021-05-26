<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="gsmviewdetail.aspx.vb" Inherits="PTECCENTER.gsmviewdetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
                           <i class="fa fa-tasks" aria-hidden="true"></i> GSM to D365
                  </li>
                </ol>
                <p></p>
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">วันที่ปิดวัน</span>
                          </div>
                          <asp:label class="form-control" ID="lblCloseDate" runat="server"></asp:label>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">สาขา</span>
                          </div>
                          <asp:label class="form-control" ID="lblBranch" runat="server"></asp:label>
                        </div>
                    </div>
                </div>
                        <div class="card"><!-- begin sale by meter -->
                        <div class="card-header">
                              ยอดขายสุทธิ รวมมูลค่า : <asp:Label ID="lblmetertotal" runat="server" Text="..."></asp:Label>
                              <div style="position:absolute;right:0;top:0"><img id="ShowID" src="../icon/arrowdown.png" style="width:20px" onclick="show('meter')" />
                              <img id="HideID" src="../icon/arrowup.png" style="width:20px" onclick="hide('meter')" /></div>
                        </div> 
                          <div style="display:none" id="meter" class="card-body">
                              <div class="table-responsive">
                                <asp:GridView ID="gvDataSaleMeter"  
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    emptydatatext="No data available." 
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#CCCCFF" />

                                      <Columns>
	                                    <asp:TemplateField HeaderText="ยอดขาย">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblGradeTitle" runat="server" Text='<%#Eval("grade_title")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ลิตร/หน่วย">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblVolume" runat="server" Text='<%#Eval("volume", "{0:N2}")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ราคา">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblPrice" runat="server" Text='<%#Eval("price", "{0:N2}")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="มูลค่า">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblAmount" runat="server" Text='<%#Eval("amount", "{0:N2}")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>	                                    
                                    </Columns>
                              </asp:GridView>
                                               
                            </div>
                          </div>
                        </div><!-- end sale by meter -->

                        <div class="card"><!-- begin test meter-->
                        <div class="card-header">
                              ทดสอบ Meter รวมมูลค่า : <asp:Label ID="lbltestmeter" runat="server" Text="0.00"></asp:Label>
                              <div style="position:absolute;right:0;top:0"><img id="ShowIDtestmeter" src="../icon/arrowdown.png" style="width:20px" onclick="show('testmeter')" />
                              <img id="HideIDtestmeter" src="../icon/arrowup.png" style="width:20px" onclick="hide('testmeter')" /></div>
                        </div> 
                          <div  style="display:none" id="testmeter" class="card-body">
                              <div class="table-responsive">
                                <asp:GridView ID="gvDataTest"  
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    emptydatatext="ไม่มีข้อมูลการทดสอบมิเตอร์" 
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#CCCCFF" />

                                      <Columns>
	                                    <asp:TemplateField HeaderText="ทดสอบ">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblGradeTitle" runat="server" Text='<%#Eval("grade_title")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ลิตร">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblVolume" runat="server" Text='<%#Eval("volume", "{0:N2}")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ราคา">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblPrice" runat="server" Text='<%#Eval("price", "{0:N2}")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="มูลค่า">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblAmount" runat="server" Text='<%#Eval("amount", "{0:N2}")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>	                                    
                                    </Columns>
                              </asp:GridView>
                                               
                            </div>
                          </div>
                        </div><!-- end test meter-->

                        <div class="card"><!-- begin topup meter-->
                        <div class="card-header">
                              Topup Meter รวมมูลค่า : <asp:Label ID="lbltopup" runat="server" Text="0.00"></asp:Label>
                              <div style="position:absolute;right:0;top:0"><img id="ShowIDtopup" src="../icon/arrowdown.png" style="width:20px" onclick="show('topup')" />
                              <img id="HideIDtopup" src="../icon/arrowup.png" style="width:20px" onclick="hide('topup')" /></div>
                        </div> 
                          <div style="display:none" id="topup" class="card-body">
                              <div class="table-responsive">
                                <asp:GridView ID="gvDataTopup"  
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    emptydatatext="ไม่มีข้อมูลการ Topup" 
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#CCCCFF" />

                                      <Columns>
	                                    <asp:TemplateField HeaderText="Topup">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblGradeTitle" runat="server" Text='<%#Eval("grade_title")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ลิตร">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblVolume" runat="server" Text='<%#Eval("volume", "{0:N2}")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ราคา">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblPrice" runat="server" Text='<%#Eval("price", "{0:N2}")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="มูลค่า">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblAmount" runat="server" Text='<%#Eval("amount", "{0:N2}")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>	                                    
                                    </Columns>
                              </asp:GridView>
                                               
                            </div>
                          </div>
                        </div><!-- begin topup meter-->

                        <div class="card"><!-- begin ar -->
                        <div class="card-header">
                              ขายเชื่อ รวมมูลค่า : <asp:Label ID="lblAr" runat="server" Text="..."></asp:Label>
                              <div style="position:absolute;right:0;top:0"><img id="ShowIDar" src="../icon/arrowdown.png" style="width:20px" onclick="show('ar')" />
                              <img id="HideIDar" src="../icon/arrowup.png" style="width:20px" onclick="hide('ar')" /></div>
                        </div> 
                          <div style="display:none" id="ar" class="card-body">
                              <div class="table-responsive">
                                <asp:GridView ID="gvDataAR"  
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    emptydatatext="No data available." 
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#CCCCFF" />

                                      <Columns>
	                                    <asp:TemplateField HeaderText="เงินเชื่อ">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblAr" runat="server" Text='<%#Eval("ar_number")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ลูกค้า">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblCustomer" runat="server" Text='<%#Eval("customer_name")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ผลิตภัณฑ์">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblProduct" runat="server" Text='<%#Eval("product")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="จำนวน">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblVolume" runat="server" Text='<%#Eval("volume", "{0:N2}")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ราคา" >
		                                    <ItemTemplate>
			                                    <asp:label id="lblPrice" runat="server" text='<%#Eval("price", "{0:N2}")%>' ></asp:label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="มูลค่า" >
		                                    <ItemTemplate>
			                                    <asp:label id="lblAmount" runat="server" text='<%#Eval("amount", "{0:N2}")%>' ></asp:label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
                                    </Columns>
                              </asp:GridView>
                                               
                            </div>
                          </div>
                        </div><!-- begin ar -->

                          <div class="card-body">
                              <div class="table-responsive">
                                <asp:GridView ID="gvDataOther"  
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    emptydatatext="No data available." 
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#CCCCFF" />

                                      <Columns>
	                                    <asp:TemplateField HeaderText="รับเงิน และอื่น ๆ">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblDescription" runat="server" Text='<%#Eval("td_description")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="มูลค่า">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblAmount" runat="server" Text='<%#Eval("amount", "{0:N2}")%>'></asp:Label>
		                                    </ItemTemplate>
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


</asp:Content>
