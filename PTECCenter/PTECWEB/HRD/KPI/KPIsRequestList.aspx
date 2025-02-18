<%@ Page Title="KPIsRequestList" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="KPIsRequestList.aspx.vb" Inherits="PTECCENTER.KPIsRequestList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        th {
            text-align: center;
        }
        
            th a {
                color: black;
            }
        #content-wrapper {
            height: 93vh;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg">
        <div id="wrapper" class="h-100">

            <!-- #include virtual ="/include/menu.inc" -->
            <!-- add side menu -->

            <div id="content-wrapper">

                <div class="container-fluid">
                    <ol class="breadcrumb" style="background-color: deeppink; color: white">
                        <li class="breadcrumb-item">สร้าง KPIs <% If operator_code.IndexOf(Session("usercode").ToString) > -1 Then%> (Operator) <% End If %>
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
                        <div class="col-auto" style="margin-left: auto;">
                            <input class="form-check-input chk-img-after" type="checkbox" id="chkCO" name="pay[1][]" runat="server">
                            <asp:Label ID="lbchkCO" CssClass="form-check-label" AssociatedControlID="chkCO" runat="server" Text="CO" />
                        </div>
                        <div class="col-auto">
                            <input class="form-check-input chk-img-after" type="checkbox" id="chkHO" name="pay[1][]" runat="server">
                            <asp:Label ID="lbchkHO" CssClass="form-check-label" AssociatedControlID="chkHO" runat="server" Text="HO" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 mb-3">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Company</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboCompany" runat="server" AutoPostBack="false"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Period</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboPeriod" runat="server" AutoPostBack="false"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 CO">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Employee KPIs</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboCreatebyCO" runat="server" readonly="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 HO">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Employee KPIs</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboCreateby" runat="server" readonly="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 HO">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Ratio Type</span>
                                </div>
                                <asp:DropDownList ID="cboRatio" class="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 HO">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Position</span>
                                </div>
                                <asp:DropDownList ID="cboPosition" class="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <% 'If operator_code.IndexOf(Session("usercode").ToString) > -1 Then%>
                        <div class="col-md-3 mb-3 HO">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Dep.</span>
                                </div>
                                <asp:DropDownList ID="cboDepartment" class="form-control" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 HO">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Sec.</span>
                                </div>
                                <asp:DropDownList ID="cboSection" class="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <%--<% End If %>--%>
                        <div class="col-md-3 mb-3 CO">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">BranchType</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboBranchGroup" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 CO">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Branch</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboBranch" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12 mb-3 HO">
                            <asp:Label ID="note" CssClass="text-danger text-right" runat="server" Text="( เงื่อนไข : รายการใน 'แผนก' จะเปลี่ยนไปตาม 'ฝ่าย' ที่เลือก )" />
                        </div>
                        <div class="col-md-12 mb-3 CO">
                            <asp:Label ID="note2" CssClass="text-danger text-right" runat="server" Text="( เงื่อนไข : รายการใน 'สาขา' จะเปลี่ยนไปตาม 'ประเภทสาขา' ที่เลือก )" />
                        </div>
                    </div>
                    <%-- <% If operator_code.IndexOf(Session("usercode").ToString) > -1 Then%>--%>


                    <div class="card-body">
                        <div class="table-responsive overflow-auto" style="font-size: 0.9rem">
                            <asp:GridView ID="gvRemind"
                                class="table table-striped table-bordered"
                                AllowSorting="true"
                                AutoGenerateColumns="false"
                                EmptyDataText="No data available."
                                PageSize="20"
                                AllowPaging="true"
                                runat="server">
                                <Columns>
                                    <asp:TemplateField SortExpression="PeriodDescription" HeaderText="งวด" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcode" runat="server" Text='<%#Eval("PeriodDescription")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="kpinew_code" HeaderText="เลขใบงาน" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcode" runat="server" Text='<%#Eval("kpinew_code")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="CreateDate" HeaderText="วันที่สร้าง" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcode" runat="server" Text='<%#Eval("CreateDate")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="ownercode" HeaderText="เจ้าของ KPIs" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcode" runat="server" Text='<%#Eval("ownercode")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="ownername" HeaderText="ชื่อเจ้าของ KPIs" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcode" runat="server" Text='<%#Eval("ownername")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="ownerempuppercode" HeaderText="ผู้มีสิทธิอนุมัติ" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBranch" runat="server" Text='<%#Eval("ownerempuppercode")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="statusname" HeaderText="สถานะ" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcode" runat="server" Text='<%#Eval("statusname")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <div class="d-flex align-items-center">
                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Eval("link")%>' Text=""><img src="../../../icon/addnote.png" title="รายละเอียด" style="width:20px" /></asp:HyperLink>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <%--<h4>ทั้งหมด <% =cntdt%> รายการ</h4>--%>
                    </div>


                </div>
            </div>
        </div>
    </div>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <CompositeScript>
            <Scripts>
                <asp:ScriptReference Path="~/vendor/jquery/jquery.min.js" />
            </Scripts>
        </CompositeScript>
    </asp:ScriptManager>
    <%--<script src="<%=Page.ResolveUrl("~/js/Sortable.js")%>"></script>--%>
    <%--<script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>--%>
    <!-- datetimepicker ต้องไปทั้งชุด-->
    <%--<script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>--%>
    <%--    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/NonPO.js")%>"></script>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.form-control').selectpicker({
                noneSelectedText: '-',
                liveSearch: true,
                maxOptions: 1
            });
            checkCOorHO();
        });
        function checkCOorHO() {
            if ($("#" + "<%= chkHO.ClientID.ToString %>").is(":checked")) {
                $(".HO").show();
                $(".CO").hide();
            } else if ($("#" + "<%= chkCO.ClientID.ToString %>").is(":checked")) {
                $(".CO").show();
                $(".HO").hide();
            } else {
                $(".CO").hide();
                $(".HO").hide();
            }
        }
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
