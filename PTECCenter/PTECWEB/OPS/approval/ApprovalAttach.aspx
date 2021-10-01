<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="ApprovalAttach.aspx.vb" Inherits="PTECCENTER.approvalattach" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- fonts -->
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,700" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/fileupload/dist/font/font-fileuploader.css")%>" rel="stylesheet">

    <link href="<%=Page.ResolveUrl("~/fileupload/dist/jquery.fileuploader.min.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/fileupload/dist/jquery.fileuploader-theme-thumbnails.css")%>" rel="stylesheet">
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

    <div id="wrapper">


        <!-- เนื้อหาเว็บ -->
        <div id="content-wrapper">

            <div class="container">

                <% If flag Then%>
                <div class="row bg-white">
                    <div class="col-lg-12">

                        <div class="card shadow">
                            <div class="card-header" style="background-color: #475b6f; color: white">
                                <div class="row justify-content-between">
                                    <div class="col text-left align-self-center">
                                        แนบเอกสารให้ฝ่ายประสานงาน
                                        (ใบแจ้งหนี้ / ใบเสร็จ / อื่นๆ)
                                    </div>
                                    <div class="col text-right align-self-center">
                                        <a href="approval.aspx" class="btn btn-sm btn-danger ">
                                            <i class="fas fa-home" aria-hidden="true"></i></a>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body">
                                <% If Not Request.QueryString("approvalcode") Is Nothing Then%>
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
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lbDocDate" CssClass="form-label" AssociatedControlID="txtDocDate" runat="server" Text="วันที่แจ้ง" />
                                            <asp:TextBox class="form-control font-weight-bold" ID="txtDocDate" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lbOwnerApprovalDate" CssClass="form-label" AssociatedControlID="txtOwnerApprovalDate" runat="server" Text="วันที่อนุมัติ" />
                                            <asp:TextBox class="form-control text-danger font-weight-bold" ID="txtOwnerApprovalDate" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <% End If %>
                                <!-- end has approvalcode -->
                                <div class="row justify-content-md-center">
                                    <div class="col-md-10">
                                        <div class="input-group justify-content-center">
                                            <input type="file" name="files" accept="image/*,.pdf" >
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Label ID="lbCodeGSM" CssClass="form-label" AssociatedControlID="txtCost" runat="server" Text="ค่าใช้จ่ายที่ใช้จริง" />
                                            <asp:Label ID="lbCodeGSMMandatory" CssClass="text-danger" AssociatedControlID="txtCost" runat="server" Text="* (กรณีไม่มีค่าใช้จ่ายใส่ 0)" />
                                            <asp:TextBox class="form-control" ID="txtCost" runat="server" type="number" min="0"  required></asp:TextBox>
                                            <div class="invalid-feedback">กรุณาใส่ค่าใช้จ่ายตามบิล</div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-footer text-center bg-white">
                                    <asp:Button ID="btnUpload" class="btn btn-primary" runat="server" OnClientClick="validateDataImg()" Text="Upload" AutoPostBack="true" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

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
                            <a href="approval.aspx" class="btn btn-sm btn-danger "><-- กลับ</a>
                        </div>
                    </div>
                </div>
                <% End If %>
                <!-- end flag -->

            </div>
            <!-- /.container-fluid -->

        </div>
        <!-- /.content-wrapper -->
        <!-- end เนื้อหาเว็บ -->


    </div>
    <!-- /#wrapper -->

    <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            //alert('t');
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
        });

    </script>
    <script type="text/javascript">
        function validateDataImg() {
            validateData();

            const urlParams = new URLSearchParams(window.location.search);
            const approvalcode = urlParams.get('approvalcode');
            console.log(approvalcode.length);

            if ($('input[type=file]').length == 1 && approvalcode.length == 12) {
                alertWarning('กรุณาแนบเอกสาร');
                event.preventDefault();
                event.stopPropagation();
            }
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
    </script>
    <script type="text/javascript">
        function alertSuccessUpload(code) {
            Swal.fire({
                title: 'อัปโหลดสำเร็จ',
                icon: 'success',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'ยืนยัน',
                allowOutsideClick: false
            }).then((result) => {
                if (result.isConfirmed) {
                    window.location.href = '../approval/approval.aspx?approvalcode=' + code;
                }
            })
        }
    </script>

</asp:Content>
