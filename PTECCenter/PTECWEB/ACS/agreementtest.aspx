<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="agreementtest.aspx.vb" Inherits="PTECCENTER.agreementtest" %>

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

            </div>
        </div>

    </div>
    <ul class="nav nav-pills mb-3" id="pills-tab" role="tablist">
      <li class="nav-item">
        <a class="nav-link active" id="pills-info-tab" data-toggle="pill" href="#info" role="tab" aria-controls="pills-info" aria-selected="<% If Session("pages") = "project" Then  %>true<% else %>false<%End if %>">ข้อมูลโครงการ</a>
      </li>
      <li class="nav-item">
        <a class="nav-link" id="pills-assets-tab" data-toggle="pill" href="#assets" role="tab" aria-controls="pills-assets" aria-selected="<% If Session("pages") = "assets" Then  %>true<% else %>false<%End if %>">ข้อมูลที่ดิน</a>
      </li>
      <li class="nav-item">
        <a class="nav-link " id="pills-agree-tab" data-toggle="pill" href="#agree" role="tab" aria-controls="pills-agree" aria-selected="<% If Session("pages") = "agree" Then  %>true<% else %>false<%End if %>">ข้อมูลสัญญา</a>
      </li>
    </ul>


  <div class="tab-content">  
    <div id="info" class="tab-pane fade" style="background-color:ghostwhite">  
      โครงการ
    </div>  
      <%----=====end tab agree info=======--%>

    <div id="agree" class="tab-pane fade" style="background-color:ghostwhite">  
        <ul class="nav nav-pills mb-3" id="agree-tab" role="tablist">
          <li class="nav-item">
            <a class="nav-link" id="agree-ag-tab" data-toggle="pill" href="#ag" role="tab" aria-controls="pills-ag" aria-selected="<% If Session("pages") = "ag" Then  %>true<% else %>false<%End if %>">ข้อมูลสัญญา</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" id="agree-payment-tab" data-toggle="pill" href="#payment" role="tab" aria-controls="pills-payment" aria-selected="<% If Session("pages") = "payment" Then  %>true<% else %>false<%End if %>">ข้อมูลค่าเช่า/ผลตอบแทน</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" id="agree-finance-tab" data-toggle="pill" href="#finance" role="tab" aria-controls="pills-finance" aria-selected="<% If Session("pages") = "finance" Then  %>true<% else %>false<%End if %>">ข้อมูลการเงิน</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" id="agree-remark-tab" data-toggle="pill" href="#remark" role="tab" aria-controls="pills-remark" aria-selected="<% If Session("pages") = "remark" Then  %>true<% else %>false<%End if %>">ข้อมูลแนบท้ายสัญญา</a>
          </li>
        </ul>
        <div class="tab-content">  
            <div id="ag" class="tab-pane fade" style="background-color:ghostwhite">  
                agree
            </div>  
              <%----=====end tab สัญญา=======--%>
            <div id="payment" class="tab-pane fade " style="background-color:ghostwhite">  
                payment
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
      <%----=====end tab agree =======--%>

    <div id="assets" class="tab-pane fade" style="background-color:white">  
        <p>บันทึกข้อมูลที่ดิน สิ่งปลูกสร้าง ที่ทำสัญญาเช่า หรือร่วมธุรกิจ</p>  
    </div>
        <%----=====end tab ที่ดิน=======--%>

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
