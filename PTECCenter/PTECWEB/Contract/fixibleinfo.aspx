<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="fixibleinfo.aspx.vb" Inherits="PTECCENTER.fixibleinfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper">

    <!-- #include virtual ="/include/menu.inc" --> <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">    
                <!-- Breadcrumbs-->
                <ol class="breadcrumb"style="background-color:deeppink;color:white">
                  <li class="breadcrumb-item" >
                           <a href="client_list.aspx"><i class="fa fa-tasks" aria-hidden="true"></i></a> ข้อมูลการจ่ายเงินแบบ คงที่ (Fix)
                  </li>
                </ol>
                <p></p>
                <div class="row">
                    <div class="col-8">
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-primary" runat="server" Text=" New " />
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text=" Save " />
                        <asp:Button ID="btnDel" class="btn btn-sm  btn-success" runat="server" Text=" Delete " />
                    </div>
                    <div class="col-4" style="text-align:right">
                        <asp:Button ID="BtnContract" class="btn btn-sm  btn-success" runat="server" Text=" กลับ สัญญา " />
                    </div>
                </div>

                <div class="card-body">
                    <div class="row">
                            <div class="col-4">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                    <span class="input-group-text">เลขที่สัญญา</span>
                                    </div>
                                    <asp:TextBox class="form-control" ID="txtContractNo" placeholder="please save first" ReadOnly="true" runat="server" ></asp:TextBox>    

                                </div>
                            </div>
                            <div class="col-4">

                            </div>
                            <div class="col-4">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                    <span class="input-group-text">สถานะ</span>
                                    </div>
                                    <asp:Label class="form-control" ID="lblStatus" style="background-color:darkgreen;color:white" runat="server" ></asp:Label>    

                                </div>
                            </div>
                        </div>
                    <div class="row" style="padding-top: 1rem;">
                        <div class="col-md-4 ">
                                ID
                            <div class="input-group sm-3">
                                <asp:TextBox class="form-control" ID="txtid" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                                ค่าใช้จ่าย
                            <div class="input-group sm-3">
                                <asp:DropDownList ID="cboPaymentType" Class="form-control" runat="server"></asp:DropDownList>                                
                            </div>
                        </div>
                        <div class="col-md-4">
                                จำนวนเงิน
                            <div class="input-group sm-3">
                                <asp:TextBox class="form-control" ID="txtAmount" runat="server" ></asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div class="row" style="padding-top: 1rem;">
                        <div class="col-md-4">
                                รอบการจ่าย
                            <div class="input-group sm-3">
                                <asp:RadioButton ID="rdoMonth" runat="server" groupname="paid" Checked="true"/>รายเดือน&nbsp;&nbsp;
                                <asp:RadioButton ID="rdoYear" runat="server" groupname="paid"/>รายปี
                            </div>
                        </div>
                        <div class="col-md-4 ">
                                ความถี่การจ่าย
                            <div class="input-group sm-">
                                <asp:TextBox class="form-control" ID="txtFrequency" runat="server" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                                วันที่เริ่มมีผล
                            <div class="input-group sm-3">
                                <asp:TextBox class="form-control" ID="txtBegindate" runat="server" ></asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div class="row" style="padding-top: 1rem;">
                        <div class="col-md-4 ">
                                วันที่สิ้นสุด
                            <div class="input-group sm-">
                                <asp:TextBox class="form-control" ID="txtEnddate" runat="server" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4 ">
                                Due date
                            <div class="input-group sm-">
                                <asp:TextBox class="form-control" ID="txtDueDate" runat="server" ></asp:TextBox>
                            </div>
                        </div>
                    </div>
             
                 </div>


            </div>            <!-- /.container-fluid -->
            <!-- Sticky Footer -->
            <footer class="sticky-footer">
                <div class="container my-auto">
                    <div class="copyright text-center my-auto">
                    <span>Copyright © Your Website 2019</span>
                    </div>
                </div>
            </footer>
        </div>        <!-- end content-wrapper -->


        <!-- end เนื้อหาเว็บ -->

    </div>
    <!-- /#wrapper -->
  <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>

    <script type="text/javascript">
        jQuery('[id$=txtDueDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format:'d/m/Y'
        });
    </script>

    <script type="text/javascript">
        jQuery('[id$=txtEnddate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>

    <script type="text/javascript">
        jQuery('[id$=txtBegindate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
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
    </script>

</asp:Content>
