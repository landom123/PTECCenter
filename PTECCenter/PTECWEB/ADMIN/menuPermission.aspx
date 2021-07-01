<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="menuPermission.aspx.vb" Inherits="PTECCENTER.menuPermission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Page.ResolveUrl("~/css/checkbox.css")%>" rel="stylesheet">

    <style>
        .table th, .table td {
            padding-left: 2rem;
            padding-right: 2rem;
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

                <!-- Breadcrumbs-->
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">กำหนดสิทธิ์การใช้งานเมนูต่างๆ : <% =Session("cbouser") %></a>
                    </li>
                </ol>

                <div class="container">
                    <div class="row">
                        <div class="col-md-4">
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
                            <%  Dim temp As String = "" %>
                            <%  Dim cnt_child As Integer = 0 %>
                            <% For i = 0 To menuAlltable.Tables(0).Rows.Count - 1 %>
                            <% If not menuAlltable.Tables(0).Rows(i).Item("parent_id").ToString = temp Then %>
                            <div class="row">
                                <div class="col">
                                    <h4 class="mt-4"><% =menuAlltable.Tables(0).Rows(i).Item("menu_name") %></h4>
                                </div>
                                <div class="col text-right align-items-end">
                                    <button type="button" class="btn btn-sm  btn-info btnparent" style="margin-top: 1.5rem !important;" onclick="AllowParent('<% =menuAlltable.Tables(0).Rows(i).Item("parent_id") %>','')">
                                        เปิดทั้งหมด : <% =menuAlltable.Tables(0).Rows(i).Item("menu_name") %>
                                    </button>
                                    <button type="button" class="btn btn-sm  btn-danger btnparent" style="margin-top: 1.5rem !important;" onclick="DisableParent('<% =menuAlltable.Tables(0).Rows(i).Item("parent_id") %>')">
                                        ปิดทั้งหมด : <% =menuAlltable.Tables(0).Rows(i).Item("menu_name") %>
                                    </button>
                                </div>
                            </div>
                            <div class="card shadow">
                                <div class="table-responsive">
                                    <table class="table table-hover">
                                        <tbody>
                                            <% cnt_child = 0 %>

                                            <% else %>
                                            <tr>
                                                <td>
                                                    <% =menuAlltable.Tables(0).Rows(i).Item("menu_name") %>
                                                </td>
                                                <td class="text-right">

                                                    <% If menuAlltable.Tables(0).Rows(i).Item("parent") = 0 Then %>
                                                    <input id="<% =menuAlltable.Tables(0).Rows(i).Item("menuid") %>" type="checkbox" onclick="allowOrDisMenu(this);"><label for="<% =menuAlltable.Tables(0).Rows(i).Item("menuid") %>"></label>
                                                    <% End if %>
                                                </td>
                                            </tr>
                                            <% cnt_child = cnt_child + 1 %>
                                            <% If cnt_child = menuAlltable.Tables(1).Rows(menuAlltable.Tables(1).Rows.IndexOf(menuAlltable.Tables(1).Select("parent_id = '" + menuAlltable.Tables(0).Rows(i).Item("parent_id").ToString + "'")(0))).Item("cnt_child") Then %>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <% End if %>


                            <% End if %>
                            <% temp = menuAlltable.Tables(0).Rows(i).Item("parent_id").ToString %>
                            <% Next i %>

                            <%-- end item--%>
                        </div>
                    </div>
                </div>
                <!-- end jobdetail -->

                <!-- Sticky Footer -->
                <footer class="sticky-footer">
                    <div class="container my-auto">
                        <div class="copyright text-center my-auto">
                            <span>Copyright © Your Website 2019</span>
                        </div>
                    </div>
                </footer>
            </div>
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

            <% If Not menuUser Is Nothing Then %>
                <% For i = 0 To menuUser.Rows.Count - 1 %>
            $('#<%=menuUser.Rows(i).Item("menuid")%>').prop('checked', true);
                <% Next i %>
            <% End if %>
        });
    </script>

    <script>
        function AllowParent(parentid, menuid) {

            var userid = document.getElementById('<%= cboUser.ClientID%>');
            //alert('เปิด : ' + parentid + 'ss ' + menuid + 'ss ' + userid.value)

            var params = "{'userid': '" + userid.value + "','parentid': '" + parentid + "','menuid': '" + menuid + "'}";
            $.ajax({
                type: "POST",
                url: "../ADMIN/menupermission.aspx/addMenuPermission",
                async: true,
                data: params,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    //console.log(msg)
                    //console.log(msg.d['menuid'])
                    var obj = JSON.parse(msg.d);
                    //console.log(obj)
                    //console.log(obj[0]['menuid'])
                    for (let i = 0; i < obj.length; i++) {
                        $('#' + obj[i]['menuid']).prop('checked', true);
                    }
                },
                error: function () {
                    alertWarning('fail')
                }
            });
        }


        function DisableParent(parentid, menuid) {
            var userid = document.getElementById('<%= cboUser.ClientID%>');
            //alert('เปิด : ' + parentid + 'ss ' + menuid + 'ss ' + userid.value)

            var params = "{'userid': '" + userid.value + "','parentid': '" + parentid + "','menuid': '" + menuid + "'}";
            $.ajax({
                type: "POST",
                url: "../ADMIN/menupermission.aspx/removeMenuPermission",
                async: true,
                data: params,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    //console.log(msg)
                    //console.log(msg.d['menuid'])
                    var obj = JSON.parse(msg.d);
                    //console.log(obj)
                    //console.log(obj[0]['menuid'])
                    for (let i = 0; i < obj.length; i++) {
                        $('#' + obj[i]['menuid']).prop('checked', false);
                    }
                },
                error: function () {
                    alertWarning('fail')
                }
            });
        }

        function allowOrDisMenu(s) {
            console.log(s)
            console.log(s.id)

            event.preventDefault();
                var userid = document.getElementById('<%= cboUser.ClientID%>');
            if (s.checked == true) {

                var params = "{'userid': '" + userid.value + "','parentid': '','menuid': '" + s.id + "'}";
                $.ajax({
                    type: "POST",
                    url: "../ADMIN/menupermission.aspx/addMenuPermission",
                    async: true,
                    data: params,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //console.log(msg)
                        //console.log(msg.d['menuid'])
                        var obj = JSON.parse(msg.d);
                        //console.log(obj)
                        //console.log(obj[0]['menuid'])
                        for (let i = 0; i < obj.length; i++) {
                            $('#' + obj[i]['menuid']).prop('checked', true);
                        }
                    },
                    error: function () {
                        alertWarning('fail')
                    }
                });
            } else {
                var params = "{'userid': '" + userid.value + "','parentid': '','menuid': '" + s.id + "'}";
                $.ajax({
                    type: "POST",
                    url: "../ADMIN/menupermission.aspx/removeMenuPermission",
                    async: true,
                    data: params,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //console.log(msg)
                        //console.log(msg.d['menuid'])
                        var obj = JSON.parse(msg.d);
                        //console.log(obj)
                        //console.log(obj[0]['menuid'])
                        for (let i = 0; i < obj.length; i++) {
                            $('#' + obj[i]['menuid']).prop('checked', false);
                        }
                    },
                    error: function () {
                        alertWarning('fail')
                    }
                });

            }
        }
    </script>
</asp:Content>
