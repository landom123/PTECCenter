<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="Jobs_Report_byRange.aspx.vb" Inherits="PTECCENTER.Jobs_Report_byRange" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
          <!-- datetimepicker-->
  <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="wrapper">

    <!-- #include virtual ="/include/menu.inc" --> <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">    
                <ol class="breadcrumb"style="background-color:deeppink;color:white">
                  <li class="breadcrumb-item" >
                    รายงาน OPS 
                  </li>
                </ol>                
                <div class="row">
                    <div class="col-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">เริ่ม</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtBeginDate" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">สิ้นสุด</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtEndDate" runat="server" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">สถานะ</span>
                              <asp:DropDownList class="form-control" ID="cboBranch" AutoPostBack="True" 
                                  runat="server" >
                              </asp:DropDownList>
                          </div>                          
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">สาขา</span>
                              <asp:DropDownList class="form-control" ID="cboStatus" AutoPostBack="True" 
                                  runat="server" >
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
                              <asp:Button class="btn btn-sm  btn-primary" ID="btnReport" runat="server" Text="เรียกดู" />
                          </div>                          
                        </div>
                    </div>
                    
                </div>
                <div class="row">
                    
                </div>

        </div>        <!-- end content-wrapper -->


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
        jQuery('[id$=txtBeginDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            format:'d/m/Y'
        });
    </script>

    <script type="text/javascript">
        jQuery('[id$=txtEndDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            format:'d/m/Y'
        });
    </script>

</asp:Content>
