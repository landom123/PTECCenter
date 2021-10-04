<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="Approval.aspx.vb" Inherits="PTECCENTER.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- fonts -->
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,700" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/fileupload/dist/font/font-fileuploader.css")%>" rel="stylesheet">

    <link href="<%=Page.ResolveUrl("~/fileupload/dist/jquery.fileuploader.min.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/fileupload/dist/jquery.fileuploader-theme-thumbnails.css")%>" rel="stylesheet">
    <style>
        .file-upload-content, .image-upload-wrap, .fileuploader-theme-thumbnails .fileuploader-items .fileuploader-item .fileuploader-action + .fileuploader-action, .fileuploader-popup .fileuploader-popup-footer .fileuploader-popup-tools li [data-action="remove"], .fileuploader-popup .fileuploader-popup-footer .fileuploader-popup-zoomer {
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


        .checkGSM {
            /*display: none;*/
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

        .statusGSM {
            margin-top: 6.5px;
            margin-right: -11px;
            width: 15px !important;
            height: 15px !important;
            background: #FEBC2F; /*เขียว:#00FF27 เหลือง:#FEBC2F แดง:#ee443b*/
            border-radius: 50%;
            margin-bottom: 0.15rem !important;
        }

        .statusGSMName {
            padding-left: 5px;
            padding-right: 20px;
            font-size: 12px;
            text-align: left !important;
            align-self: center !important;
        }

        @media screen and (max-width: 768px) {


            .statusGSM {
                margin: auto auto;
            }

            .statusGSMName {
                display: none;
                /*padding-left: 15px;
                padding-right: 15px;
                text-align: center !important;*/
            }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg">
        <div id="wrapper">

            <div id="content-wrapper">
                <div class="container">

                    <% If flag Then%>

                    <div class="row bg-white">
                        <div class="col-xl-12 mb-3" id="grid">
                            <div class="card shadow">
                                <div class="card-header" style="background-color: navy; color: white">
                                    <div class="row justify-content-between">
                                        <div class="col text-left align-self-center">
                                            แจ้งขออนุมัติ
                                        </div>
                                        <div class="col">
                                            <div id="demo2" style="color: navy; font-size: 10px;"></div>
                                            <div id="demo" style="color: navy; font-size: 10px;"></div>
                                        </div>
                                        <% If Request.QueryString("approvalcode") IsNot Nothing And detailtable IsNot Nothing Then%>
                                        <% If detailtable.Rows(0).Item("category").ToString = "หักยอดขาย" Then%>
                                        <div class="col-auto align-self-center checkGSM">
                                            <div class="row">
                                                <div class="col-md">
                                                    <div class="statusGSM" id="stGSM"></div>
                                                </div>
                                                <div class="col-md-auto statusGSMName">
                                                    <span id="statusGSMName"></span>
                                                </div>
                                            </div>
                                        </div>
                                        <% End If %>
                                        <% End If %>
                                        <div class="col-auto text-right align-self-center">
                                            <a href="ApprovalMenuList.aspx" class="btn btn-sm btn-danger ">
                                                <i class="fa fa-tasks" aria-hidden="true"></i></a>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body">

                                    <% If Not Request.QueryString("approvalcode") Is Nothing Then%>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lbApprovalcode" CssClass="form-label" AssociatedControlID="txtApprovalcode" runat="server" Text="เลขที่ใบงาน" />
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
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lbCreateBy" CssClass="form-label" AssociatedControlID="txtCreateBy" runat="server" Text="ผู้แจ้งขออนุมัติ" />
                                                <asp:TextBox class="form-control" ID="txtCreateBy" runat="server" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lbDocDate" CssClass="form-label" AssociatedControlID="txtDocDate" runat="server" Text="วันที่แจ้ง" />
                                                <asp:TextBox class="form-control" ID="txtDocDate" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <% End If %>
                                    <!-- end แถว1-->
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Label ID="lbcboApproval" CssClass="form-label" AssociatedControlID="cboApproval" runat="server" Text="หัวข้อขออนุมัติ" />
                                                <asp:Label ID="lbcboApprovalMandatory" CssClass="text-danger" AssociatedControlID="cboApproval" runat="server" Text="*" />
                                                <asp:DropDownList class="form-control" ID="cboApproval" runat="server" required></asp:DropDownList>
                                                <div class="invalid-feedback">กรุณาเลือกหัวข้อขออณุมัติ</div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Label ID="lbtxtName" CssClass="form-label" AssociatedControlID="txtName" runat="server" Text="ชื่อผู้ขออนุมัติ" />
                                                <asp:Label ID="lbtxtNameMandatory" CssClass="text-danger" AssociatedControlID="txtName" runat="server" Text="*" />
                                                <asp:TextBox class="form-control" ID="txtName" runat="server" required></asp:TextBox>
                                                <div class="invalid-feedback">กรุณากรอกชื่อผู้ทำการแจ้งขออนุมัติ</div>
                                            </div>
                                        </div>
                                    </div>
                                    <% If Not Request.QueryString("approvalcode") Is Nothing And detailtable IsNot Nothing Then%>
                                    <% If detailtable.Rows(0).Item("statusid") = 4 Or detailtable.Rows(0).Item("working") Then%>
                                    <!-- status = ปิดงาน = 4 -->
                                    <!-- working = true มีสถานะ 2 อนุมัติ,8 รอประสานงานรับเรื่อง,9 ดำเนินการด้านเอกสาร,10 จัดส่งเอกสาร -->
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Label ID="lbtxtOwnerApprovalName" CssClass="form-label" AssociatedControlID="txtOwnerApprovalName" runat="server" Text="อนุมัติโดย" />
                                                <asp:TextBox class="form-control text-success font-weight-bold" ID="txtOwnerApprovalName" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Label ID="lbtxtOwnerApprovaldate" CssClass="form-label" AssociatedControlID="txtOwnerApprovaldate" runat="server" Text="อนุมัติเมื่อ" />
                                                <asp:TextBox class="form-control text-success font-weight-bold" ID="txtOwnerApprovaldate" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <% If detailtable.Rows(0).Item("statusid") = 11 Or detailtable.Rows(0).Item("statusid") = 10 Or detailtable.Rows(0).Item("statusid") = 9 Or (detailtable.Rows(0).Item("statusid") = 4 And Not detailtable.Rows(0).Item("supportby").ToString() = "") Then%>
                                        <!-- status = ปิดงาน = 4 -->
                                        <!-- status = รอแสกนเอกสาร = 11 -->
                                        <!-- status = จัดส่งเอกสาร = 10 -->
                                        <!-- status = ดำเนินการด้านเอกสาร = 9 -->
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Label ID="lbSupportby" CssClass="form-label" AssociatedControlID="txtSupportby" runat="server" Text="ประสานงานโดย" />
                                                <asp:TextBox class="form-control text-info font-weight-bold" ID="txtSupportby" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Label ID="lbSupportdate" CssClass="form-label" AssociatedControlID="txtSupportdate" runat="server" Text="เริ่มดำเนินการเมื่อ" />
                                                <asp:TextBox class="form-control text-info font-weight-bold" ID="txtSupportdate" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <% End If %>
                                        <% If detailtable.Rows(0).Item("statusid") = 10 Or (detailtable.Rows(0).Item("statusid") = 4 And Not detailtable.Rows(0).Item("supportby").ToString() = "") Then%>
                                        <!-- status = ปิดงาน = 4 -->
                                        <!-- status = จัดส่งเอกสาร = 10 -->
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Label ID="lbSupportFinished" CssClass="form-label" AssociatedControlID="txtSupportFinished" runat="server" Text="จัดส่งเอกสารเมื่อ" />
                                                <asp:TextBox class="form-control text-info font-weight-bold" ID="txtSupportFinished" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <% End If %>
                                        <% If detailtable.Rows(0).Item("statusid") = 4 Then%>
                                        <!-- status = ปิดงาน = 4 -->
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Label ID="Label2" CssClass="form-label" AssociatedControlID="txtCloseDate" runat="server" Text="วันที่ปิดงาน" />
                                                <asp:TextBox class="form-control text-danger font-weight-bold" ID="txtCloseDate" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <% If Not String.IsNullOrEmpty(detailtable.Rows(0).Item("codegsm").ToString) Then%>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Label ID="lbCodeGSM" CssClass="form-label" AssociatedControlID="txtCodeGSM" runat="server" Text="รหัส GSM" />
                                                <asp:TextBox class="form-control text-warning font-weight-bold" ID="txtCodeGSM" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <% End If %>
                                        <% End If %>
                                    </div>
                                    <% End If %>
                                    <% End If %>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Label ID="lbcboBranch" CssClass="form-label" AssociatedControlID="cboBranch" runat="server" Text="สาขา" />
                                                <asp:Label ID="lbcboBranchMandatory" CssClass="text-danger" AssociatedControlID="cboBranch" runat="server" Text="*" />
                                                <asp:DropDownList class="form-control" ID="cboBranch" runat="server" required></asp:DropDownList>
                                                <div class="invalid-feedback">กรุณาเลือกสาขา</div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Label ID="lbPrice" CssClass="form-label" AssociatedControlID="txtPrice" runat="server" Text="ค่าใช้จ่าย" />
                                                <asp:TextBox class="form-control" type="number" ID="txtPrice" runat="server" min="0" Text="0" required></asp:TextBox>
                                                <div class="invalid-feedback">* ตัวเลขจำนวนเต็ม</div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Label ID="lbtxtDay" CssClass="form-label" AssociatedControlID="txtDay" runat="server" Text="จำนวนวัน" />
                                                <asp:TextBox class="form-control" type="number" ID="txtDay" runat="server" min="0"></asp:TextBox>
                                                <div class="invalid-feedback">* ตัวเลขจำนวนเต็ม</div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row justify-content-md-center file_Approval_BF">
                                        <div class="col-md-10">
                                            <input type="file" name="files" accept="image/*,.pdf" data-fileuploader-files='<%=Approval_BF%>'>
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
                                    <% If Not Request.QueryString("approvalcode") Is Nothing And detailtable IsNot Nothing Then%>
                                    <% If detailtable.Rows(0).Item("statusid") = 3 Then%>
                                    <!-- status = ไม่อนุมัติ = 3 -->
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Label ID="lbOwnerDisApprovalName" CssClass="form-label" AssociatedControlID="txtOwnerDisApprovalName" runat="server" Text="ไม่อนุมัติโดย" />
                                                <asp:TextBox class="form-control text-danger font-weight-bold" ID="txtOwnerDisApprovalName" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Label ID="lbDisApproval" CssClass="form-label" AssociatedControlID="txtDisApproval" runat="server" Text="ไม่อนุมัติเนื่องจาก" />
                                                <asp:TextBox class="form-control text-danger font-weight-bold" ID="txtDisApproval" runat="server" Rows="3" Columns="50" TextMode="MultiLine" onkeyDown="checkTextAreaMaxLength(this,event,'255');" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <% End If %>
                                    <% If detailtable.Rows(0).Item("statusid") = 4 Then%>
                                    <!-- status = ปิดงาน = 4 -->
                                    <div class="approval">
                                        <!-- รายละเอียดปิดงาน  -->
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <asp:Label ID="lbtxtCloseDetail" CssClass="form-label" AssociatedControlID="txtCloseDetail" runat="server" Text="รายละเอียดปิดงาน" />
                                                    <asp:TextBox class="form-control text-danger font-weight-bold" ID="txtCloseDetail" runat="server" Rows="3" Columns="50" TextMode="MultiLine" onkeyDown="checkTextAreaMaxLength(this,event,'255');" ReadOnly="True"></asp:TextBox>
                                                    <div class="invalid-feedback">กรุณากรอกรายละเอียด</div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <% End If %>
                                    <% End If %>
                                    <% If Session("status") = "new" Then%>
                                    <div class="card-footer text-center bg-white">
                                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" OnClientClick="validateData()" />
                                    </div>
                                    <% ElseIf Session("status") = "read" And (Session("userid").ToString() = detailtable.Rows(0).Item("createby").ToString()) Then%>
                                    <div class="card-footer text-center bg-white">
                                        <asp:Button ID="btnConfirm" class="btn btn-warning" runat="server" Text="Confirm" OnClientClick="Confirm();" />
                                        <asp:Button ID="btnCancel" class="btn btn-danger" runat="server" Text="Cancel" />
                                        <asp:Button ID="btnClose" class="btn btn-danger" runat="server" Text="ปิดงาน" />
                                        <asp:Button ID="btnAddDoc" class="btn btn-success" runat="server" Text="แนบเอกสารให้ฝ่ายประสานงาน" />
                                        <asp:Button ID="btnEdit" class="btn btn-secondary" runat="server" Text="Edit" />

                                    </div>
                                    <% ElseIf Session("status") = "write" And detailtable.Rows(0).Item("statusid") = 1 Then%>
                                    <div class="card-footer text-center bg-white">
                                        <% If approval Then%>
                                        <asp:Button ID="btnApproval" class="btn btn-success" runat="server" Text="อนุมัติ" />
                                        <% End If %>
                                        <button runat="server" id="btnDisApproval" name="btnEdit" onclick="return disApproval();" class="btn btn-danger">
                                            ไม่อนุมัติ
                                        </button>
                                    </div>
                                    <% ElseIf Session("status") = "edit" Then%>
                                    <div class="card-footer text-center bg-white">
                                        <asp:Button ID="btnSaveEdit" class="btn btn-success" runat="server" Text="Save" OnClientClick="validateData()" />
                                        <asp:Button ID="btnCancelEdit" class="btn btn-danger" runat="server" Text="Cancel" />
                                    </div>
                                    <% End If %>
                                    <% If Not Request.QueryString("approvalcode") Is Nothing And detailtable IsNot Nothing And Session("secid").ToString = "2" Then%>
                                    <% If detailtable.Rows(0).Item("statusid") = 8 Then%>
                                    <div class="card-footer text-center bg-white" style="display:none;">
                                        <asp:Button ID="btnSupportKnowlange" class="btn btn-warning" runat="server" Text="รับเรื่อง" />
                                    </div>
                                    <% End If %>
                                    <% If (detailtable.Rows(0).Item("statusid") = 9) And Session("secid").ToString = "2" Then%>
                                    <div class="card-footer text-center bg-white">
                                        <asp:Button ID="btnSupportClose" class="btn btn-danger" runat="server" Text="ปิดงาน / กรอกรหัส" />
                                    </div>
                                    <% End If %>
                                    <% End If %>
                                </div>
                            </div>
                            <!-- end card-->

                        </div>
                        <!-- end col-lg-12-->
                        <% If not Session("status") = "new" Then%>
                        <div class="col-xl-4 files_approval">
                            <div class="row">
                                <div class="col-xl-12 mb-3  file_Approval_Doc">
                                    <div class="card shadow">
                                        <div class="card-header" style="background-color: #475b6f; color: white">
                                            <div class="row justify-content-between">
                                                <div class="col text-left">
                                                    แนบเอกสารให้ฝ่ายประสานงาน
                                                    (ใบแจ้งหนี้ / ใบเสร็จ / อื่นๆ)
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <div class="row justify-content-md-center">
                                                <div class="col-md-10">
                                                    <input type="file" name="files" accept="image/*,.pdf" data-fileuploader-files='<%=Approval_Doc%>'>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- end card-->
                                </div>

                                <div class="col-xl-12 mb-3 file_Approval_AT ">

                                    <div class="card shadow">
                                        <div class="card-header" style="background-color: #ffc107; color: white">
                                            <div class="row justify-content-between">
                                                <div class="col text-left">
                                                    รูปภาพหลังปฏิบัติงาน
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <div class="row justify-content-md-center">
                                                <div class="col-md-10">
                                                    <input type="file" name="files" accept="image/*,.pdf" data-fileuploader-files='<%=Approval_AT%>'>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- end card-->
                                </div>

                                <div class="col-xl-12 mb-3 file_Approval_Bill">
                                    <div class="card shadow">
                                        <div class="card-header" style="background-color: deeppink; color: white">
                                            <div class="row justify-content-between">
                                                <div class="col text-left">
                                                    รูปภาพใบเสร็จ
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <div class="row justify-content-md-center">
                                                <div class="col-md-10">
                                                    <input type="file" name="files" accept="image/*,.pdf" data-fileuploader-files='<%=Approval_Bill%>'>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- end card-->
                                </div>

                            </div>
                        </div>
                        <% End If %>
                    </div>
                    <!-- end row-->
                    <% If Not Request.QueryString("approvalcode") Is Nothing And detailtable IsNot Nothing And CommentTable IsNot Nothing Then%>
                    <% If (Session("status") = "read" Or Session("status") = "write") And Not detailtable.Rows(0).Item("statusid") = 7 Then%>
                    <!-- statusid = 7 รอผู้แจ้งยืนยัน-->
                    <div class="row bg-white">
                        <div class="col-lg-12">
                            <div class="card shadow">
                                <!--<div class="card-header">
                                    <div class="row justify-content-between">
                                        <div class="col text-left">
                                            Comment
                                        </div>
                                    </div>
                                </div>-->
                                <div class="card-body">

                                    <%--begin item--%>

                                    <div class="table-responsive">

                                        <% For i = 0 To CommentTable.Rows.Count - 1 %>
                                        <div class="row">
                                            <div class="col-md-12 h3">
                                                <%= CommentTable.Rows(i).Item("CreateBy").ToString() %>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 text-muted">
                                                <%= CommentTable.Rows(i).Item("CreateDate").ToString() %>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 h5">
                                                <%= CommentTable.Rows(i).Item("commentdetail").ToString() %>
                                            </div>
                                        </div>

                                        <%-- end detail row--%>
                                        <hr style="height: 2px; border-width: 0; color: gray; background-color: gray" />
                                        <% Next i %>

                                        <%if detailtable.Rows(0).Item("statusid") = 1 Then%>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <asp:TextBox class="form-control " ID="txtComment" runat="server" Rows="3" Columns="50" TextMode="MultiLine" onkeyDown="checkTextAreaMaxLength(this,event,'255');" placeholder="Comment . ." required></asp:TextBox>
                                                    <div class="invalid-feedback">กรุณากรอกรายละเอียด</div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row justify-content-center">
                                            <asp:Button ID="btnSaveComment" class="btn btn-success" runat="server" Text="Save" OnClientClick="validateData()" />
                                        </div>
                                        <% End If %>
                                    </div>
                                    <%-- end item--%>
                                </div>
                                <!-- end card body-->

                            </div>
                            <!-- end card-->

                        </div>
                        <!-- end col-lg-12-->
                    </div>
                    <!-- end row-->
                    <% End If %>
                    <% End If %>
                    <% Else %>
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
                </div>
                <!-- end container-->

            </div>
            <!-- end content-->

        </div>
        <!-- end wrapper-->

    </div>
    <!-- end bg-->


    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            //alert('t');
            $('input[name="files"]').fileuploader({
                example: ['pdf', 'image/*'],
                fileMaxSize: 15,
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
                    /*alert(listEl);
                    console.log(listEl.value);
                    console.log($('#fileuploader-list-files').value);
                    console.log($('#fileuploader-list-files').val.length);
                    console.log("parentEl : " + parentEl);
                    console.log(parentEl);
                    console.log("newInputEl : " + newInputEl);
                    console.log(newInputEl);
                    console.log("input : " + inputEl);
                    console.log(inputEl.length);*/

                    plusInput.on('click', function () {
                        api.open();
                    });

                    api.getOptions().dragDrop.container = plusInput;
                }/*, upload: {
                    url: 'approval.aspx?type=upload',
                    data: null,
                    type: 'POST',
                    enctype: 'multipart/form-data',
                    start: true,
                    synchron: true,
                    beforeSend: null,
                    onSuccess: function (result, item) {
                        console.log('res'+result);
                        alert('yes');
                        var data = {};


                        try {
                            data = JSON.parse(result);
                        } catch (e) {
                            data.hasWarnings = true;
                        }

                        // if success
                        if (data.isSuccess && data.files[0]) {
                            console.log('if success');
                            item.name = data.files[0].name;
                            item.html.find('.column-title > div:first-child').text(data.files[0].name).attr('title', data.files[0].name);
                        }

                        // if warnings
                        if (data.hasWarnings) {
                            console.log('if warnings');
                            console.log('if warnings' + data.hasWarnings);
                            for (var warning in data.warnings) {
                                alert(data.warnings);
                            }

                            item.html.removeClass('upload-successful').addClass('upload-failed');
                            // go out from success function by calling onError function
                            // in this case we have a animation there
                            // you can also response in PHP with 404
                            return this.onError ? this.onError(item) : null;
                        }

                        item.html.find('.fileuploader-action-remove').addClass('fileuploader-action-success');
                        setTimeout(function () {
                            item.html.find('.progress-bar2').fadeOut(400);
                        }, 400);
                    },
                    onError: function (item) {
                        console.log(item);
                        console.log(item.uploaded);
                        if (!item.uploaded) {
                            
                            alert('error');

                            var progressBar = item.html.find('.progress-bar2');

                            if (progressBar.length) {
                                progressBar.find('span').html(0 + "%");
                                progressBar.find('.fileuploader-progressbar .bar').width(0 + "%");
                                item.html.find('.progress-bar2').fadeOut(400);
                            }

                            item.upload.status != 'cancelled' && item.html.find('.fileuploader-action-retry').length == 0 ? item.html.find('.column-actions').prepend(
                                '<button type="button" class="fileuploader-action fileuploader-action-retry" title="Retry"><i class="fileuploader-icon-retry"></i></button>'
                                ) : null;
                         }

                    },
                    onProgress: function (data, item) {
                        alert('on progress');

                        var progressBar = item.html.find('.progress-bar2');

                        if (progressBar.length > 0) {
                            progressBar.show();
                            progressBar.find('span').html(data.percentage + "%");
                            progressBar.find('.fileuploader-progressbar .bar').width(data.percentage + "%");
                        }
                    },
                    onComplete: null,
                }, onRemove: function (item) {
                    alert('on remove');

                    $.post('approval.aspx?type=remove', { file: item.name });
                }*/
            });


            $('.form-control').selectpicker({
                liveSearch: true,
                maxOptions: 1
            });
            const urlParams = new URLSearchParams(window.location.search);
            const approvalcode = urlParams.get('approvalcode');
            console.log(approvalcode);

            if (approvalcode) {
                var asd = '<%= ownerapproval.ToString %>'

                document.getElementById("demo2").innerHTML = "(" + asd + ")";
            }

            var status = document.getElementById('<%= txtStatus.ClientID%>');
            var grid = document.getElementById('grid'); //สำหรับแสดงผล


            var Approval_BF = '<%= Approval_BF.ToString %>'
            var Approval_AT = '<%= Approval_AT.ToString %>'
            var Approval_Bill = '<%= Approval_Bill.ToString %>'
            var Approval_Doc = '<%= Approval_Doc.ToString %>'
            var status_session = "<%= Session("status")%>"

            if (status_session != "new") {
                if (!Approval_BF) {
                    //ไม่มี
                    $('.file_Approval_BF').hide();
                } else {
                    //มี
                    $('.fileuploader-thumbnails-input').css('display', 'none');
                }


                if (!Approval_Doc && !Approval_AT && !Approval_Bill) {
                    $('.files_approval').hide();
                    grid.setAttribute("class", "col-xl-12 mb-3");
                }
                else {
                    grid.setAttribute("class", "col-xl-8 mb-3");
                    if (!Approval_AT) {
                        //ไม่มี
                        $('.file_Approval_AT').hide();
                    } else {
                        //มี
                        $('.fileuploader-thumbnails-input').css('display', 'none');
                    }
                    if (!Approval_Bill) {
                        //ไม่มี
                        $('.file_Approval_Bill').hide();
                    } else {
                        //มี
                        $('.fileuploader-thumbnails-input').css('display', 'none');
                    }
                    if (!Approval_Doc) {
                        //ไม่มี
                        $('.file_Approval_Doc').hide();
                    } else {
                        //มี
                        $('.fileuploader-thumbnails-input').css('display', 'none');
                    }
                }



                $('.content-holder').css('display', 'none');
                $('.type-holder').css('display', 'none');
                $('.actions-holder').css('display', 'none');

            }

            /*console.log(elem)
            if (approvalcode && elem.getAttribute('src') === "#") {
                console.log(elem.getAttribute('src') === "#")
                console.log(approvalcode)

                $('.image-upload-wrap').hide();
                $('.image-title-wrap').hide();
                $('.file-upload-content').hide();

            }
            if (elem.getAttribute('src') != "#") {
                console.log("inif")

                $('.image-upload-wrap').hide();
                $('.image-title-wrap').hide();
                $('.file-upload-content').show();
            }
            if (status.value == "ปิดงาน") {
                console.log("ปิดงาน")
                console.log(img_after.getAttribute('src'))
                console.log(img_bil.getAttribute('src'))

                if (img_after.getAttribute('src') != "#") {
                    console.log("inif1")

                    $('.file-img-after').show();
                } else {
                    console.log("else inif1")

                    $('.file-img-after').hide();
                }

                if (img_bil.getAttribute('src') != "#") {
                    console.log("inif2")

                    $('.file-img-bill').show();
                } else {
                    console.log("else inif2")

                    $('.file-img-bill').hide();
                }
            }*/
            $('input').on('change', function () {
                resizeImages(this.files[0], function (dataUrl) {

                    // image is now a resized dataURL.  This can be sent up to the server using ajax where it can be recompiled into an image and stored.
                    ////// 5 Upload to server as dataUrl
                    //uploadResizedImages(dataUrl);

                    //console.log(dataUrl.length)
                });
            });

        });

    </script>
    <script type="text/javascript">
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

        function readURL(input) {
            console.log(input);
            if (input.files && input.files[0]) {

                var reader = new FileReader();
                reader.onload = function (e) {
                    var size = input.files[0].size;
                    console.log(size)
                    console.log(input.files[0])

                    $('.image-upload-wrap').hide();

                    $('.file-upload-image').attr('src', e.target.result);
                    $('.file-upload-content').show();

                    console.log("end")
                };

                reader.readAsDataURL(input.files[0]);

            } else {
                removeUpload();
            }
        }
        function resizeImages(file, complete) {
            // read file as dataUrl
            ////////  2. Read the file as a data Url
            var reader = new FileReader();
            // file read
            reader.onload = function (e) {
                // create img to store data url
                ////// 3 - 1 Create image object for canvas to use
                var img = new Image();
                img.onload = function () {
                    /////////// 3-2 send image object to function for manipulation
                    complete(resizeInCanvas(img));
                };
                img.src = e.target.result;
            }
            // read file
            reader.readAsDataURL(file);

        }
        function resizeInCanvas(img) {
            /////////  3-3 manipulate image
            var perferedWidth = 1024;
            var ratio = perferedWidth / img.width;
            var canvas = $("<canvas>")[0];
            canvas.width = img.width * ratio;
            canvas.height = img.height * ratio;
            var ctx = canvas.getContext("2d");
            ctx.drawImage(img, 0, 0, canvas.width, canvas.height);
            //////////4. export as dataUrl
            return canvas.toDataURL('image/jpeg', 0.5);
        }
        function removeUpload() {
            $('.file-upload-input').replaceWith($('.file-upload-input').clone());
            $('.file-upload-content').hide();
            $('.image-upload-wrap').show();
        }
        $('.image-upload-wrap').bind('dragover', function () {
            $('.image-upload-wrap').addClass('image-dropping');
        });
        $('.image-upload-wrap').bind('dragleave', function () {
            $('.image-upload-wrap').removeClass('image-dropping');
        });

        function goBack() {
            window.location.href = '../approval/ApprovalMenuList.aspx';
        }

        function disApproval() {

            /*alert(GridView);*/
            var approvalcode = document.getElementById('<%= txtApprovalcode.ClientID%>').value
            var usercode = "<%= Session("usercode")%>";

            Swal.fire({
                input: 'textarea',
                inputLabel: 'ไม่อนุมัติเนื่องจาก',
                inputPlaceholder: 'ใส่ข้อความ . . .',
                inputAttributes: {
                    'aria-label': 'ใส่ข้อความ.'
                },
                preConfirm: () => {
                    if (!document.getElementById('swal2-input').value) {
                        // Handle return value 
                        Swal.showValidationMessage('First input missing')
                    }
                },
                showCancelButton: true
            }).then((result) => {
                console.log(result.value);
                if (result.isConfirmed) {
                    var params = "{'approvalcode': '" + approvalcode + "','message': '" + result.value + "','updateby': '" + usercode + "'}";
                    console.log(params);
                    $.ajax({
                        type: "POST",
                        url: "../approval/approval.aspx/disApprovalByCode",
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
    </script>
    <script>
        function valueChangedDay() {
            if ($('.chk-day').is(":checked"))
                $(".form-day").show();
            else
                $(".img-bill").hide();

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

        function alertWarning(massage) {
            Swal.fire(
                massage,
                '',
                'warning'
            )
        }
    </script>
    <script>
        function validateComment() {
            var txtcomment = document.getElementById('<%= txtComment.ClientID%>');
            /*console.log(txtcomment.value)
            console.log(txtcomment.checkValidity())*/
            if (txtcomment.checkValidity() === false) {
                event.preventDefault();
                event.stopPropagation();
            }
            txtcomment.classList.add('was-validated');
        }
    </script>
    <script>
        // Set the date we're counting down to

        var countDownDate = new Date('<%= deadline%>')
        console.log(countDownDate)
        // Update the count down every 1 second
        var x = setInterval(function () {

            // Get today's date and time
            var now = new Date().getTime();

            // Find the distance between now and the count down date
            var distance = countDownDate - now;

            // Time calculations for days, hours, minutes and seconds
            var days = Math.floor(distance / (1000 * 60 * 60 * 24));
            var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
            var seconds = Math.floor((distance % (1000 * 60)) / 1000);

            // Output the result in an element with id="demo"
            document.getElementById("demo").innerHTML = days + "d " + hours + "h "
                + minutes + "m " + seconds + "s ";

            // If the count down is over, write some text 
            if (distance < 0) {
                clearInterval(x);
                document.getElementById("demo").innerHTML = "EXPIRED";
            }


        }, 1000);
    </script>
    <script>
        function Confirm() {

            console.log("insave");
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
            return true;
        }
        function changeColorstatusGSM(sendGSM) {
            if (sendGSM.toLowerCase() === 'true') {
                $(".statusGSM").css("background-color", "#00FF27");
                $(".statusGSM").css("box-shadow", "rgba(0, 0, 0, 0.2) 0 -1px 7px 1px, inset #304701 0 -1px 9px, #89FF00 0 0px 5px");
                $("#statusGSMName").text("sent to GSM");

            } else {
                $(".statusGSM").css("background-color", "#ee443b");
                $(".statusGSM").css("box-shadow", "rgba(0, 0, 0, 0.2) 0 -1px 7px 1px, inset #8f0000 0 -1px 9px, rgba(255, 0, 0, 0.5) 0 0px 5px");
                $("#statusGSMName").text("Not find in GSM");
            }
        }
    </script>

</asp:Content>
