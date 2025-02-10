<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="EssoLoadConfirm.aspx.vb" Inherits="PTECCENTER.EssoLoadConfirm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
          <!-- datetimepicker-->

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
                      <a href="JobsList.aspx" class="btn btn-sm btn-danger">
                           <i class="fa fa-tasks" aria-hidden="true"></i></a> เปลี่ยนแปลงการส่งน้ำมัน
                  </li>
                </ol>


                <div class="row">
                    <div class="col-12">
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-primary" runat="server" Text="New" />&nbsp;
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text="Save" />  &nbsp;              
                        <asp:Button ID="btnConfirm" class="btn btn-sm  btn-secondary" runat="server" Text="Confirm" />  &nbsp;   
                        <asp:Button ID="btnPrint" class="btn btn-sm  btn-warning" runat="server" Text="Print" />&nbsp;
                        <Button <% If Session("status") = "new" Or Session("status") = "cancel" Then %> disabled <% End if %> type="button" class="btn btn-sm  btn-danger"  OnClick="chkCancel('../ops/jobsCancel.aspx?jobno=<% =Session("jobno") %>')">Cancel</Button>   
                    </div>
                </div>
                <p></p>
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">เลขที่เอกสาร</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtJobno" runat="server" ReadOnly="true"></asp:TextBox>
                          <div class="input-group-append">
                            <button type="button" class="btn btn-sm  btn-secondary"  onclick="find('../OPS/jobs.aspx?jobno=','ระบุเลขที่ OPS')" >Find</button>
                          </div>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">วันที่สร้างรายการ</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtCreateDate" runat="server" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">ผู้สร้างรายการ</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtCreateBy" runat="server" ReadOnly="True"></asp:TextBox>
                        </div>                                                
                    </div>
                </div>
                <p></p>
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">วันที่แจ้ง</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtDocDate" runat="server" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">ผู้แจ้ง</span>
                          </div>
                            <asp:DropDownList class="form-control" ID="cboOwner" runat="server" readonly="true"></asp:DropDownList>
                        </div>                                                
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">สถานะ</span>
                          </div>
                          <asp:TextBox class="btn btn-danger" ID="txtStatus" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>                                                
                    </div>
                </div>
                <p></p>
                <div class="row">
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">สาขา</span>
                              <asp:DropDownList class="form-control" ID="cboBranch" AutoPostBack="True" 
                                  runat="server" >
                              </asp:DropDownList>
                          </div>                          
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">ฝ่าย</span>
                              <asp:DropDownList class="form-control"  ID="cboDepartment" AutoPostBack="True" 
                                  runat="server" >
                              </asp:DropDownList>
                          </div>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">แผนก</span>
                              <asp:DropDownList class="form-control"  ID="cboSection" runat="server">
                              </asp:DropDownList>
                          </div>
                        </div>                                                
                    </div>
                </div>

                        <!-- DataTables Example -->
                <br />
                        <div class="card" >
                          <div class="card-header" style="background-color:navy;color:white">
                            <i class="fas fa-table"></i>
                            รายละเอียดงาน
                          </div>
                          <div id="jobdetail" class="card-body">



                              <%--begin item--%>

                            <div class="table-responsive">
                                
                                    <% For i = 0 To detailtable.Rows.Count - 1
                                            %>
<%-- begin detail row--%>

                            <div class="row">
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                      <div class="input-group-prepend">
                                        <span class="input-group-text">ประเภทงาน</span>
                                        <span class="input-group-text" style="width:550px"><% =detailtable.Rows(i).Item("jobtype") %></span>
                                      </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                      <div class="input-group-prepend">
                                        <span class="input-group-text">รหัสทรัพย์สิน</span>
                                        <span class="input-group-text" style="width:550px"><% =detailtable.Rows(i).Item("assetcode") %></span>
                                      </div>                                      
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                      <span class="input-group-text" style="width:550px"><% =detailtable.Rows(i).Item("assetname") %></span>
                                    </div>
                                </div>
                            </div>
                              <br />
                            <div class="row">
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                      <div class="input-group-prepend">
                                        <span class="input-group-text">จำนวน</span>
                                        <span class="input-group-text" style="width:550px"><% =detailtable.Rows(i).Item("quantity") %></span>
                                      </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                      <div class="input-group-prepend">
                                        <span class="input-group-text">หน่วย</span>
                                        <span class="input-group-text" style="width:550px"><% =detailtable.Rows(i).Item("unit") %></span>
                                      </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                      <div class="input-group-prepend">
                                        <span class="input-group-text">ค่าใช้จ่าย (ประมาณ)</span>
                                        <span class="input-group-text" style="width:170px"><% =detailtable.Rows(i).Item("cost") %></span>
                                      </div>
                                    </div>
                                </div>
                            </div>
                             <br />
                            <div class="row">
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                      <div class="input-group-prepend">
                                        <span class="input-group-text">Supplier</span>
                                        <span class="input-group-text" style="width:550px"><% =detailtable.Rows(i).Item("supplier") %></span>
                                      </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                      <div class="input-group-prepend">
                                        <span class="input-group-text">ความเร่งด่วน</span>
                                        <span class="input-group-text" style="width:550px"><% =detailtable.Rows(i).Item("policy") %></span>
                                      </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                      <div class="input-group-prepend">
                                        <span class="input-group-text">วันที่ต้องการ</span>
                                        <span class="input-group-text" style="width:200px"><% =detailtable.Rows(i).Item("requestdate") %></span>
                                      </div>
                                    </div>
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-8">
                                    <div class="input-group sm-3">
                                      <div class="input-group-prepend">
                                        <span class="input-group-text">รายละเอียดงาน</span>
 <%--                                       <span class="input-group-text" style="width:550px"><% =detailtable.Rows(i).Item("details") %></span>--%>
                                          <label for="detail"><% =detailtable.Rows(i).Item("details") %></label>
                                      </div>                                       
                                    </div>
                                </div>
                                <div class="col-4">
                                    <% If  objStatus="confirm" Then %>
                                                    <button type="button" class="btn btn-sm  btn-primary"  
                                                        onclick="location.href='../OPS/jobs_followup.aspx?jobno=<% =detailtable.Rows(i).Item("jobno") %>&jobdetailid=<% =detailtable.Rows(i).Item("jobdetailid") %>'" >Followup</button>&nbsp;
                                    <span class="btn btn-sm  btn-danger" >สถานะงาน : <% =detailtable.Rows(i).Item("followup_status") %></span> 
                                    <% End if %>
                                    <% If objStatus="edit" and owner=1 Then %>
                                    <button type="button" class="btn btn-sm  btn-danger"  
                                        onclick="chkDel('../OPS/jobsdetail_delete.aspx?jobno=<% =detailtable.Rows(i).Item("jobno") %>&jobdetailid=<% =detailtable.Rows(i).Item("jobdetailid") %>')" >Delete</button>
                                    <% End if %>
                                    
                                </div>
                            </div>

