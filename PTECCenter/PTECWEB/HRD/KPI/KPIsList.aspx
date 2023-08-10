<%@ Page Title="KPIsList" Language="vb" AutoEventWireup="true" MasterPageFile="~/site.Master" CodeBehind="KPIsList.aspx.vb" Inherits="PTECCENTER.KPIsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
    <link href="<%=Page.ResolveUrl("~/css/card_comment.css")%>" rel="stylesheet">
    <style>
        
        .container {
            font-size:.8rem;
        }
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
        /*.dropdown .disabled{
            background-color: #e9ecef;
            opacity: 1;
        }*/
        .logopure {
            /*content: url("..\..\..\icon\Logo_pure.png");*/
            width: 200px;
            height: 100px;
            margin-left: 30px;
            margin-top: 10px;
            margin-bottom: 10px;
        }


        .hiddenRow {
            padding: 0 !important;
        }

        .accordion-toggle {
            cursor: pointer;
        }

        a[target="_blank"] {
            background-color: yellow;
        }

        tr[aria-expanded="true"] {
            color: #fff;
            background-color: #176c97;
        }

        tr[aria-expanded="false"] > td:last-child:after {
            content: '\002B';
            font-weight: bold;
            float: right;
            margin-left: 5px;
        }

        tr[aria-expanded="true"] > td:last-child:after {
            content: "\2212";
            font-weight: bold;
            float: right;
            margin-left: 5px;
        }

        .table__inner > thead {
            font-size:.8rem;
        }.table__inner > tbody {
            font-size:.75rem;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg">
        <div id="wrapper">

            <!-- #include virtual ="/include/menu.inc" -->
            <!-- add side menu -->
            <div id="content-wrapper" style="min-height: 600px;">
                <div class="container">
                    <%--<div class="row">
                        <div class="col text-left align-self-center">
                            Performance Update : KPIs & Competency
                        </div>

                        <div class="col-auto text-right align-self-center">
                            <a href="AdvanceMenuList.aspx" class="btn btn-sm btn-danger ">
                                <i class="fa fa-tasks" aria-hidden="true"></i></a>
                        </div>
                    </div>--%>
                    <div class="row align-items-center justify-content-center">
                        <div class="col-lg-auto mb-3 text-center">
                            <img runat="server" id="logo" class="logopure" src="#" alt="logopure" width="500" height="600">
                        </div>
                        <div class="col-lg-auto mb-3 company">
                            <div class="row company-th">
                                <div class="col text-center">
                                    <h3 runat="server" id="company_th">บริษัท เพียวพลังงานไทย จำกัด</h3>
                                </div>
                            </div>
                            <div class="row company-en">
                                <div class="col text-center">
                                    <h5 runat="server" id="company_en">PURE THAI ENERGY COMPANY LIMITED</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">

                            <%--<asp:Button ID="btnNew" class="btn btn-sm  btn-primary" runat="server" Text="New" />--%>&nbsp;
                        <asp:Button ID="btnSearch" class="btn btn-sm  btn-success" runat="server" Text="Search" />&nbsp;
                        <asp:Button ID="btnClear" class="btn btn-sm  btn-secondary" runat="server" Text="Clear" />&nbsp;
                            <asp:Button ID="btnExport" class="btn btn-sm  btn-info" runat="server" Text="Export" />&nbsp;
                        </div>
                    </div>

                    <%-- <% If operator_code.IndexOf(Session("usercode").ToString) > -1 Then%>--%>

                    <div class="row">
                        <div class="col-auto" style="margin-left: auto;">
                            <input class="form-check-input chk-img-after" type="checkbox" id="chkCO" name="pay[1][]" runat="server">
                            <asp:Label ID="lbchkCO" CssClass="form-check-label" AssociatedControlID="chkCO" runat="server" Text="CO" />
                        </div>
                        <div class="col-auto">
                            <input class="form-check-input chk-img-after" type="checkbox" id="chkHO" name="pay[1][]" runat="server">
                            <asp:Label ID="lbchkHO" CssClass="form-check-label" AssociatedControlID="chkHO" runat="server" Text="HO" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 mb-3">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">สังกัด</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboCompany" runat="server" AutoPostBack="false"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">เจ้าของ KPI</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboCreateby" runat="server" readonly="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 HO">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ฝ่าย</span>
                                </div>
                                <asp:DropDownList ID="cboDepartment" class="form-control" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 HO">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">แผนก</span>
                                </div>
                                <asp:DropDownList ID="cboSection" class="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 CO">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ประเภทสาขา</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboBranchGroup" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 mb-3 CO">
                            <div class="input-group">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">สาขา</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboBranch" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12 mb-3 HO">
                            <asp:Label ID="note" CssClass="text-danger text-right" runat="server" Text="( เงื่อนไข : รายการใน 'แผนก' จะเปลี่ยนไปตาม 'ฝ่าย' ที่เลือก )" />
                        </div>
                        <div class="col-md-12 mb-3 CO">
                            <asp:Label ID="note2" CssClass="text-danger text-right" runat="server" Text="( เงื่อนไข : รายการใน 'สาขา' จะเปลี่ยนไปตาม 'ประเภทสาขา' ที่เลือก )" />
                        </div>
                    </div>
                   
                    <!------------------------------------------------------------------------>

                        <%--begin item--%>
                        <% If AllKpi IsNot Nothing Then%>
                        <%  Dim tempowner As String = "" %>
                        <%  Dim temp As String = "" %>
                        <%  Dim cnt_child As Integer = 0 %>
                        <% For k = 0 To AllKpi.Tables(0).Rows.Count - 1 %>
                        <% If not AllKpi.Tables(0).Rows(k).Item("ownercode").ToString = tempowner Then %>

                        <div class="row">
                            <div class="col">
                                <h4><%= AllKpi.Tables(0).Rows(k).Item("ownercode").ToString%></h4>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col table-responsive-xl">

                                <table class="table table-sm shadow-sm table__header">
                                    <thead class="thead-blue ">
                                        <tr>
                                            <th class="text-center align-middle" rowspan="2">Ratio</th>
                                            <th class="text-center align-middle" rowspan="2">หัวข้อ KPIs</th>
                                            <th class="text-center align-middle" rowspan="2">น้ำหนัก</th>
                                            <th class="text-center align-middle" rowspan="2">หน่วย</th>
                                            <th class="text-center align-middle" colspan="5">ระดับประเมิน</th>
                                            <th class="text-center align-middle" rowspan="2"></th>
                                        </tr>
                                        <tr>
                                            <th class="text-center align-middle">5</th>
                                            <th class="text-center align-middle">4</th>
                                            <th class="text-center align-middle">3</th>
                                            <th class="text-center align-middle">2</th>
                                            <th class="text-center align-middle">1</th>
                                        </tr>
                                    </thead>

                                    <tbody>

                                        <% For i = 0 To AllKpi.Tables(1).Rows.Count - 1 %>
                                        <% If AllKpi.Tables(0).Rows(k).Item("ownercode").ToString = AllKpi.Tables(1).Rows(i).Item("ownercode").ToString Then %>
                                        <% If not AllKpi.Tables(1).Rows(i).Item("Kpi_Code").ToString = temp Then %>


                                        <tr data-toggle="collapse" data-target="#<%= AllKpi.Tables(1).Rows(i).Item("Kpi_Code").ToString %>" class="accordion-toggle text-center" aria-expanded="false">
                                            <td><%= AllKpi.Tables(1).Rows(i).Item("CategoryName").ToString %></td>
                                            <td class="text-left"><%= AllKpi.Tables(1).Rows(i).Item("Title").ToString %></td>
                                            <td><%= AllKpi.Tables(1).Rows(i).Item("Weight").ToString %></td>
                                            <td><%= AllKpi.Tables(1).Rows(i).Item("Unit").ToString %></td>
                                            <td><%= AllKpi.Tables(1).Rows(i).Item("Lv5").ToString %></td>
                                            <td><%= AllKpi.Tables(1).Rows(i).Item("Lv4").ToString %></td>
                                            <td><%= AllKpi.Tables(1).Rows(i).Item("Lv3").ToString %></td>
                                            <td><%= AllKpi.Tables(1).Rows(i).Item("Lv2").ToString %></td>
                                            <td><%= AllKpi.Tables(1).Rows(i).Item("Lv1").ToString %></td>
                                            <td>
                                                <%-- <button type="button" class="btn btn-sm btn-danger">
                                                    <i class="far fa-bell"></i><span class="badge badge-danger">4</span>
                                                </button>--%>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td colspan="12" class="hiddenRow">
                                                <div class="accordian-body collapse" id="<%= AllKpi.Tables(1).Rows(i).Item("Kpi_Code").ToString %>">
                                                    <table class="table table-hover table__inner">
                                                        <thead class="thead-light">
                                                            <tr class="info text-center">
                                                                <th class="text-nowrap align-middle">
                                                                    <a href="KPIsOverview.aspx?uc=<%= AllKpi.Tables(1).Rows(i).Item("ownercode").ToString %>" title="Overview"><i class="fas fa-chart-pie "></i></a>&nbsp;&nbsp;
                                                                    <a href="KPIsEdit.aspx?Kpi_Code=<%= AllKpi.Tables(1).Rows(i).Item("Kpi_Code").ToString %>" title="EditDetail"><i class="fas fa-edit"></i></a>
                                                                </th>
                                                                <th>แผนงาน (เป้าหมาย/เดือน)</th>
                                                                <th>ผลตามแผน</th>
                                                                <th>ผลการปฏิบัติงาน</th>
                                                                <th>ตนเองประเมิน</th>
                                                                <th>หัวหน้าประเมิน</th>
                                                                <th>Feedback</th>
                                                            </tr>
                                                        </thead>

                                                        <tbody>
                                                            <% For j = 0 To AllKpi.Tables(2).Rows.Count - 1 %>
                                                            <% If AllKpi.Tables(1).Rows(i).Item("Kpi_Code").ToString = AllKpi.Tables(2).Rows(j).Item("actionkpi_code").ToString Then %>
                                                            <tr class="text-center" >

                                                                <td>
                                                                    <span class="badge badge-blue"><%= AllKpi.Tables(2).Rows(j).Item("actionmonth").ToString %></span></td>

                                                                <td class="text-left pl-5"><%= AllKpi.Tables(2).Rows(j).Item("actiontitle").ToString %></td>
                                                                <td>
                                                                    <% If AllKpi.Tables(2).Rows(j).Item("actionmonthly").ToString = "1" Then %>
                                                                        <span class="badge badge-success">ตามแผนที่กำหนด</span>

                                                                    <% else If AllKpi.Tables(2).Rows(j).Item("actionmonthly").ToString = "2" Then %>
                                                                        <span class="badge badge-danger">ช้ากว่าแผนที่กำหนด</span>
                                                                    <% End if %>
                                                                </td>
                                                                <td class="text-left"><%= AllKpi.Tables(2).Rows(j).Item("actiontitleresult").ToString %></td>
                                                                <td><%= AllKpi.Tables(2).Rows(j).Item("actionrateowner").ToString %></td>
                                                                <td><%= AllKpi.Tables(2).Rows(j).Item("actionratehead").ToString %></td>
                                                                <td class="text-left"><%= AllKpi.Tables(2).Rows(j).Item("actionfeedback").ToString %></td>
                                                            </tr>

                                                            <% End if %>
                                                            <% Next j %>
                                                        </tbody>
                                                    </table>

                                                </div>
                                            </td>
                                        </tr>
                                        <% End if %>
                                        <% End if %>
                                        <% temp = AllKpi.Tables(1).Rows(i).Item("Kpi_Code").ToString %>

                                        <% Next i %>
                                    </tbody>
                                </table>

                            </div>
                        </div>

                        <% End if %>

                        <% tempowner = AllKpi.Tables(0).Rows(k).Item("ownercode").ToString %>


                        <% Next k %>





                        <% End if %>
                        <%-- end item--%>
                    <!------------------------------------------------------------------------>
                </div>

            </div>
        </div>
    </div>

    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
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
                        <asp:Label ID="lbcboAccountCode" CssClass="form-label" AssociatedControlID="cboAccountCode" runat="server" Text="ผลตามแผน" />
                        <asp:Label ID="lbcboAccountCodeMandatory" CssClass="text-danger" AssociatedControlID="cboAccountCode" runat="server" Text="*" />
                        <asp:DropDownList class="form-control" ID="cboAccountCode" runat="server" onchange="setdetail(this);"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbDetail" CssClass="form-label" AssociatedControlID="txtDetail" runat="server" Text="รายละเอียด" />
                        <asp:TextBox class="form-control" ID="txtDetail" runat="server" Rows="3" Columns="50" TextMode="MultiLine" onkeyDown="checkTextAreaMaxLength(this,event,'255');" autocomplete="off"></asp:TextBox>
                        <div class="invalid-feedback">กรุณากรอกรายละเอียด</div>
                    </div>
                    <%-- <div class="form-group">
                        <asp:Label ID="Label4" CssClass="form-label" AssociatedControlID="cboDep" runat="server" Text="Department" />
                        <asp:DropDownList class="form-control" ID="cboDep" runat="server"></asp:DropDownList>
                    </div>--%>
                    <%-- <div class="form-group">
                        <asp:Label ID="lbcboVendor" CssClass="form-label" AssociatedControlID="cboVendor" runat="server" Text="Vendor" />
                        <asp:DropDownList class="form-control" ID="cboVendor" runat="server" onchange="setVendor(this);"></asp:DropDownList>
                        <asp:TextBox class="form-control" ID="txtVendor" runat="server" TextMode="MultiLine" Rows="1"></asp:TextBox>
                    </div>--%>
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

                    <%--<div class="form-group">
                        <asp:Label ID="lbVat" CssClass="form-label" AssociatedControlID="txtVat" runat="server" Text="VAT" />
                        <asp:TextBox class="form-control noEnterSubmit" type="number" ID="txtVat" runat="server" Text="0"'></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbTax" CssClass="form-label" AssociatedControlID="txtPrice" runat="server" Text="TAX" />
                        <asp:TextBox class="form-control noEnterSubmit" type="number" ID="txtTax" runat="server" Text="0"'></asp:TextBox>
                    </div>--%>

                    <div class="form-group" style="display: none;">
                        <asp:Label ID="Label1" CssClass="form-label" AssociatedControlID="cboDep" runat="server" Text="cboDep" />
                        <asp:DropDownList class="form-control" ID="cboDep" runat="server"></asp:DropDownList>
                    </div>
                    <div class="showCost">
                        <p class="text-muted" id="p_vat"></p>
                        <p class="text-muted" id="p_tax"></p>
                        <p class="text-muted" id="p_cost"></p>
                    </div>
                    <!--  ############## End Detail ############### -->
                    <hr />
                    <h3>ใบแจ้งหนี้ / ใบส่งของ / ใบกำกับ</h3>
                    <div class="form-group">
                        <asp:Label ID="lbtaxid" CssClass="form-label" AssociatedControlID="txttaxid" runat="server" Text="Tax ID no." />
                        <asp:TextBox class="form-control noEnterSubmit" type="input" ID="txttaxid" runat="server" autocomplete="off"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <div class="row justify-content-between">
                            <div class="col">
                                <asp:Label ID="lbinvoiceno" CssClass="form-label" AssociatedControlID="txtinvoiceno" runat="server" Text="Document no." />
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
                        <asp:TextBox class="form-control noEnterSubmit" type="input" ID="txtinvoiceno" runat="server" autocomplete="off"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lbinvoicedate" CssClass="form-label" AssociatedControlID="txtinvoicedate" runat="server" Text="Document date" />
                        <asp:TextBox class="form-control noEnterSubmit" type="input" ID="txtinvoicedate" runat="server" placeholder="--- คลิกเพื่อเลือก ---" autocomplete="off"></asp:TextBox>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary noEnterSubmit" data-dismiss="modal">Close</button>
                    <%--<button type="button" id="btnAddDetail" class="btn btn-primary noEnterSubmit">Save</button>--%>
                    <asp:Button ID="btnAddDetails" class="btn btn-primary" runat="server" Text="Save" OnClientClick="postBack_addDetail();" />

                </div>
            </div>
        </div>
    </div>
    <script src="<%=Page.ResolveUrl("~/js/Sortable.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>
    <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/NonPO.js")%>"></script>
    <script type="text/javascript">
        jQuery('[id$=txtDuedate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08'
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
        $(document).ready(function () {
            $('.form-control').selectpicker({
                noneSelectedText: '-',
                liveSearch: true,
                maxOptions: 1
            });


            $('.form-control').selectpicker('refresh');

            checkCOorHO();
        });
        function btnEditDetailClick(ele) {
            console.log(ele);
            event.preventDefault();
            $('#exampleModal').modal('show');

            return false;
        }
        function checkCOorHO() {
            if ($("#" + "<%= chkHO.ClientID.ToString %>").is(":checked")) {
                $(".HO").show();
                $(".CO").hide();
            } else if ($("#" + "<%= chkCO.ClientID.ToString %>").is(":checked")) {
                $(".CO").show();
                $(".HO").hide();
            } else {
                $(".CO").hide();
                $(".HO").hide();

            }

        }
        $("input:checkbox").on('click', function () {
            // in the handler, 'this' refers to the box clicked on
            console.log(this);
            var $box = $(this);
            console.log($box.is(":checked"));
            if ($box.is(":checked")) {
                // the name of the box is retrieved using the .attr() method
                // as it is assumed and expected to be immutable
                var group = "input:checkbox";
                // the checked state of the group/box on the other hand will change
                // and the current value is retrieved using .prop() method
                $(group).prop("checked", false);
                $box.prop("checked", true);

            } else {

                $box.prop("checked", true);
                //$box.prop("checked", false);
            }

            checkCOorHO();
        });
        $('.noEnterSubmit').keypress(function (e) {
            if (e.which == 13) return false;
            //or...
            if (e.which == 13) e.preventDefault();
        });
    </script>
</asp:Content>
