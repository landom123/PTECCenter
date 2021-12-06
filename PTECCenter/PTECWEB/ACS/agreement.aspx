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
        <a class="nav-link active" id="pills-info-tab" data-toggle="pill" href="#info" role="tab" aria-controls="pills-info" aria-selected="true">ข้อมูลสัญญา</a>
      </li>
      <li class="nav-item">
        <a class="nav-link" id="pills-assets-tab" data-toggle="pill" href="#assets" role="tab" aria-controls="pills-assets" aria-selected="false">ข้อมูลที่ดิน</a>
      </li>
      <li class="nav-item">
        <a class="nav-link" id="pills-payment-tab" data-toggle="pill" href="#payment" role="tab" aria-controls="pills-payment" aria-selected="false">ข้อมูลค่าเช่า/ผลตอบแทน</a>
      </li>
      <li class="nav-item">
        <a class="nav-link" id="pills-finance-tab" data-toggle="pill" href="#finance" role="tab" aria-controls="pills-finance" aria-selected="false">ข้อมูลการเงิน</a>
      </li>
    </ul>




  <div class="tab-content">  
    <div id="info" class="tab-pane fade show active" style="background-color:ghostwhite">  
      <div class="row">
        <div class="col-4">
            เลขที่โครงการ
            <div class="input-group sm-3">
                <asp:TextBox class="form-control" ID="txtProjectNo" runat="server"></asp:TextBox>    
            </div>
        </div>
        <div class="col-4">
            วันที่เริ่มโครงการ
            <div class="input-group sm-3">
                <asp:TextBox class="form-control" ID="txtProjectDate" runat="server"></asp:TextBox>    
            </div>
        </div>
        <div class="col-4">
            ผจก.โครงการ
            <div class="input-group sm-3">
                <asp:DropDownList ID="cboPrjManager" class="form-control" runat="server" AutoPostBack="true">
                </asp:DropDownList>
            </div>               
        </div>
     </div>
      <div class="row">
        <div class="col-4">
            รหัสสาขา
            <div class="input-group sm-3">
                <asp:TextBox class="form-control" ID="txtBranchCode" placeholder="BRxxx" runat="server"></asp:TextBox>    
            </div>
        </div>
        <div class="col-4">
            สถานะโครงการ
            <div class="input-group sm-3">
                <asp:DropDownList ID="cboPrjStatus" class="form-control" runat="server" AutoPostBack="true">
                </asp:DropDownList>  
            </div>
        </div>
     </div><p></p>
      <div class="row">
        <div class="col-4">
            เลขที่สัญญา
            <div class="input-group sm-3">
                <asp:TextBox class="form-control" ID="txtAgreeNo" runat="server"></asp:TextBox>    
            </div>
        </div>
        <div class="col-4">
            วันที่ทำสัญญา
            <div class="input-group sm-3">
                <asp:TextBox class="form-control" ID="txtAgreeDate" runat="server"></asp:TextBox>    
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
	                                    <asp:TemplateField>
		                                    <ItemTemplate>
			                                    <asp:textbox id="txtNo" runat="server" Text='<%#Eval("no")%>'></asp:textbox>
		                                    </ItemTemplate>
                                            <HeaderTemplate>ลำดับ</HeaderTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="รหัส">
		                                    <ItemTemplate>
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
                              <asp:DropDownList ID="cboClient" class="form-control" runat="server"></asp:DropDownList> 
                                  <asp:LinkButton runat="server" ID="btnAdddetail" Text="<i class='fas fa-plus-circle' style='color:green;font-size:20px'></i> " 
                                                        ValidationGroup="edt" OnClick="btnAddClient" ToolTip="เพิ่ม" />
                            </div>


                </div>

    </div>  
    <div id="assets" class="tab-pane fade" style="background-color:ghostwhite">  
      <p>บันทึกข้อมูลที่ดิน สิ่งปลูกสร้าง ที่ทำสัญญาเช่า หรือร่วมธุรกิจ</p>  
    </div>  
    <div id="payment" class="tab-pane fade" style="background-color:ghostwhite">  
      <p>บันทึกข้อมูลการจ่ายค่าเช่า การจ่ายผลตอลแทน ค่าใช้จ่ายอื่น ๆ</p>  
    </div>  
    <div id="finance" class="tab-pane fade" style="background-color:ghostwhite">  
      <p>บันทึกข้อมูลทางการเงิน สำหรับโอน หรือชำระค่าเช่า ผลตอบแทน</p>  
    </div>  
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
