<%@ Page Title="ApprovalMenuList" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="ApprovalMenuList.aspx.vb" Inherits="PTECCENTER.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">

    <style>
        th {
            text-align: center;
        }

            th a {
                color: black;
            }

        .input-group {
            padding-top: 1rem;
        }

        .btn-light {
            background-color: #fff;
        }

            .btn-light.disabled {
                border: 1px solid #ced4da;
                background-color: #e9ecef;
            }

        #wrapper #content-wrapper {
            overflow-x: visible !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="wrapper">

        <!-- #include virtual ="/include/menu.inc" -->
        <!-- add side menu -->

        <!-- เนื้อหาเว็บ -->
        <div id="content-wrapper">

            <div class="container-fluid">
                <ol class="breadcrumb" style="background-color: navy; color: white">
                    <li class="breadcrumb-item">รายการ ขออนุมัติ
                    </li>
                </ol>

                <div class="row">
                    <div class="col-12 ">
                        <a href="approval.aspx" class="btn btn-sm btn-primary ">New</a>
                        &nbsp;
                        <% If Not Session("positionid") = "10" Then%>
                        <asp:Button ID="btnSearch" class="btn btn-sm  btn-success" runat="server" Text="Search" />&nbsp; 
                        <asp:Button ID="btnClear" class="btn btn-sm  btn-secondary" runat="server" Text="Clear" />&nbsp;
                        <asp:Button ID="btnPrint" class="btn btn-sm  btn-info" runat="server" Text="Export" />&nbsp;
                         <% End If %>
                    </div>
                </div>
                <% If Not Session("positionid") = "10" Then%>

                <div class="row" style="padding-top: 1rem;">
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Code</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtApprovalCode" runat="server" placeholder="APP2XXX" AutoPostBack="false" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">หมวด</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboApprovalGroup" runat="server" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ประเภทงาน</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboApprovalCategory" runat="server" AutoPostBack="false"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">หัวข้อ</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboApproval" runat="server" AutoPostBack="false"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">สถานะ</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboStatus" runat="server" AutoPostBack="false"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">พื้นที่</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboArea" runat="server" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">สาขา</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboBranch" runat="server" AutoPostBack="false"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ตั้งแต่วันที่</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtStartDate" name="txtStartDate" runat="server" placeholder="--- คลิกเพื่อเลือก ---" AutoPostBack="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">จนถึง</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtEndDate" name="txtEndDate" runat="server" placeholder="--- คลิกเพื่อเลือก ---" AutoPostBack="false"></asp:TextBox>

                        </div>
                    </div>
                </div>
                <div class="row" style="padding-top: 1rem;">
                    <div class="col-md-12">
                        <asp:Label ID="note" CssClass="text-danger text-right" runat="server" Text="( เงื่อนไข 1 : รายการใน 'หัวข้อ' จะเปลี่ยนไปตาม 'หมวด' ที่เลือก )" />
                    </div>
                    <div class="col-md-12">
                        <asp:Label ID="note2" CssClass="text-danger text-right" runat="server" Text="( เงื่อนไข 2 : รายการใน 'สาขา' จะเปลี่ยนไปตาม 'พื้นที่' ที่เลือก )" />
                    </div>
                </div>
                <% else %>
                <div class="row">
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">สถานะงาน</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboWorking" runat="server" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <% End If %>
                <div class="card-body">
                    <div class="row justify-content-end">
                        <div class="col-auto">
                            <asp:DropDownList ID="cboMaxRows" class="form-control" runat="server">
                                <asp:ListItem Value="1000">1,000 รายการ</asp:ListItem>
                                <asp:ListItem Value="2147483647">รายการทั้งหมด</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <asp:GridView ID="gvRemind"
                            class="table table-striped table-bordered"
                            AllowSorting="true"
                            AutoGenerateColumns="false"
                            EmptyDataText="No data available."
                            PageSize="20"
                            AllowPaging="true"
                            runat="server">
                            <Columns>
                                <asp:TemplateField SortExpression="approvalcode" HeaderText="เลขที่ใบงาน" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcode" runat="server" Text='<%#Eval("approvalcode")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="cladvcode" HeaderText="เลขที่ใบเคลียร์" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <div class="d-flex flex-column align-items-center">
                                            <asp:Label ID="lblcode" runat="server" Text='<%#Eval("cladvcode")%>' ToolTip='<%#Eval("cladvstatus")%>'></asp:Label>
                                            <a href="../Non-PO/Advance/ClearAdvance.aspx?NonpoCode=<%#Eval("cladvcode")%>" target="_blank" class="badge badgestatus_cladv" title="<%#Eval("cladvstatus")%>"><%#Eval("cladvstatus")%></a>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField SortExpression="createdate" HeaderText="วันที่แจ้ง">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("createdate")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="createby" HeaderText="โดย" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("createby")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="branch" HeaderText="สาขา" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranch" runat="server" Text='<%#Eval("branch")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="approvalname" HeaderText="หัวข้อ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblApprovalname" runat="server" Text='<%#Eval("approvalname")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="owner_permission" HeaderText="ผู้มีสิทธิอนุมัติ" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbOwnerpermission" runat="server" Text='<%#Eval("owner_permission")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="statusname" HeaderText="สถานะ" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("statusname")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Eval("link")%>' Text=""><img src="../../icon/addnote.png" title="รายละเอียด" style="width:20px" /></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <%--<h4>ทั้งหมด <% =cntdt%> รายการ</h4>--%>
                </div>


            </div>
            <!-- /.container-fluid -->



        </div>
        <!-- /.content-wrapper -->
        <!-- end เนื้อหาเว็บ -->


    </div>
    <!-- /#wrapper -->

    <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>

    <script type="text/javascript">
        jQuery('[id$=txtStartDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08'
            onShow: function (ct) {
                this.setOptions({
                    maxDate: jQuery('[id$=txtEndDate]').val() ? jQuery('[id$=txtEndDate]').val() : false, formatDate: 'd.m.Y'
                })
            },
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });

        jQuery('[id$=txtEndDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            onShow: function (ct) {
                this.setOptions({
                    minDate: jQuery('[id$=txtStartDate]').val() ? jQuery('[id$=txtStartDate]').val() : false, formatDate: 'd.m.Y'
                })
            },
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>

    <script type="text/javascript">

        $(document).ready(function () {
            $('.form-control').selectpicker({
                liveSearch: true,
                maxOptions: 1
            });

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
