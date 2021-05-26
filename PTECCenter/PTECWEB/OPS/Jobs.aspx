<%@ Page Title="OPS : จัดการใบแจ้งงาน" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="Jobs.aspx.vb" Inherits="PTECCENTER.frmJobs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    <div id="wrapper">

        <!-- #include virtual ="/include/menu.inc" -->
        <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">
                <!-- Breadcrumbs-->
                <ol class="breadcrumb" style="background-color: deeppink; color: white">
                    <li class="breadcrumb-item">
                        <a href="JobsList.aspx" class="btn btn-sm btn-danger">
                            <i class="fa fa-tasks" aria-hidden="true"></i></a>จัดการ Jobs
                    </li>
                </ol>


                <div class="row">
                    <div class="col-12">
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-primary" runat="server" Text="New" />&nbsp;
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success btnSave" runat="server" Text="Save" />
                        &nbsp;              
                        <asp:Button ID="btnConfirm" class="btn btn-sm  btn-secondary" runat="server" Text="Confirm" />
                        &nbsp;   
                        <asp:Button ID="btnPrint" class="btn btn-sm  btn-warning" runat="server" Text="Print" />&nbsp;
                        <button <% If Session("status") = "new" Or Session("status") = "cancel" Then %> disabled <% End if %> type="button" class="btn btn-sm  btn-danger" onclick="chkCancel('../ops/jobsCancel.aspx?jobno=<% =Session("jobno") %>')">Cancel</button>
                    </div>
                </div>
                <p></p>
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">เลขที่เอกสาร</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtJobno" runat="server" ReadOnly="true"></asp:TextBox>
                            <div class="input-group-append">
                                <button type="button" class="btn btn-sm  btn-secondary" onclick="find('../OPS/jobs.aspx?jobno=','ระบุเลขที่ OPS')">Find</button>
                            </div>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">วันที่สร้างรายการ</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtCreateDate" runat="server" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ผู้สร้างรายการ</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtCreateBy" runat="server" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <p></p>
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">วันที่แจ้ง</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtDocDate" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ผู้แจ้ง</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboOwner" runat="server" readonly="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">สถานะ</span>
                            </div>
                            <div class="input-group-append">
                                <asp:TextBox class="btn btn-danger" ID="txtStatus" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <p></p>
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">สาขา</span>
                                <asp:DropDownList class="selectpicker form-control" data-live-search="true" ID="cboBranch" AutoPostBack="True"
                                    runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ฝ่าย</span>
                                <asp:DropDownList class="form-control" ID="cboDepartment" AutoPostBack="True"
                                    runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">แผนก</span>
                                <asp:DropDownList class="form-control" ID="cboSection" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- DataTables Example -->
                <br />
                <div class="card">
                    <div class="card-header" style="background-color: navy; color: white">
                        <i class="fas fa-table"></i>
                        รายละเอียดงาน
                    </div>
                    <div id="jobdetail" class="card-body">



                        <%--begin item--%>

                        <div class="table-responsive">

                            <% For i = 0 To detailtable.Rows.Count - 1
                            %>
                            <% if detailtable.Rows(i).Item("jobdetailid") = 0 Then%>
                            <div class="row justify-content-start">
                                <asp:TextBox class="btn btn-warning" ID="TextBox1" runat="server" ReadOnly="true">ยังไม่บันทึก</asp:TextBox>
                            </div>
                            <br />
                            <% End If %>
                            <%-- begin detail row--%>
                            <% if detailtable.Rows(i).Item("jobtypeid") = 1 Then%>
                            <div class="row">
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">ประเภทงาน</span>
                                            <span class="input-group-text" style="width: 550px"><% =detailtable.Rows(i).Item("jobtype") %></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">ตำแหน่งตู้จ่าย</span>
                                            <span class="input-group-text" style="width: 550px"><% =detailtable.Rows(i).Item("assetposition") %></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                        <% if String.IsNullOrEmpty(detailtable.Rows(i).Item("assetname")) Then%>
                                        <span class="input-group-text" style="width: 550px">ยังไม่บันทึก</span>
                                        <% Else %>
                                        <span class="input-group-text" style="width: 550px"><% =detailtable.Rows(i).Item("assetname") %></span>
                                        <% End if %>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <% Else %>
                            <div class="row">
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">ประเภทงาน</span>
                                            <span class="input-group-text" style="width: 550px"><% =detailtable.Rows(i).Item("jobtype") %></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">รหัสทรัพย์สิน</span>
                                            <span class="input-group-text" style="width: 550px"><% =detailtable.Rows(i).Item("assetcode") %></span>
                                        </div>
                                    </div>
                                </div>
                                <% if Not String.IsNullOrEmpty(detailtable.Rows(i).Item("assetname")) Then%>
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                        <span class="input-group-text" style="width: 550px"><% =detailtable.Rows(i).Item("assetname") %></span>
                                    </div>
                                </div>
                                <% End if %>
                            </div>
                            <br />
                            <% End if %>
                            <div class="row">
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">จำนวน</span>
                                            <span class="input-group-text" style="width: 550px"><% =detailtable.Rows(i).Item("quantity") %></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">หน่วย</span>
                                            <span class="input-group-text" style="width: 550px"><% =detailtable.Rows(i).Item("unit") %></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">ค่าใช้จ่าย (ประมาณ)</span>
                                            <span class="input-group-text" style="width: 170px"><% =detailtable.Rows(i).Item("cost") %></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">Supplier</span>
                                            <span class="input-group-text" style="width: 550px"><% =detailtable.Rows(i).Item("supplier") %></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">ความเร่งด่วน</span>
                                            <span class="input-group-text" style="width: 550px"><% =detailtable.Rows(i).Item("policy") %></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">กำหนดการ</span>
                                            <span class="input-group-text" style="width: 200px; color: red; font-weight: bold"><% =detailtable.Rows(i).Item("requestdate") %></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-12">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">เอกสารแนบ</span>
                                            <span class="input-group-text" style="width: 550px"><a href="<%=Page.ResolveUrl("attatch/" & detailtable.Rows(i).Item("attatch")) %>" target="_blank"><% =detailtable.Rows(i).Item("attatch") %></a></span>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-8">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">รายละเอียดงาน</span>
                                            <%--                                       <span class="input-group-text" style="width:550px"><% =detailtable.Rows(i).Item("details") %></span>--%>
                                            <label for="detail"><% =detailtable.Rows(i).Item("details") %></label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <% If objStatus = "confirm" Then %>
                                    <button type="button" class="btn btn-sm  btn-primary"
                                        onclick="location.href='../OPS/jobs_followup.aspx?jobno=<% =detailtable.Rows(i).Item("jobno") %>&jobdetailid=<% =detailtable.Rows(i).Item("jobdetailid") %>'">
                                        Followup</button>&nbsp;
                                    <span class="btn btn-sm  btn-danger">สถานะงาน : <% =detailtable.Rows(i).Item("followup_status") %></span>
                                    <% End if %>
                                    <% If objStatus = "edit" And owner = 1 Then %>
                                    <button type="button" class="btn btn-sm  btn-danger"
                                        onclick="chkDel('../OPS/jobsdetail_delete.aspx?jobno=<% =detailtable.Rows(i).Item("jobno") %>&jobdetailid=<% =detailtable.Rows(i).Item("jobdetailid") %>')">
                                        Delete</button>
                                    <% End if %>
                                </div>
                            </div>

                            <%-- end detail row--%>
                            <hr style="height: 2px; border-width: 0; color: gray; background-color: gray" />
                            <% Next i %>

                            <%-- begin free detail row--%>
                            <% If objStatus = "new" Or objStatus = "edit" Then %>
                            <div class="row">
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">ประเภทงาน</span>
                                            <asp:DropDownList ID="cboJobType" class="form-control" runat="server" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <% If cboJobType.SelectedItem.Value = "1" Then %>
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">ตำแหน่งตู้จ่าย</span>
                                            <asp:DropDownList ID="cboPosition" class="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <% Else %>
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">รหัสทรัพย์สิน</span>
                                        </div>
                                        <asp:TextBox class="form-control" ID="txtAssetCode" runat="server"></asp:TextBox>
                                        <div class="input-group-append">
                                            <asp:Button ID="btnFind" class="btn btn-sm  btn-secondary" runat="server" Text="Find" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                        <asp:TextBox class="form-control" ID="txtAssetName" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>

                                <% End if %>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">จำนวน</span>
                                            <asp:TextBox class="form-control" ID="txtQuantity" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">หน่วย</span>
                                            <asp:DropDownList ID="cboUnit" class="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">ค่าใช้จ่าย (ประมาณ)</span>
                                            <asp:TextBox class="form-control" Style="width: 100px" ID="txtCost" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">Supplier</span>
                                            <asp:DropDownList class="form-control" ID="cboSupplier"
                                                runat="server" Style="width: 100%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">ความเร่งด่วน</span>
                                            <asp:DropDownList class="form-control" ID="cboPolicy"
                                                runat="server" AutoPostBack="True" Style="width: 100%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">วันที่ต้องการ</span>
                                            <asp:TextBox class="form-control" ID="txtDueDate" runat="server" Style="width: 200px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-12">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">เอกสารแนบ</span>
                                        </div>
                                        <asp:Label ID="lblattatch" class="form-control" runat="server" Text="เลือกไฟล์"></asp:Label>
                                        <asp:FileUpload ID="FileUpload1" class="btn btn-sm  btn-secondary files" runat="server" text="เลือกไฟล์ --ยังไม่เสร็จ" />
                                        <asp:Button ID="btnUpload" class="btn btn-sm  btn-secondary" runat="server" Text="upload" />
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-8">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">รายละเอียดงาน</span>
                                        </div>
                                        <asp:TextBox class="form-control" ID="txtJobDetail" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        <div class="invalid-feedback">* กรุณาใส่รายละเอียดงาน</div>
                                    </div>
                                </div>

                                <div class="col-4">
                                    <div class="d-flex justify-content-center">
                                        <asp:Button ID="btnAddDetail" class="btn btn-sm  btn-primary" runat="server" Text=" + " />&nbsp;
                                    </div>
                                </div>

                            </div>
                            <br />
                            <% End if %>
                            <%-- end free detail row--%>
                        </div>

                        <%-- end item--%>
                    </div>
                    <!-- end jobdetail -->

                </div>
                <!-- end display job detail -->

            </div>
            <!-- /.container-fluid -->
            <!-- Sticky Footer -->

        </div>
        <!-- end content-wrapper -->


        <!-- end เนื้อหาเว็บ -->

    </div>
    <!-- /#wrapper -->


    <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>

    <script type="text/javascript">
        jQuery('[id$=txtDueDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: true,
            format: 'd/m/Y H:i'
        });
    </script>

    <script type="text/javascript">

        $(document).ready(function () {
            $('.form-control').selectpicker({
                liveSearch: true,
                maxOptions: 1
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
