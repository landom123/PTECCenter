<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="BranchPermission.aspx.vb" Inherits="PTECCENTER.BranchPermission" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper">

    <!-- #include virtual ="/include/menu.inc" --> <!-- add side menu -->

        <!-- begin content-wrapper ส่วนเนื้อหา-->
        <div id="content-wrapper">
            <div class="container-fluid">    
                <!-- Breadcrumbs-->
                <ol class="breadcrumb">
                  <li class="breadcrumb-item">
                    <a href="#">กำหนดสิทธิ์การใช้งานสาขา : <% =Session("username") %></a>
                  </li>
                </ol>


                          <div id="jobdetail" class="card-body">

                              <%--begin item--%>

                            <div class="table-responsive">
                              <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0">
                                <thead>
                                  <tr>
                                    <th>Name</th>
                                    <th>
                                        <button type="button" class="btn btn-sm  btn-primary"  
                                            onclick="location.href='../ADMIN/branchpermissionadd.aspx?branchid=all'">Add All</button>
                                        <button type="button" class="btn btn-sm  btn-danger"  
                                            onclick="location.href='../ADMIN/branchpermissionremove.aspx?branchid=all'">Remove All</button>
                                    </th>
                                  </tr>
                                </thead>
                                <tfoot>
                                  <tr>
                                    <th>Name</th>
                                    <th></th>
                                   </tr>
                                </tfoot>
                                <tbody>                   
                                    <% For i = 0 To branchtable.Rows.Count - 1 %>
                                    <tr>
                                        <td>
                                            <% =branchtable.Rows(i).Item("name") %>
                                        </td>
                                        <td>
                                                <% If branchtable.Rows(i).Item("selected") = False Then %>
                                                        <button type="button" class="btn btn-sm  btn-primary"  
                                                            onclick="location.href='../ADMIN/branchpermissionadd.aspx?branchid=<% =branchtable.Rows(i).Item("branchid") %> '">Add</button>
                                                <% Else %>
                                                        <button type="button" class="btn btn-sm  btn-danger"  
                                                            onclick="location.href='../ADMIN/branchpermissionremove.aspx?branchid=<% =branchtable.Rows(i).Item("branchid") %> '">Remove</button>
                                                <% End if %>
                                        </td>
                                    </tr>
<%-- end detail row--%>
                                    <% Next i %>
                                </tbody>
                              </table>
                            </div>

                            </div>

                             <%-- end item--%>

                          </div><!-- end jobdetail -->





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
</asp:Content>
