<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="requestcontract.aspx.vb" Inherits="PTECCENTER.requestcontract" EnableEventValidation = "false" Culture="th-TH"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">

     <%--   <script type="text/javascript" src="jquery.js"></script>
    <link href="assets/bootstrap-datepicker-thai/css/datepicker.css" rel="stylesheet">
    <script type="text/javascript" src="bootstrap-datepicker-thai/js/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="bootstrap-datepicker-thai/js/bootstrap-datepicker-thai.js"></script>
    <script type="text/javascript" src="bootstrap-datepicker-thai/js/locales/bootstrap-datepicker.th.js"></script>
--%>


    <style>

        /*####################### CSS FROM MODAL ########################*/
        .modal .modal-body {
            padding: 2rem;
            padding-top: 1rem;
        }

        .modal .form-group, .modal .form-control, .modal .bootstrap-select .dropdown-toggle, .modal .bootstrap-select .dropdown-menu {
            font-size: 0.875rem;
        }

        .modal-body .btn-light.disabled, .modal-body .btn-light:disabled {
            background-color: #e9ecef;
            border-color: #ced4da;
        }

        .modal .showCost {
            background-color: #f7faff;
            padding: 1rem;
            font-size: .9rem;
        }

        .modal img {
            display: none;
            background: none;
            border: 0;
        }

        .modal a:hover img {
            width: 100%;
            height: auto;
            position: absolute;
            left: 30%;
            top: -1200%;
            display: block;
            z-index: 999;
        }
        /*####################### END CSS FROM MODAL ########################*/
        .styleSpan
        {
            padding: 10px;
            margin: 10px;
            background-color: #f00;
            color: #fff;
        }
        .spantagClass {
                display: inline-block;
                float: right;
                /*background-color: #f00;*/
                color: blueviolet;
                border-style:outset;
                border-width: thin;
                font-family: Verdana, Geneva, Tahoma, sans-serif;
                font-size:large;
                width:100%;
                padding: 10px;
                margin: 10px;
        }
        .span {
            color: green;
            text-decoration: underline;
            font-style: italic;
            font-weight: bold;
            font-size: 16px;
        }
        .span2 {
            color: green;
            text-decoration: underline;
            font-style: italic;
            font-weight: bold;
            font-size: 12px;
        }
        .menuTabs
        {
            position:relative;
            top:1px;
            left:10px;
        }
    </style>


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
                           <a href="client_list.aspx"><i class="fa fa-tasks" aria-hidden="true"></i></a> ขอสัญญาใหม่
                  </li>
                </ol>
                <p></p>
                <div class="row">
                    <div class="col-6">
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" New " />
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" Save " />
                        <asp:Button ID="btnDel" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" Delete " />
                        <button type="button" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" id="btnFromAddDetail" runat="server" 
                            data-toggle="modal" data-target="#exampleModal" data-backdrop="static" data-keyboard="false" data-whatever="new">เพิ่มสาขาใหม่</button>
                      
                    </div>
                    <div class="col-6" style="text-align:right">
                        <asp:Button ID="BtnContract" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" กลับ สัญญา " />
                    </div>
                </div>
                 
                <div class="list-group-item list-group-item-light">

                                  <div class="row" style="padding-top: 1rem; " runat="server">
                                        <span class="span" style="color:blue" runat="server">
                                          ค้นหาขอข้อมูลสัญญาใหม่
                                        </span>
                                  </div>

                        <div class="row">
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
                                <asp:Button ID="btnFind" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" Find " />
                          
                        </div>
                    <br />
                        <div class="row">
                            <div class="col-12">
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
	                                        <asp:TemplateField HeaderText="สาขา">
		                                        <ItemTemplate>
			                                        <asp:Label id="lblBranch" runat="server" Text='<%#Eval("Branch")%>'></asp:Label>
		                                        </ItemTemplate>
	                                        </asp:TemplateField>
	                                        <asp:TemplateField HeaderText="ประเภทสัญญา">
		                                        <ItemTemplate>
			                                        <asp:Label id="lblContactType" runat="server" Text='<%#Eval("AgType")%>'></asp:Label>
		                                        </ItemTemplate>
	                                        </asp:TemplateField>
	                                        <asp:TemplateField HeaderText="สถานะ">
		                                        <ItemTemplate>
			                                        <asp:Label id="lblStatusName" runat="server" Text='<%#Eval("StatusName")%>'></asp:Label>
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
                            </div>
                      

                        </div>
                </div>
                <hr style="height:2px;border-width:0;color:gray;background-color:gray">

<!---->
                <hr />


    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">รายละเอียดสาขาใหม่</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <!--  ##############  Detail ############### -->
                    <input type="hidden" class="form-control" id="row" value="0" runat="server">
                    <input type="hidden" class="form-control" id="nextrow" value="0" runat="server">
                    <input type="hidden" class="form-control" id="hiddenAdvancedetailid" value="0" runat="server">
                    <div class="form-group d-none">
                        <asp:Label ID="lbtxtdocdate" CssClass="form-label" AssociatedControlID="txtdocdate" runat="server" Text="วันที่เอกสาร" />
                        <asp:TextBox class="form-control noEnterSubmit" type="input" ID="txtdocdate" runat="server" autocomplete="off"></asp:TextBox>
                    </div>
         <%--           <div class="form-group">
                        <asp:Label ID="lblBranch" CssClass="form-label" AssociatedControlID="lblBranch" runat="server" Text="รหัสสาขา" />
                        <asp:TextBox class="form-control noEnterSubmit" type="input" ID="txtBranchCode" runat="server" autocomplete="off"></asp:TextBox>
                    </div>--%>
                   
                        <div class="form-group">    
                                        <%-- <span class="btn btn-sm  btn-outline-info w-20 noEnterSubmit">รหัสสาขา</span>--%>
                                   <asp:Label ID="lblBranchNew"  Class="control-label" AssociatedControlID="lblBranchNew" runat="server" Text="*รหัสสาขา" />                                  
                                   <asp:TextBox class="form-control noEnterSubmit" type="input" ID="txtBranchCodeNew" runat="server" autocomplete="off"></asp:TextBox>   
                                 
                        </div>

                        <div class="form-group">                                  
                                   <asp:Label ID="lblAddrNew"  Class="control-label" AssociatedControlID="lblAddrNew" runat="server" Text="* เลขที่" />
                                   <asp:TextBox class="form-control noEnterSubmit" type="input" ID="txtAddr" runat="server" autocomplete="off"></asp:TextBox>  
                                                        
                        </div>
                 
                        <div class="form-group">    
                                   <asp:Label ID="lblSubDistrictNew"  Class="control-label" AssociatedControlID="lblSubDistrictNew" runat="server" Text="* ตำบล" />
                                   <asp:TextBox class="form-control noEnterSubmit" type="input" ID="txtSubDistrictNew" runat="server" autocomplete="off"></asp:TextBox>   
                                                          
                        </div>

                        <div class="form-group">     
                                   <asp:Label ID="lblDistrictNew"  Class="control-label" AssociatedControlID="lblDistrictNew" runat="server" Text="* อำเภอ" />
                                   <asp:TextBox class="form-control noEnterSubmit" type="input" ID="txtDistrictNew" runat="server" autocomplete="off"></asp:TextBox>   
                                                  
                        </div>

                        <div class="form-group">     
                                         <%--<span class="btn btn-sm  btn-outline-info w-20 noEnterSubmit">จังหวัด</span>--%>
                                   <asp:Label ID="lblProvinceNew"  Class="control-label" AssociatedControlID="lblProvinceNew" runat="server" Text="* จังหวัด" />
                                   <asp:TextBox class="form-control noEnterSubmit" type="input" ID="txtProvinceNew" runat="server" autocomplete="off"></asp:TextBox>   
                                                          
                        </div>

                        <div class="form-group">   
                                         <%--<span class="btn btn-sm  btn-outline-info w-20 noEnterSubmit">รหัสไปรษณีย์</span>--%>
                                   <asp:Label ID="lblPostCodeNew"  Class="control-label" AssociatedControlID="lblPostCodeNew" runat="server" Text="* รหัสไปรษณีย์" />
                                   <asp:TextBox class="form-control noEnterSubmit" type="input" ID="txtPostCodeNew" runat="server" autocomplete="off"></asp:TextBox>   
                                                          
                        </div>

                        <div class="form-group">                                    
                                   <asp:Label ID="lblTelNew"  Class="control-label" AssociatedControlID="lblTelNew" runat="server" Text="เบอร์โทร" />
                                   <asp:TextBox class="form-control noEnterSubmit" type="input" ID="txtTelNew" runat="server" autocomplete="off"></asp:TextBox>   
                                                          
                        </div>

                        <div class="form-group">                                       
                                   <asp:Label ID="lblContractNew"  Class="control-label" AssociatedControlID="lblContractNew" runat="server" Text="ผู้ติดต่อ" />
                                   <asp:TextBox class="form-control noEnterSubmit" type="input" ID="txtContractNew" runat="server" autocomplete="off"></asp:TextBox>   
                                                          
                        </div>

                        <div class="form-group">
                            <asp:Label ID="lblRemarkNew" CssClass="form-label" AssociatedControlID="lblRemarkNew" runat="server" Text="หมายเหตุ" />
                            <asp:TextBox class="form-control" ID="txtRemarkNew" runat="server" Rows="3" Columns="50" TextMode="MultiLine" onkeyDown="checkTextAreaMaxLength(this,event,'255');" autocomplete="off"></asp:TextBox>
                            <div class="invalid-feedback">กรุณากรอกรายละเอียด</div>
                        </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" data-dismiss="modal">Close</button>              
                    <asp:Button ID="btnAddBranch" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text="Save" OnClientClick="AddNew();" />

                </div>
            </div>
        </div>
    </div>

<!---->

<!---->

    <div class="modal fade" id="ModalAssetType" tabindex="-1" role="dialog" aria-labelledby="ModalAssetLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="ModalAssetLabel">ประเภททรัพย์สิน</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <!--  ##############  Detail ############### -->
                    <input type="hidden" class="form-control" id="Hidden1" value="0" runat="server">
                    <input type="hidden" class="form-control" id="Hidden2" value="0" runat="server">
                    <input type="hidden" class="form-control" id="hidden3" value="0" runat="server">
                    <div class="form-group d-none">
                        <asp:Label ID="Label4" CssClass="form-label" AssociatedControlID="txtdocdate" runat="server" Text="วันที่เอกสาร" />
                        <asp:TextBox class="form-control noEnterSubmit" type="input" ID="TextBox41" runat="server" autocomplete="off"></asp:TextBox>
                    </div>
        
                        <div class="form-group">     
                                   <asp:Label ID="lblAssetType"  Class="control-label" AssociatedControlID="lblAssetType" runat="server" Text="ประเภททรัพย์สิน" />
                                   <asp:TextBox class="form-control noEnterSubmit" type="input" ID="txtAssetType" runat="server" autocomplete="off"></asp:TextBox>   
                                                  
                        </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" data-dismiss="modal">Close</button>              
                    <asp:Button ID="btnAddTypeAsset" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text="Save" OnClientClick="AddNew();" />

                </div>
            </div>
        </div>
    </div>

