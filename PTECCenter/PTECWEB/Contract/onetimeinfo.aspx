﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="onetimeinfo.aspx.vb" Inherits="PTECCENTER.onetimeinfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper" class="h-100">

    <!-- #include virtual ="/include/menu.inc" --> <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">    
                <!-- Breadcrumbs-->
                <ol class="breadcrumb"style="background-color:deeppink;color:white">
                  <li class="breadcrumb-item" >
                           <a href="client_list.aspx"><i class="fa fa-tasks" aria-hidden="true"></i></a> ข้อมูลการจ่ายเงินแบบครั้งเดียว
                  </li>
                </ol>
                <p></p>
                <div class="row">
                    <div class="col-6">
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-primary" runat="server" Text=" New " />
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text=" Save " />
                        <asp:Button ID="btnDel" class="btn btn-sm  btn-success" runat="server" Text=" Delete " />
                    </div>
                    <div class="col-6" style="text-align:right">
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
                                ประเภทการจ่าย
                            <div class="input-group sm-3">
                                <asp:dropdownlist class="form-control" ID="cboPayment" runat="server" ></asp:dropdownlist>
                            </div>
                        </div>
                        <div class="col-md-4">
                                กำหนดชำระ
                            <div class="input-group sm-3">
                                <asp:TextBox class="form-control" ID="txtDueDate" runat="server" style="background-color:white"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="padding-top: 1rem;">
                        <div class="col-md-4 ">
                                จำนวนเงิน
                            <div class="input-group sm-">
                                <asp:TextBox class="form-control" ID="txtAmount" runat="server" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                                ผู้รับผิดชอบ
                            <div class="input-group sm-3">
                                <asp:RadioButton ID="rdoClient" runat="server" groupname="clientpaid"/>คู่สัญญาจ่าย &nbsp;&nbsp;
                                <asp:RadioButton ID="rdoCompany" runat="server" groupname="clientpaid"/>บริษัทจ่าย
                            </div>
                        </div>

                    </div>
                    <div class="row" style="padding-top: 1rem;">
                        <div class="col-md-12 ">
                                หมายเหตุ
                            <div class="input-group sm-">
                                <asp:TextBox class="form-control" ID="txtRemark" runat="server" ></asp:TextBox>
                            </div>
                        </div>
                    </div>
             
                 </div>


            </div>            <!-- /.container-fluid -->
            <!-- Sticky Footer -->
            <footer class="sticky-footer d-none">
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
            lang: 'th',// แสดงภาษาไทย
            yearOffset: 543,// ใช้ปี พ.ศ. บวก 543 เพิ่มเข้าไปในปี ค.ศ  
            timepicker: false,
            scrollInput: false,
            format:'d/m/Y'
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
