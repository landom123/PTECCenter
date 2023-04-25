<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="Jobs_MapsToNonPO.aspx.vb" Inherits="PTECCENTER.Jobs_MapsToNonPO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper">

        <!-- #include virtual ="/include/menu.inc" -->
        <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-6 mb-3">

                        <button runat="server" id="btnOpen" class="btn btn-sm  btn-primary rounded-circle d-none" title="Upload">
                            <i class="fa fa-file-upload"></i>
                        </button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm mb-3">

                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text="Save" />
                        &nbsp;              
                             <asp:Button ID="btnUpdate" class="btn btn-sm  btn-warning" runat="server" Text="Update" />
                        &nbsp;   
                            <asp:Button ID="btnConfirm" class="btn btn-sm  btn-secondary" runat="server" Text="Confirm" OnClientClick="Confirm();" />
                        &nbsp;   
                            <asp:Button ID="btnCancel" class="btn btn-sm  btn-danger" runat="server" Text="Cancel" />
                        <%-- &nbsp;   
                            <% If Not Request.QueryString("approvalHOcode") Is Nothing And maintable.Rows.Count > 0 Then%>
                        <% if (maintable.Rows(0).Item("statusid") = 1) Or (maintable.Rows(0).Item("statusid") = 4) Then%>
                        <span class="text-red font-weight-bold text-danger">*** (กรุณากด confirm เพื่อยืนยัน) ***</span>
                        <% End If %>
                        <% End If %>--%>
                    </div>

                    <div class="col-sm-auto mb-3 text-lg-right text-sm-left align-self-center">
                        <button type="button" class="btn btn-sm  btn-info noEnterSubmit" style="color: #495057;" title="Export" id="btnExport" runat="server"><i class="fas fa-file-download"></i></button>
                        &nbsp;
                        <a href="ApprovalHOMenuList.aspx" class="btn btn-sm btn-danger ">
                            <i class="fa fa-tasks" aria-hidden="true"></i></a>
                    </div>

                </div>

                <div class="row">
                    <div class="col-12 mb-3">
                        <div class="card shadow">
                            <div class="card-body">
                                <div class="row justify-content-between">
                                    <div class="col-4 my-auto d-none d-md-block">
                                        <asp:TextBox class="form-control text-center font-weight-bold" ID="txtHeadDetail" runat="server" placeholder="กรอกวัตถุประสงค์ . . . " ToolTip="วัตถุประสงค์"></asp:TextBox>
                                        <asp:Label ID="lbHeadDetail" CssClass="form-label" runat="server" Text="" />
                                    </div>
                                    <div class="col statusnonpo d-flex justify-content-end align-self-center" style="/*display: none; */">
                                        <div class="badgestatus p-1 pl-4 pr-4" runat="server" id="badgestatus">
                                            <div class="row flex-column text-white">
                                                <div class="col text-right ">
                                                    <asp:Label class="font-weight-bold" Font-Size="Small" ID="statusnonpo" runat="server" ToolTip="ป้ายสถานะ" ReadOnly="true">ยังไม่บันทึก</asp:Label>
                                                </div>
                                                <div class="col">
                                                    <asp:Label class="font-weight-bold" ID="txtApprovalHOcode" runat="server" ReadOnly="True"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="table-responsive">
                                    <asp:GridView ID="gvData"
                                        class="table thead-dark table-bordered"
                                        AllowSorting="True"
                                        AllowPaging="false"
                                        AutoGenerateColumns="false"
                                        runat="server">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-CssClass="text-center table-header" ItemStyle-Width="50px" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server"
                                                        onclick="checkAll(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" data-key='<%#Eval("id").ToString + "," + Eval("approvalcode").ToString%>'
                                                        onclick="Check_Click(this)" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="id" HeaderStyle-CssClass="table-header d-none" ItemStyle-CssClass="d-none">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbid" runat="server" Text='<%#Eval("id")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="approval" HeaderStyle-CssClass="table-header" ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbapproval" runat="server" Text='<%#Eval("approval")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="branch" HeaderStyle-CssClass="table-header" ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbbranch" runat="server" Text='<%#Eval("branch")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="detail" HeaderStyle-CssClass="table-header">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbdetail" runat="server" Text='<%#Eval("detail")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="cost" HeaderStyle-CssClass="table-header" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbcost" runat="server" Text='<%#String.Format("{0:n2}", Eval("cost"))%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="APP" HeaderStyle-CssClass="table-header">
                                                <ItemTemplate>
                                                    <div class="d-flex flex-column align-items-center">
                                                        <asp:Label CssClass="approvalcode" ID="lbapprovalcode" runat="server" Text='<%#Eval("approvalcode")%>'></asp:Label>
                                                        <a href="../approval/approval.aspx?approvalcode=<%#Eval("approvalcode")%>" class="badge badgestatus_app" title="<%#Eval("statusname")%>" target="_blank"><%#Eval("statusname")%></a>
                                                        <li class="breadcrumb-item " runat="server" visible='<%#Eval("approvalcode").ToString() = HttpContext.Current.Profile.UserName %>'>
                                                            <a href="#" class="badge badgestatus_cladv text-muted " title="">ยังไม่ได้บันทึกรายการนี้</a>
                                                        </li>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CLADV" HeaderStyle-CssClass="table-header">
                                                <ItemTemplate>
                                                    <div class="d-flex flex-column align-items-center">
                                                        <asp:Label CssClass="approvalcode" ID="lbcladvcode" runat="server" Text='<%#Eval("cladvcode")%>'></asp:Label>
                                                        <a href="../Non-PO/Advance/ClearAdvance.aspx?NonpoCode=<%#Eval("cladvcode")%>" target="_blank" class="badge badgestatus_cladv" title="<%#Eval("cladvstatus")%>"><%#Eval("cladvstatus")%></a>
                                                        <li class="breadcrumb-item " runat="server" visible='<%#Eval("cladvcode").ToString() = HttpContext.Current.Profile.UserName %>'>
                                                            <a href="#" class="badge badgestatus_cladv text-muted " title="">ยังไม่มีใบ CLADV</a>
                                                        </li>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                                </div>

                            </div>
                            <div class="card-footer">
                                <div class="row text-muted align-items-center justify-content-between">
                                    <div class="col-auto" runat="server" id="groupFromAddDetail">
                                        <div class="row">
                                            <div class="col pr-0 mb-3">
                                                <div class="file-upload mr-2">
                                                    <asp:FileUpload ID="FileUpload1" class="" onchange="UploadFile(this)" runat="server" text="เลือกไฟล์" accept=".xlsx" />
                                                    <label class="btn btn-sm  btn-info rounded-circle m-0" for="FileUpload1" runat="server"><i class="fa fa-file-upload"></i></label>
                                                </div>
                                            </div>
                                            <div class="col p-0 mb-3">

                                                <button type="button" class="btn btn-sm btn-primary rounded-circle mr-2" id="btnFromAddDetail" style="width: 35px" runat="server" title="Add" data-toggle="modal" data-target="#exampleModal" data-backdrop="static" data-keyboard="false" data-whatever="new">+</button>
                                            </div>
                                            <div class="col p-0 mb-3">
                                                <asp:Button ID="btnDelete" class="btn btn-sm  btn-danger rounded-circle mr-2" runat="server" Width="35px" Text="-" OnClientClick="return sendID();" title="Delete" />
                                            </div>
                                        </div>
                                        <%--<button type="button" class="btn btn-sm  btn-info noEnterSubmit" style="color: #495057;" title="Export" id="btnExport" onclick="sendID()"><i class="fas fa-file-download"></i></button>--%>
                                    </div>
                                    <div class="col text-center d-flex align-items-center justify-content-end " style="font-size: .8rem;">
                                        <div class="row">
                                            <div class="col-lg-auto mb-3">
                                                <div class="verifyby confirm d-none" runat="server" id="verifyby">
                                                    <div class="confirm__icon content__verify">
                                                    </div>
                                                    <div class="confirm__detail bg__purple">
                                                        <div class="confirm__innerdetail">
                                                            <asp:Label ID="lbVerifyby" CssClass="form-label" runat="server" Text="" />
                                                            <asp:Label ID="lbVerifyDate" CssClass="form-label" runat="server" Text="" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-auto mb-3">
                                                <div class="approvalby confirm d-none" runat="server" id="approvalby">
                                                    <div class="confirm__icon content__approval">
                                                    </div>
                                                    <div class="confirm__detail bg__green">
                                                        <div class="confirm__innerdetail">
                                                            <asp:Label ID="lbApprovalby" CssClass="form-label" runat="server" Text="" />
                                                            <asp:Label ID="lbApprovalDate" CssClass="form-label" runat="server" Text="" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-auto mb-3">
                                                <div class="confirmby confirm d-none" runat="server" id="confirmby">
                                                    <div class="content__confirm">
                                                    </div>
                                                    <div class="confirm__detail bg__navy">
                                                        <div class="confirm__innerdetail">
                                                            <asp:Label ID="lbConfirmby" CssClass="form-label" runat="server" Text="" />
                                                            <asp:Label ID="lbConfirmDate" CssClass="form-label" runat="server" Text="" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-auto mb-3">
                                                <div class="createby">
                                                    <div>
                                                        <asp:Label ID="lbCreateby" CssClass="form-label" runat="server" Text="" />
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lbCreateDate" CssClass="form-label" runat="server" Text="" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <hr />
            </div>
        </div>
        <!-- end content-wrapper -->


        <!-- end เนื้อหาเว็บ -->


    </div>
    <!-- /#wrapper -->

    <div class="modal fade bd-example-modal-lg" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">รายละเอียดรายการ</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="lbcboApproval" CssClass="form-label" AssociatedControlID="cboApproval" runat="server" Text="หัวข้อขออนุมัติ" />
                                <asp:Label ID="lbcboApprovalMandatory" CssClass="text-danger" AssociatedControlID="cboApproval" runat="server" Text="*" />
                                <asp:DropDownList class="form-control" ID="cboApproval" runat="server" required></asp:DropDownList>
                                <div class="invalid-feedback">กรุณาเลือกหัวข้อขออณุมัติ</div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lbcboBranch" CssClass="form-label" AssociatedControlID="cboBranch" runat="server" Text="สาขา" />
                                <asp:Label ID="lbcboBranchMandatory" CssClass="text-danger" AssociatedControlID="cboBranch" runat="server" Text="*" />
                                <asp:DropDownList class="form-control" ID="cboBranch" runat="server" required></asp:DropDownList>
                                <div class="invalid-feedback">กรุณาเลือกสาขา</div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lbPrice" CssClass="form-label" AssociatedControlID="txtPrice" runat="server" Text="ค่าใช้จ่าย" />
                                <asp:TextBox class="form-control" type="number" ID="txtPrice" runat="server" min="0" Text="0" required></asp:TextBox>
                                <div class="invalid-feedback">* ตัวเลขจำนวนเต็ม</div>
                            </div>
                        </div>
                    </div>
                    <div class="row justify-content-md-center file_Approval_BF d-none">
                        <div class="col-md-10">
                            <input type="file" name="files" accept="image/*,.pdf" data-fileuploader-files=''>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="lbDetail" CssClass="form-label" AssociatedControlID="txtDetail" runat="server" Text="รายละเอียด" />
                                <asp:Label ID="lbDetailMandatory" CssClass="text-danger" AssociatedControlID="txtDetail" runat="server" Text="*" />
                                <asp:TextBox class="form-control" ID="txtDetail" runat="server" Rows="3" Columns="50" TextMode="MultiLine" onkeyDown="checkTextAreaMaxLength(this,event,'255');" required></asp:TextBox>
                                <div class="invalid-feedback">กรุณากรอกรายละเอียด</div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <%--<button type="button" id="btnAddDetail" class="btn btn-primary noEnterSubmit">Save</button>--%>
                    <asp:Button ID="btnAddDetailsss" class="btn btn-primary" runat="server" Text="Save" OnClientClick="return postBack_addDetails();"/>

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
    <%--    <div class="row btn-operator justify-content-center notPrint">
        <% If Not Request.QueryString("approvalHOcode") Is Nothing Then%> 
        <% If maintable.Rows(0).Item("statusid") = 2 And account_code.IndexOf(Session("usercode").ToString) > -1 Then%>
        <!-- 2 = รออนุมัติ-->
        <button class="btn btn-sm " style="color: #39cd5b; font-size: 3rem; position: fixed; bottom: 9rem; right: 1rem;" id="btnPass" runat="server" title="ผ่านการตรวจสอบจาก บช.">
            <i class="fas fa-check-circle shadow" style="border-radius: 100%;"></i>
        </button>
        <button class="btn btn-sm " style="color: #b8c5d1; font-size: 3rem; position: fixed; bottom: 5rem; right: 1rem;" id="btnEdit" runat="server" title="ตีกลับ">
            <i class="fas fa-pause-circle shadow" style="border-radius: 100%;"></i>
        </button>
        <button class="btn btn-sm " style="color: #dc3545; font-size: 3rem; position: fixed; bottom: 1rem; right: 1rem;" id="btnReject" runat="server" title="ยกเลิกใบงาน">
            <i class="fas fa-times-circle" style="border-radius: 100%;"></i>
        </button>
        <% End If %>
        <% End If %>
    </div>--%>


    <script src="<%=Page.ResolveUrl("~/js/NonPO.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            //alert('t');
            $('input[name="files"]').fileuploader({
                example: ['pdf', 'image/*'],
                fileMaxSize: 15,
                limit: 3,
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
                    /*alert(listEl);
                    console.log(listEl.value);
                    console.log($('#fileuploader-list-files').value);
                    console.log($('#fileuploader-list-files').val.length);
                    console.log("parentEl : " + parentEl);
                    console.log(parentEl);
                    console.log("newInputEl : " + newInputEl);
                    console.log(newInputEl);
                    console.log("input : " + inputEl);
                    console.log(inputEl.length);*/

                    plusInput.on('click', function () {
                        api.open();
                    });

                    api.getOptions().dragDrop.container = plusInput;
                }
            });


            $('.form-control').selectpicker({
                //showTick: true,
                liveSearch: true,
                maxOptions: 1
            });
           <%-- const urlParams = new URLSearchParams(window.location.search);
            const approvalcode = urlParams.get('approvalcode');
            console.log(approvalcode);

            if (approvalcode) {
                var asd = '<%= ownerapproval.ToString %>'

                document.getElementById("demo2").innerHTML = "(" + asd + ")";
            }

            var status = document.getElementById('<%= txtStatus.ClientID%>');
            var grid = document.getElementById('grid'); //สำหรับแสดงผล


            var Approval_BF = '<%= Approval_BF.ToString %>'
            var Approval_AT = '<%= Approval_AT.ToString %>'
            var Approval_Bill = '<%= Approval_Bill.ToString %>'
            var Approval_Doc = '<%= Approval_Doc.ToString %>'
            var status_session = "<%= Session("status")%>"

            if (status_session != "new") {
                if (!Approval_BF) {
                    //ไม่มี
                    $('.file_Approval_BF').hide();
                } else {
                    //มี
                    $('.fileuploader-thumbnails-input').css('display', 'none');
                }


                if (!Approval_Doc && !Approval_AT && !Approval_Bill) {
                    $('.files_approval').hide();
                    grid.setAttribute("class", "col-xl-12 mb-3");
                }
                else {
                    grid.setAttribute("class", "col-xl-8 mb-3");
                    if (!Approval_AT) {
                        //ไม่มี
                        $('.file_Approval_AT').hide();
                    } else {
                        //มี
                        $('.fileuploader-thumbnails-input').css('display', 'none');
                    }
                    if (!Approval_Bill) {
                        //ไม่มี
                        $('.file_Approval_Bill').hide();
                    } else {
                        //มี
                        $('.fileuploader-thumbnails-input').css('display', 'none');
                    }
                    if (!Approval_Doc) {
                        //ไม่มี
                        $('.file_Approval_Doc').hide();
                    } else {
                        //มี
                        $('.fileuploader-thumbnails-input').css('display', 'none');
                    }
                }



                $('.content-holder').css('display', 'none');
                $('.type-holder').css('display', 'none');
                $('.actions-holder').css('display', 'none');

            }--%>

            /*console.log(elem)
            if (approvalcode && elem.getAttribute('src') === "#") {
                console.log(elem.getAttribute('src') === "#")
                console.log(approvalcode)

                $('.image-upload-wrap').hide();
                $('.image-title-wrap').hide();
                $('.file-upload-content').hide();

            }
            if (elem.getAttribute('src') != "#") {
                console.log("inif")

                $('.image-upload-wrap').hide();
                $('.image-title-wrap').hide();
                $('.file-upload-content').show();
            }
            if (status.value == "ปิดงาน") {
                console.log("ปิดงาน")
                console.log(img_after.getAttribute('src'))
                console.log(img_bil.getAttribute('src'))

                if (img_after.getAttribute('src') != "#") {
                    console.log("inif1")

                    $('.file-img-after').show();
                } else {
                    console.log("else inif1")

                    $('.file-img-after').hide();
                }

                if (img_bil.getAttribute('src') != "#") {
                    console.log("inif2")

                    $('.file-img-bill').show();
                } else {
                    console.log("else inif2")

                    $('.file-img-bill').hide();
                }
            }*/
            //$('input').on('change', function () {
            //    resizeImages(this.files[0], function (dataUrl) {

            //        // image is now a resized dataURL.  This can be sent up to the server using ajax where it can be recompiled into an image and stored.
            //        ////// 5 Upload to server as dataUrl
            //        //uploadResizedImages(dataUrl);

            //        //console.log(dataUrl.length)
            //    });
            //});

            const arrs_app = document.querySelectorAll('.badgestatus_app');

            for (let i = 0; i < arrs_app.length; i++) {
                let st_name = arrs_app[i].textContent;
                switch (st_name) {
                    case "รอผู้แจ้งยืนยัน":
                        arrs_app[i].style.backgroundColor = "LightBlue";
                        break;
                    case "ยกเลิกการแจ้ง":
                        arrs_app[i].style.backgroundColor = "LightGray";
                        break;
                    case "รออนุมัติ":
                        arrs_app[i].style.backgroundColor = "LightYellow";
                        break;
                    case "อนุมัติ":
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
                    case "รอแสกนเอกสาร":
                        arrs_app[i].style.backgroundColor = "Brown";

                        break;
                    case "เอกสารครบถ้วน":
                        arrs_app[i].style.backgroundColor = "MediumPurple";
                }
                arrs_app[i].style.color = "#ececec";

            }

            const arrs_cladv = document.querySelectorAll('.badgestatus_cladv');

            for (let i = 0; i < arrs_cladv.length; i++) {
                let st_name = arrs_cladv[i].textContent;
                switch (st_name) {
                    case "รอยืนยัน":
                        arrs_cladv[i].style.backgroundColor = "LightBlue";
                        break;
                    case "ยกเลิก":
                        arrs_cladv[i].style.backgroundColor = "LightGray";
                        break;
                    case "รอตรวจสอบ":
                        arrs_cladv[i].style.backgroundColor = "LightGoldenrodYellow";
                        break;
                    case "รออนุมัติ":
                        arrs_cladv[i].style.backgroundColor = "LightYellow";
                        break;
                    case "ชำระเงินเสร็จสิ้น":
                        arrs_cladv[i].style.backgroundColor = "GreenYellow";
                        break;
                    case "ไม่ผ่านการอนุมัติ":
                        arrs_cladv[i].style.backgroundColor = "IndianRed";
                        break;
                    case "รอการเงินตรวจสอบ":
                        arrs_cladv[i].style.backgroundColor = "LightCoral";
                        break;
                    case "รอบัญชีตรวจสอบ":
                        arrs_cladv[i].style.backgroundColor = "LightSalmon";
                        break;
                    case "รอเคลียร์ค้างชำระ":
                        arrs_cladv[i].style.backgroundColor = "Brown";
                        break;
                    case "ขอเอกสารเพิ่มเติม":
                        arrs_cladv[i].style.backgroundColor = "MediumPurple";
                        break;
                    case "ได้รับเอกสารตัวจริง":
                        arrs_cladv[i].style.backgroundColor = "Gray";
                        break;
                    case "รอเอกสารตัวจริง":
                        arrs_cladv[i].style.backgroundColor = "Yellow";
                }
                arrs_cladv[i].style.color = "#ececec";
            }
        });

    </script>
    <script type="text/javascript">

        function alertSuccess(massage) {
            Swal.fire(
                massage,
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
        function Check_Click(objRef) {

            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode.parentNode;

            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            var headerCheckBox = inputList[0];
            var checked = true;
            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;
        }
        function checkAll(objRef) {
            let GridView = objRef.parentNode.parentNode.parentNode;
            let inputList = GridView.getElementsByTagName("input");
            for (let i = 0; i < inputList.length; i++) {
                let row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                        inputList[i].parentNode.parentNode.parentNode.classList.add("checked");

                    }
                    else {
                        /*if (row.rowIndex % 2 == 0) {
                            row.style.backgroundColor = "#C2D69B";
                        }
                        else {
                            row.style.backgroundColor = "white";
                        }*/
                        inputList[i].checked = false;
                        inputList[i].parentNode.parentNode.parentNode.classList.remove("checked");

                    }
                    //$cb.is(':checked') ? $(this).css('background-color', '#ececec') : $(this).css('background-color', '#ffffff');
                }
            }
        }
        function getSeleted() {
            //console.log("xxx22");
            let textinputs = document.querySelectorAll('td input:checked');

            //console.log(arrs);
            let arrs = [];
            for (let i = 0; i < textinputs.length; i++) {
                arrs[i] = textinputs[i].parentNode.getAttribute("data-key");

                //console.log(textinputs[i].parentNode);
                //console.log(textinputs[i].parentNode.getAttribute("data-key"));
            }
            //console.log(arrs);

            let arrsWithKey = arrs.map((arr) => {
                const myArray = arr.split(",");
                let fullname = `{"id":"${myArray[0]}","appcode":"${myArray[1]}"}`;
                return fullname;
            })
            //console.log(`arrsWithKey : ${arrsWithKey}`);
            //console.log(arrsWithKey);
            let params = arrsWithKey.reduce((txt, array) => {
                return txt + array + ',';
            }, "");

            let paramslength = params.length;
            if (params[paramslength - 1] === ',') {
                //console.log(`params sdad`);
                params = params.substring(0, params.length - 1);
            }
            params = `[${params}]`
            //console.log(params);
            return params;
        }
        function removeElem(nameele) {
            let elements = document.getElementsByName(nameele);
            //console.log(elements);
            //console.log("asdasd");
            for (let i = 0; i < elements.length; i++) {
                elements[i].remove();
            }
        }
        function sendID() {
            //console.log("xxx");
            let textinputs = document.querySelectorAll('td input:checked');
            const params = getSeleted();
            const sizeText = textinputs.length;
            //console.log("xxx");
            console.log(textinputs.length);

            removeElem("delete_value");

            let confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "delete_value";
            if (textinputs.length > 0) {
                if (confirm(`คุณต้องการจะลบรายการที่เลือกหรือไม่ (${sizeText}) ?`)) {
                    confirm_value.value = params;
                }
            }
            else {
                event.preventDefault();
                event.stopPropagation();
            }
            document.forms[0].appendChild(confirm_value);
            //console.log(confirm_value.value);
            return true;
        }
        function postBack_addDetails() {
            validateData();

            console.log("sdadad");

            <%--let row = $('#<%= row.ClientID%>').val();
            const nonpodtl_id = $('#<%= hiddenAdvancedetailid.ClientID%>').val();--%>
            const approvalid = $('#<%= cboApproval.ClientID%>').val();
            const approval = $("#<%= cboApproval.ClientID%> option:selected").text();
            const branchid = $('#<%= cboBranch.ClientID%>').val();
            const branch = $("#<%= cboBranch.ClientID%> option:selected").text();
            const cost = $('#<%= txtPrice.ClientID%>').val();
            const detail = $('#<%= txtDetail.ClientID%>').val();

            //alert('cost' + cost);

            //if (cost != 0 && accountcodeid == 0) {
            //    alertWarning('กรุณาเลือกรหัสบัญชี');
            //    event.preventDefault();
            //    event.stopPropagation();
            //    return 0;
            //}
            //if (vat != 0 && (!invoice || !taxid || !invoicedate)) {
            //    alertWarning('กรุณากรอกข้อมูล invoice ให้ครบถ้วน');
            //    event.preventDefault();
            //    event.stopPropagation();
            //    return 0;
            //}
            //alert(row);
            //var params = "{'row': '" + row + "'}";
            var params = "{'approvalid': '" + approvalid + "','approval': '" + approval +
                "','branchid': '" + branchid + "','branch': '" + branch +
                "','cost': '" + (cost == 0 ? 0.0 : cost) + "','detail': '" + detail + "'}";

            //PageMethods.addoreditdetail(params);
            console.log(approval);
            removeElem("addDetail");
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "addDetail";
            confirm_value.value = params;
            document.forms[0].appendChild(confirm_value);
            console.log(approval);

            __doPostBack('addDetails', '');
            return true;
        }
        $(".table tbody tr").click(function (e) {
            if ($(e.target).is(':checkbox')) return; //ignore when click on the checkbox

            var $cb = $(this).find(':checkbox');
            $cb.prop('checked', !$cb.is(':checked'));
            $cb.is(':checked') ? $(this).addClass("checked") : $(this).removeClass("checked");
            Check_Click(this)
        });
        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnOpen.ClientID %>").click();
            }
        }
        function Confirm() {

            console.log("insave");

            removeElem("confirm_value");
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("คุณต้องการจะบันทึกหรือไม่ ?")) {
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
        function addAttach() {

            Swal.fire({
                title: 'แนบลิ้งเอกสาร',
                html:

                    '<input id="swal-input1" class="swal2-input form-control" type="url" placeholder="URL">' +
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
            const nonpocode = urlParams.get('approvalHOcode');
            var user = "<% =btnOpen.ClientID

#End ExternalSource

#ExternalSource ("D:\Work\รวมแล้วของพี่เล็ก\PTECCenter\PTECWEB\OPS\Jobs_MapsToNonPO.aspx", 167)
        __o = cboMyfile.ClientID

#End ExternalSource

#ExternalSource ("D:\Work\รวมแล้วของพี่เล็ก\PTECCenter\PTECWEB\OPS\Jobs_MapsToNonPO.aspx", 168)
        __o = cboMyfile.ClientID

#End ExternalSource

#ExternalSource ("D:\Work\รวมแล้วของพี่เล็ก\PTECCenter\PTECWEB\OPS\Jobs_MapsToNonPO.aspx", 169)
        __o = Session("usercode").ToString %>";
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

        function disbtndelete() {
            $(".deletedetail").hide();
        }
    </script>
</asp:Content>
