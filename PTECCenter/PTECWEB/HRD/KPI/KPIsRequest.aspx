<%@ Page Title="KPIsOverview" Language="vb" AutoEventWireup="true" MasterPageFile="~/site.Master" CodeBehind="KPIsRequest.aspx.vb" Inherits="PTECCENTER.KPIsRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
    <link href="<%=Page.ResolveUrl("~/css/card_comment.css")%>" rel="stylesheet">

    <style>
        .modal .form-group, .modal .form-control, .modal .bootstrap-select .dropdown-toggle, .modal .bootstrap-select .dropdown-menu {
            font-size: 0.875rem;
        }

        .nonpounsaved, .statusnewkpi {
            width: 1000px;
            overflow-x: auto;
            overflow-y: visible;
            margin-right: auto;
            margin-left: auto;
        }

            .nonpounsaved input, .statusnewkpi input {
                border-top-left-radius: 10px;
                border-top-right-radius: 10px;
                border-bottom-left-radius: 0px;
                border-bottom-right-radius: 0px;
            }

        .pre-line {
            white-space: pre-line;
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

                            <asp:Button ID="btnSave" class="btn btn-sm  btn-success btnSave" runat="server" Text="Save" />
                            &nbsp;              
                            <asp:Button ID="btnConfirm" class="btn btn-sm  btn-secondary" runat="server" Text="Confirm" OnClientClick="Confirm();" />
                            &nbsp;   
                            <asp:Button ID="btnCancel" class="btn btn-sm  btn-danger" runat="server" Text="Cancel" OnClientClick="Confirm();" />
                            &nbsp;   
                        </div>

                        <div class="col-auto text-right align-self-center">
                            <a href="KPIsRequestList.aspx" class="btn btn-sm btn-danger ">
                                <i class="fa fa-tasks" aria-hidden="true"></i></a>
                        </div>

                    </div>
                    <hr />
                    <div class="foram">
                        <div class="row">
                            <%=allOwner %>
                        </div>
                        <div class="row">
                            <%=now_action %>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col nonpounsaved" style="display: none;">
                            <asp:TextBox class="btn btn-warning" ID="txtUnsave" runat="server" ReadOnly="true">ยังไม่บันทึก</asp:TextBox>
                        </div>
                        <div class="col statusnewkpi text-right align-self-center" style="/*display: none; */">
                            <asp:TextBox class="btn btn-warning" ID="statusnewkpi" runat="server" ToolTip="ป้ายสถานะ" ReadOnly="true">*</asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col">
                            <div class="card shadow">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col">
                                            <div class="row">
                                                <div class="col mb-3">
                                                    <asp:DropDownList ID="cboPeriod" class="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-auto">
                                                    <div class="media">
                                                        <img src="<%=Page.ResolveUrl("~/icon/Logo_pure.png")%>" class="align-self-center mr-3" alt="..." width="150">
                                                        <div class="media-body">
                                                            <div class="row">
                                                                <div class="col ">
                                                                    <h5>
                                                                        <asp:Label ID="txtOwnername" CssClass="font-weight-bold" runat="server" Text="" />
                                                                    </h5>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col mb-1">
                                                                    <asp:Label ID="preFixPosition" runat="server" Text="ตำแหน่ง : " />
                                                                    <asp:Label ID="txtPosition" runat="server" Text="" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col mb-1">

                                                                    <asp:Label ID="preFixDep" runat="server" Text="ฝ่าย : " />
                                                                    <asp:Label ID="txtDep" runat="server" Text="" />
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col mb-1">
                                                                    <asp:Label ID="preFixSec" runat="server" Text="แผนก : " />
                                                                    <asp:Label ID="txtSec" runat="server" Text="" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col d-flex flex-column justify-content-between align-items-end">
                                                    <div class="row">
                                                        <div class="col ">
                                                            <asp:Label CssClass="h5 font-weight-bold" ID="txtnewkpi" runat="server" Text="" />
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col">
                                                            <h2>
                                                                <a href="#" class="d-none" title="Ratio" runat="server" id="txtratio"></a>
                                                            </h2>
                                                        </div>
                                                    </div>
                                                    <%--competency,individual,section,department,corporate--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <div class="col">

                                            <%--<nav>
                                                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                                                    <% For i As Integer = weighttable.Rows.Count - 1 To 0 Step -1 %>
                                                    <a class="nav-item nav-link" id="nav-<%= weighttable.Rows(i).Item("CategoryName").ToString().ToLower() %>-tab" data-toggle="tab" href="#nav-<%= weighttable.Rows(i).Item("CategoryName").ToString().ToLower() %>" role="tab" aria-controls="nav-<%= weighttable.Rows(i).Item("CategoryName").ToString().ToLower() %>" aria-selected="false"><%= weighttable.Rows(i).Item("CategoryName").ToString() %>
                                                        <span class="badge badge-light">1</span>
                                                    </a>
                                                    <% Next i %>
                                                </div>
                                            </nav>--%>
                                            <div id="nav-tabContent">
                                                <% if groupdetailtable.Rows.Count > 0 Then%>
                                                <% For i As Integer = 0 To groupdetailtable.Rows.Count - 1 %>
                                                <div class="mb-5" id="nav-<%= groupdetailtable.Rows(i).Item("CategoryName").ToString().ToLower() %>" role="tabpanel" aria-labelledby="nav-<%= groupdetailtable.Rows(i).Item("CategoryName").ToString().ToLower() %>-tab">
                                                    <div class="row">
                                                        <div class="col mb-3 h5 font-weight-bold">
                                                            <span class="ratio__type"><%= groupdetailtable.Rows(i).Item("CategoryName").ToString() %></span>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col table-responsive-xl">
                                                            <table class="table table-sm table-hover ">
                                                                <thead>
                                                                    <tr class="text-center">
                                                                        <th>Code</th>
                                                                        <th>หัวข้อ</th>
                                                                        <th>น้ำหนัก</th>
                                                                        <th>หน่วยวัด</th>
                                                                        <th>ระดับ 5</th>
                                                                        <th>ระดับ 4</th>
                                                                        <th>ระดับ 3</th>
                                                                        <th>ระดับ 2</th>
                                                                        <th>ระดับ 1</th>
                                                                        <th></th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody class="DetailArea">
                                                                    <% For j = 0 To detailtable.Rows.Count - 1 %>
                                                                    <% if groupdetailtable.Rows(i).Item("CategoryName").ToString() = detailtable.Rows(j).Item("CategoryName").ToString() Then%>
                                                                    <tr class="draggable detail" data-status="<%= detailtable.Rows(j).Item("status").ToString() %>" name="<%= detailtable.Rows(j).Item("kpi_code").ToString() %>" style="cursor: pointer; transition: .2s;"
                                                                        ondblclick="btnEditDetailClick('<%= detailtable.Rows(j).Item("kpi_code").ToString() %>'
                                                                                                        ,'<%= detailtable.Rows(j).Item("categoryid").ToString() %>'
                                                                                                        ,'<%= detailtable.Rows(j).Item("title").ToString() %>'
                                                                                                        ,'<%= detailtable.Rows(j).Item("weight").ToString() %>'
                                                                                                        ,'<%= detailtable.Rows(j).Item("unit").ToString() %>'
                                                                                                        ,'<%= detailtable.Rows(j).Item("lv1").ToString() %>'
                                                                                                        ,'<%= detailtable.Rows(j).Item("lv2").ToString() %>'
                                                                                                        ,'<%= detailtable.Rows(j).Item("lv3").ToString() %>'
                                                                                                        ,'<%= detailtable.Rows(j).Item("lv4").ToString() %>'
                                                                                                        ,'<%= detailtable.Rows(j).Item("lv5").ToString() %>');">
                                                                        <td class="text-center"><span class="pre-line"><%= detailtable.Rows(j).Item("kpi_code").ToString() %></span></td>
                                                                        <td><div class="pre-line"><%= detailtable.Rows(j).Item("title").ToString() %></div></td>
                                                                        <td class="text-center"><span class="pre-line"><%= detailtable.Rows(j).Item("weight").ToString() %></span></td>
                                                                        <td class="text-center"><span class="pre-line"><%= detailtable.Rows(j).Item("unit").ToString() %></span></td>
                                                                        <td class="text-center"><span class="pre-line"><%= detailtable.Rows(j).Item("lv5").ToString() %></span></td>
                                                                        <td class="text-center"><span class="pre-line"><%= detailtable.Rows(j).Item("lv4").ToString() %></span></td>
                                                                        <td class="text-center"><span class="pre-line"><%= detailtable.Rows(j).Item("lv3").ToString() %></span></td>
                                                                        <td class="text-center"><span class="pre-line"><%= detailtable.Rows(j).Item("lv2").ToString() %></span></td>
                                                                        <td class="text-center"><span class="pre-line"><%= detailtable.Rows(j).Item("lv1").ToString() %></span></td>
                                                                        <td class="text-center">
                                                                            <div class="deletedetail">
                                                                                <a onclick="confirmDeletedetail('<%= detailtable.Rows(j).Item("kpi_code").ToString() %>','<%= detailtable.Rows(j).Item("status").ToString() %>');" class="btn btn-sm p-0 notPrint">
                                                                                    <i class="fas fa-times"></i>
                                                                                </a>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <% End If %>
                                                                    <% Next j %>
                                                                </tbody>
                                                                <tfoot>
                                                                    <tr>
                                                                        <td class="h6 text-right font-weight-bold" colspan="2">น้ำหนักรวม</td>
                                                                        <td class="h6 text-center font-weight-bold"><span class="total_weight"></span></td>
                                                                        <td colspan="7"></td>
                                                                    </tr>
                                                                </tfoot>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>

                                                <%-- <% else %>
                                                <div class="row">
                                                    <div class="col mb-5">
                                                        <div class="row">
                                                            <div class="col mb-3 h5 font-weight-bold">
                                                                <span><%= groupdetailtable.Rows(i).Item("CategoryName").ToString() %></span>
                                                        </div>
                                                    </div>
                                                    <div class="row  bg-light">
                                                        <div class="col pl-5">
                                                            ไม่มี % ใน Ratio
                                                        </div>
                                                    </div>
                                                    </div>
                                                </div>--%>
                                                <% Next i %>
                                                <% else %>
                                                <div class="row bg-light">
                                                    <div class="col mb-2 pl-5 txt">
                                                        ยังไม่มีรายการ
                                                    </div>
                                                </div>

                                                <% End If %>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-footer">
                                    <div class="row">
                                        <div class="col">
                                            <a href="#" id="btnAddNewkpi" runat="server" class="text-primary" style="cursor: pointer; transition: .2s;" data-toggle="modal" data-target="#exampleModal" data-backdrop="static" data-keyboard="false" data-whatever="new">
                                                <i class="fas fa-plus-circle"></i><span>&nbsp;เพิ่มรายการ</span></a>
                                        </div>
                                    </div>
                                    <div class="row">

                                        <% If Not Request.QueryString("NewKpiCode") Is Nothing And maintable.Rows.Count > 0 Then%>
                                        <% if (maintable.Rows(0).Item("statusid") = 2) Then%>
                                        <div class="text-center m-auto">
                                            <% If approval And maintable.Rows(0).Item("statusid") = 2 Then%>
                                            <asp:Button ID="btnApproval" class="btn btn-success" runat="server" Text="อนุมัติ" />
                                            <% End If %>
                                            <% If ((verify Or approval)) Then%>
                                            <asp:Button ID="btnSendback" class="btn btn-danger" runat="server" Text="ส่งแก้ไข" />
                                            <% End If %>
                                        </div>
                                        <% End If %>
                                        <% End If %>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row notPrint" id="card_attatch" runat="server">
                        <div class="col-md-12 mt-3" id="card_comment" runat="server">
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
        </div>
    </div>
    <div class="modal fade " id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">รายละเอียดรายการ</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body p-4">
                    <!--  ##############  Detail ############### -->

                    <input type="hidden" class="form-control" id="row" value="0" runat="server">
                    <div class="row">
                        <div class="col mb-3">
                            <div class="form-group">
                                <asp:Label ID="Label10" CssClass="form-label" AssociatedControlID="cboRatio" runat="server" Text="ประเภท" />
                                <asp:Label ID="lbMandatorycboMyfile" CssClass="text-danger" AssociatedControlID="cboRatio" runat="server" Text="*" />
                                <asp:DropDownList class="form-control" ID="cboRatio" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col mb-3">
                            <div class="form-group">
                                <asp:Label ID="Label11" CssClass="form-label" AssociatedControlID="txtKpititle" runat="server" Text="หัวข้อ" />
                                <asp:Label ID="Label12" CssClass="text-danger" AssociatedControlID="txtKpititle" runat="server" Text="*" />
                                <textarea rows="2" cols="30" class="form-control" name="actiontitle" id="txtKpititle" runat="server"></textarea>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-6 mb-3">
                            <div class="form-group">
                                <asp:Label ID="Label13" CssClass="form-label" AssociatedControlID="txtWeight" runat="server" Text="น้ำหนัก" />
                                <asp:Label ID="Label14" CssClass="text-danger" AssociatedControlID="txtWeight" runat="server" Text="*" />
                                <asp:TextBox class="form-control" type="number" ID="txtWeight" runat="server" Text="1" step="1" min="1" max="100"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-6 mb-3">
                            <div class="form-group">
                                <asp:Label ID="Label15" CssClass="form-label" AssociatedControlID="txtUnit" runat="server" Text="หน่วย" />
                                <asp:Label ID="Label16" CssClass="text-danger" AssociatedControlID="txtUnit" runat="server" Text="*" />
                                <asp:TextBox class="form-control" type="input" ID="txtUnit" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <h4 class="mb-3">ระดับประเมิน</h4>
                    <div class="row">
                        <div class="col mb-3">
                            <div class="form-group">
                                <asp:Label ID="Label17" CssClass="form-label" AssociatedControlID="txtlv5" runat="server" Text="ระดับ 5" />
                                <asp:Label ID="Label18" CssClass="text-danger" AssociatedControlID="txtlv5" runat="server" Text="*" />
                                <asp:TextBox class="form-control" type="input" ID="txtlv5" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col mb-3">
                            <div class="form-group">
                                <asp:Label ID="Label19" CssClass="form-label" AssociatedControlID="txtlv4" runat="server" Text="ระดับ 4" />
                                <asp:Label ID="Label20" CssClass="text-danger" AssociatedControlID="txtlv4" runat="server" Text="*" />
                                <asp:TextBox class="form-control" type="input" ID="txtlv4" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col mb-3">
                            <div class="form-group">
                                <asp:Label ID="Label21" CssClass="form-label" AssociatedControlID="txtlv3" runat="server" Text="ระดับ 3" />
                                <asp:Label ID="Label22" CssClass="text-danger" AssociatedControlID="txtlv3" runat="server" Text="*" />
                                <asp:TextBox class="form-control" type="input" ID="txtlv3" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col mb-3">
                            <div class="form-group">
                                <asp:Label ID="Label23" CssClass="form-label" AssociatedControlID="txtlv2" runat="server" Text="ระดับ 2" />
                                <asp:Label ID="Label24" CssClass="text-danger" AssociatedControlID="txtlv2" runat="server" Text="*" />
                                <asp:TextBox class="form-control" type="input" ID="txtlv2" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col mb-3">
                            <div class="form-group">
                                <asp:Label ID="Label25" CssClass="form-label" AssociatedControlID="txtlv1" runat="server" Text="ระดับ 1" />
                                <asp:Label ID="Label26" CssClass="text-danger" AssociatedControlID="txtlv1" runat="server" Text="*" />
                                <asp:TextBox class="form-control" type="input" ID="txtlv1" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary noEnterSubmit" data-dismiss="modal">Close</button>
                    <%--<button type="button" id="btnAddDetail" class="btn btn-primary noEnterSubmit">Save</button>--%>
                    <asp:Button ID="btnAddDetail" class="btn btn-primary" runat="server" Text="Save" OnClientClick="postBack_addDetail();" />
                </div>
            </div>
        </div>
    </div>
    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/NonPO.js")%>"></script>
    <script>

        var cntdetail =<% =chkunsave%>;
        $(document).ready(function () {
            console.log(document)
            $('.form-control').selectpicker({
                noneSelectedText: '-',
                liveSearch: true,
                maxOptions: 1
            });
            $('.form-control').selectpicker('refresh');


            $('.DetailArea tr').each(function (index, tr) {
                console.log(index);
                console.log($(this).attr("data-status"));
                if ($(this).attr("data-status") == "new" || $(this).attr("data-status") == "edit") {
                    $(this).css("background-color", "#d8d8d8");
                }

            });
            checkUnSave()
            calculateTotalWeights()

        });
        //function calTotal() {
        //    const tables = document.querySelectorAll('table');
        //    //console.log(tables);
        //    //console.log(typeof (tables));
        //    //console.log(tables[0].getElementsByTagName('tbody')[0].rows);
        //    //    const tableBodyRows = tables[0].getElementsByTagName('tbody')[0].rows;
        //    tables.forEach(table => {
        //        const allTheValues = []
        //        const tbodys = [];
        //        let total = 0
        //        tbodys.push(table.getElementsByTagName('tbody')[0].rows);
        //        //console.log(tbodys);
        //        //console.log(tbodys[0].length);
        //        if (tbodys[0].length > 0) {
        //            for (let i = 0; i < tbodys[0].length; i++) {
        //                allTheValues.push(parseInt(tbodys[0][i].cells[2].innerHTML))


        //            }
        //            total = allTheValues.reduce((acc, val) => acc + val, 0);
        //            //console.log(`333333333`);
        //            //console.log(`${total}`);
        //        }
        //        const elemratiotypename = table.parentNode.parentNode.parentNode.getElementsByClassName("ratio__type")[0];
        //        const elemTotal = table.getElementsByTagName('tfoot')[0].rows[0].cells[1];
        //        elemTotal.innerHTML = total;
        //        if (total < 100) {
        //            elemTotal.classList.add("text-muted");
        //        }
        //        else if (total > 100) {
        //            elemTotal.classList.add("text-danger");

        //            //set ratioType text red
        //            elemratiotypename.classList.add("text-danger");
        //            elemratiotypename.title = "Have a problem";
        //        }

        //    });
        //}

        function calculateTotalWeights() {
            // ดึงตารางทั้งหมดที่มีในหน้า
            const tables = document.querySelectorAll('.table-responsive-xl');

            // วนลูปผ่านแต่ละตาราง
            tables.forEach(table => {
                let totalWeight = 0;

                // ค้นหาแถวใน tbody ที่มี class 'DetailArea'
                const rows = table.querySelectorAll('tbody.DetailArea tr');

                // วนลูปแต่ละแถวเพื่อดึงค่า weight
                rows.forEach(row => {
                    const weightCell = row.querySelector('td:nth-child(3) .pre-line'); // คอลัมน์ที่ 3 สำหรับ weight
                    if (weightCell) {
                        const weight = parseFloat(weightCell.textContent.trim());
                        if (!isNaN(weight)) {
                            totalWeight += weight;
                        }
                    }
                });

                // อัปเดตค่าใน <span class="total_weight"></span>
                const totalWeightSpan = table.querySelector('.total_weight');
                if (totalWeightSpan) {
                    totalWeightSpan.textContent = totalWeight; 
                    totalWeightSpan.classList.add(totalWeight <= 100 ?"text-muted":"text-danger");
                }
            });
        }

        function postBack_addDetail() {
            let kpicode = $('#<%= row.ClientID%>').val();
            const preriodid = $('#<%= cboPeriod.ClientID%>').val();
            const categoryid = $('#<%= cboRatio.ClientID%>').val();
            const categoryname = $("#<%= cboRatio.ClientID%> option:selected").text();
            const txtKpititle = $('#<%= txtKpititle.ClientID%>').val();
            const txtWeight = $('#<%= txtWeight.ClientID%>').val();
            const txtUnit = $('#<%= txtUnit.ClientID%>').val();
            const txtlv5 = $('#<%= txtlv5.ClientID%>').val();
            const txtlv4 = $('#<%= txtlv4.ClientID%>').val();
            const txtlv3 = $('#<%= txtlv3.ClientID%>').val();
            const txtlv2 = $('#<%= txtlv2.ClientID%>').val();
            const txtlv1 = $('#<%= txtlv1.ClientID%>').val();

            const status = $(".DetailArea tr[name='" + kpicode + "']").attr("data-status");

            if ((categoryid == 0 || categoryid === "")) {
                alertWarning('กรุณากรอกข้อมูลให้ครบถ้วน' + `ประเภท`);
                event.preventDefault();
                event.stopPropagation();
                return 0;
            }
            if (!txtKpititle) {
                alertWarning('กรุณากรอกข้อมูลให้ครบถ้วน' + ` หัวข้อ`);
                event.preventDefault();
                event.stopPropagation();
                return 0;
            }
            if ((txtWeight == 0 || txtWeight === "")) {
                alertWarning('กรุณากรอกข้อมูลให้ครบถ้วน' + ` น้ำหนัก`);
                event.preventDefault();
                event.stopPropagation();
                return 0;
            }
            if ((parseInt(txtWeight) <= 0 || parseInt(txtWeight) > 100)) {
                alertWarning(`Weight 0-100`);
                event.preventDefault();
                event.stopPropagation();
                return 0;
            }
            if (!txtUnit) {
                alertWarning('กรุณากรอกข้อมูลให้ครบถ้วน' + ` หน่วย`);
                event.preventDefault();
                event.stopPropagation();
                return 0;
            }
            if (!txtlv5) {
                alertWarning('กรุณากรอกข้อมูลให้ครบถ้วน' + ` Lv5`);
                event.preventDefault();
                event.stopPropagation();
                return 0;
            }
            if (!txtlv4) {
                alertWarning('กรุณากรอกข้อมูลให้ครบถ้วน' + ` Lv4`);
                event.preventDefault();
                event.stopPropagation();
                return 0;
            }
            if (!txtlv3) {
                alertWarning('กรุณากรอกข้อมูลให้ครบถ้วน' + ` Lv3`);
                event.preventDefault();
                event.stopPropagation();
                return 0;
            } if (!txtlv2) {
                alertWarning('กรุณากรอกข้อมูลให้ครบถ้วน' + ` Lv2`);
                event.preventDefault();
                event.stopPropagation();
                return 0;
            }
            if (!txtlv1) {
                alertWarning('กรุณากรอกข้อมูลให้ครบถ้วน' + ` Lv1`);
                event.preventDefault();
                event.stopPropagation();
                return 0;
            }


            var params = `{`
                + `'preriodid': '${preriodid}'`
                + `,'status': '${status}'`
                + `,'kpicode': '${kpicode}'`
                + `,'categoryid': '${categoryid}'`
                + `,'categoryname': '${categoryname}'`
                + `,'txtkpititle': '${txtKpititle}'`
                + `,'txtweight': '${txtWeight}'`
                + `,'txtunit': '${txtUnit}'`
                + `,'txtlv5': '${txtlv5}'`
                + `,'txtlv4': '${txtlv4}'`
                + `,'txtlv3': '${txtlv3}'`
                + `,'txtlv2': '${txtlv2}'`
                + `,'txtlv1': '${txtlv1}'`
                + `}`;


            removeElem("addDetailJSON");
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "addDetailJSON";
            confirm_value.value = params;
            document.forms[0].appendChild(confirm_value);
            showBtnSpiner(document.getElementById("<%= btnAddDetail.ClientID%>"));

            return true;

        }
        function clearfromadddetail() {


            $('.form-control').selectpicker('refresh');
            console.log("clearfromadddetail");

            $('#<%= cboRatio.ClientID%>').val(0);
            $('#<%= row.ClientID%>').val('');
            $('#<%= txtKpititle.ClientID%>').val('');
            $('#<%= txtWeight.ClientID%>').val('0');
            $('#<%= txtUnit.ClientID%>').val('');
            $('#<%= txtlv5.ClientID%>').val('');
            $('#<%= txtlv4.ClientID%>').val('');
            $('#<%= txtlv3.ClientID%>').val('');
            $('#<%= txtlv2.ClientID%>').val('');
            $('#<%= txtlv1.ClientID%>').val('');

            $('.form-control').selectpicker('refresh');
        }
        $('#<% =btnAddNewkpi.ClientID%>').click(function () {
            $('#exampleModal .modal-footer #btnAddDetail,#exampleModal .text-danger').show();
            $('#exampleModal .modal-body input,#exampleModal .modal-body textarea').removeAttr("readonly");
            $('#exampleModal .modal-body select,#exampleModal .modal-body button,#exampleModal .modal-body input[type="checkbox"]').removeAttr("disabled");

            $('.form-control').selectpicker('refresh');

            clearfromadddetail();

        });
        function checkUnSave() {

            const urlParams = new URLSearchParams(window.location.search);
            if (cntdetail == 1) {
                $(".nonpounsaved").show();
            } else {
                $(".nonpounsaved").hide();
            }
        }

        function selectElement(id, valueToSelect) {
            let element = document.getElementById(id);
            element.value = valueToSelect;
        }
        function btnEditDetailClick(kpicode, cateid, title, weight, unit, lv1, lv2, lv3, lv4, lv5) {



            const cate = '<%= cboRatio.ClientID%>';
            <%--const vendor = '<%= cboVendor.ClientID%>';--%>
            $('#exampleModal').modal('show');

            selectElement(cate, cateid);

            $('#<%= row.ClientID%>').val(kpicode);
            $('#<%= txtKpititle.ClientID%>').val(title);
            $('#<%= txtWeight.ClientID%>').val(weight);
            $('#<%= txtUnit.ClientID%>').val(unit);

            $('#<%= txtlv1.ClientID%>').val(lv1);
            $('#<%= txtlv2.ClientID%>').val(lv2);
            $('#<%= txtlv3.ClientID%>').val(lv3);
            $('#<%= txtlv4.ClientID%>').val(lv4);
            $('#<%= txtlv5.ClientID%>').val(lv5);

            $('.form-control').selectpicker('refresh');



            <% If Not Request.QueryString("NewKpiCode") Is Nothing And maintable.Rows.Count > 0 Then%>
            <% If ((Session("userid") = maintable.Rows(0).Item("ownerid") And maintable.Rows(0).Item("statusid") <> 1) Or Not Session("userid") = maintable.Rows(0).Item("ownerid")) Then%>
            $('#exampleModal .modal-footer #btnAddDetail,#exampleModal .text-danger').hide();
            $('#exampleModal .modal-body input,#exampleModal .modal-body textarea').attr('readonly', true);
            $('#exampleModal .modal-body select,#exampleModal .modal-body button,#exampleModal .modal-body input[type="checkbox"]').attr('disabled', true);
            <% End If %>
            <% End If %>
        }
        function confirmDeletedetail(kpicode, status) {
            Swal.fire({
                title: 'คุุณต้องการจะลบข้อมุลนี้ใช่หรือไม่ ?',
                text: "",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes',
                allowOutsideClick: false
            }).then((result) => {
                if (result.isConfirmed) {

                    var user ="<%=Session("usercode").ToString %>";
                    var params = "{'kpicode': '" + kpicode + "','status': '" + status + "','user': '" + user + "'}";

                    __doPostBack('deletedetail', params);
                }
            })

            return false;
        }

        function disbtndelete() {
            $(".deletedetail").hide();
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
