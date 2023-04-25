<%@ Page Title="" Language="vb" AutoEventWireup="false" 
    MasterPageFile="~/site.Master" CodeBehind="assetsinfo.aspx.vb" Inherits="PTECCENTER.assetsinfo" %>

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
                           <a href="client_list.aspx"><i class="fa fa-tasks" aria-hidden="true"></i></a> ข้อมูลทรัพย์สินในสัญญา
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
                    <div class="row" style="padding-top: 0.2rem;">
                        <div class="col-4">
                            <div class="input-group sm-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">เลขที่สัญญา</span>
                                </div>
                                <asp:TextBox class="form-control" ID="txtContractNo" placeholder="please save first" ReadOnly="true" runat="server" ></asp:TextBox>    

                            </div>
              <%--               <div class="input-group sm-3">
                                <span class="input-group-text">
                                  
                                        สัญญาร่วมธุรกิจ &nbsp                                      
                                            <asp:RadioButton ID="rdoplanBlank" runat="server" groupname= "fullarea" text=" ที่ดินเปล่า " Checked="true"/> &nbsp;&nbsp;
                                            <asp:RadioButton ID="rdoplanNonBlank" runat="server" groupname= "fullarea" text=" พร้อมสิ่งปลูกสร้าง " />                                
                                </span>
                             </div>--%>


                            <div class="col-15">
                                <%--<legend> สัญญาร่วมธุรกิจ </legend>--%>
                                 <div class="input-group sm-3">
                                      <div class="input-group-prepend">
                                            <span class="input-group-text" > สัญญาร่วมธุรกิจ &nbsp 

                                 <%--               <asp:RadioButtonList ID="rdoplanBlank" runat="server" RepeatDirection="Horizontal">           
                                                    <asp:ListItem Text="ที่ดินเปล่า" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="พร้อมสิ่งปลูกสร้าง" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>--%>

                                                <asp:TextBox class="form-control" ID="txtContractType"  ReadOnly="true" runat="server" ></asp:TextBox>    
                                            </span>
                                     </div>
                                </div>
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
                    <div class="row" style="padding-top: 0.2rem;">
                        <div class="col-md-3">                           
                            <div class="input-group sm-3">
                                 รหัสทรัพย์สิน &nbsp
                                <asp:Textbox class="form-control" ID="txtAssetsNo" placeholder="please save first" runat="server" ReadOnly="true"></asp:Textbox>    
                            </div>
                        </div>
                        <div class="col-md-3">                            
                            <div class="input-group sm-3">
                                ประเภทที่ดิน &nbsp
                                <asp:DropDownList class="form-control" ID="cboAssetType" runat="server"></asp:DropDownList>    
                            </div>
                        </div>

                    </div>
                    <div class="row" style="padding-top: 0.2rem;">
                        <div class="col-md-3">                            
                            <div class="input-group sm-3">
                                เลขที่โฉนด/เอกสารสิทธิ์ &nbsp
                                <asp:Textbox class="form-control" ID="txtLandno" runat="server"></asp:Textbox>    
                            </div>
                        </div>
                        <div class="col-md-3">                            
                            <div class="input-group sm-3">
                                เลขที่หน้าสำรวจ &nbsp
                                <asp:Textbox class="form-control" ID="txtSurveyNo" runat="server"></asp:Textbox>  
                            </div>
                        </div>

                    </div>
                    <div class="row" style="padding-top: 0.2rem;">
                        <div class="col-md-3">                           
                            <div class="input-group sm-3">
                                 ตำบล &nbsp
                                <asp:Textbox class="form-control" ID="txtSubDistrict" runat="server"></asp:Textbox>    
                            </div>
                        </div>
                        <div class="col-md-3">                            
                            <div class="input-group sm-3">
                                อำเภอ &nbsp
                                <asp:Textbox class="form-control" ID="txtDistrict" runat="server"></asp:Textbox>  
                            </div>
                        </div>
                        <div class="col-md-3">                           
                            <div class="input-group sm-3">
                                 จังหวัด &nbsp
                                <asp:Textbox class="form-control" ID="txtProvince" runat="server"></asp:Textbox>  
                            </div>
                        </div>

                    </div>
                    <div class="row" style="padding-top: 0.2rem;">
                        <div class="col-md-3">
                            ทั้งแปลง/แบ่งเช่า &nbsp
                            <div class="input-group sm-3">
                                <asp:RadioButton ID="rdoFull" runat="server" groupname= "fullarea" text=" ทั้งแปลง " Checked="true"/> &nbsp;&nbsp;
                                <asp:RadioButton ID="rdoPart" runat="server" groupname= "fullarea" text=" แบ่งเช่า " />
                            </div>
                        </div>
                        <div class="col-md-3">                           
                            <div class="input-group sm-3">
                                 ไร่ &nbsp
                                <asp:Textbox class="form-control" ID="txtRai" runat="server"></asp:Textbox>  
                            </div>
                        </div>
                        <div class="col-md-3">                           
                            <div class="input-group sm-3">
                                 งาน &nbsp
                                <asp:Textbox class="form-control" ID="txtNgan" runat="server"></asp:Textbox>  
                            </div>
                        </div>

                    </div>
                    <div class="row" style="padding-top: 0.2rem;">
                        <div class="col-md-3">                           
                            <div class="input-group sm-3">
                                 ตารางวา &nbsp
                                <asp:Textbox class="form-control" ID="txtWa" runat="server"></asp:Textbox>    
                            </div>
                        </div>
                        <div class="col-md-3">                           
                            <div class="input-group sm-3">
                                 พิกัด GPS &nbsp
                                <asp:Textbox class="form-control" ID="txtGPS" runat="server"></asp:Textbox>  
                            </div>
                        </div>

                    </div>

                    <br />

                                <div class="row" id="dvShowHide"  >
                                     <%--<div class="row" style="padding-top: 0.2rem;">--%>
                                        <div class="col-md-12">
                                             <span class="input-group-text" style="background-color:green ;color:white" runat="server" > พร้อมสิ่งปลูกสร้าง </span>                                                   
                                        </div>
                                     <%--</div>--%>

                                    <div class="row" style="padding-top: 0.5rem;">
                                        <div class="col-md-3">                                            
                                            <div class="input-group sm-3">
                                                สิ่งปลูกสร้างเลขที่ &nbsp
                                                <asp:Textbox class="form-control" ID="txtbuNo" runat="server"></asp:Textbox>    
                                            </div>
                                        </div>
                                        <div class="col-md-3">                                            
                                            <div class="input-group sm-3">
                                                ตำบล &nbsp
                                                <asp:Textbox class="form-control" ID="txtbuSubDistrict" runat="server"></asp:Textbox>    
                                            </div>
                                        </div>
                                    <%--</div>--%>
                             
                                    <%--<div class="row" style="padding-top: 0.5rem;">--%>
                                        <div class="col-md-3">                                           
                                            <div class="input-group sm-3">
                                                 อำเภอ &nbsp
                                                <asp:Textbox class="form-control" ID="txtbuDistrict" runat="server"></asp:Textbox>  
                                            </div>
                                        </div>
                                        <div class="col-md-3">                                           
                                            <div class="input-group sm-3">
                                                 จังหวัด &nbsp
                                                <asp:Textbox class="form-control" ID="txtbuProvince" runat="server"></asp:Textbox>  
                                            </div>
                                        </div>    
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
        jQuery('[id$=txtBirthday]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format:'d/m/Y'
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

<%--  <script src="//code.jquery.com/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=rdoplanBlank.ClientID %>').click(function () {
                var SelectedValue = $('#<%=rdoplanBlank.ClientID %> input[type=radio]:checked').val();

                if (SelectedValue == 1) {
                    //If cash is selected then hide the Div
                    $('#dvShowHide').css("display", "none");
                    //or you can simply use jQuery hide method to hide the Div as below:
                    //$('#dvShowHide').hide();          
                }
                else if (SelectedValue == 2) {
                    //If Cheque is selected then show the Div
                    $('#dvShowHide').css("display", "block");
                    //or you can simply use jQuery show method to show the Div as below:
                    //$('#dvShowHide').show();
                    //Clear textboxes
                   // $('#<%=txtbuNo.ClientID %>').val('');
                   // $('#<%=txtbuSubDistrict.ClientID %>').val('');
                    //Set focus in bank name textbox
                   // $('#<%=txtbuNo.ClientID %>').focus();
                }
            });
        });
    </script>--%>

</asp:Content>
