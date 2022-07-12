<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="test02.aspx.vb" Inherits="PTECCENTER.test02" %>

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
                           <i class="fa fa-tasks" aria-hidden="true"></i> ทดสอบเรียกตารางค่าใช้จ่าย ที่ต้องจ่ายในเดือน ตามสัญญา
                  </li>
                </ol>
                <p></p>
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">เลขที่สัญญา</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtagreeno" runat="server"></asp:TextBox>
                          
                          
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">เดือน</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtMonthly" runat="server"></asp:TextBox>
                          &nbsp;<asp:Button ID="btnFind" class="btn btn-sm  btn-primary" runat="server" Text="Find" />&nbsp;
                        </div>
                    </div>
                </div>

                          <div class="card-body">
                              <asp:GridView ID="GridView1" runat="server"></asp:GridView>
                          </div>
                          <div class="card-body">
                              <asp:GridView ID="GridView2" runat="server"></asp:GridView>
                          </div>
                          <div class="card-body">
                              <asp:GridView ID="GridView3" runat="server"></asp:GridView>
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
        jQuery('[id$=txtCloseDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format:'d/m/Y'
        });
    </script>


</asp:Content>
