<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="ClearAdvance.aspx.vb" Inherits="PTECCENTER.ClearAdvance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">

    <link href="<%=Page.ResolveUrl("~/css/print.css")%>" rel="stylesheet" type="text/css">
    <style>
        body {
            background-color: #f0f2f5;
        }

        h3, h5 {
            margin-right: auto;
            margin-left: auto;
        }

        .form-label {
            vertical-align: sub;
        }

        .form-control, .bootstrap-select .dropdown-toggle, .bootstrap-select.form-control {
            /*border: 1px solid #000;*/
            cursor: pointer;
            /*background-color: lightpink;*/
            height: 100%;
            width: 100%;
        }

        .input-detail input {
            background-color: #ffffff !important;
        }

        .company {
            margin-top: 10px;
            padding-left: 0px;
        }

        .company-th {
            margin-top: 15px;
        }

        .logopure {
            content: url("http://vpnptec.dyndns.org:10280/OPS_Fileupload/ATT_210800066.png");
            width: 100px;
            height: auto;
            margin-left: 30px;
            margin-top: 10px;
            margin-bottom: 10px;
        }

        table {
            padding: 20px;
            width: 1000px;
            border-radius: 2px;
            border-collapse: separate;
            border-spacing: 0;
            border: 1px;
            background-color: #ffffff;
            table-layout: fixed;
        }

        td, th {
            min-height: 2rem;
            margin: 0;
            border: 1px solid #000;
            white-space: nowrap;
            padding-left: 5px;
        }

        .nonpo, .nonpounsaved {
            width: 1000px;
            overflow-x: auto;
            overflow-y: visible;
            padding: 0;
            margin-right: auto;
            margin-left: auto;
        }

            .nonpounsaved .btn {
                border-top-left-radius: 10px;
                border-top-right-radius: 10px;
                border-bottom-left-radius: 0px;
                border-bottom-right-radius: 0px;
            }


        .draggable {
            padding: 1rem;
            /*background-color: lightpink;*/
            border: 1px solid black;
            cursor: move;
        }

            .draggable.dragging {
                opacity: .5;
            }

        /*.deletedetail {
            position: absolute;
            border: 0px solid #000;
            display: none;
        }

        .detail:hover .deletedetail {
            display: inline;
            justify-content: center;
            align-items: center;
        }*/
        .detail td, th {
            font-size: .75rem;
            overflow: hidden;
            text-overflow: ellipsis;
        }

        .editComment, .deleteComment {
            cursor: pointer;
            padding: 0px;
            font-size: .75rem;
            display: none;
            border-radius: 100%;
        }

        .comment-detail:hover .editComment, .comment-detail:hover .deleteComment {
            display: inline-block;
        }

        .comment-detail {
            max-width: 100%;
            background-color: #f0f2f5;
            padding: 10px;
            border-radius: 10px;
            display: table;
            word-break: break-word;
        }

        .detailComment:focus {
            background-color: yellow;
        }

        .card_comment .btn:disabled {
            cursor: not-allowed;
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
                        <div class="col">

                            <asp:Button ID="btnSave" class="btn btn-sm  btn-success btnSave" AutoPostBack="True" runat="server" Text="Save" />
                            &nbsp;              
                            <asp:Button ID="btnConfirm" class="btn btn-sm  btn-secondary" runat="server" Text="Confirm" />
                            &nbsp;   
                        </div>

                        <div class="col-auto text-right align-self-center">
                            <button id="btnExport" class="btn btn-sm  btn-info" style="color: #495057;" title="Export" runat="server">
                                <i class="fas fa-file-download"></i>
                            </button>
                            &nbsp;
                            <button id="btnPrint" class="btn btn-sm  btn-warning" style="color: #495057;" onclick="window.print();" title="Print" runat="server">
                                <i class="fas fa-print"></i>
                            </button>
                        </div>

                    </div>
                    <hr />
                    <div class="row mb-3">
                        <div class="col-2 text-right">
                            <asp:Label ID="lbcodeRef" CssClass="form-label" AssociatedControlID="codeRef" runat="server" Text="codeRef" />
                        </div>
                        <div class="col-7">
                            <asp:TextBox class="form-control font-weight-bold" ID="codeRef" runat="server" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-2 text-right">
                            <asp:Label ID="lbamount" CssClass="form-label" AssociatedControlID="amount" runat="server" Text="ยอดค้างชำระ" />
                        </div>
                        <div class="col-7">
                            <asp:TextBox class="form-control font-weight-bold" ID="amount" runat="server" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-2 text-right">
                            <asp:Label ID="lbtxtremark" CssClass="form-label" AssociatedControlID="txtremark" runat="server" Text="รายละเอียด" />
                        </div>
                        <div class="col-7">
                            <asp:TextBox class="form-control font-weight-bold" ID="txtremark" runat="server" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="nonpounsaved">
                            <% For i = 0 To detailtable.Rows.Count - 1 %>
                            <% if detailtable.Rows(i).Item("nonpodtl_id") = 0 Then%>
                            <asp:TextBox class="btn btn-warning" ID="TextBox2" runat="server" ReadOnly="true">ยังไม่บันทึก</asp:TextBox>
                            <% GoTo endprocess %>
                            <% End If %>
                            <% Next i %>
                            <% endprocess: %>
                        </div>

                        <div class="nonpo shadow mb-3 table-responsive">

                            <!-- (padding ซ้าย + ขวา = 40px ) -->
                            <!-- (table-width = 1000px ) -->
                            <!--  เนื้อหาข้างใน = 1000px - 40px  = 960 px -->
                            <table class="print ">

                                <!--  colทั้งหทด = 24 col -->
                                <!--  960/24  = 40 px -->
                                <tr>
                                    <!--  18 * 40  = 720 px -->
                                    <td colspan="18" style="width: 720px !important; height: 10px">
                                        <div class="row">
                                            <div class="col-3">
                                                <img class="logopure" />
                                            </div>
                                            <div class="col-9 company">
                                                <div class="row company-th">
                                                    <h3>บริษัท เพียวพลังงานไทย จำกัด</h3>
                                                </div>
                                                <div class="row company-en">
                                                    <h5>PURE THAI ENERGY COMPANY LIMITED</h5>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                    <!--  6 * 40  = 240 px -->
                                    <td colspan="6" style="width: 240px !important;">
                                        <div class="row">

                                            <h5>ADVANCE CLEARING</h5>
                                        </div>
                                    </td>
                                </tr>
                                <tr>


                                    <td colspan="12" style="width: 420px !important;">
                                        <div class="row">

                                            <div class="col-1">
                                                <asp:Label ID="Label2" CssClass="form-label" AssociatedControlID="cboOwner" runat="server" Text="ผู้เบิก" />
                                            </div>
                                            <div class="col-11">

                                                <asp:DropDownList class="form-control" ID="cboOwner" runat="server" readonly="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td colspan="6" style="width: 240px !important;">
                                        <div class="row">

                                            <div class="col-2">
                                                <asp:Label ID="lbcboBranch" CssClass="form-label" AssociatedControlID="cboBranch" runat="server" Text="สาขา" />
                                            </div>
                                            <div class="col-10">
                                                <asp:DropDownList class="form-control" ID="cboBranch" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td colspan="6" style="width: 240px !important;">
                                        <div class="row">

                                            <div class="col-3">
                                                <asp:Label ID="lbtxtadvno" CssClass="form-label" AssociatedControlID="txtadvno" runat="server" Text="เลขที่" />
                                            </div>
                                            <div class="col-9">
                                                <asp:TextBox class="form-control" ID="txtadvno" runat="server" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="9" style="width: 360px !important;">
                                        <div class="row">
                                            <div class="col-2">
                                                <asp:Label ID="Label3" CssClass="form-label" AssociatedControlID="cboDepartment" runat="server" Text="ฝ่าย" />
                                            </div>
                                            <div class="col-10">
                                                <asp:DropDownList class="form-control" ID="cboDepartment" AutoPostBack="True"
                                                    runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td colspan="9" style="width: 360px !important;">
                                        <div class="row">
                                            <div class="col-2">
                                                <asp:Label ID="lbApprovalcode" CssClass="form-label" AssociatedControlID="cboSection" runat="server" Text="แผนก" />
                                            </div>
                                            <div class="col-10">
                                                <asp:DropDownList class="form-control" ID="cboSection" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>

                                    <td colspan="6" style="width: 240px !important;">
                                        <div class="row">
                                            <div class="col-3">
                                                <asp:Label ID="Label7" CssClass="form-label" AssociatedControlID="txtCreateDate" runat="server" Text="วันที่" />
                                            </div>
                                            <div class="col-9">
                                                <asp:TextBox class="form-control font-weight-bold" ID="txtCreateDate" runat="server" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="24" style="width: 960px !important; height: 50px;">
                                        <div class="row">
                                            <h5 class="m-auto">ข้าพเจ้าขอเคลียร์เงินยืม (Cash Advance) ที่เบิกจากบริษัทฯ ไปแล้ว คังรายละเอียดต่อไปนี้</h5>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="text-center" rowspan="2" colspan="2" style="width: 80px !important;">รหัสบัญชี</th>
                                    <th class="text-center" rowspan="2" colspan="10" style="width: 400px !important;">รายละเอียด</th>
                                    <th class="text-center" colspan="6" style="width: 240px !important;">Dimension</th>
                                    <th class="text-center" rowspan="2" colspan="4" style="width: 160px !important;">จำนวนเงิน</th>
                                    <th class="text-center" rowspan="2" colspan="2" style="width: 80px !important;">Vendor</th>

                                </tr>
                                <tr>
                                    <th class="text-center" colspan="2" style="width: 80px !important;">Dep.</th>
                                    <th class="text-center" colspan="2" style="width: 80px !important;">BU.</th>
                                    <th class="text-center" colspan="2" style="width: 80px !important;">PP.</th>

                                </tr>
                                <!--  ############## Detail ############### -->
                                <tbody class="shortArea">

                                    <% For i = 0 To detailtable.Rows.Count - 1 %>
                                    <tr class="draggable detail" draggable="true" name="<%= detailtable.Rows(i).Item("row").ToString() %>" ondblclick="btnEditDetailClick('<%= detailtable.Rows(i).Item("row").ToString() %>','<%= detailtable.Rows(i).Item("nonpodtl_id").ToString() %>','<%= detailtable.Rows(i).Item("accountcodeid").ToString() %>'
                                                                                        ,'<%= detailtable.Rows(i).Item("depid").ToString() %>','<%= detailtable.Rows(i).Item("buid").ToString() %>'
                                                                                        ,'<%= detailtable.Rows(i).Item("ppid").ToString() %>','<%= detailtable.Rows(i).Item("cost").ToString() %>'
                                                                                        ,'<%= detailtable.Rows(i).Item("detail").ToString() %>','<%= detailtable.Rows(i).Item("vendorcode").ToString() %>');">
                                        <td colspan="2" style="width: 80px !important; height: 22px;" title="<%= detailtable.Rows(i).Item("accountcode").ToString() %>"><%= if((detailtable.Rows(i).Item("accountcodeid").ToString()) = "0", "", detailtable.Rows(i).Item("accountcodeid").ToString()) %></td>
                                        <td colspan="10" style="width: 400px !important;" title="<%= detailtable.Rows(i).Item("detail").ToString() %>"><span><%= detailtable.Rows(i).Item("detail").ToString() %></span></td>
                                        <td colspan="2" style="width: 80px !important;" title="<%= detailtable.Rows(i).Item("depname").ToString() %>"><%= detailtable.Rows(i).Item("depname").ToString() %></td>
                                        <td colspan="2" style="width: 80px !important;" title="<%= detailtable.Rows(i).Item("buname").ToString() %>"><%= detailtable.Rows(i).Item("buname").ToString() %></td>
                                        <td colspan="2" style="width: 80px !important;" title="<%= detailtable.Rows(i).Item("ppname").ToString() %>"><%= detailtable.Rows(i).Item("ppname").ToString() %></td>
                                        <td colspan="4" style="width: 160px !important;" title="<%= detailtable.Rows(i).Item("cost").ToString() %>"><%= if((detailtable.Rows(i).Item("cost").ToString()) = "0", "", detailtable.Rows(i).Item("cost").ToString()) %>
                                        </td>
                                        <td colspan="2" style="width: 80px !important;" title="<%= detailtable.Rows(i).Item("vendorname").ToString() %>"><%= detailtable.Rows(i).Item("vendorcode").ToString() %>  </td>
                                        <td class="deletedetail notprint" style="position: absolute; border: 0px solid #000;">
                                            <div>
                                                <a onclick="confirmDeletedetail('<%= detailtable.Rows(i).Item("nonpodtl_id").ToString() %>','<%= detailtable.Rows(i).Item("row").ToString() %>')" class="btn btn-sm p-0 notPrint">
                                                    <i class="fas fa-times"></i>
                                                </a>
                                            </div>
                                        </td>
                                    </tr>
                                    <% Next i %>
                                </tbody>
                                <tr class="text-center notPrint " runat="server" id="FromAddDetail">
                                    <td colspan="24" style="width: 960px !important;">
                                        <button type="button" class="btn btn-sm  btn-outline-info w-100" id="btnFromAddDetail" runat="server" data-toggle="modal" data-target="#exampleModal" data-whatever="new">เพิ่มรายการ</button>
                                    </td>
                                </tr>

                                <!--  ############## End Detail ############### -->
                                <tfoot>
                                    <!--  total -->
                                    <tr>
                                        <td colspan="2" style="width: 80px !important; text-align: right; border-right-width: 0px;">
                                            <h5>รวม
                                            </h5>
                                        </td>
                                        <td colspan="14" style="width: 560px !important; text-align: center; border-left-width: 0px; border-right-width: 0px;">
                                            <h5>
                                                <p id="id01"></p>
                                            </h5>
                                        </td>
                                        <td colspan="4" style="width: 160px !important; border-left-width: 0px;">บาท</td>
                                        <td colspan="4" style="width: 160px !important;" id="total"><%=total %></td>
                                    </tr>
                                    <!--  end total -->
                                    <tr>
                                        <td colspan="20" style="width: 800px !important;">
                                            <div class="row">
                                                <div class="col-11" style="margin-left: auto;">
                                                    <input class="form-check-input chk-img-after" type="checkbox" id="chkpayBack" name="pay[1][]" runat="server">
                                                    <asp:Label ID="lbchkpayBack" CssClass="form-check-label" AssociatedControlID="chkpayBack" runat="server" Text="คืนเงินบริษัท" />
                                                </div>
                                            </div>
                                        </td>
                                        <td colspan="4" style="width: 160px !important;" id="payBack">
                                            <div class="row">
                                                <div class="col">
                                                        <asp:TextBox class="form-control" type="number" ID="txtamountpayBack" runat="server" min="0" Text="0"></asp:TextBox>
                                                        <div class="invalid-feedback">* ตัวเลขจำนวนเต็ม</div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="20" style="width: 800px !important;">
                                            <div class="row">
                                                <div class="col-11" style="margin-left: auto;">
                                                    <input class="form-check-input chk-img-after" type="checkbox" id="chkdeductSell" name="pay[1][]" runat="server">
                                                    <asp:Label ID="lbchkdeductSell" CssClass="form-check-label" AssociatedControlID="chkdeductSell" runat="server" Text="หักยอดขาย" />
                                                </div>
                                            </div>
                                        </td>
                                        <td colspan="4" style="width: 160px !important;" id="deduct_sell">
                                            <div class="row">
                                                <div class="col">
                                                        <asp:TextBox class="form-control" type="number" ID="txtamountdedusctsell" runat="server" min="0" Text="0"></asp:TextBox>
                                                        <div class="invalid-feedback">* ตัวเลขจำนวนเต็ม</div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="width: 240px !important; border-bottom-width: 0px;">
                                            <h5>ผู้เบิก</h5>
                                        </td>
                                        <td colspan="6" style="width: 240px !important; border-bottom-width: 0px;">
                                            <h5>ผู้รับสินค้า/บริกการ</h5>
                                        </td>
                                        <td colspan="6" style="width: 240px !important; border-bottom-width: 0px;">
                                            <h5>ผู้ตรวจ</h5>
                                        </td>
                                        <td colspan="6" style="width: 240px !important; border-bottom-width: 0px;">
                                            <h5>ผู้อนุมัติ</h5>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="width: 240px !important; border-top-width: 0px; border-bottom-width: 0px;"></td>
                                        <td colspan="6" style="width: 240px !important; border-top-width: 0px; border-bottom-width: 0px;"></td>
                                        <td colspan="6" style="width: 240px !important; border-top-width: 0px; border-bottom-width: 0px;"></td>
                                        <td colspan="6" style="width: 240px !important; border-top-width: 0px; border-bottom-width: 0px;"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="width: 240px !important; border-top-width: 0px;">วันที่</td>
                                        <td colspan="6" style="width: 240px !important; border-top-width: 0px;">วันที่</td>
                                        <td colspan="6" style="width: 240px !important; border-top-width: 0px;">วันที่</td>
                                        <td colspan="6" style="width: 240px !important; border-top-width: 0px;">วันที่</td>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                    </div>
                    <!-- End row -->


                    <hr />

                    <div class="row">

                        <% If Not Request.QueryString("NonpoCode") Is Nothing And maintable.Rows.Count > 0 Then%>
                        <% if Session("status") = "write" And maintable.Rows(0).Item("statusid") = 1 Then%>
                        <div class="text-center bg-white">
                            <asp:Button ID="btnApproval" class="btn btn-success" runat="server" Text="อนุมัติ" />
                            <button runat="server" id="btnDisApproval" name="btnEdit" onclick="return disApproval();" class="btn btn-danger">
                                ไม่อนุมัติ
                            </button>
                        </div>
                        <% End If %>
                        <% End If %>
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
                                        <div class="col">
                                            <a href="<%= AttachTable.Rows(i).Item("url").ToString() %>" class="text-primary listCommentAndAttatch " style="cursor: pointer;" target="_blank">
                                                <span><%= AttachTable.Rows(i).Item("show").ToString() %></span></a>
                                        </div>
                                    </div>
                                    <%-- end Attatch item--%>
                                    <% Next i %>
                                </div>
                                <div class="card-footer">
                                    <a onclick="addAttach()" class="text-primary" style="cursor: pointer; transition: .2s;">
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
                                                    <a onclick="btnEditCommentClick('<%= CommentTable.Rows(i).Item("commentid").ToString() %>')" class="btn btn-sm editComment">
                                                        <i class="fas fa-pen"></i>
                                                    </a>&nbsp;
                                                    <a onclick="confirmDelete('<%= CommentTable.Rows(i).Item("commentid").ToString() %>')" class="btn btn-sm deleteComment">
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
                                                    <asp:TextBox class="form-control bg-white" ID="txtComment" runat="server" Style="cursor: auto;" Rows="2" Columns="50" TextMode="MultiLine" onkeyup="stoppedTyping()" onkeyDown="checkTextAreaMaxLength(this,event,'255');" placeholder="Comment . ."></asp:TextBox>
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
    <div class="row btn-operator justify-content-center notPrint">
        <% If Not Request.QueryString("NonpoCode") Is Nothing And maintable.Rows.Count > 0 And Session("secid").ToString = "2" Then%>
        <% If maintable.Rows(0).Item("statusid") = 7 Then%>
        <!-- 7 = รอบช.ตรวจ-->
        <button class="btn btn-sm " style="color: #39cd5b; font-size: 3rem; position: fixed; bottom: 9rem; right: 1rem;" id="btnPass" runat="server" title="Pass">
            <i class="fas fa-check-circle shadow" style="border-radius: 100%;"></i>
        </button>
        <button class="btn btn-sm " style="color: #b8c5d1; font-size: 3rem; position: fixed; bottom: 5rem; right: 1rem;" id="btnEdit" runat="server" title="Edit">
            <i class="fas fa-pause-circle shadow" style="border-radius: 100%;"></i>
        </button>
        <button class="btn btn-sm " style="color: #dc3545; font-size: 3rem; position: fixed; bottom: 1rem; right: 1rem;" id="btnReject" runat="server" title="Reject">
            <i class="fas fa-times-circle" style="border-radius: 100%;"></i>
        </button>
        <% ElseIf maintable.Rows(0).Item("statusid") = 8 Then %>
        <!-- 8 = รอเอกสารตัวจริง-->
        <button class="btn btn-sm " style="color: #dc3545; font-size: 3rem; position: fixed; bottom: 1rem; right: 1rem;" id="btnWDoc" runat="server" title="waiting for documents">
            <i class="fas fa-times-circle" style="border-radius: 100%;"></i>
        </button>
        <% End If %>
        <% End If %>
    </div>
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">รายละเอียดรายการ</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <!--  ##############  Detail ############### -->
                    <input type="hidden" class="form-control" id="row" value="0" runat="server">
                    <input type="hidden" class="form-control" id="nextrow" value="0" runat="server">
                    <input type="hidden" class="form-control" id="hiddenAdvancedetailid" value="0" runat="server">
                    <div class="form-group">
                        <asp:Label ID="lbcboAccountCode" CssClass="form-label" AssociatedControlID="cboAccountCode" runat="server" Text="รหัสบัญชี" />
                        <asp:DropDownList class="form-control" ID="cboAccountCode" runat="server" onchange="setdetail(this);"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label4" CssClass="form-label" AssociatedControlID="cboDep" runat="server" Text="Department" />
                        <asp:DropDownList class="form-control" ID="cboDep" runat="server"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label8" CssClass="form-label" AssociatedControlID="cboBU" runat="server" Text="Business Unit" />
                        <asp:DropDownList class="form-control" ID="cboBU" runat="server"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label10" CssClass="form-label" AssociatedControlID="cboPP" runat="server" Text="Purpose" />
                        <asp:DropDownList class="form-control" ID="cboPP" runat="server"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbPrice" CssClass="form-label" AssociatedControlID="txtPrice" runat="server" Text="จำนวนเงิน" />
                        <asp:TextBox class="form-control" type="number" ID="txtPrice" runat="server" Text="0"></asp:TextBox>
                        <div class="invalid-feedback">* ตัวเลขจำนวนเต็ม</div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbDetail" CssClass="form-label" AssociatedControlID="txtDetail" runat="server" Text="รายละเอียด" />
                        <asp:Label ID="lbDetailMandatory" CssClass="text-danger" AssociatedControlID="txtDetail" runat="server" Text="*" />
                        <asp:TextBox class="form-control" ID="txtDetail" runat="server" Rows="3" Columns="50" TextMode="MultiLine" onkeyDown="checkTextAreaMaxLength(this,event,'255');"></asp:TextBox>
                        <div class="invalid-feedback">กรุณากรอกรายละเอียด</div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbcboVendor" CssClass="form-label" AssociatedControlID="cboVendor" runat="server" Text="Vendor" />
                        <asp:DropDownList class="form-control" ID="cboVendor" runat="server"></asp:DropDownList>
                    </div>


                    <!--  ############## End Detail ############### -->
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <asp:Button ID="btnAddDetail" class="btn btn-primary" runat="server" Text="Save" OnClientClick="validateData()" />&nbsp;
                </div>
            </div>
        </div>
    </div>

    <script src="<%=Page.ResolveUrl("~/js/Sortable.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>
    <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("../js/NonPO.js")%>"></script>

    <script type="text/javascript">

        jQuery('[id$=txtDuedate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08'
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });

    </script>

    <script>
        $(window).load(function () {

            $('.form-control').prop('disabled', false);
        });
        $(document).ready(function () {
            var groups = {};
            $("select option[data-category]").each(function () {
                groups[$.trim($(this).attr("data-category"))] = true;
            });
            $.each(groups, function (c) {
                $("select option[data-category='" + c + "']").wrapAll('<optgroup label="' + c + '">');
            });

            const total = document.getElementById("total");
            const element = document.getElementById("id01");
            const Number = CheckNumber(total.textContent);
            console.log(Number);

            if (!isNaN(Number) && (Number - 0) < 9999999.9999) {
                element.innerHTML = " (   " + ArabicNumberToText(total.textContent) + "   )";
            } else {
                element.innerHTML = "รวม ";
            }

            $('.form-control').selectpicker({
                noneSelectedText: '-',
                liveSearch: true,
                maxOptions: 1
            });

            $('.form-control').selectpicker('refresh');

           

            /*$(".listCommentAndAttatch").click(function () {
                $(".card_attatch").toggle();
                $(".card_comment").toggle();
            });*/
            stoppedTyping();
        });

        function stoppedTyping() {
            if (document.getElementById('<%= txtComment.ClientID%>').value.length > 0) {
                document.getElementById('<%= btnSaveComment.ClientID%>').disabled = false;
            } else {
                document.getElementById('<%= btnSaveComment.ClientID%>').disabled = true;

            }
        }

        function updateComment(commentID, Oldtext) {
            const elemenmt = document.getElementById(commentID);
            elemenmt.setAttribute("contenteditable", "false");
            console.log('update');
            var userid = <% =Session("userid") %>;
            console.log(userid);

            var params = "{'userid': '" + userid + "','msg': '" + elemenmt.value + "','commentid': '" + commentID + "'}";
            $.ajax({
                type: "POST",
                url: "../ADMIN/menupermission.aspx/aaaa",
                async: true,
                data: params,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: function () {

                    elemenmt.textContent = Oldtext;
                    event.preventDefault();
                    event.stopPropagation();
                    alertWarning('Update Comment fail')


                }
            });
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
                    if (!document.getElementById('swal-input1').value) {
                        // Handle return value 
                        Swal.showValidationMessage('URL missing')
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
                    const nonpocode = urlParams.get('NonpoCode');
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

        function confirmDeletedetail(nonpodtlid, row) {
            Swal.fire({
                title: 'คุุณต้องการจะลบข้อมุลนี้ใช่หรือไม่ ?',
                text: "",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes'
            }).then((result) => {
                if (result.isConfirmed) {

                    var user = "<% =Session("usercode").ToString %>";
                    var params = "{'nonpodtlid': '" + nonpodtlid + "','rows': '" + row + "','user': '" + user + "'}";
                    $.ajax({
                        type: "POST",
                        url: "../Advance/ClearAdvance.aspx/deleteDetail",
                        async: true,
                        data: params,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d == 'success') {
                                swal.fire({
                                    title: "Deleted!",
                                    text: "",
                                    icon: "success"
                                }).then(function () {
                                    __doPostBack('detailtable', '')
                                });
                            } else {
                                alertWarning('fail')
                            }
                        },
                        error: function () {
                            alertWarning('fail ee')
                        }
                    });
                }
            })

            return false;
        }
        function setdetail(Acc) {

            const myArr = Acc.options[Acc.selectedIndex].textContent.split(" - ");
            console.log(myArr[myArr.length - 1]);

            $("#<%= txtDetail.ClientID%>").val(myArr[myArr.length - 1]);

        }



        function selectElement(id, valueToSelect) {
            let element = document.getElementById(id);
            element.value = valueToSelect;
        }
        function btnEditDetailClick(row, advancedetailid, accountcodeid, depid, buid, ppid, cost, detail, vendorcode) {
            /*console.log(advancedetailid);
            console.log(accountcodeid);
            console.log(depid);
            console.log(buid);
            console.log(ppid);
            console.log(cost);
            console.log(detail);
            console.log(vendorcode);*/

            const Accountcode = '<%= cboAccountCode.ClientID%>';
            const dep = '<%= cboDep.ClientID%>';
            const bu = '<%= cboBU.ClientID%>';
            const pp = '<%= cboPP.ClientID%>';
            const vendor = '<%= cboVendor.ClientID%>';
            $('#exampleModal').modal('show');

            selectElement(Accountcode, accountcodeid);
            selectElement(dep, depid);
            selectElement(bu, buid);
            selectElement(pp, ppid);
            selectElement(vendor, vendorcode);

            $('#<%= row.ClientID%>').val(row);
            $('#<%= hiddenAdvancedetailid.ClientID%>').val(advancedetailid);
            $('#<%= txtPrice.ClientID%>').val(cost);
            $('#<%= txtDetail.ClientID%>').val(detail);

            $('.form-control').selectpicker('refresh');
            /*__doPostBack('setFromDetail', $(row).attr('name'));
            */

        }
        $("#<%= btnAddDetail.ClientID%>").click(function () {
            alert("The paragraph was clicked.");
            const row = $('#<%= row.ClientID%>').val();
            alert(row);
            var user = "<% =Session("usercode").ToString %>";
            var params = "{'nonpodtlid': '" + nonpodtlid + "','rows': '" + row + "','user': '" + user + "'}";
            $.ajax({
                type: "POST",
                url: "../Advance/ClearAdvance.aspx/deleteDetail",
                async: true,
                data: params,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == 'success') {
                        swal.fire({
                            title: "Deleted!",
                            text: "",
                            icon: "success"
                        }).then(function () {
                            __doPostBack('detailtable', '')
                        });
                    } else {
                        alertWarning('fail')
                    }
                },
                error: function () {
                    alertWarning('fail ee')
                }
            });
        });
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
