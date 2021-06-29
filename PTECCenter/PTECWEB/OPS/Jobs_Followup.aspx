<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="Jobs_Followup.aspx.vb" Inherits="PTECCENTER.JobsFollowup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .input-group {
            margin-bottom: 1rem;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper">

        <!-- #include virtual ="/include/menu.inc" -->
        <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">
                <ol class="breadcrumb" style="background-color: deeppink; color: white">
                    <li class="breadcrumb-item">ติดตามงาน (Job Follow up)
                    </li>
                </ol>

                <div class="row">
                    <div class="col-12">
                        <asp:Button ID="btnBack" class="btn btn-sm  btn-danger" runat="server" Text=" back " />
                        <% If maintable.Rows(0).Item("owner") > 0 Then %>

                        <asp:Button ID="btnPrint" class="btn btn-sm  btn-warning" runat="server" Text="Print" />
                        <% End if %>
                    </div>
                </div>

                <div class="row" style="padding-top: 1rem;">
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">เลขที่เอกสาร</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtJobno" runat="server" ReadOnly="true"></asp:TextBox>
                            <div class="input-group-append">
                            </div>
                        </div>

                    </div>
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">วันที่แจ้ง</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtDocDate" runat="server" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ผู้แจ้ง</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtOwner" runat="server" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">สาขา</span>
                                <asp:TextBox class="form-control" ID="txtBranch" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ฝ่าย</span>
                                <asp:TextBox class="form-control" ID="txtDepartment" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">แผนก</span>
                                <asp:TextBox class="form-control" ID="txtSection" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ประเภทงาน</span>
                                <asp:TextBox class="form-control" ID="txtJobType" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">รหัสทรัพย์สิน</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtAssetCode" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <asp:TextBox class="form-control" ID="txtAssetName" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-4">
                        <div class="input-group sm-4">
                            <div class="input-group-prepend">
                                <span class="input-group-text">จำนวน</span>
                                <asp:TextBox class="form-control" ID="txtQuantity" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group sm-4">
                            <div class="input-group-prepend">
                                <span class="input-group-text">หน่วย</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtUnit" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ประเภทหมวดราคา</span>
                                <asp:DropDownList class="form-control" ID="cboJobCenter"
                                    runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ค่าใช้จ่าย (ประมาณ)</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtCost" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                ControlToValidate="txtCost" runat="server"
                                ErrorMessage="ตัวเลขเท่านั้น"
                                ValidationExpression="\d+.\d+"
                                ForeColor="Red">
                            </asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Supplier</span>
                                <asp:DropDownList class="form-control" ID="cboSupplier"
                                    runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="input-group sm-3">
                            <% If maintable.Rows(0).Item("owner") > 0 And maintable.Rows(0).Item("followup_status") <> "ปิดงาน" Then %>
                            <asp:Button ID="btnUpdate" class="btn btn-sm  btn-success" runat="server" Text="Update Cost and Supplier" />
                            <% End if %>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">รายละเอียดงาน</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtDetail" runat="server" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="card-body">
                    <div class="table-responsive">

                        <table class="table table-bordered " id="dataTable">
                            <thead class="table-info">
                                <tr>
                                    <th style="width: 200px;">สถานะ</th>
                                    <th style="width: 300px;">รายละเอียด</th>
                                    <th style="width: 200px;">ผู้ปรับปรุง</th>
                                    <th style="width: 200px;">วันที่ปรับปรุง</th>
                                    <th style="width: 120px;"></th>
                                </tr>
                            </thead>
                            <tbody>
                                <% For i = 0 To followuptable.Rows.Count - 1 %>
                                <tr>
                                    <td><% =followuptable.Rows(i).Item("statusname") %></td>
                                    <td><% =followuptable.Rows(i).Item("details") %></td>
                                    <td><% =followuptable.Rows(i).Item("createby") %></td>
                                    <td><% =followuptable.Rows(i).Item("createdate") %></td>
                                    <td>
                                        <% If (i = 0) Then%>
                                            <asp:Label ID="lastStatus" CssClass="text-danger" runat="server" Text="(สถานะล่าสุด)" />
                                        <%  End If %>
                                    </td>
                                </tr>
                                <% Next i %>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="cboStatus" class="form-control" runat="server"></asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDetailFollow" class="form-control" runat="server" TextMode="MultiLine" required></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCreateBy" class="form-control" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCreateDate" class="form-control" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <% If maintable.Rows(0).Item("owner") > 0 Then %>
                                        <% If maintable.Rows(0).Item("followup_status") <> "ปิดงาน" Then %>
                                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text=" + " OnClientClick="validateData()" />
                                        <% End if %>
                                        <asp:Button ID="btnClose" class="btn btn-sm  btn-danger" runat="server" Text="ค่าใช้จ่าย" />
                                        <% ElseIf String.Equals(Session("username"), maintable.Rows(0).Item("createby")) Then %>
                                        <% If maintable.Rows(0).Item("followup_status") <> "ปิดงาน" Then %>
                                        <asp:Button ID="btnConfirm" class="btn btn-sm  btn-warning" runat="server" Text=" + " OnClientClick="validateData()" />
                                        <% End if %>
                                        <% End if %>

                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <%--   status,detail--%>
                    </div>
                </div>

            </div>
            <!-- end content-wrapper -->


            <!-- end เนื้อหาเว็บ -->

            <!-- Sticky Footer -->
            <footer class="sticky-footer">
                <div class="container my-auto">
                    <div class="copyright text-center my-auto">
                        <span>Copyright © Your Website 2019</span>
                    </div>
                </div>
            </footer>
        </div>
        <!-- /#wrapper -->
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
    <script type="text/javascript">
        $(document).ready(function () {
            $('.form-control').selectpicker({
                liveSearch: true,
                maxOptions: 1
            });
        });
    </script>
</asp:Content>
