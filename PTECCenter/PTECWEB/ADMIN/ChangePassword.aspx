<%@ Page Title="Change Password" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="ChangePassword.aspx.vb" Inherits="PTECCENTER.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper">

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">   


  <div class="container">
    <div class="card card-login mx-auto mt-5">
      <div class="card-header">Change Password</div>
      <div class="card-body">
          <label class="sr-only" for="inlineFormInputGroupUsername">New Password</label>
          <div class="input-group">
            <div class="input-group-prepend">
              <div class="input-group-text"><i class="fa fa-key" aria-hidden="true"></i></div>
            </div>
            <asp:TextBox type="password" class="form-control" placeholder="New Password" required="required" ID="txtpassword" autofocus="autofocus" runat="server"></asp:TextBox>
          </div>
          <br />
          <label class="sr-only" for="inlineFormInputGroupUsername">Confirm Password</label>
          <div class="input-group">
            <div class="input-group-prepend">
              <div class="input-group-text"><i class="fa fa-key" aria-hidden="true"></i></div>
            </div>
            <asp:TextBox type="password" class="form-control" placeholder="Confirm Password" required="required" ID="txtconfirmPassword"  runat="server"></asp:TextBox>
          </div>
          <br />
<%--          <div class="form-group">
            <div class="checkbox">
              <label>
                <input type="checkbox" value="remember-me">
                Remember Password
              </label>
            </div>
          </div>--%>
          
          
          <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="บันทึก" />
          <input type="button" class="btn btn-danger" value="Cancel" onclick="history.go(-1);return false;"><br />
          <asp:Label ID="lblMessage" runat="server" Text=".."></asp:Label>
<%--          <a class="btn btn-primary btn-block" href="index.html">Login</a>--%>
<%--        <div class="text-center">
          <a class="d-block small mt-3" href="register.html">Register an Account</a>
          <a class="d-block small" href="forgot-password.html">Forgot Password?</a>
        </div>--%>
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
</asp:Content>
