<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="agreement.aspx.vb" Inherits="PTECCENTER.agreement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container">  
    <ol class="breadcrumb"style="background-color:deeppink;color:white">
        <li class="breadcrumb-item" >
                <i class="fa fa-tasks" aria-hidden="true"></i>ข้อมูลสัญญา
        </li>
    </ol>
                <div class="row">
                    <div class="col-12">
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-primary" runat="server" Text=" New " />
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text=" Save " />
                        <asp:Button ID="btnConfirm" class="btn btn-sm  btn-success" runat="server" Text=" Confirm " />
                        <asp:Button ID="btnCancel" class="btn btn-sm  btn-danger" runat="server" Text=" Cancel " />
                    </div>
                </div>
    <p></p>    
    <div class="row">
        <div class="col-4">
            <div class="input-group sm-3">
                <div class="input-group-prepend">
                <span class="input-group-text">เลขที่โครงการ</span>
                </div>
                <asp:TextBox class="form-control" ID="txtprojectnoFind" runat="server"></asp:TextBox>    
                &nbsp;<asp:Button ID="btnFind" class="btn btn-sm  btn-primary" runat="server" Text="Find" />
            </div>
        </div>
        <div class="col-4">
            <div class="input-group sm-3">
                <div class="input-group-prepend">
                <span class="input-group-text">เลขที่สัญญา</span>
                </div>
                <asp:TextBox class="form-control" ID="txtagreenoFind" runat="server"></asp:TextBox>    
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
            <div class="input-group sm-3">
                <div class="input-group-prepend">
                <span class="input-group-text">เลขที่โครงการ</span>
                </div>
                <asp:TextBox class="form-control" ID="txtProjectNo" runat="server"></asp:TextBox>    
            </div>
        </div>
        <div class="col-4">
            <div class="input-group sm-3">
                <div class="input-group-prepend">
                <span class="input-group-text">วันที่เริ่มโครงการ</span>
                </div>
                <asp:TextBox class="form-control" ID="txtProjectDate" runat="server"></asp:TextBox>    
            </div>
        </div>
        <div class="col-4">
            <div class="input-group sm-3">
                <div class="input-group-prepend">
                <span class="input-group-text">ผจก.โครงการ</span>
                </div>
                <asp:TextBox class="form-control" ID="txtProjectManager" runat="server"></asp:TextBox>    
            </div>            
        </div>
     </div>
      <div class="row">
        <div class="col-4">
            <div class="input-group sm-3">
                <div class="input-group-prepend">
                <span class="input-group-text">รหัสสาขา</span>
                </div>
                <asp:TextBox class="form-control" ID="txtBranchCode" runat="server"></asp:TextBox>    
            </div>
        </div>
        <div class="col-4">
            <div class="input-group sm-3">
                <div class="input-group-prepend">
                <span class="input-group-text">สถานะโครงการ</span>
                </div>
                <asp:TextBox class="form-control" ID="txtProjectStatus" runat="server"></asp:TextBox>    
            </div>
        </div>
     </div><p></p>
      <div class="row">
        <div class="col-4">
            <div class="input-group sm-3">
                <div class="input-group-prepend">
                <span class="input-group-text">เลขที่สัญญา</span>
                </div>
                <asp:TextBox class="form-control" ID="txtAgreeNo" runat="server"></asp:TextBox>    
            </div>
        </div>
        <div class="col-4">
            <div class="input-group sm-3">
                <div class="input-group-prepend">
                <span class="input-group-text">วันที่ทำสัญญา</span>
                </div>
                <asp:TextBox class="form-control" ID="txtAgreeDate" runat="server"></asp:TextBox>    
            </div>
        </div>
     </div>

                <div class="card-body">
                    <div class="table-responsive">

                        <table class="table table-bordered " id="dataTable">
                            <thead class="table-info">
                                <tr>
                                    <th style="width: 50px;">ลำดับที่</th>
                                    <th style="width: 100px;">รหัส</th>
                                    <th style="width: 200px;">ชื่อ-นามสกุล คู่สัญญา</th>
                                    <th style="width: 300px;">ที่อยู่</th>
                                    <th style="width: 80px;"></th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>1</td>
                                    <td>C001</td>
                                    <td>นายทดสอบ ระบบสัญญา</td>
                                    <td>นนทบุรี</td>
                                    <td>                                        
                                        <asp:LinkButton runat="server" ID="btnDel" Text="<i class='fas fa-minus-circle' style='color:red;font-size:20px'></i> " 
                                                ValidationGroup="edt" OnClick="BindData"  ToolTip="ลบ"/>
                                        <asp:LinkButton runat="server" ID="btnClientInfo" Text="<i class='fas fa-id-card' style='color:blue;font-size:20px'></i> " 
                                                ValidationGroup="edt" OnClick="BindData"  ToolTip="รายละเอียด"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td>
                                        <asp:DropDownList ID="cboClient" class="form-control" runat="server"></asp:DropDownList>
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:LinkButton runat="server" ID="btnAdddetail" Text="<i class='fas fa-plus-circle' style='color:green;font-size:20px'></i> " 
                                                ValidationGroup="edt" OnClick="BindData" ToolTip="เพิ่ม" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
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
        jQuery('[id$=txtCloseDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format:'d/m/Y'
        });
    </script>


</asp:Content>
