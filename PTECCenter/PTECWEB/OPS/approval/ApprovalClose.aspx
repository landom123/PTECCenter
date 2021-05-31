<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="ApprovalClose.aspx.vb" Inherits="PTECCENTER.WebForm4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">

    <style>
        .card {
            margin-bottom: 1rem;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="bg">
        <div id="wrapper">

            <div id="content-wrapper">
                <div class="container">
                    <% If flag Then%>
                    <% If Not Request.QueryString("approvalcode") Is Nothing Then%>

                    <div class="row bg-white">
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
                                    <% If detailtable.Rows(0).Item("statusid") = 4 Or detailtable.Rows(0).Item("statusid") = 2 Or detailtable.Rows(0).Item("statusid") = 10 Then%>
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
                                                <asp:TextBox class="form-control text-danger font-weight-bold" ID="txtCloseDate" runat="server"  placeholder="--- คลิกเพื่อเลือก ---" required></asp:TextBox>
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
                                    <% If detailtable.Rows(0).Item("statusid") = 2 Or detailtable.Rows(0).Item("statusid") = 10 Then%>
                                    <div class="row">
                                        <div class="col-md-auto">
                                            <div class="form-check">
                                                <input class="form-check-input chk-img-after" type="checkbox" value="" id="chkAfter" runat="server" onchange="valueChangedImgAfter()">
                                                <label class="form-check-label" runat="server" associatedcontrolid="chkAfter">
                                                    รูปภาพหลังทำ
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-md-auto">
                                            <div class="form-check">
                                                <input class="form-check-input chk-img-bill" type="checkbox" value="" id="chkBill" runat="server" onchange="valueChangedImgBill()">
                                                <label class="form-check-label" runat="server" associatedcontrolid="chkBill">
                                                    รูปภาพใบเสร็จ
                                                </label>
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
                        <div class="col-md-12 col-12">
                            <div class="img-after">
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
                                                <div class="input-group justify-content-center">
                                                    <div class="image-upload-wrap justify-content-center wrap-img-after">
                                                        <i>+</i>
                                                        <asp:FileUpload ID="fileImgAfter" class="file-upload-input" runat="server" onchange="readURLafter(this);" accept="image/*" text="เลือกไฟล์ --ยังไม่เสร็จ" />
                                                    </div>
                                                </div>

                                                <div class="file-upload-content file-img-after">
                                                    <img class="file-upload-image file-upload-image-after" id="img1" src="#" alt="your image" runat="server" />
                                                    <div class="image-title-wrap">
                                                        <button runat="server" id="Button1" name="btnDelete" onclick="removeUploadImgAfter()" type='button' class='close' aria-label='Close Close-danger'>
                                                            <span aria-hidden='true'>&times;</span>
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card-->
                            </div>
                        </div>
                        <div class="col-md-12 col-12">
                            <div class="img-bill">
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
                                                <div class="input-group justify-content-center">
                                                    <div class="image-upload-wrap justify-content-center wrap-img-bill">
                                                        <i>+</i>
                                                        <asp:FileUpload ID="fileImgBill" class="file-upload-input" runat="server" onchange="readURLbill(this);" accept="image/*" text="เลือกไฟล์ --ยังไม่เสร็จ" />
                                                    </div>
                                                </div>

                                                <div class="file-upload-content file-img-bill">
                                                    <img class="file-upload-image file-upload-image-bill" id="img2" src="#" alt="your image" runat="server" />
                                                    <div class="image-title-wrap">
                                                        <button runat="server" id="Button2" name="btnDelete" onclick="removeUploadImgBill()" type='button' class='close' aria-label='Close Close-danger'>
                                                            <span aria-hidden='true'>&times;</span>
                                                        </button>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- end card-->
                            </div>

                        </div>
                    </div>
                    <!-- end row-->
                    <% If detailtable IsNot Nothing Then%>
                    <% If detailtable.Rows(0).Item("statusid") = 2 Or detailtable.Rows(0).Item("statusid") = 10 Then%>

                    <div class="row bg-white">
                        <div class="col-12 text-center">
                            <asp:Button ID="btnClose" class="btn btn-danger" runat="server" OnClientClick="validateDataImg()" Text="บันทึก" />
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

    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>

    <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>

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
            $('.form-control').selectpicker({
                liveSearch: true,
                maxOptions: 1
            });
            const urlParams = new URLSearchParams(window.location.search);
            const approvalcode = urlParams.get('approvalcode');
            console.log(approvalcode);

            var img1 = document.getElementById('<%= img1.ClientID%>');
            var img2 = document.getElementById('<%= img2.ClientID%>');
            var status = document.getElementById('<%= txtStatus.ClientID%>');
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
            }

        });

    </script>
    <script type="text/javascript">
        function readURLafter(input) {
            if (input.files && input.files[0]) {

                var reader = new FileReader();
                reader.onload = function (e) {
                    $('.wrap-img-after').hide();

                    $('.file-upload-image-after').attr('src', e.target.result);
                    $('.file-img-after').show();

                };

                reader.readAsDataURL(input.files[0]);

            } else {
                removeUpload();
            }
        }
        function readURLbill(input) {
            if (input.files && input.files[0]) {

                var reader = new FileReader();
                reader.onload = function (e) {
                    $('.wrap-img-bill').hide();

                    $('.file-upload-image-bill').attr('src', e.target.result);
                    $('.file-img-bill').show();

                };

                reader.readAsDataURL(input.files[0]);

            } else {
                removeUpload();
            }
        }
        function removeUploadImgAfter() {
            document.getElementById('<%= fileImgAfter.ClientID%>').value = "";
            $(document.getElementById('<%= fileImgAfter.ClientID%>')).replaceWith($(document.getElementById('<%= fileImgAfter.ClientID%>')).clone());
            $('.file-img-after').hide();
            $('.wrap-img-after').show();
        }
        function removeUploadImgBill() {
            document.getElementById('<%= fileImgBill.ClientID%>').value = "";
            $(document.getElementById('<%= fileImgBill.ClientID%>')).replaceWith($(document.getElementById('<%= fileImgBill.ClientID%>')).clone());
            $('.file-img-bill').hide();
            $('.wrap-img-bill').show();
        }

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

            var fileImgAfter = document.getElementById('<%= fileImgAfter.ClientID%>').value
            var fileImgBill = document.getElementById('<%= fileImgBill.ClientID%>').value
            var chkAfter = document.getElementById('<%= chkAfter.ClientID%>')
            var chkBill = document.getElementById('<%= chkBill.ClientID%>')
            if (!($('#<%= chkAfter.ClientID%>').prop("checked")) && !($('#<%= chkBill.ClientID%>').prop("checked"))) {
                alertWarning('ต้องมีรูปอย่างใดอย่างนึง');
                event.preventDefault();
                event.stopPropagation();
            }
            

            if (($('#<%= chkAfter.ClientID%>').prop("checked")) && !fileImgAfter) {
                alertWarning('ใส่รูปหลังปฏิบัติงาน');
                event.preventDefault();
                event.stopPropagation();
            }
            if (($('#<%= chkBill.ClientID%>').prop("checked")) && !fileImgBill) {
                alertWarning('ใส่รูปใบเสร็จ');
                event.preventDefault();
                event.stopPropagation();
            }


        }
    </script>
    <script type="text/javascript">
        function alertSuccessUpload() {
            Swal.fire({
                title: 'อัปโหลดสำเร็จ',
                icon: 'success',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'ยืนยัน'
            }).then((result) => {
                if (result.isConfirmed) {
                    console.log('1')
                    const urlParams = new URLSearchParams(window.location.search);
                    const approvalcode = urlParams.get('approvalcode');
                    console.log(approvalcode);
                    window.location.href = '../approval/approval.aspx?approvalcode=' + approvalcode;
                }
            })
        }
    </script>

</asp:Content>
