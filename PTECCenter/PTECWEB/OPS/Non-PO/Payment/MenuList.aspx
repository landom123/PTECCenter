<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="MenuList.aspx.vb" Inherits="PTECCENTER.MenuList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="card-body">
        <div class="table-responsive overflow-auto" style="font-size: 0.9rem">
            <asp:GridView ID="gvRemind"
                class="table table-striped table-bordered"
                AutoGenerateColumns="false"
                EmptyDataText="No data available."
                PageSize="20"
                AllowPaging="true"
                runat="server">
                <Columns>
                    <asp:TemplateField HeaderText="NonPO_Code" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <asp:Label ID="lblcode" runat="server" Text='<%#Eval("NonPO_Code")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CreateDate" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <asp:Label ID="lbljobdate" runat="server" Text='<%#Eval("CreateDate")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Createby" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <asp:Label ID="lblBranch" runat="server" Text='<%#Eval("CreateBy")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DueDate" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <asp:Label ID="lbljobdate" runat="server" Text='<%#Eval("Duedate")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Detail">
                        <ItemTemplate>
                            <asp:Label ID="lbljobdate" runat="server" Text='<%#Eval("detail")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="total" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lbldetails" runat="server" Text='<%#String.Format("{0:n}", Eval("cost"))%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <asp:Label ID="lbljobtype" runat="server" Text='<%#Eval("StatusNonPO")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <button id="btnRun" onclick="dup('<%# String.Format("{0}", Eval("NonPO_Code")) %>','<%= Session("usercode") %>');" class="btn btn-mini text-info" title="คัดลอก">
                                <i class="far fa-copy"></i>
                            </button>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Eval("link")%>' Text="" Target="_blank"><img src="../../../../icon/addnote.png" title="รายละเอียด" style="width:20px" /></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <script>
        function dup(nonpocode, usercode) {

            event.preventDefault();
            event.stopPropagation();
            Swal.fire({
                input: 'textarea',
                inputLabel: 'กรอกวัตถุประสงค์',
                inputPlaceholder: 'ใส่ข้อความ . . .',
                inputAttributes: {
                    'aria-label': 'ใส่ข้อความ.'
                },
                preConfirm: () => {
                    if (!document.getElementById('swal2-input').value) {
                        // Handle return value 
                        Swal.showValidationMessage('First input missing')
                    }
                },
                showCancelButton: true
            }).then((result) => {
                console.log(result.value);

                if (result.isConfirmed) {
                    var params = "{'nonpocode': '" + nonpocode + "','usercode': '" + usercode + "','note': '" + result.value + "'}";
                    $.ajax({
                        type: "POST",
                        url: "../Payment/MenuList.aspx/dup",
                        async: true,
                        data: params,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            /*alertSuccessToast();*/
                            if (msg.d == 'success') {
                                //alert(elemenmt.textContent);
                                window.location.href = location.href;
                            }

                        },
                        error: function () {
                            event.preventDefault();
                            event.stopPropagation();
                        }
                    });
                }
            })

            return false;
            
        }
    </script>

</asp:Content>
