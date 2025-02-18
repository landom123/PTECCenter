<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="Jobs_Close.aspx.vb" Inherits="PTECCENTER.JobsClose" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="<%=Page.ResolveUrl("~/css/Jobs.css")%>" rel="stylesheet">
    <link href="<%=Page.ResolveUrl("~/css/card_comment.css")%>" rel="stylesheet">
    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">

    <style>
        html {
            background-color: #f0f2f5 !important;
        }

        .btn-light.disabled {
            border: 1px solid #ced4da;
            background-color: #e9ecef;
        }

        #dataTable td, th {
            font-size: .8rem;
            overflow: hidden;
            text-overflow: ellipsis;
            width:100%;
        }

        /*####################### CSS FROM MODAL ########################*/

        .modal .modal-body {
            padding: 2rem;
            padding-top: 1rem;
        }

        .modal .form-group, .modal .form-control, .modal .bootstrap-select .dropdown-toggle, .modal .bootstrap-select .dropdown-menu {
            font-size: 0.875rem;
            transition: none !important;
        }

        .modal-body .btn-light.disabled, .modal-body .btn-light:disabled {
            background-color: #e9ecef;
            border-color: #ced4da;
        }

        .modal .showCost {
            background-color: #f7faff;
            padding: 1rem;
        }
        /*####################### END CSS FROM MODAL ########################*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper" class="h-100">

        <!-- #include virtual ="/include/menu.inc" -->
        <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">
                <%--<ol class="breadcrumb" style="background-color: deeppink; color: white">
                    <li class="breadcrumb-item"><a href="JobsLists.aspx" class="btn btn-sm btn-danger">
                        <i class="fa fa-tasks" aria-hidden="true"></i></a>ปิดงาน (Close Job)
                    </li>
                </ol>--%>
                <div class="headJobs mb-3" style="background-color: #a32048; color: white; padding: 0.75rem 1rem; border-radius: 0.25rem;">
                    <div class="row justify-content-between">
                        <div class="col text-left align-self-center">
                            ปิดงาน (Close Job)
                        </div>
                        <div class="col text-right align-self-center">
                            <div class="d-flex flex-column align-items-end" readonly="true">
                                <asp:Label CssClass="approvalcode font-weight-bold" ID="lbjobscode" runat="server" Text=''></asp:Label>
                                <a href="#" class="badge badgestatus_app" id="badgeStatus" runat="server"></a>
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
                    <div class="col mb-3">
                        <div class="row justify-content-end align-items-center">
                            <div class="col-auto">
                                <a class="gtContent6" id="txtCntComment" runat="server" href="#" title="ไปยังแสดงความคิดเห็น"><%= CommentTable.Rows.Count %> <i class="far fa-comment-dots"></i></a>
                            </div>
                            <div class="col-auto">
                                <a class="gtContent5" id="txtCntAttach" runat="server" href="#" title="ไปยังแสดงความคิดเห็น"><%= AttachTable.Rows.Count %> <i class="fas fa-paperclip"></i></a>
                            </div>
                        </div>
                    </div>
                    <div class="col-auto mb-3">
                        <ul class="nav">
                            <li class="nav-item dropdown">
                                <a class="nav-link dropdown-toggle" data-toggle="dropdown" href="#" role="button" aria-haspopup="true" aria-expanded="false"></a>
                                <div class="dropdown-menu">
                                    <a class="dropdown-item" id="gtContent1" href="#" title="ไปยังรายละเอียด">รายละเอียด</a>
                                    <a class="dropdown-item" id="gtContent2" href="#" title="ไปยังสำหรับผู้ปฏิบัติงาน">สำหรับผู้ปฏิบัติงาน</a>
                                    <a class="dropdown-item" id="gtContent5" href="#" title="ไปยังเอกสารแนบ">เอกสารแนบ</a>
                                    <a class="dropdown-item" id="gtContent6" href="#" title="ไปยังแสดงความคิดเห็น">แสดงความคิดเห็น</a>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>

                <div class="card shadow mb-3">
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
                                <div class="col-md-4 mb-3  d-none d-md-block">
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
                                            <span class="input-group-text">จำนวน</span>
                                        </div>
                                        <asp:TextBox class="form-control" ID="txtQuantity" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4 mb-3">
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
                                        <asp:TextBox class="form-control" ID="txtCost" runat="server" ReadOnly="true"></asp:TextBox>
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
                            <%--<div class="row">
                                <div class="col-md-4 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">ประเภทหมวดราคา</span>
                                        </div>
                                        <asp:TextBox class="form-control" ID="txtJobCenter" runat="server" ReadOnly="true">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>--%>


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

                        </div>
                    </div>
                </div>
                <div class="card shadow mb-3">
                    <div class="card-header" id="headingTwo">
                        <h5 class="mb-0">
                            <button class="btn btn-link w-100 text-left collapse__all" id="forOperator" type="button" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="true" aria-controls="collapseTwo" autopostback="False">
                                สำหรับผู้ปฏิบัติงาน
                            </button>
                        </h5>
                    </div>

                    <div id="collapseTwo" class="collapse multi-collapse show" aria-labelledby="headingTwo">
                        <div class="card-body">

                            <div class="row">
                                <div class="col-lg-3 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">วันเริ่มต้นประกัน</span>
                                        </div>
                                        <asp:TextBox class="form-control" ID="txtBeginWarr" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-3 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">วันสิ้นสุดประกัน</span>
                                        </div>
                                        <asp:TextBox class="form-control" ID="txtEndWarr" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-3 mb-3 d-none">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">ชนิดงาน</span>
                                        </div>
                                        <asp:DropDownList class="form-control" ID="cboCloseCategory" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <div class="col-lg-3 mb-3 d-none">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">วันที่ปิดงาน</span>
                                        </div>
                                        <asp:TextBox class="form-control" ID="txtCloseDate" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-3 mb-3 d-none">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">ปิด</span>
                                        </div>
                                        <asp:DropDownList class="form-control" ID="cboCloseType" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--<div class="col-lg-6 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">Invoice No</span>
                                        </div>
                                        <asp:TextBox class="form-control" ID="txtInvoiceNo" runat="server"></asp:TextBox>
                                    </div>
                                </div>--%>


                                <%-- <div class="col-lg-3 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">วันที่ Invoice</span>
                                        </div>
                                        <asp:TextBox class="form-control" ID="txtInvDate" runat="server"></asp:TextBox>
                                    </div>
                                </div>--%>
                            </div>
                            <div class="row">
                                <div class="col-12 mb-3">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">รายละเอียดสำหรับ Payment</span>
                                        </div>
                                        <asp:TextBox class="form-control" ID="txtRemark" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 mb-3">

                                    <div class="card-body shadow" style="background-color: #fff2fb;">
                                        <div class="table-responsive ">
                                            <%--table table-borderless--%>
                                            <table class="table table-borderless table-hover table-Light" id="dataTable">
                                                <thead class="table text-center">
                                                    <tr>
                                                        <td class="text-right" colspan="8">
                                                            <a href="#" class="badge badgestatus_app" style="color: black !important; background-color: gray !important;" id="lockcost" runat="server"></a>
                                                        </td>
                                                    </tr>
                                                    <tr class="border border-dark border-right-0 border-left-0 text-info">
                                                        <th scope="col" style="width: 250px !important;">รายการ</th>
                                                        <th scope="col" style="width: 100px !important;">ราคากลาง/หน่วย</th>
                                                        <th scope="col" style="width: 100px !important;">จำนวน</th>
                                                        <th scope="col" style="width: 100px !important;">ราคา/หน่วย</th>
                                                        <th scope="col" style="width: 100px !important;">รวมก่อน VAT</th>
                                                        <th scope="col" style="width: 50px !important;">VAT</th>
                                                        <th scope="col" style="width: 50px !important;">(WHT)</th>
                                                        <th scope="col" style="width: 100px !important;">รวมทั้งสิ้น</th>
                                                        <th scope="col" style="width: 50px !important;">Bill</th>
                                                        <%--<% If Not chkPAYMENT Then%>
                                                        <th scope="col"><a id="generatedPayment" class="btn-sm" style="color: red; font-size: 1rem; font-weight: bold;">
                                                            <i class="fas fa-file-invoice-dollar"></i>
                                                        </a></th>
                                                        <% End If %>--%>
                                                    </tr>
                                                </thead>
                                                <tbody class="border border-dark border-right-0 border-left-0">
                                                    <% For i = 0 To costtable.Rows.Count %>
                                                    <% If i < costtable.Rows.Count Then%>
                                                    <tr ondblclick="btnEditDetailClick('<%= costtable.Rows(i).Item("jobcostid").ToString() %>'
                                                                        ,'<%= costtable.Rows(i).Item("JobsCenterDtlID").ToString() %>'
                                                                        ,'<%= costtable.Rows(i).Item("bu").ToString() %>'
                                                                        ,'<%= costtable.Rows(i).Item("pp").ToString() %>'
                                                                        ,'<%= costtable.Rows(i).Item("pj").ToString() %>'
                                                                        ,'<%= costtable.Rows(i).Item("jobcostunit").ToString() %>'
                                                                        ,'<%= costtable.Rows(i).Item("unitprice").ToString() %>'
                                                                        ,'<%= costtable.Rows(i).Item("vat_per").ToString() %>'
                                                                        ,'<%= costtable.Rows(i).Item("tax_per").ToString() %>'
                                                                        ,'<%= costtable.Rows(i).Item("invoiceno").ToString() %>'
                                                                        ,'<%= costtable.Rows(i).Item("invoicedate").ToString() %>'
                                                                        ,'<%= costtable.Rows(i).Item("nobill").ToString() %>'
                                                                        ,'<%= costtable.Rows(i).Item("incompletebill").ToString() %>'
                                                                        );">
                                                        <td title="<% =costtable.Rows(i).Item("jobscenterdtlname") %>"><% =costtable.Rows(i).Item("jobscenterdtlname") %></td>
                                                        <td class="text-right" title="<% =costtable.Rows(i).Item("jobscenterdtlprice") %>"><% =String.Format("{0:n}", costtable.Rows(i).Item("jobscenterdtlprice")) %></td>
                                                        <td class="text-center" title="<% =costtable.Rows(i).Item("jobcostunit") %>"><% =costtable.Rows(i).Item("jobcostunit") %></td>
                                                        <td class="text-right" title="<% =costtable.Rows(i).Item("unitprice") %>" style="color:<%= if( ( costtable.Rows(i).Item("jobscenterdtlprice") < costtable.Rows(i).Item("amount")), "red", "black") %>;"><% =String.Format("{0:n}", costtable.Rows(i).Item("unitprice")) %></td>
                                                        <td class="text-right" title="<% =costtable.Rows(i).Item("vat_per") %>"><% =String.Format("{0:n}", costtable.Rows(i).Item("unitprice") * costtable.Rows(i).Item("jobcostunit")) %></td>
                                                        <td class="text-right" title="<% =costtable.Rows(i).Item("vat_per") %>"><% =String.Format("{0:n}", costtable.Rows(i).Item("unitprice") * costtable.Rows(i).Item("vat_per") / 100) %></td>
                                                        <td class="text-right" title="<% =costtable.Rows(i).Item("tax_per") %>">(<% =String.Format("{0:n}", costtable.Rows(i).Item("unitprice") * costtable.Rows(i).Item("tax_per") / 100)%>)</td>
                                                        <td class="text-right" title="<% =costtable.Rows(i).Item("amount") %>"><% =String.Format("{0:n}", costtable.Rows(i).Item("amount")) %></td>
                                                        <td class="text-center" title=""><%= if( (Not costtable.Rows(i).Item("nobill") And Not costtable.Rows(i).Item("incompletebill")), "", If(costtable.Rows(i).Item("nobill"), "N", "U")) %></td>
                                                        <% If maintable.Rows(0).Item("lockcost") = False Then %>
                                                        <td>

                                                            <a onclick="confirmDelete('<% =costtable.Rows(i).Item("jobcostid") %>')" class="btn btn-sm p-0 notPrint">
                                                                <i class="fas fa-times"></i>
                                                            </a>
                                                        </td>
                                                        <% End if %>
                                                    </tr>
                                                    <% End If %>
                                                    <% Next i %>
                                                    <tr class="border border-dark border-right-0 border-left-0">
                                                        <th></th>
                                                        <th></th>
                                                        <th></th>
                                                        <th></th>
                                                        <th class="text-right"><% =cost %></th>
                                                        <th class="text-right"><% =vat %></th>
                                                        <th></th>
                                                        <th class="text-right"><% =total %></th>
                                                    </tr>
                                                </tbody>

                                                <tr class="text-center notPrint " runat="server" id="FromAddDetail">
                                                    <td colspan="24" style="width: 960px !important;">
                                                        <button type="button" class="btn btn-sm  btn-outline-info w-100 noEnterSubmit" id="btnFromAddDetail" runat="server" data-toggle="modal" data-target="#exampleModal" data-backdrop="static" data-keyboard="false" data-whatever="new">เพิ่มรายการ</button>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td class="text-right text-info" colspan="7">รวมเงิน</td>
                                                    <td class="text-right"><% =cost %></td>
                                                </tr>
                                                <tr>
                                                    <td class="text-right text-info" colspan="7">ภาษีมูลค่าเพิ่ม</td>
                                                    <td class="text-right"><% =vat %></td>
                                                </tr>
                                                <tr>
                                                    <th class="text-right text-info" colspan="7">จำนวนเงินรวมสุทธิ</th>
                                                    <th class="text-right"><% =total %></th>
                                                </tr>

                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row justify-content-center">
                                <div class="col-11">
                                    <hr />
                                </div>
                            </div>
                            <div class="row justify-content-center" style="padding-bottom: 1rem;">
                                <div class="col-12 text-center">
                                    <asp:Button ID="btnClose" class="btn btn-sm btn-danger justify-content-center" runat="server" Text="ปิดค่าใช้จ่าย" AutoPostBack="true" UseSubmitBehavior="false" />&nbsp;
                                </div>
                            </div>
                        </div>
                    </div>

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
                                            <asp:Button ID="btnSaveComment" class="btn btn-primary w-100" runat="server" Text="Post" AutoPostBack="True" disabled UseSubmitBehavior="false" />
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
    </div>
    <div class="modal fade bd-example-modal" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog " role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">รายละเอียดรายการ</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body" style="overflow-y:unset;max-height:unset;">
                    <input type="hidden" class="form-control" id="hiddenAdvancedetailid" value="0" runat="server">
                    <div class="form-group">
                        <asp:Label ID="lbcboCost" CssClass="form-label" AssociatedControlID="cboCost" runat="server" Text="รหัสบัญชี" />
                        <asp:Label ID="lbcboCostMandatory" CssClass="text-danger" AssociatedControlID="cboCost" runat="server" Text="*" />
                        <asp:DropDownList class="form-control" ID="cboCost" runat="server" onchange="setdetail(this);"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbBU" CssClass="form-label" AssociatedControlID="cboBU" runat="server" Text="Business Unit" />
                        <asp:DropDownList class="form-control" ID="cboBU" runat="server"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbPP" CssClass="form-label" AssociatedControlID="cboPP" runat="server" Text="Purpose" />
                        <asp:DropDownList class="form-control" ID="cboPP" runat="server"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbPJ" CssClass="form-label" AssociatedControlID="cboPJ" runat="server" Text="Project - ( PM / OilLoss / Capex )" />
                        <asp:DropDownList class="form-control" ID="cboPJ" runat="server"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbtxtCostUnit" CssClass="form-label" AssociatedControlID="txtCostUnit" runat="server" Text="จำนวนชิ้น" />
                        <asp:TextBox class="form-control noEnterSubmit " type="number" ID="txtCostUnit" runat="server" Text="0" min="1" onchange="calculate();"></asp:TextBox>
                        <div class="invalid-feedback">* ตัวเลขจำนวนเต็ม</div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbPrice" CssClass="form-label" AssociatedControlID="txtPrice" runat="server" Text="จำนวนเงิน (ก่อน VAT)" />
                        <asp:TextBox class="form-control noEnterSubmit" type="number" ID="txtPrice" runat="server" Text="0" onchange="calculate();"></asp:TextBox>
                        <div class="invalid-feedback">* ตัวเลขจำนวนเต็ม</div>
                    </div>
                    <div class="row flex-nowrap">
                        <div class="form-group ">
                            <div class="row justify-content-between mr-0 ml-0">
                                <div class="col text-left align-self-center">
                                    <asp:Label ID="Label4" CssClass="form-label" AssociatedControlID="txtVat" runat="server" Text="VAT (%)" />
                                </div>
                            </div>
                            <div class="col">
                                <asp:TextBox class="form-control noEnterSubmit" type="number" ID="txtVat" runat="server" min="0" Text="0" onchange="calculate();"></asp:TextBox>
                            </div>
                            <div class="invalid-feedback">* ตัวเลขจำนวนเต็ม</div>
                        </div>
                        <div class="form-group ">
                            <div class="col">
                                <asp:Label ID="Label5" CssClass="form-label" AssociatedControlID="txtTax" runat="server" Text="WHT (%)" />
                            </div>
                            <div class="col">
                                <asp:TextBox class="form-control noEnterSubmit" type="number" ID="txtTax" runat="server" min="0" Text="0" onchange="calculate();"></asp:TextBox>
                                <div class="invalid-feedback">* ตัวเลขจำนวนเต็ม</div>
                            </div>
                        </div>
                    </div>
                    <div class="showCost">
                        <p class="text-muted" id="p_vat"></p>
                        <p class="text-muted" id="p_tax"></p>
                        <p class="text-muted" id="p_costunit"></p>
                        <p class="text-muted" id="p_cost"></p>
                    </div>
                    <!--  ############## End Detail ############### -->
                    <hr />
                    <h3>ใบแจ้งหนี้ / ใบส่งของ / ใบกำกับ</h3>
                    <div class="form-group">
                        <div class="row justify-content-between">
                            <div class="col">
                                <asp:Label ID="lbtxtInvoiceNo" CssClass="form-label" AssociatedControlID="txtInvoiceNo" runat="server" Text="Document no." />
                            </div>
                            <div class="col gropincompletebill">
                                <div class="row flex-nowrap form-group">
                                    <div class="pr-4" style="color: #0f66c4;">
                                        <input class="form-check-input" type="checkbox" id="chkNoBill" runat="server">
                                        <asp:Label ID="lbchkNoBill" CssClass="form-check-label" AssociatedControlID="chkNoBill" runat="server" Text="ไม่มีบิล (N)" />
                                    </div>
                                    <div class="pl-4" style="color: #0f66c4;">
                                        <input class="form-check-input" type="checkbox" id="chkIncompleteBill" runat="server">
                                        <asp:Label ID="lbchkIncompleteBill" CssClass="form-check-label" AssociatedControlID="chkIncompleteBill" runat="server" Text="บิลไม่สมบูรณ์ (U)" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:TextBox class="form-control noEnterSubmit" type="input" ID="txtInvoiceNo" runat="server" autocomplete="off"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbtxtInvDate" CssClass="form-label" AssociatedControlID="txtInvDate" runat="server" Text="Document date" />
                        <asp:TextBox class="form-control noEnterSubmit" type="input" ID="txtInvDate" runat="server" placeholder="--- คลิกเพื่อเลือก ---" autocomplete="off"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary noEnterSubmit" data-dismiss="modal">Close</button>
                    <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" UseSubmitBehavior="false" />
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
                    <asp:Button ID="asd" class="btn btn-primary" runat="server" Text="Save" OnClientClick="chooseMyfile(); return false;" UseSubmitBehavior="false" />
                </div>
            </div>
        </div>
    </div>

    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <CompositeScript>
            <Scripts>
                <asp:ScriptReference Path="~/datetimepicker/jquery.js" />
                <asp:ScriptReference Path="~/datetimepicker/build/jquery.datetimepicker.full.min.js" />
                <asp:ScriptReference Path="~/js/Jobs.js" />
                <asp:ScriptReference Path="~/js/NonPO.js" />
            </Scripts>
        </CompositeScript>
    </asp:ScriptManager>
    <!-- datetimepicker ต้องไปทั้งชุด-->
   <%-- <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/Jobs.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/NonPO.js")%>"></script>--%>

    <script type="text/javascript">
        jQuery('[id$=txtBeginWarr]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
        jQuery('[id$=txtEndWarr]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
        jQuery('[id$=txtInvDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });

        $(document).ready(function () {
            var groups = {};
            $("select option[data-category]").each(function () {
                groups[$.trim($(this).attr("data-category"))] = true;
            });
            $.each(groups, function (c) {
                $("select option[data-category='" + c + "']").wrapAll('<optgroup label="' + c + '">');
            });
            $('.form-control').selectpicker({
                noneSelectedText: '-',
                liveSearch: true,
                maxOptions: 1
            });
            /*if (cboCost == '99999') {
                alertWarning('กรอกไม่ครบถ้วน');
                event.preventDefault();
                event.stopPropagation();
                $('.boxcost').show();
            } else {
                $('.boxcost').hide();
            }*/
            //const urlParams = new URLSearchParams(window.location.search);
            //let jobno = urlParams.get('jobno');
            //let jobdetailid = urlParams.get('jobdetailid');
            //var a = document.getElementById('generatedPayment');
            //a.href = "../OPS/Non-PO/Payment/Payment.aspx?f=JOB&code_ref=" + jobno + "&code_ref_dtl=" + jobdetailid


        });

        jQuery('[id$=cboCost]').on('show.bs.dropdown', function () {
            $('.table-responsive').css("overflow", "inherit");
        });

        jQuery('[id$=cboCost]').on('hide.bs.dropdown', function () {
            $('.table-responsive').css("overflow", "auto");
        })

        /*jQuery('[id$=cboCost]').on('change', function () {
            if (this.value == '99999') {
 
                $('.boxcost').show();
            } else {
                $('.boxcost').hide();
 
            }
        })*/
        function confirmDelete(jobcostid) {

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
                    var params = "{'jobcostid': '" + jobcostid + "'}";
                    $.ajax({
                        type: "POST",
                        url: "../OPS/jobs_Close.aspx/deleteCostById",
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
        function calculate() {

            //console.log("############ calculate");

            let costunit = CheckNumber(document.getElementById("<%= txtCostUnit.ClientID%>").value);
            let cost = CheckNumber(document.getElementById("<%= txtPrice.ClientID%>").value);
            let vat = CheckNumber(document.getElementById("<%= txtVat.ClientID%>").value);
            let tax = CheckNumber(document.getElementById("<%= txtTax.ClientID%>").value);

            const p_tax = document.getElementById("p_tax");
            const p_vat = document.getElementById("p_vat");
            const p_costunit = document.getElementById("p_costunit");
            const p_cost = document.getElementById("p_cost");

            costunit = parseFloat(costunit);
            cost = parseFloat(cost);
            vat = parseFloat(vat);
            tax = parseFloat(tax);

            //console.log(cost);
            //console.log(vat);
            //console.log(tax);

            const c_CostUnit = calCostTotal(cost, vat, tax).toFixed(2);
            const c_CostTotal = (calCostTotal(cost, vat, tax) * costunit).toFixed(2);
            const c_Vat = calVat(cost, vat).toFixed(2);
            const c_Tax = calTax(cost, tax).toFixed(2);

            //console.log(calCostTotal(cost, vat, tax).toFixed(2));
            //console.log(calVat(cost, vat).toFixed(2));
            //console.log(calTax(cost, tax).toFixed(2));



            if (!isNaN(cost) && (cost - 0) < 999999999.9999) {
                p_costunit.innerHTML = "ราคา ต่อชิ้น : " + numberWithCommas(c_CostUnit) + " บาท";
                p_cost.innerHTML = "รวมทั้งสิ้น : " + numberWithCommas(c_CostTotal) + " บาท";

            } else {
                p_cost.innerHTML = "";
                p_costunit.innerHTML = "";
            }

            if (!isNaN(vat) && (vat - 0) < 999999999.9999) {
                p_vat.innerHTML = "Vat ต่อชิ้น : " + numberWithCommas(c_Vat) + " บาท";
            } else {
                p_vat.innerHTML = "";
            }

            if (!isNaN(tax) && (tax - 0) < 999999999.9999) {
                p_tax.innerHTML = "Tax ต่อชิ้น : (" + numberWithCommas(c_Tax) + ") บาท";
            } else {
                p_tax.innerHTML = "";
            }

        }
        function selectElement(id, valueToSelect) {
            let element = document.getElementById(id);
            element.value = valueToSelect;
        }
        function btnEditDetailClick(costid,accountcodeid, buid, ppid, pjid, unit, cost, vat, tax, invoice, invoicedate, NoBill, IncompleteBill) {
            console.log(accountcodeid);
            console.log(buid);
            console.log(ppid);
            console.log(cost);
            console.log(NoBill.toLowerCase());

            const Accountcode = '<%= cboCost.ClientID%>';
            const bu = '<%= cboBU.ClientID%>';
            const pp = '<%= cboPP.ClientID%>';
            const pj = '<%= cboPJ.ClientID%>';
            <%--const vendor = '<%= cboVendor.ClientID%>';--%>
            $('#exampleModal').modal('show');

            selectElement(Accountcode, accountcodeid);
            selectElement(bu, buid);
            selectElement(pp, ppid);
            selectElement(pj, pjid);
            //selectElement(vendor, vendorcode);
          <%--  $('#<%= cboVendor.ClientID%>').filter(function () {
                //may want to use $.trim in here
                return $(this).text() == vendorcode;
            }).prop('selected', true);
            var value = $('#<%= cboVendor.ClientID%>').filter(function () {
                return $(this).text();
            }).first().attr("value");
            console.log(value);
            vendor.value = value; --%>

            <%-- $('#<%= txtVendor.ClientID%>').val(vendorcode);--%>

            $('#<%= hiddenAdvancedetailid.ClientID%>').val(costid);
            $('#<%= txtCostUnit.ClientID%>').val(unit);
            $('#<%= txtPrice.ClientID%>').val(cost);
            $('#<%= txtVat.ClientID%>').val(vat);
            $('#<%= txtTax.ClientID%>').val(tax);

            $('#<%= txtInvoiceNo.ClientID%>').val(invoice);
            $('#<%= txtInvDate.ClientID%>').val(invoicedate);
            $('#<%= chkNoBill.ClientID%>').prop('checked', NoBill.toLowerCase() == "true" ? true : false);
            $('#<%= chkIncompleteBill.ClientID%>').prop('checked', IncompleteBill.toLowerCase() == "true" ? true : false);
            $('.form-control').selectpicker('refresh');
            /*__doPostBack('setFromDetail', $(row).attr('name'));
*/
             <% If maintable.Rows(0).Item("lockcost") = True Then %>
            $('#exampleModal .modal-footer #btnAddDetail').hide();
            $('#exampleModal .modal-body input,#exampleModal .modal-body textarea').attr('readonly', true);
            $('#exampleModal .modal-body select,#exampleModal .modal-body button,#exampleModal .modal-body input[type="checkbox"]').attr('disabled', true);

            $('#<%= btnSave.ClientID%>').hide();
            <% End If %>

        }

        function clearfromadddetail() {

            $('.form-control').selectpicker('refresh');

            $('#<%= hiddenAdvancedetailid.ClientID%>').val(0);
            $('#<%= cboCost.ClientID%>').val(0);
            <%--$('#<%= cboBU.ClientID%>').val(0);
            $('#<%= cboPP.ClientID%>').val(0);
            $('#<%= cboPJ.ClientID%>').val(0);--%>

            $('#<%= txtUnit.ClientID%>').val('1');
            $('#<%= txtPrice.ClientID%>').val('0');
            $('#<%= txtVat.ClientID%>').val('7');
            $('#<%= txtTax.ClientID%>').val('3');

            $('#<%= cboBU.ClientID%>').val(1);
            $('#<%= cboPP.ClientID%> option:contains(' + '<%= maintable.Rows(0).Item("branchcode") %>' + ')').prop("selected", true);

            <%--$('#<%= txtInvoiceNo.ClientID%>').val('');
            $('#<%= txtInvDate.ClientID%>').val('');
            $('#<%= chkNoBill.ClientID%>').prop('checked', false);
            $('#<%= chkIncompleteBill.ClientID%>').prop('checked', false);--%>

            $('.form-control').selectpicker('refresh');
        }
        $('#<% =btnFromAddDetail.ClientID%>').click(function () {
            $('#exampleModal .modal-footer #btnAddDetail').show();
            $('#exampleModal .modal-body input,#exampleModal .modal-body textarea').removeAttr("readonly");
            $('#exampleModal .modal-body select,#exampleModal .modal-body button,#exampleModal .modal-body input[type="checkbox"]').removeAttr("disabled");
            $('#<% =txtInvDate.ClientID%>').attr('readonly', true);

            $('.form-control').selectpicker('refresh');

            clearfromadddetail();
            $('#<%= btnSave.ClientID%>').show();


        });
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
                    //console.log(url, description);
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
                    console.log(msg);

                    alertWarning('Add URL faila');

                }
            });

        } function stoppedTyping() {
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
