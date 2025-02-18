<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="approvalHO.aspx.vb" Inherits="PTECCENTER.approvalHO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Page.ResolveUrl("~/fileupload/dist/font/font-fileuploader.css")%>" rel="stylesheet">

    <link href="<%=Page.ResolveUrl("~/css/card_comment.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/fileupload/dist/jquery.fileuploader.min.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/fileupload/dist/jquery.fileuploader-theme-thumbnails.css")%>" rel="stylesheet">
    <style>
        .file-upload-content, .image-upload-wrap, .fileuploader-theme-thumbnails .fileuploader-items .fileuploader-item .fileuploader-action + .fileuploader-action, .fileuploader-popup .fileuploader-popup-footer .fileuploader-popup-tools li [data-action="remove"], .fileuploader-popup .fileuploader-popup-footer .fileuploader-popup-zoomer, .fileuploader-item-inner .type-holder, .fileuploader-item-inner .content-holder {
            display: none;
        }

        .fileuploader-theme-thumbnails .fileuploader-thumbnails-input-inner {
            background: #f0cccc;
            border: 2px dashed #ff0000;
            color: #ff0000;
        }

        .checked {
            background-color: #ececec;
        }

        .table-header {
            background-color: #01987a;
            color: #fff;
            text-align: center;
        }


        .file-upload {
            height: max-content;
            width: max-content;
            border-radius: 50%;
            position: relative;
            display: flex;
            justify-content: center;
            align-items: center;
            overflow: hidden;
            background-image: linear-gradient(to bottom, #2590EB 50%, #FFFFFF 50%);
            background-size: 100% 200%;
            transition: all 1s;
            color: #FFFFFF;
            font-size: 100px;
            cursor: pointer;
        }

            .file-upload label {
                width: 35px;
                cursor: pointer;
            }

        input[type='file'] {
            height: 200px;
            width: 200px;
            position: absolute;
            top: 0;
            left: 0;
            opacity: 0;
            cursor: pointer;
        }

        .approvalcode {
            font-size: 1rem;
        }

        .statusnonpo .badgestatus {
            border-top-left-radius: 10px;
            border-top-right-radius: 10px;
            border-bottom-left-radius: 0px;
            border-bottom-right-radius: 0px;
        }

        /* ///////////////////////////////////////////// */
        .confirm {
            --width: 150px;
            --height: calc(var(--width) / 3);
            --border-radius: calc(var(--height) / 2);
            display: inline-block;
        }

        .confirm__detail {
            position: relative;
            width: var(--width);
            height: var(--height);
            border-radius: var(--border-radius);
            transition: background 0.2s;
        }

        .confirm__icon {
            content: '';
            position: absolute;
            max-height: var(--height);
            max-width: var(--height);
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.25);
            border-radius: var(--border-radius);
            border: 0;
            transition: transform 0.2s;
            /* overflow: hidden; */
            z-index: 1;
        }

        .confirm__innerdetail {
            height: var(--height);
            width: calc((var(--width) /3)*2);
            word-break: break-word;
            margin-left: auto;
            margin-right: 0px;
            display: flex;
            align-items: center;
            justify-content: center;
            flex-direction: column;
        }

            .confirm__innerdetail span {
                color: whitesmoke;
                font-size: .65rem;
            }

        .bg__green {
            background: #01987A;
        }

        .bg__purple {
            background: #940073;
        }

        .bg__navy {
            background: #006794;
        }

        .content__verify {
            content: url("data:image/svg+xml,%3Csvg width='78' height='79' viewBox='0 0 78 79' fill='none' xmlns='http://www.w3.org/2000/svg'%3E%3Cg filter='url(%23filter0_d_0_1)'%3E%3Cellipse cx='37.5' cy='38' rx='37.5' ry='38' fill='%23DBCE59'/%3E%3C/g%3E%3Cpath d='M14 41.4118L27.4167 55L60 22' stroke='%23FFFAFA' stroke-width='10' stroke-linecap='round'/%3E%3Cdefs%3E%3Cfilter id='filter0_d_0_1' x='0' y='0' width='78' height='79' filterUnits='userSpaceOnUse' color-interpolation-filters='sRGB'%3E%3CfeFlood flood-opacity='0' result='BackgroundImageFix'/%3E%3CfeColorMatrix in='SourceAlpha' type='matrix' values='0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0' result='hardAlpha'/%3E%3CfeMorphology radius='1' operator='dilate' in='SourceAlpha' result='effect1_dropShadow_0_1'/%3E%3CfeOffset dx='2' dy='2'/%3E%3CfeComposite in2='hardAlpha' operator='out'/%3E%3CfeColorMatrix type='matrix' values='0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.25 0'/%3E%3CfeBlend mode='normal' in2='BackgroundImageFix' result='effect1_dropShadow_0_1'/%3E%3CfeBlend mode='normal' in='SourceGraphic' in2='effect1_dropShadow_0_1' result='shape'/%3E%3C/filter%3E%3C/defs%3E%3C/svg%3E%0A");
        }

        .content__approval {
            content: url("data:image/svg+xml,%3Csvg width='78' height='79' viewBox='0 0 78 79' fill='none' xmlns='http://www.w3.org/2000/svg'%3E%3Cg filter='url(%23filter0_d_0_1)'%3E%3Cellipse cx='37.5' cy='38' rx='37.5' ry='38' fill='%23ADE6E6'/%3E%3C/g%3E%3Cpath d='M14 41.4118L27.4167 55L60 22' stroke='%23FFFAFA' stroke-width='10' stroke-linecap='round'/%3E%3Cdefs%3E%3Cfilter id='filter0_d_0_1' x='0' y='0' width='78' height='79' filterUnits='userSpaceOnUse' color-interpolation-filters='sRGB'%3E%3CfeFlood flood-opacity='0' result='BackgroundImageFix'/%3E%3CfeColorMatrix in='SourceAlpha' type='matrix' values='0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0' result='hardAlpha'/%3E%3CfeMorphology radius='1' operator='dilate' in='SourceAlpha' result='effect1_dropShadow_0_1'/%3E%3CfeOffset dx='2' dy='2'/%3E%3CfeComposite in2='hardAlpha' operator='out'/%3E%3CfeColorMatrix type='matrix' values='0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.25 0'/%3E%3CfeBlend mode='normal' in2='BackgroundImageFix' result='effect1_dropShadow_0_1'/%3E%3CfeBlend mode='normal' in='SourceGraphic' in2='effect1_dropShadow_0_1' result='shape'/%3E%3C/filter%3E%3C/defs%3E%3C/svg%3E%0A");
        }

        .content__confirm {
            content: url("data:image/svg+xml,%3Csvg width='121' height='116' viewBox='0 0 121 116' fill='none' xmlns='http://www.w3.org/2000/svg'%3E%3Cg filter='url(%23filter0_d_0_1)'%3E%3Cpath d='M50.0384 8.19758C53.3312 -1.93684 67.6687 -1.93686 70.9616 8.19755L76.5528 25.4053C78.0254 29.9375 82.2489 33.0061 87.0144 33.0061H105.108C115.764 33.0061 120.194 46.6419 111.573 52.9053L96.9355 63.5403C93.0801 66.3413 91.4669 71.3064 92.9395 75.8386L98.5307 93.0463C101.824 103.181 90.2243 111.608 81.6034 105.345L66.9656 94.7098C63.1103 91.9087 57.8897 91.9087 54.0344 94.7098L39.3966 105.345C30.7758 111.608 19.1765 103.181 22.4693 93.0463L28.0605 75.8386C29.5331 71.3064 27.9198 66.3413 24.0645 63.5402L9.42669 52.9053C0.805841 46.6419 5.23639 33.0061 15.8924 33.0061H33.9856C38.7511 33.0061 42.9746 29.9375 44.4472 25.4053L50.0384 8.19758Z' fill='%23F3BA26'/%3E%3C/g%3E%3Cg filter='url(%23filter1_i_0_1)'%3E%3Cpath d='M41 60.0588L52.6667 72L81 43' stroke='%23FFFAFA' stroke-width='10' stroke-linecap='round'/%3E%3C/g%3E%3Cdefs%3E%3Cfilter id='filter0_d_0_1' x='0.871338' y='0.596741' width='119.257' height='114.899' filterUnits='userSpaceOnUse' color-interpolation-filters='sRGB'%3E%3CfeFlood flood-opacity='0' result='BackgroundImageFix'/%3E%3CfeColorMatrix in='SourceAlpha' type='matrix' values='0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0' result='hardAlpha'/%3E%3CfeOffset dy='4'/%3E%3CfeGaussianBlur stdDeviation='2'/%3E%3CfeComposite in2='hardAlpha' operator='out'/%3E%3CfeColorMatrix type='matrix' values='0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.25 0'/%3E%3CfeBlend mode='normal' in2='BackgroundImageFix' result='effect1_dropShadow_0_1'/%3E%3CfeBlend mode='normal' in='SourceGraphic' in2='effect1_dropShadow_0_1' result='shape'/%3E%3C/filter%3E%3Cfilter id='filter1_i_0_1' x='36' y='38' width='50' height='45.1547' filterUnits='userSpaceOnUse' color-interpolation-filters='sRGB'%3E%3CfeFlood flood-opacity='0' result='BackgroundImageFix'/%3E%3CfeBlend mode='normal' in='SourceGraphic' in2='BackgroundImageFix' result='shape'/%3E%3CfeColorMatrix in='SourceAlpha' type='matrix' values='0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0' result='hardAlpha'/%3E%3CfeOffset dy='4'/%3E%3CfeGaussianBlur stdDeviation='2'/%3E%3CfeComposite in2='hardAlpha' operator='arithmetic' k2='-1' k3='1'/%3E%3CfeColorMatrix type='matrix' values='0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.25 0'/%3E%3CfeBlend mode='normal' in2='shape' result='effect1_innerShadow_0_1'/%3E%3C/filter%3E%3C/defs%3E%3C/svg%3E%0A");
            position: absolute;
            max-height: var(--height);
            max-width: var(--height);
            border: 0;
            transition: transform 0.2s;
            /* overflow: hidden; */
            z-index: 1;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper" class="h-100">

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

                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text="Save"   />
                        &nbsp;              
                             <asp:Button ID="btnUpdate" class="btn btn-sm  btn-warning" runat="server" Text="Update"   />
                        &nbsp;   
                            <asp:Button ID="btnConfirm" class="btn btn-sm  btn-secondary" runat="server" Text="Confirm" OnClientClick="Confirm();"   />
                        &nbsp;   
                            <asp:Button ID="btnCancel" class="btn btn-sm  btn-danger" runat="server" Text="Cancel"   />
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

                <div class="row text-muted" style="font-size: .7rem;">
                    <div class="col">
                        <div class="row">
                            <%=confirmer_code %>
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
                        <div class="row">
                            <%=now_action %>
                        </div>
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

                                <h4>รวมทั้งสิ้น <% =total%>.-</h4>

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
                                                <asp:Button ID="btnDelete" class="btn btn-sm  btn-danger rounded-circle mr-2" runat="server" Width="35px" Text="-" OnClientClick="return sendID();" title="Delete"   />
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
                <div class="row ">
                    <div class="text-center m-auto">

                        <% If Not Request.QueryString("approvalHOcode") Is Nothing And maintable.Rows.Count > 0 Then%>
                        <% if ViewState("status_approvalHO") = "write" And (maintable.Rows(0).Item("statusid") = 2) Then%>
                        <% If approval And maintable.Rows(0).Item("statusid") = 2 Then%>
                        <asp:Button ID="btnApproval" class="btn btn-success" runat="server" Text="อนุมัติ"   />
                        <% End If %>
                        <% If verify And maintable.Rows(0).Item("statusid") = 2 Then%>
                        <asp:Button ID="btnVerify" class="btn btn-warning" runat="server" Text="ยืนยันการตรวจสอบ"    />
                        <% End If %>
                        <% If ((verify Or approval)) Then%>
                        <asp:Button ID="btnDisApproval" class="btn btn-danger" runat="server" Text="ไม่อนุมัติ"   />
                        <% End If %>
                        <% ElseIf (ViewState("status_approvalHO") = "confirm") And (confirmer_code.IndexOf(Session("usercode").ToString) > -1) And (maintable.Rows(0).Item("statusid") = 6) And confirmer Then %>
                        <asp:Button ID="btnConfirmerApproval" class="btn btn-success" runat="server" Text="ยืนยันหักยอดขาย"   />
                        <% End If %>
                        <% End If %>
                    </div>
                </div>
                <div class="row notPrint mb-3" id="card_attatch" runat="server">
                    <div class="col-md-6 mt-3">
                        <div class="card shadow card_attatch">
                            <div class="card-header">
                                เอกสารแนบ
                            </div>
                            <div class="card-body attatchItems">
                                <%--begin Attatch item--%>

                                <% For i = 0 To AttachTable.Rows.Count - 1 %>
                                <div class="row">
                                    <% If Not Request.QueryString("approvalHOcode") Is Nothing And maintable.Rows.Count > 0 And (Session("depid").ToString = "2" Or Session("depid").ToString = "4" Or Session("depid").ToString = "24" Or Session("depid").ToString = "25") Then%>
                                    <% If maintable.Rows(0).Item("statusid") = 7 Then%>
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
                                            <asp:Button ID="btnSaveComment" class="btn btn-primary w-100" runat="server" Text="Post" AutoPostBack="True"   disabled />
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
                                <asp:TextBox class="form-control" ID="txtDetail" runat="server" Rows="3" Columns="50" TextMode="MultiLine" required></asp:TextBox>
                                <div class="invalid-feedback">กรุณากรอกรายละเอียด</div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <%--<button type="button" id="btnAddDetail" class="btn btn-primary noEnterSubmit">Save</button>--%>
                    <asp:Button ID="btnAddDetailsss" class="btn btn-primary" runat="server" Text="Save" OnClientClick="return postBack_addDetails();"   />

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
                    <asp:Button ID="asd" class="btn btn-primary" runat="server" Text="Save" OnClientClick="chooseMyfile(); return false;"   />
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

    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <CompositeScript>
            <Scripts>
                <asp:ScriptReference Path="~/js/NonPO.js" />
                <asp:ScriptReference Path="~/vendor/jquery/jquery.min.js" />
            </Scripts>
        </CompositeScript>
    </asp:ScriptManager>
<%--    <script src="<%=Page.ResolveUrl("~/js/NonPO.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>--%>
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
            var status_session = "<%= ViewState("status")%>"

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
                        arrs_app[i].style.color = "White";

                        break;
                    case "รอนุมัติจากหน่วยงานที่เกี่ยวข้อง":
                        arrs_app[i].style.backgroundColor = "LightYellow";
                        break;
                    case "รอบัญชีตรวจสอบ":
                        arrs_app[i].style.backgroundColor = "LightSalmon";

                        break;
                    case "ไม่ผ่านอนุมัติจากหน่วยงานที่เกี่ยวข้อง":
                        arrs_app[i].style.backgroundColor = "IndianRed";
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
        function disbtndelete() {
            $(".deletedetail").hide();
        }
    </script>
</asp:Content>
