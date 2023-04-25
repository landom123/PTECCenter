<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="requestcontract.aspx.vb" Inherits="PTECCENTER.requestcontract" EnableEventValidation = "false" %>

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
                           <a href="client_list.aspx"><i class="fa fa-tasks" aria-hidden="true"></i></a> ร้องขอสัญญาใหม่
                  </li>
                </ol>
                <p></p>
                <div class="row">
                    <div class="col-6">
                        <asp:Button ID="btnNew" class="btn btn-sm  btn-primary" runat="server" Text=" New " />
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success" runat="server" Text=" Save " />
                        <asp:Button ID="btnDel" class="btn btn-sm  btn-success" runat="server" Text=" Delete " />
                    </div>
                    <div class="col-6" style="text-align:right">
                        <asp:Button ID="BtnContract" class="btn btn-sm  btn-success" runat="server" Text=" กลับ สัญญา " />
                    </div>
                </div>
              
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
   
                                    <asp:GridView ID="gvData"  
                                        class="table table-striped table-bordered" 
                                        AllowSorting="True" 
                                        allowpaging="false"
                                        AutoGenerateColumns="false" 
                                        emptydatatext=" ไม่มีข้อมูล " 
                                        OnRowDataBound="OnRowDataBound"
                                         OnSelectedIndexChanged="OnSelectedIndexChanged"
                                        runat="server" CssClass="table table-striped">
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                        <AlternatingRowStyle BackColor="#CCCCFF" />

                                          <Columns>	                                  
                                              <asp:BoundField DataField="DocuNo" HeaderText="เลขที่เอกสาร"  />
	                                        <asp:TemplateField HeaderText="สาขา">
		                                        <ItemTemplate>
			                                        <asp:Label id="lblBranch" runat="server" Text='<%#Eval("Branch")%>'></asp:Label>
		                                        </ItemTemplate>
	                                        </asp:TemplateField>
	                                        <asp:TemplateField HeaderText="ประเภทสัญญา">
		                                        <ItemTemplate>
			                                        <asp:Label id="lblContactType" runat="server" Text='<%#Eval("AgType")%>'></asp:Label>
		                                        </ItemTemplate>
	                                        </asp:TemplateField>
	                                        <asp:TemplateField HeaderText="สถานะ">
		                                        <ItemTemplate>
			                                        <asp:Label id="lblStatusName" runat="server" Text='<%#Eval("StatusName")%>'></asp:Label>
		                                        </ItemTemplate>
	                                        </asp:TemplateField>
	                                        <asp:TemplateField HeaderText="วันที่เริ่ม">
		                                        <ItemTemplate>
			                                        <asp:Label id="lblBegindate" runat="server" Text='<%#Eval("Begindate")%>'></asp:Label>
		                                        </ItemTemplate>
	                                        </asp:TemplateField>
	                                        <asp:TemplateField HeaderText="สิ้นสุดวันที่">
		                                        <ItemTemplate>
			                                        <asp:Label id="lblEndDate" runat="server" Text='<%#Eval("EndDate")%>'></asp:Label>
		                                        </ItemTemplate>
	                                        </asp:TemplateField>
	                                        <asp:TemplateField HeaderText="คู่สัญญา">
		                                        <ItemTemplate>
			                                        <asp:Label id="lblCustName" runat="server" Text='<%#Eval("CustName")%>'></asp:Label>
		                                        </ItemTemplate>
	                                        </asp:TemplateField>
	                                        <asp:TemplateField HeaderText="วันที่ขอ">
		                                        <ItemTemplate>
			                                        <asp:Label id="lblCreateDate" runat="server" Text='<%#Eval("CreateDate")%>'></asp:Label>
		                                        </ItemTemplate>
	                                        </asp:TemplateField>
	                                        <asp:TemplateField HeaderText="ผู้ขอ">
		                                        <ItemTemplate>
			                                        <asp:Label id="lblCreateBy" runat="server" Text='<%#Eval("CreateBy")%>'></asp:Label>
		                                        </ItemTemplate>
	                                        </asp:TemplateField>
                                               <asp:BoundField DataField="ID" HeaderText="ID"  />
	
 <%--                                           <asp:TemplateField HeaderText="">
                                                <ItemTemplate>                          
                                                    <asp:HyperLink ID="HyperLink" runat="server" NavigateUrl='<%#Eval("link")%>' Text="" data-toggle="tooltip" data-placement="right" title="View Detail"><i class="fa fa-eye" aria-hidden="true"></i></asp:HyperLink>

                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                  </asp:GridView>
                                               
                                </div><!-- end Table คู่สัญญา-->   
                            </div>
                      

                        </div>
                </div>
                 <hr style="height:2px;border-width:0;color:gray;background-color:gray">
                <div class="card-body">

                        <div class="row">
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                        <span class="input-group-text">เลขที่เอกสาร</span>
                                        </div>
                                        <asp:TextBox class="form-control" ID="txtdocuno" placeholder="Document No" ReadOnly="true" runat="server" ></asp:TextBox>    

                                    </div>
                                </div>
                                <div class="col-4">

                                </div>
                                <div class="col-4">
                                    <div class="input-group sm-3">
                                        <div class="input-group-prepend">
                                        <span class="input-group-text">สถานะ</span>
                                        </div>
                                        <asp:Label class="form-control" ID="lblStatus" style="background-color:darkgreen;color:white" runat="server" ></asp:Label>    

                                    </div>
                                </div>
                            </div>

                            <%-- input area --%>
                              <div class="card-body">
                      
                                    <div class="row">
                                            <div class="col-3">
                                                <div class="input-group sm-3">
                                                    <div class="input-group-prepend">
                                                    <span class="input-group-text">รหัสสาขา</span>
                                                    </div>
                                                        <asp:DropDownList ID="cboBranch" class="form-control" runat="server" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-3">
                                                <div class="input-group sm-3">
                                                    <div class="input-group-prepend">
                                                    <span class="input-group-text">ประเภทสัญญา</span>
                                                    </div>
                                                    <asp:DropDownList class="form-control" ID="cboContractType"  runat="server" ></asp:DropDownList>    
                                                </div>
                                            </div>

                                             <div class="col-3">
                                                <div class="input-group sm-3">
                                                    <div class="input-group-prepend">
                                                    <span class="input-group-text">วันที่เริ่มสัญญา</span>
                                                    </div>
                                                    <asp:TextBox class="form-control" ID="txtContractBeginDate" style="background-color:white" runat="server"></asp:TextBox>    

                                                </div>
                                            </div>

                                             <div class="col-3">
                                                <div class="input-group sm-3">
                                                    <div class="input-group-prepend">
                                                    <span class="input-group-text">วันที่สิ้นสุดสัญญา</span>
                                                    </div>
                                                    <asp:TextBox class="form-control" ID="txtContractEndDate" style="background-color:white" runat="server"></asp:TextBox>    

                                                </div>
                                            </div>


                                    </div>

                                    <div class="row" style="padding-top: 1rem;">
                                        <div class="col-md-3">
                                                ชื่อ-นามสกุล
                                            <div class="input-group sm-">
                                                <asp:TextBox class="form-control" ID="txtName" runat="server" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                                เลขที่่บัตรประจำตัวประชาชน
                                            <div class="input-group sm-3">
                                                <asp:TextBox class="form-control" ID="txtCardID" runat="server" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                                เพศ
                                            <div class="input-group sm-3">
                                                <asp:dropdownlist class="form-control" ID="cboSex" runat="server" ></asp:dropdownlist>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="padding-top: 1rem;">
                                        <div class="col-md-3">
                                                ชื่อบริษัท
                                            <div class="input-group sm-">
                                                <asp:TextBox class="form-control" ID="txtCompany" runat="server" ></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                                Mobile Phone
                                            <div class="input-group sm-3">
                                                <asp:TextBox class="form-control" ID="txtMobile" runat="server" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top: 1rem;">
                                        <div class="col-md-3">
                                                Tel
                                            <div class="input-group sm-">
                                                <asp:TextBox class="form-control" ID="txtTel" runat="server" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                                Email
                                            <div class="input-group sm-3">
                                                <asp:TextBox class="form-control" ID="txtEmail" runat="server" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                                Line
                                            <div class="input-group sm-3">
                                                <asp:TextBox class="form-control" ID="txtLine" runat="server" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="padding-top: 1rem;">
                                            <div class="col-3">
                                                <div class="input-group sm-3">
                                                    <div class="input-group-prepend">
                                                    <span class="input-group-text">สถานะ</span>
                                                    </div>
                                                    <asp:DropDownList class="form-control" ID="cboStatus"  runat="server" ></asp:DropDownList>    
                                                </div>
                                            </div>
                                    </div>

                                    <hr style="height:2px;border-width:0;color:gray;background-color:gray">
                                    <div class="row" style="padding-top: 1rem;">
                                        <div class="col-md-12">
                                                ที่อยู่
                                            <div class="input-group sm-3">
                                                <asp:TextBox class="form-control" ID="txtAddress" runat="server" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top: 1rem;">
                                        <div class="col-md-3">
                                                ตำบล/แขวง
                                            <div class="input-group sm-3">
                                                <asp:TextBox class="form-control" ID="txtSubDistrict" runat="server" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                                อำเภอ/เขต
                                            <div class="input-group sm-3">
                                                <asp:TextBox class="form-control" ID="txtDistrict" runat="server" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                                จังหวัด
                                            <div class="input-group sm-3">
                                                <asp:TextBox class="form-control" ID="txtProvince" runat="server" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top: 1rem;">
                                        <div class="col-md-3">
                                                รหัสไปรษณีย์
                                            <div class="input-group sm-3">
                                                <asp:TextBox class="form-control" ID="txtPostcode" runat="server" ></asp:TextBox>
                                            </div>
                                             <asp:TextBox  style="display:none" class="form-control" ID="txtDocAction" runat="server" ></asp:TextBox>
                                             <asp:TextBox  style="display:none" class="form-control" ID="txtDocIDAction" runat="server" ></asp:TextBox>
                                        </div>
                                    </div>


                            </div>

                  </div>


            </div>            <!-- /.container-fluid -->
            <!-- Sticky Footer -->
             <hr style="height:2px;border-width:0;color:gray;background-color:gray">
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
        jQuery('[id$=txtBirthday]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08

            lang: 'th',// แสดงภาษาไทย
            yearOffset: 543,// ใช้ปี พ.ศ. บวก 543 เพิ่มเข้าไปในปี ค.ศ  
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
        jQuery('[id$=txtContractBeginDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08

            lang: 'th',// แสดงภาษาไทย
            yearOffset: 543,// ใช้ปี พ.ศ. บวก 543 เพิ่มเข้าไปในปี ค.ศ  
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>

    <script type="text/javascript">
        jQuery('[id$=txtContractEndDate]').datetimepicker({
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

</asp:Content>
