<%@ Page Title="จัดการงาน (PCCO)" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="NonPOManage_PCCO.aspx.vb" Inherits="PTECCENTER.NonPOManage_PCCO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="<%=Page.ResolveUrl("~/css/card_comment.css")%>" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="wrapper" class="h-100">

        <!-- #include virtual ="/include/menu.inc" -->
        <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">
                <!-- Breadcrumbs-->
                <ol class="breadcrumb" style="background-color: navy; color: white">
                    จัดการงาน (PCCO)
                </ol>
                <div class="row">
                    <div class="col-12 mb-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">เลขที่เอกสาร</span>
                            </div>
                            <div class="d-flex flex-column align-items-center input-group-text" readonly="true">
                                <asp:Label CssClass="approvalcode" ID="lbapprovalcode" runat="server" Text=''></asp:Label>
                                <a href="#" class="badge badgestatus_app" id="badgeapprovalcode" runat="server"></a>
                            </div>
                            <div class="input-group-append">
                                <button type="button" class="btn btn-sm  btn-secondary" onclick="find('../Master/NonPOManage_PCCO.aspx?NonpoCode=','ระบุเลขที่เอกสาร')">Find</button>
                            </div>
                        </div>
                    </div>
                </div>
                <% If Not Request.QueryString("NonpoCode") Is Nothing And nonpodt.Rows.Count > 0 Then%>
                <div class="row">
                    <div class="col-lg-6 mb-3">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ต้องการเปลี่ยนเป็นสถานะ</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboStatusFollow" runat="server" AutoPostBack="false"></asp:DropDownList>
                            <asp:Button ID="btnSave" class="btn btn-success btn-sm" runat="server" Text="Save" OnClientClick="Confirm();" />
                        </div>
                    </div>
                </div>
                <hr />
                <div class="row notPrint" id="card_attatch" runat="server">
                    <div class="col-md-6  mb-3">
                        <div class="card shadow card_attatch">
                            <div class="card-header">
                                เอกสารแนบ
                            </div>
                            <div class="card-body attatchItems">
                                <%--begin Attatch item--%>

                                <% For i = 0 To AttachTable.Rows.Count - 1 %>
                                <div class="row">
                                    <% If Not Request.QueryString("NonpoCode") Is Nothing And nonpodt.Rows.Count > 0 And (Session("depid").ToString = "2" Or Session("depid").ToString = "4" Or Session("depid").ToString = "24" Or Session("depid").ToString = "25") Then%>
                                    <% If nonpodt.Rows(0).Item("statusid") = 7 Then%>
                                    <div class="col-1">
                                        <div class="form-check">
                                            <input class="form-check-input" type="checkbox" id="<%= AttachTable.Rows(i).Item("id") %>" onclick="chkAttach(this,'<%= Session("userid") %>')">
                                        </div>
                                    </div>
                                    <% End If %>
                                    <% End If %>
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
                    <div class="col-md-6 mb-3" id="card_comment" runat="server">
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
                <% End If %>
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
    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>

    <script src="<%=Page.ResolveUrl("~/js/NonPO.js")%>"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('.form-control').selectpicker({
                noneSelectedText: '-',
                liveSearch: true,
                maxOptions: 1
            });

            $('.form-control').selectpicker('refresh');

            const arrs_app = document.querySelectorAll('.badgestatus_app');
            console.log(arrs_app)
            for (let i = 0; i < arrs_app.length; i++) {
                let st_name = arrs_app[i].textContent;
                console.log(st_name)
                switch (st_name) {
                    case "รอยืนยัน":
                        arrs_app[i].style.backgroundColor = "LightBlue";
                        break;
                    case "ยกเลิก":
                        arrs_app[i].style.backgroundColor = "LightGray";
                        break;
                    case "รอตรวจสอบ":
                        arrs_app[i].style.backgroundColor = "LightGoldenrodYellow";
                        break;
                    case "รออนุมัติ":
                        arrs_app[i].style.backgroundColor = "LightYellow";
                        break;
                    case "ชำระเงินเสร็จสิ้น":
                        arrs_app[i].style.backgroundColor = "GreenYellow";
                        break;
                    case "ไม่ผ่านการอนุมัติ":
                        arrs_app[i].style.backgroundColor = "IndianRed";
                        break;
                    case "ได้รับเอกสารตัวจริง":
                        arrs_app[i].style.backgroundColor = "Gray";
                        break;
                    case "รอการเงินตรวจสอบ":
                        arrs_app[i].style.backgroundColor = "LightCoral";
                        break;
                    case "ดำเนินการด้านเอกสาร":
                        arrs_app[i].style.backgroundColor = "OrangeRed";
                        break;
                    case "รอเอกสารตัวจริง":
                        arrs_app[i].style.backgroundColor = "Yellow";
                        break;
                    case "การเงินได้รับเอกสาร":
                        arrs_app[i].style.backgroundColor = "Gray";
                        break;
                    case "รอเคลียร์ค้างชำระ":
                        arrs_app[i].style.backgroundColor = "Brown";
                        arrs_app[i].style.color = "White";

                        break;
                    case "รอบัญชีตรวจสอบ":
                        arrs_app[i].style.backgroundColor = "LightSalmon";

                        break;
                    case "ขอเอกสารเพิ่มเติม":
                        arrs_app[i].style.backgroundColor = "MediumPurple";
                }
                arrs_app[i].style.color = "#ececec";
            }

        });
        function Confirm() {

            validateData();
            console.log("insave");
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("คุณต้องการจะบันทึกหรือไม่ ?")) {
                confirm_value.value = "Yes";
            }
            else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
            return true;
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
            const nonpocode = urlParams.get('NonpoCode');
            var user = "<% =Session("usercode").ToString %>";
            var userid = <%= Session("userid") %>;
            var params = "{'user': '" + user + "','url': '" + url + "','description': '" + description + "','nonpocode': '" + nonpocode + "'}";
            $.ajax({
                type: "POST",
                url: "../Non-PO/PettyCash/PettyCashCO2.aspx/addAttach",
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
    </script>
</asp:Content>
