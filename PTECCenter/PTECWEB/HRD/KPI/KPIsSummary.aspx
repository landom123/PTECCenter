﻿<%@ Page Title="KPIsSummary" Language="vb" AutoEventWireup="true" MasterPageFile="~/site.Master" CodeBehind="KPIsSummary.aspx.vb" Inherits="PTECCENTER.KPIsSummary" %>

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
        <div id="wrapper" class="h-100">

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
                                        <div class="card border-bottom" id="card__<%= formsTable.Rows(j).Item("KPIForm_ID").ToString() %>">
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
                                                                <i class="fas fa-cog"></i>&nbsp;แก้ไขแบบฟอร์ม (Admin) **ยังไม่เสร็จ
                                                            </button>
                                                        </div>
                                                        <div class="col-sm-12 col-md-auto">
                                                            <button class="btn btn-link btn-block text-left p-0 text-info" type="button" onclick="return dupForms(<%= formsTable.Rows(j).Item("KPIForm_ID").ToString() %>,'<%= Session("userid").ToString %>');">
                                                                <i class="far fa-copy"></i>&nbsp;คัดลอกแบบฟอร์ม (Admin) **ยังไม่เสร็จ
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


    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <CompositeScript>
            <Scripts>
                <asp:ScriptReference Path="~/datetimepicker/jquery.js" />
                <asp:ScriptReference Path="~/datetimepicker/build/jquery.datetimepicker.full.min.js" />
                <asp:ScriptReference Path="~/js/NonPO.js" />
            </Scripts>
        </CompositeScript>
    </asp:ScriptManager>
    <%--<script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/NonPO.js")%>"></script>--%>
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
                            Swal.showValidationMessage('กรุณากรอกข้อมูลให้ครบถ้วน')
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
                                        document.getElementById(`card__${formid}`)?.remove();


                                        // รีเซ็ต counter-reset ใหม่ใน CSS
                                        const container = document.querySelector('.container .accordion');
                                        if (container) {
                                            container.style.setProperty('counter-reset', 'rowNumber 0'); 

                                            // ปรับลำดับตัวเลขใหม่
                                            const cardHeaders = container.querySelectorAll('.card-header');
                                            cardHeaders.forEach((header, index) => {
                                                header.style.setProperty('counter-reset', `rowNumber ${index}`);
                                            });
                                        }
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
                title: 'กรอกข้อมูลแบบฟอร์มประเมินประจำปี',
                width: '90%',
                html: `
                    <div class="row mx-0">
                        <div class="col-12 mb-5 mb-md-3">
                            <div class="row">
                                <div class="col-md-4 text-left">
                                    <label for="title" class="font-weight-bold">หัวข้อ <span style="color: red;">*</span></label>
                                </div>
                                <div class="col-md-8">
                                    <textarea id="title" rows="3" class="form-control" placeholder="กรุณากรอก หัวข้อ แบบฟอร์ม" required></textarea>
                                    <div class="invalid-feedback">กรุณากรอก หัวข้อ แบบฟอร์ม</div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 mb-5 mb-md-3">
                            <div class="row">
                                <div class="col-md-4 mb-3 text-left">
                                    <span class="font-weight-bold">พนักงานประเมิน</span>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <div class="col-12 mb-3 text-left">
                                            <span>วันเริ่มต้น</span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 mb-3">
                                            <input type="datetime-local" id="ownerbegin_date" class="form-control">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <div class="col-12 mb-3 text-left">
                                            <span>วันเริ่มต้น</span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 mb-3">
                                            <input type="datetime-local" id="ownerend_date" class="form-control">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 mb-5 mb-md-3">
                            <div class="row">
                                <div class="col-md-4 mb-3 text-left">
                                    <span class="font-weight-bold">หัวหน้างานประเมิน</span>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <div class="col-12 mb-3 text-left">
                                            <span>วันเริ่มต้น</span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 mb-3">
                                            <input type="datetime-local" id="approvalbegin_date" class="form-control">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <div class="col-12 mb-3 text-left">
                                            <span>วันเริ่มต้น</span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12 mb-3">
                                            <input type="datetime-local" id="approvalend_date" class="form-control">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                        `,
                focusConfirm: false,
                showCancelButton: true,
                confirmButtonText: "Save",
                didOpen: () => {
                    const ownerBeginDate = document.getElementById('ownerbegin_date');
                    const ownerEndDate = document.getElementById('ownerend_date');

                    ownerBeginDate.addEventListener('change', function () {
                        ownerEndDate.setAttribute('min', this.value);
                    });

                    ownerEndDate.addEventListener('change', function () {
                        ownerBeginDate.setAttribute('max', this.value);
                    });

                    const approvalBeginDate = document.getElementById('approvalbegin_date');
                    const approvalEndDate = document.getElementById('approvalend_date');

                    approvalBeginDate.addEventListener('change', function () {
                        approvalEndDate.setAttribute('min', this.value);
                    });

                    approvalEndDate.addEventListener('change', function () {
                        approvalBeginDate.setAttribute('max', this.value);
                    });

                },
                preConfirm: () => {
                    const title = document.getElementById('title');

                    // รีเซ็ตการแสดง error ก่อน validate
                    title.classList.remove("is-invalid");

                    if (!title.value.trim()) {
                        title.classList.add("is-invalid");
                        Swal.showValidationMessage('กรุณากรอกข้อมูลให้ครบถ้วน');
                        return false;
                    }

                    return {
                        title: title.value.trim(),
                        ownerBeginDate: document.getElementById('ownerbegin_date').value,
                        ownerEndDate: document.getElementById('ownerend_date').value,
                        approvalBeginDate: document.getElementById('approvalbegin_date').value,
                        approvalEndDate: document.getElementById('approvalend_date').value
                    };
                }
            }).then((result) => {
                if (result.isConfirmed) {
                    const { title, ownerBeginDate, ownerEndDate, approvalBeginDate, approvalEndDate } = result.value;

                    const cboPeriod = document.getElementById('<%= cboPeriod.ClientID %>');
                    const selectedPeriod = cboPeriod.value; // ค่าที่ถูกเลือกใน cboPeriod

                    //console.log('Title:', title);
                    //console.log('Owner Begin Date:', ownerBeginDate);
                    //console.log('Owner End Date:', ownerEndDate);
                    //console.log('Approval Begin Date:', approvalBeginDate);
                    //console.log('Approval End Date:', approvalEndDate);
                    //console.log('Selected Period:', selectedPeriod);

                    // Validate ว่ามีค่า title และ selectedPeriod
                    if (!title || !selectedPeriod) {
                        alertWarning('Validate Fail');
                        return; // ไม่ส่ง request ถ้า validation fail
                    }

                    let params = JSON.stringify({
                        title: title,
                        ownerBeginDate: ownerBeginDate,
                        ownerEndDate: ownerEndDate,
                        approvalBeginDate: approvalBeginDate,
                        approvalEndDate: approvalEndDate,
                        selectedPeriod: selectedPeriod, // เพิ่มค่าจาก cboPeriod
                        updateby: user
                    });

                    //console.log('Params:', params);
                    fetch('../KPI/KPisSummary.aspx/addForms', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: params
                    }).then(response => response.json())
                        .then(data => {
                            //console.log('Response:', data);

                            if (data.d.success) {
                                swal.fire({
                                    title: "success!",
                                    text: "",
                                    icon: "success"
                                }).then(function () {
                                    window.location.href = location.href;
                                });
                            } else {
                                alertWarning('add fail');
                            }
                        })
                        .catch(error => {
                            console.error('Fetch Error:', error);
                            alertWarning('add fail');
                        });
                }

            });

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
                            Swal.showValidationMessage('กรุณากรอกข้อมูลให้ครบถ้วน')
                        }
                    },
                    showCancelButton: true
                }).then((result) => {
                    console.log(result.value);
                    if (result.isConfirmed) {
                        var params =
                            "{'formid': '" + formid + "'," +
                            "'message': '" + result.value + "'," +
                            "'updateby': '" + user + "'}";
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
        function parseDatetime(input) {
            if (!input) {
                return null; // หรือจะใช้ค่าเริ่มต้นอื่น เช่น new Date().toISOString()
            }

            const parts = input.split(" ");
            if (parts.length !== 2) return null;

            const dateParts = parts[0].split("/");
            if (dateParts.length !== 3) return null;

            const timeParts = parts[1].split(":");
            if (timeParts.length !== 2) return null;

            const [day, month, year] = dateParts.map(Number);
            const [hour, minute] = timeParts.map(Number);

            if (isNaN(day) || isNaN(month) || isNaN(year) || isNaN(hour) || isNaN(minute)) {
                return null;
            }

            const dateObj = new Date(year, month - 1, day, hour, minute);
            return dateObj.toISOString(); // แปลงเป็นรูปแบบ JSON ISO 8601
        }
    </script>
</asp:Content>
