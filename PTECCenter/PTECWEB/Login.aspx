<%@ Page Title="Login" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="Login.aspx.vb" Inherits="PTECCENTER.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper">

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">   


  <div class="container">
    <div class="card card-login mx-auto mt-5">
      <div class="card-header">Login</div>
      <div class="card-body">

          <label class="sr-only" for="inlineFormInputGroupUsername">Username</label>
          <div class="input-group">
            <div class="input-group-prepend">
              <div class="input-group-text"><i class="fa fa-user-circle" aria-hidden="true"></i></div>
            </div>
            <asp:TextBox type="text" class="form-control" placeholder="Username" required="required" ID="txtUsername" autofocus="autofocus" runat="server"></asp:TextBox>
          </div>
          <br />
          <label class="sr-only" for="inlineFormInputGroupUsername">Password</label>
          <div class="input-group">
            <div class="input-group-prepend">
              <div class="input-group-text"><i class="fa fa-key" aria-hidden="true"></i></div>
            </div>
            <asp:TextBox type="password" class="form-control" placeholder="Password" required="required" ID="txtPassword"  runat="server"></asp:TextBox>
          </div>
          <div class="text-right mb-3 mt-1">
              <a class="small pr-1" href="forgot-password.aspx">ลืมรหัสผ่าน ?</a>
          </div>
<%--          <div class="form-group">
            <div class="checkbox">
              <label>
                <input type="checkbox" value="remember-me">
                Remember Password
              </label>
            </div>
          </div>--%>
          <asp:Button ID="btnLogin" class="btn btn-primary btn-block" runat="server" Text="Login" /><br />
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
           <%-- <footer class="sticky-footer">
                <div class="container my-auto">
                    <div class="copyright text-center my-auto">
                    <span>Copyright © Your Website 2019</span>
                    </div>
                </div>
            </footer>--%>
        </div>        <!-- end content-wrapper -->


        <!-- end เนื้อหาเว็บ -->


    </div>
    <!-- /#wrapper -->
</asp:Content>
