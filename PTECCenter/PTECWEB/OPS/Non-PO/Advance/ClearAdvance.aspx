﻿<%@ Page Title="ClearAdvance" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="ClearAdvance.aspx.vb" Inherits="PTECCENTER.ClearAdvance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Page.ResolveUrl("~/css/autocomplete.css")%>" rel="stylesheet">

    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
    <link href="<%=Page.ResolveUrl("~/css/card_comment.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/css/print.css")%>" rel="stylesheet" type="text/css">
    <style>
        body {
            background-color: #f0f2f5;
        }

        h3, h5 {
            margin-right: auto;
            margin-left: auto;
        }

        .form-label {
            vertical-align: sub;
        }

        .form-control, .bootstrap-select .dropdown-toggle, .bootstrap-select.form-control {
            /*border: 1px solid #000;*/
            cursor: pointer;
            /*background-color: lightpink;*/
            height: 100%;
            width: 100%;
        }

        .input-detail input {
            background-color: #ffffff !important;
        }

        .company {
            margin-top: 10px;
            padding-left: 0px;
        }

        .company-th {
            margin-top: 15px;
        }

        .logopure {
            /*content: url("http://vpnptec.dyndns.org:10280/OPS_Fileupload/ATT_210800066.png");*/
            width: 200px;
            height: 100px;
            margin-left: 30px;
            margin-top: 10px;
            margin-bottom: 10px;
        }

        table {
            padding: 20px;
            width: 1000px;
            border-radius: 2px;
            border-collapse: separate;
            border-spacing: 0;
            border: 1px;
            background-color: #ffffff;
            table-layout: fixed;
        }

        td, th {
            min-height: 2rem;
            margin: 0;
            border: 1px solid #000;
            white-space: nowrap;
            padding-left: 5px;
        }

        .nonpo, .nonpounsaved, .statusnonpo {
            width: 1000px;
            overflow-x: auto;
            overflow-y: visible;
            padding: 0;
            margin-right: auto;
            margin-left: auto;
        }

            .nonpounsaved input, .statusnonpo input {
                border-top-left-radius: 10px;
                border-top-right-radius: 10px;
                border-bottom-left-radius: 0px;
                border-bottom-right-radius: 0px;
            }


        /*.draggable {
            padding: 1rem;*/
        /*background-color: lightpink;*/
        /*border: 1px solid black;
            cursor: move;
        }

            .draggable.dragging {
                opacity: .5;
            }*/

        /*.deletedetail {
            position: absolute;
            border: 0px solid #000;
            display: none;
        }

        .detail:hover .deletedetail {
            display: inline;
            justify-content: center;
            align-items: center;
        }*/
        .detail td, th {
            font-size: .75rem;
            overflow: hidden;
            text-overflow: ellipsis;
        }


        /*####################### CSS FROM ATTATCH ########################*/
        .attatchItems-link-btndelete .deletedetail {
            font-size: .7rem
        }
        /*####################### END CSS FROM ATTATCH ########################*/

        /*####################### CSS FROM MODAL ########################*/
        .modal .modal-body {
            padding: 2rem;
            padding-top: 1rem;
        }

        .form-group, .form-control, .bootstrap-select .dropdown-toggle, .bootstrap-select .dropdown-menu {
            font-size: 0.875rem;
        }

        .modal-body .btn-light.disabled, .modal-body .btn-light:disabled {
            background-color: #e9ecef;
            border-color: #ced4da;
        }

        .modal .showCost {
            background-color: #f7faff;
            padding: 1rem;
        }
        /*####################### END CSS FROM MODAL ########################*/
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
                        <div class="col">

                            <asp:Button ID="btnSave" class="btn btn-sm  btn-success btnSave" runat="server" Text="Save"   />
                            &nbsp;              
                             <asp:Button ID="btnUpdate" class="btn btn-sm  btn-warning" runat="server" Text="Update"   />
                            &nbsp;   
                            <asp:Button ID="btnConfirm" class="btn btn-sm  btn-secondary" runat="server" Text="Confirm" OnClientClick="Confirm();"   />
                            &nbsp;   
                            <asp:Button ID="btnCancel" class="btn btn-sm  btn-danger" runat="server" Text="Cancel"   />
                            &nbsp;   
                            <% If Not Request.QueryString("NonpoCode") Is Nothing And maintable.Rows.Count > 0 Then%>
                            <% if (maintable.Rows(0).Item("statusid") = 1) Or (maintable.Rows(0).Item("statusid") = 4) Then '1	รอยืนยัน , 4	ขอเอกสารเพิ่มเติม %>
                            <span class="text-red font-weight-bold text-danger">*** (กรุณากด confirm เพื่อยืนยัน) ***</span>
                            <% End If %>
                            <% End If %>
                        </div>

                        <div class="col-auto text-right align-self-center">
                            <%--<button id="btnExport" class="btn btn-sm  btn-info" style="color: #495057;" title="Export" runat="server">
                                <i class="fas fa-file-download"></i>
                            </button>--%>
                            <button type="button" class="btn btn-sm  btn-info noEnterSubmit" style="color: #495057;" title="Export" id="btnExport" runat="server" data-toggle="modal" data-target="#modalExport" data-backdrop="static" data-keyboard="false" data-whatever="new"><i class="fas fa-file-download"></i></button>

                            &nbsp;
                            <button id="btnPrint" class="btn btn-sm  btn-warning" style="color: #495057;" onclick="event.preventDefault();event.stopPropagation();window.print();" title="Print" runat="server">
                                <i class="fas fa-print"></i>
                            </button>
                            <a href="ClearAdvanceMenuList2.aspx" class="btn btn-sm btn-danger ">
                                <i class="fa fa-tasks" aria-hidden="true"></i></a>
                        </div>

                    </div>
                    <hr />
                    <div class="row mb-3">
                        <div class="col-2 text-right">
                            <asp:Label ID="lbcodeRef" CssClass="form-label" AssociatedControlID="codeRef" runat="server" Text="codeRef" />
                        </div>
                        <div class="col-7">
                            <asp:TextBox class="form-control font-weight-bold" ID="codeRef" runat="server" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-2 text-right">
                            <asp:Label ID="lbamount" CssClass="form-label" AssociatedControlID="amount" runat="server" Text="ยอดค้างชำระ" />
                        </div>
                        <div class="col-7">
                            <asp:TextBox class="form-control font-weight-bold text-danger " ID="amount" runat="server" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-2 text-right">
                            <asp:Label ID="lbtxtremark" CssClass="form-label" AssociatedControlID="txtremark" runat="server" Text="รายละเอียด" />
                        </div>
                        <div class="col-7">
                            <asp:TextBox class="form-control font-weight-bold" ID="txtremark" runat="server" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-2 text-right">
                            <asp:Label ID="lbDuedate" CssClass="form-label" AssociatedControlID="txtDuedate" runat="server" Text="Due Date" />
                        </div>
                        <div class="col-7">
                            <div class="input-group">
                                <asp:TextBox type="search" class="form-control font-weight-bold" ID="txtDuedate" runat="server" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="foram">
                        <div class="row">
                            <%=ViewState("status_clearadvance") %>
                        </div>
                        <div class="row">
                            <%=allOwner %>
                        </div>
                        <div class="row">
                            <%=at %>
                        </div>
                        <div class="row">
                            <%=approver %>
                        </div>
                        <div class="row">
                            <%=verifier %>
                        </div>
                        <div class="row">
                            <%=now_action %>
                        </div>
                        <div class="row">
                            บช. ที่ดูแล : <%=account_code %>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="row nonpo">

                            <div class="col nonpounsaved" style="display: none;">
                                <%--<% For i = 0 To detailtable.Rows.Count - 1 %>
                            <% if detailtable.Rows(i).Item("nonpodtl_id") = 0 Then%>--%>
                                <asp:TextBox class="btn btn-warning" ID="txtUnsave" runat="server" ReadOnly="true">ยังไม่บันทึก</asp:TextBox>
                                <%--<% GoTo endprocess %>
                            <% End If %>
                            <% Next i %>
                            <% endprocess: %>--%>
                            </div>
                            <div class="col statusnonpo text-right align-self-center" style="/*display: none; */">
                                <%--<% For i = 0 To detailtable.Rows.Count - 1 %>
                            <% if detailtable.Rows(i).Item("nonpodtl_id") = 0 Then%>--%>
                                <asp:TextBox class="btn btn-warning" ID="statusnonpo" runat="server" ToolTip="ป้ายสถานะ" ReadOnly="true">ยังไม่บันทึก</asp:TextBox>
                                <%--<% GoTo endprocess %>
                            <% End If %>
                            <% Next i %>
                            <% endprocess: %>--%>
                            </div>
                        </div>

                        <div class="nonpo shadow mb-3 table-responsive">

                            <!-- (padding ซ้าย + ขวา = 40px ) -->
                            <!-- (table-width = 1000px ) -->
                            <!--  เนื้อหาข้างใน = 1000px - 40px  = 960 px -->
                            <table class="print ">

                                <!--  colทั้งหทด = 24 col -->
                                <!--  960/24  = 40 px -->
                                <tr>
                                    <!--  18 * 40  = 720 px -->
                                    <td colspan="18" style="width: 720px !important; height: 10px">
                                        <div class="row">
                                            <div class="col-3">
                                                <img runat="server" id="logo" class="logopure" src="#" alt="logopure" width="500" height="600">
                                            </div>
                                            <div class="col-9 company">
                                                <div class="row company-th">
                                                    <h3 runat="server" id="company_th">บริษัท เพียวพลังงานไทย จำกัด</h3>
                                                </div>
                                                <div class="row company-en">
                                                    <h5 runat="server" id="company_en">PURE THAI ENERGY COMPANY LIMITED</h5>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                    <!--  6 * 40  = 240 px -->
                                    <td colspan="6" style="width: 240px !important;">
                                        <div class="row">
                                            <h5>ADVANCE CLEARING</h5>
                                        </div>
                                        <div class="row ">
                                            <div class="col text-center">
                                                <span id="txtSubtitle" runat="server"></span>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>


                                    <td colspan="12" style="width: 420px !important;">
                                        <div class="row">

                                            <div class="col-1">
                                                <asp:Label ID="Label2" CssClass="form-label" AssociatedControlID="cboCreateby" runat="server" Text="ผู้เบิก" />
                                            </div>
                                            <div class="col-11">

                                                <asp:DropDownList class="form-control" ID="cboCreateby" runat="server" readonly="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td colspan="6" style="width: 240px !important;">
                                        <div class="row">

                                            <div class="col-2">
                                                <asp:Label ID="lbcboBranch" CssClass="form-label" AssociatedControlID="cboBranch" runat="server" Text="สาขา" />
                                            </div>
                                            <div class="col-10">
                                                <asp:DropDownList class="form-control" ID="cboBranch" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td colspan="6" style="width: 240px !important;">
                                        <div class="row">

                                            <div class="col-3">
                                                <asp:Label ID="lbtxtadvno" CssClass="form-label" AssociatedControlID="txtadvno" runat="server" Text="เลขที่" />
                                            </div>
                                            <div class="col-9">
                                                <asp:TextBox class="form-control" ID="txtadvno" runat="server" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="width: 240px !important;">
                                        <div class="row">
                                            <div class="col-2">
                                                <asp:Label ID="Label3" CssClass="form-label" AssociatedControlID="cboDepartment" runat="server" Text="ฝ่าย" />
                                            </div>
                                            <div class="col-10">
                                                <asp:DropDownList class="form-control" ID="cboDepartment" AutoPostBack="True"
                                                    runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td colspan="6" style="width: 240px !important;">
                                        <div class="row">
                                            <div class="col-2">
                                                <asp:Label ID="lbApprovalcode" CssClass="form-label" AssociatedControlID="cboSection" runat="server" Text="แผนก" />
                                            </div>
                                            <div class="col-10">
                                                <asp:DropDownList class="form-control" ID="cboSection" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>

                                    <td colspan="6" style="width: 240px !important;">
                                        <div class="row">
                                            <div class="col-3">
                                                <asp:Label ID="lbCompany" CssClass="form-label" AssociatedControlID="cboCompany" runat="server" Text="สังกัด" />
                                            </div>
                                            <div class="col-9">
                                                <asp:DropDownList class="form-control " ID="cboCompany" runat="server" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td colspan="6" style="width: 240px !important;">
                                        <div class="row">
                                            <div class="col-3">
                                                <asp:Label ID="Label7" CssClass="form-label" AssociatedControlID="txtCreateDate" runat="server" Text="วันที่" />
                                            </div>
                                            <div class="col-9">
                                                <asp:TextBox class="form-control font-weight-bold" ID="txtCreateDate" runat="server" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="24" style="width: 960px !important; height: 50px;">
                                        <div class="row">
                                            <div class="col-1">
                                                <asp:Label ID="lbcboOwner" CssClass="form-label" AssociatedControlID="cboOwner" runat="server" Text="ผู้รับเงิน" />
                                            </div>
                                            <div class="col-11">
                                                <asp:DropDownList class="form-control" ID="cboOwner" runat="server" readonly="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="24" style="width: 960px !important; height: 50px;">
                                        <div class="row">
                                            <h5 class="m-auto" id="txtSwear" runat="server">ข้าพเจ้าขอเคลียร์เงินยืม (Cash Advance) ที่เบิกจากบริษัทฯ ไปแล้ว ดังรายละเอียดต่อไปนี้</h5>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="text-center" rowspan="2" colspan="2" style="width: 80px !important;">รหัสบัญชี</th>
                                    <th class="text-center" rowspan="2" colspan="6" style="width: 240px !important;">รายละเอียด</th>
                                    <th class="text-center" rowspan="2" colspan="4" style="width: 160px !important;">Vendor</th>
                                    <th class="text-center" colspan="6" style="width: 180px !important;">Dimension</th>
                                    <th class="text-center" rowspan="2" colspan="3" style="width: 120px !important;">จำนวนเงิน</th>
                                    <th class="text-center" rowspan="2" colspan="1" style="width: 40px !important;">
                                        <div class="row">
                                            <div class="col">
                                                %Vat
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col">
                                                <input class="form-check-input chk-img-after" style="margin-left: -0.8rem;" type="checkbox" id="chkVat" runat="server">
                                                <asp:Label ID="Label6" CssClass="form-check-label" AssociatedControlID="chkVat" runat="server" Text="รอ" />
                                            </div>
                                        </div>
                                    </th>
                                    <th class="text-center" rowspan="2" colspan="1" style="width: 40px !important; text-overflow: unset; padding-left: 0px;">%WHT</th>

                                    <th class="text-center gropincompletebill pl-0" rowspan="2" colspan="1" style="width: 40px !important;">Bill</th>
                                </tr>
                                <tr>
                                    <th class="text-center" colspan="2" style="width: 80px !important;">BU.</th>
                                    <th class="text-center" colspan="2" style="width: 80px !important;">PP.</th>
                                    <th class="text-center" colspan="2" style="width: 80px !important;">PJ.</th>

                                </tr>
                                <!--  ############## Detail ############### -->
                                <tbody class="DetailArea">

                                    <% For i = 0 To detailtable.Rows.Count - 1 %>
                                    <tr class="draggable detail" <%--draggable="true"--%> data-status="<%= detailtable.Rows(i).Item("status").ToString() %>" name="<%= detailtable.Rows(i).Item("row").ToString() %>" ondblclick="btnEditDetailClick('<%= detailtable.Rows(i).Item("row").ToString() %>','<%= detailtable.Rows(i).Item("nonpodtl_id").ToString() %>','<%= detailtable.Rows(i).Item("accountcodeid").ToString() %>'
                                                                                        ,'<%= detailtable.Rows(i).Item("depid").ToString() %>','<%= detailtable.Rows(i).Item("buid").ToString() %>'
                                                                                        ,'<%= detailtable.Rows(i).Item("ppid").ToString() %>','<%= detailtable.Rows(i).Item("pjid").ToString() %>','<%= detailtable.Rows(i).Item("cost").ToString() %>'
                                                                                        ,'<%= detailtable.Rows(i).Item("vat_per").ToString() %>','<%= detailtable.Rows(i).Item("tax_per").ToString() %>'
                                                                                        ,'<%= detailtable.Rows(i).Item("detail").ToString() %>','<%= detailtable.Rows(i).Item("vendorcode").ToString() %>'
                                                                                        ,'<%= detailtable.Rows(i).Item("invoice").ToString() %>','<%= detailtable.Rows(i).Item("taxid").ToString() %>','<%= detailtable.Rows(i).Item("invoicedate").ToString() %>'
                                                                                        ,'<%= detailtable.Rows(i).Item("nobill").ToString() %>','<%= detailtable.Rows(i).Item("incompletebill").ToString() %>');">
                                        <%--<tr class="draggable detail" name="<%= detailtable.Rows(i).Item("row").ToString() %>">--%>
                                        <td colspan="2" style="width: 80px !important; height: 22px; text-align: center;" title="<%= detailtable.Rows(i).Item("accountcode").ToString() %>"><%= if((detailtable.Rows(i).Item("accountcodeid").ToString()) = "0", "", Left(detailtable.Rows(i).Item("accountcodeid").ToString(), 6)) %></td>
                                        <td colspan="6" style="width: 240px !important;" title="<%= detailtable.Rows(i).Item("detail").ToString() %>"><span><%= detailtable.Rows(i).Item("detail").ToString() %></span></td>
                                        <%--<td colspan="2" style="width: 80px !important;" title="<%= detailtable.Rows(i).Item("depname").ToString() %>"><%= detailtable.Rows(i).Item("depname").ToString() %></td>--%>
                                        <td colspan="4" style="width: 160px !important;" title="<%= detailtable.Rows(i).Item("vendorcode").ToString() %>"><%= detailtable.Rows(i).Item("vendorcode").ToString() %>  </td>
                                        <td colspan="2" style="width: 80px !important; text-align: center;" title="<%= detailtable.Rows(i).Item("buname").ToString() %>"><%= detailtable.Rows(i).Item("buname").ToString() %></td>
                                        <td colspan="2" style="width: 80px !important; text-align: center;" title="<%= detailtable.Rows(i).Item("ppname").ToString() %>"><%= detailtable.Rows(i).Item("ppname").ToString() %></td>
                                        <td colspan="2" style="width: 80px !important; text-align: center;" title="<%= detailtable.Rows(i).Item("pjname").ToString() %>"><%= detailtable.Rows(i).Item("pjname").ToString() %></td>
                                        <td colspan="3" style="width: 120px !important; text-align: right;" title="<%= detailtable.Rows(i).Item("cost").ToString() %>"><%= if((detailtable.Rows(i).Item("cost").ToString()) = "0", "", String.Format("{0:n2}", detailtable.Rows(i).Item("cost"))) %>
                                        </td>
                                        <td colspan="1" style="width: 40px !important; text-align: center;" title="<%= detailtable.Rows(i).Item("cost") * detailtable.Rows(i).Item("vat_per") / 100 %>"><%= detailtable.Rows(i).Item("vat_per").ToString() %></td>
                                        <td colspan="1" style="width: 40px !important; text-align: center;" title="<%= detailtable.Rows(i).Item("cost") * detailtable.Rows(i).Item("tax_per") / 100 %>"><%= detailtable.Rows(i).Item("tax_per").ToString() %></td>
                                        <td class="text-center gropincompletebill" colspan="1" style="width: 40px !important; text-align: right; padding-right: 5px;"><%= if( (Not detailtable.Rows(i).Item("nobill") And Not detailtable.Rows(i).Item("incompletebill")), "", If(detailtable.Rows(i).Item("nobill"), "N", "U")) %></td>
                                        <td class="deletedetail notprint" style="position: absolute; border: 0px solid #000;">
                                            <div>
                                                <a onclick="confirmDeletedetail('<%= detailtable.Rows(i).Item("nonpodtl_id").ToString() %>','<%= detailtable.Rows(i).Item("row").ToString() %>')" class="btn btn-sm p-0 notPrint">
                                                    <i class="fas fa-times"></i>
                                                </a>
                                            </div>
                                        </td>
                                    </tr>
                                    <% Next i %>
                                </tbody>
                                <tr class="text-center notPrint " runat="server" id="FromAddDetail">
                                    <td colspan="24" style="width: 960px !important;">
                                        <button type="button" class="btn btn-sm  btn-outline-info w-100 noEnterSubmit" id="btnFromAddDetail" runat="server" data-toggle="modal" data-target="#exampleModal" data-backdrop="static" data-keyboard="false" data-whatever="new">เพิ่มรายการ</button>
                                    </td>
                                </tr>

                                <!--  ############## End Detail ############### -->
                                <tfoot>
                                    <!--  total -->
                                    <tr>
                                        <td colspan="8" style="width: 320px !important;">
                                            <div class="row">
                                                <div class="col-11" style="margin-left: auto;">
                                                    <input class="form-check-input chk-img-after" type="checkbox" id="chkpayBack" name="pay[1][]" runat="server">
                                                    <asp:Label ID="lbchkpayBack" CssClass="form-check-label" AssociatedControlID="chkpayBack" runat="server" Text="คืนเงินบริษัท" />
                                                </div>
                                            </div>
                                        </td>
                                        <td colspan="4" style="width: 160px !important;" id="payBack">
                                            <div class="row">
                                                <div class="col">
                                                    <asp:TextBox class="form-control noEnterSubmit text-right" type="number" ID="txtamountpayBack" runat="server" min="0" Text="0"></asp:TextBox>
                                                    <div class="invalid-feedback">* ตัวเลขจำนวนเต็ม</div>
                                                </div>
                                            </div>
                                        </td>
                                        <td colspan="8" style="width: 320px !important; text-align: right; padding-right: 5px; border-bottom-width: 0px;">
                                            <h6>รวม
                                            </h6>
                                        </td>
                                        <td colspan="4" style="width: 160px !important; text-align: right;" id="total_amount"><%= total_cost %></td>
                                    </tr>
                                    <tr>
                                        <td class="d-none" colspan="9" style="width: 360px !important;">
                                            <div class="row">
                                                <div class="col-11" style="margin-left: auto;">
                                                    <input class="form-check-input chk-img-after" type="checkbox" id="chkdeductSell" name="pay[1][]" runat="server">
                                                    <asp:Label ID="lbchkdeductSell" CssClass="form-check-label" AssociatedControlID="chkdeductSell" runat="server" Text="หักยอดขาย" />
                                                </div>
                                            </div>
                                        </td>
                                        <td class="d-none" colspan="4" style="width: 160px !important;" id="deduct_sell">
                                            <div class="row">
                                                <div class="col">
                                                    <asp:TextBox class="form-control noEnterSubmit text-right" type="number" ID="txtamountdedusctsell" runat="server" min="0" Text="0"></asp:TextBox>
                                                    <div class="invalid-feedback">* ตัวเลขจำนวนเต็ม</div>
                                                </div>
                                            </div>
                                        </td>
                                        <td rowspan="2" colspan="16" style="width: 640px !important; vertical-align: text-top; color: #065ca9 !important; font-size: .8rem; border-top-width: 0px; border-right-width: 0px; border-bottom-width: 0px;">
                                            <div class="gropincompletebill">
                                                <p class="text-break mb-0">
                                                    <br />
                                                    ข้าพเจ้าขอรับรองว่า ได้ใช้จ่ายเงินจริงตามที่ระบุไว้ในเอกสารนี้ทั้งหมด<br />
                                                    เพื่อกิจการของบริษัทฯ แต่ไม่สามารถนำส่งใบเสร็จรับเงินตามระเบียบของบริษัทได้<br />
                                                </p>
                                                <p class="text-left" style="margin-left: 30px;">
                                                    U = บิลไม่สมบูรณ์<br />
                                                    N = ไม่มีบิล<br />
                                                </p>
                                            </div>
                                        </td>

                                        <td colspan="4" style="width: 160px !important; text-align: right; padding-right: 5px; border-bottom-width: 0px; border-top-width: 0px; border-left-width: 0px;">
                                            <h6>VAT
                                            </h6>
                                        </td>
                                        <td colspan="4" style="width: 160px !important; text-align: right;" id="total_tax"><%= total_vat %></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="width: 160px !important; text-align: right; padding-right: 5px; border-bottom-width: 0px; border-top-width: 0px; border-left-width: 0px;">
                                            <h6>หัก WHT
                                            </h6>
                                        </td>
                                        <td colspan="4" style="width: 160px !important; text-align: right;" id="total_vat"><%= total_tax %></td>
                                    </tr>
                                    <tr>
                                        <td colspan="20" style="width: 800px !important; text-align: right; border-bottom-width: 0px; border-top-width: 0px;">
                                            <h6>Purecard</h6>
                                        </td>
                                        <td colspan="4" style="width: 160px !important; text-align: right;" id="total_purecard"><%= total_purecard %><span class="d-none" runat="server" id="lbTotal_purecard"></span></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="width: 80px !important; text-align: right; border-right-width: 0px; border-top-width: 0px;">
                                            <h5></h5>
                                        </td>
                                        <td colspan="14" style="width: 560px !important; text-align: center; border-left-width: 0px; border-top-width: 0px; border-right-width: 0px;">
                                            <h5>
                                                <p id="id01"></p>
                                            </h5>
                                        </td>
                                        <td colspan="4" style="width: 160px !important; text-align: right; border-left-width: 0px; border-top-width: 0px; padding-right: 5px;">
                                            <b>
                                                <h6>รวมทั้งสิ้น</h6>
                                            </b>
                                        </td>
                                        <td colspan="4" style="width: 160px !important; text-align: right;" id="total"><%= total %></td>
                                    </tr>

                                    <tr>
                                        <td colspan="20" style="width: 800px !important; text-align: right; padding-right: 5px; border-top-width: 0px;">
                                            <h6>รวมยอดขอเคลียร์ทั้งหมด</h6>
                                        </td>
                                        <td colspan="4" style="width: 160px !important; text-align: right;" id="total_clear"><%= total_clear %></td>
                                    </tr>
                                    <!--  end total -->
                                    <tr>
                                        <td colspan="24" style="width: 900px !important; height: 22px; text-align: center; border-left-width: 0px; border-right-width: 0px; border-top-width: 0px;"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="width: 240px !important; border-bottom-width: 0px;">
                                            <h5>ผู้เบิก</h5>
                                        </td>
                                        <td colspan="6" style="width: 240px !important; border-bottom-width: 0px;">
                                            <h5>ผู้รับสินค้า/บริการ</h5>
                                        </td>
                                        <td colspan="6" style="width: 240px !important; border-bottom-width: 0px;">
                                            <h5>ผู้ตรวจ</h5>
                                        </td>
                                        <td colspan="6" style="width: 240px !important; border-bottom-width: 0px;">
                                            <h5>ผู้อนุมัติ</h5>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="width: 240px !important; border-top-width: 0px; border-bottom-width: 0px; text-align: center;">
                                            <h5><b><% If Not Request.QueryString("NonpoCode") Is Nothing And maintable.Rows.Count > 0 Then%> <%=maintable.Rows(0).Item("withdraw_by") %><% End If %></b></h5>
                                        </td>
                                        <td colspan="6" style="width: 240px !important; border-top-width: 0px; border-bottom-width: 0px; text-align: center;">
                                            <h5><b><% If Not Request.QueryString("NonpoCode") Is Nothing And maintable.Rows.Count > 0 Then%> <%=maintable.Rows(0).Item("service_by") %><% End If %></b></h5>
                                        </td>
                                        <td colspan="6" style="width: 240px !important; border-top-width: 0px; border-bottom-width: 0px; text-align: center;">
                                            <h5><b><% If Not Request.QueryString("NonpoCode") Is Nothing And maintable.Rows.Count > 0 Then%> <%=maintable.Rows(0).Item("verify_by") %><% End If %></b></h5>
                                        </td>
                                        <td colspan="6" style="width: 240px !important; border-top-width: 0px; border-bottom-width: 0px; text-align: center;">
                                            <h5><b><% If Not Request.QueryString("NonpoCode") Is Nothing And maintable.Rows.Count > 0 Then%> <%=maintable.Rows(0).Item("approval_by") %><% End If %></b></h5>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="width: 240px !important; border-top-width: 0px;">วันที่ <% If Not Request.QueryString("NonpoCode") Is Nothing And maintable.Rows.Count > 0 Then%>
                                            <span style="margin-left: 40px;"><%=maintable.Rows(0).Item("withdraw_date") %></span>
                                            <% End If %></td>
                                        <td colspan="6" style="width: 240px !important; border-top-width: 0px;">วันที่ <% If Not Request.QueryString("NonpoCode") Is Nothing And maintable.Rows.Count > 0 Then%>
                                            <span style="margin-left: 40px;"><%=maintable.Rows(0).Item("service_date") %></span>
                                            <% End If %></td>
                                        <td colspan="6" style="width: 240px !important; border-top-width: 0px;">วันที่ <% If Not Request.QueryString("NonpoCode") Is Nothing And maintable.Rows.Count > 0 Then%>
                                            <span style="margin-left: 40px;"><%=maintable.Rows(0).Item("verify_date") %></span>
                                            <% End If %></td>
                                        <td colspan="6" style="width: 240px !important; border-top-width: 0px;">วันที่ <% If Not Request.QueryString("NonpoCode") Is Nothing And maintable.Rows.Count > 0 Then%>
                                            <span style="margin-left: 40px;"><%=maintable.Rows(0).Item("approval_date") %></span>
                                            <% End If %></td>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                    </div>
                    <!-- End row -->


                    <hr />

                    <div class="row">

                        <% If Not Request.QueryString("NonpoCode") Is Nothing And maintable.Rows.Count > 0 Then%>
                        <% if ViewState("status_clearadvance") = "write" And (maintable.Rows(0).Item("statusid") = 2 Or maintable.Rows(0).Item("statusid") = 15) Then ' 2	รออนุมัติ , 15	รอตรวจสอบ%>
                        <div class="text-center m-auto">
                            <% If approval And maintable.Rows(0).Item("statusid") = 2 Then ' 2	รออนุมัติ%>
                            <asp:Button ID="btnApproval" class="btn btn-success" runat="server" Text="อนุมัติ"   />
                            <% End If %>
                            <% If verify And maintable.Rows(0).Item("statusid") = 2 Then ' 2	รออนุมัติ%>
                            <asp:Button ID="btnVerify" class="btn btn-warning" runat="server" Text="ยืนยันการตรวจสอบ"   />
                            <% End If %>
                            <% If ((verify Or approval)) Then%>
                            <asp:Button ID="btnDisApproval" class="btn btn-danger" runat="server" Text="ไม่อนุมัติ"   />
                            <% End If %>
                        </div>
                        <% End If %>
                        <% End If %>
                    </div>

                    <div class="row notPrint" id="card_attatch" runat="server">
                        <div class="col-md-6 mt-3">
                            <div class="card shadow card_attatch">
                                <div class="card-header">
                                    เอกสารแนบ
                                </div>
                                <div class="card-body attatchItems">
                                    <%--begin Attatch item--%>

                                    <% For i = 0 To AttachTable.Rows.Count - 1 %>
                                    <div class="row">
                                        <% If Not Request.QueryString("NonpoCode") Is Nothing And maintable.Rows.Count > 0 And (Session("depid").ToString = "2" Or Session("depid").ToString = "4" Or Session("depid").ToString = "24" Or Session("depid").ToString = "25") Then%>
                                        <% If maintable.Rows(0).Item("statusid") = 7 Then '7	รอบัญชีตรวจสอบ%>
                                        <div class="col-1">
                                            <div class="form-check">
                                                <input class="form-check-input" type="checkbox" id="<%= AttachTable.Rows(i).Item("id") %>" onclick="chkAttach(this,'<%= Session("userid") %>')">
                                            </div>
                                        </div>
                                        <% End If %>
                                        <% End If %>
                                        <div class="attatchItems-link-btndelete" id="ATT<%= AttachTable.Rows(i).Item("id") %>">
                                            <div class="col-auto">
                                                <a href="<%= Page.ResolveUrl(AttachTable.Rows(i).Item("url").ToString()) %>" class="text-primary listCommentAndAttatch " style="cursor: pointer;" target="_blank">
                                                    <span><%= AttachTable.Rows(i).Item("show").ToString() %></span></a>

                                                <a onclick="removeAttach('<%= AttachTable.Rows(i).Item("id") %>','<%= Session("userid") %>');" class="btn btn-sm pt-0 text-danger deletedetail">
                                                    <i class="fas fa-times"></i>
                                                </a>
                                            </div>

                                        </div>
                                    </div>
                                    <%-- end Attatch item--%>
                                    <% Next i %>
                                </div>
                                <div class="card-footer">
                                    <div id="btnAddAttatch" runat="server">
                                        <a onclick="addAttach()" id="btnAddNewAttatch" runat="server" class="text-primary" style="cursor: pointer; transition: .2s;">
                                            <i class="fas fa-plus-circle"></i><span>&nbsp;แนบลิ้งเอกสาร</span></a>
                                        <a href="#" id="btnAddAttatch2" runat="server" title="addAttach" data-toggle="modal" data-target="#chooseMyfile">เลือกจากคลังไฟล์...</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6 mt-3" id="card_comment" runat="server">
                            <div class="card shadow card_comment">
                                <div class="table-responsive">
                                    <div class="card-header">
                                        แสดงความคิดเห็น
                                    </div>
                                    <div class="card-body comments">
                                        <%--begin item--%>


                                        <% For i = 0 To CommentTable.Rows.Count - 1 %>
                                        <div class="comment-detail mb-2">

                                            <div class="row">
                                                <div class="col-auto font-weight-bolder" style="font-size: 1rem; display: flex; justify-content: flex-start; align-items: center;">
                                                    <%= CommentTable.Rows(i).Item("CreateBy").ToString() %>
                                                </div>
                                                <% If CommentTable.Rows(i).Item("CreateBy").ToString = Session("username").ToString Then %>
                                                <div class="col-auto">
                                                    <a onclick="btnEditCommentClick('<%= CommentTable.Rows(i).Item("commentid").ToString() %>')" style="display: none;" class="btn btn-sm editComment">
                                                        <i class="fas fa-pen"></i>
                                                    </a>&nbsp;
                                                    <a onclick="confirmDelete('<%= CommentTable.Rows(i).Item("commentid").ToString() %>','<%= Session("userid") %>')" class="btn btn-sm deleteComment">
                                                        <i class="fas fa-trash-alt"></i>
                                                    </a>
                                                </div>
                                                <% End If %>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12 text-muted" style="font-size: .75rem;">
                                                    <%= CommentTable.Rows(i).Item("CreateDate").ToString() %>
                                                </div>
                                            </div>
                                            <div class="row commentDetail">
                                                <div contenteditable="false" class="col-md-12 detailComment" id="<%= CommentTable.Rows(i).Item("commentid").ToString() %>" style="font-size: 1rem;" onblur="cancelEditComment(this,'<%= CommentTable.Rows(i).Item("commentdetail").ToString() %>');" onkeydown="checkEditcomment(this,event,'255','<%= CommentTable.Rows(i).Item("commentdetail").ToString() %>');">
                                                    <span>
                                                        <%= CommentTable.Rows(i).Item("commentdetail").ToString() %>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                        <%-- end detail row--%>
                                        <% Next i %>
                                    </div>

                                    <div class="card-footer">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <asp:TextBox class="form-control bg-white" ID="txtComment" runat="server" Style="cursor: auto;" Rows="2" Columns="50" TextMode="MultiLine" onkeyup="stoppedTyping();" placeholder="Comment . ." value="" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row justify-content-center">
                                            <div class="col-md-12">
                                                <asp:Button ID="btnSaveComment" class="btn btn-primary w-100" runat="server" Text="Post" AutoPostBack="True" disabled   />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%-- end item--%>
                            </div>
                            <!-- end card-->
                        </div>
                    </div>
                </div>



            </div>

        </div>
    </div>
    <div class="row btn-operator justify-content-center notPrint">
        <% If Not Request.QueryString("NonpoCode") Is Nothing And maintable.Rows.Count > 0 And (Session("depid").ToString = "2" Or Session("depid").ToString = "4" Or Session("depid").ToString = "24" Or Session("depid").ToString = "25") Then%>
        <% If maintable.Rows(0).Item("statusid") = 7 Then%>
        <!-- 7 = รอบช.ตรวจ-->
        <button class="btn btn-sm " style="color: #39cd5b; font-size: 3rem; position: fixed; bottom: 9rem; right: 1rem;" id="btnPass" runat="server" title="ผ่านการตรวจสอบจาก บช.">
            <i class="fas fa-check-circle shadow" style="border-radius: 100%;"></i>
        </button>
        <button class="btn btn-sm " style="color: #b8c5d1; font-size: 3rem; position: fixed; bottom: 5rem; right: 1rem;" id="btnEdit" runat="server" title="ขอเอกสารเพิ่มเติม">
            <i class="fas fa-pause-circle shadow" style="border-radius: 100%;"></i>
        </button>
        <button class="btn btn-sm " style="color: #dc3545; font-size: 3rem; position: fixed; bottom: 1rem; right: 1rem;" id="btnReject" runat="server" title="ยกเลิกใบงาน">
            <i class="fas fa-times-circle" style="border-radius: 100%;"></i>
        </button>
        <% ElseIf maintable.Rows(0).Item("statusid") = 8 Then %>
        <!-- 8 = รอเอกสารตัวจริง-->
        <button class="btn btn-sm " style="color: #ffc107; font-size: 3rem; position: fixed; bottom: 1rem; right: 1rem;" id="btnWDoc" runat="server" title="ยืนยันรับเอกสาร">
            <i class="fas fa-check-circle shadow" style="border-radius: 100%;"></i>
        </button>
        <% End If %>
        <% End If %>
    </div>
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
                    <!--  ##############  Detail ############### -->
                    <input type="hidden" class="form-control" id="row" value="0" runat="server">
                    <input type="hidden" class="form-control" id="nextrow" value="0" runat="server">
                    <input type="hidden" class="form-control" id="hiddenAdvancedetailid" value="0" runat="server">
                    <div class="row">
                        <div class="col d-flex flex-row-reverse">
                            <a href="#" runat="server" id="btnAddRef" data-target="#exampleModalToggle2" data-toggle="modal" data-dismiss="modal" data-backdrop="static" data-keyboard="false">เลือกจากรายการ..</a>
                        </div>
                    </div>
                    <div class="form-group ">
                        <asp:Label ID="lbcboAccountCode" CssClass="form-label" AssociatedControlID="cboAccountCode" runat="server" Text="รหัสบัญชี" />
                        <asp:Label ID="lbcboAccountCodeMandatory" CssClass="text-danger" AssociatedControlID="cboAccountCode" runat="server" Text="*" />
                        <asp:DropDownList class="form-control" ID="cboAccountCode" runat="server" onchange="setdetail(this);"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbDetail" CssClass="form-label" AssociatedControlID="txtDetail" runat="server" Text="รายละเอียด" />
                        <asp:TextBox class="form-control" ID="txtDetail" runat="server" Rows="3" Columns="50" TextMode="MultiLine" autocomplete="off"></asp:TextBox>
                        <div class="invalid-feedback">กรุณากรอกรายละเอียด</div>
                    </div>
                    <%-- <div class="form-group">
                        <asp:Label ID="Label4" CssClass="form-label" AssociatedControlID="cboDep" runat="server" Text="Department" />
                        <asp:DropDownList class="form-control" ID="cboDep" runat="server"></asp:DropDownList>
                    </div>--%>

                    <div class="form-group">
                        <asp:Label ID="lbBU" CssClass="form-label" AssociatedControlID="cboBU" runat="server" Text="Business Unit" />
                        <asp:DropDownList class="form-control" ID="cboBU" runat="server"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbPP" CssClass="form-label" AssociatedControlID="cboPP" runat="server" Text="Purpose" />
                        <asp:DropDownList class="form-control" ID="cboPP" runat="server"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbPJ" CssClass="form-label" AssociatedControlID="cboPJ" runat="server" Text="Project" />
                        <asp:DropDownList class="form-control" ID="cboPJ" runat="server"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbPrice" CssClass="form-label" AssociatedControlID="txtPrice" runat="server" Text="จำนวนเงิน (ก่อน VAT)" />
                        <asp:TextBox class="form-control noEnterSubmit" type="number" ID="txtPrice" runat="server" Text="0" onkeyup="setnetprice();calculate();"></asp:TextBox>
                        <div class="invalid-feedback">* ตัวเลขจำนวนเต็ม</div>
                    </div>
                    <div class="row flex-nowrap">
                        <div class="form-group ">
                            <div class="col">
                                <asp:Label ID="Label4" CssClass="form-label" AssociatedControlID="txtVat" runat="server" Text="VAT (%)" />
                            </div>
                            <div class="col">
                                <asp:TextBox class="form-control noEnterSubmit" type="number" ID="txtVat" runat="server" min="0" Text="0" onkeyup="setprice();calculate();"></asp:TextBox>
                            </div>
                            <div class="invalid-feedback">* ตัวเลขจำนวนเต็ม</div>
                        </div>
                        <div class="form-group ">
                            <div class="col">
                                <asp:Label ID="Label5" CssClass="form-label" AssociatedControlID="txtTax" runat="server" Text="WHT (%)" />
                            </div>
                            <div class="col">
                                <asp:TextBox class="form-control noEnterSubmit" type="number" ID="txtTax" runat="server" min="0" Text="0" onkeyup="setprice();calculate();"></asp:TextBox>
                                <div class="invalid-feedback">* ตัวเลขจำนวนเต็ม</div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbNetPrice" CssClass="form-label" AssociatedControlID="TxtNetPrice" runat="server" Text="จำนวนเงิน (รวม VAT)" />
                        <asp:TextBox class="form-control noEnterSubmit" type="number" ID="TxtNetPrice" runat="server" Text="0" onkeyup="setprice();calculate();"></asp:TextBox>
                        <div class="invalid-feedback">* ตัวเลขจำนวนเต็ม</div>
                    </div>
                    <%--<div class="form-group">
                        <asp:Label ID="lbVat" CssClass="form-label" AssociatedControlID="txtVat" runat="server" Text="VAT" />
                        <asp:TextBox class="form-control noEnterSubmit" type="number" ID="txtVat" runat="server" Text="0"'></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbTax" CssClass="form-label" AssociatedControlID="txtPrice" runat="server" Text="TAX" />
                        <asp:TextBox class="form-control noEnterSubmit" type="number" ID="txtTax" runat="server" Text="0"'></asp:TextBox>
                    </div>--%>

                    <div class="form-group" style="display: none;">
                        <asp:Label ID="Label1" CssClass="form-label" AssociatedControlID="cboDep" runat="server" Text="cboDep" />
                        <asp:DropDownList class="form-control" ID="cboDep" runat="server"></asp:DropDownList>
                    </div>
                    <div class="showCost">
                        <p class="text-muted" id="p_vat"></p>
                        <p class="text-muted" id="p_tax"></p>
                        <p class="text-muted  font-weight-bold" id="p_cost"></p>
                    </div>
                    <div class="form-group autocomplete">
                        <asp:Label ID="lbcboVendor" CssClass="form-label" AssociatedControlID="cboVendor" runat="server" Text="Vendor" />
                        <asp:DropDownList class="form-control d-none" ID="cboVendor" runat="server" onchange="setVendor(this);"></asp:DropDownList>
                        <asp:TextBox class="form-control" ID="txtVendor" runat="server" TextMode="MultiLine" Rows="1" autocomplete="off"></asp:TextBox>
                    </div>
                    <!--  ############## End Detail ############### -->
                    <hr />
                    <h3>ใบแจ้งหนี้ / ใบส่งของ / ใบกำกับ</h3>

                    <div class="form-group">
                        <asp:Label ID="lbtaxid" CssClass="form-label" AssociatedControlID="txttaxid" runat="server" Text="Tax ID no." />
                        <asp:TextBox class="form-control noEnterSubmit" type="search" ID="txttaxid" runat="server" autocomplete="off"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <div class="row justify-content-between">
                            <div class="col">
                                <asp:Label ID="lbinvoiceno" CssClass="form-label" AssociatedControlID="txtinvoiceno" runat="server" Text="Document no." />
                            </div>
                            <div class="col gropincompletebill">
                                <div class="row flex-nowrap form-group">
                                    <div class="pr-4" style="color: #0f66c4;">
                                        <input class="form-check-input" type="checkbox" id="chkNoBill" runat="server">
                                        <asp:Label ID="lbchkNoBill" CssClass="form-check-label" AssociatedControlID="chkNoBill" runat="server" Text="ไม่มีบิล (N)" />
                                    </div>
                                    <div class="pl-4" style="color: #0f66c4;">
                                        <input class="form-check-input" type="checkbox" id="chkIncompleteBill" runat="server">
                                        <asp:Label ID="lbchkIncompleteBill" CssClass="form-check-label" AssociatedControlID="chkIncompleteBill" runat="server" Text="บิลไม่สมบูรณ์ (U)" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:TextBox class="form-control noEnterSubmit" type="search" ID="txtinvoiceno" runat="server" autocomplete="off"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbinvoicedate" CssClass="form-label" AssociatedControlID="txtinvoicedate" runat="server" Text="Document date" />
                        <asp:TextBox class="form-control noEnterSubmit" type="input" ID="txtinvoicedate" runat="server" placeholder="--- คลิกเพื่อเลือก ---" autocomplete="off"></asp:TextBox>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary noEnterSubmit" data-dismiss="modal">Close</button>
                    <%--<button type="button" id="btnAddDetail" class="btn btn-primary noEnterSubmit">Save</button>--%>
                    <asp:Button ID="btnAddDetails" class="btn btn-primary" runat="server" Text="Save" OnClientClick="postBack_addDetail();"   />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalExport" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel_report" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel_report">ลายละเอียดรายงาน</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group pl-5">
                        <input class="form-check-input chk-img-after" type="checkbox" id="chkGroupVAT" runat="server">
                        <asp:Label ID="Label8" CssClass="form-check-label" AssociatedControlID="chkGroupVAT" runat="server" Text="รวบ VAT" />
                    </div>
                    <div class="form-group pl-5">
                        <input class="form-check-input chk-img-after" type="checkbox" id="chkGroupVendor" runat="server">
                        <asp:Label ID="Label10" CssClass="form-check-label" AssociatedControlID="chkGroupVendor" runat="server" Text="รวบ Vendor" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary noEnterSubmit" data-dismiss="modal">Close</button>
                    <%--<button type="button" id="btnAddDetail" class="btn btn-primary noEnterSubmit">Save</button>--%>

                    <asp:Button ID="btnDowload" class="btn btn-primary" runat="server" Text="Dowload"   />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade bd-example-modal-lg" id="chooseMyfile" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel2" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel2">เลือกจากคลังไฟล์</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="lbcboMyfile" CssClass="form-label" AssociatedControlID="cboMyfile" runat="server" Text="ไฟล์ของฉัน" />
                                <asp:Label ID="lbMandatorycboMyfile" CssClass="text-danger" AssociatedControlID="cboMyfile" runat="server" Text="*" />
                                <asp:DropDownList class="form-control" ID="cboMyfile" runat="server" required></asp:DropDownList>
                                <div class="invalid-feedback">กรุณาเลือกไฟล์</div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <%--<button type="button" id="btnAddDetail" class="btn btn-primary noEnterSubmit">Save</button>--%>
                    <asp:Button ID="asd" class="btn btn-primary" runat="server" Text="Save" OnClientClick="chooseMyfile(); return false;"   />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="exampleModalToggle2" aria-hidden="true" aria-labelledby="exampleModalToggleLabel2" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalToggleLabel2">เลือกรายการ</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>

                </div>
                <div class="modal-body">
                    <asp:ListBox class="cbomulti w-100" runat="server" ID="multiSelect" SelectionMode="multiple" data-selected-text-format="count"></asp:ListBox>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary noEnterSubmit" data-dismiss="modal">Close</button>
                    <asp:Button ID="btnAddDetailsFromSmartBill" class="btn btn-primary" runat="server" Text="Save" OnClientClick="showBtnSpiner(this);"   />
                </div>
            </div>
        </div>
    </div>

    
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <CompositeScript>
            <Scripts>
                <asp:ScriptReference Path="~/js/Sortable.js" />
                <asp:ScriptReference Path="~/vendor/jquery/jquery.min.js" />
                <asp:ScriptReference Path="~/datetimepicker/jquery.js" />
                <asp:ScriptReference Path="~/datetimepicker/build/jquery.datetimepicker.full.min.js" />
                <asp:ScriptReference Path="~/js/NonPO.js" />
            </Scripts>
        </CompositeScript>
    </asp:ScriptManager>