<!---->


                <div class="card-body" style="background-color:white ">

                        <div class="row">
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                        <span class="input-group-text">เลขที่ขอสัญญา</span>
                                        </div>
                                        <asp:TextBox class="form-control" ID="txtdocuno" placeholder="Document No" ReadOnly="true" runat="server" ></asp:TextBox>    

                                    </div>
                                </div>
                                    <div class="col-4">
                                           
                                                <div class="input-group sm-3">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">สถานะ</span>
                                                    </div>
                                                    <asp:DropDownList class="form-control" ID="cboStatus"  runat="server" ></asp:DropDownList>    
                                                </div>
                                           
                                    </div>
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                        <span class="input-group-text">สถานะ</span>
                                        </div>
                                        <asp:Label class="form-control" ID="lblStatus" style="background-color:darkgreen;color:white" runat="server" ></asp:Label>    

                                    </div>
                                </div>
                            </div>

                            <%-- input area --%>
                              <div class="card-body" runat="server">
                      
                                    <div class="row" >
                                            <div class="col-3" runat="server">
                                                <div class="input-group sm-3" runat="server">
                                                    <div class="input-group-prepend" runat="server">
                                                    <span class="input-group-text">รหัสสาขา</span>
                                                    </div>
                                                        <asp:DropDownList ID="cboBranch" class="form-control" runat="server" AutoPostBack="true" ></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-3" runat="server">
                                                <div class="input-group sm-3" runat="server">
                                                    <div class="input-group-prepend" runat="server">
                                                    <span class="input-group-text" runat="server">ประเภทสัญญา</span>
                                                    </div>
                                                    <asp:DropDownList class="form-control" ID="cboContractType"  runat="server" ></asp:DropDownList>    
                                                </div>
                                                <div class="input-group sm-3" runat="server">
                                                    <div class="input-group-prepend" runat="server">
                                                    <span class="input-group-text" runat="server">ประเภทสัญญา</span>
                                                    </div>
                                                    <asp:DropDownList class="form-control" ID="DropDownList1"  runat="server" ></asp:DropDownList>    
                                                </div>
                                            </div>

                                            <div class="col-2" runat="server">
                                                <div class="input-group sm-2" runat="server">
                                                    <div class="input-group-prepend" runat="server">
                                                    <span class="input-group-text" runat="server">เลขที่สัญญา</span>
                                                    </div>
                                                     <asp:TextBox class="form-control" ID="txtContractNo" runat="server" ></asp:TextBox>   
                                                </div>
                                            </div>

                                             <div class="col-2" runat="server">
                                                <div class="input-group sm-2" runat="server">
                                                    <div class="input-group-prepend" runat="server">
                                                    <span class="input-group-text" runat="server"> วันที่เริ่มสัญญา</span>
                                                    </div>
                                                    <asp:TextBox class="form-control" ID="txtContractBeginDate" style="background-color:white" runat="server"></asp:TextBox>    

                                                </div>
                                            </div>

                                             <div class="col-2" >
                                                <div class="input-group sm-2" runat="server">
                                                    <div class="input-group-prepend" runat="server">
                                                    <span class="input-group-text" >วันที่สิ้นสุดสัญญา</span>
                                                    </div>
                                                    <asp:TextBox class="form-control" ID="txtContractEndDate" style="background-color:white" runat="server"></asp:TextBox>    

                                                </div>
                                            </div>
                                    </div>
                                   <div class="row" style="padding-top: 0.2rem;" runat="server" >
                                        <div class="col-2" runat="server">
                                            <div class="input-group sm-3" runat="server">
                                                <div class="input-group-prepend" runat="server">
                                                    <span class="input-group-text" runat="server">อายุสัญญา</span>
                                                </div>
                                                <asp:TextBox class="form-control" ID="txtcontractPeriod" style="background-color:white" runat="server" ReadOnly="true" ></asp:TextBox>   
                                                <div class="input-group-prepend" runat="server">
                                                    <span class="input-group-text" runat="server">ปี</span>
                                                </div>                                
                                            </div>
                                        </div>

                                        <div class="col-2" runat="server">
                                            <div class="input-group sm-3" runat="server">
                                                <asp:TextBox class="form-control" ID="txtcontractPeriod2" style="background-color:white" runat="server" ReadOnly="true" ></asp:TextBox>   
                                                <div class="input-group-prepend" runat="server">
                                                    <span class="input-group-text" runat="server">เดือน</span>
                                                </div>                                
                                            </div>
                                        </div>
                                        <div class="col-4">
                                            <div class="input-group sm-3">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">กำหนดจดทะเบียนเช่าสถานี ณ จังหวัด</span>
                                                </div>
                                                <asp:TextBox class="form-control" ID="txtRentalReg" style="background-color:white" runat="server"></asp:TextBox>                                                              
                                            </div>
                                        </div>

                                   </div>
