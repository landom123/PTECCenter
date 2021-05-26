<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="OilPriceFormulaUpdate.aspx.vb" Inherits="PTECCENTER.OilPriceFormulaUpdate" %>

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
                           <i class="fa fa-tasks" aria-hidden="true"></i> ข้อมูลสูตรคำนวนราคาหน้าปั้ม
                  </li>
                </ol>
                <p></p>
                <div class="row">
                    <div class="col-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">สาขา</span>
                          </div>
                          <asp:label class="form-control" ID="lblBranch" runat="server"></asp:label>
                        </div>                        
                    </div>
                    <div class="col-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">วันที่เริ่มใช้</span>
                          </div>
                          <asp:textbox style="background-color:yellow" class="form-control" ID="txtbegindate" runat="server"></asp:textbox>
                        </div>                        
                    </div>
                    <div class="col-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">วันที่สิ้นสุด</span>
                          </div>
                          <asp:label class="form-control" ID="lblenddate" runat="server"></asp:label>
                        </div>                        
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">ผู้บันทึก</span>
                          </div>
                          <asp:label class="form-control" ID="lblcreateby" runat="server"></asp:label>
                        </div>                        
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">วันที่บันทึก</span>
                          </div>
                          <asp:label class="form-control" ID="lblcreatedate" runat="server"></asp:label>
                        </div>                        
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">ส่วนต่าง สนพ</span>
                          </div>
                          <asp:textbox style="background-color:yellow" class="form-control" ID="txtdiffbkk" runat="server"></asp:textbox>
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">ภาษี</span>
                          </div>
                          <asp:textbox style="background-color:yellow" class="form-control" ID="txttax" runat="server"></asp:textbox>
                        </div>
                    </div>

                </div>
                <br />
                <div class="card"><!-- begin sale by meter -->
                    <div class="card-header" style="background-color:royalblue;color:white">
                            ส่วนเพิ่ม / ลด ราคา ตามพื้นที่ใกล้เคียง
                    </div> 
                    <br />
                    <div class="row">
                        <div class="col-3">
                            <div class="input-group sm-3">
                              <div class="input-group-prepend">
                                <span class="input-group-text">B7</span>
                              </div>
                              <asp:textbox style="background-color:yellow" class="form-control" ID="txtb7" runat="server"></asp:textbox>
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="input-group sm-3">
                              <div class="input-group-prepend">
                                <span class="input-group-text">B10</span>
                              </div>
                              <asp:textbox style="background-color:yellow" class="form-control" ID="txtb10" runat="server"></asp:textbox>
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="input-group sm-3">
                              <div class="input-group-prepend">
                                <span class="input-group-text">Gas 91</span>
                              </div>
                              <asp:textbox style="background-color:yellow" class="form-control" ID="txtg91" runat="server"></asp:textbox>
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="input-group sm-3">
                              <div class="input-group-prepend">
                                <span class="input-group-text">Gas 95</span>
                              </div>
                              <asp:textbox style="background-color:yellow" class="form-control" ID="txtg95" runat="server"></asp:textbox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-3">
                            <div class="input-group sm-3">
                              <div class="input-group-prepend">
                                <span class="input-group-text">E20</span>
                              </div>
                              <asp:textbox style="background-color:yellow" class="form-control" ID="txte20" runat="server"></asp:textbox>
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="input-group sm-3">
                              <div class="input-group-prepend">
                                <span class="input-group-text">PADO</span>
                              </div>
                              <asp:textbox style="background-color:yellow" class="form-control" ID="txtpado" runat="server"></asp:textbox>
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="input-group sm-3">
                              <div class="input-group-prepend">
                                <span class="input-group-text">PG95</span>
                              </div>
                              <asp:textbox style="background-color:yellow" class="form-control" ID="txtpg95" runat="server"></asp:textbox>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-12">
                            <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text="บันทึก" />&nbsp;   
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
            format:'d/m/Y'
        });
    </script>

</asp:Content>
