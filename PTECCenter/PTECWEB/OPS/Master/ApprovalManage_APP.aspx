<%@ Page Title="จัดการงาน (APP)" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="ApprovalManage_APP.aspx.vb" Inherits="PTECCENTER.ApprovalManage_APP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="<%=Page.ResolveUrl("~/css/card_comment.css")%>" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="wrapper">

        <!-- #include virtual ="/include/menu.inc" -->
        <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">
                <!-- Breadcrumbs-->
                <ol class="breadcrumb" style="background-color: navy; color: white">
                    จัดการงาน (APP)
                </ol>
                <div class="row">
                    <div class="col-md-12 mb-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">เลขที่เอกสาร</span>
                            </div>
                            <div class="d-flex flex-column align-items-center input-group-text" readonly="true">
                                <asp:Label CssClass="approvalcode" ID="lbapprovalcode" runat="server" Text=''></asp:Label>
                                <a href="#" class="badge badgestatus_app" id="badgeapprovalcode" runat="server"></a>
                            </div>
                            <div class="input-group-append">
                                <button type="button" class="btn btn-sm  btn-secondary" onclick="find('../Master/ApprovalManage_APP.aspx?approvalcode=','ระบุเลขที่เอกสาร')">Find</button>
                            </div>
                        </div>
                    </div>
                </div>
                <% If Not Request.QueryString("approvalcode") Is Nothing And detailtable.Rows.Count > 0 Then%>
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
                    <div class="col-lg-3 mb-3">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ค่าใช้จ่าย</span>
                            </div>
                            <asp:TextBox class="form-control" type="number" ID="txtPrice" runat="server" min="0" Text="0" required></asp:TextBox>
                            <asp:Button ID="btnSavePrice" class="btn btn-success btn-sm" runat="server" Text="Save" OnClientClick="Confirm();" />
                        </div>
                    </div>
                    <div class="col-lg-3 mb-3">
                        
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">จำนวนวัน</span>
                            </div>
                            <asp:TextBox class="form-control" type="number" ID="txtDay" runat="server" min="0"></asp:TextBox>
                            <asp:Button ID="btnSaveDay" class="btn btn-success btn-sm" runat="server" Text="Save" OnClientClick="Confirm();" />
                        </div>
                    </div>
                </div>
                <hr />
                <div class="row notPrint" id="card_attatch" runat="server">
                    <div class="col-lg-12  mb-3" id="card_comment" runat="server">
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
                //console.log(st_name)
                switch (st_name) {
                    case "รอผู้แจ้งยืนยัน":
                        arrs_app[i].style.backgroundColor = "LightBlue";
                        break;
                    case "ยกเลิกการแจ้ง":
                        arrs_app[i].style.backgroundColor = "LightGray";
                        break;
                    case "อนุมัติ":
                        arrs_app[i].style.backgroundColor = "GreenYellow";
                        break;
                    case "รออนุมัติ":
                        arrs_app[i].style.backgroundColor = "LightYellow";
                        break;
                    case "ชำระเงินเสร็จสิ้น":
                        arrs_app[i].style.backgroundColor = "GreenYellow";
                        break;
                    case "ไม่อนุมัติ":
                        arrs_app[i].style.backgroundColor = "IndianRed";
                        break;
                    case "ปิดงาน":
                        arrs_app[i].style.backgroundColor = "Gray";
                        break;
                    case "รอประสานงานรับเรื่อง":
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
                    case "รอแสกนเอกสาร":
                        arrs_app[i].style.backgroundColor = "Brown";
                        arrs_app[i].style.color = "White";

                        break;
                    case "รอบัญชีตรวจสอบ":
                        arrs_app[i].style.backgroundColor = "LightSalmon";

                        break;
                    case "เอกสารครบถ้วน":
                        arrs_app[i].style.backgroundColor = "MediumPurple";
                }
                arrs_app[i].style.color = "#ececec";
            }

        });
        function Confirm() {

            //validateData();
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
        function stoppedTyping() {
            if (document.getElementById('<%= txtComment.ClientID%>').value.length > 0) {
                    document.getElementById('<%= btnSaveComment.ClientID%>').disabled = false;
                } else {
                    document.getElementById('<%= btnSaveComment.ClientID%>').disabled = true;

            }
        }

    </script>
</asp:Content>
