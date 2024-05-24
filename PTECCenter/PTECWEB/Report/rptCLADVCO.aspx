<%@ Page Title="rptCLADVCO" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="rptCLADVCO.aspx.vb" Inherits="PTECCENTER.rptCLADVCO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
    <style>
        th {
            text-align: center;
            background-color: #cbf4f0;
        }
        th a {
            color:black;
        }
        .checked {
            background-color: #ececec;
            opacity: 1 !important;
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
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">Report CLADV TO D365 <% If operator_code.IndexOf(Session("usercode").ToString) > -1 Then%> (Operator) <% End If %>
                    </li>
                </ol>

                <div class="row">
                    <div class="col-auto mb-3">

                        <asp:Button ID="btnSearch" class="btn btn-sm  btn-success" runat="server" Text="Search" />&nbsp;
                            <asp:Button ID="btnClear" class="btn btn-sm  btn-secondary" runat="server" Text="Clear" />&nbsp;
                            <asp:Button ID="btnExport" class="btn btn-sm  btn-info" runat="server" Text="Export" />&nbsp;
                            <asp:Button ID="btnCLADVTOD365" class="btn btn-sm  btn-info" runat="server" Text="Export TO D365" OnClientClick="sendID();" />
                        &nbsp;   

                    </div>
                    <div class="col">
                        <div class="form-group pl-5">
                            <input class="form-check-input chk-img-after" type="checkbox" id="chkGroupVAT" runat="server">
                            <asp:Label ID="Label1" CssClass="form-check-label" AssociatedControlID="chkGroupVAT" runat="server" Text="รวบ VAT" />
                        </div>
                        <div class="form-group pl-5">
                            <input class="form-check-input chk-img-after" type="checkbox" id="chkGroupVendor" runat="server">
                            <asp:Label ID="Label2" CssClass="form-check-label" AssociatedControlID="chkGroupVendor" runat="server" Text="รวบ Vendor" />
                        </div>
                    </div>
                </div>


                <div class="row jobtype">
                    <div class="col-auto mb-3" style="margin-left: auto;">
                        <input class="form-check-input chk-img-after" type="checkbox" id="chkCO" name="pay[1][]" runat="server">
                        <asp:Label ID="lbchkCO" CssClass="form-check-label" AssociatedControlID="chkCO" runat="server" Text="CO" />
                    </div>
                    <div class="col-auto mb-3">
                        <input class="form-check-input chk-img-after" type="checkbox" id="chkHO" name="pay[1][]" runat="server">
                        <asp:Label ID="lbchkHO" CssClass="form-check-label" AssociatedControlID="chkHO" runat="server" Text="HO" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 mb-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Code</span>
                            </div>
                            <asp:TextBox class="form-control noEnterSubmit" ID="txtclearadv" runat="server" placeholder="21XXXXXXX" AutoPostBack="false" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3 mb-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Code_ref</span>
                            </div>
                            <asp:TextBox class="form-control noEnterSubmit" ID="txtcoderef" runat="server" placeholder="21XXXXXXX" AutoPostBack="false" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3 mb-3">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">สถานะงานย่อย</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboStatusFollow" runat="server" AutoPostBack="false"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-3 mb-3">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">สังกัด</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboCompany" runat="server" AutoPostBack="false"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3 mb-3">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ตั้งแต่วันที่</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtStartDate" name="txtStartDate" runat="server" placeholder="--- คลิกเพื่อเลือก ---" AutoPostBack="false" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3 mb-3">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">จนถึง</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtEndDate" name="txtEndDate" runat="server" placeholder="--- คลิกเพื่อเลือก ---" AutoPostBack="false" autocomplete="off"></asp:TextBox>

                        </div>
                    </div>
                    <% If operator_code.IndexOf(Session("usercode").ToString) > -1 Then%>
                    <div class="col-md-3 mb-3">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ผู้เบิก</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboCreateby" runat="server" readonly="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3 mb-3">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ผู้รับเงิน</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboOwner" runat="server" readonly="true"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-3 mb-3 HO">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ฝ่าย</span>
                            </div>
                            <asp:DropDownList ID="cboDepartment" class="form-control" runat="server" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3 mb-3 HO">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">แผนก</span>
                            </div>
                            <asp:DropDownList ID="cboSection" class="form-control" runat="server" AutoPostBack="false"></asp:DropDownList>
                        </div>
                    </div>
                    <% End If %>
                    <% If Not Session("positionid") = "10" Then %>
                    <div class="col-md-3 mb-3 CO">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ประเภทสาขา</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboBranchGroup" runat="server" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3 mb-3 CO">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">สาขา</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboBranch" runat="server" AutoPostBack="false"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3 mb-3 CO">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">หัวข้อ</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboApproval" runat="server" AutoPostBack="false"></asp:DropDownList>
                        </div>
                    </div>
                    <% End If %>
                </div>

                <div class="row" style="padding-top: 1rem;">
                    <div class="col-md-12 HO">
                        <asp:Label ID="note" CssClass="text-danger text-right" runat="server" Text="( เงื่อนไข : รายการใน 'แผนก' จะเปลี่ยนไปตาม 'ฝ่าย' ที่เลือก )" />
                    </div>
                    <div class="col-md-12 CO">
                        <asp:Label ID="note2" CssClass="text-danger text-right" runat="server" Text="( เงื่อนไข : รายการใน 'สาขา' จะเปลี่ยนไปตาม 'ประเภทสาขา' ที่เลือก )" />
                    </div>
                </div>
                <div class="card-body">
                    <div class="row justify-content-end">
                        <div class="col-auto">
                            <asp:DropDownList ID="cboMaxRows" class="form-control" runat="server">
                                <asp:ListItem Value="50">50 รายการ</asp:ListItem>
                                <asp:ListItem Value="100">100 รายการ</asp:ListItem>
                                <asp:ListItem Value="1000">1,000 รายการ</asp:ListItem>
                                <asp:ListItem Value="2147483647">รายการทั้งหมด</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="table-responsive overflow-auto" style="font-size: 0.9rem">
                        <asp:GridView ID="gvRemind"
                            class="table table-hover table-bordered"
                            AllowSorting="true"
                            AutoGenerateColumns="false"
                            EmptyDataText="No data available."
                            PageSize="20"
                            AllowPaging="false"
                            runat="server">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-CssClass="text-center table-header" ItemStyle-Width="50px" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server"
                                            onclick="checkAll(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" data-key='<%#Eval("NonPO_Code").ToString%>'
                                            onclick="Check_Click(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="comcode" HeaderText="สังกัด" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbcomcode" runat="server" Text='<%#Eval("comcode")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="NonPO_Code" HeaderText="เลขใบงาน" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcode" runat="server" Text='<%#Eval("NonPO_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Code_ref" HeaderText="Code_ref" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCode_ref" runat="server" Text='<%#Eval("Code_ref")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="CreateBy" HeaderText="ผู้เบิก" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbCreateBy" runat="server" Text='<%#Eval("CreateBy")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="ownername" HeaderText="ผู้รับเงิน" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbownername" runat="server" Text='<%#Eval("ownername")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="CreateDate" HeaderText="วันที่ทำรายการ" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbCreateDate" runat="server" Text='<%#Eval("CreateDate")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="approvalname" HeaderText="หัวข้อ">
                                    <ItemTemplate>
                                        <asp:Label ID="lbapprovalname" runat="server" Text='<%#Eval("approvalname")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="detail" HeaderText="รายละเอียด">
                                    <ItemTemplate>
                                        <asp:Label ID="lbdetail" runat="server" Text='<%#Eval("detail")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="owner_permission" HeaderText="ผู้มีสิทธิอนุมัติ" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbowner_permission" runat="server" Text='<%#Eval("owner_permission")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="StatusNonPO" HeaderText="สถานะ" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbStatusNonPO" runat="server" Text='<%#Eval("StatusNonPO")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <%--<h4>ทั้งหมด <% =cntdt%> รายการ</h4>--%>

                </div>

            </div>
            <!-- /.container-fluid -->


        </div>
    </div>

    <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/NonPO.js")%>"></script>
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
            checkCOorHO();

            checkSelected()
        });

        $(".jobtype input:checkbox").on('click', function () {
            // in the handler, 'this' refers to the box clicked on
            console.log(this);
            var $box = $(this);
            if ($box.is(":checked")) {
                // the name of the box is retrieved using the .attr() method
                // as it is assumed and expected to be immutable
                var group = ".jobtype input:checkbox";
                // the checked state of the group/box on the other hand will change
                // and the current value is retrieved using .prop() method
                $(group).prop("checked", false);
                $box.prop("checked", true);

            } else {
                $box.prop("checked", false);
            }

            checkCOorHO();
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
        $('.noEnterSubmit').keypress(function (e) {
            if (e.which == 13) return false;
            //or...
            if (e.which == 13) e.preventDefault();
        });
        $(".table tbody tr").click(function (e) {

            //if ($(e.target).is(':button')) return; //ignore when click on the checkbox
            //console.log(this)
            var $cb = $(this).find(':checkbox');
            if (!$(e.target).is(':checkbox')) {
                $cb.prop('checked', !$cb.is(':checked'));
            }
            $cb.is(':checked') ? $(this).addClass("checked") : $(this).removeClass("checked");
            Check_Click(this)
        });
        function Check_Click(objRef) {

            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode.parentNode;

            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            var headerCheckBox = inputList[0];
            var checked = true;
            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;
        }
        function checkAll(objRef) {
            let GridView = objRef.parentNode.parentNode.parentNode;
            let inputList = GridView.getElementsByTagName("input");
            for (let i = 0; i < inputList.length; i++) {
                let row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                        inputList[i].parentNode.parentNode.parentNode.classList.add("checked");

                    }
                    else {
                        /*if (row.rowIndex % 2 == 0) {
                            row.style.backgroundColor = "#C2D69B";
                        }
                        else {
                            row.style.backgroundColor = "white";
                        }*/
                        inputList[i].checked = false;
                        inputList[i].parentNode.parentNode.parentNode.classList.remove("checked");

                    }
                    //$cb.is(':checked') ? $(this).css('background-color', '#ececec') : $(this).css('background-color', '#ffffff');
                }
            }
        }
        function checkSelected() {
            let inputList = $(".table tbody tr").find(':checkbox');
            for (let i = 0; i < inputList.length; i++) {
                if (inputList[i].type == "checkbox") {
                    if (inputList[i].checked) {
                        inputList[i].parentNode.parentNode.parentNode.classList.add("checked");

                    }
                    else {
                        inputList[i].parentNode.parentNode.parentNode.classList.remove("checked");

                    }
                    //$cb.is(':checked') ? $(this).css('background-color', '#ececec') : $(this).css('background-color', '#ffffff');
                }
            }
        }
        function getSeleted() {
            //console.log("xxx22");
            let textinputs = document.querySelectorAll('td input:checked');

            //console.log(arrs);
            let arrs = [];
            for (let i = 0; i < textinputs.length; i++) {
                arrs[i] = textinputs[i].parentNode.getAttribute("data-key");

                //console.log(textinputs[i].parentNode);
                //console.log(textinputs[i].parentNode.getAttribute("data-key"));
            }
            //console.log(arrs);

            let arrsWithKey = arrs.map((arr) => {
                let fullname = `{"code":"${arr}"}`;
                return fullname;
            })
            //console.log(`arrsWithKey : ${arrsWithKey}`);
            //console.log(arrsWithKey);
            let params = arrsWithKey.reduce((txt, array) => {
                return txt + array + ',';
            }, "");

            let paramslength = params.length;
            if (params[paramslength - 1] === ',') {
                //console.log(`params sdad`);
                params = params.substring(0, params.length - 1);
            }
            params = `[${params}]`
            //console.log(params);
            return params;
        }
        function sendID() {
            //console.log("xxx");
            let textinputs = document.querySelectorAll('td input:checked');
            //console.log(textinputs);
            const params = getSeleted();
            const sizeText = textinputs.length;
            //console.log("xxx");
            console.log(params);
            //console.log(textinputs.length);

            //removeElem("delete_value");

            removeElem("export_value");
            let confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "export_value";
            if (textinputs.length > 0) {
                if (confirm(`Export จาก (${sizeText}) รายการที่เลือกหรือไม่ ?`)) {
                    confirm_value.value = params;

                } else {
                    event.preventDefault();
                    event.stopPropagation();
                }

            }
            else {
                alert("กรุณาเลือกรายการก่อน");
                event.preventDefault();
                event.stopPropagation();
            }
            document.forms[0].appendChild(confirm_value);
            //console.log(confirm_value.value);
            return true;
        }
    </script>

</asp:Content>
