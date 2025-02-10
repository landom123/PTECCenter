<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="projectinfo.aspx.vb" Inherits="PTECCENTER.projectinfo" EnableEventValidation = "false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">    
        <style>
        body {
            background-color: #f0f2f5;
        }

        .col-form-label {
            text-align: right;
        }


        @media only screen and (max-width: 992px) {
            .col-form-label {
                text-align: left;
            }
        }


        .icon__status {
            background-color: #bfc2c4;
        
        }
        .name__status {
            text-align:center;
            font-size: .7rem;
        }
        .group__status .past {
            background-color: #0078d4;
        }
        .group__status .now {
            background-color: #00ff27;
        }
        .group__status .end {
            background-color: #dc3545;
        }
        /*####################### CSS FROM ATTATCH ########################*/
        .attatchItems-link-btndelete .deletedetail {
            font-size: .7rem
        }
        /*####################### END CSS FROM ATTATCH ########################*/
    </style>
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
                           <a href="projectlist.aspx"><i class="fa fa-tasks" aria-hidden="true"></i></a> ข้อมูลโครงการ
                  </li>
                </ol>
                <p></p>
                    <div class="row">
                        <div class="col-12 mb-3">

                            <div class="group__status row" >
                                <div class="col">

                                <div class="icon__status past" id="stGSM" style="margin-top: 6.5px; margin-left: auto; margin-right: auto; width: 15px !important; height: 15px !important; border-radius: 50%; margin-bottom: 0.15rem !important;">
                                </div>
                                    <div class="name__status" >
                                        บันทึก
                                    </div>
                                </div>

                                <div class="col">

                                <div class="icon__status now" id="stGSM" style="margin-top: 6.5px; margin-left: auto; margin-right: auto; width: 15px !important; height: 15px !important; border-radius: 50%; margin-bottom: 0.15rem !important;">
                                </div>
                                    <div class="name__status" >
                                        ตรวจสอบ
                                    </div>
                                </div>

                                <div class="col">

                                    <div class="icon__status " id="stGSM" style="margin-top: 6.5px; margin-left: auto; margin-right: auto; width: 15px !important; height: 15px !important; border-radius: 50%; margin-bottom: 0.15rem !important;">
                                    </div>
                                    <div class="name__status" >
                                        อนุมัติ
                                    </div>
                                </div>

                                <div class="col">

                                    <div class="icon__status " id="stGSM" style="margin-top: 6.5px; margin-left: auto; margin-right: auto; width: 15px !important; height: 15px !important; border-radius: 50%; margin-bottom: 0.15rem !important;">
                                    </div>
                                    <div class="name__status" >
                                        ทำสัญญา
                                    </div>
                                </div>

                            </div>                            
                        </div>
                    </div>
                <div class="row">
                    <div class="col-6">
                        <asp:Button ID="btnNewProject" class="btn btn-sm  btn-primary" runat="server" Text=" New " />
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text=" Save " />
                        <asp:Button ID="btnConfirm" class="btn btn-sm  btn-success" runat="server" Text=" Confirm " OnClientClick="return confirm_data();" />
                        <asp:Button ID="btnCancel" class="btn btn-sm  btn-danger" runat="server" Text=" Cancel " OnClientClick="return cancel_data();"/>
                    </div>
                    <div class="col-6 text-right">
                        <asp:Button ID="btnChange" class="btn btn-sm  btn-primary" runat="server" Text=" Change " />
                        <asp:Button ID="btnApprove" class="btn btn-sm  btn-success" runat="server" Text=" Approve " />
                    </div>
                </div>

                 <%--<hr style="height:2px;border-width:0;color:gray;background-color:gray">--%>
                <div class="card-body">
                        <div class="row">
                            <div class="col-2">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                    <span class="input-group-text">วันที่</span>
                                    </div>
                                    <asp:TextBox class="form-control" ID="txtBegindate" style="background-color:white" runat="server"></asp:TextBox>    

                                </div>
                            </div>
                            <div class="col-2">
                                <div class="input-group sm-3">
                                    <div class="input-group-prepend">
                                    <span class="input-group-text">ถึงวันที่</span>
                                    </div>
                                    <asp:TextBox class="form-control" ID="txtEnddate" style="background-color:white" runat="server"></asp:TextBox>    

                                </div>
                            </div>
                            <%--<div class="col-3">--%>
                                <asp:Button ID="btnFind" class="btn btn-sm  btn-success" runat="server" Text=" Find " />
                            <%--</div>--%>
                        </div>
                    <br />
                        <div class="row">
                            <div class="col-12">
                                 <div class="table-responsive">
   
                                    <asp:GridView ID="gvRequest"  
                                        class="table table-striped table-bordered" 
                                        AllowSorting="True" 
                                        allowpaging="false"
                                        AutoGenerateColumns="false" 
                                        emptydatatext=" test ไม่มีข้อมูล " 
                                        OnRowDataBound="OnRowDataBound"
                                         OnSelectedIndexChanged="OnSelectedIndexChanged"
                                        runat="server" CssClass="table table-striped">
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                        <AlternatingRowStyle BackColor="#CCCCFF" />

                                          <Columns>	                                  
                                              <asp:BoundField DataField="DocuNo" HeaderText="เลขที่เอกสาร"  />	                                 
                                              <asp:BoundField DataField="Branch" HeaderText="สาขา"  />
                                              <asp:BoundField DataField="AgType" HeaderText="ประเภทสัญญา"  />
	                                          <asp:BoundField DataField="StatusName" HeaderText="สถานะ"  />
	                                          <asp:BoundField DataField="Begindate" HeaderText="วันที่เริ่ม"  />
	                                          <asp:BoundField DataField="EndDate" HeaderText="สิ้นสุดวันที่"  />
                                              <asp:BoundField DataField="CustName" HeaderText="คู่สัญญา"  />
	                                          <asp:BoundField DataField="CreateDate" HeaderText="วันที่ขอ"  />
                                              <asp:BoundField DataField="CreateBy" HeaderText="ผู้ขอ"  />    
                                              <asp:BoundField DataField="ID" HeaderText="ID"  />
                                        </Columns>
                                  </asp:GridView>
                                               
                                </div><!-- end Table คู่สัญญา-->   
                            </div>
                      

                        </div>
                </div>
                 <hr style="height:2px;border-width:0;color:gray;background-color:gray">

    <p></p>    
    <div class="row">
        <div class="col-4">
            <div class="input-group sm-3">
                <div class="input-group-prepend">
                <span class="input-group-text">เลขที่โครงการ</span>
                </div>
                <asp:TextBox class="form-control" ID="txtprojectno" placeholder="please save first" ReadOnly="true" runat="server" ></asp:TextBox>    

            </div>
        </div>
        <div class="col-4">
            <div class="input-group sm-3">
                <div class="input-group-prepend">
                <span class="input-group-text">รหัสสาขา</span>
                </div>
                <asp:TextBox class="form-control" ID="txtbranch" placeholder="ตัวอย่าง 116" runat="server"></asp:TextBox>    

            </div>
        </div>
        <div class="col-4" style="text-align:center" >
                <asp:label class="form-control" ID="lblStatus"  runat="server"></asp:label>    
        </div>
    </div><br />
    <div class="row">
        <div class="col-4">
            <div class="input-group sm-3">
                <div class="input-group-prepend">
                <span class="input-group-text">ประมาณการยอดขาย (ลิตร)</span>
                </div>
                <asp:TextBox class="form-control" ID="txtSaleVolume"  runat="server" ></asp:TextBox>    
            </div>
        </div>
        <div class="col-8">
            <div class="input-group sm-3">
                <div class="input-group-prepend">
                <span class="input-group-text">Note</span>
                </div>
                <asp:TextBox class="form-control" ID="txtRemark"  TextMode="MultiLine" runat="server"></asp:TextBox>    
                <asp:TextBox  style="display:none" class="form-control" ID="txtDocID" runat="server" ></asp:TextBox>
            </div>
        </div>
    </div>
                          <div class="card-body">
