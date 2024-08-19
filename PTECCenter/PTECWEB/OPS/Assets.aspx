<%@ Page Title="" Language="vb" AutoEventWireup="false" Async="true" MasterPageFile="~/site.Master" CodeBehind="Assets.aspx.vb" Inherits="PTECCENTER.frmAssets" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

      <!-- datetimepicker-->
  <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper">

    <!-- #include virtual ="/include/menu.inc" --> <!-- add side menu -->
        <!-- hidden field -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">         
                <div class="row">
                    <div class="col-12 mb-3">
                        <button type="button" class="btn btn-sm  btn-primary"  onclick="chkNew()" >new</button> &nbsp;
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text="save" /> &nbsp;           
                        <asp:Button ID="btnSearch" class="btn btn-sm  btn-secondary" runat="server" Text="Search" />  &nbsp;   
                        <asp:Button ID="btnPrint" class="btn btn-sm  btn-warning" runat="server" Text="Print" />&nbsp;
                    </div>
                </div>
                
                <div class="row">
                    <div class="col-md-4 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">รหัสทรัพย์สิน</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtAssetCode" runat="server" ReadOnly="true"></asp:TextBox>
                          <div class="input-group-append">
                            <button type="button" class="btn btn-sm  btn-secondary"  onclick="find('../OPS/Assets.aspx?assetcode=','ระบุเลขที่ทรัพย์สิน')" >Find</button>
                          </div>
                        </div>
                    </div>
                    <div class="col-md-4 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">วันที่สร้างรายการ</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtCreateDate" runat="server" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">ผู้สร้างรายการ</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtCreateBy" runat="server" ReadOnly="True"></asp:TextBox>
                        </div>                                                
                    </div>
                </div>
                
                <div class="row">
                    <div class="col-md-4 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">ชื่อ</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtname" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">กลุ่ม</span>
                          </div>
                              <asp:DropDownList class="form-control" ID="cboAssetGroup" runat="server"                                   
                                  AutoPostBack="True" >
                              </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">ประเภท</span>
                          </div>
                              <asp:DropDownList class="form-control" ID="cboAssetType" runat="server">
                              </asp:DropDownList>
                        </div>
                    </div>
                </div>

                
                <div class="row">
                    <div class="col-md-4 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">Serial No.</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtSerialNo" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">ราคา</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtPrice" runat="server" ></asp:TextBox>
                          <asp:CompareValidator class="btn btn-sm  btn-danger" runat="server" Operator="DataTypeCheck" Type="Double" 
                                ControlToValidate="txtPrice" ErrorMessage=" ใส่จำนวนเงิน" />
                        </div>                                                
                    </div>
                    <div class="col-md-4 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">รหัสทรัพย์สิน (บัญชี)</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtAssetAccCode" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                
                <div class="row">
                    <div class="col-md-4 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">Supplier</span>
                          </div>
                              <asp:DropDownList class="form-control" ID="cboSupplier" runat="server">
                              </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">เริ่มต้นรับประกัน</span>
                          </div>
                              <asp:TextBox class="form-control" ID="txtBeginDate" runat="server" >
                              </asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">สิ้นสุดรับประกัน</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtEndDate" runat="server"></asp:TextBox>
                        </div>                                                
                    </div>
                </div>
                
                <div class="row">
                    <div class="col-md-4 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">เลขที่ Invoice</span>
                          </div>
                            <asp:TextBox class="form-control" ID="txtInvoiceno" runat="server"  ></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                              <span class="input-group-text">วันที่ Invoice</span>
                          </div>
                            <asp:TextBox class="form-control" ID="txtInvoiceDate" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                
                <div class="row">
                    <div class="col-md-4 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">สาขา</span>
                              <asp:DropDownList class="form-control" ID="cboBranch" runat="server"  AutoPostBack="True" >
                              </asp:DropDownList>
                          </div>
                        </div>
                    </div>
                    <div class="col-md-4 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">ฝ่าย</span>
                              <asp:DropDownList class="form-control" ID="cboDepartment" runat="server" 
                                  AutoPostBack="true" >
                              </asp:DropDownList>
                          </div>
                        </div>
                    </div>
                    <div class="col-md-4 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">แผนก</span>
                              <asp:DropDownList class="form-control" ID="cboSection" runat="server">
                              </asp:DropDownList>
                          </div>
                        </div>
                    </div>
                </div>
                
                <div class="row">
                    <div class="col-md-12 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">รายละเอียด (255 ตัวอักษร)</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtDetails" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </div>
                
                <div class="row">
                    <div class="card" >
                        <div class="card-header">
                              รายละเอียดทรัพย์สิน&nbsp;
                              <img id="ShowID" src="../icon/browser-download.png" style="width:20px" onclick="show('assetdetail')" />
                              <img id="HideID" src="../icon/browser-upload.png" style="width:20px" onclick="hide('assetdetail')" />
                        </div> 
                          <div style="display:none" id="assetdetail" class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Button ID="btnSaveDetail" class="btn btn-sm  btn-success" runat="server" Text="Add Detail" />  &nbsp;              
                                </div>
                            </div>
                              <br />
                            <div class="row">
                                <div class="col-8">
                                    <div class="input-group sm-3">
                                      <div class="input-group-prepend">
                                        <span class="input-group-text">ประเภทรายละเอียด</span>
                                          <asp:DropDownList ID="cboAssetItemType" class="form-control"  runat="server">
                                          </asp:DropDownList>
                                      </div>
                                    </div>
                                </div>
                            </div>
                              <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="input-group sm-3">
                                      <div class="input-group-prepend">
                                        <span class="input-group-text">รายละเอียด</span>
                                      </div>
                                      <asp:TextBox class="form-control" ID="txtItemDetail" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                              
                          </div><!-- end jobdetail -->
                          <div class="card-body">
                            
                            <div class="table-responsive">
                              <table class="table table-bordered" id="dataTable">
                                <thead>
                                  <tr>
                                    <th style="width:10px;"></th>
                                    <th style="width:200px;">ประเภท</th>
                                    <th style="width:700px;">รายละเอียด</th>
                                  </tr>
                                </thead>
                                <tfoot>
                                  <tr>
                                    <th style="width:10px;"></th>
                                    <th style="width:200px;">ประเภท</th>
                                    <th style="width:700px;">รายละเอียด</th>                                    
                                  </tr>
                                </tfoot>
                                <tbody>

                                    <% For i = 0 To itemtypeTable.Rows.Count - 1
                                            %>
                                    <tr>
                                        <td>
                                            <img src="../icon/bin.png" style="width:20px" onclick="chkDel('../OPS/assetsitem_delete.aspx?assetcode=<% =Session("assetcode") %>&assetitemtyeid=<% =itemtypeTable.Rows(i).Item("itemtypeid") %>&detail=<% =itemtypeTable.Rows(i).Item("itemdetail") %>')" />
                                        </td>
                                        <td><% =itemtypeTable.Rows(i).Item("itemtype") %></td>
                                        <td><% =itemtypeTable.Rows(i).Item("itemdetail") %></td>
                                    </tr>
                                    <% Next i %>
                                </tbody>
                              </table>
                            </div>
                          </div>
                        <div class="card-footer small text-muted">Updated yesterday at 11:59 PM</div>
                    </div>
                </div>
            </div>            <!-- /.container-fluid -->
        </div><!-- end content-wrapper -->

        <!-- end เนื้อหาเว็บ -->

    <!-- Sticky Footer -->
    <footer class="sticky-footer d-none">
        <div class="container my-auto">
            <div class="copyright text-center my-auto">
            <span>Copyright © Your Website 2019</span>
            </div>
        </div>
    </footer>
    </div>
    <!-- /#wrapper -->
  <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>

    <script type="text/javascript">
        jQuery('[id$=txtBeginDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format:'d/m/yy'
        });
    </script>
    <script type="text/javascript">
        jQuery('[id$=txtEndDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format:'d/m/yy'
        });
    </script>
    <script type="text/javascript">
        jQuery('[id$=txtInvoiceDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format:'d/m/yy'
        });
    </script>
</asp:Content>
