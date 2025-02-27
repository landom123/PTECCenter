<%@ Page Title="จัดการสถานะงานOPS" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="JobsManageCost.aspx.vb" Inherits="PTECCENTER.JobsManageCost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper" class="h-100">

        <!-- #include virtual ="/include/menu.inc" -->
        <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">
                <!-- Breadcrumbs-->
                <ol class="breadcrumb">
                    <li class="breadcrumb-item">
                        <a href="#">ADMIN > จัดการข้อมูลงานOPS</a>
                    </li>
                </ol>
                <div class="row">
                    <div class="col-12  mb-3">
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-primary" runat="server" Text="New" />
                        &nbsp;
                         
                    </div>
                </div>

                <div class="row">
                    <div class="col-xl-4  mb-3">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">เลขที่เอกสาร</span>
                            </div>
                            <asp:TextBox class="form-control" ID="txtJobnofind" runat="server" ReadOnly="true"></asp:TextBox>
                            <div class="input-group-append">
                                <button type="button" class="btn btn-sm  btn-secondary" onclick="find('../ADMIN/JobsManageCost.aspx?jobno=','ระบุเลขที่ OPS')">Find</button>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-4 mb-3">
                        <div class="input-group sm-3">
                            <div class="input-group-prepend">
                                <span class="input-group-text">เลขที่งานย่อย</span>
                            </div>
                            <asp:DropDownList class="form-control" ID="cboJobdtlId" runat="server" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="card  mb-3">
                    <div class="card-header" style="background-color: navy; color: white">
                        รายละเอียดงาน
                    </div>
                    <div class="card-body">

                        <div class="row" style="padding-top: 1rem;">
                            <div class="col-md-4 mb-3">
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
                            <div class="col-md-4 mb-3">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">ฝ่าย</span>
                                    </div>
                                    <asp:TextBox class="form-control" ID="txtDepartment" runat="server" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4 mb-3">
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
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">จำนวน</span>
                                    </div>
                                    <asp:TextBox class="form-control" ID="txtQuantity" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4 mb-3">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">หน่วย</span>
                                    </div>
                                    <asp:TextBox class="form-control" ID="txtUnit" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4 mb-3">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">ประเภทหมวดราคา</span>
                                    </div>
                                    <asp:TextBox class="form-control" ID="txtJobCenter" runat="server" ReadOnly="true">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4 mb-3">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">ค่าใช้จ่าย (ประมาณ)</span>
                                    </div>
                                    <asp:TextBox class="form-control" ID="txtCost" runat="server" ReadOnly="true"></asp:TextBox>
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
                            <div class="col-12">
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


                <div class="card  mb-3">
                    <div class="card-header" style="background-color: lightslategray; color: white">
                        สำหรับผู้ปฏิบัติงาน
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-xl-4  mb-3">
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">ความเร่งด่วน</span>
                                    </div>
                                    <asp:DropDownList class="form-control" ID="cboPolicy"
                                        runat="server" AutoPostBack="false">
                                    </asp:DropDownList>
                                    <div class="input-group-append">
                                        <asp:Button ID="btnUpdatePolicy" class="btn btn-sm  btn-success" runat="server" Text="Update" />

                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-4  mb-3">
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">วันที่ต้องการ</span>
                                    </div>
                                    <asp:TextBox class="form-control" ID="txtDueDate" runat="server"></asp:TextBox>
                                    <div class="input-group-append">
                                        <asp:Button ID="btnUpdateDuedate" class="btn btn-sm  btn-success" runat="server" Text="Update" />
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>


                <div class="card  mb-3">
                    <div class="card-header" style="background-color: lightslategray; color: white">
                        สำหรับผู้ปฏิบัติงาน
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-4 mb-3">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">ค่าใช้จ่าย (ประมาณ)</span>
                                    </div>
                                    <asp:TextBox ID="TextBox1" class="form-control text-right" runat="server" type="number" min="0"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4 mb-3">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">Supplier</span>
                                    </div>
                                        <asp:DropDownList class="form-control" ID="cboSupplier"
                                            runat="server">
                                        </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4 mb-3">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">ประเภทหมวดราคา</span>
                                    </div>
                                        <asp:DropDownList class="form-control" ID="cboJobCenter"
                                            runat="server">
                                        </asp:DropDownList>
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
                            <asp:Button ID="btnUpdate" class="btn btn-sm  btn-success" runat="server" Text="Update Cost and Supplier" />
                        </div>
                    </div>
                </div>





                <div class="card  mb-3">
                    <div class="card-header" style="background-color: lightslategray; color: white">
                        สำหรับผู้ปฏิบัติงาน
                    </div>
                    <div id="jobdetail" class="card-body">
                        <div class="row">
                            <div class="col-md-3 mb-3">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">ปิด</span>
                                    </div>
                                        <asp:DropDownList class="form-control" ID="cboCloseType" runat="server">
                                        </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3 mb-3">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">วันเริ่มต้นประกัน</span>
                                    </div>
                                    <asp:TextBox class="form-control" ID="txtBeginWarr" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3 mb-3">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">วันสิ้นสุดประกัน</span>
                                    </div>
                                    <asp:TextBox class="form-control" ID="txtEndWarr" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3 mb-3">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">ชนิดงาน</span>
                                    </div>
                                        <asp:DropDownList class="form-control" ID="cboCloseCategory" runat="server">
                                        </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3 mb-3">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">Invoice No</span>
                                    </div>
                                        <asp:TextBox class="form-control" ID="txtInvoiceNo" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3 mb-3">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">วันที่ Invoice</span>
                                    </div>
                                    <asp:TextBox class="form-control" ID="txtInvDate" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3 mb-3">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">วันที่ปิดงาน</span>
                                    </div>
                                        <asp:TextBox class="form-control" ID="txtCloseDate" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-12 mb-3">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">รายละเอียดปิดงาน</span>
                                    </div>
                                    <asp:TextBox class="form-control" ID="txtRemark" runat="server" TextMode="MultiLine"></asp:TextBox>
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
                            <asp:Button ID="btnUpdateinvoice" class="btn btn-sm  btn-success" runat="server" Text="update invoice" />
                        </div>
                    </div>
                </div>
                <div class="card mb-3">
                    <div class="card-header" style="background-color: lightslategray; color: white">
                        สำหรับผู้ปฏิบัติงาน
                    </div>
                    <div class="row">
                        <div class="col-12">

                            <div class="card-body">
                                <div class="table-responsive">

                                    <table class="table table-bordered" id="dataTable">
                                        <thead class="table-warning">
                                            <tr>
                                                <th scope="col" class="text-center">รายการ</th>
                                                <th scope="col" class="text-center">ราคา/หน่วย</th>
                                                <th scope="col" class="text-center">หน่วย</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <% For i = 0 To costtable.Rows.Count %>
                                            <% If i < costtable.Rows.Count Then%>
                                            <tr>
                                                <td><% =costtable.Rows(i).Item("jobscenterdtlname") %></td>
                                                <td class="text-right"><% =costtable.Rows(i).Item("jobcostamount") %></td>
                                                <td class="text-right"><% =costtable.Rows(i).Item("jobcostunit") %></td>
                                                <td>
                                                    <button runat="server" id="btnDelete" name="btnDelete" onclick="return confirmDelete(this);" type='button' class='close' aria-label='Close Close-danger'>
                                                        <span aria-hidden='true'>&times;</span>
                                                    </button>
                                                </td>
                                                <input type="hidden" name="iddelete" value="<% =costtable.Rows(i).Item("jobcostid") %>">
                                            </tr>
                                            <% Else %>
                                            <tr>
                                                <th class="text-right">รวม</th>
                                                <th class="text-right"><% =total %></th>
                                                <th class="text-right"></th>
                                            </tr>
                                            <% End If %>
                                            <% Next i %>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="cboCost" class="form-control" runat="server" data-toggle="dropdown"></asp:DropDownList>
                                                    <!-- <div class="input-group sm-3 boxcost">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">ค่า</span>
                                                </div>
                                                <asp:TextBox class="form-control" ID="txtcboCost" runat="server" required></asp:TextBox>
                                                <div class="invalid-feedback">กรุณากรอกรายละเอียด</div>
                                            </div> -->
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
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.container-fluid -->
        </div>
        <!-- end content-wrapper -->


        <!-- end เนื้อหาเว็บ -->


    </div>
    <!-- /#wrapper -->


    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <CompositeScript>
            <Scripts>
                <asp:ScriptReference Path="~/datetimepicker/jquery.js" />
                <asp:ScriptReference Path="~/datetimepicker/build/jquery.datetimepicker.full.min.js" />
            </Scripts>
        </CompositeScript>
    </asp:ScriptManager>
    <!-- datetimepicker ต้องไปทั้งชุด-->
    <%--    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>--%>



    <script type="text/javascript">
        jQuery('[id$=txtBeginWarr]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
        jQuery('[id$=txtDueDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: true,
            scrollInput: false,
            format: 'd/m/Y H:i'
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
            timepicker: true,
            scrollInput: false,
            format: 'd/m/Y H:i'
        });
    </script>
    <script type="text/javascript">

        $(document).ready(function () {
            var groups = {};
            $("select option[data-category]").each(function () {
                groups[$.trim($(this).attr("data-category"))] = true;
            });
            $.each(groups, function (c) {
                $("select option[data-category='" + c + "']").wrapAll('<optgroup label="' + c + '">');
            });
            $('.form-control').selectpicker({
                liveSearch: true,
                maxOptions: 1
            });
        });

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
        function validateDataCost() {

            var txtcboCost = document.getElementById('<%= txtcboCost.ClientID%>').value
            var cboCost = document.getElementById('<%= cboCost.ClientID%>').value
            if (cboCost == '99999' && !txtcboCost) {
                alertWarning('กรอกไม่ครบถ้วน');
                event.preventDefault();
                event.stopPropagation();
            }

        }
    </script>


</asp:Content>
