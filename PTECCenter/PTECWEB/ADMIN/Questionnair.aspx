<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="Questionnair.aspx.vb" Inherits="PTECCENTER.Questionnair" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper" class="h-100">

    <!-- #include virtual ="/include/menu.inc" --> <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">    
  <div class="container">

      <div class="card-body">
    <div class="row"><div class="col-12">แบบทดสอบ</div></div></br>
<div class="row">
    <div class="col-3">

              <label class="sr-only" for="inlineFormInputGroupUsername"></label>
              <div class="input-group">
                <div class="input-group-prepend">
                  <div class="input-group-text"><i class="fa fa-question-circle-o" aria-hidden="true">ข้อที่ 1</i></div>
                </div>
                  <asp:DropDownList ID="ans01" class="form-control" required="required" runat="server">
                      <asp:ListItem Selected="True">A</asp:ListItem>
                      <asp:ListItem>B</asp:ListItem>
                      <asp:ListItem>C</asp:ListItem>
                      <asp:ListItem>D</asp:ListItem>
                  </asp:DropDownList>            
              </div>
              <br />
              <div class="input-group">
                <div class="input-group-prepend">
                  <div class="input-group-text"><i class="fa fa-question-circle-o" aria-hidden="true">ข้อที่ 2</i></div>
                </div>
                  <asp:DropDownList ID="ans02" class="form-control" required="required" runat="server">
                      <asp:ListItem Selected="True">A</asp:ListItem>
                      <asp:ListItem>B</asp:ListItem>
                      <asp:ListItem>C</asp:ListItem>
                      <asp:ListItem>D</asp:ListItem>
                  </asp:DropDownList>            
              </div>
              <br />
              <div class="input-group">
                <div class="input-group-prepend">
                  <div class="input-group-text"><i class="fa fa-question-circle-o" aria-hidden="true">ข้อที่ 3</i></div>
                </div>
                  <asp:DropDownList ID="ans03" class="form-control" required="required" runat="server">
                      <asp:ListItem Selected="True">A</asp:ListItem>
                      <asp:ListItem>B</asp:ListItem>
                      <asp:ListItem>C</asp:ListItem>
                      <asp:ListItem>D</asp:ListItem>
                  </asp:DropDownList>            
              </div>
              <br />
              <div class="input-group">
                <div class="input-group-prepend">
                  <div class="input-group-text"><i class="fa fa-question-circle-o" aria-hidden="true">ข้อที่ 4</i></div>
                </div>
                  <asp:DropDownList ID="ans04" class="form-control" required="required" runat="server">
                      <asp:ListItem Selected="True">A</asp:ListItem>
                      <asp:ListItem>B</asp:ListItem>
                      <asp:ListItem>C</asp:ListItem>
                      <asp:ListItem>D</asp:ListItem>
                  </asp:DropDownList>            
              </div>
              <br />
              <div class="input-group">
                <div class="input-group-prepend">
                  <div class="input-group-text"><i class="fa fa-question-circle-o" aria-hidden="true">ข้อที่ 5</i></div>
                </div>
                  <asp:DropDownList ID="ans05" class="form-control" required="required" runat="server">
                      <asp:ListItem Selected="True">A</asp:ListItem>
                      <asp:ListItem>B</asp:ListItem>
                      <asp:ListItem>C</asp:ListItem>
                      <asp:ListItem>D</asp:ListItem>
                  </asp:DropDownList>            
              </div>
    </div>
    <div class="col-3">

              <label class="sr-only" for="inlineFormInputGroupUsername"></label>
              <div class="input-group">
                <div class="input-group-prepend">
                  <div class="input-group-text"><i class="fa fa-question-circle-o" aria-hidden="true">ข้อที่ 6</i></div>
                </div>
                  <asp:DropDownList ID="ans06" class="form-control" required="required" runat="server">
                      <asp:ListItem Selected="True">A</asp:ListItem>
                      <asp:ListItem>B</asp:ListItem>
                      <asp:ListItem>C</asp:ListItem>
                      <asp:ListItem>D</asp:ListItem>
                  </asp:DropDownList>            
              </div>
              <br />
              <div class="input-group">
                <div class="input-group-prepend">
                  <div class="input-group-text"><i class="fa fa-question-circle-o" aria-hidden="true">ข้อที่ 7</i></div>
                </div>
                  <asp:DropDownList ID="ans07" class="form-control" required="required" runat="server">
                      <asp:ListItem Selected="True">A</asp:ListItem>
                      <asp:ListItem>B</asp:ListItem>
                      <asp:ListItem>C</asp:ListItem>
                      <asp:ListItem>D</asp:ListItem>
                  </asp:DropDownList>            
              </div>
              <br />
              <div class="input-group">
                <div class="input-group-prepend">
                  <div class="input-group-text"><i class="fa fa-question-circle-o" aria-hidden="true">ข้อที่ 8</i></div>
                </div>
                  <asp:DropDownList ID="ans08" class="form-control" required="required" runat="server">
                      <asp:ListItem Selected="True">A</asp:ListItem>
                      <asp:ListItem>B</asp:ListItem>
                      <asp:ListItem>C</asp:ListItem>
                      <asp:ListItem>D</asp:ListItem>
                  </asp:DropDownList>            
              </div>
              <br />
              <div class="input-group">
                <div class="input-group-prepend">
                  <div class="input-group-text"><i class="fa fa-question-circle-o" aria-hidden="true">ข้อที่ 9</i></div>
                </div>
                  <asp:DropDownList ID="ans09" class="form-control" required="required" runat="server">
                      <asp:ListItem Selected="True">A</asp:ListItem>
                      <asp:ListItem>B</asp:ListItem>
                      <asp:ListItem>C</asp:ListItem>
                      <asp:ListItem>D</asp:ListItem>
                  </asp:DropDownList>            
              </div>
              <br />
              <div class="input-group">
                <div class="input-group-prepend">
                  <div class="input-group-text"><i class="fa fa-question-circle-o" aria-hidden="true">ข้อที่ 10</i></div>
                </div>
                  <asp:DropDownList ID="ans10" class="form-control" required="required" runat="server">
                      <asp:ListItem Selected="True">A</asp:ListItem>
                      <asp:ListItem>B</asp:ListItem>
                      <asp:ListItem>C</asp:ListItem>
                      <asp:ListItem>D</asp:ListItem>
                  </asp:DropDownList>            
              </div>

    </div>
    <div class="col-3">

              <label class="sr-only" for="inlineFormInputGroupUsername"></label>
              <div class="input-group">
                <div class="input-group-prepend">
                  <div class="input-group-text"><i class="fa fa-question-circle-o" aria-hidden="true">ข้อที่ 11</i></div>
                </div>
                  <asp:DropDownList ID="ans11" class="form-control" required="required" runat="server">
                      <asp:ListItem Selected="True">A</asp:ListItem>
                      <asp:ListItem>B</asp:ListItem>
                      <asp:ListItem>C</asp:ListItem>
                      <asp:ListItem>D</asp:ListItem>
                  </asp:DropDownList>            
              </div>
              <br />
              <div class="input-group">
                <div class="input-group-prepend">
                  <div class="input-group-text"><i class="fa fa-question-circle-o" aria-hidden="true">ข้อที่ 12</i></div>
                </div>
                  <asp:DropDownList ID="ans12" class="form-control" required="required" runat="server">
                      <asp:ListItem Selected="True">A</asp:ListItem>
                      <asp:ListItem>B</asp:ListItem>
                      <asp:ListItem>C</asp:ListItem>
                      <asp:ListItem>D</asp:ListItem>
                  </asp:DropDownList>            
              </div>
              <br />
              <div class="input-group">
                <div class="input-group-prepend">
                  <div class="input-group-text"><i class="fa fa-question-circle-o" aria-hidden="true">ข้อที่ 13</i></div>
                </div>
                  <asp:DropDownList ID="ans13" class="form-control" required="required" runat="server">
                      <asp:ListItem Selected="True">A</asp:ListItem>
                      <asp:ListItem>B</asp:ListItem>
                      <asp:ListItem>C</asp:ListItem>
                      <asp:ListItem>D</asp:ListItem>
                  </asp:DropDownList>            
              </div>
              <br />
              <div class="input-group">
                <div class="input-group-prepend">
                  <div class="input-group-text"><i class="fa fa-question-circle-o" aria-hidden="true">ข้อที่ 14</i></div>
                </div>
                  <asp:DropDownList ID="ans14" class="form-control" required="required" runat="server">
                      <asp:ListItem Selected="True">A</asp:ListItem>
                      <asp:ListItem>B</asp:ListItem>
                      <asp:ListItem>C</asp:ListItem>
                      <asp:ListItem>D</asp:ListItem>
                  </asp:DropDownList>            
              </div>
              <br />
              <div class="input-group">
                <div class="input-group-prepend">
                  <div class="input-group-text"><i class="fa fa-question-circle-o" aria-hidden="true">ข้อที่ 15</i></div>
                </div>
                  <asp:DropDownList ID="ans15" class="form-control" required="required" runat="server">
                      <asp:ListItem Selected="True">A</asp:ListItem>
                      <asp:ListItem>B</asp:ListItem>
                      <asp:ListItem>C</asp:ListItem>
                      <asp:ListItem>D</asp:ListItem>
                  </asp:DropDownList>            
              </div>

    </div>
    <div class="col-3">

              <label class="sr-only" for="inlineFormInputGroupUsername"></label>
              <div class="input-group">
                <div class="input-group-prepend">
                  <div class="input-group-text"><i class="fa fa-question-circle-o" aria-hidden="true">ข้อที่ 16</i></div>
                </div>
                  <asp:DropDownList ID="ans16" class="form-control" required="required" runat="server">
                      <asp:ListItem Selected="True">A</asp:ListItem>
                      <asp:ListItem>B</asp:ListItem>
                      <asp:ListItem>C</asp:ListItem>
                      <asp:ListItem>D</asp:ListItem>
                  </asp:DropDownList>            
              </div>
              <br />
              <div class="input-group">
                <div class="input-group-prepend">
                  <div class="input-group-text"><i class="fa fa-question-circle-o" aria-hidden="true">ข้อที่ 17</i></div>
                </div>
                  <asp:DropDownList ID="ans17" class="form-control" required="required" runat="server">
                      <asp:ListItem Selected="True">A</asp:ListItem>
                      <asp:ListItem>B</asp:ListItem>
                      <asp:ListItem>C</asp:ListItem>
                      <asp:ListItem>D</asp:ListItem>
                  </asp:DropDownList>            
              </div>
              <br />
              <div class="input-group">
                <div class="input-group-prepend">
                  <div class="input-group-text"><i class="fa fa-question-circle-o" aria-hidden="true">ข้อที่ 18</i></div>
                </div>
                  <asp:DropDownList ID="ans18" class="form-control" required="required" runat="server">
                      <asp:ListItem Selected="True">A</asp:ListItem>
                      <asp:ListItem>B</asp:ListItem>
                      <asp:ListItem>C</asp:ListItem>
                      <asp:ListItem>D</asp:ListItem>
                  </asp:DropDownList>            
              </div>
              <br />
              <div class="input-group">
                <div class="input-group-prepend">
                  <div class="input-group-text"><i class="fa fa-question-circle-o" aria-hidden="true">ข้อที่ 19</i></div>
                </div>
                  <asp:DropDownList ID="ans19" class="form-control" required="required" runat="server">
                      <asp:ListItem Selected="True">A</asp:ListItem>
                      <asp:ListItem>B</asp:ListItem>
                      <asp:ListItem>C</asp:ListItem>
                      <asp:ListItem>D</asp:ListItem>
                  </asp:DropDownList>            
              </div>
              <br />
              <div class="input-group">
                <div class="input-group-prepend">
                  <div class="input-group-text"><i class="fa fa-question-circle-o" aria-hidden="true">ข้อที่ 20</i></div>
                </div>
                  <asp:DropDownList ID="ans20" class="form-control" required="required" runat="server">
                      <asp:ListItem Selected="True">A</asp:ListItem>
                      <asp:ListItem>B</asp:ListItem>
                      <asp:ListItem>C</asp:ListItem>
                      <asp:ListItem>D</asp:ListItem>
                  </asp:DropDownList>            
              </div>
              <br />
    </div>
</div>
          <asp:Button ID="btnSave" class="btn btn-primary btn-block" runat="server" Text="ส่งคำตอบ" UseSubmitBehavior="false" /><br />
          <asp:label ID="lblstatus" class="btn btn-danger btn-block" runat="server" Text=".." /><br />
<%--          <a class="btn btn-primary btn-block" href="index.html">Login</a>--%>
<%--        <div class="text-center">
          <a class="d-block small mt-3" href="register.html">Register an Account</a>
          <a class="d-block small" href="forgot-password.html">Forgot Password?</a>
        </div>--%>
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
</asp:Content>
