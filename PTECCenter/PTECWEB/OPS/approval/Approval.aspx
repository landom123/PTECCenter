<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="Approval.aspx.vb" Inherits="PTECCENTER.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .image-upload-wrap {
            border-radius: 0.25rem;
            background: #f0cccc;
            border: 2px dashed #ff0000;
            text-align: center;
            font-size: 30px;
            color: #ff0000;
            cursor: pointer;
            opacity: 0.5;
            height: 130px;
            width: 130px;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
        }

            .image-upload-wrap i {
                position: absolute;
                font-style: normal;
                top: 50%;
                left: 50%;
                -webkit-transform: translateX(-50%) translateY(-50%);
                transform: translateX(-50%) translateY(-50%);
            }

        .file-upload-input {
            cursor: pointer;
            opacity: 0;
            height: 100%;
            width: 100%;
        }

        .btn-light {
            background-color: #fff;
        }

            .btn-light.disabled {
                border: 1px solid #ced4da;
                background-color: #e9ecef;
            }

        .file-upload-content {
            display: none;
            text-align: center;
        }

        .file-upload-image {
            max-width: 100%;
            max-height: 100%;
            margin: auto;
            padding: 20px;
        }

        .remove-image {
            border: 0;
            background: #fe7676;
            border-radius: 50%;
            box-shadow: -1px 1px 6px rgb(254 118 118 / 80%);
            color: #fdfdfd;
            text-shadow: 1px 1px 3px rgb(0 0 0 / 30%);
        }

        .image-title-wrap {
            position: absolute;
            top: 6px;
            right: 6px;
            z-index: 2;
            height: 20px;
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
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg">
        <div id="wrapper">

            <div id="content-wrapper">
                <div class="container">

                    <% If flag Then%>

                    <div class="row bg-white">
                        <div class="col-lg-12">
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
                                        <div class="col text-right align-self-center">
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
                                    <!-- working = true มีสถานะ 2 อนุมัติ,8 รอประสานงานยืนยัน,9 กำลังจัดทำเอกสาร,10 จัดส่งเอกสาร -->
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
                                        <!-- status = ส่งเอกสารให้ผู้เกี่ยวข้อง = 11 -->
                                        <!-- status = จัดส่งเอกสาร = 10 -->
                                        <!-- status = กำลังจัดทำเอกสาร = 9 -->
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
                                                <asp:Label ID="lbPrice" CssClass="form-label" AssociatedControlID="txtPrice" runat="server" Text="จำนวนเงิน" />
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
                                    <div class="row justify-content-md-center">
                                        <div class="col-md-10">
                                            <div class="input-group justify-content-center">
                                                <div class="image-upload-wrap justify-content-center">
                                                    <i>+</i>
                                                    <asp:FileUpload ID="FileUpload1" class="file-upload-input" runat="server" onchange="readURL(this);" accept="image/*" text="เลือกไฟล์ --ยังไม่เสร็จ" />
                                                </div>
                                            </div>

                                            <div class="file-upload-content">
                                                <img class="file-upload-image" id="img1" src="#" alt="your image" runat="server" />
                                                <div class="image-title-wrap">
                                                    <button runat="server" id="btnDelete" name="btnDelete" onclick="removeUpload()" type='button' class='close' aria-label='Close Close-danger'>
                                                        <span aria-hidden='true'>&times;</span>
                                                    </button>
                                                </div>
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

                                    <!-- รูปหลังทำ  -->
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="file-img-after">
                                                <img class="file-upload-image file-upload-image-after" id="img2" src="#" alt="your image" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <!-- รูปบิล  -->
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="file-img-bill">
                                                <img class="file-upload-image file-upload-image-bill" id="img3" src="#" alt="your image" runat="server" />
                                            </div>
                                        </div>
                                    </div>

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
                                    <% End If %>
                                    <% End If %>
                                    <% If Session("status") = "new" Then%>
                                    <div class="card-footer text-center bg-white">
                                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" OnClientClick="validateData()" />
                                    </div>
                                    <% ElseIf Session("status") = "read" And (Session("userid").ToString() = detailtable.Rows(0).Item("createby")) Then%>
                                    <div class="card-footer text-center bg-white">
                                        <asp:Button ID="btnConfirm" class="btn btn-warning" runat="server" Text="Confirm" OnClientClick="Confirm();" />
                                        <asp:Button ID="btnCancel" class="btn btn-danger" runat="server" Text="Cancel" />
                                        <asp:Button ID="btnClose" class="btn btn-danger" runat="server" Text="ปิดงาน" />
                                        <asp:Button ID="btnEdit" class="btn btn-secondary" runat="server" Text="Edit" />

                                    </div>
                                    <% ElseIf Session("status") = "write" And detailtable.Rows(0).Item("statusid").ToString() = 1 Then%>
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
                                    <div class="card-footer text-center bg-white">
                                        <asp:Button ID="btnSupportAllow" class="btn btn-warning" runat="server" Text="ดำเนินการจัดทำเอกสาร" />
                                    </div>
                                    <% End If %>
                                    <% If (detailtable.Rows(0).Item("statusid") = 9 Or detailtable.Rows(0).Item("statusid") = 11) And (Session("userid").ToString = detailtable.Rows(0).Item("supportid").ToString) Then%>
                                    <div class="card-footer text-center bg-white">
                                        <asp:Button ID="btnSupportFinished" class="btn btn-danger" runat="server" Text="เอกสารครบถ้วน" />
                                        <% If detailtable.Rows(0).Item("statusid") = 9 Then%>
                                        <asp:Button ID="btnSupportForaward" class="btn btn-warning" runat="server" Text="ส่งเอกสารให้ผู้เกี่ยวข้อง" />
                                        <% End If %>
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
                    <% If Not Request.QueryString("approvalcode") Is Nothing And detailtable IsNot Nothing And CommentTable IsNot Nothing Then%>
                    <% If (Session("status") = "read" Or Session("status") = "write") And Not detailtable.Rows(0).Item("statusid").ToString() = 7 Then%>
                    <!-- statusid = 7 รอผู้แจ้งยืนยัน-->
                    <div class="row bg-white" style="padding-top: 0.1rem;">
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

                                        <%if detailtable.Rows(0).Item("statusid").ToString() = 1 Then%>
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
                console.log(asd)
            }

            var status = document.getElementById('<%= txtStatus.ClientID%>');
            var elem = document.getElementById('<%= img1.ClientID%>');
            var img_after = document.getElementById('<%= img2.ClientID%>');
            var img_bil = document.getElementById('<%= img3.ClientID%>');
            console.log(elem)
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
            }
            $('input').on('change', function () {
                resizeImages(this.files[0], function (dataUrl) {

                    // image is now a resized dataURL.  This can be sent up to the server using ajax where it can be recompiled into an image and stored.
                    ////// 5 Upload to server as dataUrl
                    //uploadResizedImages(dataUrl);

                    console.log(dataUrl.length)
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
            document.getElementById('<%= FileUpload1.ClientID%>').value = "";
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
            console.log(txtcomment.value)
            console.log(txtcomment.checkValidity())
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
    </script>

</asp:Content>
