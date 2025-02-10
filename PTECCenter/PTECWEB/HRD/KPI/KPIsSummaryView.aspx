<%@ Page Title="KPIsSummaryView" Language="vb" AutoEventWireup="true" MasterPageFile="~/site.Master" CodeBehind="KPIsSummaryView.aspx.vb" Inherits="PTECCENTER.KPIsSummaryView" %>

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
        .text-gray {
            color: #c0c0c0;
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
                        <div class="col  align-self-center text-success font-weight-bold">
                            (ผลประเมินสรุปของหัวหน้างาน)
                        </div>

                        <div class="col-auto text-right align-self-center">
                            <a href="KPIsSummaryList.aspx" class="btn btn-sm btn-danger ">
                                <i class="fa fa-tasks" aria-hidden="true"></i></a>
                        </div>

                    </div>
                    <hr />
                    <div class="alert alert-danger alert-dismissible fade show" role="alert" id="alNotHasDataApprover" runat="server">
                        <strong>ข้อมูลยังไม่ครบถ้วน !</strong> คะแนนของฝั่งหัวหน้างาน ยังไม่ถูกประเมิน
                      <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                          <span aria-hidden="true">&times;</span>
                      </button>
                    </div>
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
                                            <u>
                                                <asp:Label ID="txtApprovalBy" CssClass="text-muted" runat="server" Text="" /></u>
                                        </div>
                                    </div>
                                    <div class="ratio__main  mb-5" id="cardRatio" runat="server">
                                        <div class="row">
                                            <div class="col font-weight-bold">
                                                <span>สรุปผลการประเมิน (ผลประเมินของหัวหน้างาน)
                                                </span>
                                            </div>
                                            <div class="col-auto text-right align-self-center ">
                                                <a href="#" class="text-gray font-italic dynamic-link" title="ดูผลคะแนนดิบ"  target="_blank">*</a>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col mb-3">
                                                <span class="text-muted">คำนวน Ratio ตามตำแหน่ง (Corporate, Department, Section, Individual, Competency)
                                                </span>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col overflow-x-auto">
                                                <div class="table-responsive">
                                                    <asp:Table ID="tbRatio" runat="server" BorderWidth="1" CellPadding="5" CellSpacing="0">
                                                    </asp:Table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="project__main  mb-5" id="cardProject" runat="server">
                                        <div class="row">
                                            <div class="col font-weight-bold">
                                                <span>สรุปผลงาน หรือ โครงการ (ผ่านการ Approve จากหัวหน้างาน)
                                                </span>
                                            </div>
                                            
                                            <div class="col-auto text-right align-self-center ">
                                                <a href="#" class="text-gray font-italic dynamic-link" title="ดูผลคะแนนดิบ"  target="_blank">*</a>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col">
                                                <div class="mx-3 bg-light p-2 text-muted font-italic">
                                                    <asp:Label ID="txtProject" runat="server" Text="-" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="competency__main  mb-5" id="cardcompetency" runat="server">
                                        <div class="row">
                                            <div class="col font-weight-bold">
                                                <span>สรุปหัวข้อการพัฒนา (Training Need)
                                                </span>
                                            </div>
                                            
                                            <div class="col-auto text-right align-self-center ">
                                                <a href="#" class="text-gray font-italic dynamic-link" title="ดูผลคะแนนดิบ"  target="_blank">*</a>
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
                                            
                                            <div class="col-auto text-right align-self-center ">
                                                <a href="#" class="text-gray font-italic dynamic-link" title="ดูผลคะแนนดิบ"  target="_blank">*</a>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col overflow-x-auto">
                                                <div class="table-responsive">
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
                                            
                                            <div class="col-auto text-right align-self-center ">
                                                <a href="#" class="text-gray font-italic dynamic-link" title="ดูผลคะแนนดิบ"  target="_blank">*</a>
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
                                            
                                            <div class="col-auto text-right align-self-center ">
                                                <a href="#" class="text-gray font-italic dynamic-link" title="ดูผลคะแนนดิบ"  target="_blank">*</a>
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
                                            
                                            <div class="col-auto text-right align-self-center ">
                                                <a href="#" class="text-gray font-italic dynamic-link" title="ดูผลคะแนนดิบ"  target="_blank">*</a>
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

                                    <div class="feedbackApprover__main  mb-5" id="cardFeedback" runat="server">
                                        <div class="row">
                                            <div class="col font-weight-bold">
                                                <span>Feedback (ของหัวหน้างาน)
                                                </span>
                                            </div>
                                            
                                            <div class="col-auto text-right align-self-center ">
                                                <a href="#" class="text-gray font-italic dynamic-link" title="ดูผลคะแนนดิบ"  target="_blank">*</a>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col">
                                                <div class="mx-3 bg-light p-2 text-muted font-italic">
                                                    <asp:Literal ID="txtFeedbackApprover" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="talent__main  mb-5" id="cardTalent" runat="server">
                                        <div class="row">
                                            <div class="col font-weight-bold">
                                                <span>สรุปการประเมินสักยภาพด้านการเติบโต (Career path)
                                                </span>
                                            </div>
                                            
                                            <div class="col-auto text-right align-self-center ">
                                                <a href="#" class="text-gray font-italic dynamic-link" title="ดูผลคะแนนดิบ"  target="_blank">*</a>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col">
                                                <div class="mx-3 bg-light p-2 text-muted font-italic">
                                                    <asp:Label ID="txtTalent" runat="server" Text="-" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="<%=Page.ResolveUrl("~/default.aspx")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/js/NonPO.js")%>"></script>
    <script>

        $(document).ready(function () {
            $('.form-control').selectpicker({
                noneSelectedText: '-',
                liveSearch: true,
                maxOptions: 1
            });
            $('.form-control').selectpicker('refresh');


            $('[data-toggle="tooltip"]').tooltip();

            const params = new URLSearchParams(window.location.search);
            const f = params.get('f') || ''; // ค่าเริ่มต้นเป็นค่าว่างหากไม่มี 'f'
            const ow = params.get('ow') || ''; // ค่าเริ่มต้นเป็นค่าว่างหากไม่มี 'ow'

            const newUrl = `KPIsSummary2Person.aspx?f=${f}&ow=${ow}`;

            const links = document.querySelectorAll('a.dynamic-link');

            links.forEach(link => {
                link.setAttribute('href', newUrl);
            });
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
