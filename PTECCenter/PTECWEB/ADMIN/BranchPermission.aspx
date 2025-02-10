<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="BranchPermission.aspx.vb" Inherits="PTECCENTER.BranchPermission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="<%=Page.ResolveUrl("~/css/checkbox.css")%>" rel="stylesheet">
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
                        <a href="#">กำหนดสิทธิ์การใช้งานสาขา : <% =Session("cbouser_branch") %></a>
                    </li>
                </ol>


                <div class="container">
                    <div class="row">
                        <div class="col-md-4 mb-3">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">User</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboUser" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <%--begin item--%>

                            <div class="table-responsive">
                                <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0">
                                    <thead>
                                        <tr>
                                            <th>Name
             
                                        <%--<input id="notify" type="checkbox" checked data-toggle="toggle" data-style="ios"></th>--%>
                                            <th>

                                                <% If Not maintable Is Nothing Then %>
                                                <button type="button" class="btn btn-sm  btn-primary btnparent"
                                                    onclick="AllowParent();">
                                                    Add All</button>
                                                <button type="button" class="btn btn-sm  btn-danger btnparent"
                                                    onclick="DisableParent();">
                                                    Remove All</button>
                                                <% End if %>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tfoot>
                                        <tr>
                                            <th>Name</th>
                                            <th></th>
                                        </tr>
                                    </tfoot>
                                    <tbody>
                                        <% For i = 0 To branchtable.Rows.Count - 1 %>
                                        <tr>
                                            <%--<td>
                                                <% =branchtable.Rows(i).Item("name") %>
                                            </td>
                                            <td>
                                                <% If branchtable.Rows(i).Item("selected") = False Then %>
                                                <button type="button" class="btn btn-sm  btn-primary"
                                                    onclick="location.href='../ADMIN/branchpermissionadd.aspx?branchid=<% =branchtable.Rows(i).Item("branchid") %> '">
                                                    Add</button>
                                                <% Else %>
                                                <button type="button" class="btn btn-sm  btn-danger"
                                                    onclick="location.href='../ADMIN/branchpermissionremove.aspx?branchid=<% =branchtable.Rows(i).Item("branchid") %> '">
                                                    Remove</button>
                                                <% End if %>
                                            </td>--%>
                                            <td>
                                                <% =branchtable.Rows(i).Item("name") %>
                                            </td>
                                            <td class="text-right">
                                                <input id="<% =branchtable.Rows(i).Item("branchid") %>" type="checkbox" onclick="allowOrDisBranch(this);"><label for="<% =branchtable.Rows(i).Item("branchid") %>"></label>
                                            </td>




                                        </tr>
                                        <%-- end detail row--%>
                                        <% Next i %>
                                    </tbody>
                                </table>
                            </div>

                        </div>
                    </div>
                </div>

                <%-- end item--%>
            </div>
            <!-- end jobdetail -->





            <!-- Sticky Footer -->
            <footer class="sticky-footer d-none">
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

    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $('.form-control').selectpicker({
                liveSearch: true,
                maxOptions: 1
            });
            var confirm_value = document.createElement("cboUser");

            var cboUser = document.getElementById('<%= cboUser.ClientID%>');
            if (cboUser.value == 0) {
                $("input[type='checkbox']").prop({
                    disabled: true
                });

                $(".btnparent").hide();
            }

            <% If Not branchUser Is Nothing Then %>
                <% For i = 0 To branchUser.Rows.Count - 1 %>
            $('#<%=branchUser.Rows(i).Item("branchid")%>').prop('checked', true);
                <% Next i %>
            <% End if %>
        });


    </script>

    <script>
        function AllowParent() {
            <% If Not maintable Is Nothing Then %>

            var params = "{'usercode': '<% =maintable.Rows(0).Item("UserCode").ToString %>','branchid': 'all'}";
            $.ajax({
                type: "POST",
                url: "../ADMIN/branchpermission.aspx/addBranchPermission",
                async: true,
                data: params,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    fetchData(msg);
                },
                error: function () {
                    alertWarning('fail')
                }
            });
            <% End if %>

        }


        function DisableParent() {
            <% If Not maintable Is Nothing Then %>

            var params = "{'usercode': '<% =maintable.Rows(0).Item("UserCode").ToString %>','branchid': 'all'}";
            $.ajax({
                type: "POST",
                url: "../ADMIN/branchpermission.aspx/removeBranchPermission",
                async: true,
                data: params,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    fetchData(msg);
                },
                error: function () {
                    alertWarning('fail')
                }
            });
            <% End if %>

        }

        function allowOrDisBranch(s) {
            console.log(s)
            console.log(s.id)

            event.preventDefault();

            <% If Not branchtable Is Nothing And Not maintable Is Nothing Then %>
            console.log(s.checked)

            if (s.checked == true) {

                var params = "{'usercode': '<% =maintable.Rows(0).Item("UserCode").ToString %>','branchid': '" + s.id + "'}";
                $.ajax({
                    type: "POST",
                    url: "../ADMIN/branchpermission.aspx/addBranchPermission",
                    async: true,
                    data: params,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        fetchData(msg);

                        ////console.log(msg)
                        //console.log(msg.d['branchid'] + '11111')
                        //var obj = JSON.parse(msg.d);
                        ////console.log(obj)
                        ////console.log(obj[0]['menuid'])
                        //for (let i = 0; i < obj.length; i++) {
                        //    $('#' + obj[i]['branchid']).prop('checked', true);
                        //}
                    },
                    error: function () {
                        alertWarning('fail')
                    }
                });
            } else {
                var params = "{'usercode': '<% =maintable.Rows(0).Item("UserCode").ToString %>','branchid': '" + s.id + "'}";
                $.ajax({
                    type: "POST",
                    url: "../ADMIN/branchpermission.aspx/removeBranchPermission",
                    async: true,
                    data: params,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        fetchData(msg);

                        ////console.log(msg)
                        ////console.log(msg.d['menuid'])
                        //var obj = JSON.parse(msg.d);
                        ////console.log(obj)
                        ////console.log(obj[0]['menuid'])
                        //for (let i = 0; i < obj.length; i++) {
                        //    $('#' + obj[i]['branchid']).prop('checked', false);
                        //}
                    },
                    error: function () {
                        alertWarning('fail')
                    }
                });

            }
            <% End if %>

        }


        function fetchData(msg) {
            //console.log(msg)
            console.log(msg.d['branchid'] + '11111')
            var obj = JSON.parse(msg.d);
            console.log(obj)
            //console.log(obj[0]['menuid'])
            for (let i = 0; i < obj.length; i++) {
                if (obj[i]['active']) {
                    $('#' + obj[i]['branchid']).prop('checked', true);
                } else {
                    $('#' + obj[i]['branchid']).prop('checked', false);
                }
            }
        }
    </script>
</asp:Content>
