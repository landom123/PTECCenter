<%@ Page Title="Forgot password" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="forgot-password.aspx.vb" Inherits="PTECCENTER.forgotpassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper">

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">

                <div class="container">
                    <div class="card card-login mx-auto mt-5">
                        <%--<div class="card-header"> </div>--%>
                        <div class="card-body">
                            <div class="preReset mb-3" id="preReset" runat="server">

                                <div class="text-center mb-4">
                                    <h4>ลืมรหัสผ่าน ?</h4>
                                    <p>รีเซ็ตรหัสผ่านด้วย Usercode และ Email ที่ใช้งาน</p>
                                </div>
                                <div class="form-group">
                                    <div class="form-label-group">
                                        <asp:TextBox class="form-control mb-3" ID="txtUsercode" runat="server" placeholder="Enter Usercode" required="required" autofocus="autofocus"></asp:TextBox>
                                        <asp:Label AssociatedControlID="txtUsercode" runat="server" Text="Enter Usercode" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="form-label-group">
                                        <asp:TextBox class="form-control" ID="txtEmail" runat="server" placeholder="Enter email address" required="required"></asp:TextBox>
                                        <asp:Label AssociatedControlID="txtEmail" runat="server" Text="Enter email address" />
                                    </div>
                                </div>
                                <asp:Button ID="btnSubmit" class="btn btn-primary btn-block" runat="server" Text="รีเซ็ตรหัสผ่าน" />
                            </div>
                            <div class="postReset mb-3" id="postReset" runat="server">
                                <asp:Label ID="lbcodeRef" runat="server" Text="Coderef : " />
                            </div>

                            <div class="text-center">
                                <a class="d-block small" href="login.aspx">กลับสู่หน้า login</a>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <!-- /.container-fluid -->
            <!-- Sticky Footer -->
        </div>
        <!-- end content-wrapper -->


        <!-- end เนื้อหาเว็บ -->


    </div>

    <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <!-- /#wrapper -->
    <script type="text/javascript">

        $(document).ready(function () {
            document.body.style.backgroundColor = "#343a40";
            $('nav').hide();

        });

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
