<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="JobsLists.aspx.vb" Inherits="PTECCENTER.JobsList_test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
    <style>
        th {
            text-align: center;
        }

        .input-group {
            padding-top: 1rem;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper">

        <!-- #include virtual ="/include/menu.inc" -->
        <!-- add side menu -->

        <!-- เนื้อหาเว็บ -->
        <div id="content-wrapper">

            <div class="container-fluid">
                <ol class="breadcrumb" style="background-color: deeppink; color: white">
                    <li class="breadcrumb-item">รายการ Jobs
                    </li>
                </ol>

                <div class="row">
                    <div class="col-12">
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-primary" runat="server" Text="New" />&nbsp;
                        <% If Not Session("positionid") = "10" Then%>
                        <asp:Button ID="btnSearch" class="btn btn-sm  btn-warning" runat="server" Text="Search" />&nbsp; 
                        <asp:Button ID="btnClear" class="btn btn-sm  btn-secondary" runat="server" Text="Clear" />&nbsp;
                         <% End If %>
                        <button type="button" class="btn btn-sm  btn-info noEnterSubmit" style="color: #495057;" title="Export" id="btnExport" runat="server" data-toggle="modal" data-target="#modalExport" data-backdrop="static" data-keyboard="false" data-whatever="new"><i class="fas fa-file-download"></i></button>

                    </div>
                </div>

                <% If Not Session("positionid") = "10" Then%>

                <div class="row">
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Code</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtjobcode" runat="server" placeholder="21XXXXXXX"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ฝ่าย</span>
                            </div>
                            <asp:DropDownList ID="cboDep" class="form-control" runat="server" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">หัวข้องาน</span>
                            </div>
                            <asp:DropDownList ID="cboJobType" class="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">สถานะงานย่อย</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboStatusFollow" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ประเภทสาขา</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboBranchGroup" runat="server" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">สาขา</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboBranch" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ตั้งแต่วันที่</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtStartDate" name="txtStartDate" runat="server" placeholder="--- คลิกเพื่อเลือก ---"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">จนถึง</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtEndDate" name="txtEndDate" runat="server" placeholder="--- คลิกเพื่อเลือก ---"></asp:TextBox>

                        </div>
                    </div>
                </div>
                <div class="row" style="padding-top: 1rem;">
                    <div class="col-md-12">
                        <asp:Label ID="note" CssClass="text-danger text-right" runat="server" Text="( เงื่อนไข 1 : รายการใน 'หัวข้องาน' จะเปลี่ยนไปตาม 'ฝ่าย' ที่เลือก )" />
                    </div>
                    <div class="col-md-12">
                        <asp:Label ID="note2" CssClass="text-danger text-right" runat="server" Text="( เงื่อนไข 2 : รายการใน 'สาขา' จะเปลี่ยนไปตาม 'ประเภทสาขา' ที่เลือก )" />
                    </div>
                </div>
                <% else %>
                <div class="row">
                    <div class="col-md-4">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">สถานะงาน</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboWorking" runat="server" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <% End If %>
                <div class="card-body">
                    <div class="table-responsive overflow-auto" style="font-size: 0.9rem">
                        <asp:GridView ID="gvRemind"
                            class="table table-striped table-bordered"
                            AutoGenerateColumns="false"
                            EmptyDataText="No data available."
                            PageSize="20"
                            AllowPaging="true"
                            runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcode" runat="server" Text='<%#Eval("jobcode")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranch" runat="server" Text='<%#Eval("branch")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbljobdate" runat="server" Text='<%#Eval("jobdate")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lbljobtype" runat="server" Text='<%#Eval("jobtype")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Detail">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldetails" runat="server" Text='<%#Eval("details")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Update">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldetails" runat="server" Text='<%#Eval("lastupdate")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Update Detail">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldetails" runat="server" Text='<%#Eval("detailFollow")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldetails" runat="server" Text='<%#String.Format("{0:n}", Eval("cost"))%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("status")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Eval("link")%>' Text=""><img src="../icon/addnote.png" title="รายละเอียด" style="width:20px" /></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <h4>ทั้งหมด <% =cntdt%> รายการ</h4>
                </div>


            </div>
            <!-- /.container-fluid -->


            <!-- Sticky Footer -->
            <footer class="sticky-footer">
                <div class="container my-auto">
                    <div class="copyright text-center my-auto">
                        <span>Copyright © Your Website 2019</span>
                    </div>
                </div>
            </footer>

        </div>
        <!-- /.content-wrapper -->
        <!-- end เนื้อหาเว็บ -->


    </div>
    <!-- /#wrapper -->


    <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>
    <script type="text/javascript">
        jQuery('[id$=txtStartDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08'
            onShow: function (ct) {
                this.setOptions({
                    maxDate: jQuery('[id$=txtEndDate]').val() ? jQuery('[id$=txtEndDate]').val() : false, formatDate: 'd.m.Y'
                })
            },
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });

        jQuery('[id$=txtEndDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            onShow: function (ct) {
                this.setOptions({
                    minDate: jQuery('[id$=txtStartDate]').val() ? jQuery('[id$=txtStartDate]').val() : false, formatDate: 'd.m.Y'
                })
            },
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>

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
