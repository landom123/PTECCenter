﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="RVtoHQ.aspx.vb" Inherits="PTECCENTER.RVtoHQ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
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
                           <i class="fa fa-tasks" aria-hidden="true"></i>RV to HQ
                  </li>
                </ol>
                <p>
                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
                </p>
                <div class="row">
                    <div class="col-12">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">วันที่ปิดวัน</span>
                          </div>
                          <asp:FileUpload ID="FileUpload1" class="btn btn-primary" runat="server" text="เลือกไฟล์" />
                          &nbsp;<asp:Button ID="btnOpen" class="btn btn-sm  btn-primary" runat="server" Text="Open" />&nbsp;
                          &nbsp;<asp:Button ID="btnSave" class="btn btn-sm  btn-primary" runat="server" Text="Save to HQ" />&nbsp;
                          
                        </div>
                    </div>
                </div>

                          <div class="card-body">
                              <div class="table-responsive">
                                <asp:GridView ID="gvData2"  
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="true"
                                    emptydatatext="No data available." 
                                    CssClass="table table-striped"
                                    showheader="false"
                                    runat="server">
                                    
                                    <AlternatingRowStyle BackColor="#CCCCFF" />

                              </asp:GridView>
                                                      <asp:GridView ID="gvdata"  runat="server"                                      ></asp:GridView>
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


</asp:Content>