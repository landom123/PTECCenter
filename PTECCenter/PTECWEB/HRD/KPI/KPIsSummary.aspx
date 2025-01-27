<%@ Page Title="KPIsSummary" Language="vb" AutoEventWireup="true" MasterPageFile="~/site.Master" CodeBehind="KPIsSummary.aspx.vb" Inherits="PTECCENTER.KPIsSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
    <link href="<%=Page.ResolveUrl("~/css/card_comment.css")%>" rel="stylesheet">
    <style>
        .container .accordion {
            counter-reset: rowNumber;
        }

        .container .card-header::before {
            display: table-cell;
            counter-increment: rowNumber;
            content: counter(rowNumber) ".";
            padding-right: 0.3em;
            text-align: right;
        }

        .container .collapse.show:before {
            position: absolute;
            top: 0;
            left: 0;
            height: -webkit-fill-available;
            width: 5px;
            background: #CF3339;
            content: "";
            display: block;
            border-top-left-radius: 3px;
            border-bottom-left-radius: 3px;
        }

        .tooltip-inner {
            max-width: 320px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg">
        <div id="wrapper">

            <!-- #include virtual ="/include/menu.inc" -->
            <!-- add side menu -->
            <div id="content-wrapper">

                <div class="container">
                    <div class="row">
                        <div class="col  align-self-center">
                            <span class="h3 font-weight-bold">ประเมินผลประจำปี</span>
                        </div>

                        <div class="col-auto text-right align-self-center">
                            <a href="KPIsSummaryList.aspx" class="btn btn-sm btn-danger ">
                                <i class="fa fa-tasks" aria-hidden="true"></i></a>
                        </div>

                    </div>
                    <hr />

                    <div class="card shadow-sm">
                        <div class="card-body">
                            <div class="row mb-5">
                                <div class="col-12 mb-3">
                                    <span class="ml-md-3">1. เลือกปีที่จะประเมิน</span>
                                </div>
                                <div class="col-12 mb-3">
                                    <% If adm_code.IndexOf(Session("usercode").ToString) > -1 Then%> (Admin) <% End If %>
                                    <asp:DropDownList ID="cboPeriod" class="form-control" runat="server" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-12 mb-3">
                                    <div class="d-flex justify-content-between">
                                        <span class="ml-md-3">2. เลือกทำแบบฟอร์มที่จะประเมิน</span>
                                        <% If adm_code.IndexOf(Session("usercode").ToString) > -1 Then%>
                                        <a class="text-info" onclick="return addForms('<%= Session("userid").ToString %>');" style="cursor: pointer; transition: .2s;">
                                            <i class="fas fa-plus-circle"></i><span>&nbsp;สร้างแบบฟอร์ม (Admin)</span>
                                        </a>
                                        <% End If %>
                                    </div>
                                </div>
                                <div class="col">
                                    <% If formsTable IsNot Nothing Then %>

                                    <% If formsTable.Rows.Count > 0 Then %>
                                    <div class="accordion" id="accordionExample">
                                        <% For j = 0 To formsTable.Rows.Count - 1 %>
                                        <div class="card border-bottom">
                                            <div class="card-header d-flex flex-row align-items-center" id="headingOne">
                                                <div class="mb-0 d-flex align-items-center">
                                                    <button class="btn btn-link btn-block text-left text-decoration-none btn__collapse" type="button" data-toggle="collapse" data-target="#collapse__<%= formsTable.Rows(j).Item("KPIForm_ID").ToString() %>" aria-expanded="false" aria-controls="collapse__<%= formsTable.Rows(j).Item("KPIForm_ID").ToString() %>">
                                                        <%= formsTable.Rows(j).Item("title").ToString() %>
                                                    </button>
                                                    <% If formsTable.Rows(j).Item("has_res") And formsTable.Rows(j).Item("status").ToString() = "draft" Then%>
                                                    <i class="far fa-clock text-warning"></i>
                                                    <% Else If formsTable.Rows(j).Item("has_res") And (formsTable.Rows(j).Item("status").ToString() = "submitted" Or formsTable.Rows(j).Item("status").ToString() = "approved") Then%>
                                                    <i class="far fa-thumbs-up text-success"></i>
                                                    <% End If %>
                                                </div>
                                                <div class="ml-auto">
                                                    <span id="txtStatusPeriod" class="badge <%= If(formsTable.Rows(j).Item("in_OwnerPeriod"), "badge-success", "badge-danger") %>">
                                                        <%= If(formsTable.Rows(j).Item("in_OwnerPeriod"), "อยู่ในช่วงประเมิน", "นอกช่วงประเมิน") %>
                                                    </span>
                                                    <span class="text-muted" data-toggle="tooltip" data-placement="top" data-html="true"
                                                        title="<span>พนักงาน :<%= formsTable.Rows(j).Item("ownerbegin_date").ToString() & " - " & formsTable.Rows(j).Item("ownerend_date").ToString() %></span><br /><span>หัวหน้า : <%= formsTable.Rows(j).Item("approvalbegin_date").ToString() & " - " & formsTable.Rows(j).Item("approvalend_date").ToString() %></span>">
                                                        <i class="fas fa-info-circle"></i></span>
                                                </div>
                                            </div>

                                            <div id="collapse__<%= formsTable.Rows(j).Item("KPIForm_ID").ToString() %>" class="collapse" aria-labelledby="headingOne" data-parent="#accordionExample">
                                                <div class="card-body">
                                                    <div class="row align-items-center">
                                                        <div class="col-sm-12 col-md-auto ml-md-4">
                                                            <% If formsTable.Rows(j).Item("has_res") And formsTable.Rows(j).Item("in_OwnerPeriod") And formsTable.Rows(j).Item("has_Permission") And formsTable.Rows(j).Item("status").ToString() = "draft" Then%>
                                                            <a href="./KPIsForm.aspx?f=<%= formsTable.Rows(j).Item("KPIForm_ID").ToString() %>" class="text-warning"><i class="far fa-edit"></i>&nbsp;ทำแบบฟอร์ม..(ต่อ)</a>
                                                            <% Else If formsTable.Rows(j).Item("has_res") And (formsTable.Rows(j).Item("status").ToString() = "submitted" Or formsTable.Rows(j).Item("status").ToString() = "approved") Then%>
                                                            <a href="./KPIsForm.aspx?f=<%= formsTable.Rows(j).Item("KPIForm_ID").ToString() %>&ow=<%=Session("userid") %>" class="badge badge-success"><i class="far fa-thumbs-up"></i>&nbsp;ทำรายการเสร็จสิ้น "คลิก" เพื่อดูรายละเอียด</a>
                                                            <% Else If formsTable.Rows(j).Item("in_OwnerPeriod") And formsTable.Rows(j).Item("has_Permission") Then %>
                                                            <a href="./KPIsForm.aspx?f=<%= formsTable.Rows(j).Item("KPIForm_ID").ToString() %>"><i class="far fa-edit"></i>&nbsp;ทำแบบฟอร์ม</a>
                                                            <% End If %>
                                                        </div>
                                                        <% If formsTable.Rows(j).Item("has_res") And formsTable.Rows(j).Item("in_OwnerPeriod") And formsTable.Rows(j).Item("has_Permission") And formsTable.Rows(j).Item("status").ToString() = "draft" Then%>
                                                        <div class="col-sm-12 col-md-auto">
                                                            <a href="#" onclick="return __doPostBack('delRes',<%=formsTable.Rows(j).Item("KPIForm_ID") %>);"><i class="fas fa-times text-danger"></i>&nbsp;ลบแบบฟอร์มที่เคยทำ</a>
                                                        </div>
                                                        <% End If %>
                                                        <% If adm_code.IndexOf(Session("usercode").ToString) > -1 Then%>
                                                        <div class="col-sm-12 col-md-auto ">
                                                            <button class="btn btn-link btn-block text-left p-0 text-info" type="button" onclick="">
                                                                <i class="fas fa-cog"></i>&nbsp;แก้ไขแบบฟอร์ม (Admin)
                                                            </button>
                                                        </div>
                                                        <div class="col-sm-12 col-md-auto">
                                                            <button class="btn btn-link btn-block text-left p-0 text-info" type="button" onclick="return dupForms(<%= formsTable.Rows(j).Item("KPIForm_ID").ToString() %>,'<%= Session("userid").ToString %>');">
                                                                <i class="far fa-copy"></i>&nbsp;คัดลอกแบบฟอร์ม (Admin)
                                                            </button>
                                                        </div>
                                                        <div class="col-sm-12 col-md-auto">
                                                            <button class="btn btn-link btn-block text-left p-0 text-danger" type="button" onclick="return deleteForms(<%= formsTable.Rows(j).Item("KPIForm_ID").ToString() %>,'<%= Session("userid").ToString %>');">
                                                                <i class="far fa-trash-alt"></i>&nbsp;ลบแบบฟอร์ม (Admin)
                                                            </button>
                                                        </div>
                                                        <% End If %>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                        <% Next j %>
                                    </div>

                                    <% Else %>

                                    <div class="bg-light p-2 align-items-center d-flex"><span class="ml-lg-3">ยังไม่มีแบบฟอร์มให้ประเมิน</span></div>

                                    <% End If %>
                                    <% End If %>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="<%=Page.ResolveUrl("~/default.aspx")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/NonPO.js")%>"></script>
    <script>

        $(document).ready(function () {
            $('.form-control').selectpicker({
                noneSelectedText: '-',
                liveSearch: true,
                maxOptions: 1
            });
            $('.form-control').selectpicker('refresh');


            $('[data-toggle="tooltip"]').tooltip();
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
        function deleteForms(formid, user) {
            if (formid > 0) {
                Swal.fire({
                    input: 'textarea',
                    inputLabel: 'กรุณาใส่เหตุผลในการยกเลิก',
                    inputPlaceholder: 'ใส่ข้อความ . . .',
                    inputAttributes: {
                        'aria-label': 'ใส่ข้อความ.'
                    },
                    customClass: {
                        inputLabel: "text-danger",
                    },
                    preConfirm: (value) => {
                        if (!value) {
                            Swal.showValidationMessage('input missing')
                        }
                    },
                    showCancelButton: true
                }).then((result) => {
                    console.log(result.value);
                    if (result.isConfirmed) {
                        var params = "{'formid': '" + formid + "','message': '" + result.value + "','updateby': '" + user + "'}";
                        console.log(params);
                        $.ajax({
                            type: "POST",
                            url: "../KPI/KPisSummary.aspx/CancelByCode",
                            async: true,
                            data: params,
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                console.log(msg.d)
                                if (msg.d) {
                                    swal.fire({
                                        title: "success!",
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
            }

            return false;
        }


        function addForms(user) {
            Swal.fire({
                input: 'textarea',
                inputLabel: 'กรุณากรอกชื่อแบบฟอร์ม',
                inputPlaceholder: 'ใส่ข้อความ . . .',
                inputAttributes: {
                    'aria-label': 'ใส่ข้อความ.'
                },
                preConfirm: (value) => {
                    if (!value) {
                        Swal.showValidationMessage('input missing')
                    }
                },
                showCancelButton: true
            }).then((result) => {
                console.log(result.value);
                if (result.isConfirmed) {
                    var params = "{'formid': '" + formid + "','message': '" + result.value + "','updateby': '" + user + "'}";
                    console.log(params);
                    $.ajax({
                        type: "POST",
                        url: "../KPI/KPisSummary.aspx/addForms",
                        async: true,
                        data: params,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            console.log(msg.d)
                            if (msg.d) {
                                swal.fire({
                                    title: "success!",
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
        function dupForms(formid, user) {
            if (formid > 0) {
                Swal.fire({
                    input: 'textarea',
                    inputLabel: 'กรุณากรอกชื่อแบบฟอร์ม',
                    inputPlaceholder: 'ใส่ข้อความ . . .',
                    inputAttributes: {
                        'aria-label': 'ใส่ข้อความ.'
                    },
                    preConfirm: (value) => {
                        if (!value) {
                            Swal.showValidationMessage('input missing')
                        }
                    },
                    showCancelButton: true
                }).then((result) => {
                    console.log(result.value);
                    if (result.isConfirmed) {
                        var params = "{'formid': '" + formid + "','message': '" + result.value + "','updateby': '" + user + "'}";
                        console.log(params);
                        $.ajax({
                            type: "POST",
                            url: "../KPI/KPisSummary.aspx/dupForms",
                            async: true,
                            data: params,
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                console.log(msg.d)
                                if (msg.d) {
                                    swal.fire({
                                        title: "success!",
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
            }

            return false;
        }
    </script>
</asp:Content>
