<%@ Page Title="New NTI" Language="vb" AutoEventWireup="true" MasterPageFile="~/site.Master" CodeBehind="newNTI.aspx.vb" Inherits="PTECCENTER.newNTI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
    <link href="<%=Page.ResolveUrl("~/css/card_comment.css")%>" rel="stylesheet">
    <style>
        body {
            background-color: #f0f2f5;
        }

        .col-form-label {
            text-align: right;
        }


        @media only screen and (max-width: 992px) {
            .col-form-label {
                text-align: left;
            }
        }


        .icon__status {
            background-color: #bfc2c4;
        
        }
        .name__status {
            text-align:center;
            font-size: .7rem;
        }
        .group__status .past {
            background-color: #0078d4;
        }
        .group__status .now {
            background-color: #00ff27;
        }
        .group__status .end {
            background-color: #dc3545;
        }
        /*####################### CSS FROM ATTATCH ########################*/
        .attatchItems-link-btndelete .deletedetail {
            font-size: .7rem
        }
        /*####################### END CSS FROM ATTATCH ########################*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg">
        <div id="wrapper" class="h-100">

            <!-- #include virtual ="/include/menu.inc" -->
            <!-- add side menu -->
            <div id="content-wrapper">
                <div class="container">
                    <div class="row">
                        <div class="col mb-3">

                            <%--&nbsp;   
                            <asp:Button ID="btnConfirm" class="btn btn-sm  btn-secondary" runat="server" Text="Confirm" OnClientClick="Confirm();" />
                            &nbsp;
                            <asp:Button ID="btnCancel" class="btn btn-sm  btn-danger" runat="server" Text="Cancel" />
                            &nbsp;   --%>
                        </div>

                        <div class="col-auto text-right align-self-center">
                            <%-- <button id="btnExport" class="btn btn-sm  btn-info" style="color: #495057;" title="Export" runat="server">
                                <i class="fas fa-file-download"></i>
                            </button>--%>
                            <%--<button type="button" class="btn btn-sm  btn-info noEnterSubmit" style="color: #495057;" title="Export" id="btnExport" runat="server" data-toggle="modal" data-target="#modalExport" data-backdrop="static" data-keyboard="false" data-whatever="new"><i class="fas fa-file-download"></i></button>

                            &nbsp;
                            <button id="btnPrint" class="btn btn-sm  btn-warning" style="color: #495057;" onclick="event.preventDefault();event.stopPropagation();window.print();" title="Print" runat="server">
                                <i class="fas fa-print"></i>
                            </button>--%>
                            <a href="newNTIMenuList.aspx" class="btn btn-sm btn-danger ">
                                <i class="fa fa-tasks" aria-hidden="true"></i></a>
                        </div>

                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-12 mb-3">

                            <div class="group__status row" >
                            <% For i = 0 To stepStatus.Rows.Count - 1 %>
                                <div class="col-lg-auto">

                                <div class="icon__status <%= stepStatus.Rows(i).Item("step") %>" id="stGSM" style="margin-top: 6.5px; margin-left: auto; margin-right: auto; width: 15px !important; height: 15px !important; border-radius: 50%; margin-bottom: 0.15rem !important;">
                                </div>
                                <div class="name__status" >
                                    <%= stepStatus.Rows(i).Item("StatusName") %>
                                </div>
                                </div>

                                <%-- end Attatch item--%>
                            <% Next i %>
                            </div>

                            
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-12 mb-3">
                            <div class="card shadow">
                                <div class="card-header" style="background-color: #065ca9;">
                                    <div class="row justify-content-between">
                                        <div class="col-auto text-left align-self-center">
                                            <h1 style="color: white">NEW NTI</h1>
                                        </div>
                                        <div class="col-auto text-end align-self-center mr-4">
                                            <div class="row justify-content-end">
                                                <button class="btn btn-primary" type="button" data-toggle="collapse" data-target="#multiCollapseExample2" aria-expanded="false" aria-controls="multiCollapseExample2"><i class="far fa-address-card"></i></button>
                                            </div>
                                            <div class="row justify-content-end">
                                                <div class="collapse multi-collapse" id="multiCollapseExample2">
                                                    <div class="card card-body">
                                                        <div class="row">
                                                            <span id="txtcreateby" runat="server"></span>
                                                        </div>
                                                        <div class="row">
                                                            <span id="txttell" runat="server"></span>
                                                        </div>
                                                        <div class="row">
                                                            <span id="txtemail" runat="server"></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body">
                                    <div class="row mb-3">
                                        <div class="col-lg-2 col-form-label">
                                            <asp:Label ID="lbtxtnticode" CssClass="form-label" AssociatedControlID="txtnticode" runat="server" Text="เลขที่เอกสาร" />
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:TextBox class="form-control" ID="txtnticode" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:TextBox class="form-control" ID="txtcreatedate" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-4">
                                            <asp:TextBox class="form-control" ID="txtstatus" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <%--<div class="row mb-3">
                                        <div class="col-lg-2 col-form-label">
                                            <asp:Label ID="lbtxtstatus" CssClass="form-label" AssociatedControlID="txtstatus" runat="server" Text="สถานะเอกสาร" />
                                        </div>
                                        <div class="col-lg-10">
                                        </div>
                                    </div>--%>
                                    <div class="row mb-3">
                                        <div class="col-lg-2 col-form-label">
                                            <asp:Label ID="latxttitle" CssClass="form-label" AssociatedControlID="txttitle" runat="server" Text="ชื่อสถานที่" />
                                        </div>
                                        <div class="col-lg-10">
                                            <div class="input-group ">
                                                <asp:TextBox class="form-control" ID="txttitle" runat="server"></asp:TextBox>
                                                <div class="input-group-append">
                                                    <asp:Button ID="btnUpdate" class="btn btn-sm  btn-warning" runat="server" Text="Update" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-lg-2 col-form-label">
                                            <asp:Label ID="lbcboOfferType" CssClass="form-label" AssociatedControlID="cboOfferType" runat="server" Text="ข้อเสนอ" />
                                        </div>
                                        <div class="col-lg-10">
                                            <div class="input-group ">
                                                <asp:DropDownList class="form-control" ID="cboOfferType" runat="server"></asp:DropDownList>
                                                <div class="input-group-append">
                                                    <asp:Button ID="btnUpdateOffer" class="btn btn-sm  btn-warning" runat="server" Text="Update" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-lg-2 col-form-label">
                                            <asp:Label ID="lbtxtOwnerName" CssClass="form-label" AssociatedControlID="txtOwnerName" runat="server" Text="ชื่อ (-)" />
                                        </div>
                                        <div class="col-lg-10">
                                            <asp:TextBox class="form-control" ID="txtOwnerName" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-lg-2 col-form-label">
                                            <asp:Label ID="lbtxtOwnerTell" CssClass="form-label" AssociatedControlID="txtOwnerTell" runat="server" Text="เบอร์ (-)" />
                                        </div>
                                        <div class="col-lg-10">
                                            <asp:TextBox class="form-control" ID="txtOwnerTell" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-lg-2 col-form-label">
                                            <asp:Label ID="lbtxtAreaType" CssClass="form-label" AssociatedControlID="txtAreaType" runat="server" Text="ประเภทที่ดิน" />
                                        </div>
                                        <div class="col-lg-10">
                                            <asp:TextBox class="form-control" ID="txtAreaType" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-lg-2 col-form-label">
                                            <asp:Label ID="lbtxtAreaAddress" CssClass="form-label" AssociatedControlID="txtAreaAddress" runat="server" Text="ข้อมูลที่อยู่ที่ดิน" />
                                        </div>
                                        <div class="col-lg-10">
                                            <asp:TextBox class="form-control" ID="txtAreaAddress" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-lg-2 col-form-label">
                                            <asp:Label ID="lbtxtArea" CssClass="form-label" AssociatedControlID="txtArea" runat="server" Text="ขนาดพื้นที่" />
                                        </div>
                                        <div class="col-lg-10">
                                            <asp:TextBox class="form-control" ID="txtArea" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-lg-2 col-form-label">
                                            <asp:Label ID="lbbtnGPS" CssClass="form-label" AssociatedControlID="btnGPS" runat="server" Text="GPS" />
                                        </div>
                                        <div class="col-lg-10">
                                            <a id="btnGPS" href="#" runat="server" class="text-primary" style="cursor: pointer; transition: .2s;">
                                                <i class="fas fa-map-marker-alt"></i><span id="txtlalong" runat="server">&nbsp;</span></a>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-lg-2 col-form-label">
                                            <asp:Label ID="lbtxtNumberArea" CssClass="form-label" AssociatedControlID="txtNumberArea" runat="server" Text="เลขที่โฉนดที่ดิน" />
                                        </div>
                                        <div class="col-lg-10">
                                            <asp:TextBox class="form-control" ID="txtNumberArea" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-lg-2 col-form-label">
                                            <asp:Label ID="lbtxtRemark" CssClass="form-label" AssociatedControlID="txtRemark" runat="server" Text="รายละเอียดอื่นๆ" />
                                        </div>
                                        <div class="col-lg-10">
                                            <asp:TextBox class="form-control " ID="txtRemark" runat="server" Rows="3" Columns="50" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>



                                </div>
                                <div class="card-footer">
                                    <div class="row justify-content-center">
                                        <div class="col-6">
                                            <%--<div class="input-group d-none">
                                                <asp:DropDownList ID="cboStatus" class="form-control" runat="server"></asp:DropDownList>
                                                <div class="input-group-append">
                                                    <asp:Button ID="btnConfirm" class="btn btn-sm  btn-warning" runat="server" Text=" + " OnClientClick="validateData()" />
                                                </div>
                                            </div>--%>

                                            <asp:Button ID="btnConfirm" class="btn btn-success" runat="server" Text="ขั้นต่อไป" />
                                            <button runat="server" id="btnDisApproval" name="btnEdit" onclick="return disApproval();" class="btn btn-danger">
                                                ไม่อนุมัติ
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- end card-->
                        </div>
                    </div>
                    <div class="row notPrint" id="card_attatch" runat="server">
                        <div class="col-md-6 mt-3">
                            <div class="card shadow card_attatch">
                                <div class="card-header">
                                    เอกสารแนบ
                                </div>
                                <div class="card-body attatchItems">
                                    <%--begin Attatch item--%>

                                    <% For i = 0 To AttachTable.Rows.Count - 1 %>
                                    <div class="row">
                                        <%--<% If Not Request.QueryString("NonpoCode") Is Nothing And maintable.Rows.Count > 0 And (Session("depid").ToString = "2" Or Session("depid").ToString = "4" Or Session("depid").ToString = "24" Or Session("depid").ToString = "25") Then%>
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
                                                    <asp:TextBox class="form-control bg-white" ID="txtComment" runat="server" Style="cursor: auto;" Rows="2" Columns="50" TextMode="MultiLine" onkeyup="stoppedTyping();" value="" autocomplete="off"></asp:TextBox>
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
        </div>
    </div>

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
                    <%--<button type="button" id="btnAddDetail" class="btn btn-primary noEnterSubmit">Save</button>--%>
                    <asp:Button ID="asd" class="btn btn-primary" runat="server" Text="Save" OnClientClick="chooseMyfile(); return false;" />
                </div>
            </div>
        </div>
    </div>

    <script src="<%=Page.ResolveUrl("~/js/Sortable.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>
    <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/NonPO.js")%>"></script>
    <script>

        $(document).ready(function () {

            $('.form-control').selectpicker({
                noneSelectedText: '-',
                liveSearch: true,
                maxOptions: 1
            });

            $('.form-control').selectpicker('refresh');

        });


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
        function chooseMyfile() {
            validateData();

            const url = $('#<%= cboMyfile.ClientID%>').val();
            const description = $("#<%= cboMyfile.ClientID%> option:selected").text();
            sentAddAttach(url, description)

            return true;
        }
        function sentAddAttach(url, description) {
            if (url.substring(0, 7) != 'http://' && url.substring(0, 8) != 'https://') {
                url = 'http://' + url;
            }
            /*alert(url);*/
            let msg = '<a href="' + url + '" target="_blank">' + description + '</a>'

            const urlParams = new URLSearchParams(window.location.search);
            const nonpocode = urlParams.get('nticode');
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
        function stoppedTyping() {
            if (document.getElementById('<%= txtComment.ClientID%>').value.length > 0) {
                document.getElementById('<%= btnSaveComment.ClientID%>').disabled = false;
            } else {
                document.getElementById('<%= btnSaveComment.ClientID%>').disabled = true;

            }
        }


        function disApproval() {

            /*alert(GridView);*/
            var approvalcode = document.getElementById('<%= txtnticode.ClientID%>').value
            var usercode = "<%= Session("usercode")%>";

        Swal.fire({
            input: 'textarea',
            inputLabel: 'ไม่อนุมัติเนื่องจาก',
            inputPlaceholder: 'ใส่ข้อความ . . .',
            inputAttributes: {
                'aria-label': 'ใส่ข้อความ.'
            },
            preConfirm: (value) => {
                if (!value) {
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
                    url: "../Station/newNTI.aspx/disApprovalByCode",
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

</asp:Content>
