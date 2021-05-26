<%@ Page Title="จัดการผู้ใช้งาน" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="UserManage.aspx.vb" Inherits="PTECCENTER.UserManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper">

    <!-- #include virtual ="/include/menu.inc" --> <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">    
                <!-- Breadcrumbs-->
                <ol class="breadcrumb">
                  <li class="breadcrumb-item">
                    <a href="#">ADMIN > <i class="fa fa-user-circle" aria-hidden="true"></i>จัดการผู้ใช้</a>
                  </li>
                </ol>


                <div class="row">
                    <div class="col-12">
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-primary" runat="server" Text="New" />&nbsp;
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text="Save" />  &nbsp;              
                        <asp:Button ID="btnResetPassword" class="btn btn-sm  btn-danger" runat="server" Text="Reset Password" />  &nbsp;     
                        &nbsp;   
                        &nbsp;</div>
                </div>
                <p></p>
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">Username / Login name</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtUsercode" runat="server"></asp:TextBox>
                          <div class="input-group-append">
                            <button type="button" class="btn btn-sm  btn-secondary"  onclick="find('../jobs/jobs.aspx?jobno=','ระบุ Login')" >Find</button>
                          </div>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend"><span class="input-group-text">ชื่อ-สกุล</span></div>
                          <asp:TextBox class="form-control" ID="txtUserName" runat="server" ></asp:TextBox>
                        </div>
                    </div>
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
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">ฝ่าย</span>
                          </div>
                          <asp:DropDownList class="custom-select" ID="cboDepart" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend"><span class="input-group-text">แผนก</span></div>
                          <asp:DropDownList class="custom-select" ID="cboSection" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend"><span class="input-group-text">ตำแหน่ง</span></div>
                          <asp:DropDownList class="custom-select" ID="cboPosition" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">อีเมล์</span>
                          </div>
                          <asp:TextBox class="form-control" ID="TextBox3" runat="server" ></asp:TextBox>
                        </div>                                                
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
</asp:Content>
