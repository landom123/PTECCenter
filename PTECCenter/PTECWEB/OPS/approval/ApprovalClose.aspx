<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="ApprovalClose.aspx.vb" Inherits="PTECCENTER.WebForm4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- fonts -->
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,700" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/fileupload/dist/font/font-fileuploader.css")%>" rel="stylesheet">

    <link href="<%=Page.ResolveUrl("~/fileupload/dist/jquery.fileuploader.min.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/fileupload/dist/jquery.fileuploader-theme-thumbnails.css")%>" rel="stylesheet">
    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">

    <style>
        .table, .table tbody input, .table tbody textarea {
            font-size: .75rem;
        }
        /*.table thead th {
                vertical-align: middle;
            }*/
        th {
            text-align: center;
        }

        .table td {
            vertical-align: middle;
        }


        .file-upload-content, .image-upload-wrap {
            display: none;
        }

        .fileuploader-theme-thumbnails .fileuploader-items .fileuploader-item .fileuploader-action + .fileuploader-action {
            display: none;
        }

        .fileuploader-popup .fileuploader-popup-footer {
            display: none;
        }

        .fileuploader-theme-thumbnails .fileuploader-thumbnails-input-inner {
            background: #f0cccc;
            border: 2px dashed #ff0000;
            color: #ff0000;
        }

        .btn-light {
            background-color: #fff;
        }

            .btn-light.disabled {
                border: 1px solid #ced4da;
                background-color: #e9ecef;
            }




        .noContent {
            width: 200px;
            height: 200px;
            position: absolute;
            top: 50%;
            left: 50%;
            margin-top: -100px;
            margin-left: -100px;
        }

        .modal .showCost {
            background-color: #f7faff;
            padding: 1rem;
        }

        .hide {
            display: none;
        }

        .myDIV:hover + .hide {
            display: block;
            max-width: 40%;
            height: auto;
            left: 0;
            z-index: 1;
            position: absolute;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="bg">
        <div id="wrapper" class="h-100">

            <!-- #include virtual ="/include/menu.inc" -->
            <!-- add side menu -->
            <div id="content-wrapper">
                <div class="container container-fluid">
                    <% If flag Then%>
                    <% If Not Request.QueryString("approvalcode") Is Nothing Then%>

                    <div class="row bg-white mb-3">
                        <div class="col-lg-12">
                            <div class="card shadow">
                                <div class="card-header" style="background-color: navy; color: white">
                                    <div class="row justify-content-between">
                                        <div class="col text-left">
                                            ปิดงาน
                                        </div>
                                        <div class="col text-right">
                                            <a href="ApprovalMenuList.aspx" class="btn btn-sm btn-danger ">
                                                <i class="fa fa-tasks" aria-hidden="true"></i></a>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body">

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-floating">
                                                <asp:Label ID="lbApprovalcode" CssClass="form-label" AssociatedControlID="txtApprovalcode" runat="server" Text="Approval No." />
                                                <asp:TextBox class="form-control" ID="txtApprovalcode" runat="server" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lbStatus" CssClass="form-label" AssociatedControlID="txtStatus" runat="server" Text="สถานะ" />
                                                <asp:TextBox class="form-control" ID="txtStatus" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Label ID="lbcboApproval" CssClass="form-label" AssociatedControlID="txtApproval" runat="server" Text="หัวข้อขออนุมัติ" />
                                                <asp:TextBox class="form-control" ID="txtApproval" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <% If detailtable IsNot Nothing Then%>
                                    <% If detailtable.Rows(0).Item("statusid") = 4 Or detailtable.Rows(0).Item("statusid") = 2 Or detailtable.Rows(0).Item("statusid") = 9 Then%>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lbCreateBy" CssClass="form-label" AssociatedControlID="txtCreateBy" runat="server" Text="ผู้ขออนุมัติ" />
                                                <asp:TextBox class="form-control font-weight-bold" ID="txtCreateBy" runat="server" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lbtxtOwnerApprovalName" CssClass="form-label" AssociatedControlID="txtOwnerApprovalName" runat="server" Text="อนุมัติโดย" />
                                                <asp:TextBox class="form-control font-weight-bold" ID="txtOwnerApprovalName" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <% End If %>
                                    <% End If %>

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lbDocDate" CssClass="form-label" AssociatedControlID="txtDocDate" runat="server" Text="วันที่แจ้ง" />
                                                <asp:TextBox class="form-control font-weight-bold" ID="txtDocDate" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lbOwnerApprovalDate" CssClass="form-label" AssociatedControlID="txtOwnerApprovalDate" runat="server" Text="วันที่วันที่อนุมัติ" />
                                                <asp:TextBox class="form-control text-danger font-weight-bold" ID="txtOwnerApprovalDate" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Label ID="lbCloseDate" CssClass="form-label" AssociatedControlID="txtCloseDate" runat="server" Text="วันที่ปิดงาน" />
                                                <asp:Label ID="lbCloseDateMandatory" CssClass="text-danger" AssociatedControlID="txtCloseDate" runat="server" Text="*" />
                                                <asp:TextBox class="form-control text-danger font-weight-bold" ID="txtCloseDate" runat="server" placeholder="--- คลิกเพื่อเลือก ---" required></asp:TextBox>
                                                <div class="invalid-feedback">กรุณาเลือกวัน</div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Label ID="lbDetail" CssClass="form-label" AssociatedControlID="txtDetail" runat="server" Text="รายละเอียด" />
                                                <asp:Label ID="lbDetailMandatory" CssClass="text-danger" AssociatedControlID="txtDetail" runat="server" Text="*" />
                                                <asp:TextBox class="form-control" ID="txtDetail" runat="server" Rows="3" Columns="50" TextMode="MultiLine" onkeyDown="checkTextAreaMaxLength(this,event,'255');" required></asp:TextBox>
                                                <div class="invalid-feedback">กรุณากรอกรายละเอียด</div>
                                            </div>
                                        </div>
                                    </div>
                                    <% If detailtable IsNot Nothing Then%>
                                    <% If detailtable.Rows(0).Item("statusid") = 9 Then%>
                                    <div class="row" style="display: none;">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Label ID="lbCodeGSM" CssClass="form-label" AssociatedControlID="txtCodeGSM" runat="server" Text="รหัส GSM" />
                                                <asp:Label ID="lbCodeGSMMandatory" CssClass="text-danger" AssociatedControlID="txtCodeGSM" runat="server" Text="*" />
                                                <asp:TextBox class="form-control" ID="txtCodeGSM" runat="server"></asp:TextBox>
                                                <div class="invalid-feedback">กรุณากรอกรหัส GSM</div>
                                            </div>
                                        </div>
                                    </div>
                                    <% End If %>
                                    <% If detailtable.Rows(0).Item("statusid") = 2 Or detailtable.Rows(0).Item("statusid") = 9 Then%>
                                    <div class="row">
                                        <div class="col-md-auto">
                                            <div class="form-check">
                                                <input class="form-check-input chk-img-after" type="checkbox" value="" id="chkAfter" runat="server" onchange="valueChangedImgAfter()">
                                                <asp:Label ID="lbchkAfter" CssClass="form-check-label" AssociatedControlID="chkAfter" runat="server" Text="รูปภาพหลังทำ" />

                                            </div>
                                        </div>
                                        <div class="col-md-auto">
                                            <div class="form-check">
                                                <input class="form-check-input chk-img-bill" type="checkbox" value="" id="chkBill" runat="server" onchange="valueChangedImgBill()">
                                                <asp:Label ID="lbchkBill" CssClass="form-check-label" AssociatedControlID="chkBill" runat="server" Text="รูปภาพใบเสร็จ" />
                                            </div>
                                        </div>
                                    </div>
                                    <% End If %>
                                    <% End If %>
                                </div>
                            </div>
                            <!-- end card-->

                        </div>
                        <!-- end col-lg-12-->
                    </div>
                    <!-- end row-->

                    <div class="row bg-white">
                        <div class="col-md-12 col-12 mb-3 img-after" style="display: none;">
                            <div class="card shadow">
                                <div class="card-header" style="background-color: #ffc107; color: white">
                                    <div class="row justify-content-between">
                                        <div class="col text-left">
                                            รูปภาพหลังปฏิบัติงาน
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body">
                                    <!-- end แถว1-->
                                    <div class="row justify-content-md-center">
                                        <div class="col-md-6 col-12">
                                            <div class="input-group justify-content-center file_af">
                                                <input type="file" name="files" id="file_af" accept="image/*,.pdf">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card-->
                            </div>
                        </div>
                        <div class="col-md-12 col-12 mb-3 img-bill" style="display: none;">
                            <div class="card shadow">
                                <div class="card-header" style="background-color: deeppink; color: white">
                                    <div class="row justify-content-between">
                                        <div class="col text-left">
                                            รูปภาพใบเสร็จ
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body">
                                    <!-- end แถว1-->
                                    <div class="row justify-content-md-center">
                                        <div class="col-md-6 col-12">
                                            <div class="input-group justify-content-center file_bill file_bill">
                                                <input type="file" name="files" id="file_bill" accept="image/*,.pdf">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card-->
                            </div>
                        </div>
                    </div>

                    <% If detailtable IsNot Nothing Then%>
                    <% If detailtable.Rows(0).Item("statusid") = 2 Or detailtable.Rows(0).Item("statusid") = 9 Then%>
                    <% If detailtable.Rows(0).Item("approvallistid") = 23 Then%>

                    <div class="row bg-white">
                        <div class="col-md-12 col-12 mb-3 ">
                            <div class="card shadow">
                                <div class="card-header" style="background-color: cornflowerblue; color: white">
                                    <div class="row justify-content-between">
                                        <div class="col text-left">
                                            ผลการบีบทดสอบมิเตอร์หัวจ่ายด้วยหม้อตวงน้ำมันขนาด 5 ลิตร และ 20 ลิตร
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body">
                                    <div class="table-responsive">
                                        <table class="table table-bordered table-sm">
                                            <thead>
                                                <tr>
                                                    <th style="width: 5% !important;">ลำดับที่</th>
                                                    <th style="width: 10% !important;">ยี่ห้อ</th>
                                                    <th style="width: 5% !important;">ชนิดน้ำมัน</th>
                                                    <th style="width: 20% !important;">เลขที่มาตร</th>
                                                    <th style="width: 5% !important;">หัวจ่าย</th>
                                                    <th style="width: 10% !important;">ครั้งที่ 1</th>
                                                    <th style="width: 10% !important;">ครั้งที่ 2</th>
                                                    <th style="width: 10% !important;">ครั้งที่ 3</th>
                                                    <th style="width: 10% !important;">เฉลี่ย</th>
                                                    <th style="width: 15% !important;">หมายเหตุ</th>
                                                    <th style="width: 15% !important;"></th>
                                                </tr>
                                            </thead>
                                            <tbody class="text-center align-bottom">
                                                <% If Not Request.QueryString("approvalcode") Is Nothing And nozzletable.Rows.Count > 0 Then%>
                                                <% For j = 0 To nozzletable.Rows.Count - 1 %>
                                                <tr>
                                                    <td>
                                                        <span><%= nozzletable.Rows(j).Item("rownumber").ToString %></span>
                                                    </td>
                                                    <td>
                                                        <span><%= nozzletable.Rows(j).Item("brand").ToString %></span>
                                                    </td>
                                                    <td>
                                                        <span><%= nozzletable.Rows(j).Item("productType").ToString %></span>
                                                    </td>
                                                    <td>
                                                        <span><%= nozzletable.Rows(j).Item("nozzle_No").ToString %></span>
                                                    </td>
                                                    <td>
                                                        <span><%= nozzletable.Rows(j).Item("positionOnAssest").ToString %></span>
                                                    </td>
                                                    <td>
                                                        <div class="d-flex flex-column text-center ">
                                                            <span><% If Not String.IsNullOrEmpty(nozzletable.Rows(j).Item("rond1").ToString()) Then %>
                                                                <% If nozzletable.Rows(j).Item("rond1") > 0 Then %>
                                                                +<%= nozzletable.Rows(j).Item("rond1").ToString %>
                                                                <% else If nozzletable.Rows(j).Item("rond1") < 0 Then %>
                                                                (<%= (nozzletable.Rows(j).Item("rond1") * -1).ToString %>)
                                                                <% else %>
                                                                <%= nozzletable.Rows(j).Item("rond1").ToString %>
                                                                <% End If %>
                                                                <% End If %>
                                                            </span>
                                                            <% If Not String.IsNullOrEmpty(nozzletable.Rows(j).Item("url1").ToString()) Then %>
                                                            <a class="myDIV" href="<%= nozzletable.Rows(j).Item("url1").ToString() %>" target="_blank">รูป 1</a>
                                                            <img class="hide" src="<%= nozzletable.Rows(j).Item("url1").ToString() %>"
                                                                alt="Alternate Text" />
                                                            <% End If %>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="d-flex flex-column text-center ">
                                                            <span><% If Not String.IsNullOrEmpty(nozzletable.Rows(j).Item("rond2").ToString()) Then %>
                                                                <% If nozzletable.Rows(j).Item("rond2") > 0 Then %>
                                                                +<%= nozzletable.Rows(j).Item("rond2").ToString %>
                                                                <% else If nozzletable.Rows(j).Item("rond2") < 0 Then %>
                                                                (<%= (nozzletable.Rows(j).Item("rond2") * -1).ToString %>)
                                                                <% else  %>
                                                                <%= nozzletable.Rows(j).Item("rond2").ToString %>
                                                                <% End If %>
                                                                <% End If %>
                                                            </span>
                                                            <% If Not String.IsNullOrEmpty(nozzletable.Rows(j).Item("url2").ToString()) Then %>
                                                            <a class="myDIV" href="<%= nozzletable.Rows(j).Item("url2").ToString() %>" target="_blank">รูป 2</a>
                                                            <img class="hide" src="<%= nozzletable.Rows(j).Item("url2").ToString() %>"
                                                                alt="Alternate Text" />
                                                            <% End If %>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <div class="d-flex flex-column text-center ">
                                                            <span><% If Not String.IsNullOrEmpty(nozzletable.Rows(j).Item("rond3").ToString()) Then %>
                                                                <% If nozzletable.Rows(j).Item("rond3") > 0 Then %>
                                                                +<%= nozzletable.Rows(j).Item("rond3").ToString %>
                                                                <% else If nozzletable.Rows(j).Item("rond3") < 0 Then %>
                                                                (<%= (nozzletable.Rows(j).Item("rond3") * -1).ToString %>)
                                                                <% else  %>
                                                                <%= nozzletable.Rows(j).Item("rond3").ToString %>
                                                                <% End If %>
                                                                <% End If %>
                                                            </span>
                                                            <% If Not String.IsNullOrEmpty(nozzletable.Rows(j).Item("url3").ToString()) Then %>
                                                            <a class="myDIV" href="<%= nozzletable.Rows(j).Item("url3").ToString() %>" target="_blank">รูป 3</a>
                                                            <img class="hide" src="<%= nozzletable.Rows(j).Item("url3").ToString() %>"
                                                                alt="Alternate Text" />
                                                            <% End If %>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <span><% If Not String.IsNullOrEmpty(nozzletable.Rows(j).Item("avgAllRond").ToString()) Then %>
                                                            <% If nozzletable.Rows(j).Item("avgAllRond") > 0 Then %>
                                                                +<%= nozzletable.Rows(j).Item("avgAllRond").ToString %>
                                                            <% else If nozzletable.Rows(j).Item("avgAllRond") < 0 Then %>
                                                                (<%= (nozzletable.Rows(j).Item("avgAllRond") * -1).ToString %>)
                                                                <% else  %>
                                                            <%= nozzletable.Rows(j).Item("avgAllRond").ToString %>
                                                            <% End If %>
                                                            <% End If %></span>
                                                    </td>
                                                    <td>
                                                        <span><%= nozzletable.Rows(j).Item("remark").ToString %></span>
                                                    </td>
                                                    <td>

                                                        <% if string.IsNullOrEmpty(nozzletable.Rows(j).Item("rond1").ToString()) And String.IsNullOrEmpty(nozzletable.Rows(j).Item("rond2").ToString()) And String.IsNullOrEmpty(nozzletable.Rows(j).Item("rond3").ToString()) Then %>
                                                        <a title="EditDetail" onclick="btnEditDetailClick('<%= nozzletable.Rows(j).Item("approvalNozzle_ID").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("rond1").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("rond2").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("rond3").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("url1").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("url2").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("url3").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("remark").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("rowNumber").ToString() %>'
                                                                         );"><i class="fas fa-edit color__purple"></i></a>&nbsp;&nbsp;
                                                        <% else %>
                                                        <a onclick="confirmDeletedetail('<%= nozzletable.Rows(j).Item("approvalNozzle_ID").ToString() %>','<%= nozzletable.Rows(j).Item("rownumber").ToString() %>')" class="btn btn-sm p-0 NotPrint">
                                                            <i class="fas fa-times"></i>
                                                        </a>
                                                        <% End if %>

                                                    </td>
                                                </tr>

                                                <% Next j %>
                                                <% End if %>
                                            </tbody>
                                        </table>
                                    </div>

                                </div>
                                <!-- end card-->
                            </div>
                        </div>
                        <div class="col-md-12 col-12 mb-3  5l d-none">
                            <div class="card shadow">
                                <div class="card-header" style="background-color: navy; color: white">
                                    <div class="row justify-content-between">
                                        <div class="col text-left">
                                            ผลการบีบทดสอบมิเตอร์หัวจ่ายด้วยหม้อตวงน้ำมันขนาด 5 ลิตร และ 20 ลิตร
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body">
                                    <div class="table-responsive">
                                        <table class="table table-bordered table-sm">
                                            <thead>
                                                <tr>
                                                    <th style="width: 5% !important;">ลำดับที่</th>
                                                    <th style="width: 10% !important;">ยี่ห้อ</th>
                                                    <th style="width: 5% !important;">ชนิดน้ำมัน</th>
                                                    <th style="width: 20% !important;">เลขที่มาตร</th>
                                                    <th style="width: 5% !important;">หัวจ่าย</th>
                                                    <th style="width: 10% !important;">ครั้งที่ 1</th>
                                                    <th style="width: 10% !important;">ครั้งที่ 2</th>
                                                    <th style="width: 10% !important;">ครั้งที่ 3</th>
                                                    <th style="width: 10% !important;">เฉลี่ย</th>
                                                    <th style="width: 15% !important;">หมายเหตุ</th>
                                                </tr>
                                            </thead>
                                            <tbody class="text-center align-bottom">
                                                <% If Not Request.QueryString("approvalcode") Is Nothing And nozzletable.Rows.Count > 0 Then%>
                                                <% For j = 0 To nozzletable.Rows.Count - 1 %>
                                                <tr>
                                                    <td>
                                                        <span><%= nozzletable.Rows(j).Item("rownumber").ToString %></span>
                                                    </td>
                                                    <td>
                                                        <span><%= nozzletable.Rows(j).Item("brand").ToString %></span>
                                                    </td>
                                                    <td>
                                                        <span><%= nozzletable.Rows(j).Item("productType").ToString %></span>
                                                    </td>
                                                    <td>
                                                        <span><%= nozzletable.Rows(j).Item("nozzle_No").ToString %></span>
                                                    </td>
                                                    <td>
                                                        <span><%= nozzletable.Rows(j).Item("positionOnAssest").ToString %></span>
                                                    </td>
                                                    <td>
                                                        <input class="form-control" type="number" name="nozzleround" value="" id="nozzleround6_<%= nozzletable.Rows(j).Item("rownumber").ToString %>" autocomplete="off" onchange="calavg(this)" />
                                                    </td>
                                                    <td>
                                                        <input class="form-control" type="number" name="nozzleround" value="" id="nozzleround7_<%= nozzletable.Rows(j).Item("rownumber").ToString %>" autocomplete="off" onchange="calavg(this)" />
                                                    </td>
                                                    <td>
                                                        <input class="form-control" type="number" name="nozzleround" value="" id="nozzleround8_<%= nozzletable.Rows(j).Item("rownumber").ToString %>" autocomplete="off" onchange="calavg(this)" />
                                                    </td>
                                                    <td>
                                                        <span>0.0</span>
                                                    </td>
                                                    <td>
                                                        <textarea rows="1" cols="40" class="form-control" name="actiontitle" id="actiontitle10_<%= nozzletable.Rows(j).Item("rownumber").ToString %>"></textarea>
                                                    </td>
                                                </tr>

                                                <% Next j %>
                                                <% End if %>
                                            </tbody>
                                        </table>
                                    </div>

                                </div>
                                <!-- end card-->
                            </div>
                        </div>
                    </div>

                    <% End If %>
                    <% End If %>
                    <% End If %>
                    <!-- end row-->
                    <% If detailtable IsNot Nothing Then%>
                    <% If detailtable.Rows(0).Item("statusid") = 2 Or detailtable.Rows(0).Item("statusid") = 9 Then%>

                    <div class="row bg-white">
                        <div class="col-12 text-center">
                            <asp:Button ID="btnClose" class="btn btn-danger" runat="server" OnClientClick="validateDataImg()" Text="ปิดงาน" UseSubmitBehavior="false" />
                        </div>
                    </div>
                    <% End If %>
                    <!-- end status = 2 -->
                    <% End If %>


                    <% End If %>
                    <!-- end has approvalcode -->

                    <% Else %>
                    <!-- else flag -->
                    <div class="noContent">
                        <div class="row">
                            <div class="col">
                                <div class="text-center">
                                    คุณไม่มีสิทธ์เข้าถึงข้อมูล กรุณาติดต่อ ADMIN
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col text-center">
                                <a href="#" onclick="goBack()" class="btn btn-sm btn-danger "><-- กลับ</a>
                            </div>
                        </div>
                    </div>
                    <% End If %>
                    <!-- end flag -->
                </div>
                <!-- end container-->

            </div>
            <!-- end content-->

        </div>
        <!-- end wrapper-->

    </div>
    <!-- end bg-->
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
                    <div class="form-group">
                        <div class="row align-items-baseline">
                            <div class="col-auto">
                                <asp:Label ID="Label1" CssClass="form-label" AssociatedControlID="txtRound1" runat="server" Text="ครั้งที่ 1" />
                                <asp:Label ID="Label6" CssClass="text-danger" AssociatedControlID="txtRound1" runat="server" Text="*" />
                            </div>
                            <div class="col">
                                <asp:TextBox class="form-control noEnterSubmit" type="number" ID="txtRound1" runat="server" min="0" Text="0" onchange="calculateAVG();"></asp:TextBox>
                            </div>
                        </div>
                        <div class="file_rond1">
                            <input type="file" name="files" id="file_rond1" accept="image/*,.pdf" data-fileuploader-limit="1">
                        </div>
                    </div>
                    <hr />
                    <div class="form-group">
                        <div class="row align-items-baseline">
                            <div class="col-auto">
                                <asp:Label ID="Label3" CssClass="form-label" AssociatedControlID="txtRound2" runat="server" Text="ครั้งที่ 2" />
                                <%--<asp:Label ID="Label5" CssClass="text-danger" AssociatedControlID="txtRound2" runat="server" Text="*" />--%>
                            </div>
                            <div class="col">
                                <asp:TextBox class="form-control noEnterSubmit" type="number" ID="txtRound2" runat="server" min="0" Text="0" onchange="calculateAVG();"></asp:TextBox>
                            </div>
                        </div>
                        <div class="file_rond2">
                            <input type="file" name="files" id="file_rond2" accept="image/*,.pdf" data-fileuploader-listinput="file_rond2" data-fileuploader-limit="1" data-fileuploader-files=''>
                        </div>
                    </div>
                    <hr />
                    <div class="form-group">
                        <div class="row align-items-baseline">
                            <div class="col-auto">
                                <asp:Label ID="Label4" CssClass="form-label" AssociatedControlID="txtRound3" runat="server" Text="ครั้งที่ 3" />
                                <%--<asp:Label ID="Label7" CssClass="text-danger" AssociatedControlID="txtRound3" runat="server" Text="*" />--%>
                            </div>
                            <div class="col">
                                <asp:TextBox class="form-control noEnterSubmit" type="number" ID="txtRound3" runat="server" min="0" Text="0" onchange="calculateAVG();"></asp:TextBox>
                            </div>
                        </div>
                        <div class="file_rond3">
                            <input type="file" name="files" id="file_rond3" accept="image/*,.pdf" data-fileuploader-listinput="file_rond3" data-fileuploader-limit="1" data-fileuploader-files=''>
                        </div>
                    </div>
                    <div class="showCost">
                        <p class="text-muted" id="p_avg"></p>
                    </div>
                    <hr />
                    <div class="form-group">
                        <asp:Label ID="Label2" CssClass="form-label" AssociatedControlID="txtRemark" runat="server" Text="หมายเหตุ" />
                        <asp:TextBox class="form-control" ID="txtRemark" runat="server" Rows="3" Columns="50" TextMode="MultiLine" onkeyDown="checkTextAreaMaxLength(this,event,'255');" autocomplete="off"></asp:TextBox>
                        <div class="invalid-feedback">กรุณากรอกรายละเอียด</div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary noEnterSubmit" data-dismiss="modal">Close</button>
                    <asp:Button ID="btnAddDetails" class="btn btn-primary" runat="server" Text="Save" OnClientClick="postBack_addDetail();" UseSubmitBehavior="false"/>
                </div>
            </div>
        </div>
    </div>

    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>

    <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>

    <script src="<%=Page.ResolveUrl("~/js/NonPO.js")%>"></script>
    <script type="text/javascript">
        jQuery('[id$=txtCloseDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08'
            timepicker: true,
            scrollInput: false,
            format: 'd/m/Y H:i'
        });
    </script>

    <script>
        function goBack() {
            window.location.href = '../approval/ApprovalMenuList.aspx';
        }
    </script>
    <script type="text/javascript">

        $(document).ready(function () {

            $('input[name="files"]').fileuploader({
                example: ['pdf', 'image/*'],
                fileMaxSize: 30,
                limit: 3,
                changeInput: ' ',
                theme: 'thumbnails',
                enableApi: true,
                addMore: true,
                thumbnails: {
                    box: '<div class="fileuploader-items">' +
                        '<ul class="fileuploader-items-list" style="text-align: center;">' +
                        '<li class="fileuploader-thumbnails-input"><div class="fileuploader-thumbnails-input-inner"><i>+</i></div></li>' +
                        '</ul>' +
                        '</div>',
                    item: '<li class="fileuploader-item">' +
                        '<div class="fileuploader-item-inner">' +
                        '<div class="type-holder">${extension}</div>' +
                        '<div class="actions-holder">' +
                        '<button type="button" class="fileuploader-action fileuploader-action-remove" title="${captions.remove}"><i class="fileuploader-icon-remove"></i></button>' +
                        '</div>' +
                        '<div class="thumbnail-holder">' +
                        '${image}' +
                        '<span class="fileuploader-action-popup"></span>' +
                        '</div>' +
                        '<div class="content-holder"><h5>${name}</h5><span>${size2}</span></div>' +
                        '<div class="progress-holder">${progressBar}</div>' +
                        '</div>' +
                        '</li>',
                    item2: '<li class="fileuploader-item">' +
                        '<div class="fileuploader-item-inner">' +
                        '<div class="type-holder">${extension}</div>' +
                        '<div class="actions-holder">' +
                        '<a href="${file}" class="fileuploader-action fileuploader-action-download" title="${captions.download}" download><i class="fileuploader-icon-download"></i></a>' +
                        '<button type="button" class="fileuploader-action fileuploader-action-remove" title="${captions.remove}"><i class="fileuploader-icon-remove"></i></button>' +
                        '</div>' +
                        '<div class="thumbnail-holder">' +
                        '${image}' +
                        '<span class="fileuploader-action-popup"></span>' +
                        '</div>' +
                        '<div class="content-holder"><h5 title="${name}">${name}</h5><span>${size2}</span></div>' +
                        '<div class="progress-holder">${progressBar}</div>' +
                        '</div>' +
                        '</li>',
                    startImageRenderer: true,
                    useObjectUrl: false,
                    canvasImage: false,
                    _selectors: {
                        list: '.fileuploader-items-list',
                        item: '.fileuploader-item',
                        start: '.fileuploader-action-start',
                        retry: '.fileuploader-action-retry',
                        remove: '.fileuploader-action-remove'
                    },
                    onItemShow: function (item, listEl, parentEl, newInputEl, inputEl) {
                        var plusInput = listEl.find('.fileuploader-thumbnails-input'),
                            api = $.fileuploader.getInstance(inputEl.get(0));

                        plusInput.insertAfter(item.html)[api.getOptions().limit && api.getChoosedFiles().length >= api.getOptions().limit ? 'hide' : 'show']();

                        if (item.format == 'image') {
                            item.html.find('.fileuploader-item-icon').hide();
                        }
                    },
                    onItemRemove: function (html, listEl, parentEl, newInputEl, inputEl) {
                        var plusInput = listEl.find('.fileuploader-thumbnails-input'),
                            api = $.fileuploader.getInstance(inputEl.get(0));

                        html.children().animate({ 'opacity': 0 }, 200, function () {
                            html.remove();

                            if (api.getOptions().limit && api.getChoosedFiles().length - 1 < api.getOptions().limit)
                                plusInput.show();
                        });
                    }
                },
                dragDrop: {
                    container: '.fileuploader-thumbnails-input'
                },
                afterRender: function (listEl, parentEl, newInputEl, inputEl) {
                    var plusInput = listEl.find('.fileuploader-thumbnails-input'),
                        api = $.fileuploader.getInstance(inputEl.get(0));

                    plusInput.on('click', function () {
                        api.open();
                    });

                    api.getOptions().dragDrop.container = plusInput;
                }
            });
            $('.form-control').selectpicker({
                liveSearch: true,
                maxOptions: 1
            });
            /*const urlParams = new URLSearchParams(window.location.search);
            const approvalcode = urlParams.get('approvalcode');
            console.log(approvalcode);

            console.log(status.value)

            if (status.value == "อนุมัติ" || status.value == "เอกสารครบถ้วน") {
                if (img1.getAttribute('src') != "#") {
                    console.log("inif1")

                    $('.wrap-img-after').hide();
                    $('.image-title-wrap').hide();
                    $('.file-img-after').show();
                } else {
                    $(".img-after").hide();
                }

                if (img2.getAttribute('src') != "#") {
                    console.log("inif2")

                    $('.wrap-img-bill').hide();
                    $('.image-title-wrap').hide();
                    $('.file-img-bill').show();
                } else {
                    $(".img-bill").hide();
                }
            }
            if (status.value == "ปิดงาน") {
                if (img1.getAttribute('src') != "#") {
                    console.log("inif1")

                    $('.wrap-img-after').hide();
                    $('.image-title-wrap').hide();
                    $('.file-img-after').show();
                } else {
                    $(".img-after").hide();
                }

                if (img2.getAttribute('src') != "#") {
                    console.log("inif2")

                    $('.wrap-img-bill').hide();
                    $('.image-title-wrap').hide();
                    $('.file-img-bill').show();
                } else {
                    $(".img-bill").hide();
                }
            }*/
            console.log('---');
        });

    </script>

    <script type="text/javascript">
        function valueChangedImgAfter() {
            if ($('.chk-img-after').is(":checked"))
                $(".img-after").show();
            else
                $(".img-after").hide();

        }
        function valueChangedImgBill() {
            if ($('.chk-img-bill').is(":checked"))
                $(".img-bill").show();
            else
                $(".img-bill").hide();

        }
        function checkTextAreaMaxLength(textBox, e, length) {
            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);

            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event)//IE
                        e.returnValue = false;
                    else//Firefox
                        e.preventDefault();
                }
            }
        }
        function checkSpecialKeys(e) {
            if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
                return false;
            else
                return true;
        }
        function validateDataImg() {
            validateData();
            const chkAfter = document.getElementById('<%= chkAfter.ClientID%>');
            const chkBill = document.getElementById('<%= chkBill.ClientID%>');

            <% If detailtable IsNot Nothing Then%>
            <% If detailtable.Rows(0).Item("statusid") = 2 Or detailtable.Rows(0).Item("statusid") = 9 Then%>
            <% If Not (detailtable.Rows(0).Item("approvallistid") = 23) Then%>
            const secid = "<%= Session("secid")%>"
            if (secid != "2" && secid != "35") {
                //alert("in secid != 2")
                if (!($('#<%= chkAfter.ClientID%>').prop("checked")) && !($('#<%= chkBill.ClientID%>').prop("checked"))) {
                    alertWarning('ต้องมีรูปอย่างใดอย่างนึง');
                    event.preventDefault();
                    event.stopPropagation();
                    return false;
                }
            } else {
                const codeGSM = document.getElementById('<%= txtCodeGSM.ClientID%>').value;
                /*if (!(codeGSM)) {
                    alertWarning('กรุณาใส่รหัส GSM');
                    event.preventDefault();
                    event.stopPropagation();
                    return false;
                }*/
            }

            if (($('#<%= chkAfter.ClientID%>').prop("checked")) && $('.file_af input[type=file]').length == 1) {
                alertWarning('ใส่รูปหลังปฏิบัติงาน');
                event.preventDefault();
                event.stopPropagation();
                return false;
            }
            if (($('#<%= chkBill.ClientID%>').prop("checked")) && $('.file_bill input[type=file]').length == 1) {
                alertWarning('ใส่รูปใบเสร็จ');
                event.preventDefault();
                event.stopPropagation();
                return false;
            }
            //console.log('--1-');

            reNameAttr($('.file_af input[type=file]'), 'approval_af')
            reNameAttr($('.file_bill input[type=file]'), 'approval_bill')

           <%-- <% If detailtable IsNot Nothing Then%>
            <% If detailtable.Rows(0).Item("statusid") = 2 Or detailtable.Rows(0).Item("statusid") = 9 Then%>
            <% If detailtable.Rows(0).Item("approvallistid") = 23 Then%>

            if (validate5l()) {
                alertWarning('กรุณาใส่ข้อมูลให้ครบ');
                event.preventDefault();
                event.stopPropagation();
                return false;
            } else {
                let param = getValue();
                const params = JSON.stringify(param);
                //console.log(params);
                let confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                confirm_value.value = params;
                document.forms[0].appendChild(confirm_value);
            }
            <% End If %>
            <% End If %>
            <% End If %>--%>
            <% End If %>
            <% End If %>
            <% End If %>
        }

        function reNameAttr(ele, newName) {
            for (var i = 0; i < ele.length - 1; i++) {
                ele.eq(i).attr('name', newName);

            }
        }

    </script>
    <script type="text/javascript">
        function alertSuccessUpload() {
            Swal.fire({
                title: 'อัปโหลดสำเร็จ',
                icon: 'success',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'ยืนยัน',
                allowOutsideClick: false
            }).then((result) => {
                if (result.isConfirmed) {
                    const urlParams = new URLSearchParams(window.location.search);
                    const approvalcode = urlParams.get('approvalcode');
                    window.location.href = '../approval/approval.aspx?approvalcode=' + approvalcode;
                }
            })
        }
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
        function calavg() {
            const tables = document.querySelectorAll('.5l table');
            //console.log(tables);
            //console.log(typeof (tables));
            //console.log(tables[0].getElementsByTagName('tbody')[0].rows);
            //    const tableBodyRows = tables[0].getElementsByTagName('tbody')[0].rows;
            tables.forEach(table => {
                const tbodys = [];
                let sum = 0
                tbodys.push(table.getElementsByTagName('tbody')[0].rows);
                //console.log(tbodys);
                //console.log(tbodys[0].length);
                if (tbodys[0].length > 0) {
                    for (let i = 0; i < tbodys[0].length; i++) {
                        const allTheValues = []
                        let sum = 0
                        allTheValues.push(parseInt((tbodys[0][i].cells[5].getElementsByTagName('input')[0].value) ? tbodys[0][i].cells[5].getElementsByTagName('input')[0].value : 0))
                        allTheValues.push(parseInt((tbodys[0][i].cells[6].getElementsByTagName('input')[0].value) ? tbodys[0][i].cells[6].getElementsByTagName('input')[0].value : 0))
                        allTheValues.push(parseInt((tbodys[0][i].cells[7].getElementsByTagName('input')[0].value) ? tbodys[0][i].cells[7].getElementsByTagName('input')[0].value : 0))
                        const result = allTheValues.filter((val) => val > 0);
                        sum = result.reduce((acc, val) => acc + val, 0);
                        const avg = ((sum / result.length) || 0).toFixed(1)
                        tbodys[0][i].cells[8].innerHTML = avg;
                        //console.log(`row ${i} : ${avg}`);
                    }

                }

            });


        }
        function validate5l() {

            let res = false;
            const tables = document.querySelectorAll('.5l table');
            //console.log(tables);
            //console.log(typeof (tables));
            //console.log(tables[0].getElementsByTagName('tbody')[0].rows);
            //    const tableBodyRows = tables[0].getElementsByTagName('tbody')[0].rows;
            tables.forEach(table => {
                const allTheValues = []
                const tbodys = [];
                let sum = 0
                tbodys.push(table.getElementsByTagName('tbody')[0].rows);
                //console.log(tbodys);
                //console.log(tbodys[0].length);
                if (tbodys[0].length > 0) {
                    for (let i = 0; i < tbodys[0].length; i++) {
                        allTheValues.push(parseInt((tbodys[0][i].cells[5].getElementsByTagName('input')[0].value) ? tbodys[0][i].cells[5].getElementsByTagName('input')[0].value : 0))
                        allTheValues.push(parseInt((tbodys[0][i].cells[6].getElementsByTagName('input')[0].value) ? tbodys[0][i].cells[6].getElementsByTagName('input')[0].value : 0))
                        allTheValues.push(parseInt((tbodys[0][i].cells[7].getElementsByTagName('input')[0].value) ? tbodys[0][i].cells[7].getElementsByTagName('input')[0].value : 0))

                        //console.log(allTheValues);
                        if (allTheValues.some(item => item == 0)) {
                            res = true;
                            break;
                        }
                    }

                }

            });
            return res;
        }

        function getValue() {



            const tables = document.querySelectorAll('.5l table');
            var params = [];
            //console.log(tables);
            //console.log(typeof (tables));
            //console.log(tables[0].getElementsByTagName('tbody')[0].rows);
            //    const tableBodyRows = tables[0].getElementsByTagName('tbody')[0].rows;
            tables.forEach(table => {
                const tbodys = [];
                let sum = 0
                tbodys.push(table.getElementsByTagName('tbody')[0].rows);
                console.log(tbodys);
                //console.log(tbodys[0].length);
                if (tbodys[0].length > 0) {
                    for (let i = 0; i < tbodys[0].length; i++) {
                        params.push({
                            "rownumber": tbodys[0][i].cells[0].getElementsByTagName('span')[0].innerHTML,
                            "brand": tbodys[0][i].cells[1].getElementsByTagName('span')[0].innerHTML,
                            "producttype": tbodys[0][i].cells[2].getElementsByTagName('span')[0].innerHTML,
                            "nozzle_no": tbodys[0][i].cells[3].getElementsByTagName('span')[0].innerHTML,
                            "positiononassest": tbodys[0][i].cells[4].getElementsByTagName('span')[0].innerHTML,
                            "round1": parseInt(tbodys[0][i].cells[5].getElementsByTagName('input')[0].value),
                            "round2": parseInt(tbodys[0][i].cells[6].getElementsByTagName('input')[0].value),
                            "round3": parseInt(tbodys[0][i].cells[7].getElementsByTagName('input')[0].value),
                            "url1": '',
                            "url2": '',
                            "url3": '',
                            "remark": tbodys[0][i].cells[9].getElementsByTagName('textarea')[0].innerHTML
                        });

                    }

                }

            });

            return params;
        }
        function btnEditDetailClick(approvalNozzle_ID, round1, round2, round3, url1, url2, url3, remark, row) {

            clearfromadddetail();
            calculateAVG();
            $('#exampleModal').modal('show');

            $('#<%= row.ClientID%>').val(row);
            $('#<%= hiddenAdvancedetailid.ClientID%>').val(approvalNozzle_ID);
            $('#<%= txtRound1.ClientID%>').val(round1);
            $('#<%= txtRound2.ClientID%>').val(round2);
            $('#<%= txtRound3.ClientID%>').val(round3);
            $('#<%= txtRemark.ClientID%>').val(remark);


            $('.form-control').selectpicker('refresh');

        }

        function calculateAVG() {

            //console.log("############ calculate");


            let round1 = CheckNumber(document.getElementById("<%= txtRound1.ClientID%>").value);
            let round2 = CheckNumber(document.getElementById("<%= txtRound2.ClientID%>").value);
            let round3 = CheckNumber(document.getElementById("<%= txtRound3.ClientID%>").value);

            const p_avg = document.getElementById("p_avg");


            round1 = parseFloat(round1);
            round2 = parseFloat(round2);
            round3 = parseFloat(round3);
            let sum = round1 + round2 + round3

            const avg = ((sum / 3) || 0).toFixed(1)


            //console.log(calCostTotal(cost, vat, tax).toFixed(2));
            //console.log(calVat(cost, vat).toFixed(2));
            //console.log(calTax(cost, tax).toFixed(2));

            if (!isNaN(avg) && (avg - 0) < 999999999.9999) {
                p_avg.innerHTML = `เฉลี่ย : ${avg}`;
            } else {
                p_avg.innerHTML = "";
            }

        }

        function clearfromadddetail() {


            $('#<%= row.ClientID%>').val(0);
            $('#<%= hiddenAdvancedetailid.ClientID%>').val(0);
            $('#<%= txtRound1.ClientID%>').val(0);
            $('#<%= txtRound2.ClientID%>').val(0);
            $('#<%= txtRound3.ClientID%>').val(0);
            $('#<%= txtRemark.ClientID%>').val('');

            $('.file_rond1 input[type=hidden]').val('[]');
            $('.file_rond1 .fileuploader-item').remove();
            $('.file_rond1 .fileuploader-thumbnails-input').show();

            $('.file_rond2 input[type=hidden]').val('[]');
            $('.file_rond2 .fileuploader-item').remove();
            $('.file_rond2 .fileuploader-thumbnails-input').show();

            $('.file_rond3 input[type=hidden]').val('[]');
            $('.file_rond3 .fileuploader-item').remove();
            $('.file_rond3 .fileuploader-thumbnails-input').show();

            $('.form-control').selectpicker('refresh');
        }
        function postBack_addDetail() {
            let row = $('#<%= row.ClientID%>').val();
            const approvalNozzle_ID = $('#<%= hiddenAdvancedetailid.ClientID%>').val();
            const Round1 = $('#<%= txtRound1.ClientID%>').val();
            const Round2 = $('#<%= txtRound2.ClientID%>').val();
            const Round3 = $('#<%= txtRound3.ClientID%>').val();
            const Remark = $('#<%= txtRemark.ClientID%>').val();


            if (!(Round1)) {
                alertWarning('กรุณากรอกข้อมูลให้ครบ');
                event.preventDefault();
                event.stopPropagation();
                return 0;
            }

            if (Round1 && !(parseFloat(Round1) >= (-50.00) && parseFloat(Round1) <= (50.00))) {
                alertWarning('กรุณากรอกข้อมูลอยู่ในช่วง -50.0 ถึง 50.00');
                event.preventDefault();
                event.stopPropagation();
                return 0;
            }

            if (Round2 && !(parseFloat(Round2) >= (-50.00) && parseFloat(Round2) <= (50.00))) {
                alertWarning('กรุณากรอกข้อมูลอยู่ในช่วง -50.0 ถึง 50.00');
                event.preventDefault();
                event.stopPropagation();
                return 0;
            }

            if (Round3 && !(parseFloat(Round3) >= (-50.00) && parseFloat(Round3) <= (50.00))) {
                alertWarning('กรุณากรอกข้อมูลอยู่ในช่วง -50.0 ถึง 50.00');
                event.preventDefault();
                event.stopPropagation();
                return 0;
            }

            if (Round1 && $('.file_rond1 input[type=hidden]').val().length == 2 ){
                alertWarning('กรุณาแนบเอกสารให้ครบ');
                event.preventDefault();
                event.stopPropagation();
                return 0;
            }

            if (Round2 && $('.file_rond2 input[type=hidden]').val().length == 2 ){
                alertWarning('กรุณาแนบเอกสารให้ครบ');
                event.preventDefault();
                event.stopPropagation();
                return 0;
            }

            if (Round3 && $('.file_rond3 input[type=hidden]').val().length == 2 ){
                alertWarning('กรุณาแนบเอกสารให้ครบ');
                event.preventDefault();
                event.stopPropagation();
                return 0;
            }

            //if (!(Round1) ||
            //    !(Round2) ||
            //    !(Round3)) {
            //    alertWarning('กรุณากรอกข้อมูลให้ครบ');
            //    event.preventDefault();
            //    event.stopPropagation();
            //    return 0;
            //} else if (!(parseFloat(Round1) >= (-50.00) && parseFloat(Round1) <= (50.00)) ||
            //    !(parseFloat(Round2) >= (-50.00) && parseFloat(Round2) <= (50.00)) ||
            //    !(parseFloat(Round3) >= (-50.00) && parseFloat(Round3) <= (50.00))) {
            //    alertWarning('กรุณากรอกข้อมูลอยู่ในช่วง -50.0 ถึง 50.00');
            //    event.preventDefault();
            //    event.stopPropagation();
            //    return 0;
            //}

            //if ($('.file_rond1 input[type=hidden]').val().length == 2 ||
            //    $('.file_rond2 input[type=hidden]').val().length == 2 ||
            //    $('.file_rond3 input[type=hidden]').val().length == 2) {
            //    alertWarning('กรุณาแนบเอกสารให้ครบ');
            //    event.preventDefault();
            //    event.stopPropagation();
            //    return 0;
            //}


            //alert('ss')
            $('.file_rond1 input[type=file]').attr('name', 'nozzle_rond1');
            $('.file_rond2 input[type=file]').attr('name', 'nozzle_rond2');
            $('.file_rond3 input[type=file]').attr('name', 'nozzle_rond3');

            let param = [];
            //alert(row);
            //var params = "{'row': '" + row + "'}";
            param.push({
                "row": row,
                "approvalnozzle_id": approvalNozzle_ID,
                "round1": Round1,
                "round2": Round2,
                "round3": Round3,
                "remark": Remark
            });

            const params = JSON.stringify(param);
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

        function confirmDeletedetail(approvalnozzle_id, row) {
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

                    var params = "{'approvalnozzle_id': '" + approvalnozzle_id + "','row': '" + row + "'}";

                    __doPostBack('deletedetail', params);

                }
            })

            return false;
        }
        function containsOnlyNumbers(str) {
            return /^\d+\.\d$/.test(str);
        }
    </script>

</asp:Content>
