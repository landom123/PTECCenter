<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="requestcontract2.aspx.vb" Inherits="PTECCENTER.requestcontract2" EnableEventValidation = "false" Culture="th-TH"  %>

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
            color: green;
            /*font-style: italic;*/
            background-color:whitesmoke ;
        }

        /*####################### menu ########################*/
        div.hideSkiplink
        {
            background-color:white;
            width:100%;
        }

        div.menu
        {
            padding: 4px 0px 4px 8px;
        }

        div.menu ul
        {
            list-style: none;
            margin: 0px;
            padding: 0px;
            width: auto;

        }

        div.menu ul li a, div.menu ul li a:visited
        {
            background-color: #465c71;
            border: 1px #4e667d solid;
            color: #dde4ec;
            display: block;
            line-height: 1.35em;
            padding: 4px 20px;
            text-decoration: none;
            white-space: nowrap;
        }

        div.menu ul li a:hover
        {
            background-color: #bfcbd6;
            color: #465c71;
            text-decoration: none;
        }

        div.menu ul li a:active
        {
            background-color: #465c71;

            color: #cfdbe6;
            text-decoration: none;
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
                           <a href="requestcontract.aspx"><i class="fa fa-tasks" aria-hidden="true"></i></a> ขอสัญญาใหม่
                  </li>
                </ol>
                <p></p>
                <div class="row">
                    <div class="col-6">
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" New " />
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" Save " />
                        <%--<asp:Button ID="btnDel" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" Delete " />--%>

                        <button type="button" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" id="btnFromAddDetail" runat="server" 
                            data-toggle="modal" data-target="#exampleModal" data-backdrop="static" data-keyboard="false" data-whatever="new">เพิ่มสาขาใหม่</button>

                       <asp:Button ID="btnApprove" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" อนุมัติ " />
                        <asp:Button ID="btnPreview" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" Preview " />
                    </div>
                    <div class="col-6" style="text-align:right">
                        <asp:Button ID="BtnContract" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" กลับ สัญญา " />
                    </div>
                    

                </div>
                    
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
                    
                        <div class="row" style="padding-top: 0.2rem;">
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
                                                        <span class="input-group-text">ผู้ออกสัญญา</span>
                                                    </div>
                                                    <%--<asp:DropDownList class="form-control" ID="cboStatus"  runat="server" ></asp:DropDownList>--%>   
                                                    <asp:DropDownList class="form-control" ID="cboCompany" AutoPostBack="true" OnSelectedIndexChanged="cboCompany_SelectedIndexChanged" runat="server" ></asp:DropDownList> 
                                                </div>
                                           
                                    </div>
                                <div class="col-2">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                        <span class="input-group-text">สถานะ</span>
                                        </div>
                                        <asp:Label class="form-control" ID="lblStatus" style="background-color:darkgreen;color:white" runat="server" ></asp:Label>    

                                    </div>
                                </div>
                                <div class="col-2">
                                                <div class="input-group sm-3">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text">สถานะ</span>
                                                    </div>                                                   
                                                    <asp:DropDownList class="form-control" ID="cboStatusLaw" runat="server" ></asp:DropDownList>    
                                                    <asp:Button ID="btnStatusLaw" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" Update " />
                                                </div>
                                </div>
                            </div>

                            <%-- input area --%>
                              <div class="card-body" runat="server">
                      
                                    <div class="row" style="padding-top: 0.2rem;">
                                            <div class="col-3" runat="server">
                                                <div class="input-group sm-3" runat="server">
                                                    <div class="input-group-prepend" runat="server">
                                                    <span class="input-group-text">รหัสสาขา</span>
                                                    </div>
                                                        <asp:DropDownList ID="cboBranch" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged ="OnSelectedIndexChanged_cboBranch" ></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-3" runat="server">
                                                <div class="input-group sm-3" runat="server">
                                                    <div class="input-group-prepend" runat="server">
                                                    <span class="input-group-text" runat="server">กลุ่มสัญญา</span>
                                                    </div>
                                                    <asp:DropDownList class="form-control" ID="cboMainContact" AutoPostBack="true" OnSelectedIndexChanged="cboMainContact_SelectedIndexChanged" runat="server" ></asp:DropDownList>    
                                                </div>
                                            </div>

                                            <div class="col-3" runat="server">
                                                <div class="input-group sm-3" runat="server">
                                                    <div class="input-group-prepend" runat="server">
                                                    <span class="input-group-text" runat="server">ประเภทสัญญา</span>
                                                    </div>
                                                    <asp:DropDownList class="form-control" ID="cboContractType" AutoPostBack="true" OnSelectedIndexChanged="cboContractType_SelectedIndexChanged"  runat="server" ></asp:DropDownList>    
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


                                    </div>
                                  <div class="row" style="padding-top: 0.2rem;" >
                                             <div class="col-3" runat="server">
                                                <div class="input-group sm-2" runat="server">
                                                    <div class="input-group-prepend" runat="server">
                                                    <span class="input-group-text" runat="server"> วันที่เริ่มสัญญา</span>
                                                    </div>
                                                    <asp:TextBox class="form-control" ID="txtContractBeginDate" style="background-color:white" runat="server" AutoPostBack="true" OnTextChanged="txtContractBeginDate_TextChanged"></asp:TextBox>    

                                                </div>
                                            </div>

                                             <div class="col-3" >
                                                <div class="input-group sm-2" runat="server">
                                                    <div class="input-group-prepend" runat="server">
                                                    <span class="input-group-text" >วันที่สิ้นสุดสัญญา</span>
                                                    </div>
                                                    <asp:TextBox class="form-control" ID="txtContractEndDate" style="background-color:white" runat="server" AutoPostBack="true" OnTextChanged="txtContractBeginDate_TextChanged" ></asp:TextBox>    

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
                            <div class="card-body clear hideSkiplink">

 
                                <%--<span style="color:blue">รายละเอียดสัญญา</span>--%>
                                <%--<br />--%>
                                <asp:Menu ID="Menu1" Width="100%" runat="server" orientation="Horizontal" StaticEnableDefaultPopOutImage="False" 
                                            EnableViewState="False" IncludeStyleBlock="false"  CssClass="menu"
                                            OnMenuItemClick="Menu1_MenuItemClick" RenderingMode="Table"  >
                                            <Items>
                                                <asp:MenuItem  Text=" คู่สัญญา(บุคคลธรรมดา) " Value="0"></asp:MenuItem>
                                                <asp:MenuItem  Text=" คู่สัญญา(นิติบุคคล) " Value="1"></asp:MenuItem>
                                                <asp:MenuItem  Text=" ทรัพย์สิน " Value="4"></asp:MenuItem>
                                                <asp:MenuItem  Text=" เงือนไขค่าสัญญา " Value="2"></asp:MenuItem>
                                                <asp:MenuItem  Text=" ช่องทางการชำระเงิน " Value="3"></asp:MenuItem>
                                                <asp:MenuItem  Text=" ร่วมแนบ1 " Value="8"></asp:MenuItem>
                                                <asp:MenuItem  Text=" ร่วมแนบ2 " Value="5"></asp:MenuItem>
                                                <asp:MenuItem  Text=" ร่วมแนบ3 " Value="6"></asp:MenuItem>
                                                <asp:MenuItem  Text=" ร่วมแนบ4 " Value="7"></asp:MenuItem>
                                               <%-- <asp:MenuItem  Text=" หนังสือมอบอำนาจ " Value="9"></asp:MenuItem>
                                                <asp:MenuItem  Text=" สัญญาพื้นที่เช่า Non Oil " Value="10"></asp:MenuItem>
                                                <asp:MenuItem  Text=" สัญญาอืนๆ " Value="11"></asp:MenuItem>
                                                <asp:MenuItem  Text=" สัญญาเช่าที่ดิน+ร่วมธุรกิจ " Value="12"></asp:MenuItem>--%>
                                            </Items>
                                     
                                        </asp:Menu>
                                       <%-- <hr style="height:2px;border-width:0;color:gray;background-color:gray">--%>
                                        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                            <asp:View ID="Tab1" runat="server">
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
                               

                                                                                        <div class="row" style="padding-top: 0.2rem; " runat="server">
                                                                                            <div class="col-md-3" runat="server">                                                
                                                                                                <div class="input-group sm-3" runat="server">
                                                                                                        <div class="input-group-prepend" runat="server">
                                                                                                            <span class="input-group-text" runat="server">* รหัส ผู้ให้เช่า</span>
                                                                                                        </div>
                                                                                                    <asp:TextBox class="form-control" ID="txtCustCode" runat="server"  ></asp:TextBox>
                                                                                                </div>
                                                                                            </div>
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

                                                                                       </div>

                                                                                       <div class="row" style="padding-top: 0.2rem;" runat="server">  
                                                                                            <div class="col-md-3" runat="server">                                                 
                                                                                                <div class="input-group sm-3" runat="server">
                                                                                                        <div class="input-group-prepend"  runat="server">
                                                                                                            <span class="input-group-text" runat="server">เพศ</span>
                                                                                                        </div>
                                                                                                    <asp:dropdownlist class="form-control" ID="cboSex" runat="server" ></asp:dropdownlist>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2" runat="server">                                                 
                                                                                                <div class="input-group sm-2" runat="server">
                                                                                                        <div class="input-group-prepend"  runat="server">
                                                                                                            <%--<span class="input-group-text" runat="server">เป็นเจ้าของที่ดิน</span>--%>
                                                                                                            <asp:checkbox class="input-group-text" ID="chkOwner" runat="server" text="เป็นเจ้าของที่ดิน" ></asp:checkbox>
                                                                                                        </div>
                                                                                                    
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="row" style="padding-top: 0.2rem;" runat="server">                                       
                                                                                                <div class="col-md-12" runat="server">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server"> 
                                                                                                                <span class="input-group-text" runat="server">* ที่อยู่</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtAddress" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                        </div>
                                                                                        <div class="row" style="padding-top: 0.2rem;">                                                                               
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

                                                                                        <div class="row col-sm-3 span2" style="padding-top: 0.2rem;" runat="server">
                                                                                                    พยาน
                                                                                        </div>

                                                                                        <div class="row" style="padding-top: 0.2rem;" runat="server">
                                                                                                <div class="col-md-3" runat="server" style="padding-top: 1rem;">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text"  runat="server">พยานคนที่ 1</span>
                                                                                                            </div>
                                                                                                            <asp:TextBox class="form-control" ID="txtPerWitness1" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-3" runat="server" style="padding-top: 1rem;">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text"  runat="server">พยานคนที่ 2</span>
                                                                                                            </div>
                                                                                                            <asp:TextBox class="form-control" ID="txtPerWitness2" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>                                                                                            
                                                                                        </div>

                                                                                        <div class="row col-sm-3 span2" style="padding-top: 0.2rem;" runat="server">                                                                                               
                                                                                                      ที่อยู่ส่งเอกสาร                                                                                                    
                                                                                        </div>

                                                                                        <div class="row" style="padding-top: 0.2rem;" runat="server">                                       
                                                                                                <div class="col-md-12" runat="server">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server"> 
                                                                                                                <span class="input-group-text" runat="server">ที่อยู่</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtSendAddr" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                        </div>

                                                                                        <div class="row" style="padding-top: 0.2rem;">
                                                                                                                                                                        
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

                                                                                              <div class="row col-sm-3 span2" style="padding-top: 0.2rem;" runat="server">
                                                                                               <%--     <span class="span2" style="color:blue" runat="server">--%>
                                                                                                      ผู้ติดต่อ
                                                                                                    <%--</span>--%>
                                                                                              </div>

                                                                                        <div class="row" style="padding-top: 0.2rem;">

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

                                                                                      <div class="row col-md-3" style="padding-top: 0.2rem;"  runat="server">
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
                                                                                                                    <asp:BoundField DataField="Witness1" HeaderText="พยานคนที่ 1" />
                                                                                                                    <asp:BoundField DataField="Witness2" HeaderText="พยานคนที่ 2" />

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
                                                                      <div class="row" style="padding-top: 0.2rem;"  runat="server">
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

                                                                    <div class="row" style="padding-top: 0.2rem; " runat="server">

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

                                                                    <div class="row" style="padding-top: 0.2rem;"  runat="server">  
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

                                                                                        <div class="row col-sm-3 span2" style="padding-top: 0.2rem;" runat="server">                                                                                               
                                                                                                      พยาน                                                                                                    
                                                                                        </div>

                                                                                        <div class="row" style="padding-top: 0.2rem;" runat="server">
                                                                                                <div class="col-md-3" runat="server" style="padding-top: 1rem;">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text"  runat="server">พยานคนที่ 1</span>
                                                                                                            </div>
                                                                                                            <asp:TextBox class="form-control" ID="txtComWitness1" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-3" runat="server" style="padding-top: 1rem;">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text"  runat="server">พยานคนที่ 2</span>
                                                                                                            </div>
                                                                                                            <asp:TextBox class="form-control" ID="txtComWitness2" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>                                                                                            
                                                                                        </div>
                                                                                        <div class="row col-sm-3 span2" style="padding-top: 0.2rem;" runat="server">                                                                                               
                                                                                                      ที่อยู่ส่งเอกสาร                                                                                                   
                                                                                        </div>

                                                                                        <div class="row" style="padding-top: 0.2rem;" runat="server">                                       
                                                                                                <div class="col-md-12" runat="server">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server"> 
                                                                                                                <span class="input-group-text" runat="server">ที่อยู่</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtComAddrSend" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                                                                        </div>

                                                                                        <div class="row" style="padding-top: 0.2rem;"  runat="server">
                                                                                                                                                                        
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
                                      
                                                                                                <div class="col-md-3" runat="server" style="padding-top: 0.2rem;">                                                
                                                                                                    <div class="input-group sm-3" runat="server">
                                                                                                            <div class="input-group-prepend" runat="server">
                                                                                                                <span class="input-group-text" runat="server">เบอร์โทร</span>
                                                                                                            </div>
                                                                                                        <asp:TextBox class="form-control" ID="txtComTelSend" runat="server" ></asp:TextBox>
                                                                                                    </div>
                                                                                                </div>
                                        
                                                                                        </div>

                                                                                              <div class="row col-sm-3 span2" style="padding-top: 0.2rem;" runat="server">
                                                                                               <%--     <span class="span2" style="color:blue" runat="server">--%>
                                                                                                      ผู้ติดต่อ
                                                                                                    <%--</span>--%>
                                                                                              </div>

                                                                                        <div class="row" style="padding-top: 0.2rem;"  runat="server">

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
                                                                  <div class="row col-md-3" style="padding-top: 0.2rem;">
                                                                        <asp:Button ID="btnAddCompanyPer" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" เพิ่มคู่สัญญา(นิติบุคคล) " />
                                                                        <asp:Button ID="btnEditCompanyPer" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" แก้คู่สัญญา(นิติบุคคล) " />
                                                                  </div>                  
                                                                                <div class="row" style="padding-top: 0.2rem;">

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
                                                                                                <asp:BoundField DataField="Witness1" HeaderText="พยานคนที่ 1" />
                                                                                                <asp:BoundField DataField="Witness2" HeaderText="พยานคนที่ 2" />

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
    

                                            </asp:View>
                                            <asp:View ID="Tab3" runat="server">
                                                <table width="100%"  cellpadding="0" cellspacing="0">
                                                    <tr valign="top">                           
                                                        <td class="TabArea" style="width: 100%">

                                                                    <div class="col-md-3" runat="server"> 
                                                                          <div class="row" style="padding-top: 0.2rem; " runat="server">
                                                                                <span class="span" style="color:blue" runat="server">
                                                                                  เงือนไขค่าสัญญา                                                 
                                                                                </span>
                                                                          </div>
                                                                    </div>

                                                                     <div class="list-group-item list-group-item-light" >

                                                                        <div class="row" style="padding-top: 0.2rem;">
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
                                                                        <div class="row" style="padding-top: 0.2rem;">

                                                                                    <div class="col-3">
                                                                                        <div class="input-group sm-3">
                                                                                            <div class="input-group-prepend">
                                                                                                <span class="input-group-text">* รอบการจ่าย</span>
                                                                                          </div>
                                                                                            <asp:RadioButton ID="rdoMonthFix" runat="server" groupname="paid" Checked="true"/>รายเดือน&nbsp;&nbsp;
                                                                                            <asp:RadioButton ID="rdoYearFix" runat="server" groupname="paid"/>รายปี &nbsp;&nbsp;
                                                                                            <asp:RadioButton ID="rdoOnceFix" runat="server" groupname="paid"/>จ่ายครั้งเดียว
                                                                                        </div>
                                                                                    </div>    
                                                                                    <div class="col-3">
                                                                                        <div class="input-group sm-3">
                                                                                            <div class="input-group-prepend">
                                                                                                <span class="input-group-text">* ความถี่การจ่าย</span>
                                                                                          </div>
                                                                                            <asp:TextBox class="form-control" ID="txtFrequencyFix" runat="server" ></asp:TextBox>
                                                                                            <%--<asp:DropDownList class="form-control" ID="cboFrequency"  runat="server" ></asp:DropDownList>--%> 
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
                                                                        <div class="row" style="padding-top: 0.2rem;">
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

                                                                        <div class="row" style="padding-top: 0.2rem;">
                   
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
                                                                        <div class="row" style="padding-top: 0.2rem;">
                                                                                <div class="col-2">
                                                                                        <div class="input-group sm-3">
                                                                                            <div class="input-group-prepend">
                                                                                                <span class="input-group-text">รวมทั้งหมด</span>
                                                                                          </div>
                                                                                            <asp:label class="form-control" ID="txtAmntPayment" runat="server" ></asp:label>
                                                                                        </div>
                                                                                </div>
                                                                        </div>

                                                                          <div class="row col-md-3" style="padding-top: 0.2rem;">
                                                                            <asp:Button ID="btnAddcontractFix" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" เพิ่มเงือนไขสัญญา " />
                                                                            <asp:Button ID="btnEditcontractFix" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" แก้เงือนไขสัญญา " />
                                                                          </div>
           
                                                                                        <div class="row" style="padding-top: 0.2rem;">
                                                                                            <div class="col-md-3" runat="server"> 
                                                                                                  <div class="row" style="padding-top: 0.2rem; " runat="server">
                                                                                                        <span class="span" style="color:darkgreen" runat="server">
                                                                                                          รวมทุกประเภท                                              
                                                                                                        </span>
                                                                                                  </div>
                                                                                            </div> 
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
                                                                                        <div class="row" style="padding-top: 0.2rem;">
                                                                                            <div class="col-md-3" runat="server"> 
                                                                                                  <div class="row" style="padding-top: 0.2rem; " runat="server">
                                                                                                        <span class="span" style="color:darkgreen" runat="server">
                                                                                                          Group BY : Preriod Date                                             
                                                                                                        </span>
                                                                                                  </div>
                                                                                            </div> 
                                                                                            <div class="table-responsive">
                                                                                                <asp:GridView ID="gvContractFix1"
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
                                                                                                        <asp:BoundField DataField="BeginDate" HeaderText="วันที่เริ่มต้น" />
                                                                                                        <asp:BoundField DataField="EndDate" HeaderText="วันที่สิ้นสุด" />
                                                                                                        <asp:BoundField DataField="PayTye1" HeaderText="ค่าหน้าดิน"/>
                                                                                                        <asp:BoundField DataField="PayTye2" HeaderText="ค่าจดเช่า" visible="false"/>
                                                                                                        <asp:BoundField DataField="PayTye3" HeaderText="ค่าอากรสัญญา"  visible="false"/>
                                                                                                        <asp:BoundField DataField="PayTye4" HeaderText="ค่าเช่าล่วงหน้า" />
                                                                                                        <asp:BoundField DataField="PayTye5" HeaderText="ค่าร่วมธุระกิจ" />                                                               
                                                                                                        <asp:BoundField DataField="PayTye6" HeaderText="ค่าร่วมธุรกิจล่วงหน้า" />
                                                                                                    <%--    <asp:BoundField DataField="ItemNo" HeaderText="ItemNo" />
                                                                                                        <asp:BoundField DataField="PayTypeID" HeaderText="PayTypeID" />
                                                                                                        <asp:BoundField DataField="DueTypeID" HeaderText="DueTypeID" />--%>

                                                                                                    </Columns>
                                                                                                </asp:GridView>

                                                                                            </div>
                                                                                        </div>    
                                                                        <!---->
                               

                                                                    </div>


                                                        </td>
                                                    </tr>
                                                </table>

                                            </asp:View>
                                            <asp:View ID="Tab4" runat="server">
                                                <table width="100%"  cellpadding="0" cellspacing="0">
                                                    <tr valign="top">
                                                        <td class="TabArea" style="width: 100%"> 

                                                                <div class="col-md-3" runat="server"> 
                                                                      <div class="row" style="padding-top: 0.2rem; " runat="server">
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

                                                                    <div class="row" style="padding-top: 0.2rem;">
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
                                                                    <div class="row" style="padding-top: 0.2rem;">
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

                                                                    <div class="row" style="padding-top: 0.2rem;">
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

                                                                      <div class="row col-md-3" style="padding-top: 0.2rem;">
                                                                        <asp:Button ID="btnAddPayment" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" บันทึกการจ่ายเงิน " />
                                                                        <asp:Button ID="btnAddPaymentEdit" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" แก้ไขการจ่ายเงิน " />
                                                                      </div>

                                                                    <!---->
                                                                                    <div class="row" style="padding-top: 0.2rem;">

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
             
                                            </asp:View>

                                            <asp:View ID="Tab5" runat="server">
                                                <table width="100%"  cellpadding="0" cellspacing="0">
                                                    <tr valign="top">
                                                        <td class="TabArea" style="width: 100%"> 

                                                                  <div class="row" style="padding-top: 0.2rem; " runat="server">
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

                                                                        <div class="row col-sm-3 span2" style="padding-top: 0.2rem;" runat="server">
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
                                                                            <div class="col-md-2" runat="server">                            
                                                                                <div class="input-group-prepend" runat="server">
                                                                                    <span class="input-group-text">เลขที่ดิน</span>
                                                                                    <asp:Textbox class="form-control" ID="txtLandNo2" runat="server"></asp:Textbox>  
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
                                                                                    <asp:Textbox class="form-control" ID="txtRai" runat="server" style="text-align:right "></asp:Textbox> 
                                                                                    <span class="input-group-text">ไร่</span>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-2" runat="server">                            
                                                                                <div class="input-group-prepend" runat="server">   
                                                                                    <asp:Textbox class="form-control" ID="txtNgan" runat="server" style="text-align:right "></asp:Textbox>  
                                                                                    <span class="input-group-text">งาน</span>
                                                                                </div>
                                                                            </div>                                                                                                                                                  
                                                                            <div class="col-md-2" runat="server">                            
                                                                                <div class="input-group-prepend" runat="server">   
                                                                                    <asp:Textbox class="form-control" ID="txtWa" runat="server" style="text-align:right "></asp:Textbox>
                                                                                    <span class="input-group-text">ตารางวา</span>
                                                                                </div>
                                                                            </div>     
                                                                            <asp:Button ID="btnAddAssetMain" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" เพิ่มรายการ " />

                                                                        </div>

                                                                        <div class="card-body">

                                                                                    <div class="row" style="padding-top: 0.2rem;">

                                                                                        <div class="table-responsive">
                                                                                            <asp:GridView ID="gvAssetMain"
                                                                                                style="text-align:center;width: 50%;height:10%"
                                                                                                class="table table-striped table-bordered"
                                                                                                AllowSorting="True"
                                                                                                AllowPaging="false"
                                                                                                AutoGenerateColumns="false"
                                                                                                EmptyDataText=" ไม่มีข้อมูล "                                     
                                                                                                OnRowDataBound="OnRowDataBound_AssetMain"                                                                                                
                                                                                                OnSelectedIndexChanged="OnSelectedIndexChanged_AssetMain"                                                                                               
                                                                                                runat="server" CssClass="table table-striped">
                                                                                                <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-Size="Smaller" ForeColor="White" />
                                                                                                <AlternatingRowStyle BackColor="#CCCCFF" />

                                                                                                <Columns>                                                             
                                                                                                    <asp:BoundField DataField="ItemNo" HeaderText="#" />
                                                                                                    <asp:BoundField DataField="DocNo" HeaderText="เลขที่โฉนด" />
                                                                                                    <asp:BoundField DataField="ServayNo" HeaderText="เลขหน้าสำรวจ" />
                                                                                                    <asp:BoundField DataField="Landno" HeaderText="เลขหน้าที่ดิน" />
                                                                                                    <asp:BoundField DataField="Rai" ItemStyle-HorizontalAlign="Right"  HeaderText="ไร่" />
                                                                                                    <asp:BoundField DataField="Ngan" ItemStyle-HorizontalAlign="Right"  HeaderText="งาน" />
                                                                                                    <asp:BoundField DataField="Wa" ItemStyle-HorizontalAlign ="Right"  HeaderText="ตารางวา" />

                                                                                                    <asp:BoundField DataField="Addr" HeaderText="เลขที่" />   
                                                                                                    <asp:BoundField DataField="SubDistrict" HeaderText="ตำบล/แขวง" />   
                                                                                                    <asp:BoundField DataField="District" HeaderText="อำเภอ/เขต" />   
                                                                                                    <asp:BoundField DataField="Province" HeaderText="จังหวัด" />   
                                                                                                    <asp:BoundField DataField="PostCode" HeaderText="ไปรษณีย์" />                                                               
                                                                                                                                                                            
                                                                                                    <asp:BoundField DataField="ID" HeaderText="ID" />
                                                                                                    <asp:BoundField DataField="ItemNo" HeaderText="ItemNo" />                                                                                        
                                                                                                    <asp:BoundField DataField="AssetID" HeaderText="AssetID" />

                                                                                                </Columns>
                                                                                                                                                                                       
                                                                                            </asp:GridView>
                                                                                     
                                                                                        </div>
                                                                                    </div>

                                                                        </div>

                                                                        <div class="row col-sm-3 span2" style="padding-top: 0.2rem;" runat="server">                                                                                            
                                                                                 แบ่งเช่าบางส่วน                                                                                                
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

                                                                            <div class="row" style="padding-top: 0.2rem;" runat="server">

                                                                                <div class="col-md-2" runat="server">                            
                                                                                    <div class="input-group-prepend" runat="server">       
                                                                                        <asp:Textbox class="form-control" ID="txtRai1" runat="server" style="text-align:right "></asp:Textbox>  
                                                                                        <span class="input-group-text">ไร่</span>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-2" runat="server">                            
                                                                                    <div class="input-group-prepend" runat="server"> 
                                                                                        <asp:Textbox class="form-control" ID="txtNgan1" runat="server" style="text-align:right "></asp:Textbox>  
                                                                                        <span class="input-group-text">งาน</span>
                                                                                    </div>
                                                                                </div>                                                                                                                                                  
                                                                                <div class="col-md-2" runat="server">                            
                                                                                    <div class="input-group-prepend" runat="server">  
                                                                                        <asp:Textbox class="form-control" ID="txtWa1" runat="server" style="text-align:right "></asp:Textbox>   
                                                                                        <span class="input-group-text">ตารางวา</span>
                                                                                    </div>
                                                                                </div>   
                                                                                <div class="col-md-2" runat="server">                            
                                                                                    <div class="input-group-prepend" runat="server">   
                                                                                        <span class="input-group-text">ด้านหน้าติดถนนประมาณ</span>
                                                                                        <asp:Textbox class="form-control" ID="txtRoadTo" runat="server"></asp:Textbox>    
                                                                                    </div>
                                                                                </div>                                                                              
                                                                                <asp:Button ID="btnAddAssetSublet" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" เพิ่มรายการแบ่งเช่า " />
                                                                            </div>
                                                                                                                                                
                                                                        <div class="card-body">

                                                                                    <div class="row" style="padding-top: 0.2rem;">

                                                                                        <div class="table-responsive">
                                                                                            <asp:GridView ID="gvSublet"
                                                                                                style="text-align:center;width: 50%;height:10%"
                                                                                                class="table table-striped table-bordered"
                                                                                                AllowSorting="True"
                                                                                                AllowPaging="false"
                                                                                                AutoGenerateColumns="false"
                                                                                                EmptyDataText=" ไม่มีข้อมูล "                                     
                                                                                                OnRowDataBound="OnRowDataBound_AssetSublet"                                                                                                
                                                                                                OnSelectedIndexChanged="OnSelectedIndexChanged_AssetSublet"                                                                                               
                                                                                                runat="server" CssClass="table table-striped">
                                                                                                <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-Size="Smaller" ForeColor="White" />
                                                                                                <AlternatingRowStyle BackColor="#CCCCFF" />

                                                                                                <Columns>                                                             
                                                                                                    <asp:BoundField DataField="DeedNo" HeaderStyle-Width="10%" HeaderStyle-Height="5%" HeaderText="เลขที่โฉนด" />
                                                                                                    <asp:BoundField DataField="SurveyNo" HeaderStyle-Width="10%" HeaderStyle-Height="5%" HeaderText="เลขที่หน้าสำรวจ" />
                                                                                                    <asp:BoundField DataField="LandNo" HeaderStyle-Width="1%" HeaderStyle-Height="5%" HeaderText="LandNo" />
                                                                                                    <asp:BoundField DataField="Rai" HeaderStyle-Width="5%" HeaderStyle-Height="5%" ItemStyle-HorizontalAlign="Right"  HeaderText="ไร่" />
                                                                                                    <asp:BoundField DataField="Ngan" HeaderStyle-Width="5%" HeaderStyle-Height="5%" ItemStyle-HorizontalAlign="Right"  HeaderText="งาน" />
                                                                                                    <asp:BoundField DataField="Wa" HeaderStyle-Width="5%" HeaderStyle-Height="5%" ItemStyle-HorizontalAlign ="Right"  HeaderText="ตารางวา" />
                                                                                                    <asp:BoundField DataField="LongRoad" HeaderStyle-Width="10%" HeaderStyle-Height="5%" HeaderText="ด้านหน้าติดถนน" />                                                               
                                                                                                                                                                            
                                                                                                    <asp:BoundField DataField="ID" HeaderStyle-Width="1%" HeaderStyle-Height="5%" HeaderText="ID" />
                                                                                                    <asp:BoundField DataField="ItemNo" HeaderStyle-Width="1%" HeaderStyle-Height="5%" HeaderText="ItemNo" />                                                                                        

                                                                                                </Columns>
                                                                                                                                                                                       
                                                                                            </asp:GridView>
                                                                                     
                                                                                        </div>
                                                                                    </div>

                                                                        </div>

                                                                            <div class="row col-sm-3 span2" style="padding-top: 0.2rem;" runat="server">
                                                                                  พยาน
                                                                           </div>
                                                                           <div class="row" style="padding-top: 0.2rem;" runat="server">
                                                                                  <div class="col-md-3" runat="server" style="padding-top: 1rem;">                                                
                                                                                         <div class="input-group sm-3" runat="server">
                                                                                                <div class="input-group-prepend" runat="server">
                                                                                                      <span class="input-group-text"  runat="server">พยานคนที่ 1</span>
                                                                                                </div>
                                                                                                <asp:TextBox class="form-control" ID="TextBox1" runat="server" ></asp:TextBox>
                                                                                        </div>
                                                                                  </div>
                                                                                  <div class="col-md-3" runat="server" style="padding-top: 1rem;">                                                
                                                                                        <div class="input-group sm-3" runat="server">
                                                                                               <div class="input-group-prepend" runat="server">
                                                                                                     <span class="input-group-text"  runat="server">พยานคนที่ 2</span>
                                                                                               </div>
                                                                                               <asp:TextBox class="form-control" ID="TextBox2" runat="server" ></asp:TextBox>
                                                                                        </div>
                                                                                  </div>                                                                                            
                                                                           </div>

                                                                        <br />
                                                                                   <div class="row col-sm-3 span2" style="padding-top: 0.2rem;" runat="server">                                                                                                               
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
                                                                                                    <asp:Textbox class="form-control" ID="txtRai2" runat="server" style="text-align:right "></asp:Textbox>  
                                                                                                    <span class="input-group-text">ไร่</span>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-2" runat="server">                            
                                                                                                <div class="input-group-prepend" runat="server">  
                                                                                                    <asp:Textbox class="form-control" ID="txtNgan2" runat="server" style="text-align:right "></asp:Textbox>  
                                                                                                    <span class="input-group-text">งาน</span>
                                                                                                </div>
                                                                                            </div>                                                                                                                                                  
                                                                                            <div class="col-md-2" runat="server">                            
                                                                                                <div class="input-group-prepend" runat="server">   
                                                                                                    <asp:Textbox class="form-control" ID="txtWa2" runat="server" style="text-align:right "></asp:Textbox>  
                                                                                                    <span class="input-group-text">ตารางวา</span>
                                                                                                </div>
                                                                                            </div>                                                                                                                   

                                                                                        </div>     
                                                                                    </div>
                                                                                    <div class="row" style="padding-top: 0.2rem;">
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
                                                                                    <div class="row" style="padding-top: 0.2rem;" runat="server">
                                                                                        <div class="col-md-4" runat="server">
                                                                                            <%--<INPUT id="File4" type="file" runat="server" NAME="oFile">--%>
                                                                                            <asp:FileUpload id="fiUpload" runat="server"></asp:FileUpload>
                                                                                            <asp:button id="btnUpload1" type="submit" text="Upload" runat="server"  Visible="false"></asp:button>
                                                                                            <asp:Panel ID="Panel4" Visible="False" Runat="server">
                                                                                                <asp:Label id="Label5" Runat="server"></asp:Label>
                                                                                            </asp:Panel>
                                                                                        </div>
                                                                                        <div class="col-md-4" runat="server">
                                                                                           <%-- <INPUT id="File5" type="file" runat="server" NAME="oFile">--%>
                                                                                            <asp:FileUpload id="fiUpload2" runat="server"></asp:FileUpload>
                                                                                            <asp:button id="btnUpload2" type="submit" text="Upload" runat="server"  Visible="false"></asp:button>
                                                                                            <asp:Panel ID="Panel5" Visible="False" Runat="server">
                                                                                                <asp:Label id="Label6" Runat="server"></asp:Label>
                                                                                            </asp:Panel>
                                                                                        </div>       
                                                                                  </div>

                                                                                <div class="row" runat="server">
                                                                                        <div  class="col-md-4" runat="server">
                                                                                                <asp:Image ID="image1" runat="server" Width="150px" Height="100px"/>
                                                                                        </div>
                                                                                        <div  class="col-md-4" runat="server">
                                                                                                <asp:Image ID="image2" runat="server" Width="150px" Height="100px"/>
                                                                                        </div>
                                                                                </div>

                                                                    </div>

                                                                      <div class="row col-md-3" style="padding-top: 0.2rem;">
                                                                        <asp:Button ID="btnAddasset" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" บันทึกทรัพย์สิน " />
                                                                        <asp:Button ID="btnEditasset" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" แก้ไขทรัพย์สิน " />
                                                                      </div>

                                                                                    <div class="row" style="padding-top: 0.2rem;">

                                                                                        <div class="table-responsive">
                                                                                            <asp:GridView ID="gvAsset"
                                                                                                class="table table-striped table-bordered"
                                                                                                AllowSorting="True"
                                                                                                AllowPaging="false"
                                                                                                AutoGenerateColumns="false"
                                                                                                EmptyDataText=" ไม่มีข้อมูล "                                     
                                                                                                OnRowDataBound="OnRowDataBoundAsset"                                                                                                
                                                                                                OnSelectedIndexChanged="OnSelectedIndexChangedAsset"                                                                                               
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
                                                                                                    <asp:BoundField DataField="RoadTo" ItemStyle-HorizontalAlign="Right" HeaderText="RoadTo" />
                                                                                                    <asp:BoundField DataField="Area" ItemStyle-HorizontalAlign="Right" HeaderText="Area" />
                                                                                                    <asp:BoundField DataField="Farm" ItemStyle-HorizontalAlign="Right" HeaderText="Farm" />
                                                                                                    <asp:BoundField DataField="Squarewah" ItemStyle-HorizontalAlign="Right" HeaderText="Squarewah" />
                                                                                                    <asp:BoundField DataField="Lanno" HeaderText="Lanno" />
                                                                                                    <asp:BoundField DataField="Area1" ItemStyle-HorizontalAlign="Right" HeaderText="Area1" />
                                                                                                    <asp:BoundField DataField="Farm1" ItemStyle-HorizontalAlign="Right" HeaderText="Farm1" />
                                                                                                    <asp:BoundField DataField="Squarewah1" ItemStyle-HorizontalAlign="Right" HeaderText="Squarewah1" />
                                                                                                    <asp:BoundField DataField="Buno" HeaderText="Buno" />
                                                                                                    <asp:BoundField DataField="Area2" ItemStyle-HorizontalAlign="Right" HeaderText="Area2" />
                                                                                                    <asp:BoundField DataField="Farm2" ItemStyle-HorizontalAlign="Right" HeaderText="Farm2" />
                                                                                                    <asp:BoundField DataField="Squarewah2" ItemStyle-HorizontalAlign="Right" HeaderText="Squarewah2" />
                                                                                                    <asp:BoundField DataField="Remark" HeaderText="Remark" />
                                                                                                    <asp:BoundField DataField="Remark1" HeaderText="Remark1" />
                                                                                                    <asp:BoundField DataField="Landno2" HeaderText="Landno2" />
                                                                                                    
                                                                                                    <asp:ImageField DataImageUrlField="pic1" HeaderText="pic1"></asp:ImageField>  
                                                                                                    <asp:ImageField DataImageUrlField="pic2" HeaderText="pic2"></asp:ImageField> 
                                                                                                    <%--<asp:BoundField DataField="pic1"  HeaderText="pic1" />--%>
                                                                                                    <%--<asp:BoundField DataField="pic2" HeaderText="pic2" />--%>
                                                                                                    

                                                                                                    <%--<asp:Image ID="pic1"  HeaderText="pic1" />--%>
                                                                                                    <%--<asp:Image ID="pic2" HeaderText="pic2" />--%>

                                                                                                    <asp:BoundField DataField="ID" HeaderText="ID" />
                                                                                                    <asp:BoundField DataField="ItemNo" HeaderText="ItemNo" />

                                                                                                    <%--<asp:TemplateField HeaderText="pic1">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Image ID="pic1" runat="server" Height="100px" Width="100px"
                                                                                                                ImageUrl='<%# Eval(Session("imageUrl1")) %>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="pic2">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Image ID="pic2" runat="server" Height="100px" Width="100px"
                                                                                                                ImageUrl='<%# Eval(Session("imageUrl2")) %>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>--%>

                                                                                                </Columns>
                                                                                                                                                                                       
                                                                                            </asp:GridView>
                                                                                            <%--<div id="dialog" style="display: none">
                                                                                        </div>--%>
                                                                                        <!-- end Table payment-->
                                                                                        </div>
                                                                                    </div>
                                                          

                                                        </td>
                                                    </tr>
                                                </table>
                                    
                                            </asp:View>

                                            <asp:View ID="Tab6" runat="server" >
                                                                    <div class="row" style="padding-top: 0.2rem;">
                                                                        <div class="input-group sm-col-2" >   
                                                                            <span class="input-group-text">โฉนดที่ดินเลขที่</span>
                                                                            <span class="input-group-text">เลขที่สัญญา   <%= Session("sDocNo")%> </span>
                                                                           
                                                                        </div>
                                                                      
                                                                            <div class="row col-md-8" style="padding-top: 0.2rem;">                                                                                
                                                                              <span  class="span" >  บันทึกต่อท้ายสัญญา	</span> <br />
                                                                                	          บันทึกต่อท้ายสัญญานี้ถือเป็นส่วนหนึ่งของสัญญาเลขที่ เลขที่สัญญา เลขที่สัญญา  <%= Session("sDocNo")%>  ระหว่างบริษัท เพียวพลังงานไทย จำกัด (บริษัท ) <br />
                                                                                                และ <br />
                                                                                                    1. <%= Session("Contact1")%>  <br />
                                                                                                    2. <%= Session("Contact2")%>  <br />
                                                                                                    3. <%= Session("Contact3")%>  <br />
                                                                                                    4. <%= Session("Contact4")%>  <br />                                                                                
                                                                                                    ( ผู้ร่วมธุรกิจ) เพื่อบันทึกรายการทรัพย์สินของผู้ร่วมธุรกิจ ดังนี้ 	<br />
                                                                                                ที่ดินตามสัญญาร่วมธุรกิจ ตั้งอยู่ที่   <%= Session("Registryto")%> 	
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
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:right; background-color:lightskyblue;' runat="server">จำนวน</asp:TableHeaderCell>                                                       
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:lightskyblue;' runat="server">รายละเอียด/ภาพถ่าย</asp:TableHeaderCell>
                                                                                                   
                                                                                                </asp:TableHeaderRow>

                                                                                                <asp:TableRow Style='margin: 0 auto; text-align: right;' runat="server">
                                                                                                    <asp:TableCell style='width=10%;' runat="server">1</asp:TableCell>
                                                                                                    <asp:TableCell runat="server" ColumnSpan="1">
                                                                                                            <div class="card-body ">
                                                                                                                <div class="input-group sm-col-2" >                                                                                                               
                                                                                                                    <span class="input-group-text">โฉนดที่ดินเลขที่</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:left;width:auto' ID="txtAtt2DocNo1" runat="server"></asp:Textbox>                                                                                                                     
                                                                                                                </div>

                                                                                                                <div class="input-group sm-col-2" >                                                                                                               
                                                                                                                    <span class="input-group-text">เลขที่ดิน</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:left;width:auto' ID="txtAtt2No1" runat="server"></asp:Textbox> 
                                                                                                                    <span class="input-group-text">เช่าเต็มแปลง</span>                                                                                                                  
                                                                                                                </div>

                                                                                                                <div class="input-group sm-col-2" >  
                                                                                                                    <span class="input-group-text">ด้านหน้าติดถนนประมาณ</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:right; width:auto' ID="txtAtt2RoadTo" runat="server"></asp:Textbox>
                                                                                                                    <span class="input-group-text">เมตร</span>
                                                                                                                </div>

                                                                                                                <div class="input-group sm-col-2" >                                                                                                               
                                                                                                                    <span class="input-group-text">เนื้อที่</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="txtAtt2Rai1" runat="server"></asp:Textbox>  
                                                                                                                    <span class="input-group-text">ไร่</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="txtAtt2Ngan1" runat="server"></asp:Textbox> 
                                                                                                                    <span class="input-group-text">งาน</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:right; width:10%' ID="txtAtt2Wa1" runat="server"></asp:Textbox>
                                                                                                                    <span class="input-group-text">ตารางวา</span>
                                                                                                                </div>
                                                         
                                                                                                                <div class="input-group sm-col-2" >    
                                                                                                                    <span class="input-group-text">บรรยาย 1</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:left;width:auto' ID="txtAtt2Remark1" runat="server"></asp:Textbox>                                                                                                                 
                                                                                                                </div>
                                                                                                                <div class="input-group sm-col-2" >                                                                                                                 
                                                                                                                    <span class="input-group-text">บรรยาย 2</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:left;width:auto' ID="txtAtt2Remark11" runat="server"></asp:Textbox>                                                                                                                 
                                                                                                                </div>    
                                                                                                             </div>                                                                                                                             
                                                                                                   

                                                                                                        </asp:TableCell>
                                                                                                    <asp:TableCell runat="server">

                                                                                                            <div class="input-group sm-col-2" >                                                                                                               
                                                                                                                <span class="input-group-text">เนื้อที่</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="txtAtt2Rai11" runat="server"></asp:Textbox>  
                                                                                                                <span class="input-group-text">ไร่</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="txtAtt2Ngan11" runat="server"></asp:Textbox> 
                                                                                                                <span class="input-group-text">งาน</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right; width:10%' ID="txtAtt2Wa11" runat="server"></asp:Textbox>
                                                                                                                <span class="input-group-text">ตารางวา</span>
                                                                                                            </div>

                                                                                                            <div class="input-group sm-col-2" >                                                                                                               
                                                                                                                <span class="input-group-text">แบ่งเช่าบางส่วน</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="txtAtt2Rai12" runat="server"></asp:Textbox>  
                                                                                                                <span class="input-group-text">ไร่</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="txtAtt2Ngan12" runat="server"></asp:Textbox> 
                                                                                                                <span class="input-group-text">งาน</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right; width:10%' ID="txtAtt2Wa12" runat="server"></asp:Textbox>
                                                                                                                <span class="input-group-text">ตารางวา</span>
                                                                                                            </div>
                                                                                                            <div class="input-group sm-col-2" >
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="txtAtt2Rai13" runat="server"></asp:Textbox> 
                                                                                                                <span class="input-group-text">ไร่</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="txtAtt2Ngan13" runat="server"></asp:Textbox> 
                                                                                                                <span class="input-group-text">งาน</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right; width:10%' ID="txtAtt2Wa13" runat="server"></asp:Textbox>
                                                                                                                <span class="input-group-text">ตารางวา</span>
                                                                                                            </div>

                                                                                                            <div class="input-group sm-col-2" >    
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="txtAtt2Rai14" runat="server"></asp:Textbox> 
                                                                                                                <span class="input-group-text">ไร่</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="txtAtt2Ngan14" runat="server"></asp:Textbox> 
                                                                                                                <span class="input-group-text">งาน</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right; width:10%' ID="txtAtt2Wa14" runat="server"></asp:Textbox>
                                                                                                                <span class="input-group-text">ตารางวา</span>
                                                                                                            </div>

                                                                                                    </asp:TableCell>

                                                                                                    <asp:TableCell runat="server">
                                                                                                            <div class="row" runat="server">
                                                                                                                    <div  class="col-md-4" runat="server">
                                                                                                                            <asp:Image ID="image3" runat="server" Width="150px" Height="100px"/>
                                                                                                                    </div>
                                                                                                                    <div  class="col-md-4" runat="server">
                                                                                                                            <asp:Image ID="image4" runat="server" Width="150px" Height="100px"/>
                                                                                                                    </div>
                                                                                                            </div>
                                                                                                    </asp:TableCell>
                                                                                                </asp:TableRow>

                                                                                                <asp:TableRow Style='margin: 0 auto; text-align: right;' runat="server">
                                                                                                    <asp:TableCell style='width=10%;' runat="server">2</asp:TableCell>
                                                                                                    <asp:TableCell runat="server" ColumnSpan="1">
                                                                                                            <div class="card-body ">
                                                                                                                <div class="input-group sm-col-2" >                                                                                                               
                                                                                                                    <span class="input-group-text">โฉนดที่ดินเลขที่</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:left;width:auto' ID="txtAtt2DocNo2" runat="server"></asp:Textbox>                                                                                                                     
                                                                                                                </div>

                                                                                                                <div class="input-group sm-col-2" >                                                                                                               
                                                                                                                    <span class="input-group-text">เลขที่ดิน</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:left;width:auto' ID="txtAtt2No2" runat="server"></asp:Textbox> 
                                                                                                                    <span class="input-group-text">เช่าเต็มแปลง</span>                                                                                                                  
                                                                                                                </div>

                                                                                                                <div class="input-group sm-col-2" >  
                                                                                                                    <span class="input-group-text">ด้านหน้าติดถนนประมาณ</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:right; width:auto' ID="txtAtt2RoadTo2" runat="server"></asp:Textbox>
                                                                                                                    <span class="input-group-text">เมตร</span>
                                                                                                                </div>

                                                                                                                <div class="input-group sm-col-2" >                                                                                                               
                                                                                                                    <span class="input-group-text">เนื้อที่</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="txtAtt2Rai2" runat="server"></asp:Textbox>  
                                                                                                                    <span class="input-group-text">ไร่</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="txtAtt2Ngan2" runat="server"></asp:Textbox> 
                                                                                                                    <span class="input-group-text">งาน</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:right; width:10%' ID="txtAtt2Wa2" runat="server"></asp:Textbox>
                                                                                                                    <span class="input-group-text">ตารางวา</span>
                                                                                                                </div>
                                                         
                                                                                                                <div class="input-group sm-col-2" >    
                                                                                                                    <span class="input-group-text">บรรยาย 1</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:left;width:auto' ID="txtAtt2Remark2" runat="server"></asp:Textbox>                                                                                                                 
                                                                                                                </div>
                                                                                                                <div class="input-group sm-col-2" >                                                                                                                 
                                                                                                                    <span class="input-group-text">บรรยาย 2</span>
                                                                                                                    <asp:Textbox class="form-control" style='margin:0 auto; text-align:left;width:auto' ID="txtAtt2Remark22" runat="server"></asp:Textbox>                                                                                                                 
                                                                                                                </div>    
                                                                                                             </div>                                                                                                                             
                                                                                                   

                                                                                                        </asp:TableCell>
                                                                                                    <asp:TableCell runat="server">

                                                                                                            <div class="input-group sm-col-2" >                                                                                                               
                                                                                                                <span class="input-group-text">เนื้อที่</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="txtAtt2Rai21" runat="server"></asp:Textbox>  
                                                                                                                <span class="input-group-text">ไร่</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="txtAtt2Ngan21" runat="server"></asp:Textbox> 
                                                                                                                <span class="input-group-text">งาน</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right; width:10%' ID="txtAtt2Wa21" runat="server"></asp:Textbox>
                                                                                                                <span class="input-group-text">ตารางวา</span>
                                                                                                            </div>

                                                                                                            <div class="input-group sm-col-2" >                                                                                                               
                                                                                                                <span class="input-group-text">แบ่งเช่าบางส่วน</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="txtAtt2Rai22" runat="server"></asp:Textbox>  
                                                                                                                <span class="input-group-text">ไร่</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="txtAtt2Ngan22" runat="server"></asp:Textbox> 
                                                                                                                <span class="input-group-text">งาน</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right; width:10%' ID="txtAtt2Wa22" runat="server"></asp:Textbox>
                                                                                                                <span class="input-group-text">ตารางวา</span>
                                                                                                            </div>

                                                                                                            <div class="input-group sm-col-2" >
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="txtAtt2Rai23" runat="server"></asp:Textbox> 
                                                                                                                <span class="input-group-text">ไร่</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="txtAtt2Ngan23" runat="server"></asp:Textbox> 
                                                                                                                <span class="input-group-text">งาน</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right; width:10%' ID="txtAtt2Wa23" runat="server"></asp:Textbox>
                                                                                                                <span class="input-group-text">ตารางวา</span>
                                                                                                            </div>

                                                                                                            <div class="input-group sm-col-2" >    
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="txtAtt2Rai24" runat="server"></asp:Textbox> 
                                                                                                                <span class="input-group-text">ไร่</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right;width:10%' ID="txtAtt2Ngan24" runat="server"></asp:Textbox> 
                                                                                                                <span class="input-group-text">งาน</span>
                                                                                                                <asp:Textbox class="form-control" style='margin:0 auto; text-align:right; width:10%' ID="txtAtt2Wa24" runat="server"></asp:Textbox>
                                                                                                                <span class="input-group-text">ตารางวา</span>
                                                                                                            </div>

                                                                                                    </asp:TableCell>
                                                                                                    <asp:TableCell runat="server">
                                                                                                            <div class="row" runat="server">
                                                                                                                    <div  class="col-md-4" runat="server">
                                                                                                                            <asp:Image ID="image5" runat="server" Width="150px" Height="100px"/>
                                                                                                                    </div>
                                                                                                                    <div  class="col-md-4" runat="server">
                                                                                                                            <asp:Image ID="image6" runat="server" Width="150px" Height="100px"/>
                                                                                                                    </div>
                                                                                                            </div>
                                                                                                    </asp:TableCell>
                                                                         
                                                                                                </asp:TableRow>

                                                                                            </asp:Table>

                                                                                        </div>
                                                                                   </div>
                                       

                                                                    </div>
                                            </asp:View>
                                            <asp:View ID="Tab7" runat="server" >

                                                <table width="100%"  cellpadding="0" cellspacing="0">
                                                    <tr valign="top">
                                                        <td class="TabArea" style="width: 100% ">
                                                        	<div class="list-group-item list-group-item-light"  runat="server">
								
                                                                        <div class="input-group sm-col-2" >   
                                                                            <span class="input-group-text">เอกสารแนบท้าย 3  </span>
                                                                            <span class="input-group-text">เลขที่สัญญา    <%= Session("sDocNo")%> </span>
                                                                        </div>
                                                                      
                                                                            <div class="row" style="padding-top: 0.2rem;">
                                                                                ธุรกิจร่วมการประกอบการค้า		<br />
                                                                                (ที่ผู้ร่วมธุรกิจประสงค์จะดำเนินการ)			

                                                                            </div>
                                                                            
                                                                      
                                                                    <div class="row" style="padding-top: 1rem;">

                                                                       <div class="col-md-4" runat="server">                            
                                                                                <div class="input-group-prepend" runat="server"> 
                                                                                    <span class="input-group-text">ประเภทธุรกิจ</span>
                                                                                    <%--<asp:label class="form-control" ID="lblAtt3Type" runat="server"></asp:label>--%>
                                                                                    <asp:textbox class="form-control" ID="txtAtt3Type" runat="server"></asp:textbox>
                                                                                </div>
                                                                       </div>
                                                                       <div class="col-md-4" runat="server">                            
                                                                                <div class="input-group-prepend" runat="server"> 
                                                                                    <span class="input-group-text">ขนาดพื้นที่</span>
                                                                                    <asp:Textbox class="form-control" ID="txtAtt3Size" runat="server"></asp:Textbox>    
                                                                                </div>
                                                                       </div>
                                                                        
                                                           <%--           <div class="row col-md-3" style="padding-top: 0.2rem;">
                                                                        <asp:Button ID="Button1" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" บันทึกทรัพย์สิน " />
                                                                        <asp:Button ID="Button4" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" แก้ไขทรัพย์สิน " />
                                                                      </div>--%>
                                                                    <asp:Button ID="btnAtt3Add" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" เพิ่ม " />

                                                                    </div>
                                                                       <div class="row" style="padding-top: 1rem;">
                                                                                   
                                                                                        <div class="table-responsive" runat="server" >
                                                                                            <asp:GridView ID="gvAtt3Add1" 
                                                                                                class="table table-striped table-bordered"
                                                                                                AllowSorting="True"
                                                                                                AllowPaging="false"
                                                                                                AutoGenerateColumns="false"
                                                                                                EmptyDataText=" ไม่มีข้อมูล "   
                                                                                                OnRowDataBound="OnRowDataBoundAtt3Add1"  
                                                                                                OnPageIndexChanging="OnSelectedIndexChangedAtt3Add1" 
                                                                                                runat="server" CssClass="table table-striped">
                                                                                                <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-Size="Smaller" ForeColor="White" />
                                                                                                <AlternatingRowStyle BackColor="#CCCCFF" />
                                                                                            
                                                                                            <%--    <asp:TableHeaderRow TableSection="TableHeader" runat="server">
                                                                                                    <asp:TableHeaderCell RowSpan="2" style='margin:0 auto; text-align:center; background-color:white;' runat="server">ลำดับ</asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell RowSpan="2" style='margin:0 auto; text-align:center; background-color:white;' runat="server">ประเภทธุรกิจ</asp:TableHeaderCell>
                                                                                                    <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:white;' runat="server">ขนาดพื้นที่</asp:TableHeaderCell>                                                          
                                                                                                </asp:TableHeaderRow>   --%>                                                                                                                                                                         
                                                                                                <columns>
                                                                                                    <asp:BoundField DataField="ItemNo" HeaderText="ลำดับ" />
                                                                                                    <asp:BoundField DataField="TypeName" HeaderText="ประเภทธุรกิจ" />
                                                                                                    <asp:BoundField DataField="Size" HeaderText="ขนาดพื้นที่" />
                                                                                                </columns>
                                                                                            </asp:GridView>

                                                                                        </div>
                
                                                                                        <div class="row">
                                                                                         
                                                                                            <span  class="span" >  หมายเหตุ	</span><br />
	                                                                                            หากผู้ร่วมธุรกิจ ไม่สามารถเปิดดำเนินการธุรกิจดังกล่าวได้ภายใน 1 ปี นับจากวันที่ทำสัญญานี้ <br />
                                                                                             ผู้ร่วมธุรกิจจะยอมให้สิทธินั้นตกแก่บริษัทหรือตัวแทน ดำเนินการตามที่บริษัทเห็นสมควร	
                                                                                        </div>
                                                                                 
                                       
                                                                       </div>

                                                                                                                                                                      
                                                                            <div class="row" style="padding-top: 0.2rem;">
                                                                                ธุรกิจร่วมการประกอบการค้า		<br />
                                                                                (ที่ผู้ร่วมธุรกิจดำเนินการ)		

                                                                            </div>
                                                                                                                                               
                                                                            <div class="row" style="padding-top: 1rem;">                                                                           

                                                                               <div class="col-md-4" runat="server">                            
                                                                                        <div class="input-group-prepend" runat="server"> 
                                                                                            <span class="input-group-text">ประเภทธุรกิจ</span>
                                                                                            <%--<asp:label class="form-control" ID="lblAtt3Type2" runat="server"></asp:label>--%> 
                                                                                            <asp:textbox class="form-control" ID="txtAtt3Type2" runat="server"></asp:textbox> 
                                                                                        </div>
                                                                               </div>
                                                                               <div class="col-md-4" runat="server">                            
                                                                                        <div class="input-group-prepend" runat="server"> 
                                                                                            <span class="input-group-text">ขนาดพื้นที่</span>
                                                                                            <asp:Textbox class="form-control" ID="txtAtt3SizeAdd2" runat="server"></asp:Textbox>    
                                                                                        </div>
                                                                               </div>
                                                                                                                                    
                                                                                <asp:Button ID="btnAtt3Add2" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" เพิ่ม " />

                                                                            </div>
                                                                        
                                                                        <%--<span class="input-group-text">บันทึกต่อท้ายสัญญา</span>--%>
                                                                             <div  class="row" style="padding-top: 1rem;">
                                                                                        <div class="table-responsive" runat="server" >
                                                                                            <asp:GridView ID="gvAtt3Add2" 
                                                                                                class="table table-striped table-bordered"
                                                                                                AllowSorting="True"
                                                                                                AllowPaging="false"
                                                                                                AutoGenerateColumns="false"
                                                                                                EmptyDataText=" ไม่มีข้อมูล "   
                                                                                                OnRowDataBound="OnRowDataBoundAtt3Add2"  
                                                                                                OnPageIndexChanging="OnSelectedIndexChangedAtt3Add2" 
                                                                                                runat="server" CssClass="table table-striped">
                                                                                                <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-Size="Smaller" ForeColor="White" />
                                                                                                <AlternatingRowStyle BackColor="#CCCCFF" />
                                                                                                                                                                                                                                                                                                                                              
                                                                                                <columns>
                                                                                                    <asp:BoundField DataField="ItemNo" HeaderText="ลำดับ" />
                                                                                                    <asp:BoundField DataField="TypeName" HeaderText="ประเภทธุรกิจ" />
                                                                                                    <asp:BoundField DataField="Size" HeaderText="ขนาดพื้นที่" />
                                                                                                </columns>
                                                                                            </asp:GridView>

                                                                                        </div>
                

                                                                                        <div class="row">
                                                                                            	
                                                                                            <span  class="span" >  หมายเหตุ	</span><br />
	                                                                                            หากผู้ร่วมธุรกิจ หยุดประกอบธุรกิจข้างต้นต่อเนื่องครบ 1 ปี ให้ถือว่าผู้ร่วมธุรกิจไม่ประสงค์ <br />
                                                                                            จะประกอบธุรกิจนั้นอีกต่อไปและให้สิทธินั้นตกแก่บริษัทหรือมอบให้ผู้อื่นดำเนินการตามที่บริษัท เห็นสมควร	

                                                                                        </div>  
                                                                             </div>       
								                            </div>
							                            </td>
                                                    </tr>
                                                </table>                                                                                   

                                            </asp:View>
                                            <asp:View ID="Tab8" runat="server" >

                                                <table width="100%"  cellpadding="0" cellspacing="0">
                                                    <tr valign="top">
                                                        <td class="TabArea" style="width: 100% ">
                                                        	<div class="list-group-item list-group-item-light"  runat="server">
								

                                                                        <div class="input-group sm-col-2" >   
                                                                            <span class="input-group-text">เอกสารแนบท้าย 4  </span>
                                                                            <span class="input-group-text">เลขที่สัญญา   <%= Session("sDocNo")%>  </span>
                                                                        </div>
                                                                      
                                                                            <div class="row" style="padding-top: 0.2rem;">
                                                                                    ค่าตอบแทนแก่ผู้ร่วมธุรกิจ	
                                                                            </div>
                                                                            <div class="row" style="padding-top: 1rem;">                                                                           

                                                                             <div class="col-md-4" runat="server">                            
                                                                                        <div class="input-group-prepend" runat="server"> 
                                                                                            <span class="input-group-text">อัตราค่าตอบ</span>
                                                                                            <%--<asp:label class="form-control" ID="Label1" runat="server"></asp:label>--%>
                                                                                            <asp:textbox class="form-control" ID="txtAtt4ServiceRate" runat="server"></asp:textbox>
                                                                                        </div>
                                                                               </div>
                                                                               <div class="col-md-4" runat="server">                            
                                                                                        <div class="input-group-prepend" runat="server"> 
                                                                                            <span class="input-group-text">จำนวนเงิน(บาท)</span>
                                                                                            <asp:Textbox class="form-control" ID="txtAtt4Amnt" runat="server"></asp:Textbox>    
                                                                                        </div>
                                                                               </div>
                                                                                                                                    
                                                                                <asp:Button ID="btnAtt4Add" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" เพิ่ม " />

                                                                            </div>
                                                                                     <div  class="row" style="padding-top: 1rem;">
                                                                                                <div class="table-responsive" runat="server" >
                                                                                                    <asp:GridView ID="gvAtt4" 
                                                                                                        class="table table-striped table-bordered"
                                                                                                        AllowSorting="True"
                                                                                                        AllowPaging="false"
                                                                                                        AutoGenerateColumns="false"
                                                                                                        EmptyDataText=" ไม่มีข้อมูล "   
                                                                                                        OnRowDataBound="OnRowDataBoundAtt3Add2"  
                                                                                                        OnPageIndexChanging="OnSelectedIndexChangedAtt3Add2" 
                                                                                                        runat="server" CssClass="table table-striped">
                                                                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-Size="Smaller" ForeColor="White" />
                                                                                                        <AlternatingRowStyle BackColor="#CCCCFF" />
                                                                                                                                                                                                                                                                                                                                              
                                                                                                        <columns>
                                                                                                            <asp:BoundField DataField="ItemNo" HeaderText="ลำดับ" />
                                                                                                            <asp:BoundField DataField="ServiceRate" HeaderText="อัตราค่าตอบ" />
                                                                                                            <asp:BoundField DataField="Amount" HeaderText="จำนวนเงิน(บาท)" />
                                                                                                        </columns>
                                                                                                    </asp:GridView>

                                                                                                </div>
                                                                                     </div>  

                                                                                    <div class="col-md-4" runat="server">
 
 

                                                                                        <div class="row">
                                                                                             <span  class="span" >   เงื่อนไขการจ่ายค่าตอบแทนร่วมธุรกิจ	</span><br />
                                                                                                (1).  	ค่าตอบแทนร่วมธุรกิจกำหนดจ่ายปีละ 1 ครั้งโดยที่ปลอดค่าร่วมธุรกิจและค่าเช่า.......   (......... ถึง ........ )	<br />
	                                                                                                ค่าร่วมธุรกิจล่วงหน้า ชำระในวันที่จดทะเบียนเช่า แล้วเสร็จ	<br />
                                                                                                กรณีรายเดือน	ค่าตอบแทนร่วมธุรกิจต่อเดือน [  <%= Session("BeginDate")%>  ถึง  <%= Session("EndDate")%>  ] ของแต่ละเดือน ชำระ ภายในวันที่ 15  ของเดือนนั้น ๆ โดยเริ่มตั้งแต่ เดือน .... ..... เป็นต้นไป 	<br />
	                                                                                                ค่าตอบแทนร่วมธุรกิจต่อปี [ <%= Session("BeginDate")%>  ถึง  <%= Session("EndDate")%> ] ของแต่ละปี ชำระ ภายในวันที่ 15 .... ของปีนั้น ๆ 	<br />
                                                                                                (2).  	บริษัทฯ จะทำการโอนค่าตอบแทนร่วมธุรกิจ โดยจ่ายให้   <br />
                                                                                                        1. <%= Session("AccName1")%> <br />
                                                                                                        2. <%= Session("AccName2")%> <br />
                                                                                                        3. <%= Session("AccName3")%> <br />
                                                                                                        4. <%= Session("AccName4")%> <br />
                                                                                                 โดยผู้ร่วมธุรกิจ	<br />
                                                                                                 ยินยอมให้บริษัทฯ จ่ายค่าตอบแทนดังกล่าวผ่านบัญชี <br />
                                                                                                        1.<%= Session("BankName1")%>  สาขา  <%= Session("BranchName1")%>   จังหวัด............ ประเภทบัญชี .......... 		<br />
                                                                                                 เลขที่บัญชี  <%= Session("AccCode1")%>   โดยให้ถือเอาใบโอนเงินผ่านธนาคารดังกล่าว เป็นการชำระหนี้โดยถูกต้อง ครบถ้วนจากบริษัทฯแล้ว		<br />
                                                                                                        2.<%= Session("BankName2")%>  สาขา  <%= Session("BranchName2")%>   จังหวัด............ ประเภทบัญชี .......... 		<br />
                                                                                                 เลขที่บัญชี  <%= Session("AccCode2")%>   โดยให้ถือเอาใบโอนเงินผ่านธนาคารดังกล่าว เป็นการชำระหนี้โดยถูกต้อง ครบถ้วนจากบริษัทฯแล้ว		<br />
                                                                                                        3.<%= Session("BankName3")%>  สาขา  <%= Session("BranchName3")%>   จังหวัด............ ประเภทบัญชี .......... 		<br />
                                                                                                 เลขที่บัญชี  <%= Session("AccCode3")%>   โดยให้ถือเอาใบโอนเงินผ่านธนาคารดังกล่าว เป็นการชำระหนี้โดยถูกต้อง ครบถ้วนจากบริษัทฯแล้ว		<br />
                                                                                                        4.<%= Session("BankName4")%>  สาขา  <%= Session("BranchName4")%>   จังหวัด............ ประเภทบัญชี .......... 		<br />
                                                                                                 เลขที่บัญชี  <%= Session("AccCode4")%>   โดยให้ถือเอาใบโอนเงินผ่านธนาคารดังกล่าว เป็นการชำระหนี้โดยถูกต้อง ครบถ้วนจากบริษัทฯแล้ว		<br />
                                                                                                (3).	ผู้ร่วมธุรกิจ ยินยอมให้บริษัทฯ ดำเนินการหักภาษี ณ ที่จ่าย ตามที่กฎหมายกำหนดไว้จนครบถ้วนทุกครั้งที่มีการจ่าย  	<br />
                                                                                                 ค่าตอบแทนให้  ผู้ร่วมธุรกิจ 		


                                                                                        </div>
                                                                                   </div>

								                                </div>
							                                </td>
                                                        </tr>
                                                    </table>

                                            </asp:View>

                                            <asp:View ID="Tab9" runat="server">
                                                <table width="100%"  cellpadding="0" cellspacing="0">
                                                    <tr valign="top">
                                                        <td class="TabArea" style="width: 100% ">
                                                        
                                                            <div class="list-group-item list-group-item-light"  runat="server">
                                                                <div class="input-group sm-col-2" >   
                                                                    <span class="input-group-text">เอกสารแนบท้าย 1  </span>
                                                                           
                                                                </div>
                                                                      
                                                                 <div class="row" style="padding-top: 0.2rem;">
                   
                                                                      <div class="col-3">
                                                                           <div class="input-group sm-3">
                                                                                                <div class="input-group-prepend">
                                                                                                    <span class="input-group-text">ชื่อสัญญาหลัก</span>
                                                                                                    </div>
                                                                                                    <asp:DropDownList class="form-control" ID="cboMainContract"  runat="server" ></asp:DropDownList>    
                                                                                                </div>
                                                                     </div>                   

                                                                      <div class="col-3">
                                                                                                <div class="input-group sm-3">
                                                                                                    <div class="input-group-prepend">
                                                                                                    <span class="input-group-text">ผู้จ่ายภาษีที่ดิน</span>
                                                                                                    </div>
                                                                                                    <asp:DropDownList class="form-control" ID="cboTaxpayground"  runat="server" ></asp:DropDownList>    
                                                                                                </div>
                                                                        </div>
                 
                                                                           <div class="col-3">
                                                                                                <div class="input-group sm-3">
                                                                                                    <div class="input-group-prepend">
                                                                                                    <span class="input-group-text">ผู้จ่ายภาษีสิ่งปลูกสร้าง</span>
                                                                                                    </div>
                                                                                                    <asp:DropDownList class="form-control" ID="cboTaxpaybuild"  runat="server" ></asp:DropDownList>    
                                                                                               </div>
                                                                           </div>                                                       
                                                                        <asp:LinkButton ID="lnkView" runat="server" Text="View (ดู)" OnClick="View"></asp:LinkButton>
                                                                        <hr />
                                                                        <asp:Literal ID="ltEmbed" runat="server" />

                                                               </div>  
                                                            </div>
  
                                                            
							                            </td>
                                                    </tr>
                                                </table>
                                            </asp:View>
                                           
                                            <asp:View ID="Tab13" runat="server">

                                                <table width="100%"  cellpadding="0" cellspacing="0">
                                                    <tr valign="top">
                                                        <td class="TabArea" style="width: 100% ">
                                                        	<div class="list-group-item list-group-item-light"  runat="server">								

                                                                    <div class="input-group sm-col-2" >   
                                                                        <span class="input-group-text"> สัญญาเช่าที่ดิน+ร่วมธุรกิจ </span>                                                                           
                                                                    </div>
                                                                    <div class="row" style="padding-top: 0.2rem;">
                                                                               <div class="col-6">
                                                                                    <div class="input-group sm-6">
                                                                                        <div class="input-group-prepend">
                                                                                             <span class="input-group-text">ประเภทสัญญา</span>
                                                                                        </div>
                                                                                        <asp:label class="form-control" ID="lblContractTypeRentJoin" runat="server"></asp:label>
                                                                                    </div>
                                                                               </div>                                     
                                                                             <div class="col-4" runat="server">
                                                                                <div class="input-group sm-4" runat="server">
                                                                                    <div class="input-group-prepend" runat="server">
                                                                                    <span class="input-group-text" runat="server"> วันที่ทำสัญญา</span>
                                                                                    </div>
                                                                                    <div class="col-3" runat="server">
                                                                                        <asp:TextBox class="form-control" ID="txtDocDateRentJoin" style="background-color:white" runat="server" AutoPostBack="true" OnTextChanged="txtBeginDate4_TextChanged"></asp:TextBox> 
                                                                                    </div>   
                                                                                </div>
                                                                            </div>

                                                                    </div>
                                                                    <div class="row" style="padding-top: 0.2rem;">
                                                                               <div class="col-4">
                                                                                    <div class="input-group sm-4">
                                                                                        <div class="input-group-prepend">
                                                                                             <span class="input-group-text">ชื่อคู่สัญญา 1</span>
                                                                                        </div>
                                                                                        <asp:TextBox class="form-control" ID="txtVendorContract1" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                               </div>
                                                                               <div class="col-4">
                                                                                    <div class="input-group sm-4">
                                                                                        <div class="input-group-prepend">
                                                                                             <span class="input-group-text">2</span>
                                                                                        </div>
                                                                                        <asp:TextBox class="form-control" ID="txtVendorContract2" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                               </div>
                                                                               <div class="col-4">
                                                                                    <div class="input-group sm-4">
                                                                                        <div class="input-group-prepend">
                                                                                             <span class="input-group-text">3</span>
                                                                                        </div>
                                                                                        <asp:TextBox class="form-control" ID="txtVendorContract3" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                               </div>
                                                                    </div>
                                                                    <div class="row" style="padding-top: 0.2rem;">
                                                                               <div class="col-4">
                                                                                    <div class="input-group sm-4">
                                                                                        <div class="input-group-prepend">
                                                                                             <span class="input-group-text">ผู้มีอำนาจลงนาม 1</span>
                                                                                        </div>
                                                                                        <asp:TextBox class="form-control" ID="txtVendorSign1" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                               </div>
                                                                               <div class="col-4">
                                                                                    <div class="input-group sm-4">
                                                                                        <div class="input-group-prepend">
                                                                                             <span class="input-group-text">2</span>
                                                                                        </div>
                                                                                        <asp:TextBox class="form-control" ID="txtVendorSign2" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                               </div>
                                                                               <div class="col-4">
                                                                                    <div class="input-group sm-4">
                                                                                        <div class="input-group-prepend">
                                                                                             <span class="input-group-text">3</span>
                                                                                        </div>
                                                                                        <asp:TextBox class="form-control" ID="txtVendorSign3" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                               </div>
                                                                    </div>
                                                                    <div class="row" style="padding-top: 0.2rem;">
                                                                               <div class="col-4">
                                                                                    <div class="input-group sm-4">
                                                                                        <div class="input-group-prepend">
                                                                                             <span class="input-group-text">พยานในสัญญาร่วม</span>
                                                                                        </div>
                                                                                        <asp:TextBox class="form-control" ID="txtWitnessRentJoin" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                               </div>                                                 
                                                                    </div>
                                                                    <div class="row" style="padding-top: 0.2rem;">
                                                                               <div class="col-6">
                                                                                    <div class="input-group sm-6">
                                                                                        <div class="input-group-prepend">
                                                                                             <span class="input-group-text">ที่อยู่ / ที่ตั้งสำนักงาน</span>
                                                                                        </div>
                                                                                        <asp:TextBox class="form-control" ID="txtAddrRentJoin" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                               </div>

                                                                    </div>
                                                                    <div class="row" style="padding-top: 0.2rem;">
                                                                               <div class="col-6">
                                                                                    <div class="input-group sm-6">
                                                                                        <div class="input-group-prepend">
                                                                                             <span class="input-group-text">ชื่อ / เบอร์โทร ผู้ติดต่อ</span>
                                                                                        </div>
                                                                                        <asp:TextBox class="form-control" ID="txtContactRentJoin" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                               </div>
                                                                               <div class="col-6">
                                                                                    <div class="input-group sm-6">
                                                                                        <div class="input-group-prepend">
                                                                                             <span class="input-group-text">สาขา(สถานีบริการน้ำมันเชื้อเพลิง)</span>
                                                                                        </div>
                                                                                        <asp:TextBox class="form-control" ID="txtBranchRentJoin" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                               </div>
                                                                    </div>
                                                                    <div class="row" style="padding-top: 0.2rem;">
                                                                                <div class="col-4" runat="server">
                                                                                           <div class="input-group sm-4" runat="server">
                                                                                                <div class="input-group-prepend" runat="server">
                                                                                                    <span class="input-group-text" runat="server"> วันที่เริ่มต้น / สิ้นสุดสัญญา</span>
                                                                                                </div>
                                                                                                <div class="col-3" runat="server">
                                                                                                    <asp:TextBox class="form-control" ID="txtBeginDateRentJoin" style="background-color:white" runat="server" AutoPostBack="true" OnTextChanged="txtBeginDate4_TextChanged"></asp:TextBox> 
                                                                                                </div>
                                                                                                <div class="col-3" runat="server">
                                                                                                    <asp:TextBox class="form-control" ID="txtEndDateRentJoin" style="background-color:white" runat="server" AutoPostBack="true" OnTextChanged="txtEndDate4_TextChanged"></asp:TextBox> 
                                                                                                </div>     
                                                                                            </div>
                                                                                 </div>
                                                                    </div>
                                                                    <div class="row" style="padding-top: 0.2rem;">
                                                                               <div class="col-6">
                                                                                    <div class="input-group sm-6">
                                                                                        <div class="input-group-prepend">
                                                                                             <span class="input-group-text">กำหนดจ่ายค่าเช่า</span>
                                                                                        </div>
                                                                                        <asp:TextBox class="form-control" ID="txtDuePayRentJoin" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                               </div>

                                                                    </div>
                                                                    <div class="row" style="padding-top: 0.2rem;">
                                                                               <div class="col-6">
                                                                                    <div class="input-group sm-3">
                                                                                        <div class="input-group-prepend">
                                                                                             <span class="input-group-text">พื้นที่เช่า</span>
                                                                                        </div>
                                                                                        <asp:TextBox class="form-control" ID="txtRentAreaRentJoin1" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                               </div>
                                                                    </div>
                                                                    <div class="row" style="padding-top: 0.2rem;">
                                                                               <div class="col-6">
                                                                                    <div class="input-group sm-3">                                                                 
                                                                                        <asp:TextBox class="form-control" ID="txtRentAreaRentJoin2" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                               </div>
                                                                    </div>
                                                                    <div class="row" style="padding-top: 0.2rem;">
                                                                               <div class="col-6">
                                                                                    <div class="input-group sm-3">                                                                 
                                                                                        <asp:TextBox class="form-control" ID="txtRentAreaRentJoin3" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                               </div>
                                                                    </div>
                                                                    <div class="row" style="padding-top: 0.2rem;">
                                                                               <div class="col-6">
                                                                                    <div class="input-group sm-3">                                                                 
                                                                                        <asp:TextBox class="form-control" ID="txtRentAreaRentJoin4" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                               </div>
                                                                    </div>
                                                                    <div class="row" style="padding-top: 0.2rem;">
                                                                               <div class="col-6">
                                                                                    <div class="input-group sm-3">
                                                                                        <div class="input-group-prepend">
                                                                                             <span class="input-group-text">อื่นๆ</span>
                                                                                        </div>
                                                                                        <asp:TextBox class="form-control" ID="txtOthRentJoin1" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                               </div>
                                                                    </div>
                                                                    <div class="row" style="padding-top: 0.2rem;">
                                                                               <div class="col-6">
                                                                                    <div class="input-group sm-3">                                                                 
                                                                                        <asp:TextBox class="form-control" ID="txtOthRentJoin2" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                               </div>
                                                                    </div>
                                                                    <div class="row" style="padding-top: 0.2rem;">
                                                                               <div class="col-6">
                                                                                    <div class="input-group sm-3">                                                                 
                                                                                        <asp:TextBox class="form-control" ID="txtOthRentJoin3" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                               </div>
                                                                    </div>
                                                                    <div class="row" style="padding-top: 0.2rem;">
                                                                               <div class="col-6">
                                                                                    <div class="input-group sm-3">                                                                 
                                                                                        <asp:TextBox class="form-control" ID="txtOthRentJoin4" runat="server"></asp:TextBox>
                                                                                    </div>
                                                                               </div>
                                                                    </div>
                                                                    <div class="row col-md-3" style="padding-top: 0.2rem;"  runat="server">
                                                                         <asp:Button ID="btnAddRentJoin" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" เพิ่มสัญญา " />
                                                                         <asp:Button ID="btnEditRentJoin" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" แก้สัญญา " />
                                                                    </div>

								                            </div>
							                            </td>
                                                    </tr>
                                                </table>

                                            </asp:View>

                                            <asp:View ID="Tab14" runat="server">
                                                    
                                            </asp:View>
                                            <asp:View ID="Tab15" runat="server">
                                                    
                                            </asp:View>
                                        </asp:MultiView>

                                    <asp:Menu ID="Menu2" Width="100%" runat="server" orientation="Horizontal" StaticEnableDefaultPopOutImage="False"
                                                OnMenuItemClick="Menu1_MenuItemClick" RenderingMode="Table"  >
                                                <Items>                      
                                                    <asp:MenuItem  Text=" หนังสือมอบอำนาจ " Value="9"></asp:MenuItem>
                            
                                                </Items>
                                     
                                    </asp:Menu>

                                        <%--<hr style="height:2px;border-width:0;color:gray;background-color:gray">--%>
                                        <asp:MultiView ID="MultiView2" runat="server" ActiveViewIndex="0">
                                           
                                            <asp:View ID="Tab10" runat="server">
                                                    <div class="input-group sm-col-2" >   
                                                        <span class="input-group-text"> หนังสือมอบอำนาจ </span>                                                                           
                                                    </div>                                                                     

                                                   <div class="row" style="padding-top: 0.2rem;">
                                                        
                                                               <div class="col-2">
                                                                    <div class="input-group sm-3">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">ประเภท</span>
                                                                        </div>
                                                                        <asp:label class="form-control" ID="lblContractBook" runat="server"></asp:label>
                                                                    </div>
                                                               </div>
                                                              <div class="col-2">
                                                                  <div class="input-group sm-2">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">วันที่ทำ</span>
                                                                        </div>                                                                        
                                                                        <asp:TextBox class="form-control" ID="txtDocDateBook"  runat="server" ></asp:TextBox>
                                                                  </div>
                                                              </div>                                                                                                     
                                 
                                                              <div class="col-2">
                                                                  <div class="input-group sm-2">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">วันที่ออกหนังสือ</span>
                                                                        </div>                                                                        
                                                                        <asp:TextBox class="form-control" ID="txtIssueDateBook"  runat="server" ></asp:TextBox>
                                                                  </div>
                                                              </div>   

                                                   </div>
                                                   <div class="row col-sm-3 span2" style="padding-top: 0.2rem;" runat="server">                                                                  
                                                        ข้อมูลมอบอำนาจ                                                                 
                                                   </div>
                                                   <div class="row" style="padding-top: 0.2rem;">
                                                              <div class="col-4">                                                                      
                                                                           <div class="input-group sm-4">
                                                                                  <div class="input-group-prepend">
                                                                                        <span class="input-group-text"> ชื่อผู้มอบอำนาจ </span>                                                                                
                                                                                  </div>  
                                                                                  <asp:TextBox class="form-control" ID="txtEmpfr"  runat="server" ></asp:TextBox>
                                                                           </div>                                                                     
                                                              </div>    
  
                                                   </div>

                                                   <div class="row" style="padding-top: 0.2rem;">
                                                              <div class="col-4">                                                                      
                                                                           <div class="input-group sm-4">
                                                                                  <div class="input-group-prepend">
                                                                                        <span class="input-group-text">* ชื่อผู้รับมอบอำนาจ</span>                                                                                
                                                                                  </div>  
                                                                                  <asp:TextBox class="form-control" ID="txtEmpto"  runat="server" ></asp:TextBox>
                                                                           </div>                                                                     
                                                              </div>
                                                              <div class="col-4">                                                                      
                                                                           <div class="input-group sm-4">
                                                                                  <div class="input-group-prepend">
                                                                                        <span class="input-group-text"> ชื่อผู้รับมอบอำนาจ</span>                                                                                
                                                                                  </div>  
                                                                                  <asp:TextBox class="form-control" ID="txtEmpto2"  runat="server" ></asp:TextBox>
                                                                           </div>                                                                     
                                                              </div>
                                                              <div class="col-4">                                                                      
                                                                           <div class="input-group sm-4">
                                                                                  <div class="input-group-prepend">
                                                                                        <span class="input-group-text"> ชื่อผู้รับมอบอำนาจ</span>                                                                                
                                                                                  </div>  
                                                                                  <asp:TextBox class="form-control" ID="txtEmpto3"  runat="server" ></asp:TextBox>
                                                                           </div>                                                                     
                                                              </div>
                                                   </div>

                                                   <div class="row" style="padding-top: 0.2rem;">
                                                              <div class="col-4">
                                                                  <div class="input-group sm-4">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">ชื่อพยาน 1</span>
                                                                        </div>                                                                        
                                                                        <asp:TextBox class="form-control" ID="txtWitness1"  runat="server" ></asp:TextBox>
                                                                       
                                                                  </div>
                                                              </div> 
                                                              <div class="col-4">
                                                                  <div class="input-group sm-4">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">ชื่อพยาน 2</span>
                                                                        </div>                                                                        
                                                                        <asp:TextBox class="form-control" ID="txtWitness2"  runat="server" ></asp:TextBox>
                                                                       
                                                                  </div>
                                                              </div>

                                                   </div>
                                                   <div class="row col-sm-3 span2" style="padding-top: 0.2rem;" runat="server">                                                                  
                                                        ข้อมูลที่อยู่                                                                 
                                                   </div>
                                                   <div class="row" style="padding-top: 0.2rem;">
                                                              <div class="col-3">
                                                                  <div class="input-group sm-3">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">ชื่อสาขา</span>
                                                                        </div>                                                                        
                                                                        <asp:TextBox class="form-control" ID="txtBrCodePowerBook"  runat="server" ></asp:TextBox>                                                                       
                                                                  </div>
                                                              </div>
                                                              <div class="col-5">
                                                                  <div class="input-group sm-5">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">ที่อยู่สาขา</span>
                                                                        </div>                                                                        
                                                                        <asp:TextBox class="form-control" ID="txtAddrPowerBook"  runat="server" ></asp:TextBox>                                                                       
                                                                  </div>
                                                              </div>
                                                   </div>

                                                   <div class="row" style="padding-top: 0.2rem;">
                                                              <div class="col-6">
                                                                  <div class="input-group sm-6">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">หน่วยงานที่ติดต่อ</span>
                                                                        </div>                                                                        
                                                                        <asp:TextBox class="form-control" ID="txtContactPowerBook"  runat="server" ></asp:TextBox>                                                                       
                                                                  </div>
                                                              </div>
                                                   </div>

                                                   <div class="row col-sm-3 span2" style="padding-top: 0.2rem;" runat="server">                                                                  
                                                        วัตถูประสงค์                                                              
                                                   </div>
                                                   <div class="row" style="padding-top: 0.2rem;">
                                                            <div class="col-3">
                                                                 <div class="input-group sm-2">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">วัตถูประสงค์ในการมอบอำนาจ</span>
                                                                        </div>                                  
                                                                  </div>
                                                            </div>

                                                   </div>
                                                   <div class="row" style="padding-top: 0.2rem;">
                                                            <div class="col-5">
                                                                  <div class="input-group sm-5">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">1</span>
                                                                        </div>                                                                        
                                                                        <asp:TextBox class="form-control" ID="txtObj1"  runat="server" ></asp:TextBox>
                                                                  </div>
                                                            </div>
                                                   </div>
                                                   <div class="row" style="padding-top: 0.2rem;">
                                                            <div class="col-5">
                                                                  <div class="input-group sm-5">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">2</span>
                                                                        </div>                                                                        
                                                                        <asp:TextBox class="form-control" ID="txtObj2"  runat="server" ></asp:TextBox>
                                                                  </div>
                                                            </div>
                                                   </div>
                                                   <div class="row" style="padding-top: 0.2rem;">
                                                            <div class="col-5">
                                                                  <div class="input-group sm-5">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">3</span>
                                                                        </div>                                                                        
                                                                        <asp:TextBox class="form-control" ID="txtObj3"  runat="server" ></asp:TextBox>
                                                                  </div>
                                                            </div>
                                                   </div>

                                                   <div class="row" style="padding-top: 0.2rem;">
                                                            <div class="col-3">
                                                                 <div class="input-group sm-2">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">อื่นๆ</span>
                                                                        </div>                                  
                                                                  </div>
                                                            </div>

                                                   </div>
                                                   <div class="row" style="padding-top: 0.2rem;">
                                                            <div class="col-5">
                                                                  <div class="input-group sm-5">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">1</span>
                                                                        </div>                                                                        
                                                                        <asp:TextBox class="form-control" ID="txtOth1"  runat="server" ></asp:TextBox>
                                                                  </div>
                                                            </div>
                                                   </div>
                                                   <div class="row" style="padding-top: 0.2rem;">
                                                            <div class="col-5">
                                                                  <div class="input-group sm-5">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">2</span>
                                                                        </div>                                                                        
                                                                        <asp:TextBox class="form-control" ID="txtOth2"  runat="server" ></asp:TextBox>
                                                                  </div>
                                                            </div>
                                                   </div>
                                                   <div class="row" style="padding-top: 0.2rem;">
                                                            <div class="col-5">
                                                                  <div class="input-group sm-5">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">3</span>
                                                                        </div>                                                                        
                                                                        <asp:TextBox class="form-control" ID="txtOth3"  runat="server" ></asp:TextBox>
                                                                  </div>
                                                            </div>
                                                   </div>

                                                   <div class="row" style="padding-top: 0.2rem;" runat="server">
                                                         <div class="col-md-4" runat="server">
                                                                                            <%--<INPUT id="File4" type="file" runat="server" NAME="oFile">--%>
                                                                                            <asp:FileUpload id="FileUpload1" runat="server"></asp:FileUpload>
                                                                                            <asp:button id="btnUploadPicPower1" type="submit" text="Upload" runat="server"  Visible="false"></asp:button>
                                                                                            <asp:Panel ID="Panel1" Visible="False" Runat="server">
                                                                                                <asp:Label id="Label1" Runat="server"></asp:Label>
                                                                                            </asp:Panel>
                                                                                        </div>
                                                         <div class="col-md-4" runat="server">
                                                                                           <%-- <INPUT id="File5" type="file" runat="server" NAME="oFile">--%>
                                                                                            <asp:FileUpload id="FileUpload2" runat="server"></asp:FileUpload>
                                                                                            <asp:button id="btnUploadPicPower2" type="submit" text="Upload" runat="server"  Visible="false"></asp:button>
                                                                                            <asp:Panel ID="Panel2" Visible="False" Runat="server">
                                                                                                <asp:Label id="Label2" Runat="server"></asp:Label>
                                                                                            </asp:Panel>
                                                        </div>       
                                                   </div>
                                                     <div class="row" runat="server">
                                                           <div  class="col-md-4" runat="server">
                                                                 <asp:Image ID="image9" runat="server" Width="150px" Height="100px"/>
                                                           </div>
                                                           <div  class="col-md-4" runat="server">
                                                                 <asp:Image ID="image10" runat="server" Width="150px" Height="100px"/>
                                                           </div>
                                                      </div>
                                                    <div class="row col-md-3" style="padding-top: 0.2rem;"  runat="server">
                                                         <asp:Button ID="btnAddPowerBook" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" บันทึกสัญญา " />
                                                         <%--<asp:Button ID="btnEditPowerBook" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" แก้สัญญา " />--%>
                                                    </div>
                                                
                                            </asp:View>      
                                            
                                            <asp:View ID="Tab21" runat="server">

                                            </asp:View>
                                        </asp:MultiView>

                                    <asp:Menu ID="Menu3" Width="100%" runat="server" orientation="Horizontal" StaticEnableDefaultPopOutImage="False"
                                                OnMenuItemClick="Menu1_MenuItemClick" RenderingMode="Table"  >
                                                <Items>                      
                                                    <asp:MenuItem  Text=" สัญญาพื้นที่เช่า Non Oil " Value="10"></asp:MenuItem>                                              
                                                </Items>
                                     
                                    </asp:Menu>
                                        <asp:MultiView ID="MultiView3" runat="server" ActiveViewIndex="0">
                                            <asp:View ID="Tab11" runat="server">
                                              <%--      <div class="input-group sm-col-2" >   
                                                        <span class="input-group-text"> สัญญาพื้นที่เช่า Non Oil </span>                                                                           
                                                    </div>  --%>
                                                    <div class="row" style="padding-top: 0.2rem;">
                                                               <div class="col-4">
                                                                    <div class="input-group sm-4">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">ประเภทสัญญา</span>
                                                                        </div>
                                                                        <asp:label class="form-control" ID="lblContractTypeNonOil" runat="server"></asp:label>
                                                                    </div>
                                                               </div>
                                            <%--                   <div class="col-3">
                                                                      <div class="input-group sm-3">
                                                                             <div class="input-group-prepend">
                                                                                   <span class="input-group-text">* ประเภทค่าใช้จ่าย</span>
                                                                             </div>
                                                                             <asp:DropDownList class="form-control" ID="cboPayTypeNonOil"  runat="server" ></asp:DropDownList>    
                                                                     </div>
                                                               </div>
                                                               <div class="col-2">
                                                                    <div class="input-group sm-2">
                                                                          <div class="input-group-prepend">
                                                                                 <span class="input-group-text">* จำนวนเงิน</span>
                                                                          </div>
                                                                          <asp:TextBox class="form-control" ID="txtAmountNonOil" runat="server" ></asp:TextBox>
                                                                   </div>
                                                             </div>  --%>                                                      
                                                    </div>
                                                   <div class="row col-sm-3 span2" style="padding-top: 0.2rem;" runat="server">                                                                  
                                                        ค่าใช้จ่าย                                                            
                                                   </div>
                                                    <div class="row" style="padding-top: 0.2rem;">
                                                               <div class="col-2">
                                                                    <div class="input-group sm-2">
                                                                          <div class="input-group-prepend">
                                                                                <span class="input-group-text">ค่าบริการ / ค่าเช่า</span>
                                                                          </div>
                                                                          <asp:TextBox class="form-control" ID="txtPayNonOilService" runat="server" style="text-align:right"></asp:TextBox>
                                                                   </div>
                                                             </div>     
                                                               <div class="col-2">
                                                                    <div class="input-group sm-2">
                                                                          <div class="input-group-prepend">
                                                                                <span class="input-group-text">เงินประกัน</span>
                                                                          </div>
                                                                          <asp:TextBox class="form-control" ID="txtPayNonOilInsurance" runat="server" style="text-align:right"></asp:TextBox>
                                                                   </div>
                                                             </div>     
                                                               <div class="col-2">
                                                                    <div class="input-group sm-2">
                                                                          <div class="input-group-prepend">
                                                                                <span class="input-group-text">ค่าประปา/หน่วย</span>
                                                                          </div>
                                                                          <asp:TextBox class="form-control" ID="txtPayNonOilWater" runat="server" style="text-align:right"></asp:TextBox>
                                                                   </div>
                                                             </div>     
                                                               <div class="col-2">
                                                                    <div class="input-group sm-2">
                                                                          <div class="input-group-prepend">
                                                                                <span class="input-group-text">ค่าไฟฟ้า/หน่วย</span>
                                                                          </div>
                                                                          <asp:TextBox class="form-control" ID="txtPqyNonOilElectrice" runat="server" style="text-align:right"></asp:TextBox>
                                                                   </div>
                                                             </div>    
                                                    </div>

                                                    <div class="row" style="padding-top: 0.2rem;">                                                        
                                                               <div class="col-2">
                                                                    <div class="input-group sm-2">
                                                                          <div class="input-group-prepend">
                                                                                <span class="input-group-text">ค่าส่วนกลาง</span>
                                                                          </div>
                                                                          <asp:TextBox class="form-control" ID="txtPayNonOilCenter" runat="server" style="text-align:right"></asp:TextBox>
                                                                   </div>
                                                             </div>     
                                                               <div class="col-2">
                                                                    <div class="input-group sm-2">
                                                                          <div class="input-group-prepend">
                                                                                 <span class="input-group-text">ค่าขยะ</span>
                                                                          </div>
                                                                          <asp:TextBox class="form-control" ID="txtPayNonOilGarbag" runat="server" style="text-align:right"></asp:TextBox>
                                                                   </div>
                                                             </div>
                                                               <div class="col-2">
                                                                    <div class="input-group sm-2">
                                                                          <div class="input-group-prepend">
                                                                                  <span class="input-group-text">ค่าไฟส่วางป้าย</span>
                                                                          </div>
                                                                          <asp:TextBox class="form-control" ID="txtPayNonOilLabel" runat="server" style="text-align:right"></asp:TextBox>
                                                                   </div>
                                                             </div>
                                                               <div class="col-2">
                                                                    <div class="input-group sm-2">
                                                                          <div class="input-group-prepend">
                                                                                  <span class="input-group-text">อื่นๆ</span>
                                                                          </div>
                                                                          <asp:TextBox class="form-control" ID="txtPayNonOilRemarkOther" runat="server" style="text-align:right"></asp:TextBox>
                                                                   </div>
                                                             </div>
                                                            <div class="col-md-3" runat="server">                                                
                                                                 <div class="input-group sm-3" runat="server" >
                                                                      <div class="input-group-prepend" runat="server">
                                                                           <span class="input-group-text" > หมายเหตุ </span>
                                                                      </div>
                                                                      <asp:TextBox class="form-control" ID="txtNonOilRemarkOther" runat="server" ></asp:TextBox>
                                                                 </div>
                                                            </div>
                                                    </div>


                                                   <div class="row col-sm-3 span2" style="padding-top: 0.2rem;" runat="server">                                                                  
                                                        เงือนไขการจ่าย                                                                 
                                                   </div>
                                                    <div class="row" style="padding-top: 0.2rem;">
                                                             <div class="col-4">
                                                                   <div class="input-group sm-4">
                                                                         <div class="input-group-prepend">
                                                                               <span class="input-group-text">* รอบการจ่าย</span>
                                                                         </div>
                                                                         <asp:RadioButton ID="rdoMonthNonOil" runat="server" groupname="paid" Checked="true"/>รายเดือน&nbsp;&nbsp;
                                                                         <asp:RadioButton ID="rdoYearNonOil" runat="server" groupname="paid"/>รายปี &nbsp;&nbsp;
                                                                         <asp:RadioButton ID="rdoOnceNonOil" runat="server" groupname="paid"/>จ่ายครั้งเดียว &nbsp;&nbsp;
                                                                         <asp:RadioButton ID="rdoFreeNonOil" runat="server" groupname="paid"/>ฟรีค่าเช่า
                                                                  </div>
                                                            </div>  
                                                               <div class="col-2">
                                                                    <div class="input-group sm-2">
                                                                          <div class="input-group-prepend">
                                                                                 <span class="input-group-text">* ความถี่การจ่าย</span>
                                                                          </div>
                                                                          <asp:TextBox class="form-control" ID="txtFrequencyNonOil" runat="server" ></asp:TextBox>
                                                                   </div>
                                                             </div>                                                          
                                                             <div class="col-2" runat="server">
                                                                <div class="input-group sm-2" runat="server">
                                                                    <div class="input-group-prepend" runat="server">
                                                                    <span class="input-group-text" runat="server"> DueDate </span>
                                                                    </div>
                                                                    <asp:TextBox class="form-control" ID="txtDueDateNonOil" style="background-color:white" runat="server" ></asp:TextBox>    

                                                                </div>
                                                            </div>

                                                    </div>
                                                    <div class="row" style="padding-top: 0.2rem;">
                                                          <div class="col-2">
                                                                 <div class="input-group sm-3">
                                                                       <div class="input-group-prepend">
                                                                            <span class="input-group-text">จากวันที่</span>
                                                                       </div>
                                                                            <asp:TextBox class="form-control" ID="txtBeginDateNonOil" style="background-color:white" runat="server"></asp:TextBox>    

                                                                 </div>
                                                          </div>
                                                                 <div class="col-2">
                                                                       <div class="input-group sm-3">
                                                                            <div class="input-group-prepend">
                                                                                <span class="input-group-text">ถึงวันที่</span>
                                                                            </div>
                                                                            <asp:TextBox class="form-control" ID="txtEndDateNonOil" style="background-color:white" runat="server" ></asp:TextBox>    

                                                                       </div>
                                                                </div>
                                                   </div>
                                                   <div  class="row" style="padding-top: 0.2rem;">
                                                               <div class="col-3">
                                                                    <div class="input-group sm-3">
                                                                          <div class="input-group-prepend">
                                                                               <span class="input-group-text">ขนาดพื้นที่</span>
                                                                          </div>
                                                                          <asp:TextBox class="form-control" ID="txtSizeNonOil" runat="server" ></asp:TextBox>
                                                                   </div>
                                                             </div>  
                                                             <div class="col-3">
                                                                    <div class="input-group sm-3">
                                                                          <div class="input-group-prepend">
                                                                               <span class="input-group-text">กว้างxยาว (ตรม.)</span>
                                                                          </div>
                                                                          <asp:TextBox class="form-control" ID="txtNonOilWide" runat="server" ></asp:TextBox>
                                                                   </div>
                                                             </div>
                                                               <div class="col-3">
                                                                    <div class="input-group sm-3">
                                                                          <div class="input-group-prepend">
                                                                                  <span class="input-group-text">หมายเลขห้อง</span>
                                                                          </div>
                                                                          <asp:TextBox class="form-control" ID="txtRoomNumberNonOil" runat="server" ></asp:TextBox>
                                                                   </div>
                                                             </div>  
                                                               <div class="col-3">
                                                                    <div class="input-group sm-3">
                                                                          <div class="input-group-prepend">
                                                                                  <span class="input-group-text">ประเภทธุรกิจ</span>
                                                                          </div>
                                                                          <asp:TextBox class="form-control" ID="txtBusinessTypeNonOil" runat="server" ></asp:TextBox>
                                                                   </div>
                                                             </div>  
                                                   </div>

                                                   <div class="row col-sm-3 span2" style="padding-top: 0.2rem;" runat="server">                                                                  
                                                        ข้อมูลคู่สัญญา                                                                  
                                                   </div>

                                                   <div class="row" style="padding-top: 0.2rem; " runat="server">
                                                        
                                                     <%--        <div class="col-md-3" runat="server">                                                
                                                                 <div class="input-group sm-3" runat="server">
                                                                      <div class="input-group-prepend" runat="server">
                                                                           <span class="input-group-text" runat="server">* รหัสผู้เช่า</span>
                                                                      </div>
                                                                      <asp:TextBox class="form-control" ID="txtCustCodeNonOil" runat="server"  ></asp:TextBox>
                                                                 </div>
                                                            </div>--%>
                                                            <div class="col-md-3" runat="server">                                                
                                                                 <div class="input-group sm-3" runat="server">
                                                                      <div class="input-group-prepend" runat="server">
                                                                           <span class="input-group-text" runat="server">* ชื่อ-นามสกุล</span>
                                                                      </div>
                                                                      <asp:TextBox class="form-control" ID="txtCustNameNonOil" runat="server" ></asp:TextBox>
                                                                 </div>
                                                            </div>
                                                            <div class="col-md-3" runat="server">                                                
                                                                 <div class="input-group sm-3" runat="server" >
                                                                      <div class="input-group-prepend" runat="server">
                                                                           <span class="input-group-text"  runat="server" > เลขที่่บัตรประจำตัวประชาชน</span>
                                                                      </div>
                                                                      <asp:TextBox class="form-control" ID="txtIDCardNonOil" runat="server" ></asp:TextBox>
                                                                 </div>
                                                            </div>

                                                            <div class="col-md-3" runat="server">                                                
                                                                 <div class="input-group sm-3" runat="server" >
                                                                      <div class="input-group-prepend" runat="server">
                                                                           <span class="input-group-text" > เลขที่ผู้เสียภาษี</span>
                                                                      </div>
                                                                      <asp:TextBox class="form-control" ID="txtTaxIDNonOil" runat="server" ></asp:TextBox>
                                                                 </div>
                                                            </div>


                                                   </div>

                                                   <div class="row col-sm-3 span2" style="padding-top: 0.2rem;" runat="server">                                                                  
                                                        ที่อยู่                                                                  
                                                   </div>

                                                   <div class="row" style="padding-top: 0.2rem; " runat="server">

                                                            <div class="col-md-2" runat="server">                                                
                                                                 <div class="input-group sm-3" runat="server" >
                                                                      <div class="input-group-prepend" runat="server">
                                                                           <span class="input-group-text" > เลขที่ </span>
                                                                      </div>
                                                                      <asp:TextBox class="form-control" ID="txtHomeIDNonOil" runat="server" ></asp:TextBox>
                                                                 </div>
                                                            </div>

                                                            <div class="col-md-2" runat="server">                                                
                                                                 <div class="input-group sm-3" runat="server" >
                                                                      <div class="input-group-prepend" runat="server">
                                                                           <span class="input-group-text" > ตำบล </span>
                                                                      </div>
                                                                      <asp:TextBox class="form-control" ID="txtSubDistrictNonOil" runat="server" ></asp:TextBox>
                                                                 </div>
                                                            </div>

                                                            <div class="col-md-2" runat="server">                                                
                                                                 <div class="input-group sm-3" runat="server" >
                                                                      <div class="input-group-prepend" runat="server">
                                                                           <span class="input-group-text" > อำเภอ </span>
                                                                      </div>
                                                                      <asp:TextBox class="form-control" ID="txtDistrictNonOil" runat="server" ></asp:TextBox>
                                                                 </div>
                                                            </div>

                                                            <div class="col-md-2" runat="server">                                                
                                                                 <div class="input-group sm-3" runat="server" >
                                                                      <div class="input-group-prepend" runat="server">
                                                                           <span class="input-group-text" > จังหวัด </span>
                                                                      </div>
                                                                      <asp:TextBox class="form-control" ID="txtProvinceNonOil" runat="server" ></asp:TextBox>
                                                                 </div>
                                                            </div>

                                                            <div class="col-md-2" runat="server">                                                
                                                                 <div class="input-group sm-3" runat="server" >
                                                                      <div class="input-group-prepend" runat="server">
                                                                           <span class="input-group-text" > ไปรษณีย์ </span>
                                                                      </div>
                                                                      <asp:TextBox class="form-control" ID="txtPostCodeNonOil" runat="server" ></asp:TextBox>
                                                                 </div>
                                                            </div>

                                                   </div>

                                                   <div class="row col-sm-3 span2" style="padding-top: 0.2rem;" runat="server">                                                                  
                                                        ติดต่อ                                                                  
                                                   </div>
                                                   <div class="row" style="padding-top: 0.2rem; " runat="server">

                                                            <div class="col-md-3" runat="server">                                                
                                                                 <div class="input-group sm-3" runat="server" >
                                                                      <div class="input-group-prepend" runat="server">
                                                                           <span class="input-group-text" > ผู้ติดต่อ. </span>
                                                                      </div>
                                                                      <asp:TextBox class="form-control" ID="txtContactNonOil" runat="server" ></asp:TextBox>
                                                                 </div>
                                                            </div>

                                                            <div class="col-md-2" runat="server">                                                
                                                                 <div class="input-group sm-3" runat="server" >
                                                                      <div class="input-group-prepend" runat="server">
                                                                           <span class="input-group-text" > Tel. </span>
                                                                      </div>
                                                                      <asp:TextBox class="form-control" ID="txtTelNonOil" runat="server" ></asp:TextBox>
                                                                 </div>
                                                            </div>

                                                            <div class="col-md-2" runat="server">                                                
                                                                 <div class="input-group sm-3" runat="server" >
                                                                      <div class="input-group-prepend" runat="server">
                                                                           <span class="input-group-text" > line. </span>
                                                                      </div>
                                                                      <asp:TextBox class="form-control" ID="txtLineNonOil" runat="server" ></asp:TextBox>
                                                                 </div>
                                                            </div>

                                                            <div class="col-md-2" runat="server">                                                
                                                                 <div class="input-group sm-3" runat="server" >
                                                                      <div class="input-group-prepend" runat="server">
                                                                           <span class="input-group-text" > E-Mail. </span>
                                                                      </div>
                                                                      <asp:TextBox class="form-control" ID="txtEmailNonOil" runat="server" ></asp:TextBox>
                                                                 </div>
                                                            </div>

                                                   </div>
                                                   <div  class="row" style="padding-top: 0.2rem; " runat="server">
                                                            <div class="col-md-5" runat="server">                                                
                                                                 <div class="input-group sm-5" runat="server" >
                                                                      <div class="input-group-prepend" runat="server">
                                                                           <span class="input-group-text" > อื่นๆ </span>
                                                                      </div>
                                                                      <asp:TextBox class="form-control" ID="txtNonOilRemark" runat="server" ></asp:TextBox>
                                                                 </div>
                                                            </div>
                                                   </div>

                                                   <div class="row" style="padding-top: 0.2rem;" runat="server">
                                                         <div class="col-md-4" runat="server">
                                                                                            <%--<INPUT id="File4" type="file" runat="server" NAME="oFile">--%>
                                                                                            <asp:FileUpload id="FileUpload3" runat="server"></asp:FileUpload>
                                                                                            <asp:button id="btnUploadPicNon1" type="submit" text="Upload" runat="server"  Visible="false"></asp:button>
                                                                                            <asp:Panel ID="Panel3" Visible="False" Runat="server">
                                                                                                <asp:Label id="Label3" Runat="server"></asp:Label>
                                                                                            </asp:Panel>
                                                                                        </div>
                                                         <div class="col-md-4" runat="server">
                                                                                           <%-- <INPUT id="File5" type="file" runat="server" NAME="oFile">--%>
                                                                                            <asp:FileUpload id="FileUpload4" runat="server"></asp:FileUpload>
                                                                                            <asp:button id="btnUploadPicNon2" type="submit" text="Upload" runat="server"  Visible="false"></asp:button>
                                                                                            <asp:Panel ID="Panel6" Visible="False" Runat="server">
                                                                                                <asp:Label id="Label7" Runat="server"></asp:Label>
                                                                                            </asp:Panel>
                                                        </div>       
                                                   </div>
                                                     <div class="row" runat="server">
                                                           <div  class="col-md-4" runat="server">
                                                                 <asp:Image ID="image11" runat="server" Width="150px" Height="100px"/>
                                                           </div>
                                                           <div  class="col-md-4" runat="server">
                                                                 <asp:Image ID="image12" runat="server" Width="150px" Height="100px"/>
                                                           </div>
                                                      </div>

                                                    <div class="row col-md-3" style="padding-top: 0.2rem;"  runat="server">
                                                         <asp:Button ID="btnAddNonOil" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" บันทึกสัญญา Non Oil " />
                                                         <%--<asp:Button ID="btnEditNonOil" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" แก้สัญญา Non Oil" />--%>
                                                    </div>

                                                   <div class="row col-sm-10 span2" style="padding-top: 0.2rem;" runat="server">
                                                         <div  class="row" style="padding-top: 1rem;">
                                                                 <div class="table-responsive" runat="server" >
                                                                        <asp:GridView ID="gvPayTypeNonOil" 
                                                                              class="table table-striped table-bordered"
                                                                              AllowSorting="True"
                                                                              AllowPaging="false"
                                                                              AutoGenerateColumns="false"
                                                                              EmptyDataText=" ไม่มีข้อมูล "           
                                                                              OnRowDataBound="OnRowDataBound_PayTypeNonOil"  
                                                                              OnSelectedIndexChanged="OnSelectedIndexChanged_PayNonOil"                                                                              
                                                                              runat="server" CssClass="table table-striped">
                                                                              <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-Size="Smaller" ForeColor="White" />
                                                                              <AlternatingRowStyle BackColor="#CCCCFF" />                                                                                                                                                                                                                                                                                                                                              
                                                                                 <columns>
                                                                                     <asp:BoundField DataField="ID" HeaderText="ID" />
                                                                                     <asp:BoundField DataField="ItemNo" HeaderText="ItemNo" />     
                                                                                     <asp:BoundField DataField="ContractTypeName" HeaderText="ContractTypeName" /> 
                                                                                     <asp:BoundField DataField="CustName" HeaderText="CustName" /> 
                                                                                     <asp:BoundField DataField="BeginDate" HeaderText="BeginDate" /> 
                                                                                     <asp:BoundField DataField="EndDate" HeaderText="EndDate" /> 
                                                                                     <asp:BoundField DataField="DueDate" HeaderText="DueDate" /> 
                                                                                     <asp:BoundField DataField="BusinessType" HeaderText="BusinessType" /> 
                                                                                     <asp:BoundField DataField="Addr" HeaderText="Addr" /> 
                                                                                     <asp:BoundField DataField="SubDistrict" HeaderText="SubDistrict" /> 
                                                                                     <asp:BoundField DataField="District" HeaderText="District" /> 
                                                                                     <asp:BoundField DataField="Province" HeaderText="Province" /> 
                                                                                     <asp:BoundField DataField="PostCode" HeaderText="PostCode" /> 
                                                                                     <asp:BoundField DataField="Contact" HeaderText="Contact" /> 
                                                                                     <asp:BoundField DataField="Tel" HeaderText="Tel" /> 
                                                                                     <asp:BoundField DataField="Line" HeaderText="Line" /> 
                                                                                     <asp:BoundField DataField="Email" HeaderText="Email" />     
                                                                                  </columns>
                                                                         </asp:GridView>
                                                                 </div>          
                                                          </div>  
                                                   </div>

                                            </asp:View>

                                            <asp:View ID="View1" runat="server">

                                            </asp:View>
                                        </asp:MultiView>

                                <asp:Menu ID="Menu4" Width="100%" runat="server" orientation="Horizontal" StaticEnableDefaultPopOutImage="False"
                                            OnMenuItemClick="Menu1_MenuItemClick" RenderingMode="Table"  >
                                            <Items>                                           
                                                <asp:MenuItem  Text=" สัญญาอืนๆ " Value="11"></asp:MenuItem>                                               
                                            </Items>
                                     
                                </asp:Menu>
                                        <asp:MultiView ID="MultiView4" runat="server" ActiveViewIndex="0">

                                            <asp:View ID="Tab12" runat="server">
                                                    <div class="input-group sm-col-2" >   
                                                        <span class="input-group-text"> สัญญาอื่นๆ  </span>                                                                           
                                                    </div> 
                                                   <div class="row col-sm-3 span2" style="padding-top: 0.2rem;" runat="server">                                                                  
                                                        ข้อมูลสัญญา                                                                 
                                                   </div>                                                
                                                    <div class="row" style="padding-top: 0.2rem;">
                                                               <div class="col-4">
                                                                    <div class="input-group sm-4">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">บริษัทที่ออกสัญญา</span>
                                                                        </div>
                                                                        <asp:label class="form-control" ID="lblCompany4" runat="server"></asp:label>
                                                                    </div>
                                                               </div>
                                                               <div class="col-4">
                                                                    <div class="input-group sm-4">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">ประเภทสัญญา</span>
                                                                        </div>
                                                                        <asp:label class="form-control" ID="lblContractType4" runat="server"></asp:label>
                                                                    </div>
                                                               </div>
                                                    </div>

                                                    <div class="row" style="padding-top: 0.2rem;">
                                                             <div class="col-2" runat="server">
                                                                <div class="input-group sm-2" runat="server">
                                                                    <div class="input-group-prepend" runat="server">
                                                                        <span class="input-group-text" runat="server"> วันที่ทำสัญญา</span>
                                                                    </div>
                                                                    <asp:TextBox class="form-control" ID="txtStartDate" style="background-color:white" runat="server" AutoPostBack="true" OnTextChanged="txtContractBeginDate_TextChanged"></asp:TextBox>   
                                                                </div>
                                                            </div>
                                                             <div class="col-5" runat="server">
                                                                <div class="input-group sm-5" runat="server">
                                                                    <div class="input-group-prepend" runat="server">
                                                                        <span class="input-group-text" runat="server"> วันที่เริ่มต้น </span>
                                                                    </div>
                                                                    <div class="col-3" runat="server">
                                                                        <asp:TextBox class="form-control" ID="txtBeginDate4" style="background-color:white" runat="server" AutoPostBack="true" OnTextChanged="txtBeginDate4_TextChanged"></asp:TextBox> 
                                                                    </div>
                                                                    <div class="input-group-prepend" runat="server">
                                                                        <span class="input-group-text" runat="server"> สิ้นสุดสัญญา </span>                                                                           
                                                                    </div>                                               
                                                                    <div class="col-3" runat="server">
                                                                        <asp:TextBox class="form-control" ID="txtEndDate4" style="background-color:white" runat="server" AutoPostBack="true" OnTextChanged="txtEndDate4_TextChanged"></asp:TextBox> 
                                                                    </div>  
                                                                    <div class="col-2" runat="server">
                                                                        <asp:CheckBox ID="chkEndDate4" runat="server" text="ไม่ระบุ" ></asp:CheckBox>
                                                                    </div>                                                                                                                                   
                                                                </div>
                                                            </div>

                                                             <div class="col-5">
                                                                <div class="input-group sm-5" >
                                                                    <div class="input-group-prepend">
                                                                        <span class="input-group-text" runat="server"> ระยะงานแล้วเสร็จ </span>
                                                                    </div>
                                                                    <div class="col-3" runat="server">
                                                                        <asp:TextBox class="form-control" ID="txtDueDate4" style="background-color:white" runat="server" ></asp:TextBox> 
                                                                    </div>
                                                                    <div class="col-2" runat="server">
                                                                        <asp:CheckBox ID="chkDueDate4" runat="server" text="ไม่ระบุ" ></asp:CheckBox>    
                                                                    </div>                                                                        
                                                                </div>
                                                            </div>
                                                             <div class="col-5">
                                                                <div class="input-group sm-5" >
                                                                    <div class="input-group-prepend">
                                                                        <span class="input-group-text" runat="server"> ชำระภายในวันที่ </span>
                                                                    </div>
                                                                    <div class="col-3" runat="server">
                                                                        <asp:TextBox class="form-control" ID="txtDueDateOth" style="background-color:white" runat="server" ></asp:TextBox> 
                                                                    </div>                                                                                                                               
                                                                </div>
                                                            </div>
                                                    </div>
                                                   <div class="row col-sm-3 span2" style="padding-top: 0.2rem;" runat="server">                                                                  
                                                        ข้อมูลสถานที่                                                                 
                                                   </div>
                                                   <div class="row" style="padding-top: 0.2rem;">
                                                            <div class="col-5" >
                                                                <div class="input-group sm-5" >
                                                                    <div class="input-group-prepend">
                                                                        <span class="input-group-text"> ที่อยู่ </span>
                                                                    </div>                                                                  
                                                                      <asp:TextBox class="form-control" ID="txtAddrOther" style="background-color:white" runat="server" AutoPostBack="true" OnTextChanged="txtBeginDate4_TextChanged"></asp:TextBox>                                                                   
                                                                </div>
                                                            </div>
                                                            <div class="col-2" >
                                                                <div class="input-group sm-2" >
                                                                    <div class="input-group-prepend">
                                                                        <span class="input-group-text"> หมายเลขห้อง </span>
                                                                    </div>                                                                  
                                                                      <asp:TextBox class="form-control" ID="txtRoomNumOth" style="background-color:white" runat="server" AutoPostBack="true" OnTextChanged="txtBeginDate4_TextChanged"></asp:TextBox>                                                                   
                                                                </div>
                                                            </div>
                                                   </div>
                                                   <div class="row col-sm-3 span2" style="padding-top: 0.2rem;" runat="server">                                                                  
                                                        เงือนไขการเช่า                                                                 
                                                   </div>
                                                   <div class="row" style="padding-top: 0.2rem;">                             
                                                               <div class="col-2">
                                                                    <div class="input-group sm-2">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">ค่าประกัน</span>
                                                                        </div>
                                                                        <asp:TextBox class="form-control" ID="txtPayInsuranceOth" runat="server" style="text-align:right"></asp:TextBox>
                                                                    </div>
                                                               </div>
                                                               <div class="col-2">
                                                                    <div class="input-group sm-2">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">ค่าเช่าล่วงหน้า</span>
                                                                        </div>
                                                                        <asp:TextBox class="form-control" ID="txtPayBeforeOth" runat="server" style="text-align:right"></asp:TextBox>
                                                                    </div>
                                                               </div>     
                                                               <div class="col-2">
                                                                    <div class="input-group sm-2">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">ค่าปรับ</span>
                                                                        </div>
                                                                        <asp:TextBox class="form-control" ID="txtPayFineOth" runat="server" style="text-align:right"></asp:TextBox>
                                                                    </div>
                                                               </div>    
                                                               <div class="col-2">
                                                                    <div class="input-group sm-2">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">ค่าน้ำ/หน่วย</span>
                                                                        </div>
                                                                        <asp:TextBox class="form-control" ID="txtPayWaterOth" runat="server" style="text-align:right"></asp:TextBox>
                                                                    </div>
                                                               </div>    
                                                               <div class="col-2">
                                                                    <div class="input-group sm-2">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">ค่าไฟ/หน่วย</span>
                                                                        </div>
                                                                        <asp:TextBox class="form-control" ID="txtPayElectricOth" runat="server" style="text-align:right"></asp:TextBox>
                                                                    </div>
                                                               </div>             
                                                               <div class="col-2">
                                                                    <div class="input-group sm-2">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">ค่าส่วนกลาง</span>
                                                                        </div>
                                                                        <asp:TextBox class="form-control" ID="txtPayCenterOth" runat="server" style="text-align:right"></asp:TextBox>
                                                                    </div>
                                                               </div>  
                                                               <div class="col-2">
                                                                    <div class="input-group sm-2">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">ค่าเช่าต่อเดือน</span>
                                                                        </div>
                                                                        <asp:TextBox class="form-control" ID="txtRentPerMonthOth" runat="server" style="text-align:right"></asp:TextBox>
                                                                    </div>
                                                               </div>                                                         
                                            
                                                   </div>
                                                   <div class="row col-sm-3 span2" style="padding-top: 0.2rem;" runat="server">                                                                  
                                                        ข้อมูลคู่สัญญา                                                                 
                                                   </div>
                                                    <div class="row" style="padding-top: 0.2rem;">
                                                               <div class="col-4">
                                                                    <div class="input-group sm-4">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">ชื่อคู่สัญญา</span>
                                                                        </div>
                                                                        <asp:TextBox class="form-control" ID="txtCustomerparty" runat="server"></asp:TextBox>
                                                                    </div>
                                                               </div>
                                                               <div class="col-4">
                                                                    <div class="input-group sm-4">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">ผู้มีอำนาจลงนาม</span>
                                                                        </div>
                                                                        <asp:TextBox class="form-control" ID="txtCustomerparty1" runat="server"></asp:TextBox>
                                                                    </div>
                                                               </div>
                                                               <div class="col-4">
                                                                    <div class="input-group sm-4">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">ผู้มีอำนาจลงนาม</span>
                                                                        </div>
                                                                        <asp:TextBox class="form-control" ID="txtCustomerparty2" runat="server"></asp:TextBox>
                                                                    </div>
                                                               </div>
                                                    </div>
                                                    <div class="row" style="padding-top: 0.2rem;">
                                                               <div class="col-6">
                                                                    <div class="input-group sm-6">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">ที่อยู่คู่สัญญา / ที่ตั้งสำนักงาน</span>
                                                                        </div>
                                                                        <asp:TextBox class="form-control" ID="txtAddr4" runat="server"></asp:TextBox>
                                                                    </div>
                                                               </div>

                                                    </div>
                                                    <div class="row" style="padding-top: 0.2rem;">
                                                               <div class="col-6">
                                                                    <div class="input-group sm-6">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">ชื่อ / เบอร์โทร ผู้ติดต่อ</span>
                                                                        </div>
                                                                        <asp:TextBox class="form-control" ID="txtContact4" runat="server"></asp:TextBox>
                                                                    </div>
                                                               </div>
                                                               <div class="col-6">
                                                                    <div class="input-group sm-6">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">สถานที่ / สาขา</span>
                                                                        </div>
                                                                        <asp:TextBox class="form-control" ID="txtBranchContract4" runat="server"></asp:TextBox>
                                                                    </div>
                                                               </div>
                                                    </div>                              
                                                   <div class="row col-sm-3 span2" style="padding-top: 0.2rem;" runat="server">                                                                  
                                                        ข้อมูลสัญญา                                                                 
                                                   </div>                                           
                                                    <div class="row" style="padding-top: 0.2rem;">
                                                               <div class="col-6">
                                                                    <div class="input-group sm-6">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">มูลค่างาน / ค่าจ้าง</span>
                                                                        </div>
                                                                        <asp:TextBox class="form-control" ID="txtAmount4" runat="server"></asp:TextBox>
                                                                    </div>
                                                               </div>
                                                               <div class="col-6">
                                                                    <div class="input-group sm-6">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">อายุสัญญา</span>
                                                                        </div>
                                                                        <asp:TextBox class="form-control" ID="txtPeriod" runat="server"></asp:TextBox>
                                                                    </div>
                                                               </div>
                                                    </div>
                                                                                
                                                    <div class="row" style="padding-top: 0.2rem;">
                                                               <div class="col-4">
                                                                    <div class="input-group sm-3">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">ค่าปรับ</span>
                                                                        </div>
                                                                        <asp:TextBox class="form-control" ID="txtFineAmnt4" runat="server"></asp:TextBox>
                                                                    </div>
                                                               </div>
                                                               <div class="col-4">
                                                                    <div class="input-group sm-3">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">ค่าประกันผลงาน</span>
                                                                        </div>
                                                                        <asp:TextBox class="form-control" ID="txtPerformanceIns" runat="server"></asp:TextBox>
                                                                    </div>
                                                               </div>
                                                    </div>
                                        
                                                   <div class="row" style="padding-top: 0.2rem;">
                                                               <div class="col-4">
                                                                    <div class="input-group sm-4">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">เงือนไขประกัน</span>
                                                                        </div>
                                                                        <asp:TextBox class="form-control" ID="txtConditionIns" runat="server"></asp:TextBox>
                                                                    </div>
                                                               </div>
                                                               <div class="col-4">
                                                                    <div class="input-group sm-4">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">เงือนไขชำระเงิน</span>
                                                                        </div>
                                                                        <asp:TextBox class="form-control" ID="txtConditionAmnt" runat="server"></asp:TextBox>
                                                                    </div>
                                                               </div>
                                                   </div>
                                                                                 
                                                    <div class="row" style="padding-top: 0.2rem;">
                                                               <div class="col-6">
                                                                    <div class="input-group sm-3">
                                                                        <div class="input-group-prepend">
                                                                             <span class="input-group-text">อื่นๆ</span>
                                                                        </div>
                                                                        <asp:TextBox class="form-control" ID="txtRemark4" runat="server"></asp:TextBox>
                                                                    </div>
                                                               </div>
                                                    </div>
                                                    <div class="row" style="padding-top: 0.2rem;">
                                                               <div class="col-6">
                                                                    <div class="input-group sm-3">                                                                 
                                                                        <asp:TextBox class="form-control" ID="txtRemark41" runat="server"></asp:TextBox>
                                                                    </div>
                                                               </div>
                                                    </div>
                                                    <div class="row" style="padding-top: 0.2rem;">
                                                               <div class="col-6">
                                                                    <div class="input-group sm-3">                                                                 
                                                                        <asp:TextBox class="form-control" ID="txtRemark42" runat="server"></asp:TextBox>
                                                                    </div>
                                                               </div>
                                                    </div>
                                                    <div class="row" style="padding-top: 0.2rem;">
                                                               <div class="col-6">
                                                                    <div class="input-group sm-3">                                                                 
                                                                        <asp:TextBox class="form-control" ID="txtRemark43" runat="server"></asp:TextBox>
                                                                    </div>
                                                               </div>
                                                    </div>

                                                    <!--************************************-->
                                                     <div class="row" style="padding-top: 0.2rem;" runat="server">
                                                           <div class="col-md-4" runat="server">
                                                                                            <%--<INPUT id="File4" type="file" runat="server" NAME="oFile">--%>
                                                                  <asp:FileUpload id="FileUpload5" runat="server"></asp:FileUpload>
                                                                  <asp:button id="btnUploadPicOth1" type="submit" text="Upload" runat="server" Visible="false"></asp:button>
                                                                  <asp:Panel ID="Panel7" Visible="False" Runat="server">
                                                                    <asp:Label id="Label8" Runat="server"></asp:Label>
                                                                  </asp:Panel>
                                                           </div>
                                                           <div class="col-md-4" runat="server">
                                                                                           <%-- <INPUT id="File5" type="file" runat="server" NAME="oFile">--%>
                                                                  <asp:FileUpload id="FileUpload6" runat="server"></asp:FileUpload>
                                                                  <asp:button id="btnUploadPicOth2" type="submit" text="Upload" runat="server"  Visible="false"></asp:button>
                                                                  <asp:Panel ID="Panel8" Visible="False" Runat="server">
                                                                    <asp:Label id="Label9" Runat="server"></asp:Label>
                                                                  </asp:Panel>
                                                           </div>       
                                                     </div>

                                                     <div class="row" runat="server">
                                                           <div  class="col-md-4" runat="server">
                                                                 <asp:Image ID="image7" runat="server" Width="150px" Height="100px"/>
                                                           </div>
                                                           <div  class="col-md-4" runat="server">
                                                                 <asp:Image ID="image8" runat="server" Width="150px" Height="100px"/>
                                                               <%--<asp:Image runat="server" ID="Image13" Visible="false" Height="60" Width="60" />--%>
                                                           </div>
                                                      </div>

                                                    <!--************************************-->                                                

                                                    <div class="row col-md-3" style="padding-top: 0.2rem;"  runat="server">
                                                         <asp:Button ID="btnAddContractOth" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" บันทึกสัญญา " />
                                                         <%--<asp:Button ID="btnEditContractOth" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" แก้สัญญา " />--%>
                                                    </div>
                                
                                            </asp:View>

                                            <asp:View ID="View2" runat="server">

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
        jQuery('[id$=txtStartDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            lang: 'th',// แสดงภาษาไทย
            yearOffset: 543,// ใช้ปี พ.ศ. บวก 543 เพิ่มเข้าไปในปี ค.ศ  
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>

    <script type="text/javascript">
        jQuery('[id$=txtBeginDate4]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            lang: 'th',// แสดงภาษาไทย
            yearOffset: 543,// ใช้ปี พ.ศ. บวก 543 เพิ่มเข้าไปในปี ค.ศ  
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>

    <script type="text/javascript">
        jQuery('[id$=txtEndDate4]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            lang: 'th',// แสดงภาษาไทย
            yearOffset: 543,// ใช้ปี พ.ศ. บวก 543 เพิ่มเข้าไปในปี ค.ศ  
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>

    <script type="text/javascript">
        jQuery('[id$=txtDueDate4]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            lang: 'th',// แสดงภาษาไทย
            yearOffset: 543,// ใช้ปี พ.ศ. บวก 543 เพิ่มเข้าไปในปี ค.ศ  
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>

    <script type="text/javascript">
        jQuery('[id$=txtDocDateBook]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            lang: 'th',// แสดงภาษาไทย
            yearOffset: 543,// ใช้ปี พ.ศ. บวก 543 เพิ่มเข้าไปในปี ค.ศ  
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>

    <script type="text/javascript">
        jQuery('[id$=txtIssueDateBook]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            lang: 'th',// แสดงภาษาไทย
            yearOffset: 543,// ใช้ปี พ.ศ. บวก 543 เพิ่มเข้าไปในปี ค.ศ  
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>
    <script type="text/javascript">
        jQuery('[id$=txtDocDateRentJoin]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            lang: 'th',// แสดงภาษาไทย
            yearOffset: 543,// ใช้ปี พ.ศ. บวก 543 เพิ่มเข้าไปในปี ค.ศ  
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>
    <script type="text/javascript">
        jQuery('[id$=txtBeginDateRentJoin]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            lang: 'th',// แสดงภาษาไทย
            yearOffset: 543,// ใช้ปี พ.ศ. บวก 543 เพิ่มเข้าไปในปี ค.ศ  
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>
    <script type="text/javascript">
        jQuery('[id$=txtEndDateRentJoin]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            lang: 'th',// แสดงภาษาไทย
            yearOffset: 543,// ใช้ปี พ.ศ. บวก 543 เพิ่มเข้าไปในปี ค.ศ  
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>
    <script type="text/javascript">
        jQuery('[id$=txtDueDateNonOil]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            lang: 'th',// แสดงภาษาไทย
            yearOffset: 543,// ใช้ปี พ.ศ. บวก 543 เพิ่มเข้าไปในปี ค.ศ  
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>
    <script type="text/javascript">
        jQuery('[id$=txtBeginDateNonOil]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            lang: 'th',// แสดงภาษาไทย
            yearOffset: 543,// ใช้ปี พ.ศ. บวก 543 เพิ่มเข้าไปในปี ค.ศ  
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>
    <script type="text/javascript">
        jQuery('[id$=txtEndDateNonOil]').datetimepicker({
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

<%--<script language="javascript" type="text/javascript">

    $(function () {
        // this will get the full URL at the address bar
        var url = window.location.href;

        // passes on every "a" tag 
        $("#cssmenu a").each(function () {
            // checks if its the same on the address bar
            if (url == (this.href)) {
                $(this).closest("li").addClass("active");
            }
        });
        $("#header a").each(function () {
            // checks if its the same on the address bar
            if (url == (this.href)) {
                $(this).closest("li").addClass("active");
            }
        });

    });
</script>--%>      

<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.24/themes/start/jquery-ui.css" />
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.24/jquery-ui.min.js"></script>
<script type="text/javascript">
    $(function () {
        $("#dialog").dialog({
            autoOpen: false,
            modal: true,
            height: 600,
            width: 600,
            title: "Zoomed Image"
        });
        $("[ItemNo*=gvAsset] img").click(function () {
            $('#dialog').html('');
            $('#dialog').append($(this).clone());
            $('#dialog').dialog('open');
        });
    });
</script>

  
</asp:Content>
