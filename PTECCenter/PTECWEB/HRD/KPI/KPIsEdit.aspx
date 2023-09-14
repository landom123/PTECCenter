<%@ Page Title="KPIsEdit" Language="vb" AutoEventWireup="true" MasterPageFile="~/site.Master" CodeBehind="KPIsEdit.aspx.vb" Inherits="PTECCENTER.KPIsEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
    <link href="<%=Page.ResolveUrl("~/css/card_comment.css")%>" rel="stylesheet">
    <style>
         .content-wrapper {
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
            
            max-width: 230px;
        }
        /*.approval {
            display:none;
        }*/
        .form-control:disabled, .form-control[readonly], .btn-light.disabled {
            color: #acacac;
            background-color: #dddddd;
            opacity: 1;
        }
        tr .form-control,tr .filter-option-inner-inner {
            font-size: .75rem;
        }
        .badge-blue {
            font-size: .65rem;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg">
        <div id="wrapper">

            <!-- #include virtual ="/include/menu.inc" -->
            <!-- add side menu -->
            <div id="content-wrapper">
                <div class="px-5">
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
                        </div>


                        <div class="row">
                            <div class="col table-responsive-xl">

                                <table class="table table-sm shadow-sm">
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
                                        <%--begin item--%>
                                        <%  Dim temp As String = "" %>
                                        <%  Dim cnt_child As Integer = 0 %>
                                        <% If Not Request.QueryString("Kpi_Code") Is Nothing And AllKpi IsNot Nothing Then%>
                                        <% For i = 0 To AllKpi.Tables(0).Rows.Count - 1 %>
                                        <% If not AllKpi.Tables(0).Rows(i).Item("Kpi_Code").ToString = temp Then %>

                                        <tr class=" text-center">
                                            <td><%= AllKpi.Tables(0).Rows(i).Item("CategoryName").ToString %></td>
                                            <td class="text-left"><%= AllKpi.Tables(0).Rows(i).Item("Title").ToString %></td>
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
                                                <asp:Button ID="btnUpdateKPI" class="btn btn-sm  btn-warning" runat="server" Text="UpdateKPI" />
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
                                                                <th>เจ้าของงาน</th>
                                                                <th>แผนงาน / เป้าหมาย</th>
                                                                <th>ผลตามแผน</th>
                                                                <th>ผลการปฏิบัติงาน</th>
                                                                <th>พนักงานประเมิน</th>
                                                                <th class="approval">หัวหน้าประเมิน</th>
                                                                <th class="approval">รายละเอียดการประเมิน</th>
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
                                                            <tr class="text-center" id="actionid_<%= AllKpi.Tables(1).Rows(j).Item("actionid").ToString %>" data-datenow="<%= AllKpi.Tables(0).Rows(i).Item("datenow").ToString %>" data-opt="<%= AllKpi.Tables(0).Rows(i).Item("operatortype").ToString %>"
                                                                data-empuppercode="<%= AllKpi.Tables(1).Rows(j).Item("actionempuppercode").ToString %>"
                                                                data-ownercode="<%= AllKpi.Tables(1).Rows(j).Item("actionownercode").ToString %>"
                                                                data-ownername="<%= AllKpi.Tables(1).Rows(j).Item("NameOwner").ToString %>"
                                                                data-usercode="<%= Session("usercode").ToString %>"
                                                                data-username="<%= Session("username").ToString %>"
                                                                data-managername="<%= managername %>"
                                                                data-editstart="<%= AllKpi.Tables(1).Rows(j).Item("actionedit_begindate").ToString %>"
                                                                data-editend="<%= AllKpi.Tables(1).Rows(j).Item("actionedit_enddate").ToString %>"
                                                                data-approvalstart="<%= AllKpi.Tables(1).Rows(j).Item("actionapproval_begindate").ToString %>"
                                                                data-approvalend="<%= AllKpi.Tables(1).Rows(j).Item("actionapproval_enddate").ToString %>">
                                                                <td><span class="badge badge-blue"><%= AllKpi.Tables(1).Rows(j).Item("actionmonth").ToString %></span>
                                                                <td><span><%= AllKpi.Tables(1).Rows(j).Item("NameOwner").ToString %></span>
                                                                </td>
                                                                <td class="text-left">
                                                                    <% If (String.IsNullOrEmpty(AllKpi.Tables(0).Rows(0).Item("operatortype").ToString)) Then%>
                                                                        <span name="actiontitle" ><%= AllKpi.Tables(1).Rows(j).Item("actiontitle").ToString %></span>
                                                                    <% ElseIf AllKpi.Tables(0).Rows(0).Item("operatortype").ToString = "A" Then %>
                                                                        <textarea rows="2" cols="40" class="form-control" name="actiontitle" id="actiontitle_<%= AllKpi.Tables(1).Rows(j).Item("actionid").ToString %>"><%= AllKpi.Tables(1).Rows(j).Item("actiontitle").ToString %></textarea>
                                                                    <% End If %>
                                                                </td>


                                                                <td>
                                                                    <select class="form-control" name="actionmonthly" id="actionmonthly_<%= AllKpi.Tables(1).Rows(j).Item("actionid").ToString %>">
                                                                        <option value="0">-</option>
                                                                        <option value="2" <% If AllKpi.Tables(1).Rows(j).Item("actionmonthly").ToString = "3" Then %>selected="selected" <% End if %>>เร็วกว่าแผน</option>
                                                                        <option value="1" <% If AllKpi.Tables(1).Rows(j).Item("actionmonthly").ToString = "1" Then %>selected="selected" <% End if %>>ตามแผน</option>
                                                                        <option value="2" <% If AllKpi.Tables(1).Rows(j).Item("actionmonthly").ToString = "2" Then %>selected="selected" <% End if %>>ช้ากว่าแผน</option>
                                                                    </select>
                                                                    <%--<%= AllKpi.Tables(1).Rows(j).Item("actionmonthly").ToString %></td>--%>


                                                                <td class="text-left">
                                                                    <textarea class="form-control" rows="2" cols="40" name="actiontitleresult" autocomplete="off" id="actiontitleresult_<%= AllKpi.Tables(1).Rows(j).Item("actionid").ToString %>" /><%= AllKpi.Tables(1).Rows(j).Item("actiontitleresult").ToString %></textarea>
                                                                    <%--<%= AllKpi.Tables(1).Rows(j).Item("actiontitleresult").ToString %>--%>

                                                                </td>
                                                                <td>
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
                                                                <td class="approval">
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
                                                                    <span class="text-muted" data-toggle="tooltip" data-placement="top" title="แก้ไข : <%= AllKpi.Tables(1).Rows(j).Item("actionedit_begindate").ToString %> - <%= AllKpi.Tables(1).Rows(j).Item("actionedit_enddate").ToString %>
                                                                        อนุมัติ : <%= AllKpi.Tables(1).Rows(j).Item("actionapproval_begindate").ToString %> - <%= AllKpi.Tables(1).Rows(j).Item("actionapproval_enddate").ToString %>"><i class="fas fa-info-circle"></i></span>
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
                    <div class="row">
                        <div class="col">
                                <input type="button" value="Update" onclick="update();" id="btnUpdate" runat="server" class="btn btn-sm  btn-warning" />
                                <input type="button" value="UpdateOP" onclick="updateOP();" id="btnUpdateOP" runat="server" class="btn btn-sm  btn-warning" />  
                        </div>

                    </div>

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

            $('[data-toggle="tooltip"]').tooltip()

            let elemActionmonthly = document.getElementsByName("actionmonthly");
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
                //--------------------------------------------------

                const empuppercode = row.getAttribute("data-empuppercode");
                const ownercode = row.getAttribute("data-ownercode");
                const ownername = row.getAttribute("data-ownername");
                const opt = row.getAttribute("data-opt");
                const usercode = row.getAttribute("data-usercode");
                const username = row.getAttribute("data-username");
                const managername = row.getAttribute("data-managername") || username;
                

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

                //alert(opt);
                console.log(ownername.indexOf(managername || usercode));
                console.log(ownername);
                console.log(managername);
                console.log(usercode);
                //console.log(empuppercode);
                //console.log(ownercode);
                //console.log(ownercode.indexOf(usercode));

                if ((ownername.indexOf(managername) > -1) && !opt) { // เจ้าของ KPI
                    console.log('เจ้าของ KPI');

                    if (!(date >= estart && date <= eend)) {
                        console.log(`#${row.id}`);
                        row.style.backgroundColor = "#f2f3f4";
                        elemActionmonthly[i].disabled = true;
                        elemActiontitleresult[i].disabled = true;
                        elemActionrateowner[i].disabled = true;
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

                    if (!(date > astart && date < aend)) {
                        elemActionratehead[i].disabled = true;
                        elemActionFeedback[i].disabled = true;
                    }
                } else if (!opt) {
                    console.log('555555555555555')
                    elemActiontitle[i].disabled = true;
                    elemActionmonthly[i].disabled = true;
                    elemActiontitleresult[i].disabled = true;
                    elemActionrateowner[i].disabled = true;

                    elemActionratehead[i].disabled = true;
                    elemActionFeedback[i].disabled = true;
                    //$('#btnUpdate').hide();
                }

                if (!(ownername == managername) && !(empuppercode==usercode) && !opt) {
                    console.log('66666666666')
                    row.hidden=true;
                } else {

                    console.log('77777777777777')
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

            return params;
        }

        function update() {
            let param = getValueUnDisabled();
            const params = JSON.stringify(param);
            console.log(params);
            let confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            confirm_value.value = params;
            document.forms[0].appendChild(confirm_value);
            return true;
        }

        function updateOP() {
            let param = getValue();
            const params = JSON.stringify(param);
            console.log(params);
            let confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            confirm_value.value = params;
            document.forms[0].appendChild(confirm_value);
            return true;
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
    </script>
</asp:Content>
