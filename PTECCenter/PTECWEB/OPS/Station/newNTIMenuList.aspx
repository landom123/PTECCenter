<%@ Page Title="newNTIMenuList" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="newNTIMenuList.aspx.vb" Inherits="PTECCENTER.newNTIMenuList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper" class="h-100">

        <!-- #include virtual ="/include/menu.inc" -->
        <!-- add side menu -->

        <!-- เนื้อหาเว็บ -->
        <div id="content-wrapper">

            <div class="container-fluid">
                <ol class="breadcrumb" style="background-color: navy; color: white">
                    <li class="breadcrumb-item">รายการ New NTI
                    </li>
                </ol>

                <div class="row">
                    <div class="col-12 mb-3">
                        <asp:Button ID="btnSearch" class="btn btn-sm  btn-success" runat="server" Text="Search" />&nbsp;
                            <asp:Button ID="btnClear" class="btn btn-sm  btn-secondary" runat="server" Text="Clear" />&nbsp;
                        
                            <asp:Button ID="btnExport" class="btn btn-sm  btn-info" runat="server" Text="Export" />&nbsp;
                    </div>
                </div>

                <%--<div class="input-group d-none">
                                                <asp:DropDownList ID="cboStatus" class="form-control" runat="server"></asp:DropDownList>
                                                <div class="input-group-append">
                                                    <asp:Button ID="btnConfirm" class="btn btn-sm  btn-warning" runat="server" Text=" + " OnClientClick="validateData()" />
                                                </div>
                                            </div>--%>
                <div class="row">
                    <div class="col-md-3 mb-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Code</span>
                            </div>
                            <asp:TextBox class="form-control noEnterSubmit" ID="txtcode" runat="server" placeholder="21XXXXXXX" AutoPostBack="false" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3 mb-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ชื่อสถานที่</span>
                            </div>
                            <asp:TextBox class="form-control noEnterSubmit" ID="txttitle" runat="server" AutoPostBack="false" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3 mb-3">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">สถานะ</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboStatus" runat="server" AutoPostBack="false"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3 mb-3">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ข้อเสนอ</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboOfferType" runat="server" AutoPostBack="false"></asp:DropDownList>
                        </div>
                    </div>
                </div>
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
                                <asp:TemplateField HeaderText="เลขที่ใบงาน" ItemStyle-HorizontalAlign="center">
                                    <itemtemplate>
                                        <asp:Label ID="lbcomcode" runat="server" Text='<%#Eval("nticode")%>'></asp:Label>
                                    </itemtemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ชื่อสถานที่" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranch" runat="server" Text='<%#Eval("title")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="วันที่ทำรายการ" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbljobdate" runat="server" Text='<%#Eval("CreateDate")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ชื่อ-นามสกุลผู้แจ้ง" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcode" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="เบอร์" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcode" runat="server" Text='<%#Eval("tell")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="อีเมล" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcode" runat="server" Text='<%#Eval("email")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รายละเอียด" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranch" runat="server" Text='<%#Eval("remark")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="สถานะ" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbljobtype" runat="server" Text='<%#Eval("Statusname")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <div class="d-flex align-items-center">
                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Eval("link")%>' Text=""><img src="../../../../icon/addnote.png" title="รายละเอียด" style="width:20px" /></asp:HyperLink>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                    <h4>ทั้งหมด <% =cntdt%> รายการ</h4>
                </div>
            </div>
            <!-- /.container-fluid -->


        </div>
    </div>

    <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>
    <script>
        // <!-- #################################  for operator ########################### -->
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
        jQuery('[id$=txtStartDueDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08'
            onShow: function (ct) {
                this.setOptions({
                    maxDate: jQuery('[id$=txtEndDueDate]').val() ? jQuery('[id$=txtEndDueDate]').val() : false, formatDate: 'd.m.Y'
                })
            },
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });

        jQuery('[id$=txtEndDueDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            onShow: function (ct) {
                this.setOptions({
                    minDate: jQuery('[id$=txtStartDueDate]').val() ? jQuery('[id$=txtStartDueDate]').val() : false, formatDate: 'd.m.Y'
                })
            },
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });

        //< !-- #################################  for operator ###########################-- >


        $(document).ready(function () {
            $('.form-control').selectpicker({
                noneSelectedText: '-',
                liveSearch: true,
                maxOptions: 1
            });
            checkCOorHO();
        });


        $("input:checkbox").on('click', function () {
            // in the handler, 'this' refers to the box clicked on
            console.log(this);
            var $box = $(this);
            if ($box.is(":checked")) {
                // the name of the box is retrieved using the .attr() method
                // as it is assumed and expected to be immutable
                var group = "input:checkbox";
                // the checked state of the group/box on the other hand will change
                // and the current value is retrieved using .prop() method
                $(group).prop("checked", false);
                $box.prop("checked", true);

            } else {
                $box.prop("checked", false);
            }

            checkCOorHO();
        });
        
        $('.noEnterSubmit').keypress(function (e) {
            if (e.which == 13) return false;
            //or...
            if (e.which == 13) e.preventDefault();
        });
    </script>

</asp:Content>
