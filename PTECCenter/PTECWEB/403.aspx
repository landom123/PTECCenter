<%@ Page Title="403page" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="403.aspx.vb" Inherits="PTECCENTER.page403" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .noContent {
            height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper" class="h-100">

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper" class="pt-0 pb-0">
            <div class="container-fluid">

                <div class="container">
                    <div class="noContent m-auto">
                        <div class="d-flex flex-column">
                            <div class="m-auto">
                                <h1>Error 403</h1>
                            </div>
                            <div class="row">
                                <div class="col mb-3">
                                    <div class="text-center">
                                        คุณไม่มีสิทธ์เข้าถึง กรุณาติดต่อ ADMIN
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col text-center mb-3">
                                    <a href="<%=Page.ResolveUrl("~/default.aspx")%>" class="btn btn-sm btn-danger "><-- กลับ</a>
                                </div>
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
