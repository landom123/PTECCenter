<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="requestcontract.aspx.vb" Inherits="PTECCENTER.requestcontract" %>

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
                           <a href="client_list.aspx"><i class="fa fa-tasks" aria-hidden="true"></i></a> ร้องขอสัญญาใหม่
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
                                        <span class="input-group-text">เลขที่เอกสาร</span>
                                        </div>
                                        <asp:TextBox class="form-control" ID="txtdocuno" placeholder="Document No" ReadOnly="true" runat="server" ></asp:TextBox>    

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

                            <%-- input area --%>
                              <div class="card-body">
                      
                                    <div class="row">
                                            <div class="col-3">
                                                <div class="input-group sm-3">
                                                    <div class="input-group-prepend">
                                                    <span class="input-group-text">รหัสสาขา</span>
                                                    </div>
                                                        <asp:DropDownList ID="cboBranch" class="form-control" runat="server" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-3">
                                                <div class="input-group sm-3">
                                                    <div class="input-group-prepend">
                                                    <span class="input-group-text">ประเภทสัญญา</span>
                                                    </div>
                                                    <asp:DropDownList class="form-control" ID="cboContractType"  runat="server" ></asp:DropDownList>    
                                                </div>
                                            </div>

                                             <div class="col-3">
                                                <div class="input-group sm-3">
                                                    <div class="input-group-prepend">
                                                    <span class="input-group-text">วันที่เริ่มสัญญา</span>
                                                    </div>
                                                    <asp:TextBox class="form-control" ID="txtContractBeginDate" style="background-color:white" runat="server"></asp:TextBox>    

                                                </div>
                                            </div>

                                             <div class="col-3">
                                                <div class="input-group sm-3">
                                                    <div class="input-group-prepend">
                                                    <span class="input-group-text">วันที่สิ้นสุดสัญญา</span>
                                                    </div>
                                                    <asp:TextBox class="form-control" ID="txtContractEndDate" style="background-color:white" runat="server"></asp:TextBox>    

                                                </div>
                                            </div>


                                    </div>

                                    <div class="row" style="padding-top: 1rem;">
                                        <div class="col-md-3">
                                                ชื่อ-นามสกุล
                                            <div class="input-group sm-">
                                                <asp:TextBox class="form-control" ID="txtName" runat="server" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                                เลขที่่บัตรประจำตัวประชาชน
                                            <div class="input-group sm-3">
                                                <asp:TextBox class="form-control" ID="txtCardID" runat="server" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                                เพศ
                                            <div class="input-group sm-3">
                                                <asp:dropdownlist class="form-control" ID="cboSex" runat="server" ></asp:dropdownlist>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="padding-top: 1rem;">
                                        <div class="col-md-3">
                                                ชื่อบริษัท
                                            <div class="input-group sm-">
                                                <asp:TextBox class="form-control" ID="txtCompany" runat="server" ></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                                Mobile Phone
                                            <div class="input-group sm-3">
                                                <asp:TextBox class="form-control" ID="txtMobile" runat="server" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top: 1rem;">
                                        <div class="col-md-3">
                                                Tel
                                            <div class="input-group sm-">
                                                <asp:TextBox class="form-control" ID="txtTel" runat="server" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                                Email
                                            <div class="input-group sm-3">
                                                <asp:TextBox class="form-control" ID="txtEmail" runat="server" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                                Line
                                            <div class="input-group sm-3">
                                                <asp:TextBox class="form-control" ID="txtLine" runat="server" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="padding-top: 1rem;">
                                            <div class="col-3">
                                                <div class="input-group sm-3">
                                                    <div class="input-group-prepend">
                                                    <span class="input-group-text">สถานะ</span>
                                                    </div>
                                                    <asp:DropDownList class="form-control" ID="cboStatus"  runat="server" ></asp:DropDownList>    
                                                </div>
                                            </div>
                                    </div>

                                    <hr style="height:2px;border-width:0;color:gray;background-color:gray">
                                    <div class="row" style="padding-top: 1rem;">
                                        <div class="col-md-12">
                                                ที่อยู่
                                            <div class="input-group sm-3">
                                                <asp:TextBox class="form-control" ID="txtAddress" runat="server" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top: 1rem;">
                                        <div class="col-md-3">
                                                ตำบล/แขวง
                                            <div class="input-group sm-3">
                                                <asp:TextBox class="form-control" ID="txtSubDistrict" runat="server" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                                อำเภอ/เขต
                                            <div class="input-group sm-3">
                                                <asp:TextBox class="form-control" ID="txtDistrict" runat="server" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                                จังหวัด
                                            <div class="input-group sm-3">
                                                <asp:TextBox class="form-control" ID="txtProvince" runat="server" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top: 1rem;">
                                        <div class="col-md-3">
                                                รหัสไปรษณีย์
                                            <div class="input-group sm-3">
                                                <asp:TextBox class="form-control" ID="txtPostcode" runat="server" ></asp:TextBox>
                                            </div>
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
        jQuery('[id$=txtBirthday]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
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
