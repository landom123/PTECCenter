<%@ Page Title="Followup" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="Jobs_Followup.aspx.vb" Inherits="PTECCENTER.JobsFollowup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="<%=Page.ResolveUrl("~/css/autocomplete.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/fileupload/dist/font/font-fileuploader.css")%>" rel="stylesheet">

    <link href="<%=Page.ResolveUrl("~/fileupload/dist/jquery.fileuploader.min.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/fileupload/dist/jquery.fileuploader-theme-thumbnails.css")%>" rel="stylesheet">
    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">

    <link href="<%=Page.ResolveUrl("~/css/card_comment.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/css/starRating.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/css/Stepper.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/css/Jobs.css")%>" rel="stylesheet">

    <style>
        html {
            background-color: #f0f2f5 !important;
        }

        .divEditDetail {
            font-size: 1.5rem;
        }


        .fileuploader-theme-thumbnails .fileuploader-items .fileuploader-item .fileuploader-action + .fileuploader-action {
            display: none;
        }

        .fileuploader-popup .fileuploader-popup-footer {
            display: none;
        }

        .fileuploader-theme-thumbnails .fileuploader-thumbnails-input-inner {
            background: #f0cccc;
            border: 2px dashed #ff0000;
            color: #ff0000;
        }


        .modal img {
            display: none;
            background: none;
            border: 0;
        }

        .modal a:hover img {
            width: 100%;
            height: auto;
            position: absolute;
            left: 10%;
            top: -1200%;
            display: block;
            z-index: 999;
        }

        #dataTable tbody tr:not(:first-child):not(:has(td:last-child .row .statusJOBName)) {
            opacity: .4;
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
                <div class="headJobs mb-3" style="background-color: #a32048; color: white; padding: 0.75rem 1rem; border-radius: 0.25rem;">
                    <div class="row justify-content-between">
                        <div class="col text-left align-self-center">
                            ติดตามงาน (Job Follow up)
                        </div>
                        <div class="col text-right align-self-center">
                            <div class="d-flex flex-column align-items-end" readonly="true">
                                <asp:Label CssClass="approvalcode font-weight-bold" ID="lbjobscode" runat="server" Text=''></asp:Label>
                                <a href="#" class="badge badgestatus_app shadow" id="badgeStatus" runat="server"></a>
                            </div>
                        </div>
                        <div class="col d-none">
                            <div id="demo2" style="color: navy; font-size: 10px;"></div>
                            <div id="demo" style="color: navy; font-size: 10px;"></div>
                        </div>
                        <div class="col-auto text-right align-self-center">
                            <a href="JobsLists.aspx" class="btn btn-sm btn-danger">
                                <i class="fa fa-tasks" aria-hidden="true"></i></a>
                        </div>
                    </div>
                </div>
                <div class="row justify-content-between">
                    <div class="col-auto mb-3">
                        <asp:Button ID="btnBack" class="btn btn-sm  btn-danger" runat="server" Text=" back " UseSubmitBehavior="false" />
                    </div>
                    <div class="col  mb-3 d-none d-md-block">
                        <asp:Label ID="txtallOperator" runat="server" ReadOnly="True"></asp:Label>
                    </div>
                    <div class="col mb-3">
                        <div class="row justify-content-end align-items-center">

                            <div class="col-auto">
                                <a class="gtContent6" id="txtCntComment" runat="server" href="#" title="ไปยังแสดงความคิดเห็น"><%= CommentTable.Rows.Count %> <i class="far fa-comment-dots"></i></a>
                            </div>
                            <div class="col-auto">
                                <a class="gtContent5" id="txtCntAttach" runat="server" href="#" title="ไปยังเอกสารแนบ"><%= AttachTable.Rows.Count %> <i class="fas fa-paperclip"></i></a>
                            </div>
                        </div>
                    </div>
                    <div class="col-auto mb-3">
                        <ul class="nav">
                            <li class="nav-item dropdown">
                                <a class="nav-link dropdown-toggle" data-toggle="dropdown" href="#" role="button" aria-haspopup="true" aria-expanded="false"></a>
                                <div class="dropdown-menu">
                                    <a class="dropdown-item" id="gtContent1" href="#" title="ไปยังรายละเอียด">รายละเอียด</a>
                                    <a class="dropdown-item" id="gtContent2" href="#" title="ไปยังSuppiler">ข้อมูลสำหรับวิเคราะห์</a>
                                    <a class="dropdown-item" id="gtContent3" href="#" title="ไปยังSuppiler">Suppiler</a>
                                    <a class="dropdown-item" id="gtContent4" href="#" title="ไปยังคะแนนการประเมิน">คะแนนการประเมิน</a>
                                    <a class="dropdown-item" id="gtContent5" href="#" title="ไปยังเอกสารแนบ">เอกสารแนบ</a>
                                    <a class="dropdown-item" id="gtContent6" href="#" title="ไปยังแสดงความคิดเห็น">แสดงความคิดเห็น</a>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="alert alert-info alert-dismissible fade show " role="alert" id="alApproveHierarchy" runat="server">
                    <div class="row justify-content-between align-items-center">
                        <div class="col-lg-auto ">
                            <span><strong>โปรดตรวจสอบข้อมูล!</strong> "อนุมัติตามสายบังคับบัญชา" กรุณาเลือก "Approve" เพื่อยืนยัน หรือ "Reject" หากต้องการปฏิเสธ</span>
                        </div>
                        <div class="col-lg-auto text-lg-right">
                            <button type="button" class="btn btn-outline-success btn-sm ml-2" onclick="return approveHierachy(getHashParam('jobdetailid'),'hierarchy','<%= Session("userid").ToString %>');"><i class="fas fa-check"></i>อนุมัติ</button>
                            <button type="button" class="btn btn-outline-danger btn-sm ml-2" onclick="return rejectHierachy(getHashParam('jobdetailid'),'hierarchy','<%= Session("userid").ToString %>');"><i class="fas fa-times"></i>ไม่อนุมัติ</button>
                        </div>
                    </div>
                </div>
                <div class="alert alert-info alert-dismissible fade show " role="alert"  id="alApproveManageroperator" runat="server">
                    <div class="row justify-content-between align-items-center">
                        <div class="col-lg-auto ">
                            <span><strong>โปรดตรวจสอบข้อมูล!</strong> "อนุมัติจากหน่วยงาน" กรุณาเลือก "Approve" เพื่อยืนยัน หรือ "Reject" หากต้องการปฏิเสธ</span>
                        </div>
                        <div class="col-lg-auto text-lg-right">
                            <button type="button" class="btn btn-outline-success btn-sm ml-2" onclick="return approveHierachy(getHashParam('jobdetailid'),'manageroperator','<%= Session("userid").ToString %>');"><i class="fas fa-check"></i>อนุมัติ</button>
                            <button type="button" class="btn btn-outline-danger btn-sm ml-2" onclick="return rejectHierachy(getHashParam('jobdetailid'),'manageroperator','<%= Session("userid").ToString %>');"><i class="fas fa-times"></i>ไม่อนุมัติ</button>
                        </div>
                    </div>
                </div>
                <div id="accordion">
                    <div class="card shadow mb-3" id="cardone" runat="server">
                        <div class="card-header" id="headingOne">
                            <h5 class="mb-0">
                                <button class="btn btn-link w-100 text-left collapse__all" id="detailJobs" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne" autopostback="False">
                                    รายละเอียดงาน
                                </button>
                            </h5>
                        </div>
                        <div id="collapseOne" class="collapse multi-collapse show" aria-labelledby="headingOne">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col mb-3 text-left text-md-right divEditDetail" id="divEditDetail">
                                        <a id="btnNozzle" runat="server" href="#" title="ดูข้อมูลมือจ่าย" data-toggle="modal" data-target="#nozzleDetail"><%= nozzletable.Rows.Count %> <i class="fas fa-gas-pump"></i></a>&nbsp;&nbsp;
                                        <a href="#" id="btnEditDetail" runat="server" title="คลิกแก้ไขข้อมูล" data-toggle="modal" data-target="#EditDetail"><i class="fas fa-edit"></i></a>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4 mb-3 d-none d-md-block">
                                        <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">เลขที่เอกสาร</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtJobno" runat="server" ReadOnly="true"></asp:TextBox>
                                            <div class="input-group-append">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4 mb-3">
                                        <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">วันที่แจ้ง</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtDocDate" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4 mb-3">
                                        <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">ผู้แจ้ง</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtOwner" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4 mb-3">
                                        <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">สาขา</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtBranch" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4 mb-3 d-none d-md-block">
                                        <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">ฝ่าย</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtDepartment" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4 mb-3 d-none d-md-block">
                                        <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">แผนก</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtSection" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-4 mb-3">
                                        <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">ประเภทงาน</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtJobType" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4 mb-3">
                                        <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">รหัสทรัพย์สิน</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtAssetCode" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4 mb-3">
                                        <div class="input-group sm-3">
                                            <asp:TextBox class="form-control" ID="txtAssetName" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4 mb-3">
                                        <div class="input-group sm-4">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">ระดับความเร่งด่วน</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtPolicyName" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4 mb-3">
                                        <div class="input-group sm-4">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">กำหนดการ</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtPolicyDate" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4 mb-3 d-none">
                                        <div class="input-group sm-4">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">จำนวน</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtQuantity" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4 mb-3 d-none">
                                        <div class="input-group sm-4">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">หน่วย</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtUnit" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4 mb-3">
                                        <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">Supplier</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtSupplier" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-4 mb-3">
                                        <div class="input-group sm-4">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">ค่าใช้จ่าย (ประมาณ)</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtcosts" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4 mb-3 ">
                                        <div class="input-group sm-4">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">ประเภทงานที่ซ่อม</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtCloseType" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4 mb-3">
                                        <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">ชนิดงาน</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtCloseCategory" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 mb-3">
                                        <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">รายละเอียดงาน</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtDetail" runat="server" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="mb-3">
                                    <div class="table-responsive">
                                        <table class="table table-sm table-bordered " id="dataTable">
                                            <thead class="table-info">
                                                <tr>
                                                    <th class="text-center" style="width: 200px;">สถานะ</th>
                                                    <th class="text-center" style="width: 300px;">รายละเอียด</th>
                                                    <th class="text-center d-none d-md-table-cell" style="width: 200px;">ผู้ปรับปรุง</th>
                                                    <th class="text-center d-none d-md-table-cell" style="width: 200px;">วันที่ปรับปรุง</th>
                                                    <th class="text-center" style="width: 120px;"></th>
                                                </tr>
                                            </thead>
                                            <tbody style="background-color: #f7f7f7;">
                                                <tr class="shadow-sm" style="background-color: white;" id="fromUpdateFollowup" runat="server">
                                                    <td style="vertical-align: middle">
                                                        <asp:DropDownList ID="cboStatus" class="form-control" runat="server"></asp:DropDownList>
                                                    </td>
                                                    <td style="vertical-align: middle">
                                                        <asp:TextBox ID="txtDetailFollow" class="form-control" runat="server" TextMode="MultiLine" Rows="1" required></asp:TextBox>
                                                    </td>
                                                    <td style="vertical-align: middle" class="d-none d-md-table-cell">
                                                        <asp:Label ID="lblCreateBy" class="form-control  text-truncate" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: middle" class="d-none d-md-table-cell">
                                                        <asp:Label ID="lblCreateDate" class="form-control  text-truncate" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td style="vertical-align: middle">
                                                        <div class="d-flex flex-column">
                                                            <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text=" + " OnClientClick="validateData()" UseSubmitBehavior="false" />
                                                            <%--<asp:Button ID="btnConfirm" class="btn btn-sm  btn-warning" runat="server" Text=" + " OnClientClick="validateData()" UseSubmitBehavior="false" />--%>
                                                            <asp:Button ID="btnClose" class="btn btn-sm  btn-danger mt-1" runat="server" Text="ค่าใช้จ่าย" UseSubmitBehavior="false" />
                                                        </div>
                                                    </td>
                                                </tr>
                                                <% For i = 0 To followuptable.Rows.Count - 1 %>
                                                <tr>
                                                    <td><% =followuptable.Rows(i).Item("statusname") %></td>
                                                    <td><% =followuptable.Rows(i).Item("details") %></td>
                                                    <td class="d-none d-md-table-cell"><% =followuptable.Rows(i).Item("createby") %></td>
                                                    <td class="text-center d-none d-md-table-cell"><% =followuptable.Rows(i).Item("createdate") %></td>
                                                    <td class="text-center">
                                                        <% If (i = 0) Then%>
                                                        <%-- <asp:Label ID="lastStatus" CssClass="text-danger" runat="server" Text="(สถานะล่าสุด)" />--%>
                                                        <div class="row justify-content-center align-items-center">
                                                            <div class="col-md-auto">
                                                                <div class="statusJOB" id="stGSM"></div>
                                                            </div>
                                                            <div class="col-md-auto statusJOBName">
                                                                <span id="statusJOBName">Now</span>
                                                            </div>
                                                        </div>
                                                        <%  End If %>
                                                    </td>
                                                </tr>
                                                <% Next i %>
                                            </tbody>
                                        </table>
                                        <%--   status,detail--%>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="card shadow mb-3" id="cardtwo" runat="server">
                        <div class="card-header" id="headingTwo">
                            <h5 class="mb-0">
                                <button class="btn btn-link w-100 text-left collapse__all" type="button" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                    ข้อมูลสำหรับวิเคราะห์
                                </button>
                            </h5>
                        </div>
                        <div id="collapseTwo" class="collapse multi-collapse show" aria-labelledby="headingTwo">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col mb-3 text-left text-md-right divEditDetail">
                                        <a href="#" id="btnDataAnalyCategory" runat="server" title="คลิกแก้ไขข้อมูล" data-toggle="modal" data-target="#dataAnalyCategory"><i class="fas fa-edit"></i></a>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 mb-3">
                                        <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">หมวด</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtCateName" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col mb-3 text-left text-md-right">
                                        <a href="#" id="btnDataAnalyGroupType" runat="server" title="คลิกแก้ไขข้อมูล" data-toggle="modal" data-target="#dataAnalyGroupType"><i class="fas fa-plus"></i></a>
                                    </div>
                                </div>
                                <div class="row">
                                    <%-- <div class="col-md-12 mb-3">
                                        <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">กลุ่ม</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="TextBox2" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>--%>
                                    <div class="col-md-12 mb-3">
                                        <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">ประเภท</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtItems" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="card shadow mb-3" id="cardthree" runat="server">
                        <div class="card-header" id="headingThree">
                            <h5 class="mb-0">
                                <button class="btn btn-link w-100 text-left collapse__all" type="button" data-toggle="collapse" data-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                                    Supplier 
                                </button>
                            </h5>
                        </div>
                        <div id="collapseThree" class="collapse multi-collapse show" aria-labelledby="headingThree">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-12 mb-3">

                                        <input type="button" value="Send" onclick="sendvendor()" id="btnSentSupplier" runat="server" class="btn btn-sm  btn-warning" />
                                        <asp:Button ID="btnPrint" class="btn btn-sm  btn-info" runat="server" Text="Print" UseSubmitBehavior="false" />
                                        <asp:Button ID="btnBackStep" class="btn btn-sm  btn-secondary" runat="server" Text="<" UseSubmitBehavior="false" />
                                        <asp:Button ID="btnNextStep" class="btn btn-sm  btn-secondary" runat="server" Text=">" UseSubmitBehavior="false" />
                                        <asp:Button ID="btnCancelSupplier" class="btn btn-sm  btn-danger" runat="server" Text="Cancel" UseSubmitBehavior="false" />

                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col text-left text-md-right">
                                        <h3 id="txtstkCode" runat="server"></h3>
                                    </div>
                                </div>
                                <div class="row mb-3 ">
                                    <div class="col text-left text-md-right">
                                        <span id="txtSuppilerCode" runat="server"></span>
                                        <span id="txtSuppilerName" runat="server"></span>
                                        <span id="txtCntSupplier" runat="server"></span>
                                    </div>
                                </div>
                                <div class="row flex-md-row-reverse mb-3 ">
                                    <div class="col-auto text-left">
                                        <div class="input-group mb-3" id="groupAddSupplier" runat="server">
                                            <asp:DropDownList class="form-control d-none" ID="cboVendor" runat="server"></asp:DropDownList>
                                            <input type="text" id="txtContractorName" class="form-control" placeholder="เพิ่มชื่อผู้รับเหมาที่เข้าทำงาน">
                                            <button class="btn btn-primary" onclick="addContractor('<%= Session("usercode") %>')">เพิ่ม</button>
                                        </div>
                                        <ol id="contractorList"></ol>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col overflow-auto">
                                        <div class="md-stepper-horizontal orange">
                                            <% For i = 0 To stepsuppilertable.Rows.Count - 1 %>
                                            <div class="md-step <% If stepsuppilertable.Rows(i).Item("actived").ToString = "1" Then %>active<% else if stepsuppilertable.Rows(i).Item("actived").ToString = "2" %>doing<% End if %>">
                                                <div class="md-step-circle"><span><%= stepsuppilertable.Rows(i).Item("rownumber").ToString() %></span></div>
                                                <div class="md-step-title"><%= stepsuppilertable.Rows(i).Item("steptitle").ToString() %></div>
                                                <div class="md-step-optional"><%= stepsuppilertable.Rows(i).Item("stepdate").ToString() %></div>
                                                <div class="md-step-bar-left"></div>
                                                <div class="md-step-bar-right"></div>
                                            </div>
                                            <% Next i %>
                                        </div>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 mb-3">
                                        <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">วันที่เริ่มต้นการปฏิบัติงาน</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtSupplierBeginDate" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">วันที่สิ้นสุดการปฏิบัติงาน</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtSupplierEndDate" runat="server" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-12 mb-3">
                                        <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">รายละเอียดการปฏิบัติงาน (ผู้รับเหมา)</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtEndComment" runat="server" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="card shadow mb-3" id="cardfour" runat="server">
                        <div class="card-header" id="headingFour">
                            <h5 class="mb-0">
                                <button class="btn btn-link w-100 text-left collapse__all" type="button" data-toggle="collapse" data-target="#collapseFour" aria-expanded="false" aria-controls="collapseFour">
                                    คะแนนการประเมิน
                                </button>
                            </h5>
                        </div>
                        <div id="collapseFour" class="collapse multi-collapse show" aria-labelledby="headingFour">
                            <div class="card-body">
                                <!-- Rating -->
                                <div class="rating__main">
                                    <% If Not maintable.Rows(0).Item("supplierid") = 0 Then %>
                                    <!-- rate__Service -->
                                    <div class="rate__Service">
                                        <!-- Rating totol -->
                                        <div class="row m-auto align-items-center justify-content-center">
                                            <div class="col-12 col-md-9 mb-3">
                                                <div class="row">
                                                    <div class="col mb-3 text-center">
                                                        <h4>การประเมินโดยรวม (ทีมช่าง)</h4>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col mb-3 text-center">
                                                        <h3 class="font-weight-bold"><span id="txtAvg_Service"></span></h3>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col mb-3">
                                                        <div id="p__all_Service" class="rating mx-auto">
                                                            <span class="rating__result"></span>
                                                            <i class="rating__star fas fa-star bg-gray"></i>
                                                            <i class="rating__star fas fa-star bg-gray"></i>
                                                            <i class="rating__star fas fa-star bg-gray"></i>
                                                            <i class="rating__star fas fa-star bg-gray"></i>
                                                            <i class="rating__star fas fa-star bg-gray"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 col-md-9   mb-3">
                                                <div class="row font-weight-bold justify-content-center justify-content-lg-start">
                                                    <div class="col-1">
                                                        <span>5</span>
                                                    </div>
                                                    <div class="col-4">
                                                        <span>ดีมาก</span>
                                                    </div>
                                                    <div class="col-auto">
                                                        <progress id="5star_Service" value="0" max="10"></progress>
                                                    </div>
                                                </div>
                                                <div class="row font-weight-bold justify-content-center justify-content-lg-start">
                                                    <div class="col-1">
                                                        <span>4</span>
                                                    </div>
                                                    <div class="col-4">
                                                        <span>ดี</span>
                                                    </div>
                                                    <div class="col-auto">
                                                        <progress id="4star_Service" value="0" max="10"></progress>
                                                    </div>
                                                </div>
                                                <div class="row font-weight-bold justify-content-center justify-content-lg-start">
                                                    <div class="col-1">
                                                        <span>3</span>
                                                    </div>
                                                    <div class="col-4">
                                                        <span>ปานกลาง</span>
                                                    </div>
                                                    <div class="col-auto">
                                                        <progress id="3star_Service" value="0" max="10"></progress>
                                                    </div>
                                                </div>
                                                <div class="row font-weight-bold justify-content-center justify-content-lg-start">
                                                    <div class="col-1">
                                                        <span>2</span>
                                                    </div>
                                                    <div class="col-4">
                                                        <span>พอใช้</span>
                                                    </div>
                                                    <div class="col-auto">
                                                        <progress id="2star_Service" value="0" max="10"></progress>
                                                    </div>
                                                </div>
                                                <div class="row font-weight-bold justify-content-center justify-content-lg-start">
                                                    <div class="col-1">
                                                        <span>1</span>
                                                    </div>
                                                    <div class="col-4">
                                                        <span>ควรปรับปรุง</span>
                                                    </div>
                                                    <div class="col-auto">
                                                        <progress id="1star_Service" value="0" max="10"></progress>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- end Rating totol -->

                                        <!-- Rating table -->
                                        <div class="row m-auto justify-content-center">
                                            <div class="col-12 col-md-9 mb-3">
                                                <table class="table " id="myTableTopic_Service">
                                                    <thead>
                                                        <tr>
                                                            <th rowspan="2" class="text-center align-middle d-block d-md-table-cell">หัวข้อประเมิน</th>
                                                            <th colspan="5" class="text-center d-none d-md-table-cell">ระดับความพึงพอใจ</th>
                                                        </tr>
                                                        <tr class="d-flex justify-content-center align-items-center border border-1" style="gap: 0.4em; border-top: 0px">
                                                            <th class="border-top-0 d-block d-md-table-cell">1</th>
                                                            <th class="border-top-0 d-block d-md-table-cell">2</th>
                                                            <th class="border-top-0 d-block d-md-table-cell">3</th>
                                                            <th class="border-top-0 d-block d-md-table-cell">4</th>
                                                            <th class="border-top-0 d-block d-md-table-cell">5</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <% For i = 0 To assessmenttable.Rows.Count - 1 %>
                                                        <% If assessmenttable.Rows(i).Item("TopicGroup_id").ToString() = "2" Then %>
                                                        <tr>
                                                            <td class="d-block d-md-table-cell mt-4 border-top border-bottom-0 border-md-top border-md-bottom"><%= assessmenttable.Rows(i).Item("topic_detail").ToString() %></td>
                                                            <td class="d-block d-md-table-cell border-top-0 border-bottom-0 border-md-top border-md-bottom">

                                                                <% If assessmenttable.Rows(i).Item("Type").ToString() = "rate" Then %>
                                                                <div id="<%= assessmenttable.Rows(i).Item("topic_id").ToString() %>" class="rating_Service rating mx-auto">
                                                                    <span class="rating__result"></span>
                                                                    <i class="rating__star fas fa-star bg-gray"></i>
                                                                    <i class="rating__star fas fa-star bg-gray"></i>
                                                                    <i class="rating__star fas fa-star bg-gray"></i>
                                                                    <i class="rating__star fas fa-star bg-gray"></i>
                                                                    <i class="rating__star fas fa-star bg-gray"></i>
                                                                </div>
                                                                <% Else if assessmenttable.Rows(i).Item("Type").ToString() = "text" Then %>

                                                                <% If maintable.Rows(0).Item("followup_status") <> "ปิดงาน" Then %>
                                                                <textarea class="form-control" id="txtOther_<%= assessmenttable.Rows(i).Item("topic_id").ToString() %>" rows="1"></textarea>
                                                                <% Else %>
                                                                <textarea class="form-control" id="txtOther_<%= assessmenttable.Rows(i).Item("topic_id").ToString() %>" rows="1" readonly><%= assessmenttable.Rows(i).Item("message").ToString() %></textarea>
                                                                <% End if %>
                                                                <% End if %>
                                                            </td>
                                                        </tr>
                                                        <% End if %>
                                                        <% Next i %>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <!-- end Rating table -->
                                    </div>
                                    <!-- end rate__Service -->

                                    <hr />
                                    <% End if %>
                                    <!-- rate__Operator -->
                                    <div class="rate__Operator">
                                        <!-- Rating totol -->
                                        <div class="row m-auto align-items-center justify-content-center">
                                            <div class="col-12 col-md-9 mb-3">
                                                <div class="row">
                                                    <div class="col mb-3 text-center">
                                                        <h4>การประเมินโดยรวม (Operator)</h4>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col mb-3 text-center">
                                                        <h3 class="font-weight-bold"><span id="txtAvg_Operator"></span></h3>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col mb-3">
                                                        <div id="p__all_Operator" class="rating mx-auto">
                                                            <span class="rating__result"></span>
                                                            <i class="rating__star fas fa-star bg-gray"></i>
                                                            <i class="rating__star fas fa-star bg-gray"></i>
                                                            <i class="rating__star fas fa-star bg-gray"></i>
                                                            <i class="rating__star fas fa-star bg-gray"></i>
                                                            <i class="rating__star fas fa-star bg-gray"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 col-md-9  mb-3">
                                                <div class="row font-weight-bold justify-content-center justify-content-lg-start">
                                                    <div class="col-1">
                                                        <span>5</span>
                                                    </div>
                                                    <div class="col-4">
                                                        <span>ดีมาก</span>
                                                    </div>
                                                    <div class="col-auto">
                                                        <progress id="5star_Operator" value="0" max="10"></progress>
                                                    </div>
                                                </div>
                                                <div class="row font-weight-bold justify-content-center justify-content-lg-start">
                                                    <div class="col-1">
                                                        <span>4</span>
                                                    </div>
                                                    <div class="col-4">
                                                        <span>ดี</span>
                                                    </div>
                                                    <div class="col-auto">
                                                        <progress id="4star_Operator" value="0" max="10"></progress>
                                                    </div>
                                                </div>
                                                <div class="row font-weight-bold justify-content-center justify-content-lg-start">
                                                    <div class="col-1">
                                                        <span>3</span>
                                                    </div>
                                                    <div class="col-4">
                                                        <span>ปานกลาง</span>
                                                    </div>
                                                    <div class="col-auto">
                                                        <progress id="3star_Operator" value="0" max="10"></progress>
                                                    </div>
                                                </div>
                                                <div class="row font-weight-bold justify-content-center justify-content-lg-start">
                                                    <div class="col-1">
                                                        <span>2</span>
                                                    </div>
                                                    <div class="col-4">
                                                        <span>พอใช้</span>
                                                    </div>
                                                    <div class="col-auto">
                                                        <progress id="2star_Operator" value="0" max="10"></progress>
                                                    </div>
                                                </div>
                                                <div class="row font-weight-bold justify-content-center justify-content-lg-start">
                                                    <div class="col-1">
                                                        <span>1</span>
                                                    </div>
                                                    <div class="col-4">
                                                        <span>ควรปรับปรุง</span>
                                                    </div>
                                                    <div class="col-auto">
                                                        <progress id="1star_Operator" value="0" max="10"></progress>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- end Rating totol -->
                                        <!-- Rating table -->
                                        <div class="row m-auto justify-content-center">
                                            <div class="col-12 col-md-9 mb-3">
                                                <table class="table " id="myTableTopic_Operator">
                                                    <thead>
                                                        <tr>
                                                            <th rowspan="2" class="text-center align-middle d-block d-md-table-cell">หัวข้อประเมิน</th>
                                                            <th colspan="5" class="text-center d-none d-md-table-cell">ระดับความพึงพอใจ</th>
                                                        </tr>
                                                        <tr class="d-flex justify-content-center align-items-center border border-1" style="gap: 0.4em; border-top: 0px">
                                                            <th class="border-top-0 d-block d-md-table-cell">1</th>
                                                            <th class="border-top-0 d-block d-md-table-cell">2</th>
                                                            <th class="border-top-0 d-block d-md-table-cell">3</th>
                                                            <th class="border-top-0 d-block d-md-table-cell">4</th>
                                                            <th class="border-top-0 d-block d-md-table-cell">5</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <% For i = 0 To assessmenttable.Rows.Count - 1 %>
                                                        <% If assessmenttable.Rows(i).Item("TopicGroup_id").ToString() = "1" Then %>
                                                        <tr>
                                                            <td class="d-block d-md-table-cell mt-4 border-top border-bottom-0 border-md-top border-md-bottom"><%= assessmenttable.Rows(i).Item("topic_detail").ToString() %></td>
                                                            <td class="d-block d-md-table-cell border-top-0 border-bottom-0 border-md-top border-md-bottom">

                                                                <% If assessmenttable.Rows(i).Item("Type").ToString() = "rate" Then %>
                                                                <div id="<%= assessmenttable.Rows(i).Item("topic_id").ToString() %>" class="rating_Operator rating mx-auto">
                                                                    <span class="rating__result"></span>
                                                                    <i class="rating__star fas fa-star bg-gray"></i>
                                                                    <i class="rating__star fas fa-star bg-gray"></i>
                                                                    <i class="rating__star fas fa-star bg-gray"></i>
                                                                    <i class="rating__star fas fa-star bg-gray"></i>
                                                                    <i class="rating__star fas fa-star bg-gray"></i>
                                                                </div>
                                                                <% Else if assessmenttable.Rows(i).Item("Type").ToString() = "text" Then %>
                                                                <% If maintable.Rows(0).Item("followup_status") <> "ปิดงาน" Then %>
                                                                <textarea class="form-control" id="txtOther_<%= assessmenttable.Rows(i).Item("topic_id").ToString() %>" rows="1"></textarea>
                                                                <% Else %>
                                                                <textarea class="form-control" id="txtOther_<%= assessmenttable.Rows(i).Item("topic_id").ToString() %>" rows="1" readonly><%= assessmenttable.Rows(i).Item("message").ToString() %></textarea>
                                                                <% End if %>
                                                                <% End if %>
                                                            </td>
                                                        </tr>
                                                        <% End if %>
                                                        <% Next i %>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <!-- end Rating table -->
                                    </div>
                                    <!-- end rate__Operator -->
                                </div>
                                <!-- end Rating -->
                                <% If nozzletable IsNot Nothing Then%>
                                <% If nozzletable.Rows.Count > 0 Then%>
                                <div class="row m-auto justify-content-center">
                                    <div class="col-12 col-md-9">
                                        <div class="table-responsive-xl nozzle__management">
                                            <table class="table table-hover table-bordered table-sm">
                                                <thead class="table-info">
                                                    <tr>
                                                        <th class="text-center">ลำดับที่</th>
                                                        <th class="text-center">ยี่ห้อ(ชนิด)</th>
                                                        <th class="text-center">ตำแหน่ง</th>
                                                        <th class="text-center">เลขที่มาตร</th>
                                                        <th class="text-center">วันสิ้นคำรับรอง</th>
                                                        <th class="text-center">รูปภาพ</th>
                                                        <th class="text-center">เลขที่มาตร(ใหม่)</th>
                                                        <th class="text-center">วันสิ้นคำรับรอง(ใหม่)</th>
                                                        <% If maintable.Rows(0).Item("followup_status") = "ปิดงาน" Then %>
                                                        <th class="text-center">แก้ไขเมื่อวันที่</th>
                                                        <% Else %>
                                                        <th></th>
                                                        <% End if %>
                                                    </tr>
                                                </thead>
                                                <tbody class="text-center align-bottom">
                                                    <% If nozzletable.Rows.Count > 0 Then%>
                                                    <% For j = 0 To nozzletable.Rows.Count - 1 %>
                                                    <tr style="cursor: pointer;"
                                                        <% If maintable.Rows(0).Item("followup_status") <> "ปิดงาน" Then %>
                                                        ondblclick="btnEditDetailClick('<%= nozzletable.Rows(j).Item("rownumber").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("nozzle_id").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("nozzle_no").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("brand").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("expirydate").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("producttype").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("positiononassest").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("nozzle_No_new").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("expirydate_new").ToString() %>'
                                                                        );"
                                                        <% End if %>>
                                                        <td>
                                                            <span><%= nozzletable.Rows(j).Item("rownumber").ToString %></span>
                                                        </td>
                                                        <td>
                                                            <div class="row">
                                                                <div class="col">
                                                                    <span><%= nozzletable.Rows(j).Item("brand").ToString %></span>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col">
                                                                    (<span><%= nozzletable.Rows(j).Item("producttype").ToString %></span>)

                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <span><%= nozzletable.Rows(j).Item("positionOnAssest").ToString %></span>
                                                        </td>
                                                        <td>
                                                            <span class="text-nowrap"><%= nozzletable.Rows(j).Item("nozzle_No").ToString %></span>
                                                        </td>
                                                        <td>
                                                            <span class="text-nowrap"><%= nozzletable.Rows(j).Item("expirydate").ToString %></span>
                                                        </td>
                                                        <td>
                                                            <a href="<%= nozzletable.Rows(j).Item("url").ToString %>" target="_blank">รูปภาพ</a>
                                                        </td>
                                                        <td>
                                                            <span class="text-nowrap highlighter-rouge"><%= nozzletable.Rows(j).Item("nozzle_No_new").ToString %></span>
                                                        </td>
                                                        <td>
                                                            <span class="text-nowrap highlighter-rouge"><%= nozzletable.Rows(j).Item("expirydate_new").ToString %></span>
                                                        </td>
                                                        <% If maintable.Rows(0).Item("followup_status") = "ปิดงาน" Then %>
                                                        <td class="text-primary">
                                                            <span><%= nozzletable.Rows(j).Item("updatedate").ToString %></span>
                                                        </td>
                                                        <% Else %>
                                                        <td>
                                                            <a title="คลิกแก้ไขข้อมูล"
                                                                onclick="btnEditDetailClick('<%= nozzletable.Rows(j).Item("rownumber").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("nozzle_id").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("nozzle_no").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("brand").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("expirydate").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("producttype").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("positiononassest").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("nozzle_No_new").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("expirydate_new").ToString() %>'
                                                                        );">
                                                                <i class="fas fa-edit color__purple"></i>
                                                            </a>&nbsp;&nbsp;
                                                        </td>
                                                        <% End if %>
                                                    </tr>

                                                    <% Next j %>
                                                    <% End if %>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                                <% If maintable.Rows(0).Item("followup_status") <> "ปิดงาน" Then %>
                                <div class="alert alert-warning alert-dismissible fade show mt-3" role="alert">
                                    <strong>กรุณา ตรวจสอบ/แก้ไข/แนบรูป ข้อมูลมือจ่าย ให้เป็นปัจจุบันก่อนกด 'รับงาน'</strong>
                                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <% End if %>
                                <% End if %>
                                <% End if %>
                                <div class="row">
                                    <div class="col-12 mb-3 text-center">
                                        <asp:Button ID="btnSubmitRate" class="btn btn-success" runat="server" Text="รับงาน" autopostback="False" OnClientClick="validateRate(); " UseSubmitBehavior="false" />

                                        <button runat="server" id="btndisAccept" name="btnEdit" onclick="return disAccept();" class="btn btn-danger">
                                            ไม่รับงาน
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <hr />
                </div>
                <div class="row notPrint" id="card_attatch">
                    <div class="col-md-6 mt-3">
                        <div class="card shadow card_attatch">
                            <div class="card-header">
                                เอกสารแนบ
                            </div>
                            <div class="card-body attatchItems">
                                <%--begin Attatch item--%>
                                <% For i = 0 To AttachTable.Rows.Count - 1 %>
                                <div class="row">
                                    <div class="attatchItems-link-btndelete" id="ATT<%= AttachTable.Rows(i).Item("id") %>">
                                        <div class="col-auto">
                                            <a href="<%= Page.ResolveUrl(AttachTable.Rows(i).Item("url").ToString()) %>" class="text-primary" style="cursor: pointer;" target="_blank">
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
                    <div class="col-md-6 mt-3" id="card_comment">
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
                                            <asp:Button ID="btnSaveComment" class="btn btn-primary w-100" runat="server" Text="Post" AutoPostBack="True" UseSubmitBehavior="false" disabled />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%-- end item--%>
                        </div>
                        <!-- end card-->
                    </div>
                </div>
                <a id="back-to-top" href="#" class="btn btn-light btn-lg back-to-top shadow" role="button"><i class="fas fa-chevron-up"></i></a>
            </div>
            <!-- end content-wrapper -->
            <!-- end เนื้อหาเว็บ -->
        </div>
            <!-- /#wrapper -->
    </div>
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog " role="document">
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
                        <asp:Label ID="Label8" CssClass="form-label" AssociatedControlID="txtbrand" runat="server" Text="ยี่ห้อ" />
                        <asp:TextBox class="form-control form-control-sm" type="input" ID="txtbrand" runat="server" autocomplete="off"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label9" CssClass="form-label" AssociatedControlID="txtproducttype" runat="server" Text="ชนิดน้ำมัน" />
                        <asp:TextBox class="form-control form-control-sm" type="input" ID="txtproducttype" runat="server" autocomplete="off"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label11" CssClass="form-label" AssociatedControlID="txtpositiononassest" runat="server" Text="ตำแหน่ง" />
                        <asp:TextBox class="form-control form-control-sm" type="input" ID="txtpositiononassest" runat="server" autocomplete="off"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label10" CssClass="form-label mandatory" AssociatedControlID="txtnozzle_no" runat="server" Text="เลขที่มาตรใหม่" />
                        <asp:TextBox class="form-control form-control-sm highlighter-rouge" type="input" ID="txtnozzle_no" aria-describedby="txtnozzle_no_old" runat="server" autocomplete="off"></asp:TextBox>
                        <small id="txtnozzle_no_old" class="form-text text-muted"></small>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label12" CssClass="form-label mandatory" AssociatedControlID="txtexpirydate" runat="server" Text="วันสิ้นคำรับรองใหม่" />
                        <asp:TextBox class="form-control form-control-sm highlighter-rouge" type="input" ID="txtexpirydate" aria-describedby="txtexpirydate_old" runat="server" autocomplete="off"></asp:TextBox>
                        <small id="txtexpirydate_old" class="form-text text-muted"></small>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col">
                                <u><a class="" style="color: #c0c0c0; font-size: .8rem;">(ตัวอย่างรูปภาพที่จะต้องแนบ <i class="fas fa-info-circle"></i>)
                                    <img src="../../../icon/nozzle_pace.jpg" class="img-fluid img-thumbnail" />
                                </a>
                                </u>
                            </div>
                        </div>
                    </div>
                    <div class="file_att">
                        <input type="file" name="files__nozzle" id="file_att" accept="image/*,.pdf" data-fileuploader-listinput="file_att" data-fileuploader-limit="1" data-fileuploader-files=''>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary noEnterSubmit" data-dismiss="modal">Close</button>
                    <asp:Button ID="btnAddDetails" class="btn btn-primary" runat="server" Text="Save" OnClientClick="postBack_addDetail();" UseSubmitBehavior="false" />
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
                                <asp:DropDownList class="form-control" ID="cboMyfile" runat="server"></asp:DropDownList>
                                <div class="invalid-feedback">กรุณาเลือกไฟล์</div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <%--<button type="button" id="btnAddDetail" class="btn btn-primary noEnterSubmit">Save</button>--%>
                    <asp:Button ID="asd" class="btn btn-primary" runat="server" Text="Save" OnClientClick="chooseMyfile(); return false;" UseSubmitBehavior="false" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade bd-example-modal-lg" id="EditDetail" tabindex="-1" role="dialog" aria-labelledby="editDetailModal" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="editDetailModal">แก้ไข รายละเอียดงาน</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 mb-3">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ประเภทงาน</span>
                                </div>
                                <asp:DropDownList ID="cboJobType" class="form-control" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-12 mb-3">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ประเภทงานที่ซ่อม</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboCloseType" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-12 mb-3">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ระดับความเร่งด่วน</span>
                                </div>
                                <asp:DropDownList ID="cboPolicy" class="form-control" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-12 mb-3">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">กำหนดการ</span>
                                </div>
                                <asp:TextBox class="form-control" ID="txtPolicyRequestdate" runat="server" placeholder="" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>


                        <div class="col-md-12 mb-3">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Supplier</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboSupplier"
                                    runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-12 mb-3">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ชนิดงาน</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboCloseCategory" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-12 mb-3 d-none">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ประเภทหมวดราคา</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboJobCenter"
                                    runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-12 mb-3">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">รหัสทรัพย์สิน</span>
                                </div>
                                <asp:TextBox class="form-control" ID="txtAsset" runat="server" placeholder="" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12">
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
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <%--<button type="button" id="btnAddDetail" class="btn btn-primary noEnterSubmit">Save</button>--%>
                    <asp:Button ID="btnUpdate" class="btn btn-primary" runat="server" Text="Update" OnClientClick="postBack_updateJFU();" UseSubmitBehavior="false" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade bd-example-modal-lg" id="assetsNozzleDetail" tabindex="-1" role="dialog" aria-labelledby="assetsNozzleDetailModal" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="assetsNozzleDetailModal">รายละเอียดมือจ่ายประจำสาขา</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body nozzle__management table-responsive-xl">
                    <asp:GridView ID="gvAssetsNozzle"
                        class="table table-sm table-hover table-bordered"
                        AllowSorting="True"
                        AllowPaging="false"
                        AutoGenerateColumns="false"
                        runat="server">
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-CssClass="text-center table-header table-info " ItemStyle-Width="50px" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" runat="server"
                                        onclick="checkAll(this);" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" data-key='<%#Eval("positionOnAssest").ToString + "," + Eval("nozzle_No").ToString%>'
                                        onclick="Check_Click(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ลำดับที่" HeaderStyle-CssClass="table-header table-info text-center " HeaderStyle-HorizontalAlign="center" ItemStyle-CssClass="" ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <asp:Label ID="lbrownumber" runat="server" Text='<%#Eval("rownumber")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ยี่ห้อ" HeaderStyle-CssClass="table-header table-info text-center " HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <asp:Label ID="lbbrand" runat="server" Text='<%#Eval("brand")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ชนิดน้ำมัน" HeaderStyle-CssClass="table-header table-info text-center " HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <asp:Label ID="lbproducttype" runat="server" Text='<%#Eval("producttype")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="เลขที่มาตร" HeaderStyle-CssClass="table-header table-info text-center " HeaderStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <asp:Label ID="lbnozzleno" runat="server" Text='<%#Eval("nozzle_No")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ตำแหน่ง" HeaderStyle-CssClass="table-header table-info text-center " HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <asp:Label ID="lbpositionOnAssest" runat="server" Text='<%#Eval("positionOnAssest")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="วันสิ้นคำรับรอง" HeaderStyle-CssClass="table-header table-info text-center " HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <asp:Label ID="lbexpirydate" runat="server" Text='<%#Eval("expirydate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <asp:Button ID="btnSetNozzle" class="btn btn-primary" runat="server" Text="Save changes" OnClientClick="setSelected();" UseSubmitBehavior="false" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade bd-example-modal-lg analy" id="dataAnalyCategory" tabindex="-1" role="dialog" aria-labelledby="dataAnalyCategoryModal" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="dataAnalyCategoryModal">ข้อมูลสำหรับวิเคราะห์</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 mb-3">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">หมวดงานที่ซ่อม</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboJobCate" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <%--<button type="button" id="btnAddDetail" class="btn btn-primary noEnterSubmit">Save</button>--%>
                    <asp:Button ID="btnUpdateJobCateCode" class="btn btn-primary" runat="server" Text="Update" UseSubmitBehavior="false" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade bd-example-modal-lg analy" id="SupplierName" tabindex="-1" role="dialog" aria-labelledby="SupplierNameModal" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="SupplierNameModal">รายชื่อผู้รับเหมา</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col">
                            <%-- <div class="input-group mb-3">
                                <input type="text" id="contractorName" class="form-control" placeholder="เพิ่มชื่อผู้รับเหมา">
                                <button class="btn btn-primary" onclick="addContractor()">เพิ่ม</button>
                            </div>
                            <ul id="contractorList" class="list-group"></ul>--%>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <%--<button type="button" id="btnAddDetail" class="btn btn-primary noEnterSubmit">Save</button>--%>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade bd-example-modal-lg analy" id="dataAnalyGroupType" tabindex="-1" role="dialog" aria-labelledby="dataAnalyGroupTypeModal" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="dataAnalyGroupTypeModal">ข้อมูลสำหรับวิเคราะห์</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12 mb-3">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ประเภทงานที่ซ่อม</span>
                                </div>
                                <asp:DropDownList class="form-control cbomulti" ID="cboJobItems" runat="server" multiple>
                                </asp:DropDownList>
                            </div>
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
                    <asp:Button ID="btnUpdateJobitems" class="btn btn-primary" runat="server" Text="Update" UseSubmitBehavior="false" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade bd-example-modal-lg" id="suppilerDetail" tabindex="-1" role="dialog" aria-labelledby="suppilerDetailModal" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="suppilerDetailModal">รายละเอียดการปฏิบัติงาน</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col">
                            <div class="form-group">
                                <asp:Label ID="lbbegindate" CssClass="form-label" AssociatedControlID="txtbegindate" runat="server" Text="เริ่มต้นการปฏิบัติงาน" />
                                <asp:TextBox class="form-control noEnterSubmit" type="input" ID="txtbegindate" runat="server" placeholder="--- คลิกเพื่อเลือก ---" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col">
                            <div class="form-group">
                                <asp:Label ID="lbenddate" CssClass="form-label" AssociatedControlID="txtenddate" runat="server" Text="สิ้นสุดการปฏิบัติงาน" />
                                <asp:TextBox class="form-control noEnterSubmit" type="input" ID="txtenddate" runat="server" placeholder="--- คลิกเพื่อเลือก ---" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbDetail" CssClass="form-label" AssociatedControlID="txtDetail" runat="server" Text="รายละเอียดการปฏิบัติงาน" />
                        <asp:TextBox class="form-control" ID="txtSuppilerDetail" runat="server" Rows="3" Columns="50" TextMode="MultiLine" autocomplete="off"></asp:TextBox>
                        <div class="invalid-feedback">กรุณากรอกรายละเอียด</div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <%--<button type="button" id="btnAddDetail" class="btn btn-primary noEnterSubmit">Save</button>--%>
                    <asp:Button ID="btnSuppilerSubmit" class="btn btn-primary" runat="server" Text="Save" UseSubmitBehavior="false" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade bd-example-modal-lg" id="nozzleDetail" tabindex="-1" role="dialog" aria-labelledby="nozzleDetailModal" aria-hidden="true">
        <div class="modal-dialog modal-xl" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="nozzleDetailModal">รายละเอียดมือจ่ายในงาน</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body nozzle__management table-responsive-xl" style="font-size: .75rem;">
                    <asp:GridView ID="gvNozzle"
                        class="table table-sm thead-dark table-bordered"
                        AllowSorting="True"
                        AllowPaging="false"
                        AutoGenerateColumns="false"
                        runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="ลำดับ" HeaderStyle-CssClass="table-header table-info text-center " HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="" ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <asp:Label ID="lbid" runat="server" Text='<%#Eval("rownumber")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ยี่ห้อ (ชนิด)" HeaderStyle-CssClass="table-header table-info text-center " HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <div class="d-flex flex-column align-items-center">
                                        <asp:Label ID="lbbranch" runat="server" Text='<%#Eval("brand") %>'></asp:Label>
                                        <asp:Label ID="Label1" runat="server" Text='<%#" (" & Eval("producttype") & ")" %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ตำแหน่ง" HeaderStyle-CssClass="table-header table-info text-center " HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <asp:Label ID="lbdetailpayment" runat="server" Text='<%#Eval("positionOnAssest")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="เลขที่มาตร(เก่า)" HeaderStyle-CssClass="table-header table-info text-center " HeaderStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <asp:Label ID="lbdetail" runat="server" Text='<%#Eval("nozzle_No")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="วันสิ้นคำรับรอง(เก่า)" HeaderStyle-CssClass="table-header table-info text-center " HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <asp:Label ID="lbdetailpayment" runat="server" Text='<%#Eval("expirydate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รูปภาพ" HeaderStyle-CssClass="table-header table-info text-center " HeaderStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <a href="<%#Eval("url")%>" target="_blank">รูปภาพ</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="เลขที่มาตร(ใหม่)" HeaderStyle-CssClass="table-header table-info text-center " HeaderStyle-HorizontalAlign="center" ItemStyle-CssClass="highlighter-rouge">
                                <ItemTemplate>
                                    <asp:Label ID="lbdetail" runat="server" Text='<%#Eval("nozzle_No_new")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="วันสิ้นคำรับรอง(ใหม่)" HeaderStyle-CssClass="table-header table-info text-center " HeaderStyle-HorizontalAlign="center" ItemStyle-CssClass="highlighter-rouge" ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <asp:Label ID="lbdetailpayment" runat="server" Text='<%#Eval("expirydate_new")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="อัปเดทล่าสุด" HeaderStyle-CssClass="table-header table-info text-center " HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" ItemStyle-CssClass="">
                                <ItemTemplate>
                                    <div class="d-flex flex-column align-items-center">
                                        <asp:Label ID="lbupdateby" CssClass="text-muted" runat="server" Text='<%#Eval("UpdateByCode")%>'></asp:Label>
                                        <asp:Label ID="lbupdatedate" runat="server" Text='<%#Eval("UpdateDate")%>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" HeaderStyle-CssClass="table-header table-info text-center " HeaderStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <a title="คลิกแก้ไขข้อมูล" data-dismiss="modal"
                                        onclick="btnEditDetailClick('<%#Eval("rownumber") %>'
                                                                        ,'<%#Eval("nozzle_id") %>'
                                                                        ,'<%#Eval("nozzle_no") %>'
                                                                        ,'<%#Eval("brand") %>'
                                                                        ,'<%#Eval("expirydate") %>'
                                                                        ,'<%#Eval("producttype") %>'
                                                                        ,'<%#Eval("positiononassest") %>'
                                                                        ,'<%#Eval("nozzle_No_new") %>'
                                                                        ,'<%#Eval("expirydate_new") %>'
                                                                        );">
                                        <i class="fas fa-edit color__purple"></i>
                                    </a>&nbsp;&nbsp;
                                    <a onclick="confirmDeleteNozzle('<%#Eval("nozzle_id") %>','<%#Eval("rownumber") %>')" class="btn btn-sm p-0 notPrint">
                                        <i class="fas fa-times"></i>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <a href="#" runat="server" id="btnAddRef" data-target="#assetsNozzleDetail" data-toggle="modal" data-dismiss="modal" data-backdrop="static" data-keyboard="false">เพิ่มมือจ่าย..</a>
                </div>
            </div>
        </div>
    </div>
    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/NonPO.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/starRating.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Jobs.js")%>"></script>

    <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>
    <script type="text/javascript">
        var selected = [];
        let modalShowID = '';

        jQuery('[id$=txtbegindate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08'
            timepicker: true,
            scrollInput: false,
            format: 'd/m/Y H:i'
        });
        jQuery('[id$=txtenddate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08'
            timepicker: true,
            scrollInput: false,
            format: 'd/m/Y H:i'
        });
        jQuery('[id$=txtexpirydate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08'
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });

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

        $(document).ready(function () {
            $('input[name="files__nozzle"]').fileuploader({
                example: ['pdf', 'image/*'],
                fileMaxSize: 30,
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

                    plusInput.on('click', function () {
                        api.open();
                    });

                    api.getOptions().dragDrop.container = plusInput;
                }
            });
            $('.form-control:not(.cbomulti)').selectpicker({
                noneSelectedText: '-',
                liveSearch: true,
                maxOptions: 1
            });
            $('#assetsNozzleDetail').on('show.bs.modal', function (e) {
                clearAll();
            });

            $('.cbomulti').selectpicker({
                noneSelectedText: '-',
                liveSearch: true,
                selectedTextFormat: 'count > 2',
                maxOptions: 10
            });

            $('.cbomulti').on('changed.bs.select', function (e, clickedIndex, isSelected, previousValue) {
                //console.log(e.target.options[clickedIndex].value);
                let valnow = e.target.options[clickedIndex].value;
                if (e.target.options[clickedIndex].selected) {
                    if (!selected.includes(valnow)) {
                        selected.push(valnow);
                        refreshDiv();
                    }
                } else {
                    removeEmail(valnow);
                    //console.log('un select');

                }
                //console.log(selected);

            });

            $('.form-control').selectpicker('refresh');
            <% For i = 0 To assessmenttable.Rows.Count - 1 %>
            let ratingStars<%= assessmenttable.Rows(i).Item("topic_id").ToString() %> = getStarRating("<%= assessmenttable.Rows(i).Item("topic_id").ToString() %>");
                <% If maintable.Rows(0).Item("followup_status") <> "ปิดงาน" Then %>
            if (ratingStars<%= assessmenttable.Rows(i).Item("topic_id").ToString() %>) executeRating(ratingStars<%= assessmenttable.Rows(i).Item("topic_id").ToString() %>);
                <% else If assessmenttable.Rows(i).Item("rate") > 0 And maintable.Rows(0).Item("followup_status") = "ปิดงาน" Then %>
            executeRatingAvg(ratingStars<%= assessmenttable.Rows(i).Item("topic_id").ToString() %>,<%= assessmenttable.Rows(i).Item("rate") %>);
                <% End if %>
            <% Next i %>

            <% If Not maintable.Rows(0).Item("supplierid") = 0 Then %>
            calProgressTotal("_Service");
            <% End if %>
            calProgressTotal("_Operator");


            if (modalShowID) { modalShow(modalShowID) }
            const urlParams = new URLSearchParams(window.location.search);
            const gotoContent = urlParams.get('g');
            if (!gotoContent) {
                <% If maintable IsNot Nothing Then %>
                    <% If maintable.Rows.Count > 0 Then %>
                        <% If String.Equals(Session("username"), maintable.Rows(0).Item("jobowner")) And (statusnow = 4 Or maintable.Rows(0).Item("followup_status") = "รอลงคะแนนประเมินงาน") And maintable.Rows(0).Item("followup_status") <> "ปิดงาน" Then %>
                    checkStatusJob();
                        <% End if %>
                    <% End if %>
                <% End if %>
            }

            var arrVendor = new Array;
            //var myArray = new Array;
            $("#<%= cboVendor.ClientID%> option").each(function () {
                arrVendor.push($(this).val());
                //myArray[$(this).val()] = $(this).attr("data-taxidno");
            });
            //for (var key in myArray) {
            //    console.log("key " + key + " has value " + myArray[key]);
            //}

            const elemContractorName = document.getElementById("txtContractorName")
            if (elemContractorName) nonpo_autocomplete(document.getElementById("txtContractorName"), arrVendor, null, null);
            const txtStatus = document.querySelector('.badgestatus_app')?.textContent?.trim();
            const invalidStatuses = ["ปิดงาน", "ยกเลิก", "ไม่ผ่านการอนุมัติ"];

            getSubsupplier().then(() => {
                if (txtStatus && invalidStatuses.includes(txtStatus)) {
                    disbtndelete();
                }
            })



        });
        function modalShow(id) {
            $(`#${id}`).modal('show');
        }
        jQuery('[id$=cboStatus]').on('show.bs.dropdown', function () {
            $('.table-responsive').css("overflow", "inherit");
        });

        jQuery('[id$=cboStatus]').on('hide.bs.dropdown', function () {
            $('.table-responsive').css("overflow", "auto");
        })
        function chooseMyfile() {
            validateData();

            const url = $('#<%= cboMyfile.ClientID%>').val();
            const description = $("#<%= cboMyfile.ClientID%> option:selected").text();
            sentAddAttach(url, description)

            return true;
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
                    ////console.log(url, description);
                    sentAddAttach(url, description)
                }
            })

        }
        function sentAddAttach(url, description) {
            if (url.substring(0, 7) != 'http://' && url.substring(0, 8) != 'https://') {
                url = 'http://' + url;
            }
            /*alert(url);*/
            let msg = '<a href="' + url + '" target="_blank">' + description + '</a>'

            const urlParams = new URLSearchParams(window.location.search);
            const nonpocode = urlParams.get('jobdetailid');
            var user = "<% =Session("usercode").ToString %>";
            var userid = <%= Session("userid") %>;
            var params = "{'user': '" + user + "','url': '" + url + "','description': '" + description + "','nonpocode': '" + nonpocode + "'}";
            //alert(params)
            $.ajax({
                type: "POST",
                url: "Non-PO/Payment/Payment2.aspx/addAttach",
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
                    //console.log(msg);

                    alertWarning('Add URL faila');

                }
            });

        }


        function validateRate() {
            let cnt_null = 0
            var params = "[";
            <% For i = 0 To assessmenttable.Rows.Count - 1 %>
                <% If assessmenttable.Rows(i).Item("Type").ToString() = "rate" Then %>
            let ratingStars<%= assessmenttable.Rows(i).Item("topic_id").ToString() %> = getStarRating("<%= assessmenttable.Rows(i).Item("topic_id").ToString() %>");
            //console.log(<%= assessmenttable.Rows(i).Item("topic_id").ToString() %>);
            //console.log(`ssss ${getRate(ratingStars<%= assessmenttable.Rows(i).Item("topic_id").ToString() %>)}`);
            if (getRate(ratingStars<%= assessmenttable.Rows(i).Item("topic_id").ToString() %>) == 0) {
                cnt_null++;
            } else {
                params = params + "{'topic_id': '<%= assessmenttable.Rows(i).Item("topic_id").ToString() %>','value': '" + getRate(ratingStars<%= assessmenttable.Rows(i).Item("topic_id").ToString() %>) + "','type': 'rate'},"
            }
                <% ElseIf assessmenttable.Rows(i).Item("Type").ToString() = "text" Then %>
            let valText<%= assessmenttable.Rows(i).Item("topic_id").ToString() %> = document.getElementById("txtOther_" + "<%= assessmenttable.Rows(i).Item("topic_id").ToString() %>").value
            if (valText<%= assessmenttable.Rows(i).Item("topic_id").ToString() %>) params = params + "{'topic_id': '<%= assessmenttable.Rows(i).Item("topic_id").ToString() %>','value': '" + valText<%= assessmenttable.Rows(i).Item("topic_id").ToString() %> + "','type': 'text'},"
                <% End if %>
            <% Next i %>

            if (cnt_null > 0) {
                event.preventDefault();
                event.stopPropagation();
                alertWarning("กรุณาลงคะแนนประเมินให้ครบทุกหัวข้อ")
                return false;
            }
            else {

                //console.log(params)
                params = (params.substr(params.length - 1) === ",") ? params.substr(0, params.length - 1) + "]" : params + "]";
                //console.log(`new ${params}`)
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";

                if (confirm("คุณต้องการจะบันทึกหรือไม่ ?")) {
                    confirm_value.value = params;
                }
                else {
                    event.preventDefault();
                    event.stopPropagation();
                }
                document.forms[0].appendChild(confirm_value);
                return true;
            }
        }
        function stoppedTyping() {
            if (document.getElementById('<%= txtComment.ClientID%>').value.length > 0) {
                document.getElementById('<%= btnSaveComment.ClientID%>').disabled = false;
            } else {
                document.getElementById('<%= btnSaveComment.ClientID%>').disabled = true;

            }
        }
        function postBack_updateJFU() {
            let active = $(".md-stepper-horizontal .active").length;
            let doing = $(".md-stepper-horizontal .doing").length;
            //if (active > 0 || doing > 0) {
            //    alertWarning('ต้องการเปลี่ยนแปลง Suppiler ระหว่างดำเนินการอยู่ ใช่หรือไม่ ?');
            //    event.preventDefault();
            //    event.stopPropagation();
            //    return 0;
            //}
        }


        function refreshDiv() {
            $("#<%= jobitems.ClientID%>").val(selected.join(','));
        }

        function removeEmail(email) {
            var index = selected.indexOf(email);
            if (index !== -1) {
                selected.splice(index, 1);
            }
            refreshDiv();
        }
        function disbtndelete() {
            $(".deletedetail").hide();
        }


        <%--function postData(ajaxurl, params) {
            return $.ajax({
                type: "POST",
                url: ajaxurl,
                data: params,
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });
        };

        async function sendvendorss() {

            event.stopPropagation();
            try {
                const urlParams = new URLSearchParams(window.location.search);
                const jobcode = urlParams.get('jobno');
                const dtlid = urlParams.get('jobdetailid');
                var user = "<% =Session("usercode").ToString %>";
                var params = `{"jobcode": ${jobcode},"dtlid": ${dtlid} ,"usercode" : "${user}"}`;
                await postData('http://vpnptec.dyndns.org:32001/api/STrack_responseFlex_AfterInsert', params).then((res) => {
                    if (res.length > 0) {
                        alert('success');
                        alertSuccessToast('ส่งงานเรียบร้อย');
                        __doPostBack('sendvendor', '');
                    } 
                });
                console.log(res);
                return true;
            } catch (err) {
                alertWarning(err);
                event.preventDefault();
                event.stopPropagation();

                return false;
            }

        }--%>

        function sendvendor() {
            Swal.fire({
                title: 'คุุณต้องการจะงานไปยัง Supplier ใช่หรือไม่ ?',
                text: "",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes',
                allowOutsideClick: false
            }).then((result) => {
                if (result.isConfirmed) {

                    const urlParams = new URLSearchParams(window.location.search);
                    const jobcode = urlParams.get('jobno');
                    const dtlid = urlParams.get('jobdetailid');
                    var user = "<% =Session("usercode").ToString %>";
                    var params = `{"jobcode": ${jobcode},"dtlid": ${dtlid} ,"usercode" : "${user}"}`;
                    $.ajax({
                        type: "POST",
                        url: "http://vpnptec.dyndns.org:32001/api/STrack_responseFlex_AfterInsert",
                        async: true,
                        data: params,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            //alert(msg);
                            swal.fire({
                                title: "ส่งงานเรียบร้อย!",
                                text: "",
                                icon: "success",
                                allowOutsideClick: false
                            }).then(function () {
                                __doPostBack('flashData', '');
                            });
                        },
                        error: function () {
                            alertWarning('fail ee')
                        }
                    });
                }
            })

            return false;
        }

        function finishStep() {
            //alert(3);


            const urlParams = new URLSearchParams(window.location.search);
            const jobcode = urlParams.get('jobno');
            const dtlid = urlParams.get('jobdetailid');
            var user = "<% =Session("usercode").ToString %>";
            var params = `{"jobcode": ${jobcode},"dtlid": ${dtlid} ,"usercode" : "${user}"}`;
            //alert(params)
            $.ajax({
                type: "POST",
                url: "http://vpnptec.dyndns.org:32001/api/STrack_SuccessJob",
                await: true,
                data: params,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {

                    alert('success');
                    alertSuccessToast('ส่งงานเรียบร้อย');
                    __doPostBack('flashData', '');

                },
                error: function (msg) {
                    //console.log(msg);
                    alert('error');
                    alertWarning('Add URL faila');
                    event.preventDefault();
                    event.stopPropagation();

                }
            });

        }

        function disAccept() {

            /*alert(GridView);*/


            const urlParams = new URLSearchParams(window.location.search);
            const jobcode = urlParams.get('jobno');
            const dtlid = urlParams.get('jobdetailid');
            var usercode = "<%= Session("usercode")%>";

            Swal.fire({
                input: 'textarea',
                inputLabel: 'ไม่รับงานเนื่องจาก',
                inputPlaceholder: 'ใส่ข้อความ . . .',
                inputAttributes: {
                    'aria-label': 'ใส่ข้อความ.'
                },
                preConfirm: (value) => {
                    if (!value) {
                        // Handle return value 
                        Swal.showValidationMessage('First input missing')
                    }
                },
                showCancelButton: true
            }).then((result) => {
                if (result.isConfirmed) {
                    //alert('in');
                    //var params = "{'approvalcode': '" + approvalcode + "','message': '" + result.value + "','updateby': '" + usercode + "'}";
                    let url = "../OPS/approval/WebForm5.aspx/disAcceptByCode";
                    const cntSupplier = <%= cntSupplier %>?? 0;
                        url = (cntSupplier > 0) ? "http://vpnptec.dyndns.org:32001/api/STK_unCompletedBy_User" : "../OPS/approval/WebForm5.aspx/disAcceptByCode";

                        //alert(url);
                        var params = `{"jobcode" : "${jobcode}","dtlid" : "${dtlid}","message" : "${result.value}","user" : "${usercode}"}`;
                        console.log(params);
                        $.ajax({
                            type: "POST",
                            url: url,
                            async: true,
                            data: params,
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                console.log(msg);
                                let res = msg.d ?? msg.message ?? msg;
                                console.log(res);
                                if (res) {
                                    swal.fire({
                                        title: "success!",
                                        text: "",
                                        icon: "success"
                                    }).then(function () {
                                        window.location.href = location.href;
                                    });
                                } else {
                                    event.preventDefault();
                                    alertWarning('fail else');
                                }
                            },
                            error: function () {
                                alertWarning('fail e')
                            }
                        });
                    }
                })

            return false;
        }
        function btnEditDetailClick(rownumber, nozzle_id, nozzle_no, brand, expirydate, producttype, positiononassest, nozzle_no_new, expirydate_new) {
            console.log(rownumber, nozzle_id, nozzle_no, brand, expirydate, producttype, positiononassest);


            $('#<%= row.ClientID%>').val(rownumber);
            $('#<%= hiddenAdvancedetailid.ClientID%>').val(nozzle_id);
           <%-- $('#<%= txtVendor.ClientID%>').val(vendorcode);--%>
            $('#<%= txtbrand.ClientID%>').val(brand);
            $('#<%= txtproducttype.ClientID%>').val(producttype);
            $('#<%= txtnozzle_no.ClientID%>').val(nozzle_no_new);
            $('#<%= txtpositiononassest.ClientID%>').val(positiononassest);
            $('#<%= txtexpirydate.ClientID%>').val(expirydate_new);

            $('#txtnozzle_no_old').text(`เลขที่มาตร เดิม : ${nozzle_no}`);
            $('#txtexpirydate_old').text(`วันสิ้นคำรับรอง เดิม : ${expirydate}`);

            $('#exampleModal').modal('show');
        }

        function confirmDeleteNozzle(nozzleid) {
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

                    var params = "{'nozzleid': '" + nozzleid + "'}";

                    __doPostBack('deleteNozzle', params);
                }
            })

            return false;
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
        function clearAll() {
            let GridView = $("#assetsNozzleDetail .table tbody");
            let inputList = GridView[0].getElementsByTagName("input");
            for (let i = 0; i < inputList.length; i++) {
                if (inputList[i].type == "checkbox") {
                    inputList[i].checked = false;
                    inputList[i].parentNode.parentNode.parentNode.classList.remove("checked");

                }
            }
        }



        $("#assetsNozzleDetail .table tbody tr").click(function (e) {
            if ($(e.target).is(':checkbox')) return; //ignore when click on the checkbox

            var $cb = $(this).find(':checkbox');
            $cb.prop('checked', !$cb.is(':checked'));
            $cb.is(':checked') ? $(this).addClass("checked") : $(this).removeClass("checked");
            Check_Click(this)
        });
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
                let fullname = `{"position":"${myArray[0]}","code":"${myArray[1]}"}`;
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

        function setSelected() {
            let textinputs = document.querySelectorAll('td input:checked');
            const params = getSeleted();
            const sizeText = textinputs.length;
            removeElem("setNozzle");

            let confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "setNozzle";
            if (textinputs.length > 0) {
                if (confirm(`ต้องการแจ้งตีตรา (${sizeText}) รายการที่เลือกหรือไม่ ?`)) {
                    confirm_value.value = params;
                } else {
                    event.preventDefault();
                    event.stopPropagation();
                }

            }
            else {
                event.preventDefault();
                event.stopPropagation();
            }

            document.forms[0].appendChild(confirm_value);
            return true;
        }

        function postBack_addDetail() {
            const txtnozzle_no = $('#<%= txtnozzle_no.ClientID%>').val();
            const txtexpirydate = $('#<%= txtexpirydate.ClientID%>').val();
            if (!txtnozzle_no || !txtexpirydate) {
                alertWarning('กรุณากรอกข้อมูลให้ครบถ้วน');
                event.preventDefault();
                event.stopPropagation();
                return 0;
            }

            if ($('input[name="files__nozzle"]')[0].files.length === 0) {
                alertWarning('กรุณาแนบรูปภาพ');
                event.preventDefault();
                event.stopPropagation();
                return 0;
            }
        }
        function addContractor(usercode) {
            event.preventDefault();
            const stkcodespan = document.getElementById("<%= txtstkCode.ClientID%>");
            const stkcode = stkcodespan.innerText.trim();
            if (stkcode === "") return;

            const nameInput = document.getElementById("txtContractorName");
            const name = nameInput.value.trim();
            if (name === "") return;

            const list = document.getElementById("contractorList");

            // เช็คว่ามีชื่อใน list อยู่แล้วหรือไม่
            const exists = Array.from(list.getElementsByTagName("li")).some(item =>
                item.textContent.trim().startsWith(name)
            );

            if (exists) {
                nameInput.value = "";
                alert("ชื่อผู้รับเหมานี้มีอยู่แล้ว!");
                return;
            }

            const params = "{'stkcode': '" + stkcode + "','txtValue': '" + name + "','user': '" + usercode + "'}";
            $.ajax({
                type: "POST",
                url: "/OPS/jobs_followup.aspx/addSubSupplier",
                async: true,
                data: params,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d) {

                        const id = msg.d
                        // สร้าง list item ใหม่
                        const listItem = document.createElement("li");
                        listItem.className = "";
                        listItem.innerHTML = `${name} <button class="btn btn-link text-danger ml-2 deletedetail" onclick="removeContractor(this,${id});"> <i class="fas fa-times"></i></button>`;

                        list.appendChild(listItem);
                    } else {
                        throw new Error("Add fail");
                    }

                },
                error: function (msg) {
                    alertWarning('Add fail');
                    return;
                }
            });


            nameInput.value = "";
        }

        function removeContractor(button, id) {
            event.preventDefault();
            const usercode = "<%= Session("usercode") %>"; // ดึง user จาก session

            if (id === null || id === undefined || isNaN(id)) return;

            if (confirm("คุณต้องการลบรายการนี้หรือไม่?")) {
                $.ajax({
                    type: "POST",
                    url: "/OPS/jobs_followup.aspx/deleteSubSupplier",
                    async: true,
                    data: JSON.stringify({ ssid: id, user: usercode }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.d) {
                            button.parentElement.remove(); // ลบจาก UI เมื่อสำเร็จ
                        } else {
                            alertWarning("ลบข้อมูลไม่สำเร็จ");
                        }
                    },
                    error: function () {
                        alertWarning("เกิดข้อผิดพลาดในการลบข้อมูล");
                    }
                });
            }
        }
        async function getSubsupplier() {
            const stkcodespan = document.getElementById("<%= txtstkCode.ClientID%>");
            const stkcode = stkcodespan.innerText.trim();
            if (!stkcode) return;

            try {
                const response = await fetch("/OPS/jobs_followup.aspx/getSubSupplier", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json; charset=utf-8"
                    },
                    body: JSON.stringify({ stkcode: stkcode })
                });

                if (!response.ok) {
                    throw new Error("เกิดข้อผิดพลาดในการโหลดข้อมูล");
                }

                const result = await response.json(); // ดึงข้อมูล JSON
                if (result.d) {
                    const list = document.getElementById("contractorList");
                    list.innerHTML = ""; // เคลียร์รายการก่อน

                    const data = JSON.parse(result.d); // แปลง JSON เป็น Object
                    // console.log(data);

                    data.forEach(row => {
                        const listItem = document.createElement("li");
                        listItem.innerHTML = `
                    ${row.subsupplier_name} 
                    <button class="btn btn-link text-danger ml-2 deletedetail" 
                        onclick="removeContractor(this, ${row.id});"> 
                        <i class="fas fa-times"></i>
                    </button>
                `;
                        list.appendChild(listItem);
                    });
                }
            } catch (error) {
                console.error("Error:", error);
                alertWarning("เกิดข้อผิดพลาดในการโหลดข้อมูล");
            }

        }
        function getHashParam(key) {
            const params = new URLSearchParams(window.location.search);
            const jobdetailid = params.get(key);
            return jobdetailid; // ถ้าไม่มีค่าให้คืนเป็น ''
        }

        function approveHierachy(jobdtlid, type, userid) {
            if (jobdtlid > 0) {
                Swal.fire({
                    title: `คุณต้องการจะ "อนุมัติ" ใช่หรือไม่?`,
                    showCancelButton: true,
                    confirmButtonText: "Approve",
                }).then((result) => {
                    if (result.isConfirmed) {
                        var params = "{'jobdtlid': '" + jobdtlid + "','type': '" + type + "','updateby': '" + userid + "'}";
                        console.log(params);
                        $.ajax({
                            type: "POST",
                            url: "../ops/jobs.aspx/approveHierachy",
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



                    } else if (result.isDenied) {
                        return false;
                    }
                });
            }
            return false;
        }
        function rejectHierachy(jobdtlid, type, userid) {
            if (jobdtlid > 0) {
                Swal.fire({
                    input: 'textarea',
                    inputLabel: `กรุณาใส่เหตุผลในการ "ปฏิเสธ" ใบงาน`,
                    inputPlaceholder: 'ใส่ข้อความ . . .',
                    inputAttributes: {
                        'aria-label': 'ใส่ข้อความ.'
                    },
                    customClass: {
                        inputLabel: "text-danger",
                    },
                    preConfirm: (value) => {
                        if (!value) {
                            Swal.showValidationMessage('input missing')
                        }
                    },
                    showCancelButton: true
                }).then((result) => {
                    console.log(result.value);
                    if (result.isConfirmed) {
                        var params = "{'jobdtlid': '" + jobdtlid + "','message': '" + result.value + "','type': '" + type + "','updateby': '" + userid + "'}";
                        console.log(params);
                        $.ajax({
                            type: "POST",
                            url: "../ops/jobs.aspx/rejectHierachy",
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
            }

            return false;
        }
    </script>
</asp:Content>
