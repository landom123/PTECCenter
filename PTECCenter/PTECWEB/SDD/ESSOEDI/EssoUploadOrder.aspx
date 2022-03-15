<%@ Page Title="SDD Web Application : Upload Order" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="EssoUploadOrder.aspx.vb" Inherits="PTECCENTER.EssoUploadOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                    <a href="#">SDD\Upload ESSO PO</a>
                  </li>
                </ol>


    <div class="card card-login mx-auto mt-5">
      <div class="card-header">Upload PO</div>
      <div class="card-body">
          <label class="sr-only" for="inlineFormInputGroupUsername">Upload PO</label>
          <asp:FileUpload ID="FileUpload1" class="btn btn-primary" runat="server" text="เลือกไฟล์" />
          <p></p>
          <asp:Button ID="btnPreview" class="btn btn-sm  btn-primary" runat="server" Text="Import" />&nbsp;
          <asp:Button ID="btnUpload" class="btn btn-sm  btn-success" runat="server" Text="Upload" />
          <p><asp:label ID="lblstatus" class="btn btn-sm  btn-danger" runat="server" Text="" />  </p>
       </div>
    </div>
    <p></p>
    <div class="row">
        <div class="col-12">
            <asp:GridView ID="GridView1" runat="server" ></asp:GridView><Br />
            <asp:GridView ID="GridView2" runat="server" ></asp:GridView>
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
