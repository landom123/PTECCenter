<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="ApprovalAttach.aspx.vb" Inherits="PTECCENTER.approvalattach" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="<%=Page.ResolveUrl("~/css/autocomplete.css")%>" rel="stylesheet">

    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
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

    <div id="wrapper" class="h-100">

        <!-- #include virtual ="/include/menu.inc" -->
        <!-- add side menu -->

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
                                            <input type="file" name="files" accept="image/*,.pdf" data-fileuploader-files='<%=Approval_Doc%>'>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Label ID="lbCodeGSM" CssClass="form-label" AssociatedControlID="txtCost" runat="server" Text="ค่าใช้จ่ายที่ใช้จริง" />
                                            <asp:Label ID="lbCodeGSMMandatory" CssClass="text-danger" AssociatedControlID="txtCost" runat="server" Text="* (กรณีไม่มีค่าใช้จ่ายใส่ 0)" />
                                            <asp:TextBox class="form-control" ID="txtCost" runat="server" type="number" step="0.01" min="0" required></asp:TextBox>
                                            <div class="invalid-feedback">กรุณาใส่ค่าใช้จ่ายตามบิล</div>
                                        </div>
                                    </div>
                                </div>
                                <%--###################################################--%>
                                <div class="row">
                                    <div class="form-group ">
                                        <div class="col">
                                            <asp:Label ID="Label4" CssClass="form-label" AssociatedControlID="txtVat" runat="server" Text="VAT (%)" />
                                        </div>
                                        <div class="col">
                                            <asp:TextBox class="form-control noEnterSubmit" type="number" ID="txtVat" runat="server" min="0" Text="0" onchange="calculate();"></asp:TextBox>
                                        </div>
                                        <div class="invalid-feedback">* ตัวเลขจำนวนเต็ม</div>
                                    </div>
                                    <div class="form-group ">
                                        <div class="col">
                                            <asp:Label ID="Label5" CssClass="form-label" AssociatedControlID="txtTax" runat="server" Text="WHT (%)" />
                                        </div>
                                        <div class="col">
                                            <asp:TextBox class="form-control noEnterSubmit" type="number" ID="txtTax" runat="server" min="0" Text="0" onchange="calculate();"></asp:TextBox>
                                            <div class="invalid-feedback">* ตัวเลขจำนวนเต็ม</div>
                                        </div>
                                    </div>
                                    <div class="form-group ">
                                        <div class="col">
                                            <asp:Label ID="Label1" CssClass="form-label" AssociatedControlID="txtDistance" runat="server" Text="ระยะทาง (km)" />
                                        </div>
                                        <div class="col">
                                            <asp:TextBox class="form-control noEnterSubmit" type="number" ID="txtDistance" runat="server" min="0" Text="0" step="0.1"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div class="form-group autocomplete">
                                    <asp:Label ID="lbcboVendor" CssClass="form-label" AssociatedControlID="cboVendor" runat="server" Text="Vendor" />
                                    <asp:DropDownList class="form-control  d-none" ID="cboVendor" runat="server" onchange="setVendor(this);"></asp:DropDownList>
                                    <asp:TextBox class="form-control" ID="txtVendor" runat="server" TextMode="MultiLine" Rows="1"></asp:TextBox>
                                </div>
                                <hr />
                                <h3>ใบแจ้งหนี้ / ใบส่งของ / ใบกำกับ</h3>
                                <div class="form-group">
                                    <asp:Label ID="lbtaxid" CssClass="form-label" AssociatedControlID="txttaxid" runat="server" Text="Tax ID no." />
                                    <asp:TextBox class="form-control noEnterSubmit" type="input" ID="txttaxid" runat="server" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lbinvoiceno" CssClass="form-label" AssociatedControlID="txtinvoiceno" runat="server" Text="Invoice no." />
                                    <asp:TextBox class="form-control noEnterSubmit" type="input" ID="txtinvoiceno" runat="server" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lbinvoicedate" CssClass="form-label" AssociatedControlID="txtinvoicedate" runat="server" Text="Invoice date" />
                                    <asp:TextBox class="form-control noEnterSubmit" type="input" ID="txtinvoicedate" runat="server" placeholder="--- คลิกเพื่อเลือก ---" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="card-footer text-center bg-white">
                                    <asp:Button ID="btnUpload" class="btn btn-primary" runat="server" OnClientClick="validateDataImg()" Text="Upload" AutoPostBack="true" UseSubmitBehavior="false" />
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
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <CompositeScript>
            <Scripts>
                <asp:ScriptReference Path="~/datetimepicker/jquery.js" />
                <asp:ScriptReference Path="~/datetimepicker/build/jquery.datetimepicker.full.min.js" />
            </Scripts>
        </CompositeScript>
    </asp:ScriptManager>
    <!-- datetimepicker ต้องไปทั้งชุด-->
    <%--<script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>--%>

    <script type="text/javascript">
        jQuery('[id$=txtinvoicedate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08'
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
        $(document).ready(function () {



            $('.form-control').selectpicker({
                noneSelectedText: '-',
                liveSearch: true,
                maxOptions: 1
            });
            //alert('t');
            $('input[name="files"]').fileuploader({
                example: ['pdf', 'image/*'],
                fileMaxSize: 30,
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
        function setVendor(Acc) {

            const myArr = Acc.options[Acc.selectedIndex].textContent.split(" - ");
            console.log(myArr);
            console.log(myArr[0].substring(2, 12));

            let vendorcode = myArr[0].substring(2, 12)
            console.log(myArr[myArr.length - 1]);
            console.log(vendorcode);

            $("#<%= txtVendor.ClientID%>").val(myArr[myArr.length - 1]);

        }
        function validateDataImg() {
            validateData();

            const urlParams = new URLSearchParams(window.location.search);
            const approvalcode = urlParams.get('approvalcode');
            console.log($('input[type=file]').length);
            console.log(approvalcode.length);


            if ($('input[type=file]').length == 1 && approvalcode.length == 12) {
                alertWarning('กรุณาแนบเอกสาร');
                event.preventDefault();
                event.stopPropagation();
            } else {
                console.log("pre");
                <% If Not Request.QueryString("approvalcode") Is Nothing Then%>
                console.log("in1");
                const price = <%=detailtable.Rows(0).Item("price") %> ;
                const cost = document.getElementById("<%= txtCost.ClientID%>").value;
                if (price < parseFloat(cost)) {
                    console.log("in2");
                    const payload = `ยอดที่ใส่มา (${parseFloat(cost).toLocaleString()}บ.) มากกว่า ยอดที่ตั้งเดิม (${price.toLocaleString()}บ.)`
                    <% If detailtable.Rows(0).Item("approval_form").ToString = "AHO" Then%>
                    alertValidateCostOther(payload, price, parseFloat(cost));
                    <% Else %>
                    alertValidateCostROD(payload, price, parseFloat(cost));
                    <% End If %>
                    event.preventDefault();
                    event.stopPropagation();
                } else {
                    if (confirm(`คุณต้องการดำเนินการต่อที่ยอด (${parseFloat(cost).toLocaleString()}) หรือไม่ ?`)) {
                        __doPostBack('btnUpload_Click_newCost', `${parseFloat(cost)}`);
                    } else {
                        event.preventDefault();
                        event.stopPropagation();
                    }
                }
                <% End If %>
                console.log("end");
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
        function alertValidateCostROD(massage, oldcost, newcost) {
            Swal.fire({
                title: 'กรุณาเลือกยอดที่ต้องการดำเนินการต่อ',
                text: massage,
                icon: 'info',
                width: '50%',
                showCancelButton: true,
                showDenyButton: true,
                cancelButtonColor: '#d33',
                cancelButtonText: 'ยกเลิก',
                confirmButtonColor: '#3085d6',
                confirmButtonText: `ดำเนินการต่อด้วย (${newcost.toLocaleString()})`,
                denyButtonText: `ดำเนินการต่อด้วย (${oldcost.toLocaleString()})`,
                denyButtonColor: `#3085d6`,
                allowOutsideClick: false
            }).then((result) => {
                if (result.isConfirmed) {
                    if (confirm(`คุณต้องการดำเนินการต่อที่ยอด (${newcost.toLocaleString()}) หรือไม่ ?`)) {
                        __doPostBack('btnUpload_Click_newCost_ROD', `${newcost}`);
                    }
                    else {
                        event.preventDefault();
                        event.stopPropagation();
                    }
                } else if (result.dismiss === Swal.DismissReason.cancel) {
                    event.preventDefault();
                    event.stopPropagation();
                } else if (result.isDenied) {

                    if (confirm(`คุณต้องการดำเนินการต่อที่ยอด (${oldcost.toLocaleString()}) หรือไม่ ?`)) {
                        __doPostBack('btnUpload_Click_oldCost_ROD', `${oldcost}`);
                    }
                    else {
                        event.preventDefault();
                        event.stopPropagation();
                    }
                }
            })
        }
        function alertValidateCostOther(massage, oldcost, newcost) {
            Swal.fire({
                title: 'ไม่สามารถแก้ไขยอดที่ได้รับมอบหมายจาก HO',
                text: massage,
                icon: 'warning',
                width: '50%',
                showCancelButton: true,
                showDenyButton: true,
                cancelButtonColor: '#d33',
                cancelButtonText: 'ยกเลิก',
                confirmButtonColor: '#3085d6',
                confirmButtonText: `ดำเนินการต่อ (${oldcost.toLocaleString()})`,
                denyButtonText: `ยกเลิกรายการนี้ และ แจ้งไปยังต้นเรื่อง`,
                allowOutsideClick: false
            }).then((result) => {
                if (result.isConfirmed) {
                    if (confirm(`คุณต้องการดำเนินการต่อที่ยอดเดิม (${oldcost.toLocaleString()}) หรือไม่ ?`)) {
                        __doPostBack('btnUpload_Click_oldCost_Other', `${oldcost}`);
                    }
                    else {
                        event.preventDefault();
                        event.stopPropagation();
                    }
                } else if (result.dismiss === Swal.DismissReason.cancel) {
                    event.preventDefault();
                    event.stopPropagation();
                } else if (result.isDenied) {
                    if (confirm("คุณต้องการจะยกเลิกและแจ้งต้นเรื่องรับทราบ หรือไม่ ?")) {
                        __doPostBack('btnUpload_Click_isDenied_Other', 'isDenied');
                    }
                    else {
                        event.preventDefault();
                        event.stopPropagation();
                    }
                }
            })
        }
        function alertSuccessUpload(code, urldes) {
            Swal.fire({
                title: 'อัปโหลดสำเร็จ',
                icon: 'success',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'ยืนยัน',
                allowOutsideClick: false
            }).then((result) => {
                if (result.isConfirmed) {
                    window.location.href = urldes;
                    //window.location.href = '../approval/approval.aspx?approvalcode=' + code;
                }
            })
        }

        var arrVendor = new Array;
        $("#<%= cboVendor.ClientID%> option").each(function () {
            arrVendor.push($(this).val());
        });

        console.log(arrVendor);
        autocomplete(document.getElementById("<%= txtVendor.ClientID%>"), arrVendor, '<%= cboVendor.ClientID%>', '<%= txttaxid.ClientID%>');
        function autocomplete(inp, arr, elemVendor, elemtaxid) {

            /*the autocomplete function takes two arguments,
            the text field element and an array of possible autocompleted values:*/
            var currentFocus;
            /*execute a function when someone writes in the text field:*/
            inp.addEventListener("input", function (e) {
                var a, b, i, cnt_res = 0, val = this.value;

                /*close any already open lists of autocompleted values*/
                closeAllLists();
                if (!val) { return false; }
                currentFocus = -1;
                /*create a DIV element that will contain the items (values):*/
                a = document.createElement("DIV");
                a.setAttribute("id", this.id + "autocomplete-list");
                a.setAttribute("class", "autocomplete-items");
                /*append the DIV element as a child of the autocomplete container:*/
                this.parentNode.appendChild(a);
                /*for each item in the array...*/
                for (i = 0; i < arr.length; i++) {
                    /*check if the item starts with the same letters as the text field value:*/
                    //var cnt_replace = 0
                    var index = arr[i].toUpperCase().indexOf(val.toUpperCase());
                    ////console.log(cnt_replace);
                    if (index > -1) {
                        /*create a DIV element for each matching element:*/
                        b = document.createElement("DIV");
                        /*make the matching letters bold:*/
                        //b.innerHTML = "<strong>" + arr[i].substr(index, val.length) + "</strong>";
                        //b.innerHTML += arr[i].substr(val.length);
                        b.innerHTML = arr[i].substring(0, index) + "<strong>" + arr[i].substring(index, index + val.length) + "</strong>" + arr[i].substring(index + val.length);
                        /*insert a input field that will hold the current array item's value:*/
                        b.innerHTML += "<input type='hidden' value='" + arr[i] + "'>";
                        /*execute a function when someone clicks on the item value (DIV element):*/
                        b.addEventListener("click", function (e) {
                            /*insert the value for the autocomplete text field:*/
                            inp.value = this.getElementsByTagName("input")[0].value;
                            /*close the list of autocompleted values,
                            (or any other open lists of autocompleted values:*/
                            setVendorAutocomplete(inp.value, elemVendor, elemtaxid);
                            closeAllLists();
                        });
                        a.appendChild(b);
                        cnt_res += 1;
                        //if (cnt_res >= 8) {
                        //    break;
                        //}
                    }
                }
            });
            /*execute a function presses a key on the keyboard:*/
            inp.addEventListener("keydown", function (e) {
                var x = document.getElementById(this.id + "autocomplete-list");
                if (x) x = x.getElementsByTagName("div");
                if (e.keyCode == 40) {
                    /*If the arrow DOWN key is pressed,
                    increase the currentFocus variable:*/
                    currentFocus++;
                    /*and and make the current item more visible:*/
                    addActive(x);
                } else if (e.keyCode == 38) { //up
                    /*If the arrow UP key is pressed,
                    decrease the currentFocus variable:*/
                    currentFocus--;
                    /*and and make the current item more visible:*/
                    addActive(x);
                } else if (e.keyCode == 13) {
                    /*If the ENTER key is pressed, prevent the form from being submitted,*/
                    e.preventDefault();
                    if (currentFocus > -1) {
                        /*and simulate a click on the "active" item:*/
                        if (x) x[currentFocus].click();
                    }
                }
                //console.log("keydown");
            });
            function addActive(x) {
                /*a function to classify an item as "active":*/
                if (!x) return false;
                /*start by removing the "active" class on all items:*/
                removeActive(x);
                if (currentFocus >= x.length) currentFocus = 0;
                if (currentFocus < 0) currentFocus = (x.length - 1);
                /*add class "autocomplete-active":*/
                x[currentFocus].classList.add("autocomplete-active");
                //console.log("addActive");

            }
            function removeActive(x) {
                /*a function to remove the "active" class from all autocomplete items:*/
                for (var i = 0; i < x.length; i++) {
                    x[i].classList.remove("autocomplete-active");
                }
                //console.log("removeActive");

            }
            function closeAllLists(elmnt) {
                /*close all autocomplete lists in the document,
                except the one passed as an argument:*/
                var x = document.getElementsByClassName("autocomplete-items");
                for (var i = 0; i < x.length; i++) {
                    if (elmnt != x[i] && elmnt != inp) {
                        x[i].parentNode.removeChild(x[i]);
                    }
                }
                //console.log("closeAllLists");

            }
            /*execute a function when someone clicks in the document:*/
            document.addEventListener("click", function (e) {
                closeAllLists(e.target);
                //console.log("click");
            });
        }

        function setVendorAutocomplete(vendorname, elemVendor, elemid) {
            $("#" + elemVendor + " option:contains(" + vendorname + ")")
                .attr('selected', 'selected')
                .siblings()
                .removeAttr("selected");
            const Acc = document.getElementById(elemVendor);

            var taxidno = Acc.options[Acc.selectedIndex].getAttribute("data-taxidno");
            console.log(taxidno);

            $("#" + elemid).val(taxidno);

        }
    </script>

</asp:Content>
