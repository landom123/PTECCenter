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
                                                        <span class="input-group-text">สัญญาออกโดบ</span>
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
                          </div><!-- end card-body-->


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
<<<<<<<<< Temporary merge branch 1
=========

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
>>>>>>>>> Temporary merge branch 2

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
                                                                                                <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:white;' runat="server">จำนวนเงิน((บาท))</asp:TableHeaderCell>
                                                          
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
                                                                                                (1).  	ค่าตอบแทนร่วมธุรกิจกำหนดจ่ายปีละ 1 ครั้งโดยที่ปลอดค่าร่วมธุรกิจและค่าเช่า.......   (....... ถึง 30 ...... )	<br />
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
                                        </asp:MultiView>


                            </div>
  

              
<!---->
                                 
                                  <%--<hr style="height:2px;border-width:0;color:gray;background-color:lavenderblush">--%>



                        <!--End Area Input-->
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
