<%@ Page Title="KPIsOverview" Language="vb" AutoEventWireup="true" MasterPageFile="~/site.Master" CodeBehind="KPIsOverview.aspx.vb" Inherits="PTECCENTER.KPIsOverview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
    <link href="<%=Page.ResolveUrl("~/css/card_comment.css")%>" rel="stylesheet">
    <style>
        .container {
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
            background-color: #8d9eb7;
        }

        tr[aria-expanded="false"] > td:last-child:after {
            content: '\f107';
            font-family: 'Font Awesome 5 Free';
            font-weight: bold;
            float: right;
            margin-left: 5px;
        }

        tr[aria-expanded="true"] > td:last-child:after {
            content: "\f106";
            font-family: 'Font Awesome 5 Free';
            font-weight: bold;
            float: right;
            margin-left: 5px;
        }


        .light-grey {
            border-radius: 3px;
            overflow: hidden;
            color: #000 !important;
            background-color: #f1f1f1 !important;
        }

        .green {
            color: #fff !important;
            background-color: #67c487 !important;
        }

        .blue {
            color: #fff !important;
            background-color: #6690c7 !important;
        }

        .pink {
            color: #fff !important;
            background-color: #d6037c !important;
        }

        .yello {
            color: #fff !important;
            background-color: #ffbd3d !important;
        }

        .brown {
            color: #fff !important;
            background-color: #b8a27b !important;
        }

        .text-lightgray {
            color: #b5b5b5 !important;
        }

        table {
            padding: 10px;
        }

        .table__inner > thead {
            font-size: .8rem;
        }

        .table__inner > tbody {
            font-size: .75rem;
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

                    <div class="px-5">
                        <div class="row">
                            <div class="col text-left align-self-center">
                                <%--Performance Update : KPIs & Competency--%>
                            </div>

                            <div class="col-auto text-right align-self-center">
                                <a href="KPIsList.aspx" class="btn btn-sm btn-danger ">
                                    <i class="fa fa-tasks" aria-hidden="true"></i></a>
                            </div>
                        </div>

                        <div class="row align-items-center justify-content-center">
                            <div class="col-lg-auto  text-center mb-3">
                                <img runat="server" id="logo" class="logopure" src="#" alt="logopure" width="500" height="600">
                            </div>
                            <div class="col-lg-auto company mb-3">
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
                        <!------------------------------------------------------------------------>

                        <div class="row align-items-center">
                            <div class="col-md mb-3 text-center">
                                <h3>
                                    <% If Not Request.QueryString("uc") Is Nothing And AllKpi IsNot Nothing Then%>
                                    <%= Request.QueryString("uc").ToString() %>
                                    <% End If%>
                                </h3>
                                <%= nameowner %>
                            </div>
                            <div class="col-md mb-3 text-center">
                                <div class="mx-auto" style="max-width: 320px; max-height: 320px;">
                                    <canvas id="myChart"></canvas>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col mb-3 table-responsive-xl">
                                <div class=" ">
                                    <%--begin item--%>
                                    <%  Dim tempType As String = "" %>
                                    <%  Dim tempowner As String = "" %>
                                    <%  Dim temp As String = "" %>
                                    <%  Dim cnt_child As Integer = 0 %>
                                    <table class="table table-sm table-hover shadow-sm table__header">
                                        <thead class="thead-blue ">
                                            <tr>
                                                <th class="text-center align-middle" rowspan="2"><span class="p-1">ประเภท</span></th>
                                                <th class="text-center align-middle" rowspan="2"><span class="p-1">ผู้ดูแล</span></th>
                                                <th class="text-center align-middle" rowspan="2"><span class="p-1">หัวข้อ KPIs</span></th>
                                                <th class="text-center align-middle" rowspan="2"><span class="p-1">Ratio Company</span></th>
                                                <th class="text-center align-middle" rowspan="2"><span class="p-1">Ratio KPI</span></th>
                                                <th class="text-center align-middle" rowspan="2"><span class="p-1">หน่วย</span></th>
                                                <th class="text-center align-middle" colspan="5"><span class="p-1">ระดับประเมิน</span></th>
                                                <th class="text-center align-middle" rowspan="2"><span class="p-1">% ผลการดำเนินการ</span></th>
                                                <th class="text-center align-middle" rowspan="2"><span class="p-1"></span></th>
                                            </tr>
                                            <tr>
                                                <th class="text-center align-middle"><span class="p-1">5</span></th>
                                                <th class="text-center align-middle"><span class="p-1">4</span></th>
                                                <th class="text-center align-middle"><span class="p-1">3</span></th>
                                                <th class="text-center align-middle"><span class="p-1">2</span></th>
                                                <th class="text-center align-middle"><span class="p-1">1</span></th>
                                            </tr>
                                        </thead>

                                        <tbody>

                                            <% For i = 0 To AllKpi.Tables(0).Rows.Count - 1 %>
                                            <% If not AllKpi.Tables(0).Rows(i).Item("Kpi_Code").ToString = temp And AllKpi.Tables(0).Rows(i).Item("OverviewWeight").ToString <> "0" Then %>


                                            <tr data-toggle="collapse" data-target="#<%= AllKpi.Tables(0).Rows(i).Item("Kpi_Code").ToString %>" class="accordion-toggle text-center" aria-expanded="false">
                                                <td class="text-left"><span class="p-1"><%= AllKpi.Tables(0).Rows(i).Item("CategoryName").ToString %></span></td>
                                                <td><span class="p-1"><%= AllKpi.Tables(0).Rows(i).Item("ownercode").ToString %></span></td>
                                                <td><span class="p-1 text-left"><%= AllKpi.Tables(0).Rows(i).Item("Title").ToString %></span></td>

                                                <!---------------------------------------------------------------------------------------------------------------------------------->
                                                <td>
                                                    <span class="p-1 font-weight-bold text-muted">
                                                        <% If AllKpi.Tables(0).Rows(i).Item("CategoryName").ToString <> tempType Then %>
                                                        <%= AllKpi.Tables(0).Rows(i).Item("OverviewWeight").ToString %>
                                                        <% End if %>
                                                    </span>

                                                </td>

                                                <td class="font-weight-bold text-lightgray"><span class="p-1"><%= AllKpi.Tables(0).Rows(i).Item("Weight").ToString %></span></td>
                                                <td><span class="p-1"><%= AllKpi.Tables(0).Rows(i).Item("Unit").ToString %></span></td>
                                                <td><span class="p-1"><%= AllKpi.Tables(0).Rows(i).Item("Lv5").ToString %></span></td>
                                                <td><span class="p-1"><%= AllKpi.Tables(0).Rows(i).Item("Lv4").ToString %></span></td>
                                                <td><span class="p-1"><%= AllKpi.Tables(0).Rows(i).Item("Lv3").ToString %></span></td>
                                                <td><span class="p-1"><%= AllKpi.Tables(0).Rows(i).Item("Lv2").ToString %></span></td>
                                                <td><span class="p-1"><%= AllKpi.Tables(0).Rows(i).Item("Lv1").ToString %></span></td>
                                                <td>
                                                    <div class="light-grey">
                                                        <div class="<%= AllKpi.Tables(0).Rows(i).Item("color_progress").ToString %> text-center" style="width: <%= AllKpi.Tables(0).Rows(i).Item("percent_progress").ToString %>%"><%= AllKpi.Tables(0).Rows(i).Item("percent_progress").ToString %>%</div>
                                                    </div>
                                                </td>
                                                <td>
                                                    <%-- <button type="button" class="btn btn-sm btn-danger">
                                                    <i class="far fa-bell"></i><span class="badge badge-danger">4</span>
                                                </button>--%>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td colspan="12" class="hiddenRow">
                                                    <div class="accordian-body collapse" id="<%= AllKpi.Tables(0).Rows(i).Item("Kpi_Code").ToString %>">
                                                        <table class="table table-hover table__inner">
                                                            <thead class="thead-light">
                                                                <tr class="info text-center">
                                                                    <th>
                                                                        <span class="p-1">
                                                                            <% If Session("usercode").ToString.IndexOf(AllKpi.Tables(0).Rows(i).Item("ownercode")) > 0 Then%>
                                                                            <a href="KPIsEdit.aspx?Kpi_Code=<%= AllKpi.Tables(0).Rows(i).Item("Kpi_Code").ToString %>" title="EditDetail"><i class="fas fa-edit"></i></a>
                                                                            <% End If%>
                                                                        </span>
                                                                    </th>

                                                                    <th><span class="p-1">เจ้าของ kpi</span></th>
                                                                    <th><span class="p-1">แผนงาน / เป้าหมาย</span></th>
                                                                    <th><span class="p-1">ผลตามแผน</span></th>
                                                                    <th><span class="p-1">ผลการปฏิบัติงาน</span></th>
                                                                    <th><span class="p-1">พนักงานประเมิน</span></th>
                                                                    <th><span class="p-1">หัวหน้าประเมิน</span></th>
                                                                    <th><span class="p-1">Feedback</span></th>
                                                                </tr>
                                                            </thead>

                                                            <tbody>
                                                                <% For j = 0 To AllKpi.Tables(1).Rows.Count - 1 %>
                                                                <% If AllKpi.Tables(0).Rows(i).Item("Kpi_Code").ToString = AllKpi.Tables(1).Rows(j).Item("actionkpi_code").ToString Then %>
                                                                <tr class="text-center">

                                                                    <td>
                                                                        <span class="badge badge-blue">
                                                                            <%= AllKpi.Tables(1).Rows(j).Item("actionmonth").ToString %>
                                                                        </span>
                                                                    </td>
                                                                    <td><span class="p-1"><%= AllKpi.Tables(1).Rows(j).Item("NameOwner").ToString %></span></td>
                                                                    <td class="text-left">
                                                                        <span class="p-1 text-left ">
                                                                            <%= AllKpi.Tables(1).Rows(j).Item("actiontitle").ToString %>
                                                                        </span>

                                                                    </td>
                                                                    <td>
                                                                        <span class="p-1">
                                                                            <% If AllKpi.Tables(1).Rows(j).Item("actionmonthly").ToString = "1" Then %>
                                                                            <span class="badge badge-success">ตามแผน</span>

                                                                            <% else If AllKpi.Tables(1).Rows(j).Item("actionmonthly").ToString = "2" Then %>
                                                                            <span class="badge badge-danger">ช้ากว่าแผน</span>
                                                                            <% else If AllKpi.Tables(1).Rows(j).Item("actionmonthly").ToString = "3" Then %>
                                                                            <span class="badge badge-danger">เร็วกว่าแผน</span>
                                                                            <% End if %>
                                                                        </span>
                                                                    </td>
                                                                    <td class="text-left"><span class="p-1"><%= AllKpi.Tables(1).Rows(j).Item("actiontitleresult").ToString %></span></td>
                                                                    <td><span class="p-1 font-weight-bold <%= If(TypeOf AllKpi.Tables(1).Rows(j).Item("actionrateowner") Is Integer, If((DirectCast(AllKpi.Tables(1).Rows(j).Item("actionrateowner"), Integer) < 3), "text-danger", "text-success"), "") %>"><%= AllKpi.Tables(1).Rows(j).Item("actionrateowner").ToString %></span></td>
                                                                    <td><span class="p-1 font-weight-bold <%= If(TypeOf AllKpi.Tables(1).Rows(j).Item("actionratehead") Is Integer, If((DirectCast(AllKpi.Tables(1).Rows(j).Item("actionratehead"), Integer) < 3), "text-danger", "text-success"), "") %>"><%= AllKpi.Tables(1).Rows(j).Item("actionratehead").ToString %></span></td>
                                                                    <td class="text-left"><span class="p-1"><%= AllKpi.Tables(1).Rows(j).Item("actionfeedback").ToString %></span></td>
                                                                </tr>

                                                                <% End if %>
                                                                <% Next j %>
                                                            </tbody>
                                                        </table>

                                                    </div>
                                                </td>
                                            </tr>
                                            <% End if %>
                                            <% tempType = AllKpi.Tables(0).Rows(i).Item("CategoryName").ToString %>
                                            <% temp = AllKpi.Tables(0).Rows(i).Item("Kpi_Code").ToString %>

                                            <% Next i %>
                                        </tbody>
                                        <tfoot>
                                            <tr class="text-center">
                                                <td colspan="3"></td>
                                                <td class="font-weight-bold text-muted"><%= totalRatio %></td>
                                                <td colspan="8"></td>

                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>

                            </div>
                        </div>






                        <!------------------------------------------------------------------------>


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

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels@2.0.0"></script>

    <script type="text/javascript">
        jQuery('[id$=txtDuedate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08'
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
        $(document).ready(function () {
            $('.form-control').selectpicker({
                liveSearch: true,
                maxOptions: 1
            });


            $('.form-control').selectpicker('refresh');
            let dataChart = []
            <% If Not Request.QueryString("uc") Is Nothing And AllKpi IsNot Nothing Then%>
            <% For j = 0 To AllKpi.Tables(2).Rows.Count - 1 %>
            dataChart.push(<%= AllKpi.Tables(2).Rows(j).Item("Weight").ToString %>);
            <% Next j %>
             <% End If%>

            const newArr = [
                {
                    label: 'Corporate',
                    data: dataChart[0],
                    backgroundColor: '#ffbd3d'
                },
                {
                    label: 'Department',
                    data: dataChart[1],
                    backgroundColor: '#6690c7'
                },
                {
                    label: 'Section',
                    data: dataChart[2],
                    backgroundColor: '#67c487'
                },
                {
                    label: 'Individual',
                    data: dataChart[3],
                    backgroundColor: '#b8a27b'
                },
                {
                    label: 'Competency',
                    data: dataChart[4],
                    backgroundColor: '#d6037c'
                }
            ]

            //---



            console.log(dataChart);
            const ctx = document.getElementById('myChart');
            new Chart(ctx, {
                type: 'doughnut',
                data: {
                    //labels: ['Corporate', 'Department', 'Section', 'Individual', 'Competency'],
                    labels: newArr.filter((fill) => fill.data > 0).map((res) => res.label),
                    datasets: [{

                        label: 'คำนวนเป็น(%)',
                        data: newArr.filter((fill) => fill.data > 0).map((res) => res.data),
                        borderWidth: 3,
                        backgroundColor: newArr.filter((fill) => fill.data > 0).map((res) => res.backgroundColor),
                    }],

                }, plugins: [ChartDataLabels], options: {
                    plugins: {
                        datalabels: {
                            //color: '#343a40',
                            color: '#fff',
                            formatter: function (value) {
                                return Math.round(value) + '%';
                            },
                            font: {
                                weight: 'bold',
                                size: 14,
                            }
                        },
                        legend: {
                            rtl: true,
                            display: true,
                            position: 'right',
                            labels: {
                                usePointStyle: true
                            },
                        }

                    },

                }


            });
        });


        function btnEditDetailClick(ele) {
            console.log(ele);
            event.preventDefault();
            $('#exampleModal').modal('show');

            return false;
        }
    </script>
</asp:Content>
