<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="Memo2.aspx.vb" Inherits="PTECCENTER.Memo2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg">
        <div id="wrapper">
            <!-- #include virtual ="/include/menu.inc" -->
            <!-- add side menu -->
            <div id="content-wrapper">
                <div class="container">
                    <div class="row">
                        <div class="col-6 border border-secondary">
                            <div class="font-weight-bold">
                                <span>Thanapol Duruangram</span>
                            </div>
                        </div>
                        <div class="col-6 text-right border border-danger">
                            <button type="button" class="btn btn-outline-info">MEMO230100001</button>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-6 mb-3 ">
                            <div class="row">
                                <div class="col text-break">
                                    <span>To : </span>
                                    <span>TPS;</span>
                                    <span>TPD;</span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col text-break">
                                    <span>Cc : </span>
                                    <span>TPS;</span>
                                    <span>TPD;</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-6 mb-3 text-right border border-danger">
                            <small id="txtCreateDate" class="text-muted">Must be 8-20 characters long.
                            </small>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col text-break">
                            <span>Subject : mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm</span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col text-break">
                            <span>MemoType : ______________________________________________________________________</span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col text-break mb-3">
                            <span>Cost : ______________________________________________________________________</span>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-3">
                            <span>ผู้ตรวจ</span>
                        </div>
                        <div class="col-3">
                            <span>ผู้อนุมัติ</span>
                        </div>
                        <div class="col-6">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-3">
                            <div class="row">
                                <div class="col-auto mb-3">
                                    <span>วันที่</span>
                                </div>
                                <div class="col mb-3 text-center">
                                    <small id="txtVeriftyDate" class="text-muted">07/12/2023</small>
                                </div>
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="row">
                                <div class="col-auto mb-3">
                                    <span>วันที่</span>
                                </div>
                                <div class="col mb-3 text-center">
                                    <small id="txtApprovalDate" class="text-muted">07/12/2023</small>
                                </div>
                            </div>
                        </div>
                        <div class="col-6 mb-3">
                        </div>
                    </div>
                </div>
                <div class="text-center">
                    <iframe class="file-upload-image" src="http://vpnptec.dyndns.org:10280/OPS_Fileupload/ATT_231100762.pdf" title="description" width="95%" height="1000px"></iframe>
                </div>
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

            var groups = {};
            $("select option[data-category]").each(function () {
                groups[$.trim($(this).attr("data-category"))] = true;
            });
            $.each(groups, function (c) {
                $("select option[data-category='" + c + "']").wrapAll('<optgroup label="' + c + '">');
            });

            $('.form-control').selectpicker({
                noneSelectedText: '-',
                liveSearch: true
            });

            $('.form-control').selectpicker('refresh');


            $('input[type="file"]').fileuploader({
                example: ['pdf', 'image/*'],
                fileMaxSize: 15,
                limit: 1,
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
    </script>
</asp:Content>
