<%@ Page Title="" Language="vb" AutoEventWireup="false"
    MasterPageFile="~/site.Master" CodeBehind="approvalHO.aspx.vb" Inherits="PTECCENTER.approvalHO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper">

        <!-- #include virtual ="/include/menu.inc" -->
        <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">
                <!-- Breadcrumbs-->
                <ol class="breadcrumb" style="background-color: deeppink; color: white">
                    <li class="breadcrumb-item">
                        <i class="fa fa-tasks" aria-hidden="true"></i>Import หักยอดขาย
                    </li>
                </ol>
                <p>
                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
                </p>
                <div class="row">
                    <div class="col-6">
                        <div class="input-group md8">
                            <asp:FileUpload ID="FileUpload1" class="btn btn-primary" runat="server" text="" />
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="input-group md4">
                            &nbsp;<asp:Button ID="btnOpen" class="btn btn-sm  btn-primary" runat="server" Text="Open" />&nbsp;
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12"><%= filename %></div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-header">
                                Featured
                            </div>
                            <div class="card-body">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvData"
                                        class="table table-striped table-bordered"
                                        AllowSorting="True"
                                        AllowPaging="false"
                                        AutoGenerateColumns="false"
                                        EmptyDataText="No data available."
                                        runat="server">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server"
                                                        onclick="checkAll(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" data-key='<%#Eval("id")%>'
                                                        onclick="Check_Click(this)" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbid" runat="server" Text='<%#Eval("id")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="branch">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbbranch" runat="server" Text='<%#Eval("branch")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                                </div>
                            </div>
                            <div class="card-footer text-muted">
                                <button type="button" class="btn btn-sm btn-primary" id="btnFromAddDetail" style="width: 35px" runat="server" title="Add" data-toggle="modal" data-target="#exampleModal" data-backdrop="static" data-keyboard="false" data-whatever="new">+</button>
                                <asp:Button ID="btnDelete" class="btn btn-sm  btn-danger" runat="server" Width="35px" Text="-" OnClientClick="return sendID();" title="Delete" />
                                <button type="button" class="btn btn-sm  btn-info noEnterSubmit" style="color: #495057;" title="Export" id="btnExport" onclick="sendID()"><i class="fas fa-file-download"></i></button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">

                    <a href="ApprovalMenuList.aspx" class="btn btn-sm btn-danger ">
                        <i class="fa fa-file-import"></i>
                    </a>
                    <a href="ApprovalMenuList.aspx" class="btn btn-sm btn-danger ">
                        <i class="fa fa-file-excel"></i>
                    </a>
                    <a href="ApprovalMenuList.aspx" class="btn btn-sm btn-danger ">
                        <i class="fa fa-file-export"></i>
                    </a>
                    <a href="ApprovalMenuList.aspx" class="btn btn-sm btn-danger ">
                        <i class="fa fa-file-download"></i>
                    </a>
                    <a href="ApprovalMenuList.aspx" class="btn btn-sm btn-danger ">
                        <i class="fa fa-file-upload"></i>
                    </a>
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
        <!-- end content-wrapper -->


        <!-- end เนื้อหาเว็บ -->


    </div>
    <!-- /#wrapper -->

    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">รายละเอียดรายการ</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <%--<button type="button" id="btnAddDetail" class="btn btn-primary noEnterSubmit">Save</button>--%>
                    <asp:Button ID="btnAddDetails" class="btn btn-primary" runat="server" Text="Save" OnClientClick="postBack_addDetail();" />

                </div>
            </div>
        </div>
    </div>

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
            var row = objRef.parentNode.parentNode;

            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
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
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        /*if (row.rowIndex % 2 == 0) {
                            row.style.backgroundColor = "#C2D69B";
                        }
                        else {
                            row.style.backgroundColor = "white";
                        }*/
                        inputList[i].checked = false;
                    }
                }
            }
        }
        function getSeleted() {
            //console.log("xxx22");

            var textinputs = document.querySelectorAll('td input:checked');

            //console.log(arrs);
            let arrs = [];
            for (let i = 0; i < textinputs.length; i++) {
                arrs[i] = textinputs[i].parentNode.getAttribute("data-key");

                //console.log(textinputs[i].parentNode);
                //console.log(textinputs[i].parentNode.getAttribute("data-key"));
            }
            //console.log(arrs);

            let arrsWithKey = arrs.map((arr) => {
                let fullname = `{"id":"${arr}"}`;
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

            const params = getSeleted();
            //console.log("xxx");
            //console.log(params);

            let elements = document.getElementsByName("confirm_value");
            //console.log(elements);
            //console.log("asdasd");
            for (let i = 0; i < elements.length; i++) {
                elements[i].remove();
            }

            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("คุณต้องการจะลบรายการที่เลือกหรือไม่ ?")) {
                confirm_value.value = params;
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
