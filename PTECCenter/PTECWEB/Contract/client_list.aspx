<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="client_list.aspx.vb" Inherits="PTECCENTER.client_list" %>

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
                           <i class="fa fa-tasks" aria-hidden="true"></i> Test Form contract
                  </li>
                </ol>
                <p></p>

                          <div class="card-body">
<%--                    <div class="col-12 text-right">
                            <asp:Button ID="btnselectall" class="btn btn-sm  btn-success" runat="server" Text="Select All" />&nbsp;  
                            <asp:Button ID="btnunselect" class="btn btn-sm  btn-danger" runat="server" Text="Un Select All" />  
                    </div>--%>

                              <div class="table-responsive">
                                <asp:GridView ID="gvData"  
                                    OnRowCommand="GridView1_RowCommand"
                                    ShowFooter="true"
                                    class="table table-striped table-bordered" 
                                    AllowSorting="True" 
                                    allowpaging="false"
                                    AutoGenerateColumns="false" 
                                    emptydatatext="No data available." 
                                    ShowHeaderWhenEmpty="true"
                                    runat="server" CssClass="table table-striped">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="false" Font-size="Smaller" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#CCCCFF" />
                                      <Columns>
	                                    <asp:TemplateField HeaderText="ID">
		                                    <ItemTemplate>
			                                    <asp:Label id="lblID" runat="server" Text='<%#Eval("clientid")%>'></asp:Label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="รหัส">
		                                    <ItemTemplate>
			                                    <asp:label id="lblclientno" runat="server" Text='<%#Eval("clientno")%>'></asp:label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
	                                    <asp:TemplateField HeaderText="ชื่อ นามสกุล">
		                                    <ItemTemplate>
			                                    <asp:label id="lblname" runat="server" Text='<%#Eval("client")%>'></asp:label>
		                                    </ItemTemplate>
	                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>                          
                                                <%--<asp:Button ID="btnUpdate" class="btn btn-sm  " runat="server" Text="<i class='fas minus-circle' style='color:blue;font-size:20px'></i> "  CommandName="Save" CommandArgument="<%# Container.DataItemIndex %>"/>                                                --%>
                                                <asp:LinkButton runat="server" ID="btnView" Text="<i class='fas fa-eye' aria-hidden='false' style='color:green;font-size:20px'></i> " 
                                                ValidationGroup="edt"  ToolTip="แสดงรายการ"  CommandName="View" CommandArgument="<%# Container.DataItemIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                              </asp:GridView>
                                               
                            </div>
                          </div>

                         <div class="row" style="padding-top: 1rem;">
                            <div class="col-md-10">
                           
                                    <div style="margin-left: auto; margin-right: auto; text-align: center;">
                                        <asp:Label ID="Label1" runat="server" Text="สัญญาร่วมธุรกิจ" Font-Bold="true" Font-Size="X-Large" CssClass="StrongText"></asp:Label>  
                                         <BR />
                                        <asp:Label ID="Label2" runat="server" Text="(ที่ดินเปล่า)" Font-Bold="true"  CssClass="StrongText"></asp:Label>
                                    </div>
                                    <div style="margin-left: auto; margin-right: auto; text-align: right;">
                                        <asp:Label ID="Label3" runat="server" Text=" ทำที่ บริษัท  เพียวพลังงานไทย จำกัด" ></asp:Label>  
                                         <BR />
                                    </div>
                                    <div style="margin-left: 80%; margin-right: auto; text-align: right;" >
                                             <div class="input-group sm-3">
                                                วันที่  &nbsp
                                                <asp:TextBox class="form-control" ID="txtContractDate" style="background-color:white" runat="server"  ></asp:TextBox>  <BR /> 
                                             </div>                                                                              
                                    </div>
                                    <div style="margin-left: 1%; margin-right: auto; text-align: center;" >
                                        <asp:Label ID="Label4" runat="server" Text="สัญญาฉบับนี้ทำขึ้นระหว่าง บริษัท  เพียวพลังงานไทย จำกัด  เลขที่ 86 อาคารสัมมากรเพลส              "></asp:Label>                                                                           
                                    </div>
                                    <div style="margin-left: auto; margin-right: auto; text-align: center;" >
                                        <asp:Label ID="Label5" runat="server" Text="ถนนรามคำแหง แขวงสะพานสูง เขตสะพานสูง กรุงเทพมหานคร ซึ่งต่อไปนี้เรียกว่า “บริษัทฯ” ฝ่ายหนึ่ง              "></asp:Label>                                                                           
                                    </div>
                                    <div style="margin-left: auto; margin-right: auto; text-align: center;" class="col-md-5">                                       
                                             <div class="input-group sm-3">
                                                กับ บริษัท / ห้างหุ้นส่วนจำกัด / นาย / นาง /นางสาว   &nbsp
                                                <asp:TextBox class="form-control" ID="txtContract" runat="server" ></asp:TextBox>  
                                             </div>
                                    </div>
                                    <div style="margin-left: auto; margin-right: auto; text-align: center;" class="col-md-6">
                                        <BR />
                                             <div class="input-group sm-3">
                                                อยู่บ้านเลขที่   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox1" runat="server" ></asp:TextBox>  
                                                หมู่ที่   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox2" runat="server" ></asp:TextBox>  
                                                ถนน   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox3" runat="server" ></asp:TextBox>  
                                                แขวง/ตำบล   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox4" runat="server" ></asp:TextBox>  
                                             </div>                                                           
                                    </div>   
                                    <div style="margin-left: auto; margin-right: auto; text-align: center;" class="col-md-6">
                                        <BR />
                                             <div class="input-group sm-3">
                                                เขต/อำเภอ   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox5" runat="server" ></asp:TextBox>  
                                                จังหวัด  &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox6" runat="server" ></asp:TextBox>  
                                               ซึ่งต่อไปในสัญญานี้จะรวมเรียกว่า “ผู้ร่วมธุรกิจ” อีกฝ่ายหนึ่ง
                                             </div>                                                           
                                    </div>                                   
                            </div>
                             <BR />
                            <div class="col-md-10">
                                    <div style="margin-left: 5%; margin-right: auto; text-align: center;" >
                                        <asp:Label ID="Label6" runat="server" Text="ถนนรามคำแหง แขวงสะพานสูง เขตสะพานสูง กรุงเทพมหานคร ซึ่งต่อไปนี้เรียกว่า “บริษัทฯ” ฝ่ายหนึ่ง              "></asp:Label>                                                                           
                                    </div>
                                    <div style="margin-left: auto; margin-right: auto; text-align: center;" class="col-md-6">
                                        <BR />
                                             <div class="input-group sm-3">
                                                น.ส.3ก เลขที่   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox7" runat="server" ></asp:TextBox>  
                                                เลขที่ดิน   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox8" runat="server" ></asp:TextBox>  
                                                หน้าสำรวจ   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox9" runat="server" ></asp:TextBox>  
                                                ตำบล   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox10" runat="server" ></asp:TextBox>                                                
                                             </div>                                                           
                                    </div> 
                                    <div style="margin-left: auto; margin-right: auto; text-align: center;" class="col-md-6">
                                        <BR />
                                             <div class="input-group sm-3">                                              
                                                อำเภอ   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox19" runat="server" ></asp:TextBox>  
                                                จังหวัด   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox20" runat="server" ></asp:TextBox>  
                                                หน้าสำรวจ   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox21" runat="server" ></asp:TextBox>  
                                                เนื้อที่ประมาณ   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox22" runat="server" ></asp:TextBox>  
                                             </div>                                                           
                                    </div> 
                                    <div style="margin-left: auto; margin-right: auto; text-align: center;" class="col-md-6">
                                        <BR />
                                             <div class="input-group sm-3">                                              
                                                ไร่   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox11" runat="server" ></asp:TextBox>  
                                                งาน   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox12" runat="server" ></asp:TextBox>  
                                                ตารางวา   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox13" runat="server" ></asp:TextBox>  
                                                และที่ดินโฉนด  เลขที่   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox14" runat="server" ></asp:TextBox>  
                                             </div>                                                           
                                    </div> 
                                    <div style="margin-left: auto; margin-right: auto; text-align: center;" class="col-md-6">
                                        <BR />
                                             <div class="input-group sm-3">                                              
                                                เลขที่ดิน   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox15" runat="server" ></asp:TextBox>  
                                                หน้าสำรวจ   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox16" runat="server" ></asp:TextBox>  
                                                ตำบล   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox17" runat="server" ></asp:TextBox>  
                                                อำเภอ   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox18" runat="server" ></asp:TextBox> 
                                                จังหวัด   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox23" runat="server" ></asp:TextBox>  
                                                เนื้อที่ประมาณ   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox24" runat="server" ></asp:TextBox>                                                  
                                             </div>                                                           
                                    </div> 
                                    <div style="margin-left: auto; margin-right: auto; text-align: center;" class="col-md-6">
                                        <BR />
                                             <div class="input-group sm-3">                                              
                                                ไร่   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox25" runat="server" ></asp:TextBox>  
                                                งาน   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox26" runat="server" ></asp:TextBox>  
                                                ตารางวา   &nbsp
                                                <asp:TextBox class="form-control" ID="TextBox27" runat="server" ></asp:TextBox>  
                                               ต่อไปนี้จะรวมเรียกว่า “พื้นที่สถานีบริการน้ำมันเชื้อเพลิง” และบริษัทฯ เป็นผู้
                                               เข้าดำเนินการก่อสร้างสถานีบริการน้ำมัน เพื่อประกอบกิจการค้าน้ำมันเชื้อเพลิงหรือผลิตภัณฑ์ปิโตรเลียมตามที่กฎหมายกำหนด   

                                                ในกรณีที่ผู้ร่วมธุรกิจมิได้เป็นเจ้าของที่ดินและ/หรือสิ่งปลูกสร้าง ผู้ร่วมธุรกิจตกลงเป็นผู้ดำเนินการให้เจ้าของที่ดินและ/หรือสิ่งปลูกสร้างดังกล่าว 
                                                ตกลงเป็นผู้ร่วมธุรกิจตามสัญญานี้อีกฝ่ายหนึ่ง หรือให้ความยินยอมแก่บริษัทฯให้สามารถเข้าใช้ประโยชน์ในที่ดิน และ/หรือสิ่งปลูกสร้างนั้นได้ ตามวัตถุประสงค์ในสัญญานี้ 
                                                ทุกฝ่ายประสงค์จะประกอบธุรกิจเกี่ยวกับการค้าและสถานีบริการน้ำมันเชื้อเพลิงร่วมกันและให้ถือเอาเอกสารแนบท้าย 1 เป็นส่วนหนึ่งของสัญญานี้ด้วย 
                                                จึงตกลงทำสัญญาโดยมีรายละเอียดดังนี้

                                             </div>                                                           
                                    </div> 

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