<!---->
                            <div class="card-body">

 
                                <span style="color:blue">รายละเอียดสัญญา</span>
                                <br />
                                <asp:Menu ID="Menu1" Width="100%" runat="server" orientation="Horizontal" StaticEnableDefaultPopOutImage="False"
                                            OnMenuItemClick="Menu1_MenuItemClick" RenderingMode="Table"  >
                                            <Items>
                                                <asp:MenuItem  Text=" คู่สัญญา(บุคคลธรรมดา) " Value="0"></asp:MenuItem>
                                                <asp:MenuItem  Text=" คู่สัญญา(นิติบุคคล) " Value="1"></asp:MenuItem>
                                                <asp:MenuItem  Text=" ทรัพย์สิน " Value="4"></asp:MenuItem>
                                                <asp:MenuItem  Text=" เงือนไขค่าสัญญา(จ่ายแบบคงที่) " Value="2"></asp:MenuItem>
                                                <asp:MenuItem  Text=" ช่องทางการชำระเงิน " Value="3"></asp:MenuItem>
                                                <asp:MenuItem  Text=" ร่วมแนบ1 " Value="8"></asp:MenuItem>
                                                <asp:MenuItem  Text=" ร่วมแนบ2 " Value="5"></asp:MenuItem>
                                                <asp:MenuItem  Text=" ร่วมแนบ3 " Value="6"></asp:MenuItem>
                                                <asp:MenuItem  Text=" ร่วมแนบ4 " Value="7"></asp:MenuItem>
                                            </Items>
                                     
                                        </asp:Menu>
                                        <hr style="height:2px;border-width:0;color:gray;background-color:gray">
                                        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                            <asp:View ID="Tab1" runat="server" >
                                                <table width="100%"  cellpadding="0" cellspacing="0">
                                                    <tr valign="top">
                                                        <td class="TabArea" style="width: 100% ">

                                                            <!---->

                                                                    <div class="list-group-item list-group-item-light"  runat="server">
                                                                                      <div class="row" style="padding-top: 1rem; " runat="server">
                                                                                            <span class="span" style="color:blue" runat="server">
                                                                                              คู่สัญญา(บุคคลธรรมดา)
                                                                                            </span>
                                                                                      </div>
                               

                                                                                        <div class="row" style="padding-top: 1rem; " runat="server">
                                                                                            <div class="col-md-3" runat="server">                                                
                                                                                                <div class="input-group sm-3" runat="server">
                                                                                                        <div class="input-group-prepend" runat="server">
                                                                                                            <span class="input-group-text" runat="server">* ชื่อ-นามสกุล</span>
                                                                                                        </div>
                                                                                                    <asp:TextBox class="form-control" ID="txtName" runat="server" ></asp:TextBox>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-3" runat="server">                                                
                                                                                                <div class="input-group sm-3" runat="server" >
                                                                                                        <div class="input-group-prepend" runat="server">
                                                                                                            <span class="input-group-text"  runat="server" >* เลขที่่บัตรประจำตัวประชาชน</span>
                                                                                                        </div>
                                                                                                    <asp:TextBox class="form-control" ID="txtCardID" runat="server" ></asp:TextBox>
                                                                                                </div>
                                                                                            </div>

                                                                                            <div class="col-md-3" runat="server">                                                
                                                                                                <div class="input-group sm-3" runat="server" >
                                                                                                        <div class="input-group-prepend" runat="server">
                                                                                                            <span class="input-group-text" >* เลขที่ผู้เสียภาษี</span>
                                                                                                        </div>
                                                                                                    <asp:TextBox class="form-control" ID="txtTaxID" runat="server" ></asp:TextBox>
                                                                                                </div>
                                                                                            </div>

                                                                                            <div class="col-md-3" runat="server">                                                 
                                                                                                <div class="input-group sm-3" runat="server">
                                                                                                        <div class="input-group-prepend"  runat="server">
                                                                                                            <span class="input-group-text" runat="server">เพศ</span>
                                                                                                        </div>
                                                                                                    <asp:dropdownlist class="form-control" ID="cboSex" runat="server" ></asp:dropdownlist>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row" style="padding-top: 1rem;" runat="server">                                       
                                                                                                <div class="col-md-12" runat="server">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server"> 
                                                                                                                <span class="input-group-text" runat="server">* ที่อยู่</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtAddress" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                        </div>
                                                                                        <div class="row" style="padding-top: 1rem;">                                                                               
                                                                                                <div class="col-md-3" runat="server">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text" runat="server">* ตำบล/แขวง</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtSubDistrict" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-3" runat="server">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text" runat="server">* อำเภอ/เขต</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtDistrict" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-3" runat="server">                                                
                                                                                                    <div class="input-group sm-3" >
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text" runat="server">* จังหวัด</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtProvince" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                       
                                       
                                                                                                <div class="col-md-3" runat="server">                                                
                                                                                                    <div class="input-group sm-3" runat="server"> 
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text "  runat="server">* รหัสไปรษณีย์</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtPostcode" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                     <asp:TextBox  style="display:none" class="form-control" ID="txtDocAction" runat="server" ></asp:TextBox>
                                                                                                     <asp:TextBox  style="display:none" class="form-control" ID="txtDocIDAction" runat="server" ></asp:TextBox>
                                                                                                </div>
                                      
                                                                                                <div class="col-md-3" runat="server" style="padding-top: 1rem;">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text" runat="server">เบอร์โทร</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtPerTel" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                        
                                                                                                <div class="col-md-3" runat="server" style="padding-top: 1rem;">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text" runat="server">Line</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtPerLine" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>

                                                                                                <div class="col-md-3" runat="server" style="padding-top: 1rem;">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text"  runat="server">e-Mail</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtPerEmail" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>

                                                                                        </div>


                                                                                              <div class="row col-sm-3 span2" style="padding-top: 1rem;" runat="server">
                                                                                               <%--     <span class="span2" style="color:blue" runat="server">--%>
                                                                                                      ที่อยู่ส่งเอกสาร
                                                                                                    <%--</span>--%>
                                                                                              </div>

                                                                                        <div class="row" style="padding-top: 1rem;" runat="server">                                       
                                                                                                <div class="col-md-12" runat="server">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server"> 
                                                                                                                <span class="input-group-text" runat="server">ที่อยู่</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtSendAddr" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                        </div>

                                                                                        <div class="row" style="padding-top: 1rem;">
                                                                                                                                                                        
                                                                                                <div class="col-md-3" runat="server">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text" runat="server">ตำบล/แขวง</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtSendSubdistrict" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-3" runat="server">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text" runat="server">อำเภอ/เขต</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtSendDistrict" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-3" runat="server">                                                
                                                                                                    <div class="input-group sm-3" >
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text" runat="server">จังหวัด</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtSendProvince" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                       
                                       
                                                                                                <div class="col-md-3" runat="server">                                                
                                                                                                    <div class="input-group sm-3" runat="server"> 
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text ">รหัสไปรษณีย์</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtSendPostCode" runat="server" ></asp:TextBox>
                                                                                                    </div>                                                                             
                                                                                                </div>
                                      
                                                                                                <div class="col-md-3" runat="server" style="padding-top: 1rem;">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text" runat="server">เบอร์โทร</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtSendTel" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                        
                                                                                        </div>

                                                                                              <div class="row col-sm-3 span2" style="padding-top: 1rem;" runat="server">
                                                                                               <%--     <span class="span2" style="color:blue" runat="server">--%>
                                                                                                      ผู้ติดต่อ
                                                                                                    <%--</span>--%>
                                                                                              </div>

                                                                                        <div class="row" style="padding-top: 1rem;">

                                                                                                <div class="col-md-3" runat="server">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text" runat="server">ผู้ติดต่อ</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtContractPer" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-3" runat="server">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text" runat="server">ความสัมพันธ์</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="TxtRelationPer" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-3" runat="server">                                                
                                                                                                    <div class="input-group sm-3" >
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text" runat="server">เบอร์โทร</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtContractTelPer" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>

                                                                                        </div>

                                                                                      <div class="row col-md-3" style="padding-top: 1rem;"  runat="server">
                                                                                        <asp:Button ID="btnAddPerCon" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" เพิ่มคู่สัญญา " />
                                                                                        <asp:Button ID="btnEditPerCon" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" แก้คู่สัญญา " />
                                                                                      </div>

                                                                                    <!---->
                                                                                                    <div class="card-body">

                                                                                                        <div class="table-responsive"  runat="server">
                                                                                                            <asp:GridView ID="gvContractPer"
                                                                                                                class="table table-striped table-bordered"
                                                                                                                AllowSorting="True"
                                                                                                                AllowPaging="false"
                                                                                                                AutoGenerateColumns="false"
                                                                                                                EmptyDataText=" ไม่มีข้อมูล "                                     
                                                                                                                OnRowDataBound="OnRowDataBoundPer"
                                                                                                                OnSelectedIndexChanged="OnSelectedIndexChangedPer"
                                                                                                                runat="server" CssClass="table table-striped">
                                                                                                                <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-Size="Smaller" ForeColor="White" />
                                                                                                                <AlternatingRowStyle BackColor="#CCCCFF" />

                                                                                                                <Columns>
                                                                                                                    <asp:BoundField DataField="PersonName" HeaderText="ชื่อ-สกุล" />
                                                                                                                    <asp:BoundField DataField="IDCard" HeaderText="เลขที่บัตรประชาชน" />
                                                                                                                    <asp:BoundField DataField="TaxID" HeaderText="เลขที่ผู้เสียภาษี" />
                                                                                                                    <asp:BoundField DataField="Addr" HeaderText="เลขที่" />
                                                                                                                    <asp:BoundField DataField="SubDistrict" HeaderText="ตำบล/แขวง" />
                                                                                                                    <asp:BoundField DataField="District" HeaderText="อำเภอ/เขต" />
                                                                                                                    <asp:BoundField DataField="Province" HeaderText="จังหวัด" />
                                                                                                                    <asp:BoundField DataField="PostCode" HeaderText="รหัสไปรษณีย์" />
                                                                                                                    <asp:BoundField DataField="Tel" HeaderText="เบอร์โทร" />
                                                                                                                    <asp:BoundField DataField="Line" HeaderText="Line" />
                                                                                                                    <asp:BoundField DataField="Email" HeaderText="E-Mail" />
                                                                                                                    <asp:BoundField DataField="ID" HeaderText="ID" />
                                                                                                                    <asp:BoundField DataField="ItemNo" HeaderText="ItemNo" />


                                                                                                                </Columns>
                                                                                                            </asp:GridView>

                                                                                                        </div>
                                                                                                        <!-- end Table payment-->

                                                                                                    </div>
                                                                                    <!---->
                                                                                                      
                                                                            </div>

                                                            <!---->

                                                        </td>
                                                    </tr>
                                                </table>
                                               <%-- <strong><span style="font-size: 14pt">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                    &nbsp; &nbsp;
                                                    <br/>
                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Home page content...</span></strong>--%>

                                            </asp:View>
                                            <asp:View ID="Tab2" runat="server">
                                                <table width="100%"  cellpadding="0" cellspacing="0"  runat="server">
                                                    <tr valign="top">
                                                        <td class="TabArea" style="width: 100%"  runat="server"> 

                                                                  <div class="row" style="padding-top: 1rem; " runat="server">
                                                                        <span class="span" style="color:blue" runat="server">
                                                                          คู่สัญญา(นิติบุคคล)
                                                                        </span>
                                                                  </div>
                                                                  <div class="card-body" style="background-color:silver  "  runat="server">
                                                                      <div class="row" style="padding-top: 1rem;"  runat="server">
                                                                            <div class="col-md-3"  runat="server">                                                
                                                                                <div class="input-group sm-3"  runat="server">
                                                                                        <div class="input-group-prepend"  runat="server">
                                                                                            <span class="input-group-text"  runat="server">* ชื่อบริษัท</span>
                                                                                        </div>
                                                                                    <asp:TextBox class="form-control" ID="txtCompany" runat="server" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-2" runat="server">                                                
                                                                                <div class="input-group sm-2" runat="server">
                                                                                        <div class="input-group-prepend" runat="server">
                                                                                            <span class="input-group-text" runat="server">* เลขที่</span>
                                                                                        </div>
                                                                                    <asp:TextBox class="form-control" ID="txtAddrCom" runat="server" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-2" runat="server">                                                
                                                                                <div class="input-group sm-2" runat="server">
                                                                                        <div class="input-group-prepend" runat="server">
                                                                                            <span class="input-group-text" runat="server">* ตำบล/แขวง</span>
                                                                                        </div>
                                                                                    <asp:TextBox class="form-control" ID="txtSubDistrictCom" runat="server" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-2" runat="server">                                                
                                                                                <div class="input-group sm-2" runat="server">
                                                                                        <div class="input-group-prepend" runat="server">
                                                                                            <span class="input-group-text" runat="server">* อำเภอ/เขต</span>
                                                                                        </div>
                                                                                    <asp:TextBox class="form-control" ID="txtDistrictCom" runat="server" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-2" runat="server">                                                
                                                                                <div class="input-group sm-2" >
                                                                                        <div class="input-group-prepend" runat="server">
                                                                                            <span class="input-group-text" runat="server">* จังหวัด</span>
                                                                                        </div>
                                                                                    <asp:TextBox class="form-control" ID="txtProvinceCom" runat="server" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-2" style="padding-top: 1rem;"  runat="server">                                               
                                                                                <div class="input-group sm-2"  runat="server">
                                                                                        <div class="input-group-prepend"  runat="server">
                                                                                            <span class="input-group-text"  runat="server">* ไปรษณีย์</span>
                                                                                        </div>
                                                                                    <asp:TextBox class="form-control" ID="txtPostCodeCom" runat="server" ></asp:TextBox>
                                                                                </div>
                                                                            </div>                                       

                                                                            <div class="col-md-3" style="padding-top: 1rem;"  runat="server">                                               
                                                                                <div class="input-group sm-3"  runat="server">
                                                                                        <div class="input-group-prepend"  runat="server">
                                                                                            <span class="input-group-text"  runat="server">Mobile Phone</span>
                                                                                        </div>
                                                                                    <asp:TextBox class="form-control" ID="txtMobile" runat="server" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                  </div>


                                                                  <!---->
                                                <div class="list-group-item list-group-item-light"  runat="server">
                                                                        <div class="col-md-3" runat="server"> 
                                                                              <div class="row" style="padding-top: 1rem; " runat="server">
                                                                                    <span class="span2" style="color:blue" runat="server">
                                                                                      กรรมการบริหาร                                                  
                                                                                    </span>
                                                                              </div>
                                                                        </div>

                                                                    <div class="row" style="padding-top: 1rem; " runat="server">

                                                                        <div class="col-md-3" runat="server">                                                
                                                                            <div class="input-group sm-3" runat="server">
                                                                                    <div class="input-group-prepend" runat="server">
                                                                                        <span class="input-group-text" runat="server">* ชื่อ-นามสกุล</span>
                                                                                    </div>
                                                                                <asp:TextBox class="form-control" ID="txtCustNameComPer" runat="server" ></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3" runat="server">                                                
                                                                            <div class="input-group sm-3" runat="server" >
                                                                                    <div class="input-group-prepend" runat="server">
                                                                                        <span class="input-group-text" >* บัตรประจำตัวประชาชน</span>
                                                                                    </div>
                                                                                <asp:TextBox class="form-control" ID="txtCardIDComPer" runat="server" ></asp:TextBox>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3" runat="server">                                                
                                                                            <div class="input-group sm-3" runat="server" >
                                                                                    <div class="input-group-prepend" runat="server">
                                                                                        <span class="input-group-text" >* เลขที่ผู้เสียภาษี</span>
                                                                                    </div>
                                                                                <asp:TextBox class="form-control" ID="txtTaxIDComPer" runat="server" ></asp:TextBox>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-2" runat="server">                                                 
                                                                            <div class="input-group sm-2" runat="server">
                                                                                    <div class="input-group-prepend" >
                                                                                        <span class="input-group-text" runat="server">เพศ</span>
                                                                                    </div>
                                                                                <asp:dropdownlist class="form-control" ID="cboGenderComPer" runat="server" ></asp:dropdownlist>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row" style="padding-top: 1rem;"  runat="server">  
                                                                            <div class="col-md-2" runat="server">                                                
                                                                                <div class="input-group sm-2" runat="server">
                                                                                        <div class="input-group-prepend" runat="server">
                                                                                            <span class="input-group-text" runat="server">* เลขที่</span>
                                                                                        </div>
                                                                                    <asp:TextBox class="form-control" ID="txtAddrComPer" runat="server" ></asp:TextBox>
                                                                                </div>
                                                                            </div>                                        
                                                                            <div class="col-md-3" runat="server">                                                
                                                                                <div class="input-group sm-3" runat="server">
                                                                                        <div class="input-group-prepend" runat="server">
                                                                                            <span class="input-group-text" runat="server">* ตำบล/แขวง</span>
                                                                                        </div>
                                                                                    <asp:TextBox class="form-control" ID="txtSubDistrictComPer" runat="server" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-3" runat="server">                                                
                                                                                <div class="input-group sm-3" runat="server">
                                                                                        <div class="input-group-prepend" runat="server">
                                                                                            <span class="input-group-text" runat="server">* อำเภอ/เขต</span>
                                                                                        </div>
                                                                                    <asp:TextBox class="form-control" ID="txtDistrictComPer" runat="server" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-2" runat="server">                                                
                                                                                <div class="input-group sm-2" >
                                                                                        <div class="input-group-prepend" runat="server">
                                                                                            <span class="input-group-text" runat="server">* จังหวัด</span>
                                                                                        </div>
                                                                                    <asp:TextBox class="form-control" ID="txtProvinceComPer" runat="server" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                       
                                       
                                                                            <div class="col-md-2" runat="server">                                                
                                                                                <div class="input-group sm-2" runat="server"> 
                                                                                        <div class="input-group-prepend" runat="server">
                                                                                            <span class="input-group-text ">* รหัสไปรษณีย์</span>
                                                                                        </div>
                                                                                    <asp:TextBox class="form-control" ID="txtPostCodeComPer" runat="server" ></asp:TextBox>
                                                                                </div>                                                                              
                                                                            </div>
                                      
                                                                            <div class="col-md-2" runat="server" style="padding-top: 1rem;">                                                
                                                                                <div class="input-group sm-2" runat="server">
                                                                                        <div class="input-group-prepend" runat="server">
                                                                                            <span class="input-group-text" runat="server">เบอร์โทร</span>
                                                                                        </div>
                                                                                    <asp:TextBox class="form-control" ID="txtTel" runat="server" ></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                        
                                                                            <div class="col-md-2" runat="server" style="padding-top: 1rem;">                                                
                                                                                <div class="input-group sm-2" runat="server">
                                                                                        <div class="input-group-prepend" runat="server">
                                                                                            <span class="input-group-text" runat="server">Line</span>
                                                                                        </div>
                                                                                    <asp:TextBox class="form-control" ID="txtLine" runat="server" ></asp:TextBox>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-2" runat="server" style="padding-top: 1rem;">                                                
                                                                                <div class="input-group sm-2" runat="server">
                                                                                        <div class="input-group-prepend" runat="server">
                                                                                            <span class="input-group-text">E-Mail</span>
                                                                                        </div>
                                                                                    <asp:TextBox class="form-control" ID="txtEmail" runat="server" ></asp:TextBox>
                                                                                </div>
                                                                            </div>

                                                                    </div>

                                                                                        <div class="row col-sm-3 span2" style="padding-top: 1rem;" runat="server">
                                                                                               <%--     <span class="span2" style="color:blue" runat="server">--%>
                                                                                                      ที่อยู่ส่งเอกสาร
                                                                                                    <%--</span>--%>
                                                                                        </div>

                                                                                        <div class="row" style="padding-top: 1rem;" runat="server">                                       
                                                                                                <div class="col-md-12" runat="server">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server"> 
                                                                                                                <span class="input-group-text" runat="server">ที่อยู่</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtComAddrSend" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                        </div>

                                                                                        <div class="row" style="padding-top: 1rem;"  runat="server">
                                                                                                                                                                        
                                                                                                <div class="col-md-3" runat="server">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text" runat="server">ตำบล/แขวง</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtComSubdistrictSend" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-3" runat="server">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text" runat="server">อำเภอ/เขต</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtComDistrictSend" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-3" runat="server">                                                
                                                                                                    <div class="input-group sm-3" >
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text" runat="server">จังหวัด</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtComProvinceSend" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                       
                                       
                                                                                                <div class="col-md-3" runat="server">                                                
                                                                                                    <div class="input-group sm-3" runat="server"> 
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text ">รหัสไปรษณีย์</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtComPostCodeSend" runat="server" ></asp:TextBox>
                                                                                                    </div>                                                                             
                                                                                                </div>
                                      
                                                                                                <div class="col-md-3" runat="server" style="padding-top: 1rem;">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text" runat="server">เบอร์โทร</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtComTelSend" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                        
                                                                                        </div>

                                                                                              <div class="row col-sm-3 span2" style="padding-top: 1rem;" runat="server">
                                                                                               <%--     <span class="span2" style="color:blue" runat="server">--%>
                                                                                                      ผู้ติดต่อ
                                                                                                    <%--</span>--%>
                                                                                              </div>

                                                                                        <div class="row" style="padding-top: 1rem;"  runat="server">

                                                                                                <div class="col-md-3" runat="server">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text" runat="server">ผู้ติดต่อ</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtComContractCom" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-3" runat="server">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text" runat="server">ความสัมพันธ์</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtComRelation" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-3" runat="server">                                                
                                                                                                    <div class="input-group sm-3" >
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text" runat="server">เบอร์โทร</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtComTelCom" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>

                                                                                        </div>



                                                                  <!---->
                                                                  <div class="row col-md-3" style="padding-top: 1rem;">
                                                                    <asp:Button ID="btnAddCompanyPer" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" เพิ่มคู่สัญญา " />
                                                                    <asp:Button ID="btnEditCompanyPer" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" แก้คู่สัญญา " />
                                                                  </div>
                                                <%--                  <div class="row" style="padding-top: 1rem;">
                                                                        <div class="table-responsive">
 
                                                                            <div class="table-responsive" runat="server" >
                                                                                <asp:Table id="tblContractCompany" class="table table-bordered table-condensed table-hover pnlstudentDetails" style='font-family:Tahoma, Courier, monospace; font-size:8pt;' 
                                                                                                        runat="server" GridLines="Both" cellspacing="0" cellpadding="5" border="1" OnPageIndexChanging="OnPageIndexChanging" PageSize="10">
                                                                                    <asp:TableHeaderRow TableSection="TableHeader" runat="server">
                                                                                        <asp:TableHeaderCell ColumnSpan="1" style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">คู่สัญญา</asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell ColumnSpan="6" style='margin:0 auto; text-align:center; background-color:yellowgreen;' runat="server">ที่อยู่</asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell ColumnSpan="3" style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">กรรมการ</asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell ColumnSpan="8" style='margin:0 auto; text-align:center; background-color:yellowgreen;' runat="server">ที่อยู่</asp:TableHeaderCell>
                                                                                    </asp:TableHeaderRow>
                                                                                    <asp:TableHeaderRow runat="server">
                                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">บริษัท"</asp:TableHeaderCell>                                                      
                                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:yellowgreen;' runat="server">เลขที่</asp:TableHeaderCell>                                                       
                                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:yellowgreen;' runat="server">ตำบล</asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:yellowgreen;' runat="server">อำเภอ</asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:yellowgreen;' runat="server">จังหวัด</asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:yellowgreen;' runat="server">ไปรษณีย์</asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:yellowgreen;' runat="server">โทร</asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">กรรมการ</asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">บัตรประชาชน</asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">เลขผู้เสียภาษี</asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:yellowgreen;' runat="server">เลขที่</asp:TableHeaderCell>                                                       
                                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:yellowgreen;' runat="server">ตำบล</asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:yellowgreen;' runat="server">อำเภอ</asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:yellowgreen;' runat="server">จังหวัด</asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:yellowgreen;' runat="server">ไปรษณีย์</asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:yellowgreen;' runat="server">โทร</asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:yellowgreen;' runat="server">Line</asp:TableHeaderCell>
                                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:yellowgreen;' runat="server">E-Mail</asp:TableHeaderCell>
                                                                                    </asp:TableHeaderRow>
                                                                                </asp:Table>

                                                                            </div>
                                                                       </div>
                                                                </div>--%>

                                                                <!---->
                                                                                <div class="row" style="padding-top: 1rem;">

                                                                                    <div class="table-responsive">
                                                                                        <asp:GridView ID="gvContractCompanyPer"
                                                                                            class="table table-striped table-bordered"
                                                                                            AllowSorting="True"
                                                                                            AllowPaging="false"
                                                                                            AutoGenerateColumns="false"
                                                                                            EmptyDataText=" ไม่มีข้อมูล "                                     
                                                                                            OnRowDataBound="OnRowDataBoundCompanyPer"
                                                                                            OnSelectedIndexChanged="OnSelectedIndexChangedCompanyPer"
                                                                                            runat="server" CssClass="table table-striped">
                                                                                            <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-Size="Smaller" ForeColor="White" />
                                                                                            <AlternatingRowStyle BackColor="#CCCCFF" />

                                                                                            <Columns>
                                                                                                <asp:BoundField DataField="CompanyName" HeaderText="บริษัท" />
                                                                                                <asp:BoundField DataField="AddrCom" HeaderText="ที่อยู่บริษัท" />
                                                                                                <asp:BoundField DataField="SubDistrictCom" HeaderText="ตำบล/แขวง" />
                                                                                                <asp:BoundField DataField="DistrictCom" HeaderText="อำเภอ/เขต" />
                                                                                                <asp:BoundField DataField="ProvinceCom" HeaderText="จังหวัด" />
                                                                                                <asp:BoundField DataField="PostCodeCom" HeaderText="ไปรษณีย์" />
                                                                                                <asp:BoundField DataField="TelCom" HeaderText="เบอร์โทร" />
                                                                                                <asp:BoundField DataField="CustNamePer" HeaderText="กรรมการ" />
                                                                                                <asp:BoundField DataField="CardIDPer" HeaderText="เลขบัตรประชาชน" />
                                                                                                <asp:BoundField DataField="TaxIDPer" HeaderText="เลขผู้เสียภาษี" />
                                                                                                <asp:BoundField DataField="AddrComPer" HeaderText="เลขที่" />
                                                                                                <asp:BoundField DataField="SubDistrictComPer" HeaderText="ตำบล/แขวง" />
                                                                                                <asp:BoundField DataField="DistrictComPer" HeaderText="อำเภอ/เขต" />
                                                                                                <asp:BoundField DataField="ProvinceComPer" HeaderText="จังหวัด" />
                                                                                                <asp:BoundField DataField="PostCodeComPer" HeaderText="ไปรษณีย์" />
                                                                                                <asp:BoundField DataField="TelComPer" HeaderText="เบอร์โทร" />
                                                                                                <asp:BoundField DataField="LineComPer" HeaderText="Line" />
                                                                                                <asp:BoundField DataField="EmailComPer" HeaderText="E-Mail" />
                                                                                                <asp:BoundField DataField="ID" HeaderText="ID" />
                                                                                                <asp:BoundField DataField="ItemNo" HeaderText="ItemNo" />

                                                                                            </Columns>
                                                                                        </asp:GridView>

                                                                                    </div>
                                                                                    <!-- end Table payment-->

                                                                                </div>
                                                                <!---->
                             
                                                                  <%--<hr style="height:2px;border-width:0;color:gray;background-color:gray">--%>
                                                                

                                                            </div>


                                                        </td>
                                                    </tr>
                                                </table>
                                           <%--     <strong><span style="font-size: 14pt">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                    &nbsp; &nbsp;&nbsp;<br />
                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Products page content...</span></strong>--%>

                                            </asp:View>
                                            <asp:View ID="Tab3" runat="server">
                                                <table width="100%"  cellpadding="0" cellspacing="0">
                                                    <tr valign="top">                           
                                                        <td class="TabArea" style="width: 100%">

                                                                    <div class="col-md-3" runat="server"> 
                                                                          <div class="row" style="padding-top: 1rem; " runat="server">
                                                                                <span class="span" style="color:blue" runat="server">
                                                                                  เงือนไขค่าสัญญา(จ่ายแบบคงที่)                                                  
                                                                                </span>
                                                                          </div>
                                                                    </div>

                                                                     <div class="list-group-item list-group-item-light" >

                                                                        <div class="row" style="padding-top: 1rem;">
                                                                                    <div class="col-3">
                                                                                        <div class="input-group sm-3">
                                                                                            <div class="input-group-prepend">
                                                                                                <span class="input-group-text">* ประเภทค่าใช้จ่าย</span>
                                                                                          </div>
                                                                                            <asp:DropDownList class="form-control" ID="cboPayType"  runat="server" ></asp:DropDownList>    
                                                                                        </div>
                                                                                    </div>
                                                                                      <div class="col-3">
                                                                                        <div class="input-group sm-3">
                                                                                            <div class="input-group-prepend">
                                                                                                <span class="input-group-text">* จำนวนเงิน</span>
                                                                                          </div>
                                                                                            <asp:TextBox class="form-control" ID="txtAmountFix" runat="server" ></asp:TextBox>
                                                                                        </div>
                                                                                    </div>     
                                                                        </div>
                                                                        <div class="row" style="padding-top: 1rem;">

                                                                                    <div class="col-3">
                                                                                        <div class="input-group sm-3">
                                                                                            <div class="input-group-prepend">
                                                                                                <span class="input-group-text">* รอบการจ่าย</span>
                                                                                          </div>
                                                                                            <asp:RadioButton ID="rdoMonthFix" runat="server" groupname="paid" Checked="true"/>รายเดือน&nbsp;&nbsp;
                                                                                            <asp:RadioButton ID="rdoYearFix" runat="server" groupname="paid"/>รายปี
                                                                                        </div>
                                                                                    </div>    
                                                                                    <div class="col-3">
                                                                                        <div class="input-group sm-3">
                                                                                            <div class="input-group-prepend">
                                                                                                <span class="input-group-text">* ความถี่การจ่าย</span>
                                                                                          </div>
                                                                                            <asp:TextBox class="form-control" ID="txtFrequencyFix" runat="server" ></asp:TextBox>
                                                                                        </div>
                                                                                    </div>        
                                                                                    <div class="col-3">
                                                                                        <div class="input-group sm-3" runat="server">
                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                <span class="input-group-text">* Due date</span>
                                                                                          </div>
                                                                                              <asp:TextBox class="form-control" ID="txtDueDateFix" style="background-color:white" runat="server"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>             
                                    
                                                                        </div>
                                                                        <div class="row" style="padding-top: 1rem;">
                                                                                <div class="col-2">
                                                                                    <div class="input-group sm-3">
                                                                                        <div class="input-group-prepend">
                                                                                        <span class="input-group-text">จากวันที่</span>
                                                                                        </div>
                                                                                        <asp:TextBox class="form-control" ID="txtBeginDateFix" style="background-color:white" runat="server"></asp:TextBox>    

                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-2">
                                                                                    <div class="input-group sm-3">
                                                                                        <div class="input-group-prepend">
                                                                                        <span class="input-group-text">ถึงวันที่</span>
                                                                                        </div>
                                                                                        <asp:TextBox class="form-control" ID="txtEndDateFix" style="background-color:white" runat="server" ></asp:TextBox>    

                                                                                    </div>
                                                                                </div>
                                                                        </div>

                                                                        <div class="row" style="padding-top: 1rem;">
                   
                                                                                <div class="col-3">
                                                                                    <div class="input-group sm-3">
                                                                                        <div class="input-group-prepend">
                                                                                        <span class="input-group-text">ผู้จ่ายภาษีที่ดิน</span>
                                                                                        </div>
                                                                                        <asp:DropDownList class="form-control" ID="cboContractLand"  runat="server" ></asp:DropDownList>    
                                                                                    </div>
                                                                                </div>                   

                                                                                <div class="col-3">
                                                                                    <div class="input-group sm-3">
                                                                                        <div class="input-group-prepend">
                                                                                        <span class="input-group-text">ผู้จ่ายภาษีสิ่งปลูกสร้าง</span>
                                                                                        </div>
                                                                                        <asp:DropDownList class="form-control" ID="cboContractBu"  runat="server" ></asp:DropDownList>    
                                                                                    </div>
                                                                                </div>
                 
                                                                                <div class="col-3">
                                                                                    <div class="input-group sm-3">
                                                                                        <div class="input-group-prepend">
                                                                                        <span class="input-group-text">การจ่ายเงินวันจดเช่า</span>
                                                                                        </div>
                                                                                        <asp:DropDownList class="form-control" ID="cboContractDayRent"  runat="server" ></asp:DropDownList>    
                                                                                    </div>
                                                                               </div>                                                       

                                                                        </div>

                                                                          <div class="row col-md-3" style="padding-top: 1rem;">
                                                                            <asp:Button ID="btnAddcontractFix" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" เพิ่มเงือนไขสัญญา " />
                                                                            <asp:Button ID="btnEditcontractFix" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" แก้เงือนไขสัญญา " />
                                                                          </div>
                                               <%--                         <div class="row" style="padding-top: 1rem;">
                                                                                        <div class="table-responsive">
 
                                                                                            <div class="table-responsive" runat="server" >
                                                                                                <asp:Table id="tblContractFix" class="table table-bordered table-condensed table-hover pnlstudentDetails" style='font-family:Tahoma, Courier, monospace; font-size:8pt;' 
                                                                                                                        runat="server" GridLines="Both" cellspacing="0" cellpadding="5" border="1" OnPageIndexChanging="OnPageIndexChanging" PageSize="10">
                                                                                                    <asp:TableHeaderRow TableSection="TableHeader" runat="server">
                                                                                                        <asp:TableHeaderCell ColumnSpan="7" style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">เงือนไขการจ่าย</asp:TableHeaderCell>
                                                          
                                                                                                    </asp:TableHeaderRow>
                                                                                                    <asp:TableHeaderRow runat="server">
                                                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">ประเภทการจ่าย</asp:TableHeaderCell>
                                                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">รอบการจ่าย</asp:TableHeaderCell>                                                             
                                                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">ความถี่การจ่าย/ครั้ง</asp:TableHeaderCell>                                                       
                                                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">กำหนดจ่าย</asp:TableHeaderCell>
                                                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">เริ่มวันที่</asp:TableHeaderCell>
                                                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">สิ้นสุดวันที่</asp:TableHeaderCell>
                                                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">มูลค่า</asp:TableHeaderCell>                                                      
                                                                                                    </asp:TableHeaderRow>
                                                                                                </asp:Table>

                                                                                            </div>
                                                                                       </div>
                                       

                                                                        </div>--%>

                                                                        <!---->
                                                                                        <div class="row" style="padding-top: 1rem;">

                                                                                            <div class="table-responsive">
                                                                                                <asp:GridView ID="gvContractFix"
                                                                                                    class="table table-striped table-bordered"
                                                                                                    AllowSorting="True"
                                                                                                    AllowPaging="false"
                                                                                                    AutoGenerateColumns="false"
                                                                                                    EmptyDataText=" ไม่มีข้อมูล "                                     
                                                                                                    OnRowDataBound="OnRowDataBoundContractFix"
                                                                                                    OnSelectedIndexChanged="OnSelectedIndexChangedContractFix"
                                                                                                    runat="server" CssClass="table table-striped">
                                                                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-Size="Smaller" ForeColor="White" />
                                                                                                    <AlternatingRowStyle BackColor="#CCCCFF" />

                                                                                                    <Columns>                                                             
                                                                                                        <asp:BoundField DataField="PayType" HeaderText="ประเภทค่าใช้จ่าย" />
                                                                                                        <asp:BoundField DataField="DueType" HeaderText="รอบการจ่าย" />
                                                                                                        <asp:BoundField DataField="Frequency" HeaderText="ความถี่การจ่าย" />
                                                                                                        <asp:BoundField DataField="DueDate" HeaderText="กำหนดจ่าย" />
                                                                                                        <asp:BoundField DataField="BeginDate" HeaderText="เริ่มต้น" />
                                                                                                        <asp:BoundField DataField="EndDate" HeaderText="สิ้นสุด" />
                                                                                                        <asp:BoundField DataField="Amount" HeaderText="มูลค่า" />                                                               
                                                                                                        <asp:BoundField DataField="ID" HeaderText="ID" />
                                                                                                        <asp:BoundField DataField="ItemNo" HeaderText="ItemNo" />
                                                                                                        <asp:BoundField DataField="PayTypeID" HeaderText="PayTypeID" />
                                                                                                        <asp:BoundField DataField="DueTypeID" HeaderText="DueTypeID" />

                                                                                                    </Columns>
                                                                                                </asp:GridView>

                                                                                            </div>
                                                                                            <!-- end Table payment-->

                                                                                        </div>
                                                                        <!---->
                               

                                                                    </div>


                                                        </td>
                                                    </tr>
                                                </table>
                                              <%--  <strong><span style="font-size: 14pt">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                    &nbsp; &nbsp;&nbsp;<br />
                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Support page content...</span></strong>--%>

                                            </asp:View>
                                            <asp:View ID="Tab4" runat="server">
                                                <table width="100%"  cellpadding="0" cellspacing="0">
                                                    <tr valign="top">
                                                        <td class="TabArea" style="width: 100%"> 

                                                                <div class="col-md-3" runat="server"> 
                                                                      <div class="row" style="padding-top: 1rem; " runat="server">
                                                                            <span class="span" style="color:blue" runat="server">
                                                                                ช่องทางการชำระเงิน                                                 
                                                                            </span>
                                                                      </div>
                                                                </div>

                                                                <div class="list-group-item list-group-item-light" >
                                             
                                                                     <div class="col-3">
                                                                            <div class="input-group sm-3">
                                                                                  <div class="input-group-prepend">
                                                                                      <span class="input-group-text">* วิธีชำระ</span>
                                                                                  </div>
                                                                                    <asp:RadioButton ID="rdoTrans" runat="server" groupname="paid" Checked="true"/>โอน&nbsp;&nbsp;
                                                                                    <asp:RadioButton ID="rdoCheque" runat="server" groupname="paid"/>เช็ค
                                                                          </div>
                                                                    </div>    

                                                                    <div class="row" style="padding-top: 1rem;">
                                                                        <div class="col-md-4">
                                                                            <div class="input-group-prepend">
                                                                                <span class="input-group-text">* ธนาคาร</span>
                                                                                <asp:DropDownList class="form-control" ID="cboBank"  runat="server" ></asp:DropDownList>  
                                                                            </div>  
                                                                        </div>
                                                                        <div class="col-md-4 ">
                                    
                                                                            <div class="input-group-prepend">
                                                                                <span class="input-group-text">รหัสสาขา</span>
                                                                                 <asp:TextBox class="form-control" ID="txtBankbranchcode" runat="server" ></asp:TextBox>
                                                                            </div>      
                                                                        </div>
                                                                        <div class="col-md-4">
                                   
                                                                            <div class="input-group-prepend">
                                                                                <span class="input-group-text">ชื่อสาขา</span>
                                                                                <asp:TextBox class="form-control" ID="txtBankbranchname" runat="server" ></asp:TextBox>
                                                                            </div>  
                                                                        </div>
                                                                    </div>
                                                                    <div class="row" style="padding-top: 1rem;">
                                                                        <div class="col-md-4 ">
                                    
                                                                            <div class="input-group-prepend">
                                                                                <span class="input-group-text">* เลขบัญชี</span>
                                                                                <asp:TextBox class="form-control" ID="txtAccountNo" runat="server" ></asp:TextBox>
                                                                            </div>  
                                                                       <%--     <div class="input-group sm-">
                                                                                <asp:TextBox class="form-control" ID="txtAccountNo" runat="server" ></asp:TextBox>
                                                                            </div>--%>
                                                                        </div>
                                                                        <div class="col-md-4 ">
                                    
                                                                            <div class="input-group-prepend">
                                                                                <span class="input-group-text">* ชื่อบัญชี</span>
                                                                                <asp:TextBox class="form-control" ID="txtAccountName" runat="server" ></asp:TextBox>
                                                                            </div>  
                                                                       <%--     <div class="input-group sm-">
                                                                                <asp:TextBox class="form-control" ID="txtAccountName" runat="server" ></asp:TextBox>
                                                                            </div>--%>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row" style="padding-top: 1rem;">
                                                                        <div class="col-md-3">                                    
                                                                            <div class="input-group-prepend">
                                                                                <span class="input-group-text">* สั่งจ่าย</span>
                                                                                <asp:DropDownList class="form-control" ID="cboPayCust" runat="server" ></asp:DropDownList>
                                                                            </div>                          
                                                                        </div>
                                                                        <div class="col-md-3">                                    
                                                                            <div class="input-group-prepend">
                                                                                <span class="input-group-text">* หักภาษี ณ ที่จ่าย</span>
                                                                                <asp:DropDownList class="form-control" ID="cboTaxCust" runat="server" ></asp:DropDownList>
                                                                            </div>                          
                                                                        </div>
                                                                        <div class="col-md-3">                                    
                                                                            <div class="input-group-prepend">
                                                                                <span class="input-group-text">* A/C Code</span>
                                                                                 <asp:TextBox class="form-control" ID="txtACCode" runat="server" ></asp:TextBox>
                                                                            </div>                          
                                                                        </div>
                                                                    </div>

                                                                      <div class="row col-md-3" style="padding-top: 1rem;">
                                                                        <asp:Button ID="btnAddPayment" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" บันทึกการจ่ายเงิน " />
                                                                        <asp:Button ID="btnAddPaymentEdit" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" แก้ไขการจ่ายเงิน " />
                                                                      </div>
                                    <%--                                <div class="row" style="padding-top: 1rem;">
                                                                                    <div class="table-responsive">
 
                                                                                        <div class="table-responsive" runat="server" >
                                                                                            <asp:Table id="tblPayment" class="table table-bordered table-condensed table-hover pnlstudentDetails" style='font-family:Tahoma, Courier, monospace; font-size:8pt;' 
                                                                                                                    runat="server" GridLines="Both" cellspacing="0" cellpadding="5" border="1" OnPageIndexChanging="OnPageIndexChanging" PageSize="10">
                                                                                                <asp:TableHeaderRow TableSection="TableHeader" runat="server">
                                                                                                    <asp:TableHeaderCell ColumnSpan="8" style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">การชำระเงิน</asp:TableHeaderCell>
                                                          
                                                                                                </asp:TableHeaderRow>
                                                                                                <asp:TableHeaderRow runat="server">
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">วิธีชำระ</asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">ธนาคาร</asp:TableHeaderCell>                                                             
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">สาขา</asp:TableHeaderCell>                                                       
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">เลขที่บัญชี</asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">ชื่อบัญชี</asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">สั่งจ่าย</asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">หักภาษี ณ ที่จ่าย</asp:TableHeaderCell>  
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">A/C Code</asp:TableHeaderCell>  
                                                                                                </asp:TableHeaderRow>
                                                                                            </asp:Table>

                                                                                        </div>
                                                                                   </div>
                                       

                                                                    </div>--%>

                                                                    <!---->
                                                                                    <div class="row" style="padding-top: 1rem;">

                                                                                        <div class="table-responsive">
                                                                                            <asp:GridView ID="gvPayment"
                                                                                                class="table table-striped table-bordered"
                                                                                                AllowSorting="True"
                                                                                                AllowPaging="false"
                                                                                                AutoGenerateColumns="false"
                                                                                                EmptyDataText=" ไม่มีข้อมูล "                                     
                                                                                                OnRowDataBound="OnRowDataBoundPayment"
                                                                                                OnSelectedIndexChanged="OnSelectedIndexChangedPayment"
                                                                                                runat="server" CssClass="table table-striped">
                                                                                                <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-Size="Smaller" ForeColor="White" />
                                                                                                <AlternatingRowStyle BackColor="#CCCCFF" />

                                                                                                <Columns>                                                             
                                                                                                    <asp:BoundField DataField="PaymentPay" HeaderText="ช่องทางจ่าย" />
                                                                                                    <asp:BoundField DataField="BankName" HeaderText="ธนาคาร" />
                                                                                                    <asp:BoundField DataField="BranchName" HeaderText="สาขา" />
                                                                                                    <asp:BoundField DataField="AccCode" HeaderText="เลขบัญชี" />
                                                                                                    <asp:BoundField DataField="AccName" HeaderText="ชื่อบัญชี" />
                                                                                                    <asp:BoundField DataField="PayCust" HeaderText="สั่งจ่าย" />
                                                                                                    <asp:BoundField DataField="TaxCust" HeaderText="หักภาษี ณ ที่จ่าย" />                                                               
                                                                                                    <asp:BoundField DataField="ACCode" HeaderText="A/C Code" />
                                                                                                    <asp:BoundField DataField="ID" HeaderText="ID" />
                                                                                                    <asp:BoundField DataField="ItemNo" HeaderText="ID" />
                                                                                                    <asp:BoundField DataField="PaymentPayID" HeaderText="PaymentPayID" />
                                                                                                    <asp:BoundField DataField="BankID" HeaderText="BankID" />    
                                                                                                </Columns>
                                                                                            </asp:GridView>

                                                                                        </div>
                                                                                        <!-- end Table payment-->

                                                                                    </div>
                                                                    <!---->
                             
                                                              </div>


                                                        </td>
                                                    </tr>
                                                </table>
                                          <%--      <strong><span style="font-size: 14pt">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                    &nbsp; &nbsp;&nbsp;<br />
                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Help page content...</span> </strong>--%>

                                            </asp:View>

                                            <asp:View ID="Tab5" runat="server">
                                                <table width="100%"  cellpadding="0" cellspacing="0">
                                                    <tr valign="top">
                                                        <td class="TabArea" style="width: 100%"> 

                                                                  <div class="row" style="padding-top: 1rem; " runat="server">
                                                                        <span class="span" style="color:blue" runat="server">
                                                                          ทรัพย์สิน
                                                                        </span>
                                                                  </div>
                                                                 
                                                                    <div class="list-group-item list-group-item-light">                                        

                                                                        <%-- input area --%>
                                                                        <div class="row" style="padding-top: 0.2rem;">
                                                                            <div class="col-md-3" runat="server" >                           
                                                                                <div class="input-group-prepend" runat="server">                                                                                     
                                                                                    <span class="input-group-text">รหัสทรัพย์สิน</span>
                                                                                    <asp:Textbox class="form-control" ID="txtAssetsNo" placeholder="please save first" runat="server" ReadOnly="true"></asp:Textbox>   
                                                                                
                                                                                </div>
                                                                            </div>
                                                                                                                                      

                                                                            <div class="col-md-3" runat="server">                            
                                                                                <div class="input-group-prepend" runat="server">
                                                                                    <span class="input-group-text">ประเภททรัพย์สิน</span>
                                                                                    <asp:DropDownList class="form-control" ID="cboAssetType" runat="server"></asp:DropDownList>  
                                                                                    
                                                                                </div>
                                                                            </div>
                                                                                    <div class="row" runat="server">
                                                                                         <button type="button" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" id="btnAddAssetType" runat="server" 
                                                                                                                    data-toggle="modal" data-target="#ModalAssetType" data-backdrop="static" data-keyboard="false" data-whatever="new">เพิ่มประเภท</button>
                                                                                    </div> 
                                                                        </div>

                                                                        <div class="row col-sm-3 span2" style="padding-top: 1rem;" runat="server">
                                                                                               <%--     <span class="span2" style="color:blue" runat="server">--%>
                                                                                 เนื้อที่ทั้งหมด
                                                                                                    <%--</span>--%>
                                                                        </div>

                                                                        <div class="row" style="padding-top: 0.2rem;" runat="server">
                                                                            <div class="col-md-2" runat="server">                            
                                                                                <div class="input-group-prepend" runat="server">                                                                                   
                                                                                    <span class="input-group-text">เลขที่โฉนด/เอกสารสิทธิ์</span>
                                                                                    <asp:Textbox class="form-control" ID="txtLandno" runat="server"></asp:Textbox>    
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-2" runat="server">                            
                                                                                <div class="input-group-prepend" runat="server"> 
                                                                                    <span class="input-group-text">เลขที่หน้าสำรวจ</span>
                                                                                    <asp:Textbox class="form-control" ID="txtSurveyNo" runat="server"></asp:Textbox>  
                                                                                </div>
                                                                            </div>

                                                                        </div>
                                                                        <div class="row" style="padding-top: 0.2rem;" runat="server">

                                                                            <div class="col-md-2" runat="server">                            
                                                                                <div class="input-group-prepend" runat="server"> 
                                                                                    <span class="input-group-text">ที่อยู่</span>
                                                                                    <asp:Textbox class="form-control" ID="txtAssetAddr" runat="server"></asp:Textbox>    
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-2" runat="server">                            
                                                                                <div class="input-group-prepend" runat="server"> 
                                                                                    <span class="input-group-text">ตำบล</span>
                                                                                    <asp:Textbox class="form-control" ID="txtAssetSubdistrict" runat="server"></asp:Textbox>    
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-2" runat="server">                            
                                                                                <div class="input-group-prepend" runat="server"> 
                                                                                    <span class="input-group-text">อำเภอ</span>
                                                                                    <asp:Textbox class="form-control" ID="txtAssetDistrict" runat="server"></asp:Textbox>  
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-2" runat="server">                            
                                                                                <div class="input-group-prepend" runat="server"> 
                                                                                    <span class="input-group-text">จังหวัด</span>
                                                                                    <asp:Textbox class="form-control" ID="txtAssetProvince" runat="server"></asp:Textbox>  
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-2" runat="server">                            
                                                                                <div class="input-group-prepend" runat="server">
                                                                                    <span class="input-group-text">ไปรษณีย์</span>
                                                                                    <asp:Textbox class="form-control" ID="txtAssPostCode" runat="server"></asp:Textbox>  
                                                                                </div>
                                                                            </div> 

                                                                        </div>
                                                                        <div class="row" style="padding-top: 0.2rem;" runat="server">

                                                                            <div class="col-md-2" runat="server">                            
                                                                                <div class="input-group-prepend" runat="server">                                                                                     
                                                                                    <span class="input-group-text">ไร่</span>
                                                                                    <asp:Textbox class="form-control" ID="txtRai" runat="server"></asp:Textbox>  
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-2" runat="server">                            
                                                                                <div class="input-group-prepend" runat="server">   
                                                                                    <span class="input-group-text">งาน</span>
                                                                                    <asp:Textbox class="form-control" ID="txtNgan" runat="server"></asp:Textbox>  
                                                                                </div>
                                                                            </div>                                                                                                                                                  
                                                                            <div class="col-md-2" runat="server">                            
                                                                                <div class="input-group-prepend" runat="server">   
                                                                                    <span class="input-group-text">ตารางวา</span>
                                                                                    <asp:Textbox class="form-control" ID="txtWa" runat="server"></asp:Textbox>    
                                                                                </div>
                                                                            </div>                                                                           
                                                                        </div>

                                                                        <div class="row col-sm-3 span2" style="padding-top: 1rem;" runat="server">
                                                                                               <%--     <span class="span2" style="color:blue" runat="server">--%>
                                                                                 แบ่งเช่าบางส่วน
                                                                                                    <%--</span>--%>
                                                                        </div>

                                                                        <div class="row" style="padding-top: 0.2rem;" runat="server">
                                                                            <div class="col-md-2" runat="server">                            
                                                                                <div class="input-group-prepend" runat="server">                                                                                   
                                                                                    <span class="input-group-text">เลขที่โฉนด/เอกสารสิทธิ์</span>
                                                                                    <asp:Textbox class="form-control" ID="txtAssetDocno" runat="server"></asp:Textbox>    
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-2" runat="server">                            
                                                                                <div class="input-group-prepend" runat="server"> 
                                                                                    <span class="input-group-text">เลขที่หน้าสำรวจ</span>
                                                                                    <asp:Textbox class="form-control" ID="txtAssetSurvey" runat="server"></asp:Textbox>  
                                                                                </div>
                                                                            </div>

                                                                        </div>
