﻿<%@ Page Title="Budget" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="BudgetOilCost.aspx.vb" Inherits="PTECCENTER.BudgetOilCost" %>
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
                <ol class="breadcrumb">
                  <li class="breadcrumb-item">
                    <a href="#">Budget</a>
                  </li>
                </ol>
                <div class="row">
                    <div class="col-md-12 mb-3">
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text="Save"   />  &nbsp;              
                    </div>
                </div>
                
                <div class="row">
                    <div class="col-md-4 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">ปีงบประมาณ</span>
                          </div>
                          <asp:DropDownList class="form-control" ID="cboYear" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">ต้นทุน บาท/ลิตร</span>
                          </div>
                            <asp:TextBox class="form-control" ID="txtCost" runat="server" ></asp:TextBox>     
                            <asp:CompareValidator class="btn btn-sm  btn-danger" runat="server" Operator="DataTypeCheck" Type="Double" 
                                ControlToValidate="txtCost" ErrorMessage=" ใส่ตัวเลข" />                            
                        </div>
                    </div>
                    <div class="col-md-4 mb-3">
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
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <CompositeScript>
            <Scripts>
                <asp:ScriptReference Path="~/datetimepicker/jquery.js" />
                <asp:ScriptReference Path="~/datetimepicker/build/jquery.datetimepicker.full.min.js" />
            </Scripts>
        </CompositeScript>
    </asp:ScriptManager>
  <!-- datetimepicker ต้องไปทั้งชุด-->
   <%-- <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>--%>

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
