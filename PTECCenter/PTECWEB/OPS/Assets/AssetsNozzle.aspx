<%@ Page Title="" Language="vb" AutoEventWireup="false" Async="true" MasterPageFile="~/site.Master" CodeBehind="AssetsNozzle.aspx.vb" Inherits="PTECCENTER.AssetsNozzle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
    <style>
        th {
            text-align: center;
        }

        .table td {
            vertical-align: middle;
        }
        .table a[href='#'] {
            display: none;
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
                <div class="container">
                    <div class="row">
                        <div class="col-md-4 mb-3">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">Branch</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboBranch" runat="server" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col">
                            <button type="button" class="btn btn-sm  btn-info noEnterSubmit" style="color: #495057;" title="Export" id="btnExport" runat="server" data-toggle="modal" data-target="#modalExport" data-backdrop="static" data-keyboard="false" data-whatever="new"><i class="fas fa-file-download"></i></button>
                            &nbsp;
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="row bg-white">
                                <div class="col-md-12 col-12 mb-3 ">
                                    <div class="card shadow">
                                        <div class="card-header" style="background-color: cornflowerblue; color: white">
                                            <div class="row justify-content-between">
                                                <div class="col text-left">
                                                    ข้อมูลมือจ่ายประจำสาขา
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <div class="table-responsive">
                                                <table class="table table-hover table-bordered table-sm">
                                                    <thead>
                                                        <tr>
                                                            <th >ลำดับที่</th>
                                                            <th >ตำแหน่ง</th>
                                                            <th >ชนิดน้ำมัน</th>
                                                            <th >เลขที่มาตร</th>
                                                            <th >ยี่ห้อ</th>
                                                            <th>วันที่สิ้นสุด</th>
                                                            <th>ข้อมูลล่าสุดวันที่</th>
                                                            <th></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody class="text-center align-bottom">
                                                        <% If nozzletable IsNot Nothing Then%>
                                                        <% If nozzletable.Rows.Count > 0 Then%>
                                                        <% For j = 0 To nozzletable.Rows.Count - 1 %>
                                                        <tr style="cursor: pointer;"
                                                            <% If Not Session("positionid") = "10" Then%>
                                                            ondblclick="btnEditDetailClick('<%= nozzletable.Rows(j).Item("rownumber").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("nozzle_id").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("nozzle_no").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("brand").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("expirydate").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("producttype").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("positiononassest").ToString() %>'
                                                                        );"
                                                            <% End if %>>
                                                            <td>
                                                                <span><%= nozzletable.Rows(j).Item("rownumber").ToString %></span>
                                                            </td>
                                                            <td>
                                                                <span><%= nozzletable.Rows(j).Item("positionOnAssest").ToString %></span>
                                                            </td>
                                                            <td>
                                                                <span><%= nozzletable.Rows(j).Item("producttype").ToString %></span>
                                                            </td>
                                                            <td>
                                                                <span><%= nozzletable.Rows(j).Item("nozzle_No").ToString %></span>
                                                            </td>
                                                            <td>
                                                                <span><%= nozzletable.Rows(j).Item("brand").ToString %></span>
                                                            </td>
                                                            <td>
                                                                <span><%= nozzletable.Rows(j).Item("expirydate").ToString %></span>
                                                            </td>
                                                            <td>
                                                                <a href="<%= nozzletable.Rows(j).Item("url").ToString %>" target="_blank">รูปภาพ</a>
                                                            </td>
                                                            <td>
                                                                <span><%= nozzletable.Rows(j).Item("updatedate").ToString %></span>
                                                            </td>
                                                            <td>
                                                                <% If Not Session("positionid") = "10" Then%>
                                                                <a title="EditDetail" onclick="btnEditDetailClick('<%= nozzletable.Rows(j).Item("rownumber").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("nozzle_id").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("nozzle_no").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("brand").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("expirydate").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("producttype").ToString() %>'
                                                                        ,'<%= nozzletable.Rows(j).Item("positiononassest").ToString() %>'
                                                                        );"><i class="fas fa-edit color__purple"></i></a>&nbsp;&nbsp;
                                                                
                                                                <% End if %>
                                                            </td>
                                                        </tr>

                                                        <% Next j %>
                                                        <% End if %>
                                                        <% End if %>
                                                    </tbody>
                                                </table>
                                            </div>

                                        </div>
                                        <!-- end card-->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
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
                        <asp:Label ID="Label8" CssClass="form-label" AssociatedControlID="txtbrand" runat="server" Text="ยี่ห้อ" />
                        <asp:TextBox class="form-control" type="input" ID="txtbrand" runat="server" autocomplete="off"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label9" CssClass="form-label" AssociatedControlID="txtproducttype" runat="server" Text="ชนิดน้ำมัน" />
                        <asp:TextBox class="form-control" type="input" ID="txtproducttype" runat="server" autocomplete="off"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label10" CssClass="form-label" AssociatedControlID="txtnozzle_no" runat="server" Text="เลขที่มาตร" />
                        <asp:TextBox class="form-control" type="input" ID="txtnozzle_no" runat="server" autocomplete="off"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label11" CssClass="form-label" AssociatedControlID="txtpositiononassest" runat="server" Text="ตำแหน่ง" />
                        <asp:TextBox class="form-control" type="input" ID="txtpositiononassest" runat="server" autocomplete="off"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label12" CssClass="form-label" AssociatedControlID="txtexpirydate" runat="server" Text="วันที่สิ้นสุด" />
                        <asp:TextBox class="form-control" type="input" ID="txtexpirydate" runat="server" autocomplete="off"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary noEnterSubmit" data-dismiss="modal">Close</button>
                    <asp:Button ID="btnAddDetails" class="btn btn-primary" runat="server" Text="Save" OnClientClick="postBack_addDetail();" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalExport" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel_report" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel_report">ลายละเอียดรายงาน</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group pl-5">
                        <input class="form-check-input chk-img-after" type="checkbox" id="chkNozzle" runat="server">
                        <asp:Label ID="Label1" CssClass="form-check-label" AssociatedControlID="chkNozzle" runat="server" Text="ข้อมูลมือจ่าย" />
                    </div>
                    <div class="form-group pl-5">
                        <input class="form-check-input chk-img-after" type="checkbox" id="chkResultTEST" runat="server">
                        <asp:Label ID="Label2" CssClass="form-check-label" AssociatedControlID="chkResultTEST" runat="server" Text="ประวัติ/ผล การ TEST" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary noEnterSubmit" data-dismiss="modal">Close</button>
                    <asp:Button ID="btnDowload" class="btn btn-primary" runat="server" Text="Dowload" />
                </div>
            </div>
        </div>
    </div>
    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>
    <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>

    <script type="text/javascript">
        jQuery('[id$=txtexpirydate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format: 'd/m/yy'
        });
        $(document).ready(function () {
            $('.form-control').selectpicker({
                liveSearch: true,
                maxOptions: 1
            });
        });
        $("input:checkbox").on('click', function () {
            // in the handler, 'this' refers to the box clicked on
            console.log(this);
            var $box = $(this);
            if ($box.is(":checked")) {
                // the name of the box is retrieved using the .attr() method
                // as it is assumed and expected to be immutable
                var group = "input:checkbox";
                // the checked state of the group/box on the other hand will change
                // and the current value is retrieved using .prop() method
                $(group).prop("checked", false);
                $box.prop("checked", true);
            } else {
                $box.prop("checked", false);
            }
        });
        function btnEditDetailClick(rownumber, nozzle_id, nozzle_no, brand, expirydate, producttype, positiononassest) {
            console.log(rownumber, nozzle_id, nozzle_no, brand, expirydate, producttype, positiononassest);
            

            $('#<%= row.ClientID%>').val(rownumber);
            $('#<%= hiddenAdvancedetailid.ClientID%>').val(nozzle_id);
           <%-- $('#<%= txtVendor.ClientID%>').val(vendorcode);--%>
            $('#<%= txtbrand.ClientID%>').val(brand);
            $('#<%= txtproducttype.ClientID%>').val(producttype);
            $('#<%= txtnozzle_no.ClientID%>').val(nozzle_no);
            $('#<%= txtpositiononassest.ClientID%>').val(positiononassest);
            $('#<%= txtexpirydate.ClientID%>').val(expirydate);

            $('#exampleModal').modal('show');
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
