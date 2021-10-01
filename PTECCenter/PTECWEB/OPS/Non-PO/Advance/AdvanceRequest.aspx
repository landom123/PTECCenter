<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="AdvanceRequest.aspx.vb" Inherits="PTECCENTER.AdvanceRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
                                <div class="card-body attatchItems">
                                    <% If Not Request.QueryString("ADVRQC") Is Nothing Then%>
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
                                            <asp:Label ID="lbCreateBy" CssClass="form-label" AssociatedControlID="txtCreateBy" runat="server" Text="ผู้แจ้งขออนุมัติ" />
                                        </div>
                                        <div class="col-lg-10">
                                            <asp:TextBox class="form-control" ID="txtCreateBy" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-lg-2 col-form-label">
                                            <asp:Label ID="lbDocDate" CssClass="form-label" AssociatedControlID="txtDocDate" runat="server" Text="วันที่แจ้ง" />
                                        </div>
                                        <div class="col-lg-10">
                                            <asp:TextBox class="form-control" ID="txtDocDate" runat="server" ReadOnly="true"></asp:TextBox>
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
                                <% If Session("status") = "new" Then%>
                                <div class="card-footer text-center bg-white">
                                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" OnClientClick="validateData()" />
                                </div>
                                <% ElseIf Session("status") = "read" And (Session("userid").ToString() = detailtable.Rows(0).Item("createby").ToString()) Then%>
                                <div class="card-footer text-center bg-white">
                                    <asp:Button ID="btnConfirm" class="btn btn-warning" runat="server" Text="Confirm" OnClientClick="Confirm();" />
                                    <asp:Button ID="btnCancel" class="btn btn-danger" runat="server" Text="Cancel" />
                                    <asp:Button ID="btnClose" class="btn btn-danger" runat="server" Text="ปิดงาน" />
                                    <asp:Button ID="btnAddDoc" class="btn btn-success" runat="server" Text="แนบเอกสารให้ฝ่ายประสานงาน" />
                                    <asp:Button ID="btnEdit" class="btn btn-secondary" runat="server" Text="Edit" />
                                    <asp:Button ID="btnClearAdvance" class="btn btn-warning" runat="server" Text="เคลียร์ค่าใช้จ่าย" />

                                </div>
                                <% ElseIf Session("status") = "write" And detailtable.Rows(0).Item("statusrqid") = 2 Then%>
                                <div class="card-footer text-center bg-white">
                                    <asp:Button ID="btnApproval" class="btn btn-success" runat="server" Text="อนุมัติ" />
                                    <button runat="server" id="btnDisApproval" name="btnEdit" onclick="return disApproval();" class="btn btn-danger">
                                        ไม่อนุมัติ
                                    </button>
                                </div>
                                <% ElseIf Session("status") = "edit" Then%>
                                <div class="card-footer text-center bg-white">
                                    <asp:Button ID="btnSaveEdit" class="btn btn-success" runat="server" Text="Save" OnClientClick="validateData()" />
                                    <asp:Button ID="btnCancelEdit" class="btn btn-danger" runat="server" Text="Cancel" />
                                </div>
                                <% End If %>
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
                                            <asp:TemplateField HeaderText="Code" ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcode" runat="server" Text='<%#Eval("AdvanceRequest_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Branch" ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBranch" runat="server" Text='<%#Eval("Createname")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbljobdate" runat="server" Text='<%#Eval("CreateDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbljobdate" runat="server" Text='<%#Eval("detail")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbljobtype" runat="server" Text='<%#String.Format("{0:n4}", Eval("amount"))%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="center">
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
                                <h4>ทั้งหมด <% =cntdt%> รายการ </h4>
                                <h4>ยอดรวมค้างชำระ <b><% =String.Format("{0:n4}", sumitem)%></b> บาท</h4>
                                <% End If %>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>


    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>
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
