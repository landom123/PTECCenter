<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="agreement.aspx.vb" Inherits="PTECCENTER.agreement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container">  
    <ol class="breadcrumb"style="background-color:deeppink;color:white">
        <li class="breadcrumb-item" >
                <i class="fa fa-file-text-o" aria-hidden="true"></i>ข้อมูลสัญญา
        </li>
    </ol>
                <div class="row">
                    <div class="col-6">
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-primary" runat="server" Text=" New " />
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text=" Save " />
                        <asp:Button ID="btnConfirm" class="btn btn-sm  btn-success" runat="server" Text=" Confirm " />
                        <asp:Button ID="btnCancel" class="btn btn-sm  btn-danger" runat="server" Text=" Cancel " />
                    </div>
                    <div class="col-6 text-right">
                        <asp:Button ID="btnChange" class="btn btn-sm  btn-primary" runat="server" Text=" Change " />
                        <asp:Button ID="btnApprove" class="btn btn-sm  btn-success" runat="server" Text=" Approve " />
                    </div>
                </div>
    <p></p>    
    <div class="row">
        <div class="col-4">
            <div class="input-group sm-3">
                <div class="input-group-prepend">
                <span class="input-group-text">เลขที่โครงการ</span>
                </div>
                <asp:TextBox class="form-control" ID="txtprojectnoFind" placeholder="PJxxxx" runat="server"></asp:TextBox>    
                &nbsp;<asp:Button ID="btnFind" class="btn btn-sm  btn-primary" runat="server" Text="Find" />
            </div>
        </div>
        <div class="col-4">
            <div class="input-group sm-3">
                <div class="input-group-prepend">
                <span class="input-group-text">เลขที่สัญญา</span>
                </div>
                <asp:TextBox class="form-control" ID="txtagreenoFind" placeholder="AGxxxx" runat="server"></asp:TextBox>    
                &nbsp;<asp:Button ID="Button1" class="btn btn-sm  btn-primary" runat="server" Text="Find" />
            </div>            
        </div>
    </div>
    <ul class="nav nav-pills mb-3" id="pills-tab" role="tablist">
      <li class="nav-item">
        <a class="nav-link active" id="pills-info-tab" data-toggle="pill" href="#info" role="tab" aria-controls="pills-info" aria-selected="<% If Session("pages") = "agree" Then  %>true<% else %>false<%End if %>">ข้อมูลสัญญา</a>
      </li>
      <li class="nav-item">
        <a class="nav-link" id="pills-assets-tab" data-toggle="pill" href="#assets" role="tab" aria-controls="pills-assets" aria-selected="<% If Session("pages") = "assets" Then  %>true<% else %>false<%End if %>">ข้อมูลที่ดิน</a>
      </li>
      <li class="nav-item">
        <a class="nav-link" id="pills-payment-tab" data-toggle="pill" href="#payment" role="tab" aria-controls="pills-payment" aria-selected="<% If Session("pages") = "payment" Then  %>true<% else %>false<%End if %>">ข้อมูลค่าเช่า/ผลตอบแทน</a>
      </li>
      <li class="nav-item">
        <a class="nav-link" id="pills-finance-tab" data-toggle="pill" href="#finance" role="tab" aria-controls="pills-finance" aria-selected="<% If Session("pages") = "finance" Then  %>true<% else %>false<%End if %>">ข้อมูลการเงิน</a>
      </li>
      <li class="nav-item">
        <a class="nav-link" id="pills-remark-tab" data-toggle="pill" href="#remark" role="tab" aria-controls="pills-remark" aria-selected="<% If Session("pages") = "remark" Then  %>true<% else %>false<%End if %>">ข้อมูลแนบท้ายสัญญา</a>
      </li>
    </ul>




  <div class="tab-content">  
    <div id="info" class="tab-pane fade <% If Session("pages") = "agree" Then  %>show active <% End if %>" style="background-color:ghostwhite">  
      <div class="row">
        <div class="col-md-4">
            เลขที่โครงการ
            <div class="input-group sm-3">
                <asp:TextBox class="form-control" ID="txtProjectNo" runat="server"></asp:TextBox>    
            </div>
        </div>
        <div class="col-md-4">
            วันที่เริ่มโครงการ
            <div class="input-group sm-3">
                <asp:TextBox class="form-control" ID="txtProjectDate" runat="server" AutoCompleteType="Disabled"></asp:TextBox>    
            </div>
        </div>
