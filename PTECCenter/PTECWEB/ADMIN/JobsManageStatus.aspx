<%@ Page Title="จัดการสถานะงานOPS" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="JobsManageStatus.aspx.vb" Inherits="PTECCENTER.JobsManageStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper" class="h-100">

        <!-- #include virtual ="/include/menu.inc" -->
        <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">
                <!-- Breadcrumbs-->
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">ADMIN > จัดการสถานะงานOPS</a>
                    </li>
                </ol>
                <div class="row mb-3">
                    <div class="col-12">
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-primary" runat="server" Text="New" />
                        &nbsp;
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text="Save"/>
                        &nbsp;
                         
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-xl-4">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">เลขที่เอกสาร</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtJobno" runat="server" ReadOnly="true"></asp:TextBox>
                            <div class="input-group-append">
                                <button type="button" class="btn btn-sm  btn-secondary" onclick="find('../ADMIN/JobsManageStatus.aspx?jobno=','ระบุเลขที่ OPS')">Find</button>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-4">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">สถานะงานปัจจุบัน</span>
                            </div>
                            <div class="input-group-append">
                                <asp:TextBox class="btn btn-danger" ID="txtStatus" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">สถานะงานที่ต้องการเปลี่ยน</span>
                                <asp:DropDownList class="form-control" ID="cboStatus" runat="server" ></asp:DropDownList>
                            </div>
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
