<%@ Page Title="OPS : จัดการใบแจ้งงาน" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="Jobs.aspx.vb" Inherits="PTECCENTER.frmJobs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
    <script src="https://cdn.tiny.cloud/1/eivog24cbgm1fhi4pm2cg4pw1lp478mhyjjtxnzml4fi51pa/tinymce/5/tinymce.min.js" referrerpolicy="origin"></script>
    <link href="<%=Page.ResolveUrl("~/css/Jobs.css")%>" rel="stylesheet">
    <style>
        html {
            background-color: #f0f2f5 !important;
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
                <!-- Breadcrumbs-->
                <%-- <ol class="breadcrumb" style="background-color: deeppink; color: white">
                    <li class="breadcrumb-item">
                        <a href="JobsLists.aspx" class="btn btn-sm btn-danger">
                            <i class="fa fa-tasks" aria-hidden="true"></i></a>จัดการ Jobs
                    </li>
                </ol>--%>
                <div class="headJobs mb-3" style="background-color: #a32048; color: white; padding: 0.75rem 1rem; border-radius: 0.25rem;">
                    <div class="row justify-content-between">
                        <div class="col text-left align-self-center">
                            จัดการ Jobs
                        </div>
                        <div class="col-auto text-right align-self-center">
                            <a href="JobsLists.aspx" class="btn btn-sm btn-danger">
                                <i class="fa fa-tasks" aria-hidden="true"></i></a>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-12 mb-3">
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-primary" runat="server" Text="New" />&nbsp;
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success btnSave" runat="server" Text="Save" />
                        &nbsp;              
                        <asp:Button ID="btnConfirm" class="btn btn-sm  btn-secondary" runat="server" Text="Confirm" />
                        &nbsp;   
                        <asp:Button ID="btnPrint" class="btn btn-sm  btn-warning" runat="server" Text="Print" />&nbsp;
                        <button <% If Session("status") = "new" Or Session("status") = "cancel" Then %> disabled <% End if %> type="button" class="btn btn-sm  btn-danger d-none" onclick="chkCancel('../ops/jobsCancel.aspx?jobno=<% =Session("jobno") %>')">Cancel</button>&nbsp;
                        <button runat="server" id="btnCancel" name="btnCancel" onclick="return cancelJobs();" class="btn btn-sm btn-danger">
                            Cancel
                        </button>&nbsp;
                        <% If Not Request.QueryString("jobno") Is Nothing And maintable.Rows.Count > 0 Then%>
                        <% if (maintable.Rows(0).Item("statusname") = "แจ้งงาน") Then%>
                        <div class="alert alert-warning alert-dismissible fade show mt-3" role="alert">
                            <strong>กด confirm เพื่อดำเนินการต่อ</strong>
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <% End If %>
                        <% End If %>
                    </div>
                </div>
                <div>
                    <div class="row">
                        <div class="col-md-4 mb-3">
                            <div class="input-group sm-3 shadow-sm">
                                <div class="input-group-prepend w-100">
                                    <span class="input-group-text">เลขที่เอกสาร</span>
                                    <asp:TextBox class="form-control" ID="txtJobno" runat="server" ReadOnly="true"></asp:TextBox>
                                    <div class="input-group-append">
                                        <button type="button" class="btn btn-sm  btn-secondary" onclick="find('../OPS/jobs.aspx?jobno=','ระบุเลขที่ OPS')">Find</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4 mb-3">
                            <div class="input-group sm-3 shadow-sm">
                                <div class="input-group-prepend w-100">
                                    <span class="input-group-text">วันที่สร้างรายการ</span>
                                    <asp:TextBox class="form-control" ID="txtCreateDate" runat="server" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4 mb-3">
                            <div class="input-group sm-3 shadow-sm">
                                <div class="input-group-prepend w-100">
                                    <span class="input-group-text">ผู้สร้างรายการ</span>
                                    <asp:TextBox class="form-control" ID="txtCreateBy" runat="server" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4 mb-3">
                            <div class="input-group sm-3 shadow-sm">
                                <div class="input-group-prepend w-100">
                                    <span class="input-group-text">วันที่แจ้ง</span>
                                    <asp:TextBox class="form-control" ID="txtDocDate" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4 mb-3">
                            <div class="input-group sm-3 shadow-sm">
                                <div class="input-group-prepend w-100">
                                    <span class="input-group-text">ผู้แจ้ง</span>
                                    <asp:DropDownList class="form-control" ID="cboOwner" runat="server" readonly="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 mb-3">
                            <div class="input-group sm-3 shadow-sm">
                                <div class="input-group-prepend w-100">
                                    <span class="input-group-text">สถานะ</span>
                                    <asp:TextBox class="btn btn-sm btn-danger w-100" ID="txtStatus" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4 mb-3">
                            <div class="input-group sm-3 shadow-sm">
                                <div class="input-group-prepend w-100">
                                    <span class="input-group-text">สาขา</span>
                                    <asp:DropDownList class="selectpicker form-control" data-live-search="true" ID="cboBranch" AutoPostBack="True"
                                        runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4 mb-3">
                            <div class="input-group sm-3 shadow-sm">
                                <div class="input-group-prepend w-100">
                                    <span class="input-group-text">ฝ่าย</span>
                                    <asp:DropDownList class="form-control" ID="cboDepartment" AutoPostBack="True"
                                        runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4 mb-3">
                            <div class="input-group sm-3 shadow-sm">
                                <div class="input-group-prepend w-100">
                                    <span class="input-group-text">แผนก</span>
                                    <asp:DropDownList class="form-control" ID="cboSection" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- DataTables Example -->

                <div class="card shadow">
                    <div class="card-header">
                        <h5 class="mb-0">
                            <button class="btn btn-link w-100 text-left collapse__all" id="detailJobs" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne" autopostback="False">
                                รายละเอียดงาน
                            </button>
                        </h5>
                    </div>
                    <div id="jobdetail" class="card-body px-md-5 px-2">



                        <%--begin item--%>

                        <div class="table-responsive">

                            <% For i = 0 To detailtable.Rows.Count - 1
                            %>
                            <% if detailtable.Rows(i).Item("jobdetailid") = 0 Then%>
                            <%--<div class="row justify-content-start mb-3">
                                <asp:TextBox class="btn btn-warning" ID="TextBox1" runat="server" ReadOnly="true">ยังไม่บันทึก</asp:TextBox>
                            </div>--%>
                            <div class="alert alert-warning alert-dismissible fade show" role="alert">
                                <strong>ยังไม่บันทึก!</strong> กดปุ่ม Save ด้านบนเพื่อดำเนินการต่อ
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <% End If %>
                            <%-- begin detail row--%>
                            <% if detailtable.Rows(i).Item("jobtypeid") = 1 Then%>
                            <div class="row">
                                <div class="col-md-4 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">ประเภทงาน</span>
                                            <span class="input-group-text" style="width: 550px"><% =detailtable.Rows(i).Item("jobtype") %></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 mb-3 ">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">ตำแหน่งตู้จ่าย</span>
                                            <span class="input-group-text" style="width: 550px"><% =detailtable.Rows(i).Item("assetposition") %></span>
                                        </div>
                                    </div>
                                </div>
                                <% if Not String.IsNullOrEmpty(detailtable.Rows(i).Item("assetname").ToString) Then%>
                                <div class="col-md-4 mb-3">
                                    <div class="input-group sm-3">
                                        <span class="input-group-text" style="width: 550px"><% =detailtable.Rows(i).Item("assetname") %></span>
                                    </div>
                                </div>
                                <% End if %>
                            </div>

                            <% Else If detailtable.Rows(i).Item("jobtypeid") = 16 Then %>
                            <div class="row">
                                <div class="col-lg-auto mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">ประเภทงาน</span>
                                            <span class="input-group-text" style="width: 550px"><% =detailtable.Rows(i).Item("jobtype") %></span>
                                        </div>
                                    </div>
                                </div>
                                <% if Not String.IsNullOrEmpty(detailtable.Rows(i).Item("assetcode").ToString) Then%>
                                <div class="col-lg-auto mb-3">
                                    <div class="row">
                                        <div class="col-md-4 mb-3">
                                            <div class="input-group sm-3">
                                                <div class="input-group-prepend w-100">
                                                    <span class="input-group-text">รหัสทรัพย์สิน</span>
                                                    <span class="input-group-text" style="width: 550px"><% =detailtable.Rows(i).Item("assetcode") %></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4 mb-3">
                                            <div class="input-group sm-3">
                                                <span class="input-group-text" style="width: 550px"><% =detailtable.Rows(i).Item("assetname") %></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <% End if %>
                            </div>
                            <% if Not String.IsNullOrEmpty(detailtable.Rows(i).Item("brand").ToString) Or Not String.IsNullOrEmpty(detailtable.Rows(i).Item("model").ToString) Then%>
                            <div class="row">
                                <% if Not String.IsNullOrEmpty(detailtable.Rows(i).Item("brand").ToString) Then%>
                                <div class="col-md-4 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">ยี่ห้อ</span>
                                            <span class="input-group-text" style="width: 550px"><% =detailtable.Rows(i).Item("brand") %></span>
                                        </div>
                                    </div>
                                </div>
                                <% End if %>
                                <% if Not String.IsNullOrEmpty(detailtable.Rows(i).Item("model").ToString) Then%>
                                <div class="col-md-4 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">รุ่น</span>
                                            <span class="input-group-text" style="width: 550px"><% =detailtable.Rows(i).Item("model") %></span>
                                        </div>
                                    </div>
                                </div>
                                <% End if %>
                            </div>
                            <% End if %>

                            <% Else %>
                            <div class="row">
                                <div class="col-md-auto col-12 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">ประเภทงาน</span>
                                            <span class="input-group-text" style="width: 550px"><% =detailtable.Rows(i).Item("jobtype") %></span>
                                        </div>
                                    </div>
                                </div>
                                <% if Not String.IsNullOrEmpty(detailtable.Rows(i).Item("assetcode").ToString) Then%>
                                <div class="col-lg-auto">
                                    <div class="row">
                                        <div class="col-md-4 mb-3">
                                            <div class="input-group sm-3">
                                                <div class="input-group-prepend w-100">
                                                    <span class="input-group-text">รหัสทรัพย์สิน</span>
                                                    <span class="input-group-text" style="width: 550px"><% =detailtable.Rows(i).Item("assetcode") %></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4 mb-3">
                                            <div class="input-group sm-3">
                                                <span class="input-group-text" style="width: 550px"><% =detailtable.Rows(i).Item("assetname") %></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <% End if %>
                            </div>

                            <% End if %>
                            <div class="row d-none">
                                <div class="col-md-4 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">จำนวน</span>
                                            <span class="input-group-text" style="width: 550px"><% =detailtable.Rows(i).Item("quantity") %></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">หน่วย</span>
                                            <span class="input-group-text" style="width: 550px"><% =detailtable.Rows(i).Item("unit") %></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">ค่าใช้จ่าย (ประมาณ)</span>
                                            <span class="input-group-text" style="width: 170px"><% =detailtable.Rows(i).Item("cost") %></span>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-4 mb-3 d-none">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">Supplier</span>
                                            <span class="input-group-text" style="width: 550px"><% =detailtable.Rows(i).Item("supplier") %></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">ความเร่งด่วน</span>
                                            <span class="input-group-text" style="width: 550px"><% =detailtable.Rows(i).Item("policy") %></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">วันที่ต้องการ</span>
                                            <span class="input-group-text" style="width: 200px; color: red; font-weight: bold"><% =detailtable.Rows(i).Item("requestdate") %></span>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-12 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">เอกสารแนบ</span>
                                            <span class="input-group-text" style="width: 550px">
                                                <% If detailtable.Rows(i).Item("attatch") IsNot Nothing Then %>
                                                <a href="<%=Page.ResolveUrl("http://vpnptec.dyndns.org:10280/OPS_แจ้งซ่อม/" & detailtable.Rows(i).Item("attatch")) %>" target="_blank">
                                                    <% =detailtable.Rows(i).Item("attatch") %>
                                                </a>
                                                <% End if %>
                                            </span>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-8 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">รายละเอียดงาน</span>
                                            <%--                                       <span class="input-group-text" style="width:550px"><% =detailtable.Rows(i).Item("details") %></span>--%>
                                            <label for="detail" style="white-space: pre-wrap;"><% =detailtable.Rows(i).Item("details") %></label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 mb-3">
                                    <% If objStatus = "confirm" Then %>
                                    <button type="button" class="btn btn-sm  btn-primary"
                                        onclick="location.href='../OPS/jobs_followup.aspx?jobno=<% =detailtable.Rows(i).Item("jobno") %>&jobdetailid=<% =detailtable.Rows(i).Item("jobdetailid") %>'">
                                        Followup</button>&nbsp;
                                    <span class="btn btn-sm  btn-danger">สถานะงาน : <% =detailtable.Rows(i).Item("followup_status") %></span>
                                    <% End if %>
                                    <% If objStatus = "edit" And owner = 1 And detailtable.Rows(i).Item("followup_status") = "" Then %>
                                    <button type="button" class="btn btn-sm  btn-danger d-none"
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
                            <% If detailtable IsNot Nothing Then %>
                            <% If detailtable.Rows.Count < 1 Then %>
                            <div class="row">
                                <div class=" col-md-6 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">แจ้งไปยังฝ่าย</span>
                                            <asp:DropDownList ID="cboDepForJobType" class="form-control" runat="server" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>


                                <%--                                <div class="col-md-auto mb-3 d-none">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">หมวดงาน</span>
                                            <asp:DropDownList ID="cboJobCate" class="form-control" runat="server" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-md-auto mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">กลุ่มงาน</span>
                                            <asp:DropDownList ID="cboJobGroup" class="form-control" runat="server" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>--%>

                                <div class="col-md-6 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">ประเภทงาน</span>
                                            <asp:DropDownList ID="cboJobType" class="form-control" runat="server" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <% If cboJobType.SelectedItem.Value = "1" Then %>
                            <div class="row d-none">
                                <div class="col-md-6 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">ตำแหน่งตู้จ่าย</span>
                                            <asp:DropDownList ID="cboPosition" class="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <% End if %>
                            <div class="row">
                                <div class="col-md-6 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">รหัสทรัพย์สิน</span>
                                            <asp:DropDownList class="selectpicker form-control" data-live-search="true" ID="cboAsset" AutoPostBack="True"
                                                runat="server">
                                            </asp:DropDownList>
                                            <%--  แก้ไข FA text box TO cbo--%>

                                            <asp:TextBox class="form-control d-none" ID="txtAssetCode" runat="server" placeholder="FA_CO ..." autocomplete="off"></asp:TextBox>
                                            <div class="input-group-append d-none">
                                                <asp:Button ID="btnFind" class="btn btn-sm  btn-secondary" runat="server" Text="Find" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <div class="input-group sm-3">
                                        <asp:TextBox class="form-control" ID="txtAssetName" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <% If cboJobType.SelectedItem.Value = "16" Then %>
                            <div class="row">
                                <div class="col-md-6 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">ชื่อยี่ห้อ</span>
                                            <asp:TextBox class="form-control" ID="txtBrand" runat="server" placeholder="ตัวอย่าง ( DELL , Brother ) ..." autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">รุ่น</span>
                                            <asp:TextBox class="form-control" ID="txtModel" runat="server" placeholder="ตัวอย่าง ( Latitude 3510 , DCP-7060D ) ..." autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <% End if %>

                            <div class="row d-none">
                                <div class="col-md-4 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">จำนวน</span>
                                            <asp:TextBox class="form-control" ID="txtQuantity" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">หน่วย</span>
                                            <asp:DropDownList ID="cboUnit" class="form-control" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">ค่าใช้จ่าย (ประมาณ)</span>
                                            <asp:TextBox class="form-control" Style="width: 100px" ID="txtCost" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-4 mb-3 d-none">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">Supplier</span>
                                            <asp:DropDownList class="form-control" ID="cboSupplier"
                                                runat="server" Style="width: 100%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">ความเร่งด่วน<h5 class="text-danger m-0">&nbsp;*&nbsp;</h5>
                                            </span>
                                            <asp:DropDownList class="form-control" ID="cboPolicy"
                                                runat="server" AutoPostBack="True" Style="width: 100%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">วันที่ต้องการ</span>
                                            <asp:TextBox class="form-control" ID="txtDueDate" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">เอกสารแนบ</span>
                                        </div>
                                        <asp:Label ID="lblattatch" class="form-control" runat="server" Text=""></asp:Label>
                                        <div class="input-group-append">
                                            <a href="#" id="btnUploadfile" class="btn btn-outline-info" runat="server" title="uploadfile" data-toggle="modal" data-target="#uploadfile"><i class="fas fa-paperclip"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-10 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend w-100">
                                            <span class="input-group-text">รายละเอียดงาน<h5 class="text-danger m-0">&nbsp;*&nbsp;</h5>
                                            </span>

                                            <%--eivog24cbgm1fhi4pm2cg4pw1lp478mhyjjtxnzml4fi51pa--%>
                                            <asp:TextBox class="form-control" ID="txtJobDetail" runat="server" TextMode="MultiLine" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-2 mb-3 d-flex justify-content-center align-items-center">
                                    <asp:Button ID="btnAddDetail" class="btn btn-sm  btn-info" runat="server" Text=" + " OnClientClick="validateData()" />&nbsp;
                                </div>

                            </div>

                            <% End if %>
                            <% End if %>
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

    <div class="modal fade bd-example-modal-lg analy" id="uploadfile" tabindex="-1" role="dialog" aria-labelledby="uploadfileModal" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="uploadfileModal">กรุณาเลือกไฟล์และกดปุ่มอัปโหลด</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 mb-3">
                            <asp:FileUpload ID="FileUpload1" class="btn btn-sm  btn-secondary files" runat="server" text="เลือกไฟล์ " accept="image/*,.pdf" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div id="email-html"></div>
                            <input type="hidden" name="jobitems" id="jobitems" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <%--<button type="button" id="btnAddDetail" class="btn btn-primary noEnterSubmit">Save</button>--%>
                    <asp:Button ID="btnUpload" class="btn btn-primary" runat="server" Text="upload" />
                </div>
            </div>
        </div>
    </div>

    <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>

    <script type="text/javascript">
        //jQuery('[id$=txtDueDate]').datetimepicker({
        //    startDate: '+1971/05/01',//or 1986/12/08
        //    timepicker: true,
        //    onShow: function (ct) {
        //        this.setOptions({
        //            minDate: 0
        //        })
        //    },
        //    scrollInput: false,
        //    format: 'd/m/Y H:i'
        //});
        jQuery('[id$=txtDocDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: true,
            scrollInput: false,
            format: 'd/m/Y H:i'
        });
    </script>


    <!--end datetimepicker ต้องไปทั้งชุด-->
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

        function cancelJobs() {

            /*alert(GridView);*/
            var jobno = document.getElementById('<%= txtJobno.ClientID%>').value
            var usercode = "<%= Session("usercode")%>";

            Swal.fire({
                input: 'textarea',
                inputLabel: 'กรุณาใส่เหตุผลในการยกเลิก',
                inputPlaceholder: 'ใส่ข้อความ . . .',
                inputAttributes: {
                    'aria-label': 'ใส่ข้อความ.'
                },
                preConfirm: (value) => {
                    if (!value) {
                        //if (!document.getElementById('swal2-input').value) {
                        // Handle return value
                        Swal.showValidationMessage('First input missing')
                    }
                },
                showCancelButton: true
            }).then((result) => {
                console.log(result.value);
                if (result.isConfirmed) {
                    var params = "{'jobno': '" + jobno + "','message': '" + result.value + "','updateby': '" + usercode + "'}";
                    console.log(params);
                    $.ajax({
                        type: "POST",
                        url: "../ops/jobs.aspx/CancelByCode",
                        async: true,
                        data: params,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            console.log(msg.d)
                            if (msg.d) {
                                swal.fire({
                                    title: "success!",
                                    text: "",
                                    icon: "success"
                                }).then(function () {
                                    window.location.href = location.href;
                                });
                            } else {
                                alertWarning('fail')
                            }
                        },
                        error: function () {
                            alertWarning('fail')
                        }
                    });
                }
            })

            return false;
        }
    </script>


    <%-- script text editor --%>
    <%--  <script type="text/javascript">
      tinymce.init({
          selector: 'textarea',
          plugins: 'a11ychecker advcode casechange export formatpainter linkchecker autolink lists checklist media mediaembed pageembed permanentpen powerpaste table advtable tinycomments tinymcespellchecker',
          toolbar: 'a11ycheck addcomment showcomments casechange checklist code export formatpainter pageembed permanentpen table',
          toolbar_mode: 'floating',
          tinycomments_mode: 'embedded',
          tinycomments_author: 'Author name',
      });
  </script>--%>
</asp:Content>
