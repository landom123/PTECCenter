<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="AdvanceRequest.aspx.vb" Inherits="PTECCENTER.AdvanceRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Page.ResolveUrl("~/css/card_comment.css")%>" rel="stylesheet">
    <style>
        .card-advancerequest {
            max-width: 960px;
        }

        .col-form-label {
            text-align: right;
        }

        @media only screen and (max-width: 992px) {
            .col-form-label {
                text-align: left;
            }
        }

        .HO, .CO {
            display: none;
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
                    <div class="foram">
                        <div class="row">
                            <%=Session("status") %>
                        </div>
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
                    </div>
                    <div class="row mb-3">
                        <div class="col">
                            <div class="card shadow card-advancerequest mx-auto">
                                <div class="card-header">
                                    <div class="row">
                                        <div class="col text-left align-self-center">
                                            ขอเบิก Advance
                                        </div>
                                        <div class="col-auto text-right align-self-center">
                                            <a href="AdvanceMenuList.aspx" class="btn btn-sm btn-danger ">
                                                <i class="fa fa-tasks" aria-hidden="true"></i></a>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body frmADV_RQ">
                                    <% If Not Request.QueryString("ADV") Is Nothing Then%>
                                    <div class="row form-group">
                                        <div class="col-lg-2 col-form-label">
                                            <asp:Label ID="lbNonPOcode" CssClass="form-label" AssociatedControlID="txtNonPOcode" runat="server" Text="เลขที่ใบงาน" />
                                        </div>
                                        <div class="col-lg-10">
                                            <asp:TextBox class="form-control" ID="txtNonPOcode" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-lg-2 col-form-label">
                                            <asp:Label ID="lbStatusRq" CssClass="form-label" AssociatedControlID="txtStatusRq" runat="server" Text="สถานะ" />
                                        </div>
                                        <div class="col-lg-10">
                                            <asp:TextBox class="form-control" ID="txtStatusRq" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-lg-2 col-form-label">
                                            <asp:Label ID="lbCreateBy" CssClass="form-label" AssociatedControlID="txtCreateBy" runat="server" Text="ผู้เปิดงาน" />
                                        </div>
                                        <div class="col-lg-10">
                                            <asp:TextBox class="form-control" ID="txtCreateBy" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <%--<div class="row form-group">
                                        <div class="col-lg-2 col-form-label">
                                            <asp:Label ID="lbtxtOwnerby" CssClass="form-label" AssociatedControlID="txtOwnerby" runat="server" Text="เจ้าของงาน" />
                                        </div>
                                        <div class="col-lg-10">
                                            <asp:TextBox class="form-control" ID="txtOwnerby" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>--%>
                                    <div class="row form-group">
                                        <div class="col-lg-2 col-form-label">
                                            <asp:Label ID="lbDocDate" CssClass="form-label" AssociatedControlID="txtDocDate" runat="server" Text="วันที่แจ้ง" />
                                        </div>
                                        <div class="col-lg-10">
                                            <asp:TextBox class="form-control" ID="txtDocDate" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-lg-2 col-form-label">
                                            <asp:Label ID="lbApprovalby" CssClass="form-label" AssociatedControlID="txtApprovalby" runat="server" Text="ผู้อนุมัติ" />
                                        </div>
                                        <div class="col-lg-10">
                                            <asp:TextBox class="form-control  text-success font-weight-bold" ID="txtApprovalby" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-lg-2 col-form-label">
                                            <asp:Label ID="lbApprovalDate" CssClass="form-label" AssociatedControlID="txtApprovalDate" runat="server" Text="วันที่อนุมัติ" />
                                        </div>
                                        <div class="col-lg-10">
                                            <asp:TextBox class="form-control  text-success font-weight-bold" ID="txtApprovalDate" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-lg-2 col-form-label">
                                            <asp:Label ID="lbtxtSupportby" CssClass="form-label" AssociatedControlID="txtSupportby" runat="server" Text="ทำจ่ายโดย" />
                                        </div>
                                        <div class="col-lg-10">
                                            <asp:TextBox class="form-control  text-info font-weight-bold" ID="txtSupportby" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-lg-2 col-form-label">
                                            <asp:Label ID="lbtxtSupportdate" CssClass="form-label" AssociatedControlID="txtSupportdate" runat="server" Text="วันที่ทำจ่าย" />
                                        </div>
                                        <div class="col-lg-10">
                                            <asp:TextBox class="form-control  text-info font-weight-bold" ID="txtSupportdate" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <% End If %>
                                    <div class="row form-group">

                                        <div class="col-lg-2 col-form-label">
                                            <asp:Label ID="lbtxtamount" CssClass="form-label" AssociatedControlID="txtamount" runat="server" Text="จำนวนเงิน" />
                                            <asp:Label ID="lbMandatoryamount" CssClass="text-danger " AssociatedControlID="txtamount" runat="server" Text="*" />
                                        </div>
                                        <div class="col-lg-10">
                                            <asp:TextBox class="form-control" ID="txtamount" runat="server" type="number" min="0" step="any" required></asp:TextBox>
                                            <div class="invalid-feedback">กรุณาใส่จำนวนเงิน</div>
                                        </div>
                                    </div>
                                    <% If Not Request.QueryString("ADV") Is Nothing Then%>
                                    <div class="row form-group">

                                        <div class="col-lg-2 col-form-label">
                                            <asp:Label ID="lbbalance" CssClass="form-label" AssociatedControlID="txtbalance" runat="server" Text="ยอดคงค้างชำระ" />
                                        </div>
                                        <div class="col-lg-10">
                                            <asp:TextBox class="form-control text-danger font-weight-bold" ID="txtbalance" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>

                                    <% End If %>
                                    <div class="row form-group">
                                        <div class="col-lg-2 col-form-label">
                                            <asp:Label ID="lbtxtdetail" CssClass="form-label" AssociatedControlID="txtdetail" runat="server" Text="รายละเอียด" />
                                            <asp:Label ID="lbMandatorydetail" CssClass="text-danger " AssociatedControlID="txtdetail" runat="server" Text="*" />
                                        </div>
                                        <div class="col-lg-10">
                                            <asp:TextBox class="form-control" ID="txtdetail" runat="server" TextMode="MultiLine" required></asp:TextBox>
                                            <div class="invalid-feedback">กรุณาใส่รายละเอียด</div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-footer text-center bg-white">
                                    <% If Session("status") = "new" Then%>
                                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" OnClientClick="validateData()" />
                                    <% ElseIf Session("status") = "edit" Then%>
                                    <asp:Button ID="btnSaveEdit" class="btn btn-success" runat="server" Text="Save" OnClientClick="validateData()" />
                                    <asp:Button ID="btnCancelEdit" class="btn btn-danger" runat="server" Text="Cancel" />
                                    <% ElseIf (Session("userid").ToString() = detailtable.Rows(0).Item("ownerid").ToString()) Then%>
                                    <asp:Button ID="btnConfirm" class="btn btn-warning" runat="server" Text="Confirm" OnClientClick="Confirm();" />
                                    <asp:Button ID="btnCancel" class="btn btn-danger" runat="server" Text="Cancel" />
                                    <asp:Button ID="btnClose" class="btn btn-danger" runat="server" Text="ปิดงาน" />
                                    <asp:Button ID="btnAddDoc" class="btn btn-success" runat="server" Text="แนบเอกสารให้ฝ่ายประสานงาน" />
                                    <asp:Button ID="btnEdit" class="btn btn-secondary" runat="server" Text="Edit" />
                                    <asp:Button ID="btnClearAdvance" class="btn btn-warning" runat="server" Text="เคลียร์ค่าใช้จ่าย" />
                                    <% End If %>
                                    <% If Not Request.QueryString("ADV") Is Nothing And detailtable.Rows.Count > 0 Then%>
                                    <% If Session("status") = "write" And approval And detailtable.Rows(0).Item("statusrqid") = 2 Then%>
                                    <asp:Button ID="btnApproval" class="btn btn-success" runat="server" Text="อนุมัติ" />

                                    <button runat="server" id="btnDisApproval" name="btnEdit" onclick="return disApproval();" class="btn btn-danger">
                                        ไม่อนุมัติ
                                    </button>
                                    <% End If %>
                                    <% If verify And detailtable.Rows(0).Item("statusrqid") = 3 Then%>
                                    <asp:Button ID="btnVerify" class="btn btn-warning" runat="server" Text="อนุมัติ" />

                                    <button runat="server" id="Button2" name="btnEdit" onclick="return disApproval();" class="btn btn-danger">
                                        ไม่อนุมัติ
                                    </button>
                                    <% End If %>
                                    <% End If %>
                                </div>

                            </div>
                        </div>
                    </div>
                    <!-- -->
                    <div class="row">
                        <div class="col">
                            <div class="card-body">
                                <div class="table-responsive overflow-auto" style="font-size: 0.9rem">
                                    <asp:GridView ID="gvRemind"
                                        class="table table-striped table-bordered"
                                        AutoGenerateColumns="false"
                                        EmptyDataText="No data available."
                                        PageSize="5"
                                        AllowPaging="true"
                                        runat="server">
                                        <Columns>
                                            <asp:TemplateField HeaderText="เลขที่ใบงาน" ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcode" runat="server" Text='<%#Eval("AdvanceRequest_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ขอโดย" ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBranch" runat="server" Text='<%#Eval("Createname")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="วันที่ขอ" ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbljobdate" runat="server" Text='<%#Eval("CreateDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รายละเอียด">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbljobdate" runat="server" Text='<%#Eval("detail")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="จำนวนเงินที่เบิก" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbljobtype" runat="server" Text='<%#String.Format("{0:n2}", Eval("amount"))%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ยอดคงค้างชำระ" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbljobtype" runat="server" Text='<%#String.Format("{0:n2}", Eval("balance"))%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="สถาณะ" ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbljobtype" runat="server" Text='<%#Eval("status")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Eval("link")%>' Text="" Target="_blank"><img src="../../../icon/addnote.png" title="รายละเอียด" style="width:20px" /></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <% If cntdt > 0 Then%>
                                <span class="text-red font-weight-bold text-danger">*** (รายการที่เบิกเงินแล้ว แต่ยังไม่เคลียร์ยอดค้างชำระ) ***</span>
                                <h4>ทั้งหมด <% =cntdt%> รายการ </h4>
                                <h4>ยอดรวมค้างชำระ <b><% =String.Format("{0:n4}", sumitem)%></b> บาท</h4>
                                <% End If %>
                            </div>

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
                                        <% If Not Request.QueryString("ADV") Is Nothing And detailtable.Rows.Count > 0 And (Session("depid").ToString = "2" Or Session("depid").ToString = "4") Then%>
                                        <% If detailtable.Rows(0).Item("statusrqid") = 3 Then%>
                                        <div class="col-1">
                                            <div class="form-check">
                                                <input class="form-check-input" type="checkbox" id="<%= AttachTable.Rows(i).Item("id") %>" onclick="chkAttach(this,'<%= Session("userid") %>')">
                                            </div>
                                        </div>
                                        <% End If %>
                                        <% End If %>

                                        <div class="col-auto">
                                            <a href="<%= AttachTable.Rows(i).Item("url").ToString() %>" class="text-primary listCommentAndAttatch " style="cursor: pointer;" target="_blank">
                                                <span><%= AttachTable.Rows(i).Item("show").ToString() %></span></a>
                                        </div>
                                    </div>
                                    <%-- end Attatch item--%>
                                    <% Next i %>
                                </div>
                                <div class="card-footer">
                                    <a onclick="addAttach()" id="btnAddAttatch" runat="server" class="text-primary" style="cursor: pointer; transition: .2s;">
                                        <i class="fas fa-plus-circle"></i><span>&nbsp;แนบลิ้งเอกสาร</span></a>
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
                                                    <asp:TextBox class="form-control bg-white" ID="txtComment" runat="server" Style="cursor: auto;" Rows="2" Columns="50" TextMode="MultiLine" onkeyup="stoppedTyping();" placeholder="Comment . ." value=""></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row justify-content-center">
                                            <div class="col-md-12">
                                                <asp:Button ID="btnSaveComment" class="btn btn-primary w-100" runat="server" Text="Post" disabled />
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


    <script src="<%=Page.ResolveUrl("~/js/Sortable.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>
    <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("../js/NonPO.js")%>"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            

            <% If Not AttachTable Is Nothing Then %>
                <% For i = 0 To AttachTable.Rows.Count - 1 %>
                    <% If AttachTable.Rows(i).Item("checked") = 1 Then %>
             $('.attatchItems #<%=AttachTable.Rows(i).Item("id")%>').prop('checked', true);
                    <% Else %>
            $('.attatchItems #<%=AttachTable.Rows(i).Item("id")%>').prop('checked', false);
                    <% End If %>
                <% Next i %>
             <% End if %>

             
         });
        function stoppedTyping() {
            if (document.getElementById('<%= txtComment.ClientID%>').value.length > 0) {
                document.getElementById('<%= btnSaveComment.ClientID%>').disabled = false;
            } else {
                document.getElementById('<%= btnSaveComment.ClientID%>').disabled = true;

            }
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
                    if (url.substring(0, 7) != 'http://' && url.substring(0, 8) != 'https://') {
                        url = 'http://' + url;
                    }
                    /*alert(url);*/
                    let msg = '<a href="' + url + '" target="_blank">' + description + '</a>'

                    const urlParams = new URLSearchParams(window.location.search);
                    const nonpocode = urlParams.get('ADV');
                    var user = "<% =Session("usercode").ToString %>";
                    var params = "{'user': '" + user + "','url': '" + url + "','description': '" + description + "','nonpocode': '" + nonpocode + "'}";
                    $.ajax({
                        type: "POST",
                        url: "../Advance/ClearAdvance.aspx/addAttach",
                        async: true,
                        data: params,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {


                            /*alertSuccessToast();*/
                            if (msg.d == 'success') {
                                if (!description) {
                                    description = 'Link';
                                }
                                /*__doPostBack('AttachTable', '')*/
                                $('.attatchItems').append(
                                    '<div class="row">' +
                                    '<div class="col">' +
                                    '<a href="' + url + '" class="text-primary listCommentAndAttatch " style="cursor: pointer;" target="_blank">' +
                                    '<span>' + description + '</span></a>' +
                                    '</div>' +
                                    '</div>'
                                );
                                alertSuccessToast('บันทึกเรียบร้อย' + description);
                            } else {
                                alertWarning('Add URL fail');
                            }

                        },
                        error: function () {
                            alertWarning('Add URL fail');
                        }
                    });

                }
            })

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
</asp:Content>