<%--        <div class="col-md-4">
            ผจก.โครงการ
            <div class="input-group sm-3">
                <asp:DropDownList ID="cboPrjManager" class="form-control" runat="server" >
                </asp:DropDownList>
            </div>               
        </div>--%>
     </div>
      <div class="row">
        <div class="col-md-4">
            รหัสสาขา
            <div class="input-group sm-3">
                <asp:TextBox class="form-control" ID="txtBranchCode" placeholder="BRxxx" runat="server"></asp:TextBox>    
            </div>
        </div>
        <div class="col-md-4">
            ประเภทสัญญา
            <div class="input-group sm-3">
                <asp:DropDownList ID="cboAgreeType" class="form-control" runat="server" >
                </asp:DropDownList>  
            </div>
        </div>
     </div><p></p>
      <div class="row">
        <div class="col-md-4">
            เลขที่สัญญา
            <div class="input-group sm-3">
                <asp:TextBox class="form-control" ID="txtAgreeNo" runat="server"></asp:TextBox>    
            </div>
        </div>
        <div class="col-md-4">
            วันที่ทำสัญญา
            <div class="input-group sm-3">
                <asp:TextBox class="form-control" ID="txtAgreeDate" runat="server" AutoCompleteType="Disabled"></asp:TextBox>    
            </div>
        </div>
        <div class="col-md-4">
            วันที่สัญญามีผลบังคับใช้
            <div class="input-group sm-3">
                <asp:TextBox class="form-control" ID="txtAgreeAcitveDate" runat="server" AutoCompleteType="Disabled"></asp:TextBox>    
            </div>
        </div>
     </div>

                <div class="card-body">
                              <div class="table-responsive">
                                <asp:GridView ID="gvClient"  
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    showfooter="false" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    ShowHeaderWhenEmpty="true"
                                    emptydatatext="No data available." 
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#CCCCFF" />

                                      <Columns>
	                                    <asp:TemplateField HeaderText="ลำดับ" >
		                                    <ItemTemplate>
			                                    <asp:textbox id="txtNo" style="width:50px" runat="server" Text='<%#Eval("no")%>' ></asp:textbox>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="รหัส">
		                                    <ItemTemplate >
			                                    <asp:Label id="lblClientNo" runat="server" Text='<%#Eval("clientno")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ชื่อ">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblClient" runat="server" Text='<%#Eval("client")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ที่อยู่">
		                                    <ItemTemplate>
			                                    <asp:Label id="lbladdress" runat="server" Text='<%#Eval("clientaddress")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>	         
	                                    <asp:TemplateField HeaderText="">
		                                    <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="btnDel" Text="<i class='fas fa-minus-circle' style='color:red;font-size:20px'></i> " 
                                                        ValidationGroup="edt" OnClick="BindData"  ToolTip="ลบ"/>
                                                <asp:LinkButton runat="server" ID="btnClientInfo" Text="<i class='fas fa-id-card' style='color:blue;font-size:20px'></i> " 
                                                        ValidationGroup="edt" OnClick="BindData"  ToolTip="รายละเอียด"/>			                                    
		                                    </ItemTemplate>
	                                    </asp:TemplateField>	                                             
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div align="center">No records found.</div>
                                    </EmptyDataTemplate>
                              </asp:GridView>
                          <div class="row">
                            <div class="col-md-8">
                                <div class="input-group sm-">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">เลือกคู่สัญญา</span>
                                    </div>
                                    <asp:DropDownList ID="cboClient" class="form-control" runat="server"></asp:DropDownList> 
                                </div>                              
                            </div>
                              <div class="col-md-4">
                                <asp:LinkButton runat="server" ID="btnAdddetail" Text="<i class='fas fa-plus-circle' style='color:green;font-size:20px'></i> " 
                                ValidationGroup="edt" OnClick="btnAddClient" ToolTip="เพิ่ม" />
                              </div>
                          </div>
                     </div>
                </div>

    </div>  
      <%----=====end tab agree info=======--%>

    <div id="assets" class="tab-pane fade <% If Session("pages") = "assets" Then  %>show active <% End if %>" style="background-color:white">  
      <p>บันทึกข้อมูลที่ดิน สิ่งปลูกสร้าง ที่ทำสัญญาเช่า หรือร่วมธุรกิจ</p>  

                   <div class="table-responsive">
                       
                        <table class="table table-striped" id="dataTable">
                            <thead class="table-info">
                                <tr>
                                    <th>รายการที่ดิน</th>
                                </tr>
                            </thead>
                            <tbody>
                                <% For i = 0 To mainAssets.Rows.Count - 1 %>
                                <tr>
                                    <td>
                                        <%-- info area--%>
                                          <div class="row">
                                            <div class="col-md-4">
                                                ID
                                                <div class="input-group sm-3">
                                                    <asp:Label class="form-control" ID="lblAsssetsID" runat="server"></asp:Label>    
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                ประเภทเอกสาร
                                                <div class="input-group sm-3">
                                                    <asp:Label class="form-control" ID="lblAssetType" runat="server"></asp:Label>    
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                เลขเอกสาร
                                                <div class="input-group sm-3">
                                                    <asp:Label class="form-control" ID="lblAssetDocno" runat="server"></asp:Label>    
                                                </div>
                                            </div>
                                         </div>
                                          <div class="row">
                                            <div class="col-md-4">
                                                เลขที่ดิน
                                                <div class="input-group sm-3">
                                                    <asp:Label class="form-control" ID="lblLandno" runat="server"></asp:Label>    
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                เลขหน้าสำรวจ
                                                <div class="input-group sm-3">
                                                    <asp:Label class="form-control" ID="lblSurveyNo" runat="server"></asp:Label>  
                                                </div>
                                            </div>

                                         </div>
                                          <div class="row">
                                            <div class="col-md-4">
                                                ตำบล
                                                <div class="input-group sm-3">
                                                    <asp:Label class="form-control" ID="lblSubdistrict" runat="server"></asp:Label>    
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                อำเภอ
                                                <div class="input-group sm-3">
                                                    <asp:Label class="form-control" ID="lblDistrict" runat="server"></asp:Label>  
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                จังหวัด
                                                <div class="input-group sm-3">
                                                    <asp:Label class="form-control" ID="lblProvince" runat="server"></asp:Label>  
                                                </div>
                                            </div>

                                         </div>
                                          <div class="row">
                                            <div class="col-md-4">
                                                ทั้งแปลง/แบ่งเช่า
                                                <div class="input-group sm-3">
                                                    <asp:Label class="form-control" ID="lblCondition" runat="server"></asp:Label>    
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                ไร่
                                                <div class="input-group sm-3">
                                                    <asp:Label class="form-control" ID="lblRai" runat="server"></asp:Label>  
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                งาน
                                                <div class="input-group sm-3">
                                                    <asp:Label class="form-control" ID="lblNgan" runat="server"></asp:Label>  
                                                </div>
                                            </div>

                                         </div>
                                          <div class="row">
                                            <div class="col-md-4">
                                                ตารางวา
                                                <div class="input-group sm-3">
                                                    <asp:Label class="form-control" ID="lblWa" runat="server"></asp:Label>    
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                พิกัด GPS
                                                <div class="input-group sm-3">
                                                    <asp:Label class="form-control" ID="lblGps" runat="server"></asp:Label>  
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                    <p>&nbsp</p>         
                                                    <asp:LinkButton runat="server" ID="btnDelAssets" Text="<i class='fas fa-minus-circle' style='color:red;font-size:20px'></i> " 
                                                    ValidationGroup="edt" OnClick="DelAssets" ToolTip="ลบ" />                           
                                            </div>
                                         </div>
                                    </td>
                                </tr>
                                <% Next i %>
                                <tr>
                                    <td>
                                        <%-- input area --%>
                                        <div class="row">
                                            <div class="col-md-4">
                                                ประเภทที่ดิน
                                                <div class="input-group sm-3">
                                                    <asp:DropDownList class="form-control" ID="cboAssetType" runat="server"></asp:DropDownList>    
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                เลขที่
                                                <div class="input-group sm-3">
                                                    <asp:Textbox class="form-control" ID="txtAssetDocNo" runat="server"></asp:Textbox>    
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                เลขที่ที่ดิน
                                                <div class="input-group sm-3">
                                                    <asp:Textbox class="form-control" ID="txtLandno" runat="server"></asp:Textbox>    
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                เลขที่หน้าสำรวจ
                                                <div class="input-group sm-3">
                                                    <asp:Textbox class="form-control" ID="txtSurveyNo" runat="server"></asp:Textbox>  
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                ตำบล
                                                <div class="input-group sm-3">
                                                    <asp:Textbox class="form-control" ID="txtSubDistrict" runat="server"></asp:Textbox>    
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                อำเภอ
                                                <div class="input-group sm-3">
                                                    <asp:Textbox class="form-control" ID="txtDistrict" runat="server"></asp:Textbox>  
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                จังหวัด
                                                <div class="input-group sm-3">
                                                    <asp:Textbox class="form-control" ID="txtProvince" runat="server"></asp:Textbox>  
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                ทั้งแปลง/แบ่งเช่า
                                                <div class="input-group sm-3">
                                                    <asp:Textbox class="form-control" ID="txtCondition" runat="server"></asp:Textbox>    
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                ไร่
                                                <div class="input-group sm-3">
                                                    <asp:Textbox class="form-control" ID="txtRai" runat="server"></asp:Textbox>  
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                งาน
                                                <div class="input-group sm-3">
                                                    <asp:Textbox class="form-control" ID="txtNgan" runat="server"></asp:Textbox>  
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                ตารางวา
                                                <div class="input-group sm-3">
                                                    <asp:Textbox class="form-control" ID="txtWa" runat="server"></asp:Textbox>    
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                พิกัด GPS
                                                <div class="input-group sm-3">
                                                    <asp:Textbox class="form-control" ID="txtGPS" runat="server"></asp:Textbox>  
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                    <p>&nbsp</p>
                                                    <asp:LinkButton runat="server" ID="btnAddAssets" Text="<i class='fas fa-plus-circle' style='color:green;font-size:20px'></i> " 
                                                    ValidationGroup="edt" OnClick="AddNewAssets" ToolTip="เพิ่ม" />                                      
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                </tr>
                            </tbody>
                        </table>
                        <%--   end table show ที่ดิน--%>
                    </div>

      </div>
      <%----=====end tab ที่ดิน=======--%>

    <div id="payment" class="tab-pane fade <% If Session("pages") = "payment" Then  %>show active <% End if %>" style="background-color:ghostwhite">  
      <p>บันทึกข้อมูลการจ่ายค่าเช่า การจ่ายผลตอลแทน ค่าใช้จ่ายอื่น ๆ</p>  
        <div class="card-body">
            ค่าใช้จ่ายแบบครั้งเดียว
                        <div class="table-responsive">
                        <asp:GridView ID="gvOneTime"  
                            class="table table-striped table-bordered" 
                            AllowSorting="True" 
                            showfooter="false" 
                            allowpaging="false"
                            AutoGenerateColumns="false" 
                            ShowHeaderWhenEmpty="true"
                            emptydatatext="No data available." 
                            runat="server" CssClass="table table-striped">
                            <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#CCCCFF" />

                                <Columns>
	                            <asp:TemplateField HeaderText="ID" >
		                            <ItemTemplate>
			                            <asp:Label id="lblOnetimeid" style="width:50px" runat="server" Text='<%#Eval("onetimeid")%>' ></asp:Label>
		                            </ItemTemplate>
	                            </asp:TemplateField>
	                            <asp:TemplateField HeaderText="ค่าใช้จ่าย">
		                            <ItemTemplate >
			                            <asp:Label id="lblOneTimepaymenttype" runat="server" Text='<%#Eval("paymenttype")%>'></asp:Label>
		                            </ItemTemplate>
	                            </asp:TemplateField>
	                            <asp:TemplateField HeaderText="กำหนดชำระ">
		                            <ItemTemplate>
			                            <asp:Label id="lblOneTimeduedate" runat="server" Text='<%#Eval("duedate")%>'></asp:Label>
		                            </ItemTemplate>
	                            </asp:TemplateField>
	                            <asp:TemplateField HeaderText="จำนวนเงิน">
		                            <ItemTemplate>
			                            <asp:Label id="lblOneTimeamount" runat="server" Text='<%#Eval("amount")%>'></asp:Label>
		                            </ItemTemplate>
	                            </asp:TemplateField>	  
	                            <asp:TemplateField HeaderText="บริษัทรับผิดชอบ">
		                            <ItemTemplate>
			                            <asp:Label id="lblWhoPaid" runat="server" Text='<%#Eval("whopaid")%>'></asp:Label>
		                            </ItemTemplate>
	                            </asp:TemplateField>	
	                            <asp:TemplateField HeaderText="หมายเหตุ">
		                            <ItemTemplate>
			                            <asp:Label id="lblremark" runat="server" Text='<%#Eval("remark")%>'></asp:Label>
		                            </ItemTemplate>
	                            </asp:TemplateField>	                                    
	                            <asp:TemplateField HeaderText="">
		                            <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="btnDelOneTime" Text="<i class='fas fa-minus-circle' style='color:red;font-size:20px'></i> " 
                                                ValidationGroup="edt" OnClick="BindData"  ToolTip="ลบ"/>	 
		                            </ItemTemplate>
	                            </asp:TemplateField>	                                             
                            </Columns>
                            <EmptyDataTemplate>
                                <div align="center">No records found.</div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <div class="row">
                            <div class="col">
                                ค่าใช้จ่าย
                                <div class="input-group sm-3">
                                    <asp:DropDownList ID="cboOneTimePaymentType" class="form-control" runat="server" >
                                    </asp:DropDownList> 
                                </div>
                            </div>
                            <div class="col">
                                กำหนดชำระ
                                <div class="input-group sm-3">
                                    <asp:Textbox class="form-control" ID="txtOneTimeDueDate" runat="server" AutoCompleteType="Disabled"></asp:Textbox>    
                                </div>
                            </div>
                            <div class="col">
                                จำนวนเงิน
                                <div class="input-group sm-3">
                                    <asp:Textbox class="form-control" ID="txtOneTimeAmount" runat="server"></asp:Textbox>    
                                </div>
                            </div>
                            <div class="col">
                                บริษัทรับผิดชอบค่าใช้จ่าย
                                <div class="input-group sm-3">
                                    <asp:checkbox class="form-control" ID="chkWhopaid" runat="server"></asp:checkbox>    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-8">
                                หมายเหตุ
                                <div class="input-group sm-3">
                                    <asp:Textbox class="form-control" ID="txtremark" runat="server"></asp:Textbox>    
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <br />
                                    <asp:LinkButton runat="server" ID="btnAddOneTime" Text="<i class='fas fa-plus-circle' style='color:green;font-size:20px'></i> " 
                                            ValidationGroup="edt" OnClick="AddOneTime"  ToolTip="เพิ่ม"/>		                                
                            </div>
                        </div>
                </div>
            <hr style="height: 2px; border-width: 0; color: gray; background-color: gray" />
            <%-- end table one time --%>
            ค่าตอบแทน / ค่าเช่า แบบชำระเป็นงวด เท่ากันทุกงวด
                <div class="table-responsive">
                        <asp:GridView ID="gvFix"  
                            class="table table-striped table-bordered" 
                            AllowSorting="True" 
                            showfooter="false" 
                            allowpaging="false"
                            AutoGenerateColumns="false" 
                            ShowHeaderWhenEmpty="true"
                            emptydatatext="No data available." 
                            runat="server" CssClass="table table-striped">
                            <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#CCCCFF" />

                            <Columns>
	                            <asp:TemplateField HeaderText="ID" >
		                            <ItemTemplate>
			                            <asp:Label id="lblFixid" style="width:50px" runat="server" Text='<%#Eval("fixid")%>' ></asp:Label>
		                            </ItemTemplate>
	                            </asp:TemplateField>
	                            <asp:TemplateField HeaderText="ค่าใช้จ่าย">
		                            <ItemTemplate >
			                            <asp:Label id="lblFixpaymenttype" runat="server" Text='<%#Eval("paymenttype")%>'></asp:Label>
		                            </ItemTemplate>
	                            </asp:TemplateField>
	                            <asp:TemplateField HeaderText="รอบการจ่าย">
		                            <ItemTemplate >
			                            <asp:Label id="lblFixRecurring" runat="server" Text='<%#Eval("recurring")%>'></asp:Label>
		                            </ItemTemplate>
	                            </asp:TemplateField>
	                            <asp:TemplateField HeaderText="ความถี่">
		                            <ItemTemplate >
			                            <asp:Label id="lblFixFrequency" runat="server" Text='<%#Eval("frequency")%>'></asp:Label>
		                            </ItemTemplate>
	                            </asp:TemplateField>
	                            <asp:TemplateField HeaderText="กำหนดชำระ เริ่มต้น">
		                            <ItemTemplate>
			                            <asp:Label id="lblFixBegindate" runat="server" Text='<%#Eval("begindate")%>'></asp:Label>
		                            </ItemTemplate>
	                            </asp:TemplateField>
	                            <asp:TemplateField HeaderText="สิ้นสุด">
		                            <ItemTemplate>
			                            <asp:Label id="lblFixEnddate" runat="server" Text='<%#Eval("enddate")%>'></asp:Label>
		                            </ItemTemplate>
	                            </asp:TemplateField>
	                            <asp:TemplateField HeaderText="จำนวนเงิน">
		                            <ItemTemplate>
			                            <asp:Label id="lblFixamount" runat="server" Text='<%#Eval("amount")%>'></asp:Label>
		                            </ItemTemplate>
	                            </asp:TemplateField>	         
	                            <asp:TemplateField HeaderText="">
		                            <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="btnDelFix" Text="<i class='fas fa-minus-circle' style='color:red;font-size:20px'></i> " 
                                                ValidationGroup="edt" OnClick="BindData"  ToolTip="ลบ"/>	 
		                            </ItemTemplate>
	                            </asp:TemplateField>	                                             
                            </Columns>
                            <EmptyDataTemplate>
                                <div align="center">No records found.</div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <div class="row">
                            <div class="col">
                                ค่าใช้จ่าย
                                <div class="input-group sm-3">
                                    <asp:DropDownList ID="cboFixPaymentType" class="form-control" runat="server" >
                                    </asp:DropDownList> 
                                </div>
                            </div>
                            <div class="col">
                                รอบการจ่าย
                                <div class="input-group sm-3">
                                    <asp:DropDownList ID="cboFixRecurring" class="form-control" runat="server" >
                                    </asp:DropDownList> 
                                </div>
                            </div>
                            <div class="col">
                                ความถี่
                                <div class="input-group sm-3">
                                    <asp:Textbox class="form-control" ID="txtFrequency" runat="server"></asp:Textbox>    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                มีผลเริ่มต้น
                                <div class="input-group sm-3">
                                    <asp:Textbox class="form-control" ID="txtFixBeginDate" runat="server" AutoCompleteType="Disabled"></asp:Textbox>    
                                </div>
                            </div>
                            <div class="col">
                                มีผลสิ้นสุด
                                <div class="input-group sm-3">
                                    <asp:Textbox class="form-control" ID="txtFixEndDate" runat="server" AutoCompleteType="Disabled"></asp:Textbox>    
                                </div>
                            </div>
                            <div class="col">
                                นัดงวดแรก
                                <div class="input-group sm-3">
                                    <asp:Textbox class="form-control" ID="txtFixDueDate" runat="server" AutoCompleteType="Disabled"></asp:Textbox>    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-4">
                                จำนวนเงิน
                                <div class="input-group sm-3">
                                    <asp:Textbox class="form-control" ID="txtFixAmount" runat="server"></asp:Textbox>    
                                    &nbsp;&nbsp;<asp:LinkButton runat="server" ID="LinkButton1" Text="<i class='fas fa-plus-circle' style='color:green;font-size:20px'></i> " 
                                            ValidationGroup="edt" OnClick="AddFix"  ToolTip="เพิ่ม"/>	
                                </div>

                            </div>

                        </div>
                </div>
            <hr style="height: 2px; border-width: 0; color: gray; background-color: gray" />
            <%-- end table recurring fix cost --%>
            ค่าตอบแทน / ค่าเช่า แบบชำระเป็นงวด ผลตอบแทนตามยอดขาย
                <div class="table-responsive">
                        <asp:GridView ID="gvFlexible"  
                            class="table table-striped table-bordered" 
                            AllowSorting="True" 
                            showfooter="false" 
                            allowpaging="false"
                            AutoGenerateColumns="false" 
                            ShowHeaderWhenEmpty="true"
                            emptydatatext="No data available." 
                            runat="server" CssClass="table table-striped">
                            <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#CCCCFF" />

                                <Columns>
	                            <asp:TemplateField HeaderText="ลำดับ" >
		                            <ItemTemplate>
			                            <asp:textbox id="txtNo" style="width:50px" runat="server" Text='<%#Eval("no")%>' ></asp:textbox>
		                            </ItemTemplate>
	                            </asp:TemplateField>
	                            <asp:TemplateField HeaderText="รหัส">
		                            <ItemTemplate >
			                            <asp:Label id="lblClientNo" runat="server" Text='<%#Eval("clientno")%>'></asp:Label>
		                            </ItemTemplate>
	                            </asp:TemplateField>
	                            <asp:TemplateField HeaderText="ชื่อ">
		                            <ItemTemplate>
			                            <asp:Label id="lblClient" runat="server" Text='<%#Eval("client")%>'></asp:Label>
		                            </ItemTemplate>
	                            </asp:TemplateField>
	                            <asp:TemplateField HeaderText="ที่อยู่">
		                            <ItemTemplate>
			                            <asp:Label id="lbladdress" runat="server" Text='<%#Eval("clientaddress")%>'></asp:Label>
		                            </ItemTemplate>
	                            </asp:TemplateField>	         
	                            <asp:TemplateField HeaderText="">
		                            <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="btnDel" Text="<i class='fas fa-minus-circle' style='color:red;font-size:20px'></i> " 
                                                ValidationGroup="edt" OnClick="BindData"  ToolTip="ลบ"/>
                                        <asp:LinkButton runat="server" ID="btnClientInfo" Text="<i class='fas fa-id-card' style='color:blue;font-size:20px'></i> " 
                                                ValidationGroup="edt" OnClick="BindData"  ToolTip="รายละเอียด"/>			                                    
		                            </ItemTemplate>
	                            </asp:TemplateField>	                                             
                            </Columns>
                            <EmptyDataTemplate>
                                <div align="center">No records found.</div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                </div>
            <%-- end table recurring flexible cost --%>
        </div>

    </div>  
      <%----=====end tab เงื่อนไขผลตอบแทน=======--%>
    <div id="finance" class="tab-pane fade <% If Session("pages") = "finance" Then  %>show active <% End if %>" style="background-color:ghostwhite">  
      <p>บันทึกข้อมูลทางการเงิน สำหรับโอน หรือชำระค่าเช่า ผลตอบแทน</p>  
    </div>  
      <%----=====end tab ข้อมูลการจ่ายเงิน=======--%>
    <div id="remark" class="tab-pane fade <% If Session("pages") = "remark" Then  %>show active <% End if %>" style="background-color:ghostwhite">  
      <p>บันทึกข้อมูลข้อความแนบท้ายสัญญา</p>  
    </div>  
      <%----=====end tab หมายเหตุแนบท้ายสัญญา=======--%>
  </div>  

</div>  
    <!-- /#wrapper -->
  <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>

    <script type="text/javascript">
        jQuery('[id$=txtProjectDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format:'d/m/Y'
        });
    </script>

    <script type="text/javascript">
        jQuery('[id$=txtAgreeDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>
    <script type="text/javascript">
        jQuery('[id$=txtOneTimeDueDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>
     <script type="text/javascript">
         jQuery('[id$=txtFixBeginDate]').datetimepicker({
             startDate: '+1971/05/01',//or 1986/12/08
             timepicker: false,
             scrollInput: false,
             format: 'd/m/Y'
         });
     </script>
    <script type="text/javascript">
        jQuery('[id$=txtFixEndDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>    
    <script type="text/javascript">
        jQuery('[id$=txtFixDueDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>   
    <script type="text/javascript">
        jQuery('[id$=txtAgreeAcitveDate]').datetimepicker({
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
</asp:Content>
