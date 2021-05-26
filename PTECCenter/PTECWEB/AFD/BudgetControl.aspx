<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="BudgetControl.aspx.vb" Inherits="PTECCENTER.BudgetControl" %>
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
                <ol class="breadcrumb">
                  <li class="breadcrumb-item">
                    <a href="#">Budget</a>
                  </li>
                </ol>
                <div class="row">
                    <div class="col-12">
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text="Save" />  &nbsp;              
                    </div>
                </div>
                <p></p>
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">ปีงบประมาณ</span>
                          </div>
                          <asp:DropDownList class="form-control" ID="cboYear" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">วันที่เริ่มทำงบฯ</span>
                          </div>
                            <asp:TextBox class="form-control" ID="txtbegindate" runat="server" ></asp:TextBox>                          
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">วันที่ครบกำหนดทำงบฯ</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtenddate" runat="server" ></asp:TextBox>   
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
        jQuery('[id$=txtbegindate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format:'d/m/yy'
        });
    </script>

    <script type="text/javascript">
        jQuery('[id$=txtenddate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format:'d/m/yy'
        });
    </script>
</asp:Content>
