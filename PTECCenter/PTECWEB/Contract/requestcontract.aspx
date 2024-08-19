<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="requestcontract.aspx.vb" Inherits="PTECCENTER.requestcontract" EnableEventValidation = "false" Culture="th-TH"  %>

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
                    <div class="col-8 ">
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text="ขอสัญญาใหม่" />  
                        <asp:Button ID="btnReEdit" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text="ขอแก้ไขสัญญา" /> 
                        <asp:Button ID="btnReNote" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text="บันทึกข้อตกลง" /> 
                        <asp:Button ID="btnPrint" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text="Print" /> 
                        <%--<button type="button" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" id="btnAddAssetType" runat="server" 
                         data-toggle="modal" data-target="#ModalReEdit" data-backdrop="static" data-keyboard="false" data-whatever="new">เพิ่มประเภท</button>--%>
                    </div>
                    <div class="col-4" style="text-align:right">
                        <asp:Button ID="btnRefresh" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text="Refresh" />  
                    </div>
                </div>

                          <div class="card-body">
<%--                    <div class="col-12 text-right">
                            <asp:Button ID="btnselectall" class="btn btn-sm  btn-success" runat="server" Text="Select All" />&nbsp;  
                            <asp:Button ID="btnunselect" class="btn btn-sm  btn-danger" runat="server" Text="Un Select All" />  
                    </div>--%>

                               <div class="row" style="padding-top: 0.2rem;">
                                    <div class="col-2">
                                        <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                            <span class="input-group-text">วันที่</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtBegindate" style="background-color:white" runat="server"></asp:TextBox>    
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                            <span class="input-group-text">ถึงวันที่</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtEnddate" style="background-color:white" runat="server"></asp:TextBox>    
                                        </div>
                                    </div>                                           
                                   <div class="col-2">
                                                <div class="input-group sm-3">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">สถานะ</span>
                                                    </div>
                                                    <asp:DropDownList class="form-control" ID="cboStatus"  runat="server" ></asp:DropDownList>    
                                                </div>
                                   </div>                                                         
                               </div>

                              <div class="row" style="padding-top: 0.2rem;">
                                   <div class="col-2">
                                        <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                 <span class="input-group-text">เลขที่สัญญา</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtContractno" runat="server"></asp:TextBox>
                                        </div>
                                   </div>
                                  <div class="col-2">
                                      <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                 <span class="input-group-text">คู่สัญญา</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtVendor" runat="server" ></asp:TextBox>
                                      </div>
                                  </div>
                                  <asp:Button ID="btnFind" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" Find " />
                             </div>
                              <hr style="height:2px;border-width:0;color:gray;background-color:gray">

                              <div class="table-responsive">
                                <asp:GridView ID="gvData"  
                                        class="table table-striped table-bordered" 
                                        AllowSorting="True" 
                                        allowpaging="false"
                                        AutoGenerateColumns="false" 
                                        emptydatatext=" ไม่มีข้อมูล " 
                                        OnRowDataBound="OnRowDataBound"
                                         OnSelectedIndexChanged="OnSelectedIndexChanged"
                                        runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#CCCCFF" />

                                          <Columns>	                                  
                                              <asp:BoundField DataField="DocuNo" HeaderText="เลขที่เอกสาร"  />
                                              <asp:BoundField DataField="ContractNo" HeaderText="เลขที่สัญญา"  />
	                                        <asp:TemplateField HeaderText="สาขา">
		                                        <ItemTemplate>
			                                        <asp:Label id="lblBranch" runat="server" Text='<%#Eval("Branch")%>'></asp:Label>
		                                        </ItemTemplate>
	                                        </asp:TemplateField>
	                                        <asp:TemplateField HeaderText="กลุ่มสัญญา">
		                                        <ItemTemplate>
			                                        <asp:Label id="lblContactType" runat="server" Text='<%#Eval("AgType")%>'></asp:Label>
		                                        </ItemTemplate>
	                                        </asp:TemplateField>
	                                        <asp:TemplateField HeaderText="ประเภทสัญญา">
		                                        <ItemTemplate>
			                                        <asp:Label id="lblContactType" runat="server" Text='<%#Eval("SubConName")%>'></asp:Label>
		                                        </ItemTemplate>
	                                        </asp:TemplateField>
	                                        <asp:TemplateField HeaderText="สถานะ">
		                                        <ItemTemplate>
			                                        <asp:Label id="lblStatusName" runat="server" Text='<%#Eval("StatusName")%>'></asp:Label>
                                                    <%--<asp:button id="btnStatus" class="btn btn-outline-info w-10 noEnterSubmit" runat="server" Text='update'></asp:button>--%>
		                                        </ItemTemplate>
	                                        </asp:TemplateField>
	                                        <asp:TemplateField HeaderText="วันที่เริ่ม">
		                                        <ItemTemplate>
			                                        <asp:Label id="lblBegindate" runat="server" Text='<%#Eval("Begindate")%>'></asp:Label>
		                                        </ItemTemplate>
	                                        </asp:TemplateField>
	                                        <asp:TemplateField HeaderText="สิ้นสุดวันที่">
		                                        <ItemTemplate>
			                                        <asp:Label id="lblEndDate" runat="server" Text='<%#Eval("EndDate")%>'></asp:Label>
		                                        </ItemTemplate>
	                                        </asp:TemplateField>
	                                        <asp:TemplateField HeaderText="คู่สัญญา">
		                                        <ItemTemplate>
			                                        <asp:Label id="lblCustName" runat="server" Text='<%#Eval("CustName")%>'></asp:Label>
		                                        </ItemTemplate>
	                                        </asp:TemplateField>
	                                        <asp:TemplateField HeaderText="วันที่ขอ">
		                                        <ItemTemplate>
			                                        <asp:Label id="lblCreateDate" runat="server" Text='<%#Eval("CreateDate")%>'></asp:Label>
		                                        </ItemTemplate>
	                                        </asp:TemplateField>
	                                        <asp:TemplateField HeaderText="ผู้ขอ">
		                                        <ItemTemplate>
			                                        <asp:Label id="lblCreateBy" runat="server" Text='<%#Eval("CreateBy")%>'></asp:Label>
		                                        </ItemTemplate>
	                                        </asp:TemplateField>
                                               <asp:BoundField DataField="ID" HeaderText="ID"  />

                                        </Columns>
                              </asp:GridView>
                                               
                            </div>
                          </div><!-- end card-body-->


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
        jQuery('[id$=txtBegindate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            lang: 'th',// แสดงภาษาไทย
            yearOffset: 543,// ใช้ปี พ.ศ. บวก 543 เพิ่มเข้าไปในปี ค.ศ  
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>

    <script type="text/javascript">
        jQuery('[id$=txtEnddate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            lang: 'th',// แสดงภาษาไทย
            yearOffset: 543,// ใช้ปี พ.ศ. บวก 543 เพิ่มเข้าไปในปี ค.ศ  
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>

</asp:Content>
