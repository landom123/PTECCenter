<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="DeducttoNEWGSM.aspx.vb" Inherits="PTECCENTER.DeducttoNEWGSM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper" class="h-100">

    <!-- #include virtual ="/include/menu.inc" --> <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">    
                <!-- Breadcrumbs-->
                <ol class="breadcrumb"style="background-color:deeppink;color:white">
                  <li class="breadcrumb-item" >
                           <i class="fa fa-tasks" aria-hidden="true"></i>Import หักยอดขาย
                  </li>
                </ol>
                <p>
                    <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
                </p>
                <div class="row">
                    <div class="col-6">
                        <div class="input-group md8">
                          <asp:FileUpload ID="FileUpload1" class="btn btn-primary" runat="server" text="" />
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="input-group md4">
                          &nbsp;<asp:Button ID="btnOpen" class="btn btn-sm  btn-primary" runat="server" Text="Open" />&nbsp;
                          &nbsp;<asp:Button ID="btnSave" class="btn btn-sm  btn-primary" runat="server" Text="Save to NEW GSM" />&nbsp;                          
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12"><%= filename %></div>
                </div>
                          <div class="card-body">
                              <div class="table-responsive">
                                <asp:GridView ID="gvData"  
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="true"
                                    emptydatatext="No data available." 
                                    CssClass="table table-striped"
                                    runat="server">
                                    
                                    <AlternatingRowStyle BackColor="#CCCCFF" />

                              </asp:GridView>

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

    
    <script type="text/javascript">
        function alertSuccess(massage) {
            Swal.fire(
                massage,
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
