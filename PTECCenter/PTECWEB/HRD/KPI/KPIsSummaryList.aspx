<%@ Page Title="KPIsSummaryList" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="KPIsSummaryList.aspx.vb" Inherits="PTECCENTER.KPIsSummaryList" %>

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

        .tooltip-inner {
            max-width: 500px;
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
                    <ol class="breadcrumb" style="background-color: navy; color: white">
                        <li class="breadcrumb-item">ดูผลประเมิน <% If operator_code.IndexOf(Session("usercode").ToString) > -1 Then%> (Operator) <% End If %>
                        </li>
                    </ol>

                    <div class="row">
                        <div class="col-12 mb-3">
                            <asp:Button ID="btnNew" class="btn btn-sm  btn-primary" runat="server" Text="New" />&nbsp;
                        <asp:Button ID="btnSearch" class="btn btn-sm  btn-success" runat="server" Text="Search" />&nbsp;
                        <asp:Button ID="btnClear" class="btn btn-sm  btn-secondary" runat="server" Text="Clear" />&nbsp;
                            <asp:Button ID="btnExport" class="btn btn-sm  btn-info" runat="server" Text="Export" />&nbsp;
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
                                <asp:DropDownList class="form-control" ID="cboPeriod" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Forms</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboForms" runat="server" AutoPostBack="false"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 CO">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">OwnerKPIs</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboCreatebyCO" runat="server" readonly="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 HO">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">OwnerKPIs</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboCreateby" runat="server" readonly="true"></asp:DropDownList>
                            </div>
                        </div>
                        
                        <div class="col-md-3 mb-3">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Approver</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboApproverby" runat="server" readonly="true"></asp:DropDownList>
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
                        <div class="row justify-content-end">
                            <div class="col-auto">
                                <asp:DropDownList ID="cboMaxRows" class="form-control" runat="server">
                                    <asp:ListItem Value="1000">1,000 รายการ</asp:ListItem>
                                    <asp:ListItem Value="2147483647">รายการทั้งหมด</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
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
                                            <asp:Label ID="lbPeriodDescription" runat="server" Text='<%#Eval("PeriodDescription")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="FormsTitle" HeaderText="รอบการประเมิน" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbFormsTitle" runat="server" Text='<%#Eval("FormsTitle")%>'></asp:Label>
                                            <span class="text-muted" data-toggle="tooltip" data-placement="top" data-html="true"
                                                title='<%# String.Format("พนักงาน : {0} - {1} <span class=""{2}"">({3})</span><br />หัวหน้า : {4} - {5} <span class=""{6}"">({7})</span>",
                                                                                    Eval("OwnerBegin_Date"),
                                                                                    Eval("OwnerEnd_Date"),
                                                                                    If(DateTime.Now >= Eval("OwnerBegin_Date") AndAlso DateTime.Now <= Eval("OwnerEnd_Date"), "text-success", "text-danger"),
                                                                                    If(DateTime.Now >= Eval("OwnerBegin_Date") AndAlso DateTime.Now <= Eval("OwnerEnd_Date"), "อยู่ในช่วงประเมิน", "นอกช่วงประเมิน"),
                                                                                    Eval("ApprovalBegin_Date"),
                                                                                    Eval("ApprovalEnd_Date"),
                                                                                    If(DateTime.Now >= Eval("ApprovalBegin_Date") AndAlso DateTime.Now <= Eval("ApprovalEnd_Date"), "text-success", "text-danger"),
                                                                                    If(DateTime.Now >= Eval("ApprovalBegin_Date") AndAlso DateTime.Now <= Eval("ApprovalEnd_Date"), "อยู่ในช่วงประเมิน", "นอกช่วงประเมิน")) %>'>
                                                <i class="fas fa-info-circle"></i>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="CommitDate" HeaderText="วันยืนยันแบบฟอร์ม" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbCommitDate" runat="server" Text='<%#Eval("commitdate")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="owner_code" HeaderText="เจ้าของแบบฟอร์ม" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbowner_code" runat="server" Text='<%#Eval("owner_code")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="owner_name" HeaderText="ชื่อพนักงาน">
                                        <ItemTemplate>
                                            <asp:Label ID="lbowner_name" runat="server" Text='<%#Eval("owner_name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="positionname" HeaderText="ตำแหน่ง">
                                        <ItemTemplate>
                                            <asp:Label ID="lbpositionname" runat="server" Text='<%#Eval("positionname")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="branchname" HeaderText="สาขา" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbbranchname" runat="server" Text='<%#Eval("branchname")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="DepCode" HeaderText="ฝ่าย" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbDepCode" runat="server" Text='<%#Eval("DepCode")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="SecCode" HeaderText="แผนก" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbSecCode" runat="server" Text='<%#Eval("SecCode")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="approver_code" HeaderText="หัวหน้างาน" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbapprover_code" runat="server" Text='<%#Eval("approver_code")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="approval_date" HeaderText="วันที่หัวหน้างานประเมิน" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbapproval_date" runat="server" Text='<%#Eval("approval_date")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="statusTH" HeaderText="สถานะ" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbstatusTH" runat="server" Text='<%#Eval("statusTH")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <div class="d-flex flex-column flex-xl-row justify-content-around align-items-center">
                                                <asp:HyperLink ID="btnForm" runat="server" NavigateUrl='<%#Eval("link1")%>' Text="" ToolTip="ทำแบบประเมิน" CssClass="text-primary"><i class="far fa-edit"></i></asp:HyperLink>
                                                <asp:HyperLink ID="btnSummarySelf" runat="server" NavigateUrl='<%#Eval("link3")%>' Text="" ToolTip="ดูคะแนนดิบพนักงาน" CssClass="text-muted"><i class="fas fa-user-alt"></i></asp:HyperLink>
                                                <asp:HyperLink ID="btnSummary2Person" runat="server" NavigateUrl='<%#Eval("link4")%>' Text="" ToolTip="ดูคะแนนดิบ" CssClass="text-muted"><i class="fas fa-user-friends"></i></asp:HyperLink>
                                                <%--<asp:LinkButton ID="btnReqApproverEdit" runat="server" OnClientClick='<%# String.Format("reqEdit(""{0}"", {1}); return false;", Eval("res_code"), Session("userid")) %>' CssClass="text-primary">
                                                    <i class="fas fa-undo text-dark"></i>
                                                </asp:LinkButton>--%>
                                                <asp:HyperLink ID="btnSummaryView" runat="server" NavigateUrl='<%#Eval("link2")%>' Text="" ToolTip="ดูผลสรุปการประเมิน" CssClass="text-muted"><i class="fas fa-users"></i></asp:HyperLink>
                                                <asp:LinkButton ID="btnDel" runat="server" ToolTip="ลบแบบประเมิน" OnClientClick='<%# String.Format("reqDel(""{0}"", {1}); return false;", Eval("res_code"), Session("userid")) %>' CssClass="text-primary">
                                                    <i class="fas fa-trash-alt text-danger"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--<script src="<%=Page.ResolveUrl("~/js/Sortable.js")%>"></script>--%>
    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>
    <!-- datetimepicker ต้องไปทั้งชุด-->
    <%--<script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>--%>
    <%--    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/NonPO.js")%>"></script>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.form-control').selectpicker({
                noneSelectedText: '-',
                liveSearch: true,
                dropdownAlignRight: true,
                selectOnTab: true,
                maxOptions: 1
            });
            $('[data-toggle="tooltip"]').tooltip();
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

        function reqEdit(res_code, userid) {
            //console.log(res_code)
            //console.log(userid)
            //alert('tttttttt')
            event.preventDefault();
            event.stopPropagation();
            Swal.fire({
                input: 'textarea',
                inputLabel: 'กรอกวัตถุประสงค์ในการแก้ไข',
                inputPlaceholder: 'ใส่ข้อความ . . .',
                inputAttributes: {
                    'aria-label': 'ใส่ข้อความ.'
                },
                preConfirm: (value) => {
                    if (!value) {
                        //if (!document.getElementById('swal2-input').value) {
                        // Handle return value

                        Swal.showValidationMessage('กรุณาใส่รายละเอียด')
                    }
                },
                showCancelButton: true
            }).then((result) => {
                console.log(result.value);

                if (result.isConfirmed) {
                    var params = "{'res_code': '" + res_code + "','note': '" + result.value + "','userid': '" + userid + "'}";
                    $.ajax({
                        type: "POST",
                        url: "../KPI/KPIsSummaryList.aspx/reqEdit",
                        async: true,
                        data: params,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            /*alertSuccessToast();*/
                            if (msg.d == 'success') {
                                //alert(elemenmt.textContent);
                                window.location.href = location.href;
                            }

                        },
                        error: function () {
                            event.preventDefault();
                            event.stopPropagation();
                        }
                    });
                }
            })

            return false;

        }

        function reqDel(res_code, userid) {
            event.preventDefault();
            event.stopPropagation();
            Swal.fire({
                input: 'textarea',
                inputLabel: 'กรอกวัตถุประสงค์ในการลบรายการ',
                inputPlaceholder: 'ใส่ข้อความ . . .',
                inputAttributes: {
                    'aria-label': 'ใส่ข้อความ.'
                },
                customClass: {
                    inputLabel: "text-danger",
                },
                preConfirm: (value) => {
                    if (!value) {

                        Swal.showValidationMessage('กรุณาใส่รายละเอียด')
                    }
                },
                showCancelButton: true
            }).then((result) => {
                console.log(result.value);

                if (result.isConfirmed) {
                    var params = "{'res_code': '" + res_code + "','note': '" + result.value + "','userid': " + userid + "}";
                    console.log(params);

                    $.ajax({
                        type: "POST",
                        url: "../KPI/KPIsSummaryList.aspx/reqDel",
                        async: true,
                        data: params,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            /*alertSuccessToast();*/
                            console.log("2222222222");
                            if (msg.d == 'success') {
                                //alert(elemenmt.textContent);
                                window.location.href = location.href;
                            }

                        },
                        error: function () {
                            event.preventDefault();
                            event.stopPropagation();
                        }
                    });
                }
            })

            return false;

        }

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
