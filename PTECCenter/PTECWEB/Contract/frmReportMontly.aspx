<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="frmReportMontly.aspx.vb" Inherits="PTECCENTER.frmReportMontly" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper">

        <!-- #include virtual ="/include/menu.inc" -->
        <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">
                <!-- Breadcrumbs-->
                <ol class="breadcrumb" style="background-color: deeppink; color: white">
                    <li class="breadcrumb-item">
                        <i class="fa fa-tasks" aria-hidden="true"></i>Esso EDI Invoice to D365
                    </li>
                </ol>
                <p></p>
                <div class="input-group sm-3 row">
                    <div class="col-3">
                        <asp:TextBox class="form-control" ID="txtCalcDate" runat="server" Style="background-color: white"></asp:TextBox>
                    </div>
                    <div class="col-sm-1">
                        <asp:Button ID="btnCalc" class="btn btn-sm  btn-primary" runat="server" Text=" คำนวน " />
                    </div>
                    <div class="col-sm-1">
                        <asp:Button ID="btnExport" class="btn btn-sm  btn-primary" runat="server" Text=" Export to excel " />
                    </div>


                </div>
                <div class="card-body">
                    <div class="card-body col-12 text-right">
                        <%--    <asp:Button ID="btnselectall" class="btn btn-sm  btn-success" runat="server" Text="Select All" />&nbsp;  
                                        <asp:Button ID="btnunselect" class="btn btn-sm  btn-danger" runat="server" Text="Un Select All" /> --%>
                        <asp:Button ID="btnBack" class="btn btn-sm  btn-success" runat="server" Text=" กลับ " />
                    </div>

                    <div class="table-responsive">

                        <div class="table-responsive" runat="server">
                            <asp:Table ID="tblData" class="table table-bordered table-condensed table-hover pnlstudentDetails" Style='font-family: Tahoma, Courier, monospace; font-size: 8pt;'
                                runat="server" GridLines="Both" CellSpacing="0" CellPadding="5" border="1" OnPageIndexChanging="OnPageIndexChanging" PageSize="10">
                                <asp:TableHeaderRow TableSection="TableHeader" runat="server">
                                    <%--<asp:TableHeaderCell  runat="server">Group</asp:TableHeaderCell>--%>
                                    <asp:TableHeaderCell ColumnSpan="2" Style='margin: 0 auto; text-align: center; background-color: moccasin;' runat="server">สาขา</asp:TableHeaderCell>
                                    <asp:TableHeaderCell ColumnSpan="1" Style='margin: 0 auto; text-align: center; background-color: moccasin;' runat="server">สัญญา</asp:TableHeaderCell>
                                    <asp:TableHeaderCell ColumnSpan="1" RowSpan="2" Style='margin: 0 auto; text-align: center; background-color: moccasin;' runat="server">A/C Code</asp:TableHeaderCell>
                                    <asp:TableHeaderCell ColumnSpan="1" RowSpan="2" Style='margin: 0 auto; text-align: center; background-color: moccasin;' runat="server">Purpose</asp:TableHeaderCell>
                                    <asp:TableHeaderCell ColumnSpan="2" Style='margin: 0 auto; text-align: center; background-color: moccasin;' runat="server">Period</asp:TableHeaderCell>
                                    <asp:TableHeaderCell ColumnSpan="1" Style='margin: 0 auto; text-align: center; background-color: moccasin;' runat="server">อัตราค่าเช่า</asp:TableHeaderCell>
                                    <asp:TableHeaderCell ColumnSpan="1" RowSpan="2" Style='margin: 0 auto; text-align: center; background-color: moccasin;' runat="server">ผู้ให้เช่า</asp:TableHeaderCell>
                                    <asp:TableHeaderCell ColumnSpan="1" RowSpan="2" Style='margin: 0 auto; text-align: center; background-color: moccasin;' runat="server">สั่งจ่ายเช็ค</asp:TableHeaderCell>
                                    <asp:TableHeaderCell ColumnSpan="1" RowSpan="2" Style='margin: 0 auto; text-align: center; background-color: moccasin;' runat="server">หักภาษี ณ ที่จ่ายในนาม</asp:TableHeaderCell>
                                    <asp:TableHeaderCell ColumnSpan="1" RowSpan="2" Style='margin: 0 auto; text-align: center; background-color: moccasin;' runat="server">ธนาคาร</asp:TableHeaderCell>
                                    <asp:TableHeaderCell ColumnSpan="1" RowSpan="2" Style='margin: 0 auto; text-align: center; background-color: moccasin;' runat="server">เลขที่บัญชี</asp:TableHeaderCell>
                                    <asp:TableHeaderCell ColumnSpan="1" RowSpan="2" Style='margin: 0 auto; text-align: center; background-color: moccasin;' runat="server">อัตราค่าเช่า</asp:TableHeaderCell>
                                    <asp:TableHeaderCell ColumnSpan="1" RowSpan="2" Style='margin: 0 auto; text-align: center; background-color: moccasin;' runat="server">อัตราค่าเช่าร่วมธุรกิจ</asp:TableHeaderCell>
                                    <asp:TableHeaderCell ColumnSpan="1" RowSpan="2" Style='margin: 0 auto; text-align: center; background-color: moccasin;' runat="server">ก่อนหักภาษี ณ ที่จ่าย</asp:TableHeaderCell>
                                    <asp:TableHeaderCell ColumnSpan="1" RowSpan="2" Style='margin: 0 auto; text-align: center; background-color: moccasin;' runat="server">หักภาษี ณ ที่จ่าย 5%</asp:TableHeaderCell>
                                    <asp:TableHeaderCell ColumnSpan="1" RowSpan="2" Style='margin: 0 auto; text-align: center; background-color: moccasin;' runat="server">ยอดเงินโอน</asp:TableHeaderCell>

                                </asp:TableHeaderRow>
                                <asp:TableHeaderRow TableSection="TableHeader" runat="server">
                                    <asp:TableHeaderCell Style='margin: 0 auto; text-align: center; background-color: greenyellow;' runat="server">คลัง</asp:TableHeaderCell>
                                    <asp:TableHeaderCell Style='margin: 0 auto; text-align: center; background-color: greenyellow;' runat="server">กลุ่ม</asp:TableHeaderCell>
                                    <asp:TableHeaderCell Style='margin: 0 auto; text-align: center; background-color: greenyellow;' runat="server">ชื่อสัญญา</asp:TableHeaderCell>
                                    <%--<asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:whitesmoke;' runat="server">A/C Code</asp:TableHeaderCell>--%>
                                    <%--<asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:whitesmoke;' runat="server">Purpose</asp:TableHeaderCell>--%>
                                    <asp:TableHeaderCell Style='margin: 0 auto; text-align: center; background-color: greenyellow;' runat="server">Begin</asp:TableHeaderCell>
                                    <asp:TableHeaderCell Style='margin: 0 auto; text-align: center; background-color: greenyellow;' runat="server">End</asp:TableHeaderCell>
                                    <asp:TableHeaderCell Style='margin: 0 auto; text-align: center; background-color: greenyellow;' HorizontalAlign="Right" runat="server">รายเดือน</asp:TableHeaderCell>
                                    <%--<asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:whitesmoke;' runat="server">ผู้ให้เช่า</asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:whitesmoke;' runat="server">สั่งจ่ายเช็ค</asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:whitesmoke;' runat="server">หักภาษีในนาม</asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:whitesmoke;' runat="server">ธนาคาร</asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:whitesmoke;' runat="server">เลขที่บัญชี</asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:whitesmoke;' runat="server">อัตราค่าเช่า</asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:whitesmoke;' runat="server">อัตราค่าเช่าร่วมธุรกิจ</asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:whitesmoke;' runat="server">ก่อนหักภาษี ณ ที่จ่าย</asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:whitesmoke;' runat="server">หักภาษี ณ ที่จ่าย 5%</asp:TableHeaderCell>
                                                                        <asp:TableHeaderCell style='margin:0 auto; text-align:center; background-color:whitesmoke;' runat="server">ยอดเงินโอน</asp:TableHeaderCell>--%>
                                </asp:TableHeaderRow>
                                <asp:TableRow Style='margin: 0 auto; text-align: right;' runat="server">
                                    <%--<asp:TableCell runat="server">Group</asp:TableCell>--%>
                                    <%--<asp:TableCell runat="server" ColumnSpan="2">bla bla</asp:TableCell>--%>
                                    <%--<asp:TableCell runat="server">bla</asp:TableCell>--%>
                                </asp:TableRow>
                                <%--        <asp:TableRow TableSection="TableFooter" runat="server">
                                                                        <asp:TableCell runat="server">bla bla</asp:TableCell>
                                                                        <asp:TableCell runat="server">bla bla</asp:TableCell>
                                                                        <asp:TableCell runat="server">bla bla</asp:TableCell>         
                                                                    </asp:TableRow>--%>
                            </asp:Table>
                        </div>


                    </div>
                </div>


            </div>
            <!-- /.container-fluid -->
            <!-- Sticky Footer -->
            <footer class="sticky-footer">
                <div class="container my-auto">
                    <div class="copyright text-center my-auto">
                        <span>Copyright © Your Website 2019</span>
                    </div>
                </div>
            </footer>
        </div>
        <!-- end content-wrapper -->


        <!-- end เนื้อหาเว็บ -->


    </div>
    <!-- /#wrapper -->
    <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>

    <script type="text/javascript">
        jQuery('[id$=txtCalcDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
    </script>


</asp:Content>
