<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="Master.aspx.vb" Inherits="PTECCENTER.frmMasters" %>

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
                    <li class="breadcrumb-item">MASTER
                    </li>
                </ol>
                <div class="row align-items-start">
                    <div class="col-12 align-items-start">
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-danger" runat="server" Text="New" />
                        &nbsp;
                        <asp:Button ID="btnSave" class="btn btn-success btn-sm" runat="server" Text="Save" OnClientClick="Confirm();" />
                        &nbsp;              
                        <asp:Button ID="btnSearch" class="btn btn-sm  btn-primary" runat="server" Text="Search" />
                    </div>
                </div>
                <br />

                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">รหัสกลุ่มหมวดงาน</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtGroupID" name="txtGroupID" runat="server" required></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ชื่อหมวดงาน</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtJobsGroupName" runat="server" required></asp:TextBox>

                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">เลขหมวดงาน</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtGroupNumber" runat="server" required></asp:TextBox>
                        </div>
                    </div>
                </div>

                <br />
                <% If objStatus = "update" Then %>
                <div class="row">


                    <div class="col-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">UpdateUser</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtUpdateUser" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">UpdateDate</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtUpdateDate" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">CreateUser</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtCreateUser" name="txtGroupID" runat="server" ReadOnly="true"></asp:TextBox>

                        </div>
                    </div>
                    <div class="col-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">CreateDate</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtCreateDate" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <% End if %>

                <div class="card">
                    <div class="card-header" style="background-color: navy; color: white">
                        <i class="fas fa-table"></i>
                        รายละเอียด
                    </div>
                    <div class="card-body">

                        <div class="row justify-content-md-center">

                            <div class="col-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvJobsGroup"
                                        class="table table-striped table-bordered"
                                        AutoGenerateColumns="false"
                                        EmptyDataText="No data available."
                                        PageSize="20"
                                        AllowPaging="true"
                                        runat="server">
                                        <Columns>
                                            <asp:TemplateField HeaderText="GroupID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGroupID" runat="server" Text='<%#Eval("GroupID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GroupDescription">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGroupDescription" runat="server" Text='<%#Eval("GroupDescription")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GroupNumber">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGroupNumber" runat="server" Text='<%#Eval("GroupNumber")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <div class="d-flex justify-content-center">
                                                        <asp:Label ID="lblEdit" runat="server" Text=''>
                                                            <button runat="server" id="btnEdit" name="btnEdit" onclick="return confirmEdit(this);" class="btn btn-primary btn-sm" customdata='<%#Eval("GroupID")%>'>
                                                                <i class="fa fa-edit"></i>Edit</a>
                                                            </button>
                                                        </asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <div class="d-flex justify-content-center">
                                                        <asp:Label ID="lblDelete" runat="server" Text=''>
                                                            <button runat="server" id="btnDelete" name="btnDelete" onclick="return confirmDelete(this);" class="btn btn-danger btn-sm" customdata='<%#Eval("GroupID")%>'>
                                                                <i class="fas fa-trash-alt"></i>Delete
                                                            </button>
                                                        </asp:Label>
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
        </div>
    </div>

    <script type="text/javascript">
        function Confirm() {

            validateData();
            console.log("insave");
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("คุณต้องการจะบันทึกหรือไม่ ?")) {
                confirm_value.value = "Yes";
            }
            else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
            return true;
        }
    </script>

    <script type="text/javascript">
        function alertSuccess() {
            Swal.fire(
                'สำเร็จ',
                '',
                'success'
            )
        }
    </script>
    <script type="text/javascript">
        function alertWarning(massage) {
            Swal.fire(
                massage,
                '',
                'warning'
            )
        }
    </script>
    <script type="text/javascript">
        function confirmDelete(lnk) {
            var row = lnk.parentNode.parentNode.parentNode.parentNode;
            console.log(row.cells[0])
            console.log(row.rowIndex - 1)
            console.log(row)

            var GroupID = document.getElementsByName('btnDelete')[row.rowIndex - 1].getAttribute("customdata");
            console.log(GroupID)
            /*alert(GridView);*/

            Swal.fire({
                title: 'คุุณต้องการจะลบข้อมุลนี้ใช่หรือไม่ ?',
                text: "",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes'
            }).then((result) => {
                if (result.isConfirmed) {
                    var params = "{'groupId': '" + GroupID + "'}";
                    $.ajax({
                        type: "POST",
                        url: "../OPS/Master.aspx/deleteGroupById",
                        async: true,
                        data: params,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == 'success') {
                                swal.fire({
                                    title: "Deleted!",
                                    text: "",
                                    icon: "success"
                                }).then(function () {
                                    window.location.href = location.href;
                                });
                            } else {
                                alertWarning('fail')
                            }
                        },
                        error: function () {
                            alertWarning('fail')
                        }
                    });
                }
            })

            return false;
        }
    </script>
    <script type="text/javascript">
        function confirmEdit(lnk) {
            var row = lnk.parentNode.parentNode.parentNode.parentNode;
            console.log(row.cells[0])
            console.log(row.rowIndex - 1)
            console.log(row)

            var GroupID = document.getElementsByName('btnEdit')[row.rowIndex - 1].getAttribute("customdata");
            console.log(GroupID)
            //alert("edit");

            var params = "{'groupId': '" + GroupID + "'}";
            $.ajax({
                type: "POST",
                url: "../OPS/Master.aspx/updateGroupById",
                async: true,
                data: params,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    console.log(data)
                    console.log(data.d['GroupID'])
                    var obj = JSON.parse(data.d);
                    console.log(obj)
                    console.log(obj['GroupID'])

                    //alert("yes")
                },
                error: function () {
                    alertWarning('fail')
                }
            });

        }
    </script>



</asp:Content>

