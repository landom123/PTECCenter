<%@ Page Title="MemoRequest" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="MemoRequest.aspx.vb" Inherits="PTECCENTER.MemoRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <link href="<%=Page.ResolveUrl("~/fileupload/dist/font/font-fileuploader.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/css/multi-select-tag.css")%>" rel="stylesheet">
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

        html, #content-wrapper {
            background-color: #f0f2f5;
        }

        #content-wrapper {
            min-height: 600px;
        }

        p {
            text-indent: 30px;
        }
    </style>
    <script src="https://cdn.ckeditor.com/ckeditor5/40.1.0/classic/ckeditor.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg">
        <div id="wrapper">
            <!-- #include virtual ="/include/menu.inc" -->
            <!-- add side menu -->
            <div id="content-wrapper">
                <div class="container">
                    <div class="row">
                        <div class="col mb-3">
                        </div>

                        <div class="col-auto  mb-3 text-right align-self-center">
                            <a href="MemoList.aspx" class="btn btn-sm btn-danger ">
                                <i class="fa fa-tasks" aria-hidden="true"></i></a>
                        </div>

                    </div>
                    <div class="row bg-danger">
                        <div class="col bg-secondary">
                            <div>
                                <asp:Label ID="txtTest" runat="server" Text="" />
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="lbcboTo" CssClass="form-label" AssociatedControlID="cboTo" runat="server" Text="To." />
                                        <asp:Label ID="lbcboToMandatory" CssClass="text-danger" AssociatedControlID="cboTo" runat="server" Text="*" />
                                        <asp:ListBox class="chip" runat="server" ID="cboTo" SelectionMode="multiple"></asp:ListBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="lbcboCC" CssClass="form-label" AssociatedControlID="cboCC" runat="server" Text="CC" />
                                        <asp:ListBox CssClass="chip" ID="cboCC" runat="server" SelectionMode="multiple"></asp:ListBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="lbtxtSubject" CssClass="form-label" AssociatedControlID="txtSubject" runat="server" Text="Subject / Objective" />
                                        <asp:Label ID="lbtxtSubjectMandatory" CssClass="text-danger" AssociatedControlID="txtSubject" runat="server" Text="*" />
                                        <asp:TextBox class="form-control" ID="txtSubject" runat="server" Rows="2" Columns="50" TextMode="MultiLine" ValidateRequestMode="Enabled" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lbcboMemoType" CssClass="form-label" AssociatedControlID="cboMemoType" runat="server" Text="Memo Group" />
                                        <asp:Label ID="lbcboMemoTypeMandatory" CssClass="text-danger" AssociatedControlID="cboMemoType" runat="server" Text="*" />
                                        <asp:DropDownList class="form-control" ID="cboMemoType" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lbtxtAmount" CssClass="form-label" AssociatedControlID="txtAmount" runat="server" Text="Amount" />
                                        <asp:TextBox class="form-control" type="number" ID="txtAmount" runat="server" ValidateRequestMode="Enabled" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row memoOther" style="display: none;">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="lbtxtMemoOther" CssClass="form-label" AssociatedControlID="txtMemoOther" runat="server" Text="(อื่นๆ)" />
                                        <asp:Label ID="lbtxtMemoOtherMandatory" CssClass="text-danger" AssociatedControlID="txtMemoOther" runat="server" Text="*" />
                                        <asp:TextBox class="form-control " ID="txtMemoOther" runat="server" Rows="2" Columns="50" TextMode="MultiLine" ValidateRequestMode="Enabled"></asp:TextBox>

                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="accordion" id="accordionExample">
                                        <div class="form-group">
                                            <div class="custom-control custom-radio custom-control-inline">
                                                <input runat="server" type="radio" id="chkMemoFile" name="customRadioInline" class="custom-control-input" data-toggle="collapse" data-target="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                                                <asp:Label ID="lbchk1" CssClass="custom-control-label" AssociatedControlID="chkMemoFile" runat="server" Text="Attach Memo" />
                                            </div>
                                            <div class="custom-control custom-radio custom-control-inline">
                                                <input runat="server" type="radio" id="chkMemoDetail" name="customRadioInline" class="custom-control-input" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                                <asp:Label ID="lbchk2" CssClass="custom-control-label" AssociatedControlID="chkMemoDetail" runat="server" Text="Write Memo (1,000 digit)" />

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div id="collapseOne" class="collapse" aria-labelledby="headingOnd" data-parent="#accordionExample">
                                                <asp:FileUpload ID="FileUpload1" class="file-upload-input" runat="server" onchange="readURL(event);" accept="image/*,.pdf" text="เลือกไฟล์" />
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <div id="collapseTwo" class="collapse" aria-labelledby="headingTwo" data-parent="#accordionExample">
                                                <div class="form-group">
                                                    <%--<asp:Label ID="lbtxtMemoDetail" CssClass="form-label" AssociatedControlID="txtMemoDetail" runat="server" Text="Subject" />--%>
                                                    <asp:TextBox class="form-control txtarea" ID="txtMemoDetail" runat="server" Rows="3" Columns="50" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                            <button type="submit" class="btn btn-primary" id="btnSave" runat="server">Save</button>
                        </div>
                    </div>



                    <%--<div class="row bg-danger">
                    </div>
                    <div class="row justify-content-md-center">
                        <div class="col-md-10">
                            <input type="file" name="files" accept="image/*,.pdf" data-fileuploader-files=''>
                        </div>
                    </div>--%>
                </div>
                <div class="file-upload-content text-center">
                    <iframe class="file-upload-image" src="#" title="description" width="95%" height="1000px"></iframe>
                </div>
            </div>
            <!-- end content-->
        </div>
        <!-- end wrapper-->
    </div>
    <!-- end bg-->



    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/multi-select-tag.js")%>"></script>

    <script type="text/javascript">
        new MultiSelectTag('<%= cboTo.ClientID.ToString %>', {
                toggleHide: true,
                tagColor: {
                    textColor: '#327b2c',
                    borderColor: '#92e681',
                    bgColor: '#eaffe6',
                },
                onChange: function (values) {
                    console.log(values)
                }

            })  // id
        new MultiSelectTag('<%= cboCC.ClientID.ToString %>')  // id
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

            console.log('ma');
            checkCboMemo();
            checkFileOrAddMemo();

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
        $('#<%= cboMemoType.ClientID%>').on('change', function () {
            checkCboMemo();
        });
        function checkCboMemo() {
            ($('#<%= cboMemoType.ClientID%>').val() == 999) ? $(".memoOther").show() : $(".memoOther").hide();
        }
        function checkFileOrAddMemo() {
            if ($("#" + "<%= chkMemoFile.ClientID.ToString %>").is(":checked")) {
                $("#collapseOne").collapse('show');
                $("#collapseTwo").collapse('hide');
            } else if ($("#" + "<%= chkMemoDetail.ClientID.ToString %>").is(":checked")) {
                $("#collapseOne").collapse('hide');
                $("#collapseTwo").collapse('show');
            } else {
                $("#collapseOne").collapse('hide');
                $("#collapseTwo").collapse('hide');
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
        function test(elem) {
            console.log(elem);
        }
        ClassicEditor
            .create(document.querySelector('.txtarea'), {
                toolbar: ['heading', '|', 'bold', 'italic', 'link', 'bulletedList', 'numberedList', 'blockQuote'],
            })
            .catch(error => {
                console.log(error);
            });

        function readURL(event) {
            const files = event.target.files;
            if (files && files.length > 0) {
                const targetFile = files[0];
                console.log(targetFile);
                try {
                    const objectURL = window.URL.createObjectURL(targetFile);
                    console.log(objectURL);
                    $('.file-upload-image').attr('src', objectURL);
                    $('.file-upload-content').show();
                    window.URL.revokeObjectURL(objectURL);

                    console.log('end try');

                }
                catch (e) {
                    console.log('catch');

                    try {
                        // Fallback if createObjectURL is not supported
                        const fileReader = new FileReader();
                        fileReader.onload = (evt) => {
                            $('.file-upload-image').attr('src', evt.target.result);
                            $('.file-upload-content').show();
                        };
                        fileReader.readAsDataURL(targetFile);
                    }
                    catch (e) {
                        console.log(`File Upload not supported: ${e}`);
                    }
                }
            }
        }
    </script>
</asp:Content>
