<%@ Page Title="KPIsForm" Language="vb" AutoEventWireup="true" MasterPageFile="~/site.Master" CodeBehind="KPIsForm.aspx.vb" Inherits="PTECCENTER.KPIsForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
    <link href="<%=Page.ResolveUrl("~/css/Jobs.css")%>" rel="stylesheet">
    <style>
        .mw-20 {
            max-width: 20%;
        }

        .w-5 {
            width: 5% !important;
        }

        .w-10 {
            width: 10% !important;
        }

        .w-90 {
            width: 90% !important;
        }

        .w-95 {
            width: 95% !important;
        }

        .prewrap {
            white-space: pre-wrap;
        }
        /*
        .show__btnDelete .input-group .input-group-append > :first-of-type {
            background-color: yellow;
        }*/
        th, tr {
            text-align: center;
        }

        .fill_text {
            counter-reset: rowNumber 0;
        }

            .fill_text .input-group::before {
                counter-increment: rowNumber 1;
                content: counter(rowNumber) ". ";
                padding-right: 0.5em;
                text-align: right;
                align-content: center;
            }

        .project__table tbody {
            counter-reset: rowNumber;
        }

            /* ให้เพิ่มค่าของ Counter ในแต่ละแถว */
            .project__table tbody tr::before {
                counter-increment: rowNumber;
                content: counter(rowNumber);
                display: table-cell;
                text-align: center;
                vertical-align: middle;
                width: 50px;
            }

        thead {
            background-color: #0b5394;
            color: #FFF;
        }


        .form-control::placeholder { /* Chrome, Firefox, Opera, Safari 10.1+ */
            color: #c2c2c2;
            opacity: 1; /* Firefox */
        }

        .form-control:-ms-input-placeholder { /* Internet Explorer 10-11 */
            color: #c2c2c2;
        }

        .form-control::-ms-input-placeholder { /* Microsoft Edge */
            color: #c2c2c2;
        }

        .overflow-x-auto {
            overflow-x: auto;
        }

        #content-wrapper {
            background-color: #9d9d9d;
        }

        .bg-owner {
            background-color: #f6f9ce !important;
        }

        .bg-approver {
            background-color: #ccf2d5 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg">
        <div id="wrapper">

            <!-- #include virtual ="/include/menu.inc" -->
            <!-- add side menu -->
            <div id="content-wrapper">

                <div class="container mw-100 px-lg-5">
                    <div class="d-none">
                        <div class="form-check">
                            <asp:CheckBox class="form-check-input" ID="flexRadioDefault1" runat="server" AutoPostBack="true" />
                            <asp:Label CssClass="form-check-label" AssociatedControlID="flexRadioDefault1" runat="server" Text="owner" />
                        </div>
                        <div class="form-check">
                            <asp:CheckBox class="form-check-input" ID="flexRadioDefault2" runat="server" AutoPostBack="true" />
                            <asp:Label CssClass="form-check-label" AssociatedControlID="flexRadioDefault2" runat="server" Text="approver" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-12 mb-3">
                            <a href="../KPI/KPIsSummary.aspx" class="text-decoration-none text-dark"><i class="fas fa-backward"></i><span class="d-none d-md-inline">กลับไปยังหน้า เลือกแบบฟอร์ม</span></a>
                        </div>
                        <div class="col-12">
                            <div class="alert alert-warning alert-dismissible fade show has__res_id" role="alert" id="boxAlert_hasResId" runat="server">
                                <span id="txtAlert_hasResId" runat="server">has_Res_id</span>
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                        </div>
                    </div>
                    <% if formsTable IsNot Nothing Then%>
                    <% For i = 0 To formsTable.Rows.Count - 1 %>
                    <!-- FORMS TITLE -->
                    <div class="row">
                        <div class="col mb-3 forms__title">
                            <div class="card shadow-sm">
                                <div class="card-body px-md-5">
                                    <span class="font-weight-bold h5 ">
                                        <%= formsTable.Rows(i).Item("title").ToString() %>
                                    </span>
                                    <hr />
                                    <span>
                                        <%= formsTable.Rows(i).Item("description").ToString() %>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- FORMS DETAILS -->
                    <div class="row">
                        <div class="col forms__details">

                            <% if groupFormsDtl IsNot Nothing Then %>
                            <% For c = 0 To groupFormsDtl.Rows.Count - 1 %>
                            <div class="card shadow-sm mb-3" data-cardid="<%= groupFormsDtl.Rows(c).Item("card_id") %>">
                                <div class="card-body px-md-5">
                                    <% if formsDtlTable IsNot Nothing Then %>
                                    <% For j = 0 To formsDtlTable.Rows.Count - 1 %>
                                    <% if groupFormsDtl.Rows(c).Item("card_id") = formsDtlTable.Rows(j).Item("card_id") Then %>
                                    <% Select Case formsDtlTable.Rows(j).Item("field_type").ToString()   %>

                                    <% Case "section" %>
                                    <div class="row" data-rowdtlid="<%= formsDtlTable.Rows(j).Item("KPIFormDtl_ID") %>" data-category="<%= formsDtlTable.Rows(j).Item("category") %>" data-type="<%= formsDtlTable.Rows(j).Item("field_type") %>">
                                        <% if formsDtlTable.Rows(j).Item("assessor").IndexOf(assessor) > -1 Then %>
                                        <div class="col  mb-3">
                                            <div class="h5 font-weight-bold">
                                                <%= formsDtlTable.Rows(j).Item("label").ToString() %>
                                                <% if formsDtlTable.Rows(j).Item("is_mandatory") Then %>
                                                <span class="text-danger font-weight-bold">*</span>
                                                <% end if %>
                                            </div>
                                        </div>
                                        <% end if %>
                                    </div>

                                    <% Case "sub_section" %>
                                    <div class="row" data-rowdtlid="<%= formsDtlTable.Rows(j).Item("KPIFormDtl_ID") %>" data-category="<%= formsDtlTable.Rows(j).Item("category") %>" data-type="<%= formsDtlTable.Rows(j).Item("field_type") %>">
                                        <% if formsDtlTable.Rows(j).Item("assessor").IndexOf(assessor) > -1 Then %>
                                        <div class="col  mb-3">
                                            <div class="h6 font-weight-bold">
                                                <%= formsDtlTable.Rows(j).Item("label").ToString() %>
                                                <% if formsDtlTable.Rows(j).Item("is_mandatory") Then %>
                                                <span class="text-danger">*</span>
                                                <% end if %>
                                            </div>
                                        </div>
                                        <% end if %>
                                    </div>

                                    <% Case "description" %>
                                    <div class="row" data-rowdtlid="<%= formsDtlTable.Rows(j).Item("KPIFormDtl_ID") %>" data-category="<%= formsDtlTable.Rows(j).Item("category") %>" data-type="<%= formsDtlTable.Rows(j).Item("field_type") %>">
                                        <% if formsDtlTable.Rows(j).Item("assessor").IndexOf(assessor) > -1 Then %>
                                        <div class="col mb-3">
                                            <div class="">
                                                <span class="text-muted">
                                                    <%= formsDtlTable.Rows(j).Item("label").ToString() %>
                                                </span>
                                            </div>
                                        </div>
                                        <% end if %>
                                    </div>

                                    <% Case "text-h1" %>
                                    <div class="row" data-rowdtlid="<%= formsDtlTable.Rows(j).Item("KPIFormDtl_ID") %>" data-category="<%= formsDtlTable.Rows(j).Item("category") %>" data-type="<%= formsDtlTable.Rows(j).Item("field_type") %>">
                                        <% if formsDtlTable.Rows(j).Item("assessor").IndexOf(assessor) > -1 Then %>
                                        <div class="col mb-3">
                                            <span class="h1">
                                                <%= formsDtlTable.Rows(j).Item("label").ToString() %>
                                            </span>
                                        </div>
                                        <% end if %>
                                    </div>

                                    <% Case "text-h2" %>
                                    <div class="row" data-rowdtlid="<%= formsDtlTable.Rows(j).Item("KPIFormDtl_ID") %>" data-category="<%= formsDtlTable.Rows(j).Item("category") %>" data-type="<%= formsDtlTable.Rows(j).Item("field_type") %>">
                                        <% if formsDtlTable.Rows(j).Item("assessor").IndexOf(assessor) > -1 Then %>
                                        <div class="col mb-3">
                                            <span class="h2">
                                                <%= formsDtlTable.Rows(j).Item("label").ToString() %>
                                            </span>
                                        </div>
                                        <% end if %>
                                    </div>

                                    <% Case "text-h3" %>
                                    <div class="row" data-rowdtlid="<%= formsDtlTable.Rows(j).Item("KPIFormDtl_ID") %>" data-category="<%= formsDtlTable.Rows(j).Item("category") %>" data-type="<%= formsDtlTable.Rows(j).Item("field_type") %>">
                                        <% if formsDtlTable.Rows(j).Item("assessor").IndexOf(assessor) > -1 Then %>
                                        <div class="col mb-3">
                                            <span class="h3">
                                                <%= formsDtlTable.Rows(j).Item("label").ToString() %>
                                            </span>
                                        </div>
                                        <% end if %>
                                    </div>

                                    <% Case "text-h4" %>
                                    <div class="row" data-rowdtlid="<%= formsDtlTable.Rows(j).Item("KPIFormDtl_ID") %>" data-category="<%= formsDtlTable.Rows(j).Item("category") %>" data-type="<%= formsDtlTable.Rows(j).Item("field_type") %>">
                                        <% if formsDtlTable.Rows(j).Item("assessor").IndexOf(assessor) > -1 Then %>
                                        <div class="col mb-3">
                                            <span class="h4">
                                                <%= formsDtlTable.Rows(j).Item("label").ToString() %>
                                            </span>
                                        </div>
                                        <% end if %>
                                    </div>

                                    <% Case "text-h5" %>
                                    <div class="row" data-rowdtlid="<%= formsDtlTable.Rows(j).Item("KPIFormDtl_ID") %>" data-category="<%= formsDtlTable.Rows(j).Item("category") %>" data-type="<%= formsDtlTable.Rows(j).Item("field_type") %>">
                                        <% if formsDtlTable.Rows(j).Item("assessor").IndexOf(assessor) > -1 Then %>
                                        <div class="col mb-3">
                                            <span class="h5">
                                                <%= formsDtlTable.Rows(j).Item("label").ToString() %>
                                            </span>
                                        </div>
                                        <% end if %>
                                    </div>

                                    <% Case "text-h6" %>
                                    <div class="row" data-rowdtlid="<%= formsDtlTable.Rows(j).Item("KPIFormDtl_ID") %>" data-category="<%= formsDtlTable.Rows(j).Item("category") %>" data-type="<%= formsDtlTable.Rows(j).Item("field_type") %>">
                                        <% if formsDtlTable.Rows(j).Item("assessor").IndexOf(assessor) > -1 Then %>
                                        <div class="col mb-3">
                                            <span class="h6">
                                                <%= formsDtlTable.Rows(j).Item("label").ToString() %>
                                            </span>
                                        </div>
                                        <% end if %>
                                    </div>

                                    <% Case "space" %>
                                    <div class="row" data-rowdtlid="<%= formsDtlTable.Rows(j).Item("KPIFormDtl_ID") %>" data-category="<%= formsDtlTable.Rows(j).Item("category") %>" data-type="<%= formsDtlTable.Rows(j).Item("field_type") %>">
                                        <div class="col mb-3">
                                        </div>
                                    </div>

                                    <% Case "kpi_holding" %>
                                    <!--kpi_holding-->
                                    <div class="row" data-rowdtlid="<%= formsDtlTable.Rows(j).Item("KPIFormDtl_ID") %>" data-category="<%= formsDtlTable.Rows(j).Item("category") %>" data-type="<%= formsDtlTable.Rows(j).Item("field_type") %>">
                                        <div class="col">
                                            <% if kpiTable IsNot Nothing Then %>
                                            <% if Convert.ToInt32(kpiTable.Compute("COUNT(kpi_code)", "CategoryID <> 5")) > 0 Then %>
                                            <div class="table-responsive-xl overflow-x-auto">
                                                <table class="table table-sm table-bordered shadow-sm bg-light">
                                                    <thead>
                                                        <tr>
                                                            <th class="text-center align-middle d-none d-md-table-cell" rowspan="2" style="min-width: 150px !important;">ประเภท</th>
                                                            <th class="text-center align-middle" rowspan="2" style="min-width: 400px !important;">หัวข้อ KPIs</th>
                                                            <th class="text-center align-middle" rowspan="2" style="min-width: 40px !important;"><span class="p-1">น้ำหนัก</span></th>
                                                            <th class="text-center align-middle" rowspan="2" style="min-width: 100px !important;"><span class="p-1">หน่วย</span></th>
                                                            <th class="text-center align-middle" rowspan="2" colspan="2" style="min-width: 400px !important;"><span class="p-1">ระดับประเมิน</span></th>
                                                            <th class="text-center align-middle" colspan="100%"><span class="p-1">ผลประเมิน</span></th>
                                                        </tr>
                                                        <tr>

                                                            <%--ส่วนประเมิน Header--%>
                                                            <th class="text-center align-middle" style="min-width: 200px !important;">พนักงาน</th>
                                                            <th class="text-center align-middle" style="min-width: 200px !important;">คำอธิบาย/เหตุผล</th>
                                                            <th class="text-center align-middle" style="min-width: 200px !important;">หัวหน้า</th>
                                                            <th class="text-center align-middle" style="min-width: 200px !important;">คำอธิบาย/เหตุผล</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <% For k = 0 To kpiTable.Rows.Count - 1 %>
                                                        <% if kpiTable.Rows(k).Item("categoryid") <> 5 Then %>
                                                        <tr>

                                                            <%-- detail Kpis--%>
                                                            <td rowspan="5" class="d-none d-md-table-cell align-middle text-center">
                                                                <%= kpiTable.Rows(k).Item("CategoryName").ToString() %>
                                                            </td>
                                                            <td rowspan="5" class="text-left align-middle">
                                                                <span class="pre-line">
                                                                    <%= kpiTable.Rows(k).Item("title").ToString %>
                                                                </span>

                                                            </td>
                                                            <td rowspan="5" class="align-middle"><span class="p-1"><%= kpiTable.Rows(k).Item("Weight").ToString %></span></td>
                                                            <td rowspan="5" class="align-middle">
                                                                <span class="p-1">
                                                                    <%= kpiTable.Rows(k).Item("Unit").ToString %>
                                                                </span>
                                                            </td>

                                                            <td class="align-middle"><span class="p-1 font-weight-bold">5</span></td>
                                                            <td class="text-left"><span class="p-1"><%= kpiTable.Rows(k).Item("Lv5").ToString %></span></td>

                                                            <%--ส่วนประเมิน--%>

                                                            <td rowspan="5" class="align-middle text-center bg-owner">
                                                                <span class="p-1" name="kpi_holding" data-kpicode="<%= kpiTable.Rows(k).Item("kpi_code").ToString() %>" data-ratenow="<%= kpiTable.Rows(k).Item("rateowner_now").ToString() %>" data-assessor="owner">
                                                                    <%= kpiTable.Rows(k).Item("rateowner_now").ToString %>
                                                                </span>
                                                                <%--<select class="form-control bg-transparent" data-style="bg-transparent" name="kpi_holding" data-kpicode="<%= kpiTable.Rows(k).Item("kpi_code").ToString() %>" data-ratenow="<%= kpiTable.Rows(k).Item("rateowner_now").ToString() %>" data-assessor="owner" id="ownerholding_<%= kpiTable.Rows(k).Item("kpi_code").ToString() %>" <% if not assessor = "owner" Then  %> disabled <% end if %> <% if formsDtlTable.Rows(j).Item("is_mandatory") Then %> required <% end if %>>
                                                                    <option disabled selected value="">-- เลือก --</option>
                                                                    <option value="5">5</option>
                                                                    <option value="4">4</option>
                                                                    <option value="3">3</option>
                                                                    <option value="2">2</option>
                                                                    <option value="1">1</option>
                                                                </select>
                                                                <div class="invalid-feedback">กรุณากรอกให้ครบถ้วน</div>--%>
                                                            </td>
                                                            <td rowspan="5" class="align-middle text-center bg-owner">
                                                                <textarea class="form-control bg-transparent" id="txtKPIActualHolding_owner" rows="5" cols="50" placeholder="" value="" autocomplete="off" data-kpicode="<%= kpiTable.Rows(k).Item("kpi_code").ToString() %>" data-assessor="owner" <% if not assessor = "owner" Then  %> disabled <% end if %> <% if formsDtlTable.Rows(j).Item("is_mandatory") Then %> required <% end if %>></textarea>
                                                            </td>
                                                            <td rowspan="5" class="align-middle text-center bg-approver">
                                                                <select class="form-control bg-transparent" data-style="bg-transparent" name="kpi_holding" data-kpicode="<%= kpiTable.Rows(k).Item("kpi_code").ToString() %>" data-ratenow="<%= kpiTable.Rows(k).Item("rateowner_now").ToString() %>" data-assessor="approver" id="approverholding_<%= kpiTable.Rows(k).Item("kpi_code").ToString() %>" <% if not assessor = "approver" Then  %> disabled <% end if %> <% if formsDtlTable.Rows(j).Item("is_mandatory") Then %> required <% end if %>>
                                                                    <option disabled selected value="">-- เลือก --</option>
                                                                    <option value="5">5</option>
                                                                    <option value="4">4</option>
                                                                    <option value="3">3</option>
                                                                    <option value="2">2</option>
                                                                    <option value="1">1</option>
                                                                </select>
                                                                <div class="invalid-feedback">กรุณากรอกให้ครบถ้วน</div>
                                                            </td>
                                                            <td rowspan="5" class="align-middle text-center bg-approver">
                                                                <textarea class="form-control bg-transparent" id="txtKPIActualHolding_approver" rows="5" cols="50" placeholder="" value="" autocomplete="off" data-kpicode="<%= kpiTable.Rows(k).Item("kpi_code").ToString() %>" data-assessor="approver" <% if not assessor = "approver" Then  %> disabled <% end if %> <% if formsDtlTable.Rows(j).Item("is_mandatory") Then %> required <% end if %>></textarea>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="text-center align-middle"><span class="p-1 font-weight-bold">4</span></td>
                                                            <td class="text-left"><span class="p-1"><%= kpiTable.Rows(k).Item("Lv4").ToString %></span></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="text-center align-middle"><span class="p-1 font-weight-bold">3</span></td>
                                                            <td class="text-left"><span class="p-1"><%= kpiTable.Rows(k).Item("Lv3").ToString %></span></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="text-center align-middle"><span class="p-1 font-weight-bold">2</span></td>
                                                            <td class="text-left"><span class="p-1"><%= kpiTable.Rows(k).Item("Lv2").ToString %></span></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="text-center align-middle"><span class="p-1 font-weight-bold">1</span></td>
                                                            <td class="text-left"><span class="p-1"><%= kpiTable.Rows(k).Item("Lv1").ToString %></span></td>
                                                        </tr>

                                                        <% End if %>
                                                        <% Next k %>
                                                    </tbody>
                                                </table>
                                            </div>

                                            <% Else %>
                                            <div class="bg-light p-4 align-items-center d-flex">
                                                <span>คุณยังไม่มี KPIs</span>
                                            </div>
                                            <% End if %>
                                            <% End if %>
                                        </div>
                                    </div>

                                    <% Case "kpi_competency" %>
                                    <!--kpi_competency-->
                                    <div class="row" data-rowdtlid="<%= formsDtlTable.Rows(j).Item("KPIFormDtl_ID") %>" data-category="<%= formsDtlTable.Rows(j).Item("category") %>" data-type="<%= formsDtlTable.Rows(j).Item("field_type") %>">
                                        <div class="col">
                                            <% if kpiTable IsNot Nothing Then %>
                                            <% if Convert.ToInt32(kpiTable.Compute("COUNT(kpi_code)", "CategoryID = 5")) > 0 Then %>

                                            <div class="table-responsive-xl overflow-x-auto">

                                                <table class="table table-sm table-bordered shadow-sm bg-light">
                                                    <thead>
                                                        <tr>
                                                            <th class="text-center align-middle d-none d-md-table-cell" rowspan="2" style="min-width: 150px !important;">ประเภท</th>
                                                            <th class="text-center align-middle" rowspan="2" style="min-width: 400px !important;">หัวข้อ KPIs</th>
                                                            <th class="text-center align-middle" rowspan="2" style="min-width: 40px !important;"><span class="p-1">น้ำหนัก</span></th>
                                                            <th class="text-center align-middle" rowspan="2" style="min-width: 100px !important;"><span class="p-1">หน่วย</span></th>
                                                            <th class="text-center align-middle" rowspan="2" colspan="2" style="min-width: 400px !important;"><span class="p-1">ระดับประเมิน</span></th>
                                                            <th class="text-center align-middle" colspan="100%"><span class="p-1">ผลประเมิน</span></th>
                                                        </tr>
                                                        <tr>

                                                            <%--ส่วนประเมิน Header--%>
                                                            <th class="text-center align-middle" style="min-width: 200px !important;">พนักงาน</th>
                                                            <th class="text-center align-middle" style="min-width: 200px !important;">คำอธิบาย/เหตุผล</th>
                                                            <th class="text-center align-middle" style="min-width: 200px !important;">หัวหน้า</th>
                                                            <th class="text-center align-middle" style="min-width: 200px !important;">คำอธิบาย/เหตุผล</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <% For k = 0 To kpiTable.Rows.Count - 1 %>
                                                        <% if kpiTable.Rows(k).Item("categoryid") = 5 Then %>
                                                        <tr>

                                                            <%-- detail Kpis--%>
                                                            <td rowspan="5" class="d-none d-md-table-cell align-middle text-center">
                                                                <%= kpiTable.Rows(k).Item("CategoryName").ToString() %>
                                                            </td>
                                                            <td rowspan="5" class="text-left align-middle">
                                                                <span class="pre-line">
                                                                    <%= kpiTable.Rows(k).Item("title").ToString %>
                                                                </span>

                                                            </td>
                                                            <td rowspan="5" class="align-middle"><span class="p-1"><%= kpiTable.Rows(k).Item("Weight").ToString %></span></td>
                                                            <td rowspan="5" class="align-middle">
                                                                <span class="p-1">
                                                                    <%= kpiTable.Rows(k).Item("Unit").ToString %>
                                                                </span>
                                                            </td>

                                                            <td class="align-middle"><span class="p-1 font-weight-bold">5</span></td>
                                                            <td class="text-left"><span class="p-1"><%= kpiTable.Rows(k).Item("Lv5").ToString %></span></td>

                                                            <%--ส่วนประเมิน--%>
                                                            <td rowspan="5" class="align-middle text-center bg-owner">
                                                                <span class="p-1" name="kpi_competency" data-kpicode="<%= kpiTable.Rows(k).Item("kpi_code").ToString() %>" data-ratenow="<%= kpiTable.Rows(k).Item("rateowner_now").ToString() %>" data-assessor="owner">
                                                                    <%= kpiTable.Rows(k).Item("rateowner_now").ToString %>
                                                                </span>
                                                                <%-- <select class="form-control bg-transparent" data-style="bg-transparent" name="kpi_competency" data-kpicode="<%= kpiTable.Rows(k).Item("kpi_code").ToString() %>" data-assessor="owner" id="ownercompetency_<%= kpiTable.Rows(k).Item("kpi_code").ToString() %>" <% if not assessor = "owner" Then  %> disabled <% end if %> <% if formsDtlTable.Rows(j).Item("is_mandatory") Then %> required <% end if %>>
                                                                    <option disabled selected value="">-- เลือก --</option>
                                                                    <option value="5">5</option>
                                                                    <option value="4">4</option>
                                                                    <option value="3">3</option>
                                                                    <option value="2">2</option>
                                                                    <option value="1">1</option>
                                                                </select>
                                                                <div class="invalid-feedback">กรุณากรอกให้ครบถ้วน</div>--%>
                                                            </td>
                                                            <td rowspan="5" class="align-middle text-center bg-owner">

                                                                <textarea class="form-control bg-transparent" id="txtKPIActualCompetency_owner" rows="5" cols="50" placeholder="" value="" autocomplete="off" data-kpicode="<%= kpiTable.Rows(k).Item("kpi_code").ToString() %>" data-assessor="owner" <% if not assessor = "owner" Then  %> disabled <% end if %> <% if formsDtlTable.Rows(j).Item("is_mandatory") Then %> required <% end if %>></textarea>
                                                            </td>

                                                            <td rowspan="5" class="align-middle text-center bg-approver">
                                                                <select class="form-control bg-transparent" data-style="bg-transparent" name="kpi_competency" data-kpicode="<%= kpiTable.Rows(k).Item("kpi_code").ToString() %>" data-assessor="approver" id="approvercompetency_<%= kpiTable.Rows(k).Item("kpi_code").ToString() %>" <% if not assessor = "approver" Then  %> disabled <% end if %> <% if formsDtlTable.Rows(j).Item("is_mandatory") Then %> required <% end if %>>
                                                                    <option disabled selected value="">-- เลือก --</option>
                                                                    <option value="5">5</option>
                                                                    <option value="4">4</option>
                                                                    <option value="3">3</option>
                                                                    <option value="2">2</option>
                                                                    <option value="1">1</option>
                                                                </select>
                                                                <div class="invalid-feedback">กรุณากรอกให้ครบถ้วน</div>
                                                            </td>
                                                            <td rowspan="5" class="align-middle text-center bg-approver">
                                                                <textarea class="form-control bg-transparent" id="txtKPIActualCompetency_approver" rows="5" cols="50" placeholder="" value="" autocomplete="off" data-kpicode="<%= kpiTable.Rows(k).Item("kpi_code").ToString() %>" data-assessor="approver" <% if not assessor = "approver" Then  %> disabled <% end if %> <% if formsDtlTable.Rows(j).Item("is_mandatory") Then %> required <% end if %>></textarea>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="text-center align-middle"><span class="p-1 font-weight-bold">4</span></td>
                                                            <td class="text-left"><span class="p-1"><%= kpiTable.Rows(k).Item("Lv4").ToString %></span></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="text-center align-middle"><span class="p-1 font-weight-bold">3</span></td>
                                                            <td class="text-left"><span class="p-1"><%= kpiTable.Rows(k).Item("Lv3").ToString %></span></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="text-center align-middle"><span class="p-1 font-weight-bold">2</span></td>
                                                            <td class="text-left"><span class="p-1"><%= kpiTable.Rows(k).Item("Lv2").ToString %></span></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="text-center align-middle"><span class="p-1 font-weight-bold">1</span></td>
                                                            <td class="text-left"><span class="p-1"><%= kpiTable.Rows(k).Item("Lv1").ToString %></span></td>
                                                        </tr>

                                                        <% End if %>
                                                        <% Next k %>
                                                    </tbody>
                                                </table>
                                            </div>

                                            <% Else %>
                                            <div class="bg-light p-4 align-items-center d-flex">
                                                <span>คุณยังไม่มี KPIs</span>
                                            </div>
                                            <% End if %>
                                            <% End if %>
                                        </div>
                                    </div>

                                    <% Case "kpi_competency_reason" %>
                                    <!--kpi_competency_reason-->
                                    <div class="row" data-rowdtlid="<%= formsDtlTable.Rows(j).Item("KPIFormDtl_ID") %>" data-category="<%= formsDtlTable.Rows(j).Item("category") %>" data-type="<%= formsDtlTable.Rows(j).Item("field_type") %>">
                                        <% if kpiTable IsNot Nothing Then %>


                                        <% if Convert.ToInt32(kpiTable.Compute("COUNT(kpi_code)", "CategoryID = 5")) > 0 Then %>
                                        <% For k = 0 To kpiTable.Rows.Count - 1 %>
                                        <% if kpiTable.Rows(k).Item("categoryid") = 5 Then %>
                                        <div class="col">
                                            <div class="table-responsive-xl overflow-x-auto">
                                                <table class="table table-sm table-bordered shadow-sm bg-light">
                                                    <thead>
                                                        <tr>
                                                            <th class="text-center align-middle d-none d-md-table-cell" rowspan="2" style="min-width: 150px !important;">ประเภท</th>
                                                            <th class="text-center align-middle" rowspan="2" style="min-width: 500px !important;">หัวข้อ KPIs</th>
                                                            <th class="text-center align-middle" rowspan="2" style="min-width: 40px !important;"><span class="p-1">น้ำหนัก</span></th>
                                                            <th class="text-center align-middle" rowspan="2" style="min-width: 100px !important;"><span class="p-1">หน่วย</span></th>
                                                            <th class="text-center align-middle" rowspan="2" colspan="2" style="min-width: 400px !important;"><span class="p-1">ระดับประเมิน</span></th>
                                                            <th class="text-center align-middle" colspan="100%"><span class="p-1">ผลประเมิน</span></th>
                                                        </tr>
                                                        <tr>

                                                            <%--ส่วนประเมิน Header--%>
                                                            <th class="text-center align-middle" style="min-width: 200px !important;">พนักงาน</th>
                                                            <th class="text-center align-middle" style="min-width: 200px !important;">หัวหน้า</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>

                                                            <%-- detail Kpis--%>
                                                            <td rowspan="5" class="d-none d-md-table-cell align-middle text-center">
                                                                <%= kpiTable.Rows(k).Item("CategoryName").ToString() %>
                                                            </td>

                                                            <td rowspan="5" class="text-left align-middle">
                                                                <span class="pre-line">
                                                                    <%= kpiTable.Rows(k).Item("title").ToString %>
                                                                </span>

                                                            </td>
                                                            <td rowspan="5" class="align-middle"><span class="p-1"><%= kpiTable.Rows(k).Item("Weight").ToString %></span></td>
                                                            <td rowspan="5" class="align-middle">
                                                                <span class="p-1">
                                                                    <%= kpiTable.Rows(k).Item("Unit").ToString %>
                                                                </span>
                                                            </td>

                                                            <td class="align-middle w-10"><span class="p-1 font-weight-bold">5</span></td>
                                                            <td class="text-left w-90"><span class="p-1"><%= kpiTable.Rows(k).Item("Lv5").ToString %></span></td>

                                                            <%--ส่วนประเมิน--%>
                                                            <td rowspan="5" class="align-middle text-center bg-owner">
                                                                <span class="p-1" name="kpi_competency_reason" data-kpicode="<%= kpiTable.Rows(k).Item("kpi_code").ToString() %>" data-ratenow="<%= kpiTable.Rows(k).Item("rateowner_now").ToString() %>" data-assessor="owner">
                                                                    <%= kpiTable.Rows(k).Item("rateowner_now").ToString %>
                                                                </span>
                                                                <%--<select class="form-control bg-transparent" data-style="bg-transparent" name="kpi_competency_reason" data-kpicode="<%= kpiTable.Rows(k).Item("kpi_code").ToString() %>" data-assessor="owner" id="ownercompetency_reason_<%= kpiTable.Rows(k).Item("kpi_code").ToString() %>" <% if not assessor = "owner" Then  %> disabled <% end if %> <% if formsDtlTable.Rows(j).Item("is_mandatory") Then %>required<% end if %>>
                                                                    <option disabled selected value="">-- เลือก --</option>
                                                                    <option value="5">5</option>
                                                                    <option value="4">4</option>
                                                                    <option value="3">3</option>
                                                                    <option value="2">2</option>
                                                                    <option value="1">1</option>
                                                                </select>
                                                                <div class="invalid-feedback">กรุณากรอกให้ครบถ้วน</div>--%>
                                                            </td>

                                                            <td rowspan="5" class="align-middle text-center bg-approver">
                                                                <select class="form-control bg-transparent" data-style="bg-transparent" name="kpi_competency_reason" data-kpicode="<%= kpiTable.Rows(k).Item("kpi_code").ToString() %>" data-assessor="approver" id="approvercompetency_reason_<%= kpiTable.Rows(k).Item("kpi_code").ToString() %>" <% if not assessor = "approver" Then %> disabled <% end if %> <% if formsDtlTable.Rows(j).Item("is_mandatory") Then %>required<% end if %>>
                                                                    <option disabled selected value="">-- เลือก --</option>
                                                                    <option value="5">5</option>
                                                                    <option value="4">4</option>
                                                                    <option value="3">3</option>
                                                                    <option value="2">2</option>
                                                                    <option value="1">1</option>
                                                                </select>
                                                                <div class="invalid-feedback">กรุณากรอกให้ครบถ้วน</div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="text-center align-middle w-10"><span class="p-1 font-weight-bold">4</span></td>
                                                            <td class="text-left w-90"><span class="p-1"><%= kpiTable.Rows(k).Item("Lv3").ToString %></span></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="text-center align-middle w-10"><span class="p-1 font-weight-bold">3</span></td>
                                                            <td class="text-left w-90"><span class="p-1"><%= kpiTable.Rows(k).Item("Lv3").ToString %></span></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="text-center align-middle w-10"><span class="p-1 font-weight-bold">2</span></td>
                                                            <td class="text-left w-90"><span class="p-1"><%= kpiTable.Rows(k).Item("Lv2").ToString %></span></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="text-center align-middle w-10"><span class="p-1 font-weight-bold">1</span></td>
                                                            <td class="text-left w-90"><span class="p-1"><%= kpiTable.Rows(k).Item("Lv1").ToString %></span></td>
                                                        </tr>

                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <div class="form-group">
                                                <textarea class="form-control bg-owner" id="txtFeedbackKPIReasonOwner" rows="2" cols="50" placeholder="เหตุผลการให้คะแนน (ประเมินตนเอง)" value="" autocomplete="off" data-kpicode="<%= kpiTable.Rows(k).Item("kpi_code").ToString() %>" data-assessor="owner" <% if not assessor = "owner" Then  %> disabled <% end if %> <% if formsDtlTable.Rows(j).Item("is_mandatory") Then %> required <% end if %>></textarea>
                                                <div class="invalid-feedback">กรุณากรอกให้ครบถ้วน</div>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <div class="form-group">
                                                <textarea class="form-control bg-approver" id="txtFeedbackKPIReasonApprover" rows="2" cols="50" placeholder="เหตุผลการให้คะแนน (หัวหน้าประเมิน)" value="" autocomplete="off" data-kpicode="<%= kpiTable.Rows(k).Item("kpi_code").ToString() %>" data-assessor="approver" <% if not assessor = "approver" Then  %> disabled <% end if %> <% if formsDtlTable.Rows(j).Item("is_mandatory") Then %> required <% end if %>></textarea>
                                                <div class="invalid-feedback">กรุณากรอกให้ครบถ้วน</div>
                                            </div>
                                        </div>
                                        <% End if %>
                                        <% Next k %>
                                        <% Else %>
                                        <div class="col">
                                            <div class="bg-light p-4 align-items-center d-flex">
                                                <span>คุณยังไม่มี KPIs</span>
                                            </div>
                                        </div>
                                        <% End if %>
                                        <% End if %>
                                    </div>

                                    <% Case "fill_text" %>
                                    <!--fill_text ยังไม่พร้อมใช้งาน-->
                                    <div class="row" data-rowdtlid="<%= formsDtlTable.Rows(j).Item("KPIFormDtl_ID") %>" data-category="<%= formsDtlTable.Rows(j).Item("category") %>" data-type="<%= formsDtlTable.Rows(j).Item("field_type") %>">
                                        <div class="col-12">
                                            <% if formsDtlTable.Rows(j).Item("assessor").IndexOf(assessor) > -1 Then %>
                                            <div class="fill_text" id="ownerfill_text">
                                                <div class="row" name="content__ownerfill_text">
                                                    <div class="col-12 mb-3">
                                                        <div class="input-group hover__show-btnDelete">
                                                            <input class="form-control bg-transparent" type="search" name="txt_filltext" value="" <% if formsDtlTable.Rows(j).Item("is_mandatory") Then %>required<% end if %> />
                                                            <div class="input-group-append">
                                                                <button class="btn btn-link text-danger" type="button" onclick="remove_elem(this,'ownerfill_text','content__ownerfill_text')"><i class="fas fa-trash-alt"></i></button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="d-flex align-items-center ">
                                                <button class="btn btn-link" type="button" onclick="fill_elem('ownerfill_text')"><i class="fas fa-plus-circle"></i><span>&nbsp;เพิ่มรายการ</span></button>
                                            </div>
                                            <% end if %>
                                        </div>

                                    </div>

                                    <% Case "fill_select" %>
                                    <!--fill_select ยังไม่พร้อมใช้งาน-->
                                    <div class="row" data-rowdtlid="<%= formsDtlTable.Rows(j).Item("KPIFormDtl_ID") %>" data-category="<%= formsDtlTable.Rows(j).Item("category") %>" data-type="<%= formsDtlTable.Rows(j).Item("field_type") %>">
                                        <div class="col-12 mb-3">
                                            <% if formsDtlTable.Rows(j).Item("assessor").IndexOf(assessor) > -1 Then %>
                                            <div class="fill_select" id="ownerfill_select">
                                                <div class="row" name="content__fill_select">
                                                    <div class="col-12 mb-3 cbo__kpi">
                                                        <select class="form-control bg-transparent" data-style="bg-transparent" <% if formsDtlTable.Rows(j).Item("is_mandatory") Then %>required<% end if %>>
                                                            <option disabled selected value="">-- เลือก --</option>
                                                            <% For k = 0 To formsDtlCboTable.Rows.Count - 1 %>
                                                            <% If formsDtlTable.Rows(j).Item("KPIFormDtl_ID") = formsDtlCboTable.Rows(k).Item("KPIFormDtl_ID") Then %>
                                                            <option value="<%= formsDtlCboTable.Rows(k).Item("cbo_id") %>"><%= formsDtlCboTable.Rows(k).Item("cbo_text") %></option>
                                                            <% End If %>
                                                            <% Next k %>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="d-flex align-items-center ">
                                                <button class="btn btn-link" type="button" onclick="fill_elem('ownerfill_select')"><i class="fas fa-plus-circle"></i><span>&nbsp;เพิ่มรายการ</span></button>
                                            </div>
                                            <% end if %>
                                        </div>
                                    </div>

                                    <% Case "textarea" %>
                                    <!--textarea-->
                                    <div class="row" data-rowdtlid="<%= formsDtlTable.Rows(j).Item("KPIFormDtl_ID") %>" data-category="<%= formsDtlTable.Rows(j).Item("category") %>" data-type="<%= formsDtlTable.Rows(j).Item("field_type") %>">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <textarea class="form-control bg-<%= formsDtlTable.Rows(j).Item("assessor") %>" id="txtFeedbackOwner" rows="2" cols="50" placeholder="<%= formsDtlTable.Rows(j).Item("label").ToString() %>" data-assessor="<%= formsDtlTable.Rows(j).Item("assessor") %>" value="" autocomplete="off" <% if formsDtlTable.Rows(j).Item("is_mandatory") Then %> required <% end if %> <% if not formsDtlTable.Rows(j).Item("assessor").IndexOf(assessor) > -1 Then %> readonly <% end if %>></textarea>
                                                <div class="invalid-feedback">กรุณากรอกให้ครบถ้วน</div>
                                            </div>
                                        </div>
                                    </div>

                                    <% Case "career" %>
                                    <!--career-->
                                    <div class="row" data-rowdtlid="<%= formsDtlTable.Rows(j).Item("KPIFormDtl_ID") %>" data-category="<%= formsDtlTable.Rows(j).Item("category") %>" data-type="<%= formsDtlTable.Rows(j).Item("field_type") %>">
                                        <div class="col">
                                            <div class="table-responsive-xl overflow-x-auto">
                                                <table class="table table-sm table-bordered shadow-sm bg-light">
                                                    <thead>
                                                        <tr>
                                                            <th class="text-center align-middle" rowspan="2" style="min-width: 500px !important;"><%= formsDtlTable.Rows(j).Item("label").ToString() %></th>
                                                            <th class="text-center align-middle" rowspan="2" colspan="2" style="min-width: 400px !important;">ระดับการประเมิน</th>
                                                            <th class="text-center align-middle" colspan="100%"><span class="p-1">ผลประเมิน</span></th>
                                                        </tr>
                                                        <tr>
                                                            <th class="text-center align-middle" style="min-width: 200px !important;">พนักงาน</th>
                                                            <th class="text-center align-middle" style="min-width: 200px !important;">หัวหน้า</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <% For k = 0 To formsDtlCareerTable.Rows.Count - 1 %>
                                                        <% if formsDtlCareerTable.Rows(k).Item("kpiformDtl_id") = formsDtlTable.Rows(j).Item("kpiformDtl_id") Then %>
                                                        <tr>
                                                            <td rowspan="5" class="align-middle"><span class="p-1"><%= formsDtlCareerTable.Rows(k).Item("career_description").ToString() %></span></td>
                                                            <td class="align-middle"><span class="p-1 font-weight-bold">5</span></td>
                                                            <td class="text-left"><span class="p-1"><%= formsDtlCareerTable.Rows(k).Item("Lv5").ToString %></span></td>
                                                            <td rowspan="5" class="align-middle text-center bg-owner">
                                                                <select class="form-control bg-transparent" data-style="bg-transparent" name="career" data-careerid="<%= formsDtlCareerTable.Rows(k).Item("career_id").ToString() %>" data-assessor="owner" <% if not assessor = "owner" Then  %> disabled <% end if %> <% if formsDtlTable.Rows(j).Item("is_mandatory") Then %> required <% end if %>>
                                                                    <option disabled selected value="">-- เลือก --</option>
                                                                    <option value="5">5</option>
                                                                    <option value="4">4</option>
                                                                    <option value="3">3</option>
                                                                    <option value="2">2</option>
                                                                    <option value="1">1</option>
                                                                </select>
                                                            </td>
                                                            <td rowspan="5" class="align-middle text-center bg-approver">
                                                                <select class="form-control bg-transparent" data-style="bg-transparent" name="career" data-careerid="<%= formsDtlCareerTable.Rows(k).Item("career_id").ToString() %>" data-assessor="approver" <% if not assessor = "approver" Then  %> disabled <% end if %> <% if formsDtlTable.Rows(j).Item("is_mandatory") Then %> required <% end if %>>
                                                                    <option disabled selected value="">-- เลือก --</option>
                                                                    <option value="5">5</option>
                                                                    <option value="4">4</option>
                                                                    <option value="3">3</option>
                                                                    <option value="2">2</option>
                                                                    <option value="1">1</option>
                                                                </select>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td class="text-center align-middle"><span class="p-1 font-weight-bold">4</span></td>
                                                            <td class="text-left"><span class="p-1"><%= formsDtlCareerTable.Rows(k).Item("Lv4").ToString %></span></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="text-center align-middle"><span class="p-1 font-weight-bold">3</span></td>
                                                            <td class="text-left"><span class="p-1"><%= formsDtlCareerTable.Rows(k).Item("Lv3").ToString %></span></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="text-center align-middle"><span class="p-1 font-weight-bold">2</span></td>
                                                            <td class="text-left"><span class="p-1"><%= formsDtlCareerTable.Rows(k).Item("Lv2").ToString %></span></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="text-center align-middle"><span class="p-1 font-weight-bold">1</span></td>
                                                            <td class="text-left"><span class="p-1"><%= formsDtlCareerTable.Rows(k).Item("Lv1").ToString %></span></td>
                                                        </tr>
                                                        <% End if %>
                                                        <% Next k %>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                    <% Case "project" %>
                                    <!--project-->
                                    <div class="row" data-rowdtlid="<%= formsDtlTable.Rows(j).Item("KPIFormDtl_ID") %>" data-category="<%= formsDtlTable.Rows(j).Item("category") %>" data-type="<%= formsDtlTable.Rows(j).Item("field_type") %>">
                                        <div class="col">
                                            <div class="table-responsive-xl overflow-x-auto">
                                                <table class="table table-sm project__table table-bordered shadow-sm bg-light">
                                                    <thead>
                                                        <tr>
                                                            <th class="text-center align-middle"></th>
                                                            <th class="text-center align-middle" style="min-width: 300px !important;"><span>หัวข้อ/ชื่อโครงการ *</span></th>
                                                            <th class="text-center align-middle" style="min-width: 300px !important;"><span>วัตถุประสงค์</span></th>
                                                            <th class="text-center align-middle" style="min-width: 300px !important;"><span>วิธีการ</span></th>
                                                            <th class="text-center align-middle" style="min-width: 300px !important;"><span>ผลลัพธ์</span></th>
                                                            <th class="text-center align-middle" style="min-width: 300px !important;"><span>ผู้ที่เกี่ยวข้อง</span></th>
                                                            <th class="text-center align-middle"><span></span></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="<%= formsDtlTable.Rows(j).Item("KPIFormDtl_ID") %>" data-assessor="owner">
                                                        <tr>
                                                            <td class="text-center align-middle bg-owner">
                                                                <textarea class="form-control bg-transparent"></textarea></td>
                                                            <td class="text-center align-middle bg-owner">
                                                                <textarea class="form-control bg-transparent"></textarea></td>
                                                            <td class="text-center align-middle bg-owner">
                                                                <textarea class="form-control bg-transparent"></textarea></td>
                                                            <td class="text-center align-middle bg-owner">
                                                                <textarea class="form-control bg-transparent"></textarea></td>
                                                            <td class="text-center align-middle bg-owner">
                                                                <textarea class="form-control bg-transparent"></textarea></td>
                                                            <td class="text-center align-middle"></td>
                                                        </tr>

                                                    </tbody>
                                                </table>
                                            </div>
                                            <% if assessor = "owner" Then  %>
                                            <button class="btn btn-link" type="button" onclick="addRow(<%= formsDtlTable.Rows(j).Item("KPIFormDtl_ID") %>)"><i class="fas fa-plus-circle"></i><span>&nbsp;เพิ่มรายการ</span></button>
                                            <% end if %>
                                        </div>

                                    </div>

                                    <% Case "cbo" %>
                                    <!--cbo-->
                                    <div class="row" data-rowdtlid="<%= formsDtlTable.Rows(j).Item("KPIFormDtl_ID") %>" data-category="<%= formsDtlTable.Rows(j).Item("category") %>" data-type="<%= formsDtlTable.Rows(j).Item("field_type") %>">
                                        <div class="col mb-3">

                                            <select class="form-control bg-transparent" data-style="bg-<%= formsDtlTable.Rows(j).Item("assessor") %>" name="cbo" data-assessor="<%= formsDtlTable.Rows(j).Item("assessor") %>" <% if not formsDtlTable.Rows(j).Item("assessor").IndexOf(assessor) > -1 Then %> disabled <% end if %> <% if formsDtlTable.Rows(j).Item("is_mandatory") Then %> required <% end if %> data-label="<%= formsDtlTable.Rows(j).Item("label") %>">
                                                <option value="">-- เลือก --</option>
                                                <% For k = 0 To formsDtlCboTable.Rows.Count - 1 %>
                                                <% If formsDtlTable.Rows(j).Item("KPIFormDtl_ID") = formsDtlCboTable.Rows(k).Item("KPIFormDtl_ID") Then %>
                                                <option value="<%= formsDtlCboTable.Rows(k).Item("cbo_id") %>"><%= formsDtlCboTable.Rows(k).Item("cbo_text") %></option>
                                                <% End If %>
                                                <% Next k %>
                                            </select>
                                        </div>
                                    </div>

                                    <% Case "cboGroup" %>
                                    <!--cboGroup-->
                                    <div class="row" data-rowdtlid="<%= formsDtlTable.Rows(j).Item("KPIFormDtl_ID") %>" data-category="<%= formsDtlTable.Rows(j).Item("category") %>" data-type="<%= formsDtlTable.Rows(j).Item("field_type") %>">
                                        <div class="col">
                                            <div class="table-responsive">
                                                <table class="table table-sm table-bordered shadow-sm bg-light table__cboGroup" data-table-id="<%= formsDtlTable.Rows(j).Item("KPIFormDtl_ID") %>" data-assessor="<%= formsDtlTable.Rows(j).Item("assessor") %>">
                                                    <tr>
                                                        <td style="width: 300px !important;">
                                                            <label>1. เลือกฝ่าย</label>
                                                            <select class="groupCbo form-control bg-transparent" data-style="bg-<%= formsDtlTable.Rows(j).Item("assessor") %>" data-assessor="<%= formsDtlTable.Rows(j).Item("assessor") %>" <% if not formsDtlTable.Rows(j).Item("assessor").IndexOf(assessor) > -1 Then %> disabled <% end if %>>
                                                                <option disabled selected value="">-- เลือกกลุ่ม --</option>
                                                                <% If groupCbo IsNot Nothing Then %>
                                                                <% For gc = 0 To groupCbo.Rows.Count - 1 %>
                                                                <% If formsDtlTable.Rows(j).Item("KPIFormDtl_ID") = groupCbo.Rows(gc).Item("KPIFormDtl_ID") Then %>
                                                                <option value="<%= groupCbo.Rows(gc).Item("cbo_Group") %>">
                                                                    <%= groupCbo.Rows(gc).Item("cbo_Group") %>
                                                                </option>
                                                                <% End If %>
                                                                <% Next gc %>
                                                                <% End If %>
                                                            </select>
                                                        </td>
                                                        <td style="width: 300px !important;">
                                                            <label>2. เลือกแผนก</label>
                                                            <select class="subGroupCbo form-control bg-transparent" data-style="bg-<%= formsDtlTable.Rows(j).Item("assessor") %>" data-assessor="<%= formsDtlTable.Rows(j).Item("assessor") %>" <% if not formsDtlTable.Rows(j).Item("assessor").IndexOf(assessor) > -1 Then %> disabled <% end if %>>
                                                                <option disabled selected value="">-- เลือกกลุ่ม --</option>
                                                                <% If subGroupCbo IsNot Nothing Then %>
                                                                <% For gc = 0 To subGroupCbo.Rows.Count - 1 %>
                                                                <% If formsDtlTable.Rows(j).Item("KPIFormDtl_ID") = subGroupCbo.Rows(gc).Item("KPIFormDtl_ID") Then %>
                                                                <option value="<%= subGroupCbo.Rows(gc).Item("cbo_subText") %>">
                                                                    <%= subGroupCbo.Rows(gc).Item("cbo_subText") %>
                                                                </option>
                                                                <% End If %>
                                                                <% Next gc %>
                                                                <% End If %>
                                                            </select>
                                                        </td>
                                                        <td>
                                                            <label>3. เลือกหัวข้อ</label>
                                                            <select class="detailCbo form-control bg-transparent" data-style="bg-<%= formsDtlTable.Rows(j).Item("assessor") %>" data-assessor="<%= formsDtlTable.Rows(j).Item("assessor") %>" <% if not formsDtlTable.Rows(j).Item("assessor").IndexOf(assessor) > -1 Then %> disabled <% end if %> <% if formsDtlTable.Rows(j).Item("is_mandatory") Then %> required <% end if %>>
                                                                <option value="">-- เลือกรายละเอียด --</option>
                                                            </select>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div id="hiddenDetailData<%= formsDtlTable.Rows(j).Item("KPIFormDtl_ID") %>" class="hiddenDetailData d-none">
                                                <% For k = 0 To formsDtlCboTable.Rows.Count - 1 %>
                                                <% If formsDtlTable.Rows(j).Item("KPIFormDtl_ID") = formsDtlCboTable.Rows(k).Item("KPIFormDtl_ID") Then %>
                                                <div data-group="<%= formsDtlCboTable.Rows(k).Item("cbo_Group") %>"
                                                    data-subgroup="<%= formsDtlCboTable.Rows(k).Item("cbo_subText") %>"
                                                    data-id="<%= formsDtlCboTable.Rows(k).Item("cbo_id") %>"
                                                    data-text="<%= formsDtlCboTable.Rows(k).Item("cbo_text") %>"
                                                    data-subtext="<%= formsDtlCboTable.Rows(k).Item("cbo_subText") %>">
                                                </div>
                                                <% end if %>
                                                <% Next k %>
                                            </div>
                                        </div>
                                    </div>

                                    <% End Select %>
                                    <% end if %>
                                    <% Next j %>

                                    <% end if %>
                                </div>
                            </div>
                            <% Next c %>
                            <% end if %>
                        </div>
                    </div>

                    <% Next i %>

                    <!-- FORMS BTN -->

                    <div class="row">
                        <div class="col-12 mb-3 px-md-5">
                            <div class="form-check form-check-inline">
                                <input type="checkbox" class="form-check-input" id="chkKpiComplete" runat="server" required>
                                <asp:Label ID="lbchkKpiComplete" CssClass="form-check-label h6" AssociatedControlID="chkKpiComplete" runat="server" Text="ข้าพเจ้าขอยืนยันว่าได้กรอกข้อมูลการประเมินผลงานประจำปี ถูกต้องครบถ้วนตามความเป็นจริง" />
                            </div>
                        </div>
                        <div class="col-12 mx-auto px-md-5">
                            <asp:Button ID="btnUpload" class="btn btn-lg btn-primary" runat="server" Text="Submit" OnClientClick="handle_btnSubmitClicked()" AutoPostBack="true" />
                            <asp:Button ID="btnApproval" class="btn  btn-lg btn-primary" runat="server" Text="Approve" OnClientClick="handle_btnAcceptClicked()" AutoPostBack="true" />
                            <%--<asp:Button ID="btnReject" class="btn  btn-lg btn-danger" runat="server" Text="ไม่อนุมัติ" OnClientClick="handle_btnSubmitClicked()" AutoPostBack="true" />--%>
                        </div>
                    </div>

                    <button type="button" class="btn btn-warning btn-lg shadow showAfterScroll" style="display: none; position: fixed; bottom: 5rem; right: 25px;" id="btnSave" runat="server" title="บันทึกไว้ก่อน" onclick="handle_btnSaveClicked();" autopostback="true">
                        <i class="fas fa-save"></i>
                    </button>
                    <a id="back-to-top" href="#" class="btn btn-light btn-lg back-to-top shadow" role="button"><i class="fas fa-chevron-up"></i></a>

                    <% End If %>
                </div>
            </div>
        </div>
    </div>
    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/NonPO.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Jobs.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/KPIs.js")%>"></script>
    <script>

        $(window).scroll(function () {
            if ($(this).scrollTop() > 50) {
                $('.showAfterScroll').fadeIn();
            } else {
                $('.showAfterScroll').fadeOut();
            }
        });
        $(document).ready(function () {
            document.body.style.backgroundColor = "#9d9d9d";
            $('.form-control').selectpicker({
                noneSelectedText: '-',
                dropupAuto: false,
                boundary: 'window',
                liveSearch: true,
                maxOptions: 1
            });

            $('.form-control').selectpicker('refresh');


            setupGroupSelect();
            replaceCboGroup("<%= assessor %>");
            replaceCbo("<%= assessor %>");
            addRadioToProject("<%= assessor %>");
            replaceCell("<%= assessor %>");
            checkAndReplaceTablesProject("<%= assessor %>");
        });
        $('.form-control').on('show.bs.select', function () {
            var $select = $(this);
            var $dropdown = $select.data('selectpicker').$menu;
            var offset = $select.offset();

            $dropdown.css({
                top: offset.top + $select.outerHeight(),
                left: offset.left,
                position: 'absolute'
            });
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


        function handle_btnSubmitClicked() {
            if (confirm("คุณต้องการจะบันทึกหรือไม่ ?")) {
                var form = $("#form1")[0];

                const spans = document.querySelectorAll('span[name="kpi_holding"], span[name="kpi_competency"], span[name="kpi_competency_reason"]');


                // ใช้ some เพื่อตรวจสอบเงื่อนไข
                const hasInvalidSpan = Array.from(spans).some(span => {
                    const content = span.textContent.trim(); // อ่านค่าเนื้อหาใน span
                    const dataRateNow = span.getAttribute("data-ratenow"); // อ่านค่า data-ratenow

                    return !content || !dataRateNow; // ตรวจสอบว่ามี span ที่ไม่มี content หรือ data-ratenow
                });
                if (form.checkValidity() === false || hasInvalidSpan) {
                    event.preventDefault();
                    event.stopPropagation();
                    alertWarning(hasInvalidSpan ? 'กรุณาลงคะแนนประเมิน KPIs ให้ครบถ้วน' : 'กรุณากรอกข้อมูลให้ครบถ้วน');
                    form.classList.add('was-validated');
                } else {
                    const payload = getJSON_input();
                    __doPostBack('submit', payload);
                }
            }
            else {
                event.preventDefault();
                event.stopPropagation();
            }
            return true;
        }

        function handle_btnAcceptClicked() {
            if (confirm("คุณต้องการจะบันทึกหรือไม่ ?")) {
                var form = $("#form1")[0];
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                    alertWarning('กรุณากรอกข้อมูลให้ครบถ้วน');
                    form.classList.add('was-validated');
                } else {
                    const payload = getJSON_input();
                    __doPostBack('accept', payload);
                }
            }
            else {
                event.preventDefault();
                event.stopPropagation();
            }
            return true;
        }
        function handle_btnSaveClicked() {
            //validateData()


            const payload = getJSON_input();
            //console.log(payload);
            __doPostBack('save', payload);

        }

        // ฟังก์ชันที่ใช้กรองข้อมูลจาก hiddenDetailData
        function updateDetailCbo(tableId, selectedGroup, selectedSubGroup) {
            const detailDropdown = document.querySelector(`table[data-table-id="${tableId}"] select.detailCbo`);
            const hiddenDetailData = document.getElementById(`hiddenDetailData${tableId}`);

            // ล้างตัวเลือกเก่า
            detailDropdown.innerHTML = '<option disabled selected value="">-- เลือกรายละเอียด --</option>';

            // หากไม่มีการเลือกกลุ่มหรือ subGroup
            if (!selectedGroup || !selectedSubGroup) {
                detailDropdown.disabled = true;
                return;
            }

            // กรองข้อมูลที่ตรงกับกลุ่มและ subGroup ที่เลือกจาก hiddenDetailData
            const filteredDetails = Array.from(hiddenDetailData.children).filter(item =>
                item.getAttribute('data-group') === selectedGroup &&
                item.getAttribute('data-subgroup') === selectedSubGroup
            );

            // เพิ่มตัวเลือกใหม่ใน dropdown
            filteredDetails.forEach(item => {
                const option = document.createElement('option');
                option.value = item.getAttribute('data-id');
                option.textContent = item.getAttribute('data-text');
                //option.setAttribute("data-subtext", `-- (แนะนำสำหรับ : ${item.getAttribute('data-subtext')})`);
                detailDropdown.appendChild(option);
            });

            detailDropdown.disabled = filteredDetails.length === 0;
            // รีเฟรช selectpicker
            try {
                $('.form-control')?.selectpicker('refresh');
            } catch (e) {
                console.log(e);
            }
        }
        function updateSubGroupCbo(tableId, selectedGroup) {
            const subGroupDropdown = document.querySelector(`table[data-table-id="${tableId}"] select.subGroupCbo`);
            const hiddenDetailData = document.getElementById(`hiddenDetailData${tableId}`);

            // ล้างตัวเลือกเก่า
            subGroupDropdown.innerHTML = '<option disabled selected value="">-- เลือกแผนก --</option>';

            // หากไม่มีการเลือกกลุ่ม
            if (!selectedGroup) {
                subGroupDropdown.disabled = true;
                return;
            }

            // กรองข้อมูล subGroup ที่ตรงกับ group ที่เลือก
            const filteredSubGroups = Array.from(hiddenDetailData.children)
                .filter(item => item.getAttribute('data-group') === selectedGroup)
                .map(item => item.getAttribute('data-subgroup')) // ดึงค่า subgroup
                .filter((value, index, self) => self.indexOf(value) === index); // เอาค่าที่ไม่ซ้ำกัน

            // เพิ่มตัวเลือกใหม่ใน dropdown
            filteredSubGroups.forEach(subGroup => {
                const option = document.createElement('option');
                option.value = subGroup;
                option.textContent = subGroup;
                subGroupDropdown.appendChild(option);
            });

            subGroupDropdown.disabled = filteredSubGroups.length === 0;
            // รีเฟรช selectpicker
            try {
                $('.form-control')?.selectpicker('refresh');
            } catch (e) {
                console.log(e);
            }
        }

        function setupGroupSelect() {
            document.querySelectorAll('table').forEach(table => {
                const groupSelect = table.querySelector('select.groupCbo');
                const subGroupSelect = table.querySelector('select.subGroupCbo');
                const detailSelect = table.querySelector('select.detailCbo');

                // ตั้งค่าเริ่มต้น: Disable subGroup และ detail
                if (groupSelect && !groupSelect.value) {
                    subGroupSelect.disabled = true;
                    detailSelect.disabled = true;
                } else if (subGroupSelect && !subGroupSelect.value) {
                    detailSelect.disabled = true;
                }

                function updateDetails() {
                    const tableId = table.getAttribute('data-table-id');
                    const selectedGroup = groupSelect.value;
                    const selectedSubGroup = subGroupSelect.value;
                    updateDetailCbo(tableId, selectedGroup, selectedSubGroup);
                }

                if (groupSelect) {
                    // ฟังการเปลี่ยนแปลง group
                    groupSelect?.addEventListener('change', function () {
                        const tableId = table.getAttribute('data-table-id');
                        const selectedGroup = groupSelect.value;

                        // อัปเดต subGroup และเปิดใช้งาน
                        updateSubGroupCbo(tableId, selectedGroup);
                        subGroupSelect.disabled = !selectedGroup; // Disable หากไม่มี group เลือก

                        // อัปเดต detail และเปิดใช้งานหากจำเป็น
                        updateDetails();
                        detailSelect.disabled = !selectedGroup; // Disable หากไม่มี group เลือก
                    });
                }

                if (subGroupSelect) {
                    // ฟังการเปลี่ยนแปลง subGroup
                    subGroupSelect.addEventListener('change', function () {
                        updateDetails();
                        detailSelect.disabled = !subGroupSelect.value; // Disable หากไม่มี subGroup เลือก
                    });
                }
            });
        }

    </script>
</asp:Content>
