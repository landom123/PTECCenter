<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="gsmapprovededuct.aspx.vb" Inherits="PTECCENTER.gsmapprovededuct" %>

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
                           <i class="fa fa-tasks" aria-hidden="true"></i> อนุมัติ คำขอหักยอดขาย
                  </li>
                </ol>
                <p></p>
                <div class="row">
                    <div class="col-md-4 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">วันที่เริ่มต้น</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtbegindate" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">วันที่สิ้นสุด</span>
                          </div>
                          <asp:TextBox class="form-control" ID="txtenddate" runat="server"></asp:TextBox>

                        </div>
                    </div>
                    <div class="col-md-4 mb-3">
                        <div class="input-group sm-3">
                          <div class="input-group-prepend">
                            <span class="input-group-text">สถานะ</span>
                          </div>
                          <asp:dropdownlist class="form-control" ID="cbostatus" runat="server"></asp:dropdownlist>
                          &nbsp;<asp:Button ID="btnFind" class="btn btn-sm  btn-primary" runat="server" Text="Find" />&nbsp;
                        </div>
                    </div>
                </div>

                          <div class="card-body">
                              <div class="table-responsive">
                                <asp:GridView ID="gvData"  
                                    OnRowCommand="GridView1_RowCommand"
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    emptydatatext="No data available." 
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#CCCCFF" />

                                      <Columns>
	                                    <asp:TemplateField HeaderText="id">
                                            <itemstyle Width="50px" />
		                                    <ItemTemplate>
			                                    <asp:Label id="lblid" runat="server" Text='<%#Eval("id")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="สาขา">
                                            <itemstyle Width="50px" />
		                                    <ItemTemplate>
			                                    <asp:Label id="lblbranch" runat="server" Text='<%#Eval("branch_id")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="เลขที่เอกสาร">
                                            <itemstyle Width="100px" />
		                                    <ItemTemplate>
			                                    <asp:Label id="lbldocno" runat="server" Text='<%#Eval("document_no")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="วันที่เอกสาร">
                                            <itemstyle Width="100px" />
		                                    <ItemTemplate>
			                                    <asp:Label id="lbldocdate" runat="server" Text='<%#Eval("document_date")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="รายการ">
                                            <itemstyle Width="200px" />
		                                    <ItemTemplate>
			                                    <asp:Label id="lbldetail" runat="server" Text='<%#Eval("detail_use")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="วันที่ใช้" >
                                            <itemstyle Width="100px" />
		                                    <ItemTemplate>
			                                    <asp:label id="lbldateuse" runat="server" text='<%#Eval("date_use")%>' ></asp:label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนเงิน">
                                            <itemstyle Width="100px" />
                                            <ItemTemplate>          
                                                <asp:label id="lblamount" runat="server" text='<%#Eval("act_amount")%>' ></asp:label>
                                                <%--<asp:HyperLink ID="HyperLink" runat="server" NavigateUrl='<%#Eval("link")%>' Text="" data-toggle="tooltip" data-placement="right" title="View Detail"><i class="fa fa-eye" aria-hidden="true"></i></asp:HyperLink>--%>
                                                <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Eval("Send")%>' Text="" data-toggle="tooltip" data-placement="left" title="Send to D365"><i class="fa fa-paper-plane" aria-hidden="true"></i></asp:HyperLink>--%>
<%--                                                <asp:checkbox id="chk" runat="server" checked='<%#Eval("chk")%>'></asp:checkbox>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="สถานะ">
                                            <itemstyle Width="50px" />
                                            <ItemTemplate>          
                                                <asp:label id="lblCurStatus" runat="server" text='<%#Eval("approve_status")%>' ></asp:label>
                                                <%--<asp:HyperLink ID="HyperLink" runat="server" NavigateUrl='<%#Eval("link")%>' Text="" data-toggle="tooltip" data-placement="right" title="View Detail"><i class="fa fa-eye" aria-hidden="true"></i></asp:HyperLink>--%>
                                                <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Eval("Send")%>' Text="" data-toggle="tooltip" data-placement="left" title="Send to D365"><i class="fa fa-paper-plane" aria-hidden="true"></i></asp:HyperLink>--%>
<%--                                                <asp:checkbox id="chk" runat="server" checked='<%#Eval("chk")%>'></asp:checkbox>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>                                                         
                                                <asp:Button class="btn btn-sm  btn-primary" Text="อนุมัติ" enabled='<%# IIf(Eval("approve_status").ToString() = "Pending", True, False) %>' runat="server" CommandName="Approve" CommandArgument="<%# Container.DataItemIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>                                                         
                                                <asp:Button class="btn btn-sm  btn-danger" Text="ไม่อนุมัติ" enabled='<%# IIf(Eval("approve_status").ToString() = "Pending", True, False) %>' runat="server" CommandName="Reject" CommandArgument="<%# Container.DataItemIndex %>" />
                                                <br />เหตุผล <asp:textbox id="txtreason" runat="server"  text='<%#Eval("reason")%>' TextMode="MultiLine" enabled='<%# IIf(Eval("approve_status").ToString() = "Pending", True, False) %>'></asp:textbox>
<%--                                                <asp:Button ID="btnApprove" class="btn btn-sm  btn-primary" runat="server" Text="อนุมัติ" />
                                                <asp:Button ID="btnReject" class="btn btn-sm  btn-danger" runat="server" Text="ไม่อนุมัติ" />
                                                <br />เหตุผล <asp:textbox id="txtreason" runat="server"  TextMode="MultiLine"></asp:textbox>--%>
                                                <%--<asp:HyperLink ID="HyperLink" runat="server" NavigateUrl='<%#Eval("link")%>' Text="" data-toggle="tooltip" data-placement="right" title="View Detail"><i class="fa fa-eye" aria-hidden="true"></i></asp:HyperLink>--%>
                                                <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Eval("Send")%>' Text="" data-toggle="tooltip" data-placement="left" title="Send to D365"><i class="fa fa-paper-plane" aria-hidden="true"></i></asp:HyperLink>--%>
<%--                                                <asp:checkbox id="chk" runat="server" checked='<%#Eval("chk")%>'></asp:checkbox>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
        jQuery('[id$=txtbegindate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format:'d/m/Y'
        });
    </script>

    <script type="text/javascript">
        jQuery('[id$=txtenddate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>
</asp:Content>
