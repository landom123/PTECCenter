<%@ Page Title="KPIsOverview" Language="vb" AutoEventWireup="true" MasterPageFile="~/site.Master" CodeBehind="KPIsRequest.aspx.vb" Inherits="PTECCENTER.KPIsRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg">
        <div id="wrapper">

            <!-- #include virtual ="/include/menu.inc" -->
            <!-- add side menu -->
            <div id="content-wrapper">

                <div class="container">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col">
                                    <div class="row">
                                        <div class="col mb-3">
                                            <asp:DropDownList ID="cboPeriod" class="form-control" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col">
                                            <div class="media">
                                                <img src="<%=Page.ResolveUrl("~/icon/Logo_pure.png")%>" class="align-self-center mr-3" alt="..." width="150">
                                                <div class="media-body">
                                                    <div class="row">
                                                        <div class="col ">
                                                            <h5>
                                                                <asp:Label ID="Label2" CssClass="font-weight-bold" runat="server" Text="Thanapol Duruangram" /></h5>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col mb-1">
                                                            <asp:Label ID="preFixPosition" runat="server" Text="ตำแหน่ง : " />
                                                            <asp:Label ID="txtPosition" runat="server" Text="เจ้าหน้าที่" />
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col mb-1">

                                                            <asp:Label ID="preFixDep" runat="server" Text="ฝ่าย : " />
                                                            <asp:Label ID="txtDep" runat="server" Text="สำนักเทคโนโลยีสารสนเทศ" />
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col mb-1">
                                                            <asp:Label ID="preFixSec" runat="server" Text="แผนก : " />
                                                            <asp:Label ID="txtSec" runat="server" Text="-" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col align-self-end text-right">
                                            <a href="#" title="Ratio" data-toggle="modal" data-target="#modalRatio" data-backdrop="static" data-keyboard="false" data-whatever="new">
                                                <h2>10 / 0 / 0 / 50 / 40 </h2>
                                            </a>
                                            <%--competency,individual,section,department,corporate--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col">

                                    <nav>
                                        <div class="nav nav-tabs" id="nav-tab" role="tablist">
                                            <% For i = 0 To detailtable.Rows.Count - 1 %>
                                                <a class="nav-item nav-link active" id="nav-<%= detailtable.Rows(i).Item("CategoryName").ToString() %>-tab" data-toggle="tab" href="#nav-<%= detailtable.Rows(i).Item("CategoryName").ToString() %>" role="tab" aria-controls="nav-<%= detailtable.Rows(i).Item("CategoryName").ToString() %>" aria-selected="true"><%= detailtable.Rows(i).Item("CategoryName").ToString() %></a>
                                            <% Next i %>
                                        </div>
                                    </nav>
                                    <div class="tab-content p-4" id="nav-tabContent">
                                        <div class="tab-pane fade show active" id="nav-competency" role="tabpanel" aria-labelledby="nav-competency-tab">
                                            <div class="row">
                                                <div class="col mb-3 h5">
                                                    <asp:Label ID="preFixCompetency" runat="server" Text="Structure Weight : " />
                                                    <asp:Label ID="txtCompetencyPercen" runat="server" Text="10.00 %" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col">
                                                    <table class="table table-sm">
                                                        <thead>
                                                            <tr>
                                                                <td>Code</td>
                                                                <td>Title</td>
                                                                <td>Frequency</td>
                                                                <td>Weight</td>
                                                                <td>Action</td>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td>
                                                                    <button type="button" class="close" aria-label="Close">
                                                                        <span aria-hidden="true">&times;</span>
                                                                    </button>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="nav-individual" role="tabpanel" aria-labelledby="nav-individual-tab">
                                            <div class="row">
                                                <div class="col mb-3 h5">
                                                    <asp:Label ID="Label1" runat="server" Text="Structure Weight : " />
                                                    <asp:Label ID="Label3" runat="server" Text="0.00 %" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col">
                                                    <table class="table table-sm">
                                                        <thead>
                                                            <tr>
                                                                <td>Code</td>
                                                                <td>Title</td>
                                                                <td>Frequency</td>
                                                                <td>Weight</td>
                                                                <td>Action</td>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td>
                                                                    <button type="button" class="close" aria-label="Close">
                                                                        <span aria-hidden="true">&times;</span>
                                                                    </button>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="nav-section" role="tabpanel" aria-labelledby="nav-section-tab">
                                            <div class="row">
                                                <div class="col mb-3 h5">
                                                    <asp:Label ID="Label4" runat="server" Text="Structure Weight : " />
                                                    <asp:Label ID="Label5" runat="server" Text="0.00 %" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col">
                                                    <table class="table table-sm">
                                                        <thead>
                                                            <tr>
                                                                <td>Code</td>
                                                                <td>Title</td>
                                                                <td>Frequency</td>
                                                                <td>Weight</td>
                                                                <td>Action</td>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td>
                                                                    <button type="button" class="close" aria-label="Close">
                                                                        <span aria-hidden="true">&times;</span>
                                                                    </button>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="nav-department" role="tabpanel" aria-labelledby="nav-department-tab">
                                            <div class="row">
                                                <div class="col mb-3 h5">
                                                    <asp:Label ID="Label6" runat="server" Text="Structure Weight : " />
                                                    <asp:Label ID="Label7" runat="server" Text="50.00 %" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col">
                                                    <table class="table table-sm">
                                                        <thead>
                                                            <tr>
                                                                <td>Code</td>
                                                                <td>Title</td>
                                                                <td>Frequency</td>
                                                                <td>Weight</td>
                                                                <td>Action</td>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td>
                                                                    <button type="button" class="close" aria-label="Close">
                                                                        <span aria-hidden="true">&times;</span>
                                                                    </button>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="nav-corporate" role="tabpanel" aria-labelledby="nav-corporate-tab">
                                            <div class="row">
                                                <div class="col mb-3 h5">
                                                    <asp:Label ID="Label8" runat="server" Text="Structure Weight : " />
                                                    <asp:Label ID="Label9" runat="server" Text="40.00 %" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col">
                                                    <table class="table table-sm">
                                                        <thead>
                                                            <tr>
                                                                <td>Code</td>
                                                                <td>Title</td>
                                                                <td>Frequency</td>
                                                                <td>Weight</td>
                                                                <td>Action</td>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td>
                                                                    <button type="button" class="close" aria-label="Close">
                                                                        <span aria-hidden="true">&times;</span>
                                                                    </button>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
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
    <div class="modal fade" id="modalRatio" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel_report" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel_report">อัตราส่วน</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <button type="button" class="btn btn-primary w-100" onclick="urlToClipboard()">Get URL</button>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary noEnterSubmit" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <%--<script src="<%=Page.ResolveUrl("~/js/Sortable.js")%>"></script>--%>
    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>
    <script>
        $(document).ready(function () {
            console.log(document)
            $('.form-control').selectpicker({
                noneSelectedText: '-',
                liveSearch: true,
                maxOptions: 1
            });
            $('.form-control').selectpicker('refresh');
        });
    </script>
</asp:Content>
