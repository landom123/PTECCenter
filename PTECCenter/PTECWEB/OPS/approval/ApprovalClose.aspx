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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="bg">
        <div id="wrapper">

            <div id="content-wrapper">
                <div class="container">
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
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Label ID="lbCodeGSM" CssClass="form-label" AssociatedControlID="txtCodeGSM" runat="server" Text="รหัส GSM" />
                                                <asp:Label ID="lbCodeGSMMandatory" CssClass="text-danger" AssociatedControlID="txtCodeGSM" runat="server" Text="*" />
                                                <asp:TextBox class="form-control" ID="txtCodeGSM" runat="server" required></asp:TextBox>
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
                        <div class="col-md-12 col-12 mb-3 img-after" style="display:none;">
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
                    <!-- end row-->
                    <% If detailtable IsNot Nothing Then%>
                    <% If detailtable.Rows(0).Item("statusid") = 2 Or detailtable.Rows(0).Item("statusid") = 9 Then%>

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
            
            const secid = "<%= Session("secid")%>"
            if (secid != "2") {
                //alert("in secid != 2")
                if (!($('#<%= chkAfter.ClientID%>').prop("checked")) && !($('#<%= chkBill.ClientID%>').prop("checked"))) {
                    alertWarning('ต้องมีรูปอย่างใดอย่างนึง');
                    event.preventDefault();
                    event.stopPropagation();
                }
            } else {
                const codeGSM = document.getElementById('<%= txtCodeGSM.ClientID%>').value;
                if (!(codeGSM)) {
                    alertWarning('กรุณาใส่รหัส GSM');
                    event.preventDefault();
                    event.stopPropagation();
                }
            }

            if (($('#<%= chkAfter.ClientID%>').prop("checked")) && $('.file_af input[type=file]').length == 1) {
                alertWarning('ใส่รูปหลังปฏิบัติงาน');
                event.preventDefault();
                event.stopPropagation();
            }
            if (($('#<%= chkBill.ClientID%>').prop("checked")) && $('.file_bill input[type=file]').length == 1) {
                alertWarning('ใส่รูปใบเสร็จ');
                event.preventDefault();
                event.stopPropagation();
            }
            //console.log('--1-');

            reNameAttr($('.file_af input[type=file]'), 'approval_af')
            reNameAttr($('.file_bill input[type=file]'), 'approval_bill')
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
                    console.log('1')
                    const urlParams = new URLSearchParams(window.location.search);
                    const approvalcode = urlParams.get('approvalcode');
                    console.log(approvalcode);
                    window.location.href = '../approval/approval.aspx?approvalcode=' + approvalcode;
                }
            })
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

</asp:Content>
