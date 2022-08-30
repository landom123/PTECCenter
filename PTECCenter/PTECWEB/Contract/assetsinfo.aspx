<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="assetsinfo.aspx.vb" Inherits="PTECCENTER.assetsinfo" %>

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
                           <a href="client_list.aspx"><i class="fa fa-tasks" aria-hidden="true"></i></a> ข้อมูลทรัพย์สินในสัญญา
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

                    <%-- input area --%>
                    <div class="row">
                        <div class="col-md-4">
                            รหัสทรัพย์สิน
                            <div class="input-group sm-3">
                                <asp:Textbox class="form-control" ID="txtAssetsNo" placeholder="please save first" runat="server" ReadOnly="true"></asp:Textbox>    
                            </div>
                        </div>
                        <div class="col-md-4">
                            ประเภทที่ดิน
                            <div class="input-group sm-3">
                                <asp:DropDownList class="form-control" ID="cboAssetType" runat="server"></asp:DropDownList>    
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            เลขที่โฉนด/เอกสารสิทธิ์
                            <div class="input-group sm-3">
                                <asp:Textbox class="form-control" ID="txtLandno" runat="server"></asp:Textbox>    
                            </div>
                        </div>
                        <div class="col-md-4">
                            เลขที่หน้าสำรวจ
                            <div class="input-group sm-3">
                                <asp:Textbox class="form-control" ID="txtSurveyNo" runat="server"></asp:Textbox>  
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            ตำบล
                            <div class="input-group sm-3">
                                <asp:Textbox class="form-control" ID="txtSubDistrict" runat="server"></asp:Textbox>    
                            </div>
                        </div>
                        <div class="col-md-4">
                            อำเภอ
                            <div class="input-group sm-3">
                                <asp:Textbox class="form-control" ID="txtDistrict" runat="server"></asp:Textbox>  
                            </div>
                        </div>
                        <div class="col-md-4">
                            จังหวัด
                            <div class="input-group sm-3">
                                <asp:Textbox class="form-control" ID="txtProvince" runat="server"></asp:Textbox>  
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            ทั้งแปลง/แบ่งเช่า
                            <div class="input-group sm-3">
                                <asp:RadioButton ID="rdoFull" runat="server" groupname= "fullarea" text=" ทั้งแปลง " Checked="true"/> &nbsp;&nbsp;
                                <asp:RadioButton ID="rdoPart" runat="server" groupname= "fullarea" text=" แบ่งเช่า " />
                            </div>
                        </div>
                        <div class="col-md-4">
                            ไร่
                            <div class="input-group sm-3">
                                <asp:Textbox class="form-control" ID="txtRai" runat="server"></asp:Textbox>  
                            </div>
                        </div>
                        <div class="col-md-4">
                            งาน
                            <div class="input-group sm-3">
                                <asp:Textbox class="form-control" ID="txtNgan" runat="server"></asp:Textbox>  
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            ตารางวา
                            <div class="input-group sm-3">
                                <asp:Textbox class="form-control" ID="txtWa" runat="server"></asp:Textbox>    
                            </div>
                        </div>
                        <div class="col-md-4">
                            พิกัด GPS
                            <div class="input-group sm-3">
                                <asp:Textbox class="form-control" ID="txtGPS" runat="server"></asp:Textbox>  
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
