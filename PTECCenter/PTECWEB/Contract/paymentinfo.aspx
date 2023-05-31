<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="paymentinfo.aspx.vb" Inherits="PTECCENTER.paymentinfo" %>

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
                           <a href="client_list.aspx"><i class="fa fa-tasks" aria-hidden="true"></i></a> ข้อมูลช่องทางการชำระเงิน
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
                                    วิธีชำระ
                                <div class="input-group sm-3">
                                    <asp:RadioButton ID="rdoTrans" runat="server" groupname="paid" Checked="true"/>โอน&nbsp;&nbsp;
                                    <asp:RadioButton ID="rdoCheque" runat="server" groupname="paid"/>เช็ค
                                </div>
                            </div>

                        </div>
                        <div class="row" style="padding-top: 1rem;">
                            <div class="col-md-4">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ธนาคาร</span>
                                    <asp:DropDownList class="form-control" ID="cboBank"  runat="server" ></asp:DropDownList>  
                                </div>                                                                
                                <%--<div class="input-group sm-3">--%>
                                    
                                    <%--<asp:TextBox class="form-control" ID="txtBankCode" runat="server" ></asp:TextBox>--%>
                                    
                                <%--</div>--%>
                            </div>
                            <div class="col-md-4 ">
                                    
                                <div class="input-group-prepend">
                                    <span class="input-group-text">รหัสสาขา</span>
                                     <asp:TextBox class="form-control" ID="txtBankbranchcode" runat="server" ></asp:TextBox>
                                </div>          
                          <%--      <div class="input-group sm-">
                                    <asp:TextBox class="form-control" ID="txtBankbranchcode" runat="server" ></asp:TextBox>
                                </div>--%>
                            </div>
                            <div class="col-md-4">
                                   
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ชื่อสาขา</span>
                                    <asp:TextBox class="form-control" ID="txtBankbranchname" runat="server" ></asp:TextBox>
                                </div>  
                              <%--     <div class="input-group sm-3">
                                 <asp:TextBox class="form-control" ID="txtBankbranchname" runat="server" ></asp:TextBox>
                                </div>--%>
                            </div>

                        </div>
                        <div class="row" style="padding-top: 1rem;">
                            <div class="col-md-4 ">
                                    
                                <div class="input-group-prepend">
                                    <span class="input-group-text">เลขบัญชี</span>
                                    <asp:TextBox class="form-control" ID="txtAccountNo" runat="server" ></asp:TextBox>
                                </div>  
                           <%--     <div class="input-group sm-">
                                    <asp:TextBox class="form-control" ID="txtAccountNo" runat="server" ></asp:TextBox>
                                </div>--%>
                            </div>
                            <div class="col-md-4 ">
                                    
                                <div class="input-group-prepend">
                                    <span class="input-group-text">ชื่อบัญชี</span>
                                    <asp:TextBox class="form-control" ID="txtAccountName" runat="server" ></asp:TextBox>
                                </div>  
                           <%--     <div class="input-group sm-">
                                    <asp:TextBox class="form-control" ID="txtAccountName" runat="server" ></asp:TextBox>
                                </div>--%>
                            </div>
                        </div>

                        <div class="row" style="padding-top: 1rem;">
                            <div class="col-md-3">                                    
                                <div class="input-group-prepend">
                                    <span class="input-group-text">สั่งจ่าย</span>
                                    <asp:DropDownList class="form-control" ID="cboPayCust" runat="server" ></asp:DropDownList>
                                </div>                          
                            </div>
                            <div class="col-md-3">                                    
                                <div class="input-group-prepend">
                                    <span class="input-group-text">หักภาษี ณ ที่จ่าย</span>
                                    <asp:DropDownList class="form-control" ID="cboTaxCust" runat="server" ></asp:DropDownList>
                                </div>                          
                            </div>
                            <div class="col-md-3">                                    
                                <div class="input-group-prepend">
                                    <span class="input-group-text">A/C Code</span>
                                     <asp:TextBox class="form-control" ID="txtACCode" runat="server" ></asp:TextBox>
                                </div>                          
                            </div>
                        </div>

                        <div class="row" style="padding-top: 1rem;">
                            <div class="col-md-4 ">                            
                                <div class="input-group sm-">
                                    <asp:checkbox class="checkbox" ID="chkActive" runat="server" text ="ใช้งาน"></asp:checkbox>
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