<%--    <script src="<%=Page.ResolveUrl("~/js/Sortable.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>
    <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/NonPO.js")%>"></script>--%>

    <script type="text/javascript">

        <% If Not Request.QueryString("NonpoCode") Is Nothing And maintable.Rows.Count > 0 Then%>
            <% If ((account_code.IndexOf(Session("usercode").ToString) > -1) And
(maintable.Rows(0).Item("statusid") = 7)) Or (maintable.Rows(0).Item("statusid") = 1) Then  ' 1	รอยืนยัน , 7	รอบัญชีตรวจสอบ  %>
        jQuery('[id$=txtinvoicedate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08'
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
            <% End If %>
        <% If head IsNot Nothing And head.Rows.Count > 0 Then %>
            <% If (account_code.IndexOf(Session("usercode").ToString) > -1) And head.Rows(0).Item("coderef").StartsWith("APP") %>
        jQuery('[id$=txtDuedate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08'
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
            <% End If %>
        <% End If %>

        <% else If ViewState("status_clearadvance").ToString = "new" Then %>
        jQuery('[id$=txtinvoicedate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08'
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
        <% End If %>


    </script>

    <script>
        var cntdetail =<% =chkunsave%>;
        $(window).load(function () {

            $('.form-control').prop('disabled', false);
        });
        $(document).ready(function () {
            var groups = {};
            $("select option[data-category]").each(function () {
                groups[$.trim($(this).attr("data-category"))] = true;
            });
            $.each(groups, function (c) {
                $("select option[data-category='" + c + "']").wrapAll('<optgroup label="' + c + '">');
            });

            const total = document.getElementById("total");
            const element = document.getElementById("id01");
            const Number = CheckNumber(total.textContent);
            console.log(Number);

            if (!isNaN(Number) && (Number - 0) < 999999999.9999) {
                element.innerHTML = " (   " + ArabicNumberToText(total.textContent) + "   )";
            } else {
                element.innerHTML = "รวม ";
            }

            $('.form-control').selectpicker({
                noneSelectedText: '-',
                liveSearch: true,
                maxOptions: 1
            });
            $('.cbomulti').selectpicker({
                noneSelectedText: '-',
                liveSearch: true
            });

            $('.form-control').selectpicker('refresh');


            $('#exampleModal').on('shown.bs.modal', function (e) {
                setnetprice();
                calculate();
            });
            /*$(".listCommentAndAttatch").click(function () {
                $(".card_attatch").toggle();
                $(".card_comment").toggle();
            });*/
            /*stoppedTyping();*/
            checkUnSave();
            /*
const urlParams = new URLSearchParams(window.location.search);
const nonpocode = urlParams.get('NonpoCode');
if (nonpocode) {
checkStatusNonpo();
} else {
alert('else nonpo')
}*/

            <% If Not AttachTable Is Nothing Then %>
                <% For i = 0 To AttachTable.Rows.Count - 1 %>
                    <% If AttachTable.Rows(i).Item("checked") = 1 Then %>
            $('.attatchItems #<%=AttachTable.Rows(i).Item("id")%>').prop('checked', true);
                    <% Else %>
            $('.attatchItems #<%=AttachTable.Rows(i).Item("id")%>').prop('checked', false);
                    <% End If %>
                <% Next i %>
            <% End if %>

            $('.DetailArea tr').each(function (index, tr) {
                console.log(index);
                console.log($(this).attr("data-status"));
                if ($(this).attr("data-status") == "new" || $(this).attr("data-status") == "edit") {
                    $(this).css("background-color", "#d8d8d8");
                }

            });
        });
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
        function checkUnSave() {

            const urlParams = new URLSearchParams(window.location.search);
            const nonpocode = urlParams.get('NonpoCode');
            if (nonpocode) {
                if (cntdetail == 1) {
                    $(".nonpounsaved").show();
                } else {
                    $(".nonpounsaved").hide();
                }
            }
        }


        function calculate() {

            //console.log("############ calculate");

            let netcost = CheckNumber(document.getElementById("<%= txtNetPrice.ClientID%>").value);
            let cost = CheckNumber(document.getElementById("<%= txtPrice.ClientID%>").value);
            let vat = CheckNumber(document.getElementById("<%= txtVat.ClientID%>").value);
            let tax = CheckNumber(document.getElementById("<%= txtTax.ClientID%>").value);

            const p_cost = document.getElementById("p_cost");
            const p_tax = document.getElementById("p_tax");
            const p_vat = document.getElementById("p_vat");

            netcost = parseFloat(netcost);

            cost = parseFloat(cost);
            vat = parseFloat(vat);
            tax = parseFloat(tax);

            //console.log(cost);
            //console.log(vat);
            //console.log(tax);

            //console.log(calCostTotal(cost, vat, tax).toFixed(2));
            //console.log(calVat(cost, vat).toFixed(2));
            //console.log(calTax(cost, tax).toFixed(2));

            const c_CostTotal = calCostTotal(cost, vat, tax).toFixed(4);
            const c_Vat = calVat(cost, vat).toFixed(4);
            const c_Tax = calTax(cost, tax).toFixed(4);

            //console.log(calCostTotal(cost, vat, tax).toFixed(2));
            //console.log(calVat(cost, vat).toFixed(2));
            //console.log(calTax(cost, tax).toFixed(2));

            if (!isNaN(cost) && (cost - 0) < 999999999.9999) {
                p_cost.innerHTML = "รวมทั้งสิ้น : " + numberWithCommas(c_CostTotal) + " บาท";
            } else {
                p_cost.innerHTML = "";
            }

            if (!isNaN(vat) && (vat - 0) < 999999999.9999) {
                p_vat.innerHTML = "Vat : " + numberWithCommas(c_Vat) + " บาท";
            } else {
                p_vat.innerHTML = "";
            }

            if (!isNaN(tax) && (tax - 0) < 999999999.9999) {
                p_tax.innerHTML = "Tax : (" + numberWithCommas(c_Tax) + ") บาท";
            } else {
                p_tax.innerHTML = "";
            }

        }


        <%--function checkStatusNonpo() {
             <% if maintable.Rows.Count > 0 Then%>
            const statusid = '<% =maintable.Rows(0).Item("statusid").ToString %>';
            alert(statusid);
            $("#statusnonpo").removeAttr("class");
            $("#statusnonpo").attr("class", "btn btn-outline-primary");


            <% End If %>
        }--%>
        function stoppedTyping() {
            if (document.getElementById('<%= txtComment.ClientID%>').value.length > 0) {
                document.getElementById('<%= btnSaveComment.ClientID%>').disabled = false;
            } else {
                document.getElementById('<%= btnSaveComment.ClientID%>').disabled = true;

            }
        }

        function updateComment(commentID, Oldtext) {
            const elemenmt = document.getElementById(commentID);
            elemenmt.setAttribute("contenteditable", "false");
            console.log('update');
            var userid = <% =Session("userid") %>;
            //console.log(elemenmt.innerHTML);
            //alert(elemenmt.textContent);

            var params = "{'userid': '" + userid + "','msg': '" + elemenmt.textContent + "','commentid': '" + commentID + "'}";
            $.ajax({
                type: "POST",
                url: "../Advance/ClearAdvance.aspx/updateComment",
                async: true,
                data: params,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    /*alertSuccessToast();*/
                    if (msg.d == 'success') {
                        //alert(elemenmt.textContent);

                        elemenmt.textContent = elemenmt.textContent;
                        alertSuccessToast('บันทึกเรียบร้อย' + description);
                    } else {
                        alertWarning('update fail');
                    }

                },
                error: function () {
                    elemenmt.textContent = Oldtext;
                    event.preventDefault();
                    event.stopPropagation();
                    alertWarning('Update Comment fail')
                }
            });

        }

        <%--function confirmDelete(commentID) {

            var userid = <% =Session("userid") %>;
            Swal.fire({
                title: 'คุุณต้องการจะลบข้อมุลนี้ใช่หรือไม่ ?',
                text: "",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes',
                allowOutsideClick: false
            }).then((result) => {
                if (result.isConfirmed) {
                    var params = "{'commentid': '" + commentID + "','userid': '" + userid + "'}";
                    $.ajax({
                        type: "POST",
                        url: "../Advance/ClearAdvance.aspx/deleteComment",
                        async: true,
                        data: params,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == 'success') {
                                window.location.href = location.href;
                            } else {
                                alertWarning('Delete fail');
                            }
                        },
                        error: function () {
                            alertWarning('Delete Comment fail');
                        }
                    });
                }
            })

            return false;
        }--%>

        function addAttach() {

            Swal.fire({
                title: 'แนบลิ้งเอกสาร',
                html:
                    '<input id="swal-input1" class="swal2-input" type="url" placeholder="URL">' +
                    '<input id="swal-input2" class="swal2-input" placeholder="Description">',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                preConfirm: () => {
                    if (!document.getElementById('swal-input1').value || !document.getElementById('swal-input2').value) {
                        // Handle return value 
                        if (!document.getElementById('swal-input1').value && document.getElementById('swal-input2').value) {
                            Swal.showValidationMessage('URL missing')
                        } else if (document.getElementById('swal-input1').value && !document.getElementById('swal-input2').value) {
                            Swal.showValidationMessage('Description missing')
                        } else {
                            Swal.showValidationMessage('URL,Description missing')
                        }
                    } else {
                        return [
                            document.getElementById('swal-input1').value,
                            document.getElementById('swal-input2').value
                        ]
                    }
                },
                confirmButtonText: 'Save',
                allowOutsideClick: false
            }).then((result) => {
                if (result.isConfirmed) {
                    let url = result.value[0];
                    let description = result.value[1];
                    sentAddAttach(url, description)
                }
            })

        }
        function chooseMyfile() {
            validateData();

            const url = $('#<%= cboMyfile.ClientID%>').val();
            const description = $("#<%= cboMyfile.ClientID%> option:selected").text();
            sentAddAttach(url, description)

            return true;
        }
        function sentAddAttach(url, description) {
            if (url.substring(0, 7) != 'http://' && url.substring(0, 8) != 'https://') {
                url = 'http://' + url;
            }
            /*alert(url);*/
            let msg = '<a href="' + url + '" target="_blank">' + description + '</a>'

            const urlParams = new URLSearchParams(window.location.search);
            const nonpocode = urlParams.get('NonpoCode');
            var user = "<% =Session("usercode").ToString %>";
            var userid = <%= Session("userid") %>;
            var params = "{'user': '" + user + "','url': '" + url + "','description': '" + description + "','nonpocode': '" + nonpocode + "'}";
            $.ajax({
                type: "POST",
                url: "../Advance/ClearAdvance.aspx/addAttach",
                async: true,
                data: params,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {


                    /*alertSuccessToast();*/
                    if (msg.d) {
                        if (!description) {
                            description = 'Link';
                        }
                        /*__doPostBack('AttachTable', '')*/
                        $('.attatchItems').append(
                            '<div class="row">' +
                            '<div class= "attatchItems-link-btndelete" id ="ATT' + msg.d + '" >' +
                            '<div class="col-auto">' +
                            '<a href="' + url + '" class="text-primary listCommentAndAttatch " style="cursor: pointer;" target="_blank">' +
                            '<span>' + description + '</span></a>' +
                            '<a onclick="removeAttach(' + msg.d + ',' + userid + ');" class="btn btn-sm pt-0 text-danger deletedetail">' +
                            '<i class="fas fa-times"></i>' +
                            '</a>' +
                            '</div>' +
                            '</div>' +
                            '</div>'
                        );
                        alertSuccessToast('บันทึกเรียบร้อย' + description);
                    } else {
                        alertWarning('Add URL faila');
                    }

                },
                error: function () {
                    alertWarning('Add URL fail');
                }
            });

        }

        function confirmDeletedetail(nonpodtlid, row) {
            Swal.fire({
                title: 'คุุณต้องการจะลบข้อมุลนี้ใช่หรือไม่ ?',
                text: "",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes',
                allowOutsideClick: false
            }).then((result) => {
                if (result.isConfirmed) {

                    var user = "<% =Session("usercode").ToString %>";
                    var params = "{'nonpodtlid': '" + nonpodtlid + "','rows': '" + row + "','user': '" + user + "'}";


                    __doPostBack('deletedetail', params);
                    //$.ajax({
                    //    type: "POST",
                    //    url: "../Advance/ClearAdvance.aspx/deleteDetail",
                    //    async: true,
                    //    data: params,
                    //    contentType: "application/json; charset=utf-8",
                    //    dataType: "json",
                    //    success: function (msg) {
                    //        if (msg.d == 'success') {
                    //            swal.fire({
                    //                title: "Deleted!",
                    //                text: "",
                    //                icon: "success",
                    //                allowOutsideClick: false
                    //            }).then(function () {
                    //                __doPostBack('detailtable', '');

                    //            });
                    //        } else {
                    //            alertWarning('fail')
                    //        }
                    //    },
                    //    error: function () {
                    //        alertWarning('fail ee')
                    //    }
                    //});
                }
            })

            return false;
        }
        function setdetail(Acc) {

            const myArr = Acc.options[Acc.selectedIndex].textContent.split(" - ");
            console.log(myArr[myArr.length - 1]);

            $("#<%= txtDetail.ClientID%>").val(myArr[myArr.length - 1]);

        }
        function clearfromadddetail() {

            $('#<%= row.ClientID%>').val(0);
            $('#<%= hiddenAdvancedetailid.ClientID%>').val(0);
            $('#<%= cboAccountCode.ClientID%>').val(0);
            $('#<%= cboDep.ClientID%>').val(0);
            $('#<%= cboBU.ClientID%>').val(0);
            $('#<%= cboPP.ClientID%>').val(0);
            $('#<%= cboPJ.ClientID%>').val(0);
            $('#<%= txtPrice.ClientID%>').val('');
            $('#<%= txtVat.ClientID%>').val('7');
            $('#<%= txtTax.ClientID%>').val('');
            $('#<%= TxtNetPrice.ClientID%>').val('');
            $('#<%= txtDetail.ClientID%>').val('');
            $('#<%= cboVendor.ClientID%>').val('');
            $('#<%= txtVendor.ClientID%>').val('');
            $('#<%= txtinvoiceno.ClientID%>').val('');
            $('#<%= txttaxid.ClientID%>').val('');
            $('#<%= txtinvoicedate.ClientID%>').val('');
            $('#<%= chkNoBill.ClientID%>').prop('checked', false);
            $('#<%= chkIncompleteBill.ClientID%>').prop('checked', false);
            $('.form-control').selectpicker('refresh');
        }


        function selectElement(id, valueToSelect) {
            let element = document.getElementById(id);
            element.value = valueToSelect;
        }
        function btnEditDetailClick(row, advancedetailid, accountcodeid, depid, buid, ppid, pjid, cost, vat, tax, detail, vendorcode, invoice, taxid, invoicedate, NoBill, IncompleteBill) {
            console.log(advancedetailid);
            console.log(accountcodeid);
            console.log(depid);
            console.log(buid);
            console.log(ppid);
            console.log(cost);
            console.log(detail);
            console.log(vendorcode);

            const Accountcode = '<%= cboAccountCode.ClientID%>';
            const dep = '<%= cboDep.ClientID%>';
            const bu = '<%= cboBU.ClientID%>';
            const pp = '<%= cboPP.ClientID%>';
            const pj = '<%= cboPJ.ClientID%>';
            const vendor = '<%= cboVendor.ClientID%>';
            $('#exampleModal').modal('show');

            selectElement(Accountcode, accountcodeid);
            selectElement(dep, depid);
            selectElement(bu, buid);
            selectElement(pp, ppid);
            selectElement(pj, pjid);
            selectElement(vendor, vendorcode);
          <%--  $('#<%= cboVendor.ClientID%>').filter(function () {
                //may want to use $.trim in here
                return $(this).text() == vendorcode;
            }).prop('selected', true);
            var value = $('#<%= cboVendor.ClientID%>').filter(function () {
                return $(this).text();
            }).first().attr("value");
            console.log(value);
            vendor.value = value; --%>

            $('#<%= row.ClientID%>').val(row);
            $('#<%= hiddenAdvancedetailid.ClientID%>').val(advancedetailid);
            $('#<%= txtVendor.ClientID%>').val(vendorcode);
            $('#<%= txtPrice.ClientID%>').val(cost);
            $('#<%= txtVat.ClientID%>').val(vat);
            $('#<%= txtTax.ClientID%>').val(tax);
            $('#<%= txtDetail.ClientID%>').val(detail);

            $('#<%= txtinvoiceno.ClientID%>').val(invoice);
            $('#<%= txttaxid.ClientID%>').val(taxid);
            $('#<%= txtinvoicedate.ClientID%>').val(invoicedate);
            $('#<%= chkNoBill.ClientID%>').prop('checked', NoBill.toLowerCase() == "true" ? true : false);
            $('#<%= chkIncompleteBill.ClientID%>').prop('checked', IncompleteBill.toLowerCase() == "true" ? true : false);

            $('.form-control').selectpicker('refresh');
            /*__doPostBack('setFromDetail', $(row).attr('name'));
*/

            <% If Not Request.QueryString("NonpoCode") Is Nothing And maintable.Rows.Count > 0 Then%>
            <% If (Not ViewState("status_clearadvance") = "new" And Not ViewState("status_clearadvance") = "edit" And Not ViewState("status_clearadvance") = "account") Then%>
            $('#exampleModal .modal-footer #btnAddDetail').hide();
            $('#exampleModal .modal-body input,#exampleModal .modal-body textarea').attr('readonly', true);
            $('#exampleModal .modal-body select,#exampleModal .modal-body button,#exampleModal .modal-body input[type="checkbox"]').attr('disabled', true);
            <% End If %>
            <% End If %>


            $('#<%= btnAddRef.ClientID%>').hide();

        }
        function setVendor(Acc) {

            const myArr = Acc.options[Acc.selectedIndex].textContent.split(" - ");
            console.log(myArr);
            console.log(myArr[0].substring(2, 12));

            let vendorcode = myArr[0].substring(2, 12)
            console.log(myArr[myArr.length - 1]);
            console.log(vendorcode);

            var taxidno = Acc.options[Acc.selectedIndex].getAttribute("data-taxidno");
            //console.log(Acc.options[Acc.selectedIndex]);
            //console.log(Acc.options[Acc.selectedIndex].getAttribute("data-taxidno"));

            $("#<%= txtVendor.ClientID%>").val(myArr[myArr.length - 1]);
            $("#<%= txttaxid.ClientID%>").val(taxidno);

        }
        function invalidtotal() {
            alertWarning('ไม่สามารถบันทึก เกินยอด')
            //event.preventDefault();
            //event.stopPropagation();
            //$('.DetailArea tr').each(function (index, tr) {
            //    //    console.log(index);
            //    //console.log($(this).attr("data-status"));
            //    if ($(this).attr("data-status") == "new" || $(this).attr("data-status") == "edit") {
            //        $(this).css("background-color", "#d8d8d8");
            //    }

            //});
            return 0;
        }
        function disbtndelete() {
            $(".deletedetail").hide();
        }

        function postBack_addDetail() {
            let row = $('#<%= row.ClientID%>').val();
            const nonpodtl_id = $('#<%= hiddenAdvancedetailid.ClientID%>').val();
            const accountcodeid = $('#<%= cboAccountCode.ClientID%>').val();
            const accountcode = $("#<%= cboAccountCode.ClientID%> option:selected").text();
            const depid = $('#<%= cboDep.ClientID%>').val();
            const depname = $("#<%= cboDep.ClientID%> option:selected").text();
            const buid = $('#<%= cboBU.ClientID%>').val();
            const buname = $("#<%= cboBU.ClientID%> option:selected").text();
            const ppid = $('#<%= cboPP.ClientID%>').val();
            const ppname = $("#<%= cboPP.ClientID%> option:selected").text();
            const pjid = $('#<%= cboPJ.ClientID%>').val();
            const pjname = $("#<%= cboPJ.ClientID%> option:selected").text();
            const cost = $('#<%= txtPrice.ClientID%>').val();
            const vat = $('#<%= txtVat.ClientID%>').val();
            const tax = $('#<%= txtTax.ClientID%>').val();
            const detail = $('#<%= txtDetail.ClientID%>').val();
            const vendorname = $("#<%= cboVendor.ClientID%> option:selected").text();
            const vendorcode = $('#<%= txtVendor.ClientID%>').val();

            const invoice = $('#<%= txtinvoiceno.ClientID%>').val();
            const taxid = $('#<%= txttaxid.ClientID%>').val();
            const invoicedate = $('#<%= txtinvoicedate.ClientID%>').val();
            const nobill = $('#<%= chkNoBill.ClientID%>').is(":checked");
            const incompletebill = $('#<%= chkIncompleteBill.ClientID%>').is(":checked");
            const status = $(".DetailArea tr[name='" + row + "']").attr("data-status")

            //alert('cost' + cost);

            //if (/['"]/.test(detail)) {
            //    alertWarning(`รายละเอียดรายการ\nต้องไม่มีเครื่องหมาย \n[ ' , " ]`);
            //    event.preventDefault();
            //    event.stopPropagation();
            //    return 0;
            //}

            if ((cost != 0 || cost === "") && accountcodeid == 0) {
                alertWarning('กรุณาเลือกรหัสบัญชี');
                event.preventDefault();
                event.stopPropagation();
                return 0;
            }
            if ((/[^0-9]/g.test(taxid))) {
                alertWarning('taxid ต้องเป็นตัวเลข 0-9 เท่านั้น');
                event.preventDefault();
                event.stopPropagation();
                return 0;
            }
            //if (vat != 0 && (!invoice || !taxid || !invoicedate)) {
            //    alertWarning('กรุณากรอกข้อมูล invoice ให้ครบถ้วน');
            //    event.preventDefault();
            //    event.stopPropagation();
            //    return 0;
            //}
            //alert(row);
            //var params = "{'row': '" + row + "'}";
            var params = "{'rows': '" + row + "','status': '" + status + "','nonpodtl_id': '" + nonpodtl_id + "','accountcodeid': '" + accountcodeid +
                "','accountcode': '" + accountcode + "','depid': '" + depid + "','depname': '" + depname +
                "','buid': '" + buid + "','buname': '" + buname + "','ppid': '" + ppid + "','ppname': '" + ppname + "','pjid': '" + pjid + "','pjname': '" + pjname +
                "','cost': '" + (cost == 0 ? 0.0 : cost) + "','vat': '" + (vat == '' ? 0 : vat) + "','tax': '" + (tax == '' ? 0 : tax) + "','detail': '" + detail +
                "','vendorname': '" + vendorname + "','vendorcode': '" + vendorcode +
                "','invoice': '" + invoice + "','taxid': '" + taxid + "','invoicedate': '" + invoicedate + "','nobill': '" + nobill + "','incompletebill': '" + incompletebill + "'}";

            //alert(params);
            //PageMethods.addoreditdetail(params);

            removeElem("addDetailJSON");
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "addDetailJSON";
            confirm_value.value = params;
            document.forms[0].appendChild(confirm_value);
            showBtnSpiner(document.getElementById("<%= btnAddDetails.ClientID%>"));
            return true;

        }
        //function chkAttach(elem,userid) {
        //    //console.log(s)
        //    //console.log(s.id)
        //    //console.log(s.checked)

        //    event.preventDefault();
        //    var params = "{'attatchid': '" + elem.id + "','chked': '" + elem.checked + "','userid': '" + userid + "'}";
        //    $.ajax({
        //        type: "POST",
        //        url: "../Advance/ClearAdvance.aspx/changeChecked",
        //        async: true,
        //        data: params,
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (msg) {
        //            //console.log(msg)
        //            //console.log(msg.d['menuid'])
        //            var obj = JSON.parse(msg.d);
        //            console.log(obj)
        //            console.log(obj[0]['attid'])
        //            for (let i = 0; i < obj.length; i++) {
        //                (obj[i]['checked']) ? $('.attatchItems #' + obj[i]['attid']).prop('checked', true) : $('.attatchItems #' + obj[i]['attid']).prop('checked', false);
        //            }
        //        },
        //        error: function () {
        //            alertWarning('fail')
        //        }
        //    });

        //}
        $('#<%= txtDuedate.ClientID%>').on('change', function () {
            cntdetail = 1; //show unsave
            checkUnSave(); //show unsave
        });
        $('#<%= chkpayBack.ClientID%>').on('change', function () {
            cntdetail = 1; //show unsave
            checkUnSave(); //show unsave
        });
        $('#<%= chkdeductSell.ClientID%>').on('change', function () {
            cntdetail = 1; //show unsave
            checkUnSave(); //show unsave
        });
        $('#<%= txtamountpayBack.ClientID%>').on('change', function () {
            cntdetail = 1; //show unsave
            checkUnSave(); //show unsave
        });
        $('#<%= txtamountdedusctsell.ClientID%>').on('change', function () {
            cntdetail = 1; //show unsave
            checkUnSave(); //show unsave
        });
        $('#<%= chkVat.ClientID%>').on('change', function () {
            cntdetail = 1; //show unsave
            checkUnSave(); //show unsave
        });
        $("#btnAddDetail").click(function () {
            //alert("The paragraph was clicked.");
            let row = $('#<%= row.ClientID%>').val();
            const nonpodtl_id = $('#<%= hiddenAdvancedetailid.ClientID%>').val();
            const accountcodeid = $('#<%= cboAccountCode.ClientID%>').val();
            const accountcode = $("#<%= cboAccountCode.ClientID%> option:selected").text();
            const depid = $('#<%= cboDep.ClientID%>').val();
            const depname = $("#<%= cboDep.ClientID%> option:selected").text();
            const buid = $('#<%= cboBU.ClientID%>').val();
            const buname = $("#<%= cboBU.ClientID%> option:selected").text();
            const ppid = $('#<%= cboPP.ClientID%>').val();
            const ppname = $("#<%= cboPP.ClientID%> option:selected").text();
            const pjid = $('#<%= cboPJ.ClientID%>').val();
            const pjname = $("#<%= cboPJ.ClientID%> option:selected").text();
            const cost = $('#<%= txtPrice.ClientID%>').val();
            const vat = $('#<%= txtVat.ClientID%>').val();
            const tax = $('#<%= txtTax.ClientID%>').val();
            const detail = $('#<%= txtDetail.ClientID%>').val();
            const vendorname = $("#<%= cboVendor.ClientID%> option:selected").text();
            const vendorcode = $('#<%= txtVendor.ClientID%>').val();

            const invoice = $('#<%= txtinvoiceno.ClientID%>').val();
            const taxid = $('#<%= txttaxid.ClientID%>').val();
            const invoicedate = $('#<%= txtinvoicedate.ClientID%>').val();
            const status = $(".DetailArea tr[name='" + row + "']").attr("data-status")

            //alert('cost' + cost);

            if (cost != 0 && accountcodeid == 0) {
                alertWarning('กรุณาเลือกรหัสบัญชี');
                event.preventDefault();
                event.stopPropagation();
                return 0;
            }
            if (vat != 0 && (!invoice || !taxid || !invoicedate)) {
                alertWarning('กรุณากรอกข้อมูล invoice ให้ครบถ้วน');
                event.preventDefault();
                event.stopPropagation();
                return 0;
            }
            //alert(row);
            //var params = "{'row': '" + row + "'}";
            var params = "{'rows': '" + row + "','status': '" + status + "','nonpodtl_id': '" + nonpodtl_id + "','accountcodeid': '" + accountcodeid +
                "','accountcode': '" + accountcode + "','depid': '" + depid + "','depname': '" + depname +
                "','buid': '" + buid + "','buname': '" + buname + "','ppid': '" + ppid + "','ppname': '" + ppname + "','pjid': '" + pjid + "','pjname': '" + pjname +
                "','cost': '" + (cost == 0 ? 0.0 : cost) + "','vat': '" + (vat == '' ? 0 : vat) + "','tax': '" + (tax == '' ? 0 : tax) + "','detail': '" + detail +
                "','vendorname': '" + vendorname + "','vendorcode': '" + vendorcode +
                "','invoice': '" + invoice + "','taxid': '" + taxid + "','invoicedate': '" + invoicedate + "'}";

            //alert(params);

            $.ajax({
                type: "POST",
                url: "../Advance/ClearAdvance.aspx/addoreditdetail",
                async: true,
                data: params,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    console.log(msg);
                    if (msg.d && msg.d != "fail") {
                        console.log("row" + row);
                        console.log("msg.d" + msg.d);
                        if (row != msg.d) {
                            row = msg.d

                            $('.DetailArea').append(
                                '<tr class="draggable detail notPrint" style="background-color: #d8d8d8;" data-status="new"  name="' + row + '" ondblclick=\'btnEditDetailClick("' + row + '","' + nonpodtl_id + '","' + accountcodeid + '","' + depid + '","' + buid + '","' + ppid + '","' + pjid + '","' + cost + '","' + vat + '","' + tax + '","' + detail + '","' + vendorcode + '","' + invoice + '","' + taxid + '","' + invoicedate + '");\' > ' +
                                //'<tr class="draggable detail" draggable="true" name="' + row + '">' +
                                '<td colspan="2" style="width: 80px !important; height: 22px;" title="' + accountcode + '">' + (accountcodeid == '0' ? '' : accountcodeid) +
                                '</td>' +
                                '<td colspan="7" style="width: 280px !important;" title="' + detail + '"><span>' + detail + '</span>' +
                                '</td>' +
                                '<td colspan="4" style="width: 160px !important;" title="' + vendorname + '">' + vendorcode +
                                '</td>' +
                                '<td colspan="2" style="width: 80px !important;" title="' + buname + '">' + buname +
                                '</td>' +
                                '<td colspan="2" style="width: 80px !important;" title="' + ppname + '">' + ppname +
                                '</td>' +
                                '<td colspan="2" style="width: 80px !important;" title="' + pjname + '">' + pjname +
                                '</td>' +
                                '<td colspan="3" style="width: 120px !important;" title="' + cost + '">' + (cost == '0' ? '' : cost) +
                                '</td>' +
                                '<td colspan="1" style="width: 40px !important;" title="' + vat + '">' + vat +
                                '</td>' +
                                '<td colspan="1" style="width: 40px !important;" title="' + tax + '">' + tax +
                                '</td>' +
                                '<td class="deletedetail notPrint" style="position: absolute; border: 0px solid #000;">' +
                                '<div>' +
                                '<a onclick="confirmDeletedetail(' + nonpodtl_id + ',' + row + ');" class="btn btn-sm p-0 notPrint">' +
                                ' <i class="fas fa-times"></i>' +
                                '</a>' +
                                '</div>' +
                                '</td>' +
                                '</tr>'


                            );
                            cntdetail = 1; //show unsave
                        } else {
                            $(".DetailArea tr[name='" + row + "']").html(
                                '<td colspan="2" style="width: 80px !important; height: 22px;" title="' + accountcode + '">' + (accountcodeid == '0' ? '' : accountcodeid) +
                                '</td>' +
                                '<td colspan="7" style="width: 280px !important;" title="' + detail + '"><span>' + detail + '</span>' +
                                '</td>' +
                                '<td colspan="4" style="width: 160px !important;" title="' + vendorname + '">' + vendorcode +
                                '</td>' +
                                '<td colspan="2" style="width: 80px !important;" title="' + buname + '">' + buname +
                                '</td>' +
                                '<td colspan="2" style="width: 80px !important;" title="' + ppname + '">' + ppname +
                                '</td>' +
                                '<td colspan="2" style="width: 80px !important;" title="' + pjname + '">' + pjname +
                                '</td>' +
                                '<td colspan="3" style="width: 120px !important;" title="' + cost + '">' + (cost == '0' ? '' : cost) +
                                '</td>' +
                                '<td colspan="1" style="width: 40px !important;" title="' + vat + '">' + vat +
                                '</td>' +
                                '<td colspan="1" style="width: 40px !important;" title="' + tax + '">' + tax +
                                '</td>' +
                                '<td class="deletedetail notPrint" style="position: absolute; border: 0px solid #000;">' +
                                '<div>' +
                                '<a onclick="confirmDeletedetail(' + nonpodtl_id + ',' + row + ');" class="btn btn-sm p-0 notPrint">' +
                                ' <i class="fas fa-times"></i>' +
                                '</a>' +
                                '</div>' +
                                '</td>'
                            );

                            $(".DetailArea tr[name='" + row + "']").attr("ondblclick", 'btnEditDetailClick("' + row + '", "' + nonpodtl_id + '", "' + accountcodeid + '", "' + depid + '", "' + buid + '", "' + ppid + '", "' + pjid + '", "' + cost + '","' + vat + '","' + tax + '", "' + detail + '", "' + vendorcode + '","' + invoice + '","' + taxid + '","' + invoicedate + '");');
                            if ($(".DetailArea tr[name='" + row + "']").attr("data-status") == "read") {
                                $(".DetailArea tr[name='" + row + "']").attr("data-status", "edit");
                                $(".DetailArea tr[name='" + row + "']").css("background-color", "#d8d8d8");
                            }
                            alert($(".DetailArea tr[name='" + row + "']").attr("data-status"));
                            cntdetail = 1; //show unsave
                        }

                        checkUnSave(); //show unsave

                        $('#exampleModal').modal('hide')
                        //alert('yes');
                        clearfromadddetail();
                    } else {
                        alertWarning('res fail')
                    }
                },
                error: function (msg) {
                    console.log(msg);
                    alertWarning(msg);
                }
            });
        });
        $('#<% =btnFromAddDetail.ClientID%>').click(function () {
            $('#exampleModal .modal-footer #btnAddDetail').show();
            $('#exampleModal .modal-body input,#exampleModal .modal-body textarea').removeAttr("readonly");
            $('#exampleModal .modal-body select,#exampleModal .modal-body button,#exampleModal .modal-body input[type="checkbox"]').removeAttr("disabled");
            $('#<% =txtinvoicedate.ClientID%>').attr('readonly', true);
            $('.form-control').selectpicker('refresh');

            clearfromadddetail();

        });
        $('.noEnterSubmit').keypress(function (e) {
            if (e.which == 13) return false;
            //or...
            if (e.which == 13) e.preventDefault();
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
        function setprice() {
            let netprice = CheckNumber(document.getElementById("<%=TxtNetPrice.ClientID%>").value);
            let vat = CheckNumber(document.getElementById("<%= txtVat.ClientID%>").value);
            let tax = CheckNumber(document.getElementById("<%= txtTax.ClientID%>").value);

            netprice = parseFloat(netprice);
            vat = parseFloat(vat);
            tax = parseFloat(tax);
            if (netprice) {


                var price = document.getElementById("<%= txtPrice.ClientID%>")
                //console.log((netprice / (1 + (vat / 100) - (tax / 100))))
                price.value = parseFloat(netprice / (1 + (vat / 100) - (tax / 100))).toFixed(4)
            }
        }
        function setnetprice() {
            let price = CheckNumber(document.getElementById("<%=txtPrice.ClientID%>").value);
            let vat = CheckNumber(document.getElementById("<%= txtVat.ClientID%>").value);
            let tax = CheckNumber(document.getElementById("<%= txtTax.ClientID%>").value);

            price = parseFloat(price);
            vat = parseFloat(vat);
            tax = parseFloat(tax);
            if (price) {
                var netprice = document.getElementById("<%= TxtNetPrice.ClientID%>")
                netprice.value = parseFloat(calCostTotal(price, vat, tax)).toFixed(4)
            }
        }
        $("#exampleModal input:checkbox").on('click', function () {
            // in the handler, 'this' refers to the box clicked on
            //console.log(this);
            var $box = $(this);
            if ($box.is(":checked")) {
                // the name of the box is retrieved using the .attr() method
                // as it is assumed and expected to be immutable
                var group = "#exampleModal input:checkbox";
                // the checked state of the group/box on the other hand will change
                // and the current value is retrieved using .prop() method
                $(group).prop("checked", false);
                $box.prop("checked", true);
            } else {
                $box.prop("checked", false);
            }
        });

        var arrVendor = new Array;
        var myArray = new Array;
        $("#<%= cboVendor.ClientID%> option").each(function () {
            arrVendor.push($(this).val());
            myArray[$(this).val()] = $(this).attr("data-taxidno");
        });

        nonpo_autocomplete(document.getElementById("<%= txtVendor.ClientID%>"), arrVendor, myArray, '<%= txttaxid.ClientID%>');
    </script>
</asp:Content>
