<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="clientinfo.aspx.vb" Inherits="PTECCENTER.clientinfo" %>

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
                           <a href="client_list.aspx"><i class="fa fa-tasks" aria-hidden="true"></i></a> ทะเบียนข้อมูลคู่สัญญา
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
                            <div class="col-3">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">เลขที่สัญญา</span>
                                    </div>
                                    <asp:TextBox class="form-control" ID="txtContractNo" placeholder="please save first" ReadOnly="true" runat="server" ></asp:TextBox>    
                                    <asp:Label class="form-control" ID="lblContracttype" style="background-color:darkgreen;color:white" runat="server" ></asp:Label>
                                </div>
                            </div>

                            <div class="col-3">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                    <span class="input-group-text">สถานะ</span>
                                    </div>
                                    <asp:Label class="form-control" ID="lblStatus" style="background-color:darkgreen;color:white" runat="server" ></asp:Label>    

                                </div>
                            </div>
                    </div>
                    <div class="row" style="padding-top: 0.2rem;">
                        <div class="col-md-3">                                
                            <div class="input-group sm-3">
                                รหัสคู่สัญญา &nbsp
                                <asp:TextBox class="form-control" ID="txtClientNo" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">                                
                            <div class="input-group sm-3">
                                วันที่สร้างรายการ &nbsp
                                <asp:TextBox class="form-control" ID="txtCreateDate" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">                               
                            <div class="input-group sm-3">
                                 ผู้สร้างรายการ &nbsp
                                <asp:TextBox class="form-control" ID="txtCreateBy" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="padding-top: 0.2rem;">
                        <div class="col-md-4 ">                                
                            <div class="input-group sm-">
                                ชื่อ-นามสกุล &nbsp
                                <asp:TextBox class="form-control" ID="txtName" runat="server" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">                                
                            <div class="input-group sm-3">
                                เลขที่่บัตรประจำตัวประชาชน &nbsp
                                <asp:TextBox class="form-control" ID="txtCardID" runat="server" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">                                
                            <div class="input-group sm-3">
                                เพศ &nbsp
                                <asp:dropdownlist class="form-control" ID="cboSex" runat="server" ></asp:dropdownlist>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="padding-top: 0.2rem;">
                        <div class="col-md-4 ">                                
                            <div class="input-group sm-">
                                ชื่อบริษัท &nbsp
                                <asp:TextBox class="form-control" ID="txtCompany" runat="server" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">                                
                            <div class="input-group sm-3">
                                วันเกิด &nbsp
                                <asp:TextBox class="form-control" ID="txtBirthday" runat="server" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">                                
                            <div class="input-group sm-3">
                                Mobile Phone &nbsp
                                <asp:TextBox class="form-control" ID="txtMobile" runat="server" ></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="padding-top: 0.2rem;">
                        <div class="col-md-3">                                
                            <div class="input-group sm-2">
                                Tel &nbsp
                                <asp:TextBox class="form-control" ID="txtTel" runat="server" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">                               
                            <div class="input-group sm-2">
                                 Email &nbsp
                                <asp:TextBox class="form-control" ID="txtEmail" runat="server" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">                              
                            <div class="input-group sm-2">
                                  Line &nbsp
                                <asp:TextBox class="form-control" ID="txtLine" runat="server" ></asp:TextBox>
                            </div>
                        </div>
                    </div>

<%--                <div class="row"  style="padding-top: 0.2rem;">
                   
                        <div class="col-3">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                <span class="input-group-text">ผู้จ่ายภาษีที่ดิน</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboContractLand"  runat="server" ></asp:DropDownList>    
                            </div>
                        </div>                   

                        <div class="col-3">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                <span class="input-group-text">ผู้จ่ายภาษีสิ่งปลูกสร้าง</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboContractBu"  runat="server" ></asp:DropDownList>    
                            </div>
                        </div>
                 
                        <div class="col-3">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                <span class="input-group-text">การจ่ายเงินวันจดเช่า</span>
                                </div>
                                <asp:DropDownList class="form-control" ID="cboContractDayRent"  runat="server" ></asp:DropDownList>    
                            </div>
                       </div>                                                       

                </div>--%>

                    <hr style="height:2px;border-width:0;color:gray;background-color:gray">

                    <div class="row" style="padding-top: 0.2rem;">
                        <div class="col-md-12">
                             <span class="input-group-text" style="background-color:green ;color:white" runat="server" > ที่อยู่ตามทะเบียนบ้าน</span>                                                   
                        </div>
                    </div>

                    <div class="row" style="padding-top: 0.2rem;">
                        <div class="col-md-10">
                          
                            <div class="input-group sm-3">
                                  ที่อยู่ &nbsp
                                <asp:TextBox class="form-control" ID="txtAddress" runat="server" ></asp:TextBox>
                            </div>

                        </div>
                    </div>
                    <div class="row" style="padding-top: 0.2rem;">
                        <div class="col-md-3">
                           
                            <div class="input-group sm-3">
                                 ตำบล/แขวง &nbsp
                                <asp:TextBox class="form-control" ID="txtSubdistrict" runat="server" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                           
                            <div class="input-group sm-3">
                                 อำเภอ/เขต &nbsp
                                <asp:TextBox class="form-control" ID="txtDistrict" runat="server" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                             
                            <div class="input-group sm-3">
                                จังหวัด &nbsp
                                <asp:TextBox class="form-control" ID="txtProvince" runat="server" ></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="padding-top: 0.2rem;">
                        <div class="col-md-3">
                           
                            <div class="input-group sm-3">
                                 รหัสไปรษณีย์ &nbsp
                                <asp:TextBox class="form-control" ID="txtPostcode" runat="server" ></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <%--<hr style="height:2px;border-width:0;color:gray;background-color:gray">--%>

                    <div class="row" style="padding-top: 0.2rem;">
                        <div class="col-md-12">
                             <span class="input-group-text" style="background-color:green ;color:white" runat="server" > ที่อยู่ส่งเอกสาร</span>                                                   
                        </div>
                    </div>

                    <div class="row" style="padding-top: 1rem;">
                        <div class="col-md-10">
                          
                            <div class="input-group sm-3">
                                  ที่อยู่ &nbsp
                                <asp:TextBox class="form-control" ID="txtAddress1" runat="server" ></asp:TextBox>
                            </div>

                        </div>
                    </div>
                    <div class="row" style="padding-top: 0.2rem;">
                        <div class="col-md-3">
                           
                            <div class="input-group sm-3">
                                 ตำบล/แขวง &nbsp
                                <asp:TextBox class="form-control" ID="txtSubdistrict1" runat="server" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                           
                            <div class="input-group sm-3">
                                 อำเภอ/เขต &nbsp
                                <asp:TextBox class="form-control" ID="txtDistrict1" runat="server" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                             
                            <div class="input-group sm-3">
                                จังหวัด &nbsp
                                <asp:TextBox class="form-control" ID="txtProvince1" runat="server" ></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="padding-top: 0.2rem;">
                        <div class="col-md-3">
                           
                            <div class="input-group sm-3">
                                 รหัสไปรษณีย์ &nbsp
                                <asp:TextBox class="form-control" ID="txtPostcode1" runat="server" ></asp:TextBox>
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
