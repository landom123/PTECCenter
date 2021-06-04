<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="FileUpload.aspx.vb" Inherits="PTECCENTER.FileUpload1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .image-upload-wrap {
            border-radius: 0.25rem;
            background: #f0cccc;
            border: 2px dashed #ff0000;
            text-align: center;
            font-size: 30px;
            color: #ff0000;
            cursor: pointer;
            opacity: 0.5;
            height: 130px;
            width: 130px;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
        }

            .image-upload-wrap i {
                position: absolute;
                font-style: normal;
                top: 50%;
                left: 50%;
                -webkit-transform: translateX(-50%) translateY(-50%);
                transform: translateX(-50%) translateY(-50%);
            }

        .file-upload-input {
            cursor: pointer;
            opacity: 0;
            height: 100%;
            width: 100%;
        }

        .file-upload-content {
            display: none;
            text-align: center;
        }

        .file-upload-image {
            max-width: 100%;
            max-height: 100%;
            margin: auto;
            padding: 20px;
        }

        .remove-image {
            border: 0;
            background: #fe7676;
            border-radius: 50%;
            box-shadow: -1px 1px 6px rgb(254 118 118 / 80%);
            color: #fdfdfd;
            text-shadow: 1px 1px 3px rgb(0 0 0 / 30%);
        }

        .image-title-wrap {
            position: absolute;
            top: 6px;
            right: 6px;
            z-index: 2;
            height: 20px;
        }


        .btn-clipboard {
            background-color: transparent;
            border: 0;
        }

        a img {
            display: none;
        }

        a:hover img {
            max-width: 25%;
            height: auto;
            left: 50%;
            bottom: 100%;
            position: absolute;
            display: block;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg">
        <div id="wrapper">

            <!-- #include virtual ="/include/menu.inc" -->
            <!-- add side menu -->

            <div id="content-wrapper">
                <div class="container">

                    <div class="row bg-white">
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Label ID="lbUserName" CssClass="form-label" AssociatedControlID="txtUserName" runat="server" Text="Username" />
                                                <asp:TextBox class="form-control" ID="txtUserName" runat="server" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row justify-content-md-center">
                                        <div class="col-md-10">
                                            <div class="input-group justify-content-center">
                                                <div class="image-upload-wrap justify-content-center">
                                                    <i>+</i>
                                                    <asp:FileUpload ID="FileUpload1" class="file-upload-input" runat="server" onchange="readURL(this);" accept="image/*" text="เลือกไฟล์" />
                                                </div>
                                            </div>
                                            <div class="file-upload-content">
                                                <img class="file-upload-image" id="img1" src="#" alt="your image" runat="server" />
                                                <div class="image-title-wrap">
                                                    <button runat="server" id="btnDelete" name="btnDelete" onclick="removeUpload()" type='button' class='close' aria-label='Close Close-danger'>
                                                        <span aria-hidden='true'>&times;</span>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Label ID="lbDetail" CssClass="form-label" AssociatedControlID="txtDetail" runat="server" Text="รายละเอียดไฟล์แนบ" />
                                                <asp:Label ID="lbDetailMandatory" CssClass="text-danger" AssociatedControlID="txtDetail" runat="server" Text="*" />
                                                <asp:TextBox class="form-control" ID="txtDetail" runat="server" Rows="3" Columns="50" TextMode="MultiLine" onkeyDown="checkTextAreaMaxLength(this,event,'255');" required></asp:TextBox>
                                                <div class="invalid-feedback">กรุณากรอกรายละเอียด</div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-footer text-center bg-white">
                                        <asp:Button ID="btnUpload" class="btn btn-primary" runat="server" Text="Upload" OnClientClick="validateData()" AutoPostBack="true" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="container-fluid">
                    <div class="card-body">
                        <div class="table-responsive">
                            <asp:GridView ID="gvRemind"
                                class="table table-striped table-bordered"
                                AutoGenerateColumns="false"
                                EmptyDataText="No data available."
                                AllowPaging="true"
                                runat="server">
                                <Columns>
                                    <asp:TemplateField HeaderText="by" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("createby")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="detail">
                                        <ItemTemplate >
                                            <asp:Label ID="lblstatus" Style="display: table; table-layout: fixed; width: 100%; word-wrap: break-word;" runat="server" Text='<%#Eval("attatchdetail")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="path" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <div class="row justify-content-between">
                                                <div class="col text-left align-self-center">
                                                    <a href="http://vpnptec.dyndns.org:10280/OPS_Fileupload/<%#Eval("attatchname")%>" target="_blank" />
                                                    http://vpnptec.dyndns.org:10280/OPS_Fileupload/<%#Eval("attatchname")%>
                                                    <img src="http://vpnptec.dyndns.org:10280/OPS_Fileupload/<%#Eval("attatchname")%>" class="img-fluid img-thumbnail" />
                                                    </a>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="extension">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("attatchextension")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>

                </div>
                <!-- end container-->

            </div>
            <!-- end content-->

        </div>
        <!-- end wrapper-->

    </div>
    <!-- end bg-->



    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>

    <script type="text/javascript">
        function readURL(input) {
            console.log(input);
            if (input.files && input.files[0]) {

                var reader = new FileReader();
                reader.onload = function (e) {
                    var size = input.files[0].size;
                    console.log(size)
                    console.log(input.files[0])

                    $('.image-upload-wrap').hide();

                    $('.file-upload-image').attr('src', e.target.result);
                    $('.file-upload-content').show();

                    console.log("end")
                };

                reader.readAsDataURL(input.files[0]);

            } else {
                removeUpload();
            }
        }
        function removeUpload() {
            document.getElementById('<%= FileUpload1.ClientID%>').value = "";
            $('.file-upload-input').replaceWith($('.file-upload-input').clone());
            $('.file-upload-content').hide();
            $('.image-upload-wrap').show();
        }
        $('.image-upload-wrap').bind('dragover', function () {
            $('.image-upload-wrap').addClass('image-dropping');
        });
        $('.image-upload-wrap').bind('dragleave', function () {
            $('.image-upload-wrap').removeClass('image-dropping');
        });
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
