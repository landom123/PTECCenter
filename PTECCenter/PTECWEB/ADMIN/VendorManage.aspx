<%@ Page Title="จัดการ Vendor" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="VendorManage.aspx.vb" Inherits="PTECCENTER.VendorManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .input-group {
            margin-bottom: 1rem;
        }
    </style>
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
                        <a href="#">ADMIN > จัดการ Vendor</a>
                    </li>
                </ol>
                <div class="row">
                    <div class="col-12 mb-3">
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-primary" runat="server" Text="New" />
                        &nbsp;
                        <asp:Button ID="btnUpdate" class="btn btn-sm  btn-warning" runat="server" Text="Update" OnClientClick="validateData();" />
                        &nbsp;  
                        <button runat="server" id="btnOpen" class="btn btn-sm  btn-primary rounded-circle d-none" title="Upload">
                            <i class="fa fa-file-upload"></i>
                        </button>
                        <div class="file-upload mr-2">
                            <asp:FileUpload ID="FileUpload1" class="" onchange="UploadFile(this)" runat="server" text="เลือกไฟล์" accept=".xlsx" />
                            <label class="btn btn-sm  btn-info rounded-circle m-0" for="FileUpload1" runat="server"><i class="fa fa-file-upload"></i></label>
                        </div>
                        &nbsp;       
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 mb-3">
                        <div class="table-responsive overflow-auto" style="font-size: 0.7rem">
                            <asp:GridView ID="gvSource"
                                class="table table-striped table-bordered"
                                AutoGenerateColumns="false"
                                EmptyDataText="No data available."
                                AllowPaging="false"
                                runat="server">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-CssClass="text-center table-header" ItemStyle-Width="50px" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbcode" runat="server" Text='+'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vendor_Code" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbcode" runat="server" Text='<%#Eval("Vendor_Code_only")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vendor_Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbname" runat="server" Text='<%#Eval("name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
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
        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnOpen.ClientID %>").click();
            }
        }
    </script>


</asp:Content>
