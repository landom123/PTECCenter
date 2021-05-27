<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="Jobs_Close.aspx.vb" Inherits="PTECCENTER.JobsClose" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper">

        <!-- #include virtual ="/include/menu.inc" -->
        <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">
                <ol class="breadcrumb" style="background-color: deeppink; color: white">
                    <li class="breadcrumb-item">ปิดงาน (Close Job)
                    </li>
                </ol>

                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">เลขที่เอกสาร</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtJobno" runat="server" ReadOnly="true"></asp:TextBox>
                            <div class="input-group-append">
                                <asp:Button ID="btnBack" class="btn btn-sm  btn-success" runat="server" Text=" back " />
                                <br />
                            </div>
                        </div>

                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">วันที่แจ้ง</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtDocDate" runat="server" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ผู้แจ้ง</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtOwner" runat="server" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <p></p>
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">สาขา</span>
                                <asp:TextBox class="form-control" ID="txtBranch" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ฝ่าย</span>
                                <asp:TextBox class="form-control" ID="txtDepartment" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">แผนก</span>
                                <asp:TextBox class="form-control" ID="txtSection" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <p></p>
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ประเภทงาน</span>
                                <asp:TextBox class="form-control" ID="txtJobType" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">รหัสทรัพย์สิน</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtAssetCode" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <asp:TextBox class="form-control" ID="txtAssetName" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">จำนวน</span>
                                <asp:TextBox class="form-control" ID="txtQuantity" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">หน่วย</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtUnit" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ประเภทหมวดราคา</span>
                                <asp:TextBox class="form-control" ID="txtJobCenter" runat="server" ReadOnly="true">
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ค่าใช้จ่าย (ประมาณ)</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtCost" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Supplier</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtSupplier" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-4 justify-content-center">
                        <% If maintable.Rows(0).Item("followup_status") <> "ปิดงาน" Then %>
                        <asp:Button ID="btnClose" class="btn btn-sm btn-success justify-content-center" runat="server" Text="Confirm Close" />
                        <% End if %>
                    </div>
                </div>

                <br />
                <div class="row">
                    <div class="col-12">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">รายละเอียดงาน</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtDetail" runat="server" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />

                <div class="row">
                    <div class="col-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ปิด</span>
                                <asp:DropDownList class="form-control" ID="cboCloseType" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">วันเริ่มต้นประกัน</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtBeginWarr" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">วันสิ้นสุดประกัน</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtEndWarr" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">ชนิดงาน</span>
                                <asp:DropDownList class="form-control" ID="cboCloseCategory" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">Invoice No</span>
                                <asp:TextBox class="form-control" ID="txtInvoiceNo" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">วันที่ Invoice</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtInvDate" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">วันที่ปิดงาน</span>
                                <asp:TextBox class="form-control" ID="txtCloseDate" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-12">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">หมายเหตุ</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtRemark" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
            </div>
            <div class="row">
                <div class="col-12">

                    <div class="card-body">
                        <div class="table-responsive">

                            <table class="table table-bordered" id="dataTable">
                                <thead class="table-warning">
                                    <tr>
                                        <th scope="col" class="text-center">Cost Name</th>
                                        <th scope="col" class="text-center">Price</th>
                                        <th scope="col" class="text-center">Unit</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <% For i = 0 To costtable.Rows.Count %>
                                    <% If i < costtable.Rows.Count Then%>
                                    <tr>
                                        <td><% =costtable.Rows(i).Item("jobscenterdtlname") %></td>
                                        <td class="text-right"><% =costtable.Rows(i).Item("jobcostamount") %></td>
                                        <td class="text-right"><% =costtable.Rows(i).Item("jobcostunit") %></td>
                                        <% If maintable.Rows(0).Item("followup_status") <> "ปิดงาน" Then %>
                                        <td>
                                            <button runat="server" id="btnDelete" name="btnDelete" onclick="return confirmDelete(this);" type='button' class='close' aria-label='Close Close-danger'>
                                                <span aria-hidden='true'>&times;</span>
                                            </button>
                                        </td>
                                        <input type="hidden" name="iddelete" value="<% =costtable.Rows(i).Item("jobcostid") %>">
                                        <% End if %>
                                    </tr>
                                    <% Else %>
                                    <tr>
                                        <th class="text-right">รวม</th>
                                        <th class="text-right"><% =total %></th>
                                    </tr>
                                    <% End If %>
                                    <% Next i %>
                                    <% If maintable.Rows(0).Item("followup_status") <> "ปิดงาน" Then %>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="cboCost" class="form-control" runat="server" data-toggle="dropdown"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <div class="input-group">
                                                <asp:TextBox ID="txtCostPrice" class="form-control text-right" runat="server" type="number" min="0"></asp:TextBox>
                                                <div class="input-group-append">
                                                    <span class='input-group-text'>THB</span>
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCostUnit" class="form-control text-right" runat="server" type="number" min="1"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text=" + " />
                                        </td>
                                    </tr>
                                    <% End if %>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <!-- end content-wrapper -->


            <!-- end เนื้อหาเว็บ -->

            <!-- Sticky Footer -->
            <footer class="sticky-footer">
                <div class="container my-auto">
                    <div class="copyright text-center my-auto">
                        <span>Copyright © Your Website 2019</span>
                    </div>
                </div>
            </footer>
        </div>
        <!-- /#wrapper -->

    </div>
    <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>

    <script type="text/javascript">
        jQuery('[id$=txtBeginWarr]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>

    <script type="text/javascript">
        jQuery('[id$=txtEndWarr]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>

    <script type="text/javascript">
        jQuery('[id$=txtInvDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>

    <script type="text/javascript">
        jQuery('[id$=txtCloseDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.form-control').selectpicker({
                liveSearch: true,
                maxOptions: 1
            });
        });
        jQuery('[id$=cboCost]').on('show.bs.dropdown', function () {
            $('.table-responsive').css("overflow", "inherit");
        });

        jQuery('[id$=cboCost]').on('hide.bs.dropdown', function () {
            $('.table-responsive').css("overflow", "auto");
        })
    </script>

    <script type="text/javascript">
        function confirmDelete(lnk) {
            var row = lnk.parentNode.parentNode;
            console.log(row)
            //console.log(row.cells[0])
            console.log(row.rowIndex - 1)

            var jobcostid = document.getElementsByName('iddelete')[row.rowIndex - 1].value;
            console.log(jobcostid)
            /*alert(GridView);*/

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
    </script>
    <script type="text/javascript">
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
