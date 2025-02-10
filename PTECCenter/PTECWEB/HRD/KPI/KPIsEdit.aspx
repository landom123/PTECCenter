<%@ Page Title="KPIsEdit" Language="vb" AutoEventWireup="true" MasterPageFile="~/site.Master" CodeBehind="KPIsEdit.aspx.vb" Inherits="PTECCENTER.KPIsEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
    <link href="<%=Page.ResolveUrl("~/css/card_comment.css")%>" rel="stylesheet">
    <style>
        #content-wrapper {
            font-size: .8rem;
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
            background-color: #ececec;
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

        .tooltip-inner {
            max-width: 320px;
        }
        /*.approval {
            display:none;
        }*/
        .form-control:disabled, .form-control[readonly], .btn-light.disabled {
            color: #acacac;
            background-color: #dddddd;
            opacity: 1;
        }

        tr .form-control, tr .filter-option-inner-inner {
            font-size: .75rem;
        }

        .badge-blue {
            font-size: .65rem;
        }

        .border__solid {
            border: solid !important;
            border-color: red !important;
        }

        .showUpdateDate:hover::after, .actiontitle:hover::after, .actionrateowner:hover::after, .actionratehead:hover::after {
            position: absolute;
            z-index: 999;
            display: block;
            margin-left: auto;
            margin-right: auto;
            text-align: center;
            content: attr(data-content);
            color: #ff7474 !important;
            font-size: 0.65rem;
            transition: .55s ease-in-out;
        }

        .row__ap td:first-child::after {
            position: absolute;
            z-index: 999;
            display: block;
            content: attr(data-content);
            color: #ff0000 !important;
            font-size: 0.75rem;
        }

        .visible {
            visibility: visible;
            opacity: 1;
            transition: opacity 2s linear;
        }

        .hidden {
            visibility: hidden;
            opacity: 0;
            transition: visibility 0s 2s, opacity 2s linear;
        }


        .footer__page {
            position: fixed;
            bottom: 0;
            background-color: #e9ecef;
            height: 65px;
            z-index: 999;
            visibility: hidden;
            opacity: 0;
            transition: height 1s;
        }

            .footer__page:has(.footer__btn) {
                visibility: visible;
                opacity: 1;
            }

            .footer__page .footer__btn::after {
                position: absolute;
                content: '';
                top: 10px;
                margin-left: 50px;
                z-index: 999;
                padding: 8px 8px;
                border-radius: 50%;
                background-color: red;
            }

        .kpicompleted {
            overflow-x: auto;
            overflow-y: visible;
            padding: 0;
            margin-right: auto;
            margin-left: auto;
        }

            .kpicompleted input {
                border-top-left-radius: 10px;
                border-top-right-radius: 10px;
                border-bottom-left-radius: 0px;
                border-bottom-right-radius: 0px;
            }

        .pre-line {
            white-space: pre-line;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg">
        <div id="wrapper" class="h-100">

            <!-- #include virtual ="/include/menu.inc" -->
            <!-- add side menu -->
            <div id="content-wrapper">
                <div class="px-sm-5  px-3">
                    <div class="row">
                        <div class="col text-left align-self-center">
                        </div>
                        <div class="col-auto text-right align-self-center">
                            <a href="KPIsList.aspx" class="btn btn-sm btn-danger ">
                                <i class="fa fa-tasks" aria-hidden="true"></i></a>
                        </div>
                    </div>
                    <div class="row d-none">
                        <div class="col">
                            Performance Update : KPIs & Competency

                        </div>

                    </div>

                    <div class="row align-items-center justify-content-center">
                        <div class="col-lg-auto  text-center">
                            <img runat="server" id="logo" class="logopure" src="#" alt="logopure" width="500" height="600">
                        </div>
                        <div class="col-lg-auto company">
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
                    <div class="foram mb-3 px-lg-5">
                        <div class="row">
                            ข้อมูลหลังบ้าน <%=OPT %>
                        </div>
                        <div class="row">
                            <%=allOwner %>
                        </div>
                        <div class="row">
                            หัวหน้าที่มีสิทธิประเมิน : <%=now_action %>
                        </div>
                        <div class="row">
                            กำหนดวันที่ปัจจุบันเพื่อใช้ในการทดสอบ : <%=now_date %>
                        </div>
                    </div>
                    <!------------------------------------------------------------------------>
                    <div class="mb-3">

                        <div class="row">
                            <div class="col">
                                <h4>
                                    <% If Not Request.QueryString("Kpi_Code") Is Nothing And AllKpi IsNot Nothing Then%>
                                    <%= AllKpi.Tables(0).Rows(0).Item("ownercode").ToString() %>
                                    <% End If%></h4>
                            </div>
                            <div class="col">
                                <div class="col w-100 kpicompleted text-right align-self-center" style="display: none;">
                                    <asp:TextBox class="btn btn-success" ID="txtUnsave" runat="server" ReadOnly="true">Completed</asp:TextBox>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col table-responsive-xl">

                                <table class="table table-sm shadow-sm">
                                    <thead class="" style="background-color: #DCCCBB;">
                                        <tr>

                                            <% If Not Request.QueryString("Kpi_Code") Is Nothing And AllKpi IsNot Nothing Then%>
                                            <% If AllKpi.Tables(0).Rows(0).Item("operatortype").ToString = "A" Then %>
                                            <th class="text-center align-middle" rowspan="2">OwnerKPI</th>
                                            <% End If %>
                                            <% End If %>
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
                                        <%--begin item--%>
                                        <%  Dim temp As String = "" %>
                                        <%  Dim cnt_child As Integer = 0 %>
                                        <% If Not Request.QueryString("Kpi_Code") Is Nothing And AllKpi IsNot Nothing Then%>
                                        <% For i = 0 To AllKpi.Tables(0).Rows.Count - 1 %>
                                        <% If not AllKpi.Tables(0).Rows(i).Item("Kpi_Code").ToString = temp Then %>

                                        <tr class=" text-center">
                                            <% If AllKpi.Tables(0).Rows(0).Item("operatortype").ToString = "A" Then %>
                                            <td><%= AllKpi.Tables(0).Rows(i).Item("ownercode").ToString %></td>
                                            <% End If %>
                                            <td><%= AllKpi.Tables(0).Rows(i).Item("CategoryName").ToString %></td>
                                            <td class="text-left pre-line"><%= AllKpi.Tables(0).Rows(i).Item("Title").ToString %></td>
                                            <td><%= AllKpi.Tables(0).Rows(i).Item("Weight").ToString %></td>
                                            <td><%= AllKpi.Tables(0).Rows(i).Item("Unit").ToString %></td>
                                            <td><%= AllKpi.Tables(0).Rows(i).Item("Lv5").ToString %></td>
                                            <td><%= AllKpi.Tables(0).Rows(i).Item("Lv4").ToString %></td>
                                            <td><%= AllKpi.Tables(0).Rows(i).Item("Lv3").ToString %></td>
                                            <td><%= AllKpi.Tables(0).Rows(i).Item("Lv2").ToString %></td>
                                            <td><%= AllKpi.Tables(0).Rows(i).Item("Lv1").ToString %></td>
                                            <td>
                                                <%-- <button type="button" class="btn btn-sm btn-danger">
                                                    <i class="far fa-bell"></i><span class="badge badge-danger">4</span>
                                                </button>--%>
                                            </td>

                                        </tr>
                                        <% If AllKpi.Tables(0).Rows(0).Item("operatortype").ToString = "A" Then %>
                                        <tr class=" text-center">
                                            <td>
                                                <asp:DropDownList class="form-control" ID="cboOwnerKPI" runat="server"></asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList class="form-control" ID="cboRatio" runat="server"></asp:DropDownList>
                                            </td>
                                            <td>
                                                <textarea rows="2" cols="30" class="form-control" name="actiontitle" id="txtKpititle" runat="server"></textarea>
                                            </td>
                                            <td>
                                                <asp:TextBox class="form-control" type="input" ID="txtWeight" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox class="form-control" type="input" ID="txtUnit" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox class="form-control" type="input" ID="txtlv5" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox class="form-control" type="input" ID="txtlv4" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox class="form-control" type="input" ID="txtlv3" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox class="form-control" type="input" ID="txtlv2" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox class="form-control" type="input" ID="txtlv1" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnUpdateKPI" class="btn btn-sm  btn-warning btnupdatekpi" runat="server" Text="UpdateKPI" />
                                            </td>

                                        </tr>
                                        <% End If %>
                                        <tr>
                                            <td colspan="12" class="hiddenRow">
                                                <div class="accordian-body " id="<%= AllKpi.Tables(0).Rows(i).Item("Kpi_Code").ToString %>">
                                                    <table class="table table-sm table-hover">
                                                        <thead class="thead-light">
                                                            <tr class="info text-center">
                                                                <th></th>
                                                                <th>เลขสาขา</th>
                                                                <th>เจ้าของงาน</th>
                                                                <th>แผนงาน / เป้าหมาย</th>
                                                                <th>ผลตามแผน</th>
                                                                <th>ผลการปฏิบัติงาน</th>
                                                                <th>พนักงานประเมิน</th>
                                                                <th class="approval">หัวหน้าประเมิน</th>
                                                                <th class="approval">Feedback</th>
                                                                <th></th>
                                                                <%--<th>หัวหน้าประเมิน</th>
                                                                <th>Feedback</th>--%>
                                                            </tr>
                                                        </thead>

                                                        <tbody>

                                                            <% If Not Request.QueryString("Kpi_Code") Is Nothing And AllKpi.Tables(1).Rows.Count > 0 Then%>
                                                            <% For j = 0 To AllKpi.Tables(1).Rows.Count - 1 %>
                                                            <% cnt_child = cnt_child + 1 %>
                                                            <% If AllKpi.Tables(0).Rows(i).Item("Kpi_Code").ToString = AllKpi.Tables(1).Rows(j).Item("actionkpi_code").ToString Then %>
                                                            <tr class="text-center row__ap" id="actionid_<%= AllKpi.Tables(1).Rows(j).Item("actionid").ToString %>" onchange="change(this);" data-datenow="<%= AllKpi.Tables(0).Rows(i).Item("datenow").ToString %>" data-opt="<%= AllKpi.Tables(0).Rows(i).Item("operatortype").ToString %>"
                                                                data-empuppercode="<%= AllKpi.Tables(1).Rows(j).Item("actionempuppercode").ToString %>"
                                                                data-ownercode="<%= AllKpi.Tables(1).Rows(j).Item("actionownercode").ToString %>"
                                                                data-ownername="<%= AllKpi.Tables(1).Rows(j).Item("NameOwner").ToString %>"
                                                                data-ownerbranch="<%= AllKpi.Tables(1).Rows(j).Item("actionbranchid").ToString %>"
                                                                data-usercode="<%= Session("usercode").ToString %>"
                                                                data-username="<%= Session("username").ToString %>"
                                                                data-managername="<%= managername %>"
                                                                data-title="<%= AllKpi.Tables(1).Rows(j).Item("actiontitle").ToString %>"
                                                                data-titlestart="<%= AllKpi.Tables(1).Rows(j).Item("actiontitle_begindate").ToString %>"
                                                                data-titleend="<%= AllKpi.Tables(1).Rows(j).Item("actiontitle_enddate").ToString %>"
                                                                data-editstart="<%= AllKpi.Tables(1).Rows(j).Item("actionedit_begindate").ToString %>"
                                                                data-editend="<%= AllKpi.Tables(1).Rows(j).Item("actionedit_enddate").ToString %>"
                                                                data-approvalstart="<%= AllKpi.Tables(1).Rows(j).Item("actionapproval_begindate").ToString %>"
                                                                data-approvalend="<%= AllKpi.Tables(1).Rows(j).Item("actionapproval_enddate").ToString %>"
                                                                data-nameownerstart="<%= AllKpi.Tables(1).Rows(j).Item("actionnameowner_begindate").ToString %>"
                                                                data-nameownerend="<%= AllKpi.Tables(1).Rows(j).Item("actionnameowner_enddate").ToString %>"
                                                                data-ownerbranchstart="<%= AllKpi.Tables(1).Rows(j).Item("actionownerbranch_begindate").ToString %>"
                                                                data-ownerbranchend="<%= AllKpi.Tables(1).Rows(j).Item("actionownerbranch_enddate").ToString %>">
                                                                <td><span class="badge badge-blue <%= If(AllKpi.Tables(1).Rows(j).Item("nowMonths") = 1, "border__solid", "") %>"><%= AllKpi.Tables(1).Rows(j).Item("actionmonth").ToString %></span>
                                                                <td class="showUpdateDate" data-content="<%= AllKpi.Tables(1).Rows(j).Item("actionownerbranch_updatedate").ToString %>">
                                                                    <!--------------------------เลขสาขา------------------------------->
                                                                    <a runat="server" class="text-primary" name="actionbranchBtn" style="cursor: pointer; transition: .2s;" onclick="btnEditOwnerBranch(this)">
                                                                        <i class="fas fa-edit"></i></a>
                                                                    <span><%= AllKpi.Tables(1).Rows(j).Item("actionbranchname").ToString %></span>

                                                                </td>
                                                                <td class="showUpdateDate" data-content="<%= AllKpi.Tables(1).Rows(j).Item("actionnameowner_updatedate").ToString %>">
                                                                    <!--------------------------เจ้าของงาน------------------------------->
                                                                    <% If AllKpi.Tables(0).Rows(0).Item("operatortype").ToString = "A" Then %>
                                                                    <a runat="server" class="text-primary" style="cursor: pointer; transition: .2s;" onclick="btnEditOwnerActionplan(this)">
                                                                        <i class="fas fa-edit"></i></a>
                                                                    <% End If %>
                                                                    <span><%= AllKpi.Tables(1).Rows(j).Item("NameOwner").ToString %></span>
                                                                </td>
                                                                <td class="text-left actiontitle" data-content="<%= AllKpi.Tables(1).Rows(j).Item("actiontitle_date").ToString %>">
                                                                    <% If (String.IsNullOrEmpty(AllKpi.Tables(0).Rows(0).Item("operatortype").ToString)) Then%>
                                                                    <a runat="server" name="actiontitleBtn" class="text-primary" style="cursor: pointer; transition: .2s;" onclick="btnEditplan(this)" hidden>
                                                                        <i class="fas fa-edit"></i></a>
                                                                    <span name="actiontitle"><%= AllKpi.Tables(1).Rows(j).Item("actiontitle").ToString %></span>
                                                                    <% ElseIf AllKpi.Tables(0).Rows(0).Item("operatortype").ToString = "A" Then %>
                                                                    <textarea rows="2" cols="40" class="form-control" name="actiontitle" id="actiontitle_<%= AllKpi.Tables(1).Rows(j).Item("actionid").ToString %>"><%= AllKpi.Tables(1).Rows(j).Item("actiontitle").ToString %></textarea>
                                                                    <% End If %>
                                                                </td>


                                                                <td>
                                                                    <select class="form-control" name="actionmonthly" id="actionmonthly_<%= AllKpi.Tables(1).Rows(j).Item("actionid").ToString %>">
                                                                        <option value="0">-</option>
                                                                        <option value="3" <% If AllKpi.Tables(1).Rows(j).Item("actionmonthly").ToString = "3" Then %>selected="selected" <% End if %>>เร็วกว่าแผน</option>
                                                                        <option value="1" <% If AllKpi.Tables(1).Rows(j).Item("actionmonthly").ToString = "1" Then %>selected="selected" <% End if %>>ตามแผน</option>
                                                                        <option value="2" <% If AllKpi.Tables(1).Rows(j).Item("actionmonthly").ToString = "2" Then %>selected="selected" <% End if %>>ช้ากว่าแผน</option>
                                                                    </select>
                                                                    <%--<%= AllKpi.Tables(1).Rows(j).Item("actionmonthly").ToString %></td>--%>


                                                                <td class="text-left">
                                                                    <textarea class="form-control" rows="2" cols="40" name="actiontitleresult" autocomplete="off" id="actiontitleresult_<%= AllKpi.Tables(1).Rows(j).Item("actionid").ToString %>" /><%= AllKpi.Tables(1).Rows(j).Item("actiontitleresult").ToString %></textarea>
                                                                    <%--<%= AllKpi.Tables(1).Rows(j).Item("actiontitleresult").ToString %>--%>

                                                                </td>
                                                                <td class="actionrateowner" data-content="<%= AllKpi.Tables(1).Rows(j).Item("actionrateowner_date").ToString %>">
                                                                    <select class="form-control" name="actionrateowner" id="actionrateowner_<%= AllKpi.Tables(1).Rows(j).Item("actionid").ToString %>">
                                                                        <option value="">-</option>
                                                                        <option value="5" <% If AllKpi.Tables(1).Rows(j).Item("actionrateowner").ToString = "5" Then %>selected="selected" <% End if %>>5</option>
                                                                        <option value="4" <% If AllKpi.Tables(1).Rows(j).Item("actionrateowner").ToString = "4" Then %>selected="selected" <% End if %>>4</option>
                                                                        <option value="3" <% If AllKpi.Tables(1).Rows(j).Item("actionrateowner").ToString = "3" Then %>selected="selected" <% End if %>>3</option>
                                                                        <option value="2" <% If AllKpi.Tables(1).Rows(j).Item("actionrateowner").ToString = "2" Then %>selected="selected" <% End if %>>2</option>
                                                                        <option value="1" <% If AllKpi.Tables(1).Rows(j).Item("actionrateowner").ToString = "1" Then %>selected="selected" <% End if %>>1</option>
                                                                        <option value="0" <% If AllKpi.Tables(1).Rows(j).Item("actionrateowner").ToString = "0" Then %>selected="selected" <% End if %>>0</option>
                                                                    </select>
                                                                    <%--<%= AllKpi.Tables(1).Rows(j).Item("actionrateowner").ToString %>--%>
                                                                </td>
                                                                <td class="approval actionratehead" data-content="<%= AllKpi.Tables(1).Rows(j).Item("actionratehead_date").ToString %>">
                                                                    <select class="form-control" name="actionratehead" id="actionratehead_<%= AllKpi.Tables(1).Rows(j).Item("actionid").ToString %>">
                                                                        <option value="">-</option>
                                                                        <option value="5" <% If AllKpi.Tables(1).Rows(j).Item("actionratehead").ToString = "5" Then %>selected="selected" <% End if %>>5</option>
                                                                        <option value="4" <% If AllKpi.Tables(1).Rows(j).Item("actionratehead").ToString = "4" Then %>selected="selected" <% End if %>>4</option>
                                                                        <option value="3" <% If AllKpi.Tables(1).Rows(j).Item("actionratehead").ToString = "3" Then %>selected="selected" <% End if %>>3</option>
                                                                        <option value="2" <% If AllKpi.Tables(1).Rows(j).Item("actionratehead").ToString = "2" Then %>selected="selected" <% End if %>>2</option>
                                                                        <option value="1" <% If AllKpi.Tables(1).Rows(j).Item("actionratehead").ToString = "1" Then %>selected="selected" <% End if %>>1</option>
                                                                        <option value="0" <% If AllKpi.Tables(1).Rows(j).Item("actionratehead").ToString = "0" Then %>selected="selected" <% End if %>>0</option>
                                                                    </select>
                                                                </td>
                                                                <td class="approval">
                                                                    <input class="form-control" type="text" name="actionfeedback" value="<%= AllKpi.Tables(1).Rows(j).Item("actionfeedback").ToString %>" id="actionfeedback_<%= AllKpi.Tables(1).Rows(j).Item("actionid").ToString %>" />
                                                                </td>
                                                                <td>
                                                                    <span class="text-muted" data-toggle="tooltip" data-placement="top" data-html="true"
                                                                        title="<span>สาขา : <%= AllKpi.Tables(1).Rows(j).Item("actionownerbranch_begindate").ToString %> - <%= AllKpi.Tables(1).Rows(j).Item("actionownerbranch_enddate").ToString %></span><br /><span>เจ้าของ : <%= AllKpi.Tables(1).Rows(j).Item("actionnameowner_begindate").ToString %> - <%= AllKpi.Tables(1).Rows(j).Item("actionnameowner_enddate").ToString %></span><br /><span>แผนงาน : <%= AllKpi.Tables(1).Rows(j).Item("actiontitle_begindate").ToString %> - <%= AllKpi.Tables(1).Rows(j).Item("actiontitle_enddate").ToString %></span><br /><span>ตนเองประเมิน : <%= AllKpi.Tables(1).Rows(j).Item("actionedit_begindate").ToString %> - <%= AllKpi.Tables(1).Rows(j).Item("actionedit_enddate").ToString %></span><br /><span>หัวหน้าประเมิน : <%= AllKpi.Tables(1).Rows(j).Item("actionapproval_begindate").ToString %> - <%= AllKpi.Tables(1).Rows(j).Item("actionapproval_enddate").ToString %></span>">
                                                                        <i class="fas fa-info-circle"></i></span>
                                                                </td>
                                                            </tr>

                                                            <% End if %>
                                                            <% Next j %>

                                                            <% End if %>
                                                        </tbody>
                                                    </table>

                                                </div>
                                            </td>
                                        </tr>

                                        <% cnt_child = 0 %>
                                        <% End if %>

                                        <% temp = AllKpi.Tables(0).Rows(i).Item("Kpi_Code").ToString %>
                                        <% Next i %>
                                        <% End if %>
                                    </tbody>
                                </table>

                            </div>
                        </div>









                        <%-- end item--%>
                    </div>
                    <!------------------------------------------------------------------------>
                    <div class="row d-none">
                        <div class="col-1">
                        </div>
                        <div class="col text-muted ">
                            <div class="form-check">
                                <input type="checkbox" class="form-check-input" id="chkKpiComplete" runat="server">
                                <asp:Label ID="lbchkKpiComplete" CssClass="form-check-label" AssociatedControlID="chkKpiComplete" runat="server" Text="กรณีที่ KPIs ของท่านดำเนินการเสร็จเรียบร้อยแล้ว กรุณาติ๊กเครื่องหมายถูกเพื่อปิดการแจ้งเตือน KPIs ในเดือนถัดไป" />
                            </div>
                        </div>

                    </div>

                </div>

            </div>
        </div>
    </div>
    <div class="footer__page w-100 ">
        <div class="d-flex justify-content-center align-items-center h-100">
            <asp:Button type="button" OnClientClick="return update();" ID="btnUpdate" runat="server" class="btn btn-warning position-relative btnUpdate" Text="Save"></asp:Button>
            <asp:Button type="button" OnClientClick="return updateOP();" ID="btnUpdateOP" runat="server" class="btn btn-warning position-relative btnUpdate" Text="Save"></asp:Button>
        </div>
    </div>
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel_report" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel_report">รายชื่อเจ้าของงาน</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <input type="hidden" class="form-control" id="hiddenAdvancedetailid" value="0" runat="server">
                    <div class="form-group">
                        <asp:Label ID="lbUserName" CssClass="form-label" AssociatedControlID="cboUserName" runat="server" Text="UserName" />
                        <asp:DropDownList class="form-control" ID="cboUserName" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary noEnterSubmit" data-dismiss="modal">Close</button>
                    <%--<button type="button" id="btnAddDetail" class="btn btn-primary noEnterSubmit">Save</button>--%>

                    <asp:Button ID="btnUpdateOwnerAP" class="btn btn-primary" runat="server" Text="Update" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="exampleModalBranch" tabindex="-1" role="dialog" aria-labelledby="exampleModalBranch_report" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalBranch_report">สาขา</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <input type="hidden" class="form-control" id="hiddenbranchid" value="0" runat="server">
                    <div class="form-group">
                        <asp:Label ID="Label1" CssClass="form-label" AssociatedControlID="cboBranch" runat="server" Text="UserName" />
                        <asp:DropDownList class="form-control" ID="cboBranch" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary noEnterSubmit" data-dismiss="modal">Close</button>
                    <%--<button type="button" id="btnAddDetail" class="btn btn-primary noEnterSubmit">Save</button>--%>

                    <asp:Button ID="btnUpdateOwnerBranch" class="btn btn-primary" runat="server" Text="Update" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="exampleModalPlan" tabindex="-1" role="dialog" aria-labelledby="exampleModalPlanLabel_report" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalPlanLabel_report">แผนงานในเดือน </h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <input type="hidden" class="form-control" id="hiddenActionplanid" value="0" runat="server">
                    <textarea rows="2" cols="40" class="form-control" name="actiontitle" id="txtPlan" runat="server"></textarea>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary noEnterSubmit" data-dismiss="modal">Close</button>
                    <%--<button type="button" id="btnAddDetail" class="btn btn-primary noEnterSubmit">Save</button>--%>

                    <asp:Button ID="btnUpdateTitle" class="btn btn-primary" runat="server" Text="Update" />
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
            //console.log('START');

            $('.footer__page').hide();
            $('.form-control').selectpicker('refresh');

            $('[data-toggle="tooltip"]').tooltip();


            let elemActionmonthly = document.getElementsByName("actionmonthly");
            let elemActionbranchBtn = document.getElementsByName("actionbranchBtn");
            let elemActiontitleBtn = document.getElementsByName("actiontitleBtn");
            let elemActiontitle = document.getElementsByName("actiontitle");
            let elemActiontitleresult = document.getElementsByName("actiontitleresult");
            let elemActionrateowner = document.getElementsByName("actionrateowner");
            let elemActionratehead = document.getElementsByName("actionratehead");
            let elemActionFeedback = document.getElementsByName("actionfeedback");



            //console.log(elemActiontitle);
            //console.log(elemActiontitle[4].value);
            //console.log(elemActionmonthly);
            //console.log(elemActiontitleresult);
            //console.log(elemActionrateowner);


            let params = [];
            for (let i = 0; i < elemActionmonthly.length; i++) {
                params.push({
                    "actionid": elemActionmonthly[i].id.split("_")[1],
                    "actiontitle": elemActiontitle[i].value,
                    "actionmonthlyid": elemActionmonthly[i].id,
                    "actionmonthly": elemActionmonthly[i].value,
                    "actiontitleresultid": elemActiontitleresult[i].id,
                    "actiontitleresult": elemActiontitleresult[i].value,
                    "actionrateownerid": elemActionrateowner[i].id,
                    "actionrateowner": elemActionrateowner[i].value,
                    "actionrateheadid": elemActionratehead[i].id,
                    "actionratehead": elemActionratehead[i].value,
                    "actionfeedbackid": elemActionFeedback[i].id,
                    "actionfeedback": elemActionFeedback[i].value

                });

                const row = elemActionmonthly[i].parentElement.parentElement.parentElement;
                const datenow = row.getAttribute("data-datenow");
                const editstart = row.getAttribute("data-editstart");
                const editend = row.getAttribute("data-editend");
                const approvalstart = row.getAttribute("data-approvalstart");
                const approvalend = row.getAttribute("data-approvalend");
                const nameownerstart = row.getAttribute("data-nameownerstart");
                const nameownerend = row.getAttribute("data-nameownerend");
                const ownerbranchstart = row.getAttribute("data-ownerbranchstart");
                const ownerbranchend = row.getAttribute("data-ownerbranchend");
                const titlestart = row.getAttribute("data-titlestart");
                const titleend = row.getAttribute("data-titleend");
                const ownerbranch = row.getAttribute("data-ownerbranch");
                //--------------------------------------------------

                const empuppercode = row.getAttribute("data-empuppercode");
                const ownercode = row.getAttribute("data-ownercode");
                const ownername = row.getAttribute("data-ownername");
                const opt = row.getAttribute("data-opt");
                const usercode = row.getAttribute("data-usercode");
                const username = row.getAttribute("data-username");
                const managername = row.getAttribute("data-managername") || username;

                //--------------------------------------------------


                const [day0, month0, year0] = datenow.split('/');
                const [day1, month1, year1] = editstart.split('/');
                const [day2, month2, year2] = editend.split('/');
                const [day3, month3, year3] = approvalstart.split('/');
                const [day4, month4, year4] = approvalend.split('/');
                const [day5, month5, year5] = nameownerstart.split('/');
                const [day6, month6, year6] = nameownerend.split('/');
                const [day7, month7, year7] = ownerbranchstart.split('/');
                const [day8, month8, year8] = ownerbranchend.split('/');
                const [day9, month9, year9] = titlestart.split('/');
                const [day10, month10, year10] = titleend.split('/');


                const date = new Date(+year0, month0 - 1, +day0);
                const estart = new Date(+year1, month1 - 1, +day1);
                const eend = new Date(+year2, month2 - 1, +day2);
                const astart = new Date(+year3, month3 - 1, +day3);
                const aend = new Date(+year4, month4 - 1, +day4);
                const namestart = new Date(+year5, month5 - 1, +day5);
                const nameend = new Date(+year6, month6 - 1, +day6);
                const branchstart = new Date(+year7, month7 - 1, +day7);
                const branchend = new Date(+year8, month8 - 1, +day8);
                const tstart = new Date(+year9, month9 - 1, +day9);
                const tend = new Date(+year10, month10 - 1, +day10);

                /*console.log(ownername.indexOf(managername || usercode));
                console.log(ownername);
                console.log(managername);
                console.log(usercode);*/
                console.log(astart);
                console.log(aend);
                //console.log(ownercode.indexOf(usercode));

                if ((ownername.indexOf(managername) > -1) && !opt) { // เจ้าของ KPI
                    console.log('เจ้าของ KPI');

                    if (!(date >= estart && date <= eend)) { //เช็คอยู่ในช่วง edit ผลตามแผน ,ผลการปฏิบัติงาน , พนักงานประเมิน
                        row.style.backgroundColor = "#f2f3f4";
                        elemActionmonthly[i].disabled = true;
                        elemActiontitleresult[i].disabled = true;
                        elemActionrateowner[i].disabled = true;
                    } else {
                        //elemActionmonthly[i].disabled = (elemActionmonthly[i].value && elemActionmonthly[i].value > 0) ? true : false;
                        //elemActiontitleresult[i].disabled = (elemActiontitleresult[i].value) ? true : false;
                        //elemActionrateowner[i].disabled = (elemActionrateowner[i].value) ? true : false;
                        if (elemActionratehead[i].value && elemActionratehead[i].value >= 0) {
                            elemActionmonthly[i].disabled = true;
                            elemActiontitleresult[i].disabled = true;
                            elemActionrateowner[i].disabled = true;

                            row.style.backgroundColor = "#f2f3f4";
                        }
                    }


                    if (!(date >= branchstart && date <= branchend)) { //แผนงาน / เป้าหมาย
                        elemActionbranchBtn[i].hidden = true;
                    } else {
                        elemActionbranchBtn[i].hidden = (ownerbranch == 901) ? true : false;
                    }

                    console.log(`ownerbranch is ${ownerbranch}`);
                    //console.log(`tstart is ${tstart}`);
                    //console.log(`tend is ${tend}`);
                    if (!(date >= tstart && date <= tend)) { //แผนงาน / เป้าหมาย
                        elemActiontitleBtn[i].hidden = true;
                    } else {
                        if (elemActionrateowner[i].value && elemActionmonthly[i].value && elemActiontitleresult[i].value) {
                            elemActiontitleBtn[i].hidden = true;
                        } else {
                            elemActiontitleBtn[i].hidden = false;
                        }
                    }

                    elemActionratehead[i].disabled = true;
                    elemActionFeedback[i].disabled = true;

                } else if (empuppercode.indexOf(usercode) > -1 && !opt) { // empupper เจ้าของ KPI
                    console.log('empupper เจ้าของ KPI');

                    if (!(date > estart && date < eend)) {
                        console.log(`#${row.id}`);
                        row.style.backgroundColor = "#f2f3f4";
                    }

                    elemActionmonthly[i].disabled = true;
                    elemActiontitleresult[i].disabled = true;
                    elemActionrateowner[i].disabled = true;
                    elemActionbranchBtn[i].hidden = true;

                    if (!(date > astart && date < aend)) {

                        elemActionratehead[i].disabled = true;
                        elemActionFeedback[i].disabled = true;
                    } else if (!elemActionrateowner[i].value) {
                        elemActionratehead[i].disabled = true;
                        elemActionFeedback[i].disabled = true;
                        //} if ((date > astart && date < aend) && (elemActionrateowner[i].value) && !(elemActionratehead[i].value)) {
                        //    elemActionratehead[i].classList.add("border__solid");
                    }

                } else if (!opt) {
                    //console.log('555555555555555')
                    elemActiontitleBtn[i].hidden = true;
                    elemActiontitle[i].disabled = true;
                    elemActionmonthly[i].disabled = true;
                    elemActiontitleresult[i].disabled = true;
                    elemActionrateowner[i].disabled = true;

                    elemActionratehead[i].disabled = true;
                    elemActionFeedback[i].disabled = true;

                    $('table a,.btnupdatekpi').hide();
                    $('.badge-blue').removeClass("border__solid");
                    $('.row__ap').css("background-color", "#f2f3f4");
                    $('.row__ap textarea').attr('readonly', true);
                    $('.form-control ,.form-check-input').attr('disabled', true);

                    //$('#btnUpdate').hide();
                }

                if (!(ownername == managername) && !(empuppercode == usercode) && !opt) {
                    //console.log('66666666666')
                    row.hidden = true;
                } else {

                    //console.log('77777777777777')
                }
                if (!(empuppercode == usercode)) {
                    console.log('8888888888')
                    checkComplete();
                } else {
                    //$(".kpicompleted").show(); //show card status
                    //console.log('9999999')
                }
            }


            $('.form-control').selectpicker('refresh');
        });

        function getValueUnDisabled() {
            let elemActionmonthly = document.getElementsByName("actionmonthly");
            let elemActiontitle = document.getElementsByName("actiontitle");
            let elemActiontitleresult = document.getElementsByName("actiontitleresult");
            let elemActionrateowner = document.getElementsByName("actionrateowner");
            let elemActionratehead = document.getElementsByName("actionratehead");
            let elemActionFeedback = document.getElementsByName("actionfeedback");
            console.log(elemActionmonthly[1]);
            //console.log(elemActiontitleresult);
            //console.log(elemActionrateowner);


            var params = [];
            for (let i = 0; i < elemActionmonthly.length; i++) {
                params.push({
                    "actionid": elemActionmonthly[i].id.split("_")[1],
                    "actiontitle": elemActiontitle[i].value,
                    "actionmonthlyid": elemActionmonthly[i].id,
                    "actionmonthly": elemActionmonthly[i].value,
                    "actiontitleresultid": elemActiontitleresult[i].id,
                    "actiontitleresult": elemActiontitleresult[i].value,
                    "actionrateownerid": elemActionrateowner[i].id,
                    "actionrateowner": elemActionrateowner[i].value,
                    "actionrateheadid": elemActionratehead[i].id,
                    "actionratehead": elemActionratehead[i].value,
                    "actionfeedbackid": elemActionFeedback[i].id,
                    "actionfeedback": elemActionFeedback[i].value
                });

                let row = elemActionmonthly[i].parentElement.parentElement.parentElement;
                let datenow = row.getAttribute("data-datenow");
                let editstart = row.getAttribute("data-editstart");
                let editend = row.getAttribute("data-editend");
                let approvalstart = row.getAttribute("data-approvalstart");
                let approvalend = row.getAttribute("data-approvalend");

                const [day0, month0, year0] = datenow.split('/');
                const [day1, month1, year1] = editstart.split('/');
                const [day2, month2, year2] = editend.split('/');
                const [day3, month3, year3] = approvalstart.split('/');
                const [day4, month4, year4] = approvalend.split('/');


                const date = new Date(+year0, month0 - 1, +day0);
                const estart = new Date(+year1, month1 - 1, +day1);
                const eend = new Date(+year2, month2 - 1, +day2);
                const astart = new Date(+year3, month3 - 1, +day3);
                const aend = new Date(+year4, month4 - 1, +day4);

                console.log(date);
                //console.log(estart);
                //console.log(eend);
                //console.log(astart);
                //console.log(aend);

                if (!(date >= estart && date <= eend)) {

                    console.log('in');
                    params.pop()
                }
            }

            params.sort((firstItem, secondItem) => firstItem.actionrateowner - secondItem.actionrateowner);
            return params;
        }

        function getValue() {
            let elemActionmonthly = document.getElementsByName("actionmonthly");
            let elemActiontitle = document.getElementsByName("actiontitle");
            let elemActiontitleresult = document.getElementsByName("actiontitleresult");
            let elemActionrateowner = document.getElementsByName("actionrateowner");
            let elemActionratehead = document.getElementsByName("actionratehead");
            let elemActionFeedback = document.getElementsByName("actionfeedback");
            console.log(elemActionmonthly[1]);
            //console.log(elemActiontitleresult);
            //console.log(elemActionrateowner);


            var params = [];
            for (let i = 0; i < elemActionmonthly.length; i++) {
                params.push({
                    "actionid": elemActionmonthly[i].id.split("_")[1],
                    "actiontitle": elemActiontitle[i].value,
                    "actionmonthlyid": elemActionmonthly[i].id,
                    "actionmonthly": elemActionmonthly[i].value,
                    "actiontitleresultid": elemActiontitleresult[i].id,
                    "actiontitleresult": elemActiontitleresult[i].value,
                    "actionrateownerid": elemActionrateowner[i].id,
                    "actionrateowner": elemActionrateowner[i].value,
                    "actionrateheadid": elemActionratehead[i].id,
                    "actionratehead": elemActionratehead[i].value,
                    "actionfeedbackid": elemActionFeedback[i].id,
                    "actionfeedback": elemActionFeedback[i].value
                });

                let row = elemActionmonthly[i].parentElement.parentElement.parentElement;
                let datenow = row.getAttribute("data-datenow");
                let editstart = row.getAttribute("data-editstart");
                let editend = row.getAttribute("data-editend");
                let approvalstart = row.getAttribute("data-approvalstart");
                let approvalend = row.getAttribute("data-approvalend");

                const [day0, month0, year0] = datenow.split('/');
                const [day1, month1, year1] = editstart.split('/');
                const [day2, month2, year2] = editend.split('/');
                const [day3, month3, year3] = approvalstart.split('/');
                const [day4, month4, year4] = approvalend.split('/');


                const date = new Date(+year0, month0 - 1, +day0);
                const estart = new Date(+year1, month1 - 1, +day1);
                const eend = new Date(+year2, month2 - 1, +day2);
                const astart = new Date(+year3, month3 - 1, +day3);
                const aend = new Date(+year4, month4 - 1, +day4);

                console.log(date);
                //console.log(estart);
                //console.log(eend);
                //console.log(astart);
                //console.log(aend);

                //if (!(date >= estart && date <= eend)) {

                //    console.log('in');
                //    params.pop()
                //}
            }
            params.sort((firstItem, secondItem) => firstItem.actionrateowner - secondItem.actionrateowner);
            return params;
        }

        function update() {


            let text = "คุณต้องการบันทึกรายการหรือไม่ ?";
            if (confirm(text)) {
                let param = getValueUnDisabled();
                const params = JSON.stringify(param);
                //console.log(params);

                removeElem("confirm_value");
                let confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                confirm_value.value = params;
                document.forms[0].appendChild(confirm_value);
                return true;
            } else {
                event.preventDefault();
                event.stopPropagation();

                return false;
            }
        }

        function updateOP() {

            let text = "คุณต้องการบันทึกรายการหรือไม่ ?";
            if (confirm(text)) {

                let param = getValue();
                const params = JSON.stringify(param);
                //console.log(params);
                removeElem("confirm_value");
                let confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                confirm_value.value = params;
                document.forms[0].appendChild(confirm_value);
                return true;
            } else {
                event.preventDefault();
                event.stopPropagation();
                return false;
            }
        }
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
        function selectElement(id, valueToSelect) {
            let element = document.getElementById(id);
            element.value = valueToSelect;
        }
        function btnEditOwnerActionplan(elem) {
            const parent = elem.parentElement
            const tr = parent.parentElement

            let ownername = tr.getAttribute("data-ownername");
            let apid = tr.id.split("_")[1];
            //selectElement()
            const usernamelist = '<%= cboUserName.ClientID%>';
            selectElement(usernamelist, ownername);
            $('#<%= hiddenAdvancedetailid.ClientID%>').val(apid);


            $('.form-control').selectpicker('refresh');
            $('#exampleModal').modal('show');

        }
        function btnEditOwnerBranch(elem) {
            const parent = elem.parentElement
            const tr = parent.parentElement

            let ownerbranch = tr.getAttribute("data-ownerbranch");
            let apid = tr.id.split("_")[1];
            //selectElement()
            const usernamelist = '<%= cboBranch.ClientID%>';
            selectElement(usernamelist, ownerbranch);
            $('#<%= hiddenbranchid.ClientID%>').val(apid);


            $('.form-control').selectpicker('refresh');
            $('#exampleModalBranch').modal('show');

        }
        function btnEditplan(elem) {
            const parent = elem.parentElement
            const tr = parent.parentElement

            let ownername = tr.getAttribute("data-ownername");
            let title = tr.getAttribute("data-title");
            let apid = tr.id.split("_")[1];

            //console.log(title)
            //console.log(ownername)
            //console.log(apid)

            $('#<%= txtPlan.ClientID%>').val(title);

            $('#<%= hiddenActionplanid.ClientID%>').val(apid);


            $('#exampleModalPlan').modal('show');

        }
        function change(elem) {
            elem.firstElementChild.setAttribute("data-content", "ยังไม่ได้บันทึก");
            showUnsave()
            $('.footer__page').show();
        }

        function showUnsave() {
            let elements = document.getElementsByClassName("btnUpdate");
            //console.log(elements)
            for (var i = 0; i < elements.length; i++) {
                elements[i].classList.add("footer__btn");
                $(elements[i]).before("<span class=\"footer__btn\"></span>");

            }
        }
        function checkComplete() {

            if ($('#<%= chkKpiComplete.ClientID%>').is(":checked")) {
                $(".kpicompleted").show();

                $('table a,.btnupdatekpi').hide();
                $('.badge-blue').removeClass("border__solid");
                $('.row__ap').css("background-color", "#f2f3f4");
                $('.row__ap textarea').attr('readonly', true);
                $('.form-control ').attr('disabled', true);

            } else {
                $(".kpicompleted").hide();
                //alert('ss')
            }

        }
        $('#<%= chkKpiComplete.ClientID%>').on('change', function () {
            //checkComplete(); //show unsave
            showUnsave()
            $('.footer__page').show();
        });
    </script>
</asp:Content>