<%--                                                                        <div class="row" style="padding-top: 0.2rem;" runat="server">
                                                                            <div class="col-md-2" runat="server">                            
                                                                                <div class="input-group-prepend" runat="server"> 
                                                                                    <span class="input-group-text">ตำบล</span>
                                                                                    <asp:Textbox class="form-control" ID="txtAssetSubdistrict1" runat="server"></asp:Textbox>    
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-2" runat="server">                            
                                                                                <div class="input-group-prepend" runat="server"> 
                                                                                    <span class="input-group-text">อำเภอ</span>
                                                                                    <asp:Textbox class="form-control" ID="txtAssetDistrict1" runat="server"></asp:Textbox>  
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-2" runat="server">                            
                                                                                <div class="input-group-prepend" runat="server"> 
                                                                                    <span class="input-group-text">จังหวัด</span>
                                                                                    <asp:Textbox class="form-control" ID="txtAssetProvince1" runat="server"></asp:Textbox>  
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-2" runat="server">                            
                                                                                <div class="input-group-prepend" runat="server">
                                                                                    <span class="input-group-text">ไปรษณีย์</span>
                                                                                    <asp:Textbox class="form-control" ID="txtAss" runat="server"></asp:Textbox>  
                                                                                </div>
                                                                            </div> 

                                                                        </div>--%>
                                                                        <div class="row" style="padding-top: 0.2rem;" runat="server">

                                                                            <div class="col-md-2" runat="server">                            
                                                                                <div class="input-group-prepend" runat="server">                                                                                     
                                                                                    <span class="input-group-text">ไร่</span>
                                                                                    <asp:Textbox class="form-control" ID="txtRai1" runat="server"></asp:Textbox>  
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-2" runat="server">                            
                                                                                <div class="input-group-prepend" runat="server">   
                                                                                    <span class="input-group-text">งาน</span>
                                                                                    <asp:Textbox class="form-control" ID="txtNgan1" runat="server"></asp:Textbox>  
                                                                                </div>
                                                                            </div>                                                                                                                                                  
                                                                            <div class="col-md-2" runat="server">                            
                                                                                <div class="input-group-prepend" runat="server">   
                                                                                    <span class="input-group-text">ตารางวา</span>
                                                                                    <asp:Textbox class="form-control" ID="txtWa1" runat="server"></asp:Textbox>    
                                                                                </div>
                                                                            </div>                                                                           
                                                                        </div>

                                                                        <br />
                                                                                        <div class="row col-sm-3 span2" style="padding-top: 1rem;" runat="server">                                                                                                               
                                                                                                 เช่าสิ่งปลูกสร้าง                                                                                                                 
                                                                                        </div>

                                                                                    <div class="row" style="padding-top: 0.2rem;" runat="server">
                                                                            
                                                                                        <div class="row" style="padding-top: 0.5rem;">
                                                                                            <div class="col-md-4" runat="server">                            
                                                                                                <div class="input-group-prepend" runat="server">  
                                                                                                    <span class="input-group-text">สิ่งปลูกสร้างเลขที่</span>
                                                                                                    <asp:Textbox class="form-control" ID="txtbuNo" runat="server"></asp:Textbox>    
                                                                                                </div>
                                                                                            </div>

                                                                                            <div class="col-md-2" runat="server">                            
                                                                                                <div class="input-group-prepend" runat="server">                                                                                     
                                                                                                    <span class="input-group-text">ไร่</span>
                                                                                                    <asp:Textbox class="form-control" ID="txtRai2" runat="server"></asp:Textbox>  
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2" runat="server">                            
                                                                                                <div class="input-group-prepend" runat="server">   
                                                                                                    <span class="input-group-text">งาน</span>
                                                                                                    <asp:Textbox class="form-control" ID="txtNgan2" runat="server"></asp:Textbox>  
                                                                                                </div>
                                                                                            </div>                                                                                                                                                  
                                                                                            <div class="col-md-2" runat="server">                            
                                                                                                <div class="input-group-prepend" runat="server">   
                                                                                                    <span class="input-group-text">ตารางวา</span>
                                                                                                    <asp:Textbox class="form-control" ID="txtWa2" runat="server"></asp:Textbox>    
                                                                                                </div>
                                                                                            </div> 

                                                                                  <%--          <div class="col-md-2" runat="server">                            
                                                                                                <div class="input-group-prepend" runat="server">
                                                                                                    <span class="input-group-text">ตำบล</span>
                                                                                                    <asp:Textbox class="form-control" ID="txtbuSubDistrict" runat="server"></asp:Textbox>    
                                                                                                </div>
                                                                                            </div>
                                                                            
                                                                                            <div class="col-md-2" runat="server">                            
                                                                                                <div class="input-group-prepend" runat="server">
                                                                                                    <span class="input-group-text">อำเภอ</span>
                                                                                                    <asp:Textbox class="form-control" ID="txtbuDistrict" runat="server"></asp:Textbox>  
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2" runat="server">                            
                                                                                                <div class="input-group-prepend" runat="server">
                                                                                                    <span class="input-group-text">จังหวัด</span>
                                                                                                    <asp:Textbox class="form-control" ID="txtbuProvince" runat="server"></asp:Textbox>  
                                                                                                </div>
                                                                                            </div>    
                                                                                            <div class="col-md-2" runat="server">                            
                                                                                                <div class="input-group-prepend" runat="server">
                                                                                                    <span class="input-group-text">ไปรษณีย์</span>
                                                                                                    <asp:Textbox class="form-control" ID="txtbuPostCode" runat="server"></asp:Textbox>  
                                                                                                </div>
                                                                                            </div> --%>
                                                                                            <div class="col-md-4" runat="server">                            
                                                                                                <div class="input-group-prepend" runat="server">  
                                                                                                    <span class="input-group-text">หมายเหตุ</span>
                                                                                                    <asp:Textbox class="form-control" ID="txtAssetRemark" runat="server"></asp:Textbox>    
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-4" runat="server">                            
                                                                                                <div class="input-group-prepend" runat="server">  
                                                                                                    <span class="input-group-text">หมายเหตุ2</span>
                                                                                                    <asp:Textbox class="form-control" ID="txtAssetRemark2" runat="server"></asp:Textbox>    
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>     
                                                                                    </div>

                                                                                    <div class="row" style="padding-top: 0.2rem;" runat="server">
                                                                                        <div class="col-md-4" runat="server">
                                                                                            <INPUT id="File4" type="file" runat="server" NAME="oFile">
                                                                                            <asp:button id="Button4" type="submit" text="Upload" runat="server"></asp:button>
                                                                                            <asp:Panel ID="Panel4" Visible="False" Runat="server">
                                                                                                <asp:Label id="Label5" Runat="server"></asp:Label>
                                                                                            </asp:Panel>
                                                                                        </div>
                                                                                        <div class="col-md-4" runat="server">
                                                                                            <INPUT id="File5" type="file" runat="server" NAME="oFile">
                                                                                            <asp:button id="Button5" type="submit" text="Upload" runat="server"></asp:button>
                                                                                            <asp:Panel ID="Panel5" Visible="False" Runat="server">
                                                                                                <asp:Label id="Label6" Runat="server"></asp:Label>
                                                                                            </asp:Panel>
                                                                                        </div>
                                                                                    </div>


                                                                    </div>

                                                                      <div class="row col-md-3" style="padding-top: 1rem;">
                                                                        <asp:Button ID="btnAddasset" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" บันทึกทรัพย์สิน " />
                                                                        <asp:Button ID="btnEditasset" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" แก้ไขทรัพย์สิน " />
                                                                      </div>

                                                                                    <div class="row" style="padding-top: 1rem;">

                                                                                        <div class="table-responsive">
                                                                                            <asp:GridView ID="gvAsset"
                                                                                                class="table table-striped table-bordered"
                                                                                                AllowSorting="True"
                                                                                                AllowPaging="false"
                                                                                                AutoGenerateColumns="false"
                                                                                                EmptyDataText=" ไม่มีข้อมูล "                                     
                                                                                                OnRowDataBound="OnRowDataBoundPayment"
                                                                                                OnSelectedIndexChanged="OnSelectedIndexChangedPayment"
                                                                                                runat="server" CssClass="table table-striped">
                                                                                                <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-Size="Smaller" ForeColor="White" />
                                                                                                <AlternatingRowStyle BackColor="#CCCCFF" />

                                                                                                <Columns>                                                             
                                                                                                    <asp:BoundField DataField="DocNo" HeaderText="DocNo" />
                                                                                                    <asp:BoundField DataField="AsstypeID" HeaderText="AsstypeID" />
                                                                                                    <asp:BoundField DataField="ServayNo" HeaderText="ServayNo" />
                                                                                                    <asp:BoundField DataField="Addr" HeaderText="Addr" />
                                                                                                    <asp:BoundField DataField="SubDistrict" HeaderText="SubDistrict" />
                                                                                                    <asp:BoundField DataField="District" HeaderText="District" />
                                                                                                    <asp:BoundField DataField="Province" HeaderText="Province" />                                                               
                                                                                                    <asp:BoundField DataField="PostCode" HeaderText="PostCode" />
                                                                                                    <asp:BoundField DataField="RoadTo" HeaderText="RoadTo" />
                                                                                                    <asp:BoundField DataField="Area" HeaderText="Area" />
                                                                                                    <asp:BoundField DataField="Farm" HeaderText="Farm" />
                                                                                                    <asp:BoundField DataField="Squarewah" HeaderText="Squarewah" />
                                                                                                    <asp:BoundField DataField="Lanno" HeaderText="Lanno" />
                                                                                                    <asp:BoundField DataField="Area1" HeaderText="Area1" />
                                                                                                    <asp:BoundField DataField="Farm1" HeaderText="Farm1" />
                                                                                                    <asp:BoundField DataField="Squarewah1" HeaderText="Squarewah1" />
                                                                                                    <asp:BoundField DataField="Buno" HeaderText="Buno" />
                                                                                                    <asp:BoundField DataField="Area2" HeaderText="Area2" />
                                                                                                    <asp:BoundField DataField="Farm2" HeaderText="Farm2" />
                                                                                                    <asp:BoundField DataField="Squarewah2" HeaderText="Squarewah2" />
                                                                                                    <asp:BoundField DataField="Remark" HeaderText="Remark" />
                                                                                                    <asp:BoundField DataField="Remark1" HeaderText="Remark1" />
                                                                                                    <asp:BoundField DataField="pic1" HeaderText="pic1" />
                                                                                                    <asp:BoundField DataField="pic2" HeaderText="pic2" />
                                                                                                    <asp:BoundField DataField="ID" HeaderText="ID" />
                                                                                                    <asp:BoundField DataField="ItemNo" HeaderText="ItemNo" />
                                                                                                </Columns>
                                                                                            </asp:GridView>

                                                                                        </div>
                                                                                        <!-- end Table payment-->

                                                                                    </div>
                                                          

                                                        </td>
                                                    </tr>
                                                </table>
                                           <%--     <strong><span style="font-size: 14pt">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                    &nbsp; &nbsp;&nbsp;<br />
                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Products page content...</span></strong>--%>

                                            </asp:View>
                                            <asp:View ID="Tab6" runat="server" >
                                                                    <div class="row" style="padding-top: 1rem;">
                                                                        <div class="input-group sm-col-2" >   
                                                                            <span class="input-group-text">โฉนดที่ดินเลขที่</span>
                                                                            <span class="input-group-text">เลขที่สัญญา   PTEC-SDD- 2566- 001</span>
                                                                           
                                                                        </div>
                                                                      
                                                                            <div class="row col-md-8" style="padding-top: 1rem;">                                                                                
                                                                              <span  class="span" >  บันทึกต่อท้ายสัญญา	</span> <br />
                                                                                	          บันทึกต่อท้ายสัญญานี้ถือเป็นส่วนหนึ่งของสัญญาเลขที่ เลขที่สัญญา เลขที่สัญญา .......... ระหว่างบริษัท เพียวพลังงานไทย จำกัด (บริษัท ) <br />
                                                                                                และ.......... ( ผู้ร่วมธุรกิจ) เพื่อบันทึกรายการทรัพย์สินของผู้ร่วมธุรกิจ ดังนี้ 	<br />
                                                                                                ที่ดินตามสัญญาร่วมธุรกิจ ตั้งอยู่ที่  .........................	
                                                                            </div>
                                                                            
                                                                            <%--<div class="row col-md-8" style="padding-top: 1rem;">--%>

                                                                                              
                                                                            <%--</div>--%>

                                                                        
                                                                        <%--<span class="input-group-text">บันทึกต่อท้ายสัญญา</span>--%>
                                                                                    <div class="table-responsive">
 
                                                                                        <div class="table-responsive" runat="server" >
                                                                                            <asp:Table id="tblPayment" class="table table-bordered table-condensed table-hover pnlstudentDetails" style='font-family:Tahoma, Courier, monospace; font-size:8pt;' 
                                                                                                                    runat="server" GridLines="Both" cellspacing="0" cellpadding="5" border="1" OnPageIndexChanging="OnPageIndexChanging" PageSize="10">
                                                                                                <asp:TableHeaderRow TableSection="TableHeader" runat="server">
                                                                                                <asp:TableHeaderCell ColumnSpan="4" style='margin:0 auto; text-align:center; background-color:whitesmoke;' runat="server"></asp:TableHeaderCell>
                                                          
                                                                                                </asp:TableHeaderRow>
                                                                                                <asp:TableHeaderRow runat="server">
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">ลำดับ</asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">รายการทรัพย์สินของผู้ร่วมธุรกิจ</asp:TableHeaderCell>                                                             
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">จำนวน</asp:TableHeaderCell>                                                       
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">รายละเอียด/ภาพถ่าย</asp:TableHeaderCell>
                                                                                                   
                                                                                                </asp:TableHeaderRow>

                                                                                                <asp:TableRow Style='margin: 0 auto; text-align: right;' runat="server">
                                                                                                    <asp:TableCell style='width=10%;' runat="server">1</asp:TableCell>
                                                                                                    <asp:TableCell runat="server" ColumnSpan="1">
                                                                                                            <div class="card-body ">
                                                                                                                <div class="input-group sm-col-2" >                                                                                                               
                                                                                                                    <span class="input-group-text">โฉนดที่ดินเลขที่</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:auto' ID="Textbox1" runat="server"></asp:Textbox>                                                                                                                     
                                                                                                                </div>

                                                                                                                <div class="input-group sm-col-2" >                                                                                                               
                                                                                                                    <span class="input-group-text">เลขที่ดิน</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:auto' ID="Textbox20" runat="server"></asp:Textbox> 
                                                                                                                    <span class="input-group-text">เช่าเต็มแปลง</span>                                                                                                                  
                                                                                                                </div>

                                                                                                                <div class="input-group sm-col-2" >  
                                                                                                                    <span class="input-group-text">ด้านหน้าติดถนนประมาณ</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:right; width:auto' ID="Textbox7" runat="server"></asp:Textbox>
                                                                                                                    <span class="input-group-text">เมตร</span>
                                                                                                                </div>

                                                                                                                <div class="input-group sm-col-2" >                                                                                                               
                                                                                                                    <span class="input-group-text">เนื้อที่</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="Textbox5" runat="server"></asp:Textbox>  
                                                                                                                    <span class="input-group-text">ไร่</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="Textbox11" runat="server"></asp:Textbox> 
                                                                                                                    <span class="input-group-text">งาน</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:right; width:10%' ID="Textbox12" runat="server"></asp:Textbox>
                                                                                                                    <span class="input-group-text">ตารางวา</span>
                                                                                                                </div>
                                                         
                                                                                                                <div class="input-group sm-col-2" >    
                                                                                                                    <span class="input-group-text">บรรยาย 1</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:left;width:auto' ID="txtRemark1" runat="server"></asp:Textbox>                                                                                                                 
                                                                                                                </div>
                                                                                                                <div class="input-group sm-col-2" >                                                                                                                 
                                                                                                                    <span class="input-group-text">บรรยาย 2</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:left;width:auto' ID="Textbox10" runat="server"></asp:Textbox>                                                                                                                 
                                                                                                                </div>    
                                                                                                             </div>                                                                                                                             
                                                                                                   

                                                                                                        </asp:TableCell>
                                                                                                    <asp:TableCell runat="server">

                                                                                                            <div class="input-group sm-col-2" >                                                                                                               
                                                                                                                <span class="input-group-text">เนื้อที่</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="Textbox13" runat="server"></asp:Textbox>  
                                                                                                                <span class="input-group-text">ไร่</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="Textbox14" runat="server"></asp:Textbox> 
                                                                                                                <span class="input-group-text">งาน</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right; width:10%' ID="Textbox15" runat="server"></asp:Textbox>
                                                                                                                <span class="input-group-text">ตารางวา</span>
                                                                                                            </div>

                                                                                                            <div class="input-group sm-col-2" >                                                                                                               
                                                                                                                <span class="input-group-text">แบ่งเช่าบางส่วน</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="Textbox6" runat="server"></asp:Textbox>  
                                                                                                                <span class="input-group-text">ไร่</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="Textbox19" runat="server"></asp:Textbox> 
                                                                                                                <span class="input-group-text">งาน</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right; width:10%' ID="Textbox21" runat="server"></asp:Textbox>
                                                                                                                <span class="input-group-text">ตารางวา</span>
                                                                                                            </div>

                                                                                                    </asp:TableCell>

                                                                                                    <asp:TableCell runat="server">

                                                                                                            <div class="input-group sm-col-2" >                                                                                                               
                                                                                                                <%--<span class="input-group-text">รูป 1</span>--%>
                                                                                                                 <INPUT id="oFile" type="file" runat="server" NAME="oFile">
                                                                                                                 <asp:button id="btnUpload" type="submit" text="Upload" runat="server"></asp:button>
                                                                                                                 <asp:Panel ID="frmConfirmation" Visible="False" Runat="server">
                                                                                                                    <asp:Label id="lblUploadResult" Runat="server"></asp:Label>
                                                                                                                 </asp:Panel>

                                                                                                                 <INPUT id="File1" type="file" runat="server" NAME="oFile">
                                                                                                                 <asp:button id="Button1" type="submit" text="Upload" runat="server"></asp:button>
                                                                                                                 <asp:Panel ID="Panel1" Visible="False" Runat="server">
                                                                                                                    <asp:Label id="Label1" Runat="server"></asp:Label>
                                                                                                                 </asp:Panel>

                                                                                                            </div>
                                                                                                    </asp:TableCell>
                                                                                                </asp:TableRow>

                                                                                                <asp:TableRow Style='margin: 0 auto; text-align: right;' runat="server">
                                                                                                    <asp:TableCell style='width=10%;' runat="server">2</asp:TableCell>
                                                                                                    <asp:TableCell runat="server" ColumnSpan="1">
                                                                                                            <div class="card-body ">
                                                                                                                <div class="input-group sm-col-2" >                                                                                                               
                                                                                                                    <span class="input-group-text">โฉนดที่ดินเลขที่</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:auto' ID="Textbox16" runat="server"></asp:Textbox>                                                                                                                     
                                                                                                                </div>

                                                                                                                <div class="input-group sm-col-2" >                                                                                                               
                                                                                                                    <span class="input-group-text">เลขที่ดิน</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:auto' ID="Textbox17" runat="server"></asp:Textbox> 
                                                                                                                    <span class="input-group-text">เช่าเต็มแปลง</span>                                                                                                                  
                                                                                                                </div>

                                                                                                                <div class="input-group sm-col-2" >  
                                                                                                                    <span class="input-group-text">ด้านหน้าติดถนนประมาณ</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:right; width:auto' ID="Textbox18" runat="server"></asp:Textbox>
                                                                                                                    <span class="input-group-text">เมตร</span>
                                                                                                                </div>

                                                                                                                <div class="input-group sm-col-2" >                                                                                                               
                                                                                                                    <span class="input-group-text">เนื้อที่</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="Textbox22" runat="server"></asp:Textbox>  
                                                                                                                    <span class="input-group-text">ไร่</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="Textbox23" runat="server"></asp:Textbox> 
                                                                                                                    <span class="input-group-text">งาน</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:right; width:10%' ID="Textbox24" runat="server"></asp:Textbox>
                                                                                                                    <span class="input-group-text">ตารางวา</span>
                                                                                                                </div>
                                                         
                                                                                                                <div class="input-group sm-col-2" >    
                                                                                                                    <span class="input-group-text">บรรยาย 1</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:left;width:auto' ID="Textbox25" runat="server"></asp:Textbox>                                                                                                                 
                                                                                                                </div>
                                                                                                                <div class="input-group sm-col-2" >                                                                                                                 
                                                                                                                    <span class="input-group-text">บรรยาย 2</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:left;width:auto' ID="Textbox26" runat="server"></asp:Textbox>                                                                                                                 
                                                                                                                </div>    
                                                                                                             </div>                                                                                                                             
                                                                                                   

                                                                                                        </asp:TableCell>
                                                                                                    <asp:TableCell runat="server">

                                                                                                            <div class="input-group sm-col-2" >                                                                                                               
                                                                                                                <span class="input-group-text">เนื้อที่</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="Textbox27" runat="server"></asp:Textbox>  
                                                                                                                <span class="input-group-text">ไร่</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="Textbox28" runat="server"></asp:Textbox> 
                                                                                                                <span class="input-group-text">งาน</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right; width:10%' ID="Textbox29" runat="server"></asp:Textbox>
                                                                                                                <span class="input-group-text">ตารางวา</span>
                                                                                                            </div>

                                                                                                            <div class="input-group sm-col-2" >                                                                                                               
                                                                                                                <span class="input-group-text">แบ่งเช่าบางส่วน</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="Textbox30" runat="server"></asp:Textbox>  
                                                                                                                <span class="input-group-text">ไร่</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="Textbox31" runat="server"></asp:Textbox> 
                                                                                                                <span class="input-group-text">งาน</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right; width:10%' ID="Textbox32" runat="server"></asp:Textbox>
                                                                                                                <span class="input-group-text">ตารางวา</span>
                                                                                                            </div>

                                                                                                    </asp:TableCell>

                                                                                                    <asp:TableCell runat="server">

                                                                                                            <div class="input-group sm-col-2" >                                                                                                               
                                                                                                                <%--<span class="input-group-text">รูป 1</span>--%>
                                                                                                                 <INPUT id="File2" type="file" runat="server" NAME="oFile">
                                                                                                                 <asp:button id="Button2" type="submit" text="Upload" runat="server"></asp:button>
                                                                                                                 <asp:Panel ID="Panel2" Visible="False" Runat="server">
                                                                                                                    <asp:Label id="Label2" Runat="server"></asp:Label>
                                                                                                                 </asp:Panel>

                                                                                                                 <INPUT id="File3" type="file" runat="server" NAME="oFile">
                                                                                                                 <asp:button id="Button3" type="submit" text="Upload" runat="server"></asp:button>
                                                                                                                 <asp:Panel ID="Panel3" Visible="False" Runat="server">
                                                                                                                    <asp:Label id="Label3" Runat="server"></asp:Label>
                                                                                                                 </asp:Panel>

                                                                                                            </div>

                                                                                                    </asp:TableCell>
                                                                                                </asp:TableRow>

                                                                                            </asp:Table>

                                                                                        </div>
                                                                                   </div>
                                       

                                                                    </div>
                                            </asp:View>
                                            <asp:View ID="Tab7" runat="server" >
                                                                        <div class="input-group sm-col-2" >   
                                                                            <span class="input-group-text">เอกสารแนบท้าย 3  </span>
                                                                            <span class="input-group-text">เลขที่สัญญา   PTEC-SDD- 2566- 001</span>
                                                                        </div>
                                                                      
                                                                            <div class="row" style="padding-top: 1rem;">
                                                                                ธุรกิจร่วมการประกอบการค้า		<br />
                                                                                (ที่ผู้ร่วมธุรกิจประสงค์จะดำเนินการ)			

                                                                            </div>
                                                                            
                                                                      
                                                                    <div class="row" style="padding-top: 1rem;">


                                                                        
                                                                        <%--<span class="input-group-text">บันทึกต่อท้ายสัญญา</span>--%>
                                                                                    <div class="table-responsive">
 
                                                                                        <div class="table-responsive" runat="server" >
                                                                                            <asp:Table id="Table1" class="table table-bordered table-condensed table-hover pnlstudentDetails" style='font-family:Tahoma, Courier, monospace; font-size:8pt;' 
                                                                                                                    runat="server" GridLines="Both" cellspacing="0" cellpadding="5" border="1" OnPageIndexChanging="OnPageIndexChanging" PageSize="10">
                                                                                                <asp:TableHeaderRow TableSection="TableHeader" runat="server">
                                                                                                <asp:TableHeaderCell RowSpan="2" style='margin:0 auto; text-align:center; background-color:white;' runat="server">ลำดับ</asp:TableHeaderCell>
                                                                                                <asp:TableHeaderCell RowSpan="2" style='margin:0 auto; text-align:center; background-color:white;' runat="server">ประเภทธุรกิจ</asp:TableHeaderCell>
                                                                                                <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:white;' runat="server">ขนาดพื้นที่</asp:TableHeaderCell>
                                                          
                                                                                                </asp:TableHeaderRow>
                                                                                                <asp:TableHeaderRow runat="server">
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:white;' runat="server"></asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:white;' runat="server"></asp:TableHeaderCell>                                                             
                                                                                                    <%--<asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">จำนวน</asp:TableHeaderCell>--%>                                                       
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:white;' runat="server"></asp:TableHeaderCell>
                                                                                                   
                                                                                                </asp:TableHeaderRow>    
                                                                                                <asp:TableHeaderRow runat="server">
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:white;' runat="server"></asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:white;' runat="server"></asp:TableHeaderCell>                                                             
                                                                                                    <%--<asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">จำนวน</asp:TableHeaderCell>--%>                                                       
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:white;' runat="server"></asp:TableHeaderCell>
                                                                                                   
                                                                                                </asp:TableHeaderRow>                                                                                                   

                                                                                            </asp:Table>

                                                                                        </div>

                                                                                        <div class="row">
                                                                                         
                                                                                            <span  class="span" >  หมายเหตุ	</span><br />
	                                                                                            หากผู้ร่วมธุรกิจ ไม่สามารถเปิดดำเนินการธุรกิจดังกล่าวได้ภายใน 1 ปี นับจากวันที่ทำสัญญานี้ <br />
                                                                                             ผู้ร่วมธุรกิจจะยอมให้สิทธินั้นตกแก่บริษัทหรือตัวแทน ดำเนินการตามที่บริษัทเห็นสมควร	
                                                                                        </div>
                                                                                   </div>
                                       

                                                                    </div>
                                                                                                                                                                      
                                                                            <div class="row" style="padding-top: 1rem;">
                                                                                ธุรกิจร่วมการประกอบการค้า		<br />
                                                                                (ที่ผู้ร่วมธุรกิจดำเนินการ)		

                                                                            </div>
                                                                                                                                               
                                                                    <div class="row" style="padding-top: 1rem;">


                                                                        
                                                                        <%--<span class="input-group-text">บันทึกต่อท้ายสัญญา</span>--%>
                                                                                    <div class="table-responsive">
 
                                                                                        <div class="table-responsive" runat="server" >
                                                                                            <asp:Table id="Table2" class="table table-bordered table-condensed table-hover pnlstudentDetails" style='font-family:Tahoma, Courier, monospace; font-size:8pt;' 
                                                                                                                    runat="server" GridLines="Both" cellspacing="0" cellpadding="5" border="1" OnPageIndexChanging="OnPageIndexChanging" PageSize="10">
                                                                                                <asp:TableHeaderRow TableSection="TableHeader" runat="server">
                                                                                                <asp:TableHeaderCell RowSpan="2" style='margin:0 auto; text-align:center; background-color:white;' runat="server">ลำดับ</asp:TableHeaderCell>
                                                                                                <asp:TableHeaderCell RowSpan="2" style='margin:0 auto; text-align:center; background-color:white;' runat="server">ประเภทธุรกิจ</asp:TableHeaderCell>
                                                                                                <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:white;' runat="server">ขนาดพื้นที่</asp:TableHeaderCell>
                                                          
                                                                                                </asp:TableHeaderRow>
                                                                                                <asp:TableHeaderRow runat="server">
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:white;' runat="server"></asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:white;' runat="server"></asp:TableHeaderCell>                                                             
                                                                                                    <%--<asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">จำนวน</asp:TableHeaderCell>--%>                                                       
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:white;' runat="server"></asp:TableHeaderCell>
                                                                                                   
                                                                                                </asp:TableHeaderRow>  
                                                                                                
                                                                                                <asp:TableHeaderRow runat="server">
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:white;' runat="server"></asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:white;' runat="server"></asp:TableHeaderCell>                                                             
                                                                                                    <%--<asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">จำนวน</asp:TableHeaderCell>--%>                                                       
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:white;' runat="server"></asp:TableHeaderCell>
                                                                                                   
                                                                                                </asp:TableHeaderRow>   

                                                                                            </asp:Table>

                                                                                        </div>

                                                                                        <div class="row">
                                                                                            	
                                                                                            <span  class="span" >  หมายเหตุ	</span><br />
	                                                                                            หากผู้ร่วมธุรกิจ หยุดประกอบธุรกิจข้างต้นต่อเนื่องครบ 1 ปี ให้ถือว่าผู้ร่วมธุรกิจไม่ประสงค์ <br />
                                                                                            จะประกอบธุรกิจนั้นอีกต่อไปและให้สิทธินั้นตกแก่บริษัทหรือมอบให้ผู้อื่นดำเนินการตามที่บริษัท เห็นสมควร	

                                                                                        </div>
                                                                                   </div>
                                       

                                                                    </div>

                                            </asp:View>
                                            <asp:View ID="Tab8" runat="server" >

                                                                        <div class="input-group sm-col-2" >   
                                                                            <span class="input-group-text">เอกสารแนบท้าย 4  </span>
                                                                            <span class="input-group-text">เลขที่สัญญา  ................</span>
                                                                        </div>
                                                                      
                                                                            <div class="row" style="padding-top: 1rem;">
                                                                                    ค่าตอบแทนแก่ผู้ร่วมธุรกิจ	
                                                                            </div>

                                                                                    <div class="table-responsive">
 
                                                                                        <div class="table-responsive" runat="server" >
                                                                                            <asp:Table id="Table3" class="table table-bordered table-condensed table-hover pnlstudentDetails" style='font-family:Tahoma, Courier, monospace; font-size:8pt;' 
                                                                                                                    runat="server" GridLines="Both" cellspacing="0" cellpadding="5" border="1" OnPageIndexChanging="OnPageIndexChanging" PageSize="10">
                                                                                                <asp:TableHeaderRow TableSection="TableHeader" runat="server">
                                                                                                <asp:TableHeaderCell RowSpan="2" style='margin:0 auto; text-align:center; background-color:white;' runat="server">ลำดับ</asp:TableHeaderCell>
                                                                                                <asp:TableHeaderCell RowSpan="2" style='margin:0 auto; text-align:center; background-color:white;' runat="server">อัตราค่าตอบ</asp:TableHeaderCell>
                                                                                                <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:white;' runat="server">จำนวนเงิน((บาท)</asp:TableHeaderCell>
                                                          
                                                                                                </asp:TableHeaderRow>
                                                                                                <asp:TableHeaderRow runat="server">
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:white;' runat="server"></asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:white;' runat="server"></asp:TableHeaderCell>                                                             
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:white;' runat="server"></asp:TableHeaderCell>                                                       
                                                                                                    <%--<asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:white;' runat="server">(ตรม.)</asp:TableHeaderCell>--%>
                                                                                                   
                                                                                                </asp:TableHeaderRow>     
                                                                                                <asp:TableHeaderRow runat="server">
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:white;' runat="server"></asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:white;' runat="server"></asp:TableHeaderCell>                                                             
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:white;' runat="server"></asp:TableHeaderCell>                                                       
                                                                                                    <%--<asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:white;' runat="server">(ตรม.)</asp:TableHeaderCell>--%>
                                                                                                   
                                                                                                </asp:TableHeaderRow>                                                                                                  

                                                                                            </asp:Table>

                                                                                        </div>

                                                                                        <div class="row">
                                                                                             <span  class="span" >   เงื่อนไขการจ่ายค่าตอบแทนร่วมธุรกิจ	</span><br />
                                                                                                (1).  	ค่าตอบแทนร่วมธุรกิจกำหนดจ่ายปีละ 1 ครั้งโดยที่ปลอดค่าร่วมธุรกิจและค่าเช่า.......   (......... ถึง ........ )	<br />
	                                                                                                ค่าร่วมธุรกิจล่วงหน้า ชำระในวันที่จดทะเบียนเช่า แล้วเสร็จ	<br />
                                                                                                กรณีรายเดือน	ค่าตอบแทนร่วมธุรกิจต่อเดือน [ .......... ถึง ......] ของแต่ละเดือน ชำระ ภายในวันที่ 15  ของเดือนนั้น ๆ โดยเริ่มตั้งแต่ เดือน .... ..... เป็นต้นไป 	<br />
	                                                                                                ค่าตอบแทนร่วมธุรกิจต่อปี [.......... ถึง ......] ของแต่ละปี ชำระ ภายในวันที่ 15 .... ของปีนั้น ๆ 	<br />
                                                                                                (2).  	บริษัทฯ จะทำการโอนค่าตอบแทนร่วมธุรกิจ โดยจ่ายให้   .............. โดยผู้ร่วมธุรกิจ	<br />
                                                                                                 ยินยอมให้บริษัทฯ จ่ายค่าตอบแทนดังกล่าวผ่านบัญชี ............... สาขา...........  จังหวัด............ ประเภทบัญชี .......... 		<br />
                                                                                                 เลขที่บัญชี .............  โดยให้ถือเอาใบโอนเงินผ่านธนาคารดังกล่าว เป็นการชำระหนี้โดยถูกต้อง ครบถ้วนจากบริษัทฯแล้ว		<br />
                                                                                                (3).	ผู้ร่วมธุรกิจ ยินยอมให้บริษัทฯ ดำเนินการหักภาษี ณ ที่จ่าย ตามที่กฎหมายกำหนดไว้จนครบถ้วนทุกครั้งที่มีการจ่าย  	<br />
                                                                                                 ค่าตอบแทนให้  ผู้ร่วมธุรกิจ 		


                                                                                        </div>
                                                                                   </div>

                                            </asp:View>

                                            <asp:View ID="Tab9" runat="server">
                                                    <div class="input-group sm-col-2" >   
                                                        <span class="input-group-text">เอกสารแนบท้าย 1  </span>
                                                                           
                                                    </div>
                                                                      

                                             <%--       <div class="input-group sm-3">
                                                        <asp:RadioButton ID="rdoFull" runat="server" groupname= "fullarea" text=" สัญญาร่วมธุรกิจ+เช่า " Checked="true"/> &nbsp;&nbsp;
                                                        <asp:RadioButton ID="rdoPart" runat="server" groupname= "fullarea" text=" สัญญาเช่า " />
                                                    </div>--%>

                                            <%--        <div class="col-4">
                                           
                                                                <div class="input-group sm-3">
                                                                    <div class="input-group-prepend">
                                                                        <span class="input-group-text">สัญญา</span>
                                                                    </div>
                                                                    <asp:DropDownList class="form-control" ID="DropDownList2"  runat="server" ></asp:DropDownList>    
                                                                </div>
                                           
                                                    </div>--%>

                                            </asp:View>
                                            <asp:View ID="Tab10" runat="server">

                                            </asp:View>

                                        </asp:MultiView>


                            </div>
  

              
