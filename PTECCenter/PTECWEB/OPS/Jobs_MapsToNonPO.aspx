<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="Jobs_MapsToNonPO.aspx.vb" Inherits="PTECCENTER.Jobs_MapsToNonPO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        html {
            background-color: #f0f2f5 !important;
        }

        #content-wrapper {
            height: 93vh;
        }


        .checked {
            background-color: #ececec;
            opacity: 1 !important;
        }

        .table-header {
            background-color: #cbf4f0;
            color: #f15a24;
            text-align: center;
        }

        .__show:after {
            content: '\f06e';
            font-family: 'Font Awesome 5 Free';
            font-weight: bold;
        }

        .__hide:after {
            content: '\f070';
            font-family: 'Font Awesome 5 Free';
            font-weight: bold;
        }

        .opacity-75 {
            opacity: 0.75;
        }

        .opacity-50 {
            opacity: 0.50;
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
                <div class="row">
                    <div class="col-12 mb-3">
                        <div class="card shadow-sm">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-auto mb-3">
                                        <asp:Button ID="btnSearch" class="btn btn-sm  btn-warning" runat="server" Text="ค้นหารายการ" />
                                        &nbsp;   
                                        <asp:Button ID="btnJTN" class="btn btn-sm  btn-secondary" runat="server" Text="ดูตัวอย่าง Payment" OnClientClick="sendID();" />
                                        &nbsp;   
                                        <asp:CheckBox  ID="chkvisible" runat="server" Text="ดูรายการทั้งหมด" AutoPostBack="true"></asp:CheckBox>
                                        &nbsp;   
                                    </div>
                                    
                                </div>
                                <div class="row">
                                    <div class="col-md-4 mb-3 ">
                                        <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Suppiler</span>
                                            </div>
                                            <asp:DropDownList class="form-control" ID="cboSupplier" runat="server" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4 mb-3">
                                        <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Code</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtjobcode" runat="server" placeholder="21XXXXXXX" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4 mb-3">
                                        <div class="input-group">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">สาขา</span>
                                            </div>
                                            <asp:DropDownList class="form-control" ID="cboBranch" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="table-responsive">
                                    <asp:GridView ID="gvRemind"
                                        class="table thead-dark table-bordered"
                                        AllowSorting="True"
                                        AllowPaging="false"
                                        AutoGenerateColumns="false"
                                        runat="server">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-CssClass="text-center table-header" ItemStyle-Width="50px" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server"
                                                        onclick="checkAll(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" data-key='<%#Eval("jobdetailid").ToString + "," + Eval("jobcode").ToString%>'
                                                        onclick="Check_Click(this)" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="id" HeaderStyle-CssClass="table-header d-none" ItemStyle-CssClass="d-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbid" runat="server" Text='<%#Eval("jobdetailid")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Job" HeaderStyle-CssClass="table-header " ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbjobcode" CssClass="d-none" runat="server" Text='<%#Eval("jobcode")%>'></asp:Label>
                                                    <a href="../OPS/jobs.aspx?jobno=<%#Eval("jobcode")%>" title="<%#Eval("jobcode")%>" target="_blank"><%#Eval("jobcode")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Branch" HeaderStyle-CssClass="table-header " ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbbranch" runat="server" Text='<%#Eval("branch")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" HeaderStyle-CssClass="table-header " ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbapproval" runat="server" Text='<%#Eval("jobtype")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Detail" HeaderStyle-CssClass="table-header ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbdetail" runat="server" Text='<%#Eval("details")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DetailPayment" HeaderStyle-CssClass="table-header ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbdetailpayment" runat="server" Text='<%#Eval("detailpayment")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cost" HeaderStyle-CssClass="table-header " ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbcost" runat="server" Text='<%#String.Format("{0:n2}", Eval("cost"))%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unlock" HeaderStyle-CssClass="table-header" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton type="Button" runat="server" CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="return ConfirmUnlock(this);">
                                                        <i class="fas fa-unlock-alt"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="table-header" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton type="Button" runat="server" CssClass='<%#Eval("visiblestatus")%>' CommandName="visible" CommandArgument="<%# Container.DataItemIndex %>">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    <!-- end content-wrapper -->


    <!-- end เนื้อหาเว็บ -->


    </div>
    <!-- /#wrapper -->
    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>
    <script type="text/javascript">
        //$(window).focus(function () {
        //    __doPostBack('searchCostCommited_list','focus')

        //});
        $(document).ready(function () {

            $('.form-control').selectpicker({
                //showTick: true,
                liveSearch: true,
                maxOptions: 1
            });

            checkSelected()
        });

    </script>
    <script type="text/javascript">

        function alertSuccess(massage) {
            Swal.fire(
                massage,
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
                const myArray = arr.split(",");
                let fullname = `{"id":"${myArray[0]}","code":"${myArray[1]}"}`;
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
        function ConfirmUnlock(elm) {

            if (confirm("คุณต้องการจะปลดล็อกหรือไม่ ?")) {
                return true;
            }
            else {
                event.preventDefault();
                event.stopPropagation();
            }
            return true;


        }
        function toggleEye(elm) {

            var x = elm.parentNode.parentNode;
            if (x.style.display === "none") {
                //x.style.display = "block";
                elm.classList.remove('__hide');
                elm.classList.add('__show');

            } else {
                //x.style.display = "none";
                x.classList.remove('checked');
                elm.classList.remove('__show');
                elm.classList.add('__hide');
            }

        }
        function Confirm() {

            console.log("insave");

            removeElem("confirm_value");
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("คุณต้องการจะบันทึกหรือไม่ ?")) {
                confirm_value.value = "Yes";
            }
            else {
                event.preventDefault();
                event.stopPropagation();
            }
            document.forms[0].appendChild(confirm_value);
            console.log(confirm_value.value);
            return true;
        }
        function sendID() {
            //console.log("xxx");
            let textinputs = document.querySelectorAll('td input:checked');
            console.log(textinputs);
            const params = getSeleted();
            const sizeText = textinputs.length;
            //console.log("xxx");
            console.log(textinputs.length);

            //removeElem("delete_value");

            let confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "delete_value";
            if (textinputs.length > 0) {
                if (confirm(`ดูตัวอย่างจาก (${sizeText}) รายการที่เลือกหรือไม่ ?`)) {
                    confirm_value.value = params;

                } else {
                    event.preventDefault();
                    event.stopPropagation();
                }

            }
            else {
                event.preventDefault();
                event.stopPropagation();
            }
            document.forms[0].appendChild(confirm_value);
            //console.log(confirm_value.value);
            return true;
        }

    </script>
</asp:Content>