<%-- end detail row--%>
                            <hr style="height:2px;border-width:0;color:gray;background-color:gray"/>
                                    <% Next i %>

<%-- begin free detail row--%>
            <% If objStatus="new" Or objStatus = "edit" Then %>
                            <div class="row">
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                      <div class="input-group-prepend">
                                        <span class="input-group-text">ประเภทงาน</span>
                                          <asp:DropDownList ID="cboJobType" class="form-control"  runat="server">
                                          </asp:DropDownList>
                                      </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                      <div class="input-group-prepend">
                                        <span class="input-group-text">รหัสทรัพย์สิน</span>
                                        <asp:TextBox class="form-control" ID="txtAssetCode" runat="server" ></asp:TextBox>
                                      </div>
                                      <div class="input-group-append">
                                        <asp:Button ID="btnFind" class="btn btn-sm  btn-secondary" runat="server" Text="Find" />
                                      </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                      <asp:TextBox class="form-control" ID="txtAssetName" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                              <br />
                            <div class="row">
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                      <div class="input-group-prepend">
                                        <span class="input-group-text">จำนวน</span>
                                        <asp:TextBox class="form-control" ID="txtQuantity" runat="server" ></asp:TextBox>
                                      </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                      <div class="input-group-prepend">
                                          <span class="input-group-text">หน่วย</span>
                                          <asp:DropDownList ID="cboUnit" class="form-control"  runat="server">
                                          </asp:DropDownList>
                                      </div>
                                    </div>
                                </div>
                                <div class="col-4" >
                                    <div class="input-group sm-3">
                                      <div class="input-group-prepend">
                                        <span class="input-group-text">ค่าใช้จ่าย (ประมาณ)</span>
                                        <asp:TextBox class="form-control" style="width:100px" ID="txtCost" runat="server" ></asp:TextBox>
                                      </div>                                      
                                    </div>
                                </div>
                            </div>
                             <br />
                            <div class="row">
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                      <div class="input-group-prepend">
                                        <span class="input-group-text">Supplier</span>
                                          <asp:DropDownList class="form-control"  ID="cboSupplier" 
                                              runat="server" >
                                          </asp:DropDownList>
                                      </div>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                      <div class="input-group-prepend">
                                        <span class="input-group-text">ความเร่งด่วน</span>
                                          <asp:DropDownList class="form-control"  ID="cboPolicy" 
                                              runat="server" >
                                          </asp:DropDownList>
                                      </div>
                                    </div>
                                </div>
                                <div class="col-4" ">
                                    <div class="input-group sm-3">
                                      <div class="input-group-prepend">
                                        <span class="input-group-text">วันที่ต้องการ</span>
                                            <asp:TextBox class="form-control" ID="txtDueDate" runat="server" style="width:200px"></asp:TextBox>
                                      </div>
                                    </div>
                                </div>
                            </div>
                            <br />

                            <div class="row">
                                <div class="col-8">
                                    <div class="input-group sm-3">
                                      <div class="input-group-prepend">
                                        <span class="input-group-text">รายละเอียดงาน</span>
                                      </div>
                                      <asp:TextBox class="form-control" ID="txtJobDetail" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                
                                <div class="col-4">
                                    <asp:Button ID="btnAddDetail" class="btn btn-sm  btn-primary" runat="server" Text=" + "/>&nbsp;
                                </div>

                            </div>
            <% End if %>
<%-- end free detail row--%>

                            </div>

                             <%-- end item--%>

                          </div><!-- end jobdetail -->
                  
                        </div>
                        <!-- end display job detail -->


<%--                    </div>
                </div>--%>
            </div>            <!-- /.container-fluid -->
            <!-- Sticky Footer -->

        </div>        <!-- end content-wrapper -->


        <!-- end เนื้อหาเว็บ -->

    </div>
    <!-- /#wrapper -->
 

</asp:Content>