<%--                    <div class="col-12 text-right">
                            <asp:Button ID="btnselectall" class="btn btn-sm  btn-success" runat="server" Text="Select All" />&nbsp;  
                            <asp:Button ID="btnunselect" class="btn btn-sm  btn-danger" runat="server" Text="Un Select All" />  
                    </div>--%>

                              <div class="table-responsive">
                                   <div class="row">
                                        <div class="col-8" style="font-size:large;">
                                            รายการสัญญา                                           
                                        </div>
                                        <div class="col-4" style="text-align:right">
                                            <asp:Button ID="btnNewAgree" class="btn btn-sm  btn-primary" runat="server" Text=" + เพิ่มสัญญา" />
                                        </div>
                                    </div>
                                <asp:GridView ID="gvData"  
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    emptydatatext=" ไม่มีข้อมูล " 
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#CCCCFF" />

                                      <Columns>
	                                    <asp:TemplateField HeaderText="เลขที่สัญญาในระบบ">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblagno" runat="server" Text='<%#Eval("agno")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="เลขที่เอกสารสัญญา">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblLawContractNo" runat="server" Text='<%#Eval("lawcontractno")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Date">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblagdate" runat="server" Text='<%#Eval("agdate")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="Type">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblagtype" runat="server" Text='<%#Eval("agtype")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>                          
                                                <asp:HyperLink ID="HyperLink" runat="server" NavigateUrl='<%#Eval("link")%>' Text="" data-toggle="tooltip" data-placement="right" title="View Detail"><i class="fa fa-eye" aria-hidden="true"></i></asp:HyperLink>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                              </asp:GridView>
                                               
                            </div>
                          </div><!-- end card-body-->

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
        jQuery('[id$=txtProjectDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format:'d/m/Y'
        });
    </script>


        <script type="text/javascript">

            jQuery('[id$=txtBegindate]').datetimepicker({

                startDate: '+1971/05/01',//or 1986/12/08

                lang: 'th',// แสดงภาษาไทย
                yearOffset: 543,// ใช้ปี พ.ศ. บวก 543 เพิ่มเข้าไปในปี ค.ศ  
                timepicker: false,
                scrollInput: false,
                format: 'd/m/Y'
            });
        </script>

        <script type="text/javascript">

            jQuery('[id$=txtEnddate]').datetimepicker({

                startDate: '+1971/05/01',//or 1986/12/08

                lang: 'th',// แสดงภาษาไทย
                yearOffset: 543,// ใช้ปี พ.ศ. บวก 543 เพิ่มเข้าไปในปี ค.ศ  
                timepicker: false,
                scrollInput: false,
                format: 'd/m/Y'
            });
        </script>
    
    <script type="text/javascript">
        function alertSuccess() {
            Swal.fire(
                'สำเร็จ',
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


    <script type="text/javascript" language="javascript"> 
        function confirm_data() {
            if (confirm("Confirm Data (yes/no) ?") == true)
                return true;
            else
                return false;
        }
    </script>

    <script type="text/javascript" language="javascript"> 
        function cancel_data() {
            if (confirm("Cancel Data (yes/no) ?") == true)
                return true;
            else
                return false;
        }
    </script>

</asp:Content>
