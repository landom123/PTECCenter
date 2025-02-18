<%@ Page Title="KPIsSummaryView" Language="vb" AutoEventWireup="true" MasterPageFile="~/site.Master" CodeBehind="KPIsSummarySelf2.aspx.vb" Inherits="PTECCENTER.KPIsSummarySelf2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
    <link href="<%=Page.ResolveUrl("~/css/card_comment.css")%>" rel="stylesheet">
    <style>
        .overflow-x-auto {
            overflow-x: auto;
        }

        .prewrap {
            white-space: pre-wrap;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg">
        <div id="wrapper" class="h-100">

            <!-- #include virtual ="/include/menu.inc" -->
            <!-- add side menu -->
            <div id="content-wrapper">

                <div class="container">
                    <div class="row">
                        <div class="col  align-self-center text-warning  font-weight-bold">
                            (ผลประเมินของพนักงาน)
                        </div>

                        <div class="col-auto text-right align-self-center">
                            <a href="KPIsSummaryList.aspx" class="btn btn-sm btn-danger ">
                                <i class="fa fa-tasks" aria-hidden="true"></i></a>
                        </div>

                    </div>
                    <hr />
                    <div class="row">
                        <div class="col">
                            <div class="card shadow-sm">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col">
                                            <div class="media">
                                                <div class="media-body">
                                                    <div class="row">
                                                        <div class="col ">
                                                            <h5>
                                                                <asp:Label ID="txtOwnername" CssClass="font-weight-bold" runat="server" Text="" />
                                                            </h5>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col mb-1">
                                                            <asp:Label ID="preFixPosition" runat="server" Text="ตำแหน่ง : " />
                                                            <asp:Label ID="txtPosition" runat="server" Text="" />
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col mb-1">

                                                            <asp:Label ID="preFixDep" runat="server" Text="ฝ่าย : " />
                                                            <asp:Label ID="txtDep" runat="server" Text="" />
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col mb-1">
                                                            <asp:Label ID="preFixSec" runat="server" Text="แผนก : " />
                                                            <asp:Label ID="txtSec" runat="server" Text="" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row mb-3">
                                        <div class="col">
                                            <asp:Label ID="txtFormTitle" CssClass="h5 font-weight-bold text-info" runat="server" Text="" />
                                        </div>
                                    </div>
                                    <div class="ratio__main  mb-5" id="cardRatio" runat="server">
                                        <div class="row">
                                            <div class="col font-weight-bold">
                                                <span>สรุปผลการประเมิน KPIs
                                                </span>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col mb-3">
                                                <span class="text-muted"></span>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col overflow-x-auto">
                                                <div class="table-responsive ">
                                                    <asp:Table ID="tbRatio" runat="server" BorderWidth="1" CellPadding="5" CellSpacing="0">
                                                    </asp:Table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="project__main  mb-5" id="cardProject" runat="server">
                                        <div class="row">
                                            <div class="col font-weight-bold">
                                                <span>สรุปผลงาน หรือ โครงการ
                                                </span>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col">
                                                <asp:Table ID="tbProject" runat="server" BorderWidth="1" CellPadding="5" CellSpacing="0">
                                                </asp:Table>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="competency__main  mb-5" id="cardcompetency" runat="server">
                                        <div class="row">
                                            <div class="col font-weight-bold">
                                                <span>สรุปหัวข้อการพัฒนา (Training Need)
                                                </span>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col">
                                                <div class="mx-3 bg-light p-2 text-muted font-italic">
                                                    <asp:Label ID="txtTraining" runat="server" Text="-" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="career__main  mb-5" id="cardCareer" runat="server">
                                        <div class="row">
                                            <div class="col font-weight-bold">
                                                <span>สรุปการประเมินศักยภาพในการปฏิบัติงาน (Potential)
                                                </span>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col overflow-x-auto">
                                                <div class="table-responsive ">
                                                    <asp:Table ID="tbCareer" runat="server" BorderWidth="1" CellPadding="5" CellSpacing="0">
                                                    </asp:Table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>



                                    <div class="txtFeedbackOwner__main  mb-5" id="Div1" runat="server">
                                        <div class="row">
                                            <div class="col font-weight-bold">
                                                <span>Feedback ประสบการณ์ (ของพนักงาน)
                                                </span>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col">
                                                <div class="mx-3 d-flex flex-column bg-light p-2 text-muted font-italic">
                                                    <asp:Literal ID="txtFeedbackExperience" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col font-weight-bold">
                                                <span>Feedback ความภาคภูมิใจ (ของพนักงาน)
                                                </span>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col">
                                                <div class="mx-3 d-flex flex-column bg-light p-2 text-muted font-italic">
                                                    <asp:Literal ID="txtFeedbackPride" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col font-weight-bold">
                                                <span>Feedback มีความสนใจงานด้านอื่นๆ (ของพนักงาน)
                                                </span>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col">
                                                <div class="mx-3 d-flex flex-column bg-light p-2 text-muted font-italic">
                                                    <asp:Literal ID="txtFeedbackMigrate" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <%--<div class="feedbackApprover__main  mb-5" id="cardFeedback" runat="server">
                                        <div class="row">
                                            <div class="col font-weight-bold">
                                                <span>Feedback (ของหัวหน้างาน)
                                                </span>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col">
                                                <div class="mx-3">
                                                    <asp:Literal ID="txtFeedbackApprover" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <CompositeScript>
            <Scripts>
                <asp:ScriptReference Path="~/datetimepicker/jquery.js" />
                <asp:ScriptReference Path="~/datetimepicker/build/jquery.datetimepicker.full.min.js" />
                <asp:ScriptReference Path="~/js/NonPO.js" />
            </Scripts>
        </CompositeScript>
    </asp:ScriptManager>
    <%--<script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/NonPO.js")%>"></script>--%>
    <script>

        $(document).ready(function () {
            $('.form-control').selectpicker({
                noneSelectedText: '-',
                liveSearch: true,
                maxOptions: 1
            });
            $('.form-control').selectpicker('refresh');


            $('[data-toggle="tooltip"]').tooltip();
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
    </script>
</asp:Content>