<!---->
                                 
                                  <%--<hr style="height:2px;border-width:0;color:gray;background-color:lavenderblush">--%>



                        <!--End Area Input-->
                        </div>
                  </div>


            </div>            <!-- /.container-fluid -->
            <!-- Sticky Footer -->
             <hr style="height:2px;border-width:0;color:gray;background-color:gray">
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
        jQuery('[id$=txtBirthday]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08

            lang: 'th',// แสดงภาษาไทย
            yearOffset: 543,// ใช้ปี พ.ศ. บวก 543 เพิ่มเข้าไปในปี ค.ศ  
            timepicker: false,
            scrollInput: false,
            format:'d/m/Y'
        });
    </script>

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

    <script type="text/javascript">
        jQuery('[id$=txtContractBeginDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08

            lang: 'th',// แสดงภาษาไทย
            yearOffset: 543,// ใช้ปี พ.ศ. บวก 543 เพิ่มเข้าไปในปี ค.ศ  
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>

    <script type="text/javascript">
        jQuery('[id$=txtContractEndDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08

            lang: 'th',// แสดงภาษาไทย
            yearOffset: 543,// ใช้ปี พ.ศ. บวก 543 เพิ่มเข้าไปในปี ค.ศ  
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>

    <script type="text/javascript">
        jQuery('[id$=txtDueDateFix]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            lang: 'th',// แสดงภาษาไทย
            yearOffset: 543,// ใช้ปี พ.ศ. บวก 543 เพิ่มเข้าไปในปี ค.ศ  
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>

    <script type="text/javascript">
        jQuery('[id$=txtBeginDateFix]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            lang: 'th',// แสดงภาษาไทย
            yearOffset: 543,// ใช้ปี พ.ศ. บวก 543 เพิ่มเข้าไปในปี ค.ศ  
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>

    <script type="text/javascript">
        jQuery('[id$=txtEndDateFix]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            lang: 'th',// แสดงภาษาไทย
            yearOffset: 543,// ใช้ปี พ.ศ. บวก 543 เพิ่มเข้าไปในปี ค.ศ  
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
      
  
</asp:Content>
