<%@ Page Title="จัดการผู้ใช้งาน" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="UserManage.aspx.vb" Inherits="PTECCENTER.UserManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .input-group {
            margin-bottom: 1rem;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper">

        <!-- #include virtual ="/include/menu.inc" -->
        <!-- add side menu -->

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
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-primary" runat="server" Text="New" />
                        &nbsp;
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text="Save" OnClientClick="validateData();" />
                        &nbsp;
                        <asp:Button ID="btnUpdate" class="btn btn-sm  btn-warning" runat="server" Text="Update" OnClientClick="validateData();" />
                        &nbsp;              
                        <asp:Button ID="btnResetPassword" class="btn btn-sm  btn-danger" runat="server" Text="Reset Password"  />
                        
                        &nbsp;     
                    </div>
                </div>
                <div class="row" style="padding-top: 1rem;">
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Usercode / Login name</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtUsercode" runat="server" Style="text-transform: uppercase" MaxLength="10" AutoPostBack="true" required></asp:TextBox>
                            <div class="invalid-feedback">กรุณาใส่ Initial</div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend"><span class="input-group-text">ชื่อ-สกุล</span></div>
                            <asp:TextBox class="form-control" ID="txtUserName" runat="server" required >
                            </asp:TextBox>
                            <div class="invalid-feedback">กรุณาใส่ ชื่อ-สกุล</div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">สาขา</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboBranch" runat="server" required></asp:DropDownList>
                            <div class="invalid-feedback">กรุณาเลือกสาขา</div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ฝ่าย</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboDepart" runat="server" ></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend"><span class="input-group-text">แผนก</span></div>
                            <asp:DropDownList class="form-control" ID="cboSection" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend"><span class="input-group-text">ตำแหน่ง</span></div>
                            <asp:DropDownList class="form-control" ID="cboPosition" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend"><span class="input-group-text">Empper</span></div>
                            <asp:DropDownList class="form-control" ID="cboEmpper" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">อีเมล์</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtEmail" runat="server" required></asp:TextBox>
                            <div class="invalid-feedback">กรุณากรอกอีเมล</div>
                        </div>
                    </div>
                </div>

            </div>
            <!-- /.container-fluid -->
        </div>
        <!-- end content-wrapper -->


        <!-- end เนื้อหาเว็บ -->


    </div>
    <!-- /#wrapper -->
    

    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>



    <script type="text/javascript">

        $(document).ready(function () {
            $('.form-control').selectpicker({
                liveSearch: true,
                maxOptions: 1
            });
        });

    </script>
    <script>
        $(".toggle-password").click(function () {

            $(this).toggleClass("fa-eye fa-eye-slash");

            var input = $($(this).attr("toggle"));
            console.log(input)
            if (input.attr("type") == "password") {
                input.attr("type", "text");
            } else {
                input.attr("type", "password");
            }
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

        function resetPassword() {

            Swal.fire({
                title: 'รหัสผ่านใหม่',
                input: 'password',
                inputLabel: 'Password',
                inputAttributes: {
                    maxlength: 10,
                    autocapitalize: 'off',
                    autocorrect: 'off'
                },
                preConfirm: () => {
                    if (!document.getElementById('swal2-input').value) {
                        // Handle return value 
                        Swal.showValidationMessage('ใส่รหัสผ่าน')
                    }
                },
                showCancelButton: true
            }).then((result) => {
                console.log(result.value);
                if (result.isConfirmed) {
                    var usercode = document.getElementById('<%= txtUsercode.ClientID%>').value;
                    var params = "{'usercode': '" + usercode + "','pass': '" + result.value + "'}";
                    //console.log(params);
                    $.ajax({
                        type: "POST",
                        url: "../admin/usermanage.aspx/resetPassword",
                        async: true,
                        data: params,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            console.log(msg.d)
                            if (msg.d) {
                                swal.fire({
                                    title: "success!",
                                    text: "",
                                    icon: "success"
                                });
                            } else {
                                alertWarning('fail')
                            }
                        },
                        error: function () {
                            alertWarning('fail')
                        }
                    });
                }
            })
        }

    </script>


</asp:Content>
