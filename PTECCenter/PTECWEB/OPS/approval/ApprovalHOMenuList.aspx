<%@ Page Title="ApprovalHOMenuList" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="ApprovalHOMenuList.aspx.vb" Inherits="PTECCENTER.ApprovalHOMenuList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
    <style>
        th {
            text-align:center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper" class="h-100">
        <!-- #include virtual ="/include/menu.inc" -->
        <!-- add side menu -->

        <!-- เนื้อหาเว็บ -->
        <div id="content-wrapper">

            <div class="container-fluid">
                <ol class="breadcrumb" style="background-color: navy; color: white">
                    <li class="breadcrumb-item">รายการ Approval HO
                    </li>
                </ol>

                <div class="row">
                    <div class="col-12 mb-3">
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-primary" runat="server" Text="New" UseSubmitBehavior="false" />&nbsp;

                        <asp:Button ID="btnSearch" class="btn btn-sm  btn-success" runat="server" Text="Search" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="btnClear" class="btn btn-sm  btn-secondary" runat="server" Text="Clear" UseSubmitBehavior="false" />&nbsp;
                        <asp:Button ID="btnExport" class="btn btn-sm  btn-info" runat="server" Text="Export" UseSubmitBehavior="false" />&nbsp;
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-4 mb-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Code</span>
                            </div>
                            <asp:TextBox class="form-control noEnterSubmit" ID="txtaho" runat="server" placeholder="21XXXXXXX" AutoPostBack="false" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <%-- <div class="col-md-4 mb-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Code_ref</span>
                            </div>
                            <asp:TextBox class="form-control noEnterSubmit" ID="txtcoderef" runat="server" placeholder="21XXXXXXX" AutoPostBack="false"></asp:TextBox>
                        </div>
                    </div>--%>

                    <div class="col-md-4 mb-3">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ตั้งแต่วันที่</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtStartDate" name="txtStartDate" runat="server" placeholder="--- คลิกเพื่อเลือก ---" AutoPostBack="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4 mb-3">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">จนถึง</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtEndDate" name="txtEndDate" runat="server" placeholder="--- คลิกเพื่อเลือก ---" AutoPostBack="false"></asp:TextBox>

                        </div>
                    </div>
                    <div class="col-md-4 mb-3">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">สถานะงานย่อย</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboStatusFollow" runat="server" AutoPostBack="false"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-4 mb-3 HO">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ฝ่าย</span>
                            </div>
                            <asp:DropDownList ID="cboDepartment" class="form-control" runat="server" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4 mb-3 HO">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">แผนก</span>
                            </div>
                            <asp:DropDownList ID="cboSection" class="form-control" runat="server" AutoPostBack="false"></asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="row" style="padding-top: 1rem;">
                    <div class="col-md-12 HO">
                        <asp:Label ID="note" CssClass="text-danger text-right" runat="server" Text="( เงื่อนไข : รายการใน 'แผนก' จะเปลี่ยนไปตาม 'ฝ่าย' ที่เลือก )" />
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
                                <asp:TemplateField HeaderText="เลขใบงาน" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcode" runat="server" Text='<%#Eval("ApprovalHO_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="วันที่ทำรายการ" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbljobdate" runat="server" Text='<%#Eval("CreateDate")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ผู้ทำรายการ" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranch" runat="server" Text='<%#Eval("ownercode")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รายละเอียด">
                                    <ItemTemplate>
                                        <asp:Label ID="lbljobdate" runat="server" Text='<%#Eval("ApprovalHO_Detail")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ยอดรวม" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldetails" runat="server" Text='<%#String.Format("{0:n}", Eval("cost"))%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ผู้มีสิทธิอนุมัติ" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranch" runat="server" Text='<%#Eval("approvalcode")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="สถานะ" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbljobtype" runat="server" Text='<%#Eval("StatusName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="สำเร็จ / ยกเลิก / ทั้งหมด">
                                    <ItemTemplate>
                                        <div class="d-flex justify-content-center">
                                            <div class="col-auto">
                                                <asp:Label ID="lbprogress" runat="server">
                                                            <span class="text-success font-weight-bold"><%#Eval("progress").ToString.Split("/")(0)%></span> / <span class="text-danger font-weight-bold"><%#Eval("progress").ToString.Split("/")(1)%></span> / <span class="font-weight-bold"><%#Eval("progress").ToString.Split("/")(2)%></span>
                                                </asp:Label>
                                            </div>
                                            <div class="col-auto">
                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Eval("link")%>' Text=""><img src="../../../../icon/addnote.png" title="รายละเอียด" style="width:20px" /></asp:HyperLink>
                                            </div>
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
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <CompositeScript>
            <Scripts>
                <asp:ScriptReference Path="~/datetimepicker/jquery.js" />
                <asp:ScriptReference Path="~/datetimepicker/build/jquery.datetimepicker.full.min.js" />
            </Scripts>
        </CompositeScript>
    </asp:ScriptManager>
    <!-- datetimepicker ต้องไปทั้งชุด-->
    <%--<script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>--%>
    <script>
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

        $(document).ready(function () {
            $('.form-control').selectpicker({
                noneSelectedText: '-',
                liveSearch: true,
                maxOptions: 1
            });
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

        });
        $('.noEnterSubmit').keypress(function (e) {
            if (e.which == 13) return false;
            //or...
            if (e.which == 13) e.preventDefault();
        });
    </script>

</asp:Content>
