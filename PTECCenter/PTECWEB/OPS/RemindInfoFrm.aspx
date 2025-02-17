<%@ Page Title="Remind : ระบบการแจ้งเตือน" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="RemindInfoFrm.aspx.vb" Inherits="PTECCENTER.FrmRemindInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

  <!-- datetimepicker-->
  <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="wrapper" class="h-100">

    <!-- #include virtual ="/include/menu.inc" --> <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">    
                <!-- Breadcrumbs-->
                <ol class="breadcrumb">
                  <li class="breadcrumb-item">
                    <a href="#">OPS : ระบบการแจ้งเตือน - <% =objStatus %></a>
                  </li>
                </ol>
                <div class="row">
                    <div class="col-12">
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text="บันทึก" readonly="true" UseSubmitBehavior="false" />  &nbsp;  
                        <asp:Button ID="btnSaveAs" class="btn btn-sm  btn-danger" runat="server" Text="บันทึกเป็น" UseSubmitBehavior="false" />  &nbsp;
                    </div>
                </div>
                <p></p>
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">เลขที่</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtRemindID" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">วันที่สร้างรายการ</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtCreateDate" runat="server" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">วันที่ปรับปรุงล่าสุด</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtUpdateDate" runat="server" ReadOnly="True"></asp:TextBox>
                        </div>                                                
                    </div>
                </div>             
<p></p>
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">สาขา</span>
                          </div>
                            <asp:DropDownList class="custom-select" ID="cboBranch" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>                
<p></p>
                <div class="row">
                    <div class="col-12">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">หัวข้อ</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtRemindDetail" runat="server" ></asp:TextBox>
                        </div>
                    </div>
                </div>
<p></p>
                <div class="row">
                    <div class="col-12">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">รายละเอียด</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtEmailMessage" runat="server" ></asp:TextBox>
                        </div>
                    </div>
                </div>
<p></p>
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">email</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtEmail" runat="server"></asp:TextBox>
                        </div>                                                
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">วันที่เตือน</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtRemindDate" runat="server"  autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">ครั้งต่อไป</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtNextDate" runat="server" readonly="true" ></asp:TextBox>
                        </div>
                    </div>
                </div>    
<p></p>
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">เตือนล่วงหน้า (วัน)</span>
                          </div>
                            <asp:TextBox class="form-control" ID="txtWarningDay" runat="server" ></asp:TextBox>                            
                        </div>                                                
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">เตือนเฉพาะวัน</span>
                          </div>
                            <asp:DropDownList class="custom-select" ID="cboRepeat" runat="server" readonly="true"></asp:DropDownList>
                        </div>                                                
                    </div>
                </div>
<p></p>
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">ประเภทการแจ้งซ้ำ</span>
                          </div>
                            <asp:DropDownList class="custom-select" ID="cboPeriodType" runat="server" readonly="true"></asp:DropDownList>
                        </div>                                                
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">ความถี่การแจ้งซ้ำ</span>
                          </div>
                            <asp:TextBox class="form-control" ID="txtPeriod" runat="server"></asp:TextBox>
                        </div>                                                
                    </div>

                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">เปิดใช้งาน</span>
                          </div>
                            <asp:DropDownList class="custom-select" ID="cboActived" runat="server" readonly="true"></asp:DropDownList>
                        </div>                                                
                    </div>
                </div>  


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
        jQuery('[id$=txtRemindDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format:'d/m/Y'
        });
    </script>

</asp:Content>
