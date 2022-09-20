<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="contractinfo.aspx.vb" Inherits="PTECCENTER.contractinfo" %>

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
                           <a href="projectlist.aspx"><i class="fa fa-tasks" aria-hidden="true"></i></a> ข้อมูลสัญญา
                  </li>
                </ol>
                <p></p>
                <div class="row">
                    <div class="col-6">
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-primary" runat="server" Text=" New " />
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text=" Save " />
                        <asp:Button ID="btnDel" class="btn btn-sm  btn-danger" runat="server" Text=" Delete " OnClientClick="return cancel_data();"/>
                    </div>
                    <div class="col-6" style="text-align:right">
                        <asp:Button ID="btnEstimate" class="btn btn-sm  btn-success" runat="server" Text=" ประมาณการ " />
                        <asp:Button ID="btnBack" class="btn btn-sm  btn-success" runat="server" Text=" กลับ Project " />
                    </div>
                </div>
                <p></p>    
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                            <span class="input-group-text">เลขที่โครงการ</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtprojectno" ReadOnly="true" runat="server" ></asp:TextBox>    

                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                            <span class="input-group-text">รหัสสาขา</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtbranch" placeholder="ตัวอย่าง 116" runat="server" ReadOnly="true"></asp:TextBox>    

                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                            <span class="input-group-text">สถานะโครงการ</span>
                            </div>
                            <asp:label class="form-control" ID="lblStatus"  runat="server"></asp:label>   
                        </div>
                    </div>
                </div>
                <br />
                <div class="card-body">
                    <div class="row">
                        <div class="col-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                <span class="input-group-text">เลขที่ในระบบ</span>
                                </div>
                                <asp:TextBox class="form-control" ID="txtContractNo" placeholder="please save first" ReadOnly="true" runat="server" ></asp:TextBox>    

                            </div>
                        </div>
                        <div class="col-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                <span class="input-group-text">เลขที่เอกสารสัญญา</span>
                                </div>
                                <asp:TextBox class="form-control" ID="txtLawContractNo" runat="server" ></asp:TextBox>    

                            </div>
                        </div>                        
                    </div>
                    <div class="row">
                        <div class="col-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                <span class="input-group-text">ประเภทสัญญา</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboContractType"  runat="server" ></asp:DropDownList>    
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                <span class="input-group-text">วันที่สัญญา</span>
                                </div>
                                <asp:TextBox class="form-control" ID="txtContractDate" style="background-color:white" runat="server"></asp:TextBox>    

                            </div>
                        </div>
                        <div class="col-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                <span class="input-group-text">วันที่สัญญามีผล</span>
                                </div>
                                <asp:TextBox class="form-control" ID="txtContractActiveDate" style="background-color:white" runat="server" ></asp:TextBox>    

                            </div>
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
                                            คู่สัญญา                                           
                                        </div>
                                        <div class="col-4" style="text-align:right">
                                            <asp:Button ID="btnClient" class="btn btn-sm  btn-primary" runat="server" Text=" + เพิ่มคู่สัญญา" />
                                        </div>
                                    </div>
                                <asp:GridView ID="gvClient"  
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    emptydatatext=" ไม่มีข้อมูล " 
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#CCCCFF" />

                                      <Columns>
	                                    <asp:TemplateField HeaderText="No">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblno" runat="server" Text='<%#Eval("no")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Client No">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblclientno" runat="server" Text='<%#Eval("clientno")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Name">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblclient" runat="server" Text='<%#Eval("client")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Address">
		                                    <ItemTemplate>
			                                    <asp:Label id="lbladdress" runat="server" Text='<%#Eval("clientaddress")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>                          
                                                <asp:HyperLink ID="HyperLink" runat="server" NavigateUrl='<%#Eval("link")%>' Text="" data-toggle="tooltip" data-placement="right" title="View Detail"><i class="fa fa-eye" aria-hidden="true"></i></asp:HyperLink>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                              </asp:GridView>
                                               
                            </div><!-- end Table คู่สัญญา-->

                              <div class="table-responsive">
                                   <div class="row">
                                        <div class="col-8" style="font-size:large;">
                                            ทรัพย์สิน                                           
                                        </div>
                                        <div class="col-4" style="text-align:right">
                                            <asp:Button ID="btnAssets" class="btn btn-sm  btn-primary" style="background-color:#00cc66"  runat="server" Text=" + เพิ่มทรัพย์สิน" />
                                        </div>
                                    </div>
                                <asp:GridView ID="gvAssets"  
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    emptydatatext=" ไม่มีข้อมูล " 
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#00cc66" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#ccffcc" />

                                      <Columns>
	                                    <asp:TemplateField HeaderText="No">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblassetsno" runat="server" Text='<%#Eval("assetsno")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Type">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblassetstype" runat="server" Text='<%#Eval("assetstype")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ที่ดิน">
		                                    <ItemTemplate>
			                                    <asp:Label id="lbllandno" runat="server" Text='<%#Eval("landno")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="สำรวจ">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblsurveyno" runat="server" Text='<%#Eval("surveyno")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>                          
                                                <asp:HyperLink ID="HyperLink" runat="server" NavigateUrl='<%#Eval("link")%>' Text="" data-toggle="tooltip" data-placement="right" title="View Detail"><i class="fa fa-eye" aria-hidden="true"></i></asp:HyperLink>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                              </asp:GridView>
                                               
                            </div><!-- end Table ทรัพย์สิน-->

                              <div class="table-responsive">
                                   <div class="row">
                                        <div class="col-8" style="font-size:large;">
                                            ค่าใช้จ่ายแบบจ่ายครั้งเดียว                                          
                                        </div>
                                        <div class="col-4" style="text-align:right">
                                            <asp:Button ID="btnOnetime" class="btn btn-sm btn-primary" runat="server" Text=" + เพิ่มค่าใช้จ่ายแบบครั้งเดียว" />
                                        </div>
                                    </div>
                                <asp:GridView ID="gvOneTime"  
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    emptydatatext=" ไม่มีข้อมูล " 
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#CCCCFF" />

                                      <Columns>
	                                    <asp:TemplateField HeaderText="ID">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblid" runat="server" Text='<%#Eval("onetimeid")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Payment">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblpaymenttype" runat="server" Text='<%#Eval("paymenttype")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Due Date">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblduedate" runat="server" Text='<%#Eval("duedate")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Payer">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblpayer" runat="server" Text='<%#Eval("payer")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Amount">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblamount" runat="server" Text='<%#Eval("amount")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Remark">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblremark" runat="server" Text='<%#Eval("remark")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>                          
                                                <asp:HyperLink ID="HyperLink" runat="server" NavigateUrl='<%#Eval("link")%>' Text="" data-toggle="tooltip" data-placement="right" title="View Detail"><i class="fa fa-eye" aria-hidden="true"></i></asp:HyperLink>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                              </asp:GridView>
                                               
                            </div><!-- end Table paymen flexible-->


                              <div class="table-responsive">
                                   <div class="row">
                                        <div class="col-8" style="font-size:large;">
                                            ผลตอบแทนแบบ Flexible                                           
                                        </div>
                                        <div class="col-4" style="text-align:right">
                                            <asp:Button ID="btnFlexible" class="btn btn-sm  btn-primary" style="background-color:#00cc66"  runat="server" Text=" + เพิ่มเงื่อนไข" />
                                        </div>
                                    </div>
                                <asp:GridView ID="gvPaymentFlex"  
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    emptydatatext=" ไม่มีข้อมูล " 
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#00cc66" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#ccffcc" />

                                      <Columns>
	                                    <asp:TemplateField HeaderText="ID">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblflexid" runat="server" Text='<%#Eval("flexid")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Type">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblpaymenttype" runat="server" Text='<%#Eval("paymenttype")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="รอบ">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblrecuring" runat="server" Text='<%#Eval("recuringtype")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ความถี่">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblfrequency" runat="server" Text='<%#Eval("frequency")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="เริ่ม">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblbegindate" runat="server" Text='<%#Eval("begindate")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="สิ้นสุด">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblenddate" runat="server" Text='<%#Eval("enddate")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="วิธีคำนวน">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblcalctype" runat="server" Text='<%#Eval("calc_type")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>                          
                                                <asp:HyperLink ID="HyperLink" runat="server" NavigateUrl='<%#Eval("link")%>' Text="" data-toggle="tooltip" data-placement="right" title="View Detail"><i class="fa fa-eye" aria-hidden="true"></i></asp:HyperLink>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                              </asp:GridView>
                                               
                            </div><!-- end Table paymen flexible-->

                              <div class="table-responsive">
                                   <div class="row">
                                        <div class="col-4" style="font-size:large;">
                                            ผลตอบแทนแบบ Fix                                           
                                        </div>
                                        <div class="col-8" style="text-align:right">
                                            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModalLong">
                                              Launch demo modal
                                            </button>
                                            <asp:Button ID="btnFix" class="btn btn-sm  btn-primary" runat="server" Text=" + เพิ่มเงื่อนไข" />
                                        </div>
                                    </div>
                                <asp:GridView ID="gvPaymentFix"  
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    emptydatatext=" ไม่มีข้อมูล " 
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#CCCCFF" />
                                    <Columns>
                                      <asp:TemplateField HeaderText="ID">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblflexid" runat="server" Text='<%#Eval("fixid")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Type">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblpaymenttype" runat="server" Text='<%#Eval("paymenttype")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="รอบ">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblrecuring" runat="server" Text='<%#Eval("recuringtype")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ความถี่">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblfrequency" runat="server" Text='<%#Eval("frequency")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="เริ่ม">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblbegindate" runat="server" Text='<%#Eval("begindate")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="สิ้นสุด">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblenddate" runat="server" Text='<%#Eval("enddate")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ยอดเงิน">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblamount" runat="server" Text='<%#Eval("amount")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>                          
                                                <asp:HyperLink ID="HyperLink" runat="server" NavigateUrl='<%#Eval("link")%>' Text="" data-toggle="tooltip" data-placement="right" title="View Detail"><i class="fa fa-eye" aria-hidden="true"></i></asp:HyperLink>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                              </asp:GridView>
                                               
                            </div><!-- end Table payment fix-->

                              <div class="table-responsive">
                                   <div class="row">
                                        <div class="col-8" style="font-size:large;">
                                            ข้อมูลการจ่ายเงิน                                           
                                        </div>
                                        <div class="col-4" style="text-align:right">
                                            <asp:Button ID="btnPayment" class="btn btn-sm  btn-primary" style="background-color:#00cc66"  runat="server" Text=" + เพิ่มข้อมูล" />
                                        </div>
                                    </div>
                                <asp:GridView ID="gvPayment"  
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    emptydatatext=" ไม่มีข้อมูล " 
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#00cc66" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#ccffcc" />

                                      <Columns>
	                                    <asp:TemplateField HeaderText="ID">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblid" runat="server" Text='<%#Eval("paymentid")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ช่องทาง">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblpaidtype" runat="server" Text='<%#Eval("paidtype")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Bank">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblbankcode" runat="server" Text='<%#Eval("bankcode")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Branch Code">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblbankbranchcode" runat="server" Text='<%#Eval("bankbranchcode")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Branch Name">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblbankbranchname" runat="server" Text='<%#Eval("bankbranchname")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="account">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblaccountno" runat="server" Text='<%#Eval("accountno")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Name">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblaccountname" runat="server" Text='<%#Eval("accountname")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>                          
                                                <asp:HyperLink ID="HyperLink" runat="server" NavigateUrl='<%#Eval("link")%>' Text="" data-toggle="tooltip" data-placement="right" title="View Detail"><i class="fa fa-eye" aria-hidden="true"></i></asp:HyperLink>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                              </asp:GridView>
                                               
                            </div><!-- end Table finance info-->

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

        <!-- Modal -->
        <div class="modal fade" id="exampleModalLong" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
          <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLongTitle">Modal title</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                ...
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                <button type="button" class="btn btn-primary">Save changes</button>
              </div>
            </div>
          </div>
        </div>

    </div>
    <!-- /#wrapper -->

  <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>

    <script type="text/javascript">
        jQuery('[id$=txtContractActiveDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format:'d/m/Y'
        });
    </script>

    <script type="text/javascript">
        jQuery('[id$=txtContractDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
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
