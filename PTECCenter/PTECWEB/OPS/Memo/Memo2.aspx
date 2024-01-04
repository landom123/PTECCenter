<%@ Page Title="Memo" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="Memo2.aspx.vb" Inherits="PTECCENTER.Memo2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Page.ResolveUrl("~/css/card_comment.css")%>" rel="stylesheet">
    <style>
        .nonpo, .nonpounsaved, .statusnonpo {
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
    </style>
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

                            <asp:Button ID="btnNew" class="btn btn-sm  btn-primary" runat="server" Text="New" />&nbsp;
                            <asp:Button ID="btnCancel" class="btn btn-sm  btn-danger" runat="server" Text="Cancel" OnClientClick="Confirm();" />
                            &nbsp;   
                        </div>

                        <div class="col-auto  mb-3 text-right align-self-center">
                            <a href="MemoList.aspx" class="btn btn-sm btn-danger ">
                                <i class="fa fa-tasks" aria-hidden="true"></i></a>
                        </div>

                    </div>
                    <div class="foram pl-5 mb-3">
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
                    </div>
                    <div class="row nonpo">

                        <div class="col nonpounsaved" style="display: none;">
                            <asp:TextBox class="btn btn-warning" ID="txtUnsave" runat="server" ReadOnly="true">ยังไม่บันทึก</asp:TextBox>
                        </div>
                        <div class="col statusnonpo text-right align-self-center" style="/*display: none; */">
                            <asp:TextBox class="btn btn-warning" ID="statusmemo" runat="server" ToolTip="ป้ายสถานะ" ReadOnly="true">ยังไม่บันทึก</asp:TextBox>
                        </div>
                    </div>
                    <div class="card shadow mb-3 Detailheader">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-6">
                                    <div class="font-weight-bold">
                                        <asp:Label ID="txtName" runat="server" Text="Name" />
                                    </div>
                                </div>
                                <div class="col-6 text-break font-weight-bold text-right">
                                    <asp:Label ID="txtMemoCode" runat="server" Text="Status" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-6 ">
                                    <div class="row">
                                        <div class="col text-break">
                                            <asp:Label ID="preFixTo" runat="server" AssociatedControlID="txtTo" Text="To : " />
                                            <asp:Label ID="txtTo" runat="server" class="font-weight-bold" Text="" />
                                            <asp:Label ID="suffixTo" runat="server" CssClass="text-primary" Text="" />
                                        </div>
                                    </div>

                                <div class="row">
                                    <div class="col text-break">
                                        <asp:Label ID="preFixCc" runat="server" AssociatedControlID="txtCc" Text="Cc : " />
                                        <asp:Label ID="txtCc" runat="server" class="font-weight-bold" Text="" />
                                    </div>
                                </div>
                                </div>
                                <div class="col-6 text-right">
                                    <div class="row">
                                        <div class="col text-break">
                                            <small class="text-muted">
                                                <asp:Label ID="txtMemoDate" runat="server" Text="01/02/2023" />
                                            </small>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col ">
                                            <a href="#" title="share" data-toggle="modal" data-target="#modalShare" data-backdrop="static" data-keyboard="false" data-whatever="new"><i class="far fa-share-square"></i></a>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col text-break">
                                    <asp:Label ID="preFixSubject" runat="server" AssociatedControlID="txtSubject" Text="Subject : " />
                                    <asp:Label ID="txtSubject" runat="server" Text="" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col text-break">
                                    <asp:Label ID="preFixMemoCate" runat="server" AssociatedControlID="txtMemoCate" Text="Group : " />
                                    <asp:Label ID="txtMemoCate" runat="server" Text="" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col text-break">
                                    <asp:Label ID="preFixMemoType" runat="server" AssociatedControlID="txtMemoType" Text="Type : " />
                                    <asp:Label ID="txtMemoType" runat="server" Text="" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col text-break">
                                    <asp:Label ID="preFixCost" runat="server" AssociatedControlID="txtAmount" Text="Cost : " />
                                    <asp:Label ID="txtAmount" runat="server" Text="" />
                                </div>
                            </div>
                            <hr />

                            <div class="row">
                                <div class="col-4">
                                    <div class="row">
                                        <div class="col-4 mb-3">
                                            <span>ผู้ตรวจ</span>
                                        </div>
                                        <div class="col-8 mb-3 text-center font-weight-bold">
                                            <asp:Label ID="txtVeriftyBy" runat="server" Text="" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="row">
                                        <div class="col-4 mb-3 ">
                                            <span>ผู้อนุมัติ</span>
                                        </div>
                                        <div class="col-8 mb-3 text-center font-weight-bold">
                                            <asp:Label ID="txtApprovalBy" runat="server" Text="ทิชา มณีอ่อน" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-4">
                                    <div class="row">
                                        <div class="col-4 mb-3">
                                            <span>วันที่</span>
                                        </div>
                                        <div class="col-8 mb-3 text-center">
                                            <small class="text-muted">
                                                <asp:Label ID="txtVeriftyDate" runat="server" Text="07/12/2023" />
                                            </small>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="row">
                                        <div class="col-4">
                                            <span>วันที่</span>
                                        </div>
                                        <div class="col-8 text-center">
                                            <small class="text-muted">
                                                <asp:Label ID="txtApprovalDate" runat="server" Text="07/12/2023" />
                                            </small>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                </div>
                            </div>
                        </div>
                    </div>
                    <h5>รายละเอียด Memo</h5>
                    <hr />
                    <div id="contentDetail" class="mb-3" runat="server">
                        <div class="text-center" id="divcontent__file" runat="server">
                            <iframe id="fileContent" runat="server" class="file-upload-image" src="#" title="description" width="95%" height="1000px"></iframe>
                        </div>
                        <div class="container border border-secondary" id="divcontent__text" runat="server">
                            <asp:Label ID="txtContent" runat="server" Text="" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col mb-3">
                            <% If Not Request.QueryString("MemoCode") Is Nothing And detailtable.Rows.Count > 0 Then%>
                            <div class="text-center m-auto">
                                <% if (detailtable.Rows(0).Item("statusid") = 1) Then%>
                                <% If approval And detailtable.Rows(0).Item("statusid") = 1 Then%>
                                <asp:Button ID="btnApproval" class="btn btn-success" runat="server" Text="อนุมัติ" OnClientClick="Confirm();" />
                                <% End If %>
                                <% If verify And detailtable.Rows(0).Item("statusid") = 1 Then%>
                                <asp:Button ID="btnVerify" class="btn btn-warning" runat="server" Text="ยืนยันการตรวจสอบ" OnClientClick="Confirm();" />
                                <% End If %>
                                <% If ((verify Or approval)) Then%>
                                <asp:Button ID="btnDisApproval" class="btn btn-danger" runat="server" Text="ไม่อนุมัติ" OnClientClick="Confirm();" />
                                <% End If %>
                                <% End If %>
                                <% if (detailtable.Rows(0).Item("statusid") = 2) Then%>
                                <% If sign And detailtable.Rows(0).Item("statusid") = 2 Then%>
                                <asp:Button ID="btnSignature" class="btn btn-warning" runat="server" Text="ลงนามรับทราบ" OnClientClick="Confirm();" />
                                <% End If %>
                                <% End If %>
                            </div>
                            <% End If %>
                        </div>
                    </div>

                    <div class="row notPrint" id="card_attatch" runat="server">
                        <div class="col-md-6 mt-3">
                            <div class="card shadow card_attatch">
                                <div class="card-header">
                                    เอกสารแนบประกอบการพิจารณา
                                </div>
                                <div class="card-body attatchItems">
                                    <%--begin Attatch item--%>

                                    <% For i = 0 To AttachTable.Rows.Count - 1 %>
                                    <div class="row">
                                        <%-- <% If Not Request.QueryString("NonpoCode") Is Nothing And maintable.Rows.Count > 0 And (Session("depid").ToString = "2" Or Session("depid").ToString = "4" Or Session("depid").ToString = "24" Or Session("depid").ToString = "25") Then%>
                                        <% If maintable.Rows(0).Item("statusid") = 7 Then%>
                                        <div class="col-1">
                                            <div class="form-check">
                                                <input class="form-check-input" type="checkbox" id="<%= AttachTable.Rows(i).Item("id") %>" onclick="chkAttach(this,'<%= Session("userid") %>')">
                                            </div>
                                        </div>
                                        <% End If %>
                                        <% End If %>--%>
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
                                                <asp:Button ID="btnSaveComment" class="btn btn-primary w-100" runat="server" Text="Post" AutoPostBack="True" disabled />
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
            <!-- end content-->
        </div>
        <!-- end wrapper-->
    </div>
    <!-- end bg-->

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
                    <asp:Button ID="asd" class="btn btn-primary" runat="server" Text="Save" OnClientClick="chooseMyfile(); return false;" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalShare" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel_report" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel_report">Get Link</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <button type="button" class="btn btn-primary w-100" onclick="urlToClipboard()">Get URL</button>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary noEnterSubmit" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/NonPO.js")%>" asp-append-version="true"></script>

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
        function chooseMyfile() {
            validateData();

            const url = $('#<%= cboMyfile.ClientID%>').val();
            const description = $("#<%= cboMyfile.ClientID%> option:selected").text();
            sentAddAttach(url, description)

            return true;
        }


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

        function sentAddAttach(url, description) {
            if (url.substring(0, 7) != 'http://' && url.substring(0, 8) != 'https://') {
                url = 'http://' + url;
            }
            /*alert(url);*/
            let msg = '<a href="' + url + '" target="_blank">' + description + '</a>'

            const urlParams = new URLSearchParams(window.location.search);
            const nonpocode = urlParams.get('MemoCode');
            var user = "<% =Session("usercode").ToString %>";
            var userid = <%= Session("userid") %>;
            var params = "{'user': '" + user + "','url': '" + url + "','description': '" + description + "','nonpocode': '" + nonpocode + "'}";
            $.ajax({
                type: "POST",
                url: "/OPS/Non-PO/Payment/Payment2.aspx/addAttach",
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
                        alertWarning('Add URL fail');
                    }

                },
                error: function (msg) {
                    console.log(msg);

                    alertWarning('Add URL faila');

                }
            });

        }
        function stoppedTyping() {
            if (document.getElementById('<%= txtComment.ClientID%>').value.length > 0) {
                document.getElementById('<%= btnSaveComment.ClientID%>').disabled = false;
            } else {
                document.getElementById('<%= btnSaveComment.ClientID%>').disabled = true;

            }
        }
        function Confirm() {

            removeElem("confirm_value");
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm(`คุณต้องดำเนินการต่อหรือไม่ ?`)) {
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
        function urlToClipboard() {

            // Copy the text inside the text field
            navigator.clipboard.writeText(window.location.href);

            // Alert the copied text
            alertSuccessToast('Copied');

            $('#modalShare').modal('hide')
        }
    </script>
</asp:Content>
