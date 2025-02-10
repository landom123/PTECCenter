<%@ Page Title="" Language="vb" AutoEventWireup="false" EnableEventValidation = "false" Culture="th-TH"
    MasterPageFile="~/site.Master" CodeBehind="requesteditcontract.aspx.vb" Inherits="PTECCENTER.requesteditcontract" %>

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
                <ol class="breadcrumb"style="background-color:deeppink;color:white">
                  <li class="breadcrumb-item" >
                           <i class="fa fa-tasks" aria-hidden="true"></i>ขออนุมัติแก้ไขสัญญา
                  </li>
                </ol>
                <p></p>
                <div class="row">
                    <div class="col-8 ">
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text="รายการใหม่" />  
                        <asp:Button ID="btnReEdit" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text="บันทึก" />                  
                    </div>
                    <div class="col-4" style="text-align:right">
                        <asp:Button ID="btnBack" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text="กลับหน้าสัญญา" />  
                    </div>
                </div>

                          <div class="card-body">
                              <div class="row" style="padding-top: 0.2rem;">
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                        <span class="input-group-text">เลขที่ขอสัญญา</span>
                                        </div>
                                        <asp:TextBox class="form-control" ID="txtdocuno" placeholder="Document No" ReadOnly="true" runat="server" ></asp:TextBox>    

                                    </div>
                                </div>
                              </div>

                              <div class="row" style="padding-top: 0.2rem;">
                                   <div class="col-2">
                                        <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                 <span class="input-group-text">เลขที่สัญญา</span>
                                            </div>
                                            <asp:TextBox class="form-control" ID="txtContractno" runat="server"></asp:TextBox>
                                        </div>
                                   </div>

                                    <asp:Button ID="btnFind" class="btn btn-sm  btn-outline-info w-20 noEnterSubmit" runat="server" Text=" Find " />


                                  
                             </div>

                              <div class="row" style="padding-top: 0.2rem;">
                                  <div class="col-2">
                                      <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                 <span class="input-group-text">คู่สัญญา</span>
                                            </div>
                                            <asp:label class="form-control" ID="lblVendor" text="" runat="server" ></asp:label>
                                      </div>
                                  </div>
                                  <div class="col-3">
                                      <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                 <span class="input-group-text">วันที่สัญญา</span>
                                            </div>
                                            <asp:label class="form-control" ID="lblBeginDate" Text="" runat="server" ></asp:label>.
                                            <asp:label class="form-control" ID="lblEndDate" Text="" runat="server" ></asp:label>
                                      </div>
                                  </div>
                                  <div class="col-3">
                                      <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                 <span class="input-group-text">ประเภทสัญญา</span>
                                            </div>
                                            <asp:label class="form-control" ID="lblConTractType" Text="" runat="server" ></asp:label>.                                     
                                      </div>
                                  </div>
                                  <div class="col-3">
                                      <div class="input-group sm-3">
                                            <div class="input-group-prepend">
                                                 <span class="input-group-text">กลุ่มสัญญา</span>
                                            </div>
                                            <asp:label class="form-control" ID="lblContractGroup" Text="" runat="server" ></asp:label>.                                     
                                      </div>
                                  </div>
                              </div>

                              <div class="row" style="padding-top: 0.2rem;">
                                  <div class="col-6">
                                      <div class="input-group sm-6">
                                            <div class="input-group-prepend">
                                                 <span class="input-group-text">รายละเอียดการขอแก้</span>
                                            </div>
                                            <asp:TextBox class="form-control" TextMode="MultiLine"  ID="txtDetail" runat="server" ></asp:TextBox>
                                      </div>
                                  </div>
                                 
                              </div>
                              <hr style="height:2px;border-width:0;color:gray;background-color:gray">

                              <div class="table-responsive">
                                <asp:GridView ID="gvData"
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    emptydatatext=" ไม่มีข้อมูล " 
                                    OnRowDataBound="OnRowDataBoundRE"
                                    OnSelectedIndexChanged="OnSelectedIndexChanged_RE"
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#CCCCFF" />
                                     <Columns>
                                           <asp:BoundField DataField="ID" HeaderText="#" />
                                           <asp:BoundField DataField="DocNo" HeaderText="เลขที่เอกสาร" />
                                           <asp:BoundField DataField="ContractNo" HeaderText="เลขที่สัญญา" />
                                           <asp:BoundField DataField="Detail" HeaderText="รายละเอียด" />                                           
                                   </Columns>
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
