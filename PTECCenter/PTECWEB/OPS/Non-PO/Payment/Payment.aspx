<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="Payment.aspx.vb" Inherits="PTECCENTER.PaymentPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- datetimepicker-->
    <link href="<%=Page.ResolveUrl("~/datetimepicker/jquery.datetimepicker.css")%>" rel="stylesheet" type="text/css">

    <link href="<%=Page.ResolveUrl("~/css/print.css")%>" rel="stylesheet" type="text/css">
    <style>
        body {
            background-color: #f0f2f5;
        }

        h3, h5 {
            margin-right: auto;
            margin-left: auto;
        }

        .form-label {
            vertical-align: sub;
        }

        .form-control, .bootstrap-select .dropdown-toggle, .bootstrap-select.form-control {
            border: 1px solid #000;
            cursor: pointer;
            background-color: lightpink;
            height: 100%;
            width: 100%;
        }

        .company {
            margin-top: 10px;
            padding-left: 0px;
        }

        .company-th {
            margin-top: 15px;
        }

        .logopure {
            content: url("http://vpnptec.dyndns.org:10280/OPS_Fileupload/ATT_210800066.png");
            width: 100px;
            height: auto;
            margin-left: 30px;
            margin-top: 10px;
            margin-bottom: 10px;
        }

        table {
            padding: 20px;
            width: 1000px;
            border-radius: 2px;
            border-collapse: separate;
            border-spacing: 0;
            border: 1px;
            background-color: lightpink;
            table-layout: fixed;
        }

        td, th {
            margin: 0;
            border: 1px solid #000;
            white-space: nowrap;
            padding-left: 5px;
        }

        .nonpo {
            width: 1000px;
            overflow-x: auto;
            overflow-y: visible;
            padding: 0;
            margin-right: auto;
            margin-left: auto;
        }


        .draggable {
            padding: 1rem;
            background-color: lightpink;
            border: 1px solid black;
            cursor: move;
        }

            .draggable.dragging {
                opacity: .5;
            }

        .addAttach {
            display: none;
            font-size: 1rem;
        }

        .detail:hover .addAttach {
            display: flex;
            justify-content: center;
            align-items: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg">
        <div id="wrapper">
            <div id="content-wrapper">

                <div class="container">
                    <div class="row">
                        <div class="col">
                            <a href="defaultPayment.aspx" class="btn  back mb-4" style="color: red; font-size: 2rem; font-weight: bold;">
                                <i class="fas fa-arrow-circle-left"></i>
                            </a>&nbsp;
                             <asp:Button ID="btnNew" class="btn btn-sm  btn-primary" runat="server" Text="New" />&nbsp;
                        <asp:Button ID="btnSave" class="btn btn-sm  btn-success btnSave" runat="server" Text="Save" />
                            &nbsp;              
                        <asp:Button ID="btnConfirm" class="btn btn-sm  btn-secondary" runat="server" Text="Confirm" />
                            &nbsp;   
                        <asp:Button ID="btnPrint" class="btn btn-sm  btn-warning" OnClientClick="window.print();" runat="server" Text="Print" />&nbsp;
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="nonpo shadow">

                            <!-- (padding ซ้าย + ขวา = 40px ) -->
                            <!-- (table-width = 1000px ) -->
                            <!--  เนื้อหาข้างใน = 1000px - 40px  = 960 px -->
                            <table class="print">

                                <!--  colทั้งหทด = 24 col -->
                                <!--  960/24  = 40 px -->
                                <tr>
                                    <!--  18 * 40  = 720 px -->
                                    <td colspan="18" style="width: 720px !important; height: 10px">
                                        <div class="row">
                                            <div class="col-3">
                                                <img class="logopure" />
                                            </div>
                                            <div class="col-9 company">
                                                <div class="row company-th">
                                                    <h3>บริษัท เพียวพลังงานไทย จำกัด</h3>
                                                </div>
                                                <div class="row company-en">
                                                    <h5>PURE THAI ENERGY COMPANY LIMITED</h5>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                    <!--  6 * 40  = 240 px -->
                                    <td colspan="6" style="width: 240px !important;">
                                        <div class="type" style="height: 120px;">

                                            <asp:DropDownList class="form-control " ID="cboBrancha" runat="server" required></asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>


                                    <td colspan="12" style="width: 420px !important;">
                                        <div class="row">

                                            <div class="col-1">
                                                <asp:Label ID="Label2" CssClass="form-label" AssociatedControlID="cboName" runat="server" Text="ผู้เบิก" />
                                            </div>
                                            <div class="col-11">
                                                <asp:DropDownList class="form-control " ID="cboName" runat="server" required></asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td colspan="6" style="width: 240px !important;">
                                        <div class="row">

                                            <div class="col-3">
                                                <asp:Label ID="Label1" CssClass="form-label" AssociatedControlID="cboBranch" runat="server" Text="สาขา" />
                                            </div>
                                            <div class="col-9">
                                                <asp:DropDownList class="form-control" ID="cboBranch" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td colspan="6" style="width: 240px !important;">
                                        <div class="row">

                                            <div class="col-3">
                                                <asp:Label ID="Label6" CssClass="form-label" AssociatedControlID="sda" runat="server" Text="เลขที่" />
                                            </div>
                                            <div class="col-9">
                                                <asp:TextBox class="form-control" ID="sda" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="width: 240px !important;">
                                        <div class="row">
                                            <div class="col-3">
                                                <asp:Label ID="lbApprovalcode" CssClass="form-label" AssociatedControlID="cboSection" runat="server" Text="แผนก" />
                                            </div>
                                            <div class="col-9">
                                                <asp:DropDownList class="form-control" ID="cboSection" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td colspan="6" style="width: 240px !important;">
                                        <div class="row">
                                            <div class="col-3">
                                                <asp:Label ID="Label3" CssClass="form-label" AssociatedControlID="cboDepartment" runat="server" Text="ฝ่าย" />
                                            </div>
                                            <div class="col-9">
                                                <asp:DropDownList class="form-control" ID="cboDepartment" AutoPostBack="True"
                                                    runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td colspan="6" style="width: 240px !important;">
                                        <div class="row">
                                            <div class="col-3">
                                                <asp:Label ID="Label4" CssClass="form-label" AssociatedControlID="sda" runat="server" Text="สายงาน" />
                                            </div>
                                            <div class="col-9">
                                                <asp:DropDownList class="form-control " ID="DropDownList3" runat="server" required></asp:DropDownList>
                                            </div>
                                        </div>
                                    </td>
                                    <td colspan="6" style="width: 240px !important;">
                                        <div class="row">
                                            <div class="col-3">
                                                <asp:Label ID="Label7" CssClass="form-label" AssociatedControlID="sda" runat="server" Text="วันที่" />
                                            </div>
                                            <div class="col-9">
                                                <asp:TextBox class="form-control font-weight-bold" ID="txtRequestDate" runat="server" required></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                </tr>

                                <tr>

                                    <td colspan="18" style="width: 720px !important;">
                                        <div class="row">
                                            <div class="col-1">
                                                <asp:Label ID="Label5" CssClass="form-label" AssociatedControlID="cboVendor" runat="server" Text="ผู้รับเงิน" />
                                            </div>
                                            <div class="col-11">
                                                <asp:DropDownList class="form-control " ID="cboVendor" runat="server" required></asp:DropDownList>
                                            </div>
                                        </div>

                                    </td>
                                    <td rowspan="2" colspan="6" style="width: 240px !important;">

                                        <div class="row">
                                            <h5>Due Date</h5>
                                        </div>
                                        <div class="row">
                                            <div class="dueDate" style="height: 60px; margin-left: 10px">
                                                <asp:TextBox class="form-control font-weight-bold text-center" ID="txtDuedate" runat="server" required></asp:TextBox>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>

                                    <td colspan="18" style="width: 720px !important;">
                                        <div class="row">
                                            <div class="col-2">
                                                <asp:Label ID="Label8" CssClass="form-label" runat="server" Text="จ่ายโดย" />
                                            </div>
                                            <div class="col-auto">
                                                <input class="form-check-input chk-img-after" type="checkbox" value="" id="check" runat="server" onchange="valueChangedImgAfter()">
                                                <label class="form-check-label" runat="server" associatedcontrolid="check">
                                                    เช็ค
                                                </label>
                                            </div>
                                            <div class="col-auto">
                                                <input class="form-check-input chk-img-after" type="checkbox" value="" id="Checkbox1" runat="server" onchange="valueChangedImgAfter()">
                                                <label class="form-check-label" runat="server" associatedcontrolid="check">
                                                    แคชเชียร์เช็ค
                                                </label>
                                            </div>
                                            <div class="col-auto">
                                                <input class="form-check-input chk-img-after" type="checkbox" value="" id="Checkbox2" runat="server" onchange="valueChangedImgAfter()">
                                                <label class="form-check-label" runat="server" associatedcontrolid="check">
                                                    TT
                                                </label>
                                            </div>
                                            <div class="col-auto">
                                                <input class="form-check-input chk-img-after" type="checkbox" value="" id="Checkbox3" runat="server" onchange="valueChangedImgAfter()">
                                                <label class="form-check-label" runat="server" associatedcontrolid="check">
                                                    EFT
                                                </label>
                                            </div>
                                            <div class="col-auto">
                                                <input class="form-check-input chk-img-after" type="checkbox" value="" id="Checkbox4" runat="server" onchange="valueChangedImgAfter()">
                                                <label class="form-check-label" runat="server" associatedcontrolid="check">
                                                    หักยอดขาย
                                                </label>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td rowspan="2" colspan="2" style="width: 80px !important;">ลำดับ</td>
                                    <td rowspan="2" colspan="10" style="width: 400px !important;">รายละเอียด</td>
                                    <td rowspan="2" colspan="2" style="width: 80px !important;">รหัสบัญชี</td>
                                    <td colspan="6" style="width: 240px !important;">Dimension</td>
                                    <td rowspan="2" colspan="4" style="width: 160px !important;">จำนวนเงิน</td>

                                </tr>
                                <tr>
                                    <td colspan="2" style="width: 80px !important;">Dep.</td>
                                    <td colspan="2" style="width: 80px !important;">CC.</td>
                                    <td colspan="2" style="width: 80px !important;">PP.</td>

                                </tr>
                                <!--  ############## Detail ############### -->
                                <tbody class="shortArea">

                                    <tr class="draggable detail" draggable="true">
                                        <td colspan="2" style="width: 80px !important;">
                                            <div class="commit text-warning" style="position: absolute; transform: translateX(-150px); transition: .2s;"></div>
                                            1
                                        </td>
                                        <td colspan="10" style="width: 400px !important;">1</td>
                                        <td colspan="2" style="width: 80px !important;">1</td>
                                        <td colspan="2" style="width: 80px !important;">1</td>
                                        <td colspan="2" style="width: 80px !important;">1</td>
                                        <td colspan="2" style="width: 80px !important;">1</td>
                                        <td colspan="4" style="width: 160px !important; display: inline-flex;">1
                                            <a onclick='newwin();' class="addAttach text-primary" style="cursor: pointer; position: absolute; transform: translateX(155px); transition: .2s;">
                                                <i class="fas fa-link"></i><span>แนบเอกสาร</span></a>
                                            <a onclick='newwin();' class="attach ">
                                                <i class="fas fa-link"></i><span>ดูเอกสาร</span></a>
                                        </td>
                                    </tr>
                                    <tr class="draggable detail" draggable="true">
                                        <td colspan="2" style="width: 80px !important;">2</td>
                                        <td colspan="10" style="width: 400px !important;">2</td>
                                        <td colspan="2" style="width: 80px !important;">2</td>
                                        <td colspan="2" style="width: 80px !important;">2</td>
                                        <td colspan="2" style="width: 80px !important;">2</td>
                                        <td colspan="2" style="width: 80px !important;">2</td>
                                        <td colspan="4" style="width: 160px !important;">2</td>
                                    </tr>
                                </tbody>
                                <tr>
                                    <td colspan="2" style="width: 80px !important;">
                                        <asp:TextBox class="form-control" ID="TextBox4" runat="server"></asp:TextBox></td>
                                    <td colspan="10" style="width: 400px !important;">
                                        <asp:TextBox class="form-control" ID="TextBox5" runat="server"></asp:TextBox></td>
                                    <td colspan="2" style="width: 80px !important;">
                                        <asp:TextBox class="form-control" ID="TextBox6" runat="server"></asp:TextBox></td>
                                    <td colspan="2" style="width: 80px !important;">
                                        <asp:TextBox class="form-control" ID="TextBox7" runat="server"></asp:TextBox></td>
                                    <td colspan="2" style="width: 80px !important;">
                                        <asp:TextBox class="form-control" ID="TextBox8" runat="server"></asp:TextBox></td>
                                    <td colspan="2" style="width: 80px !important;">
                                        <asp:TextBox class="form-control" ID="TextBox9" runat="server"></asp:TextBox></td>
                                    <td colspan="4" style="width: 160px !important;">

                                        <asp:TextBox ID="txtCostPrice" class="form-control text-right" runat="server" type="number"></asp:TextBox>

                                    </td>
                                </tr>

                                <!--  ############## End Detail ############### -->
                                <tfoot>
                                    <!--  total -->
                                    <tr>
                                        <td colspan="2" style="width: 80px !important; text-align: right; border-right-width: 0px;">
                                            <h5>รวม
                                            </h5>
                                        </td>
                                        <td colspan="14" style="width: 560px !important; text-align: center; border-left-width: 0px; border-right-width: 0px;">
                                            <h5>
                                                <p id="id01"></p>
                                            </h5>
                                        </td>
                                        <td colspan="4" style="width: 160px !important; border-left-width: 0px;">บาท</td>
                                        <td colspan="4" style="width: 160px !important;" id="total">1,546.39</td>
                                    </tr>
                                    <!--  end total -->
                                    <tr>
                                        <td colspan="6" style="width: 240px !important; border-bottom-width: 0px;">
                                            <h5>ผู้เบิก</h5>
                                        </td>
                                        <td colspan="6" style="width: 240px !important; border-bottom-width: 0px;">
                                            <h5>ผู้รับสินค้า/บริกการ</h5>
                                        </td>
                                        <td colspan="6" style="width: 240px !important; border-bottom-width: 0px;">
                                            <h5>ผู้ตรวจ</h5>
                                        </td>
                                        <td colspan="6" style="width: 240px !important; border-bottom-width: 0px;">
                                            <h5>ผู้อนุมัติ</h5>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="width: 240px !important; border-top-width: 0px; border-bottom-width: 0px;"></td>
                                        <td colspan="6" style="width: 240px !important; border-top-width: 0px; border-bottom-width: 0px;"></td>
                                        <td colspan="6" style="width: 240px !important; border-top-width: 0px; border-bottom-width: 0px;"></td>
                                        <td colspan="6" style="width: 240px !important; border-top-width: 0px; border-bottom-width: 0px;"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="width: 240px !important; border-top-width: 0px;">วันที่</td>
                                        <td colspan="6" style="width: 240px !important; border-top-width: 0px;">วันที่</td>
                                        <td colspan="6" style="width: 240px !important; border-top-width: 0px;">วันที่</td>
                                        <td colspan="6" style="width: 240px !important; border-top-width: 0px;">วันที่</td>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                    </div>
                    <hr />
                </div>

            </div>
        </div>
    </div>
    <script src="<%=Page.ResolveUrl("~/js/Sortable.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/vendor/jquery/jquery.min.js")%>"></script>
    <!-- datetimepicker ต้องไปทั้งชุด-->
    <script src="<%=Page.ResolveUrl("~/datetimepicker/jquery.js")%>"></script>
    <script src="<%=Page.ResolveUrl("~/datetimepicker/build/jquery.datetimepicker.full.min.js")%>"></script>

    <script type="text/javascript">
        jQuery('[id$=txtRequestDate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08'
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });
        jQuery('[id$=txtDuedate]').datetimepicker({
            startDate: '+1971/05/01',//or 1986/12/08'
            timepicker: false,
            scrollInput: false,
            format: 'd/m/Y'
        });

    </script>
    <script>

        $(document).ready(function () {
            const total = document.getElementById("total");
            const element = document.getElementById("id01");
            const Number = CheckNumber(total.textContent);
            console.log(Number);

            if (!isNaN(Number) && (Number - 0) < 9999999.9999) {
                element.innerHTML = " (   " + ArabicNumberToText(total.textContent) + "   )";
            } else {
                element.innerHTML = "รวม ";
            }


            $('.form-control').selectpicker({
                liveSearch: true,
                maxOptions: 1
            });
        });
        function ArabicNumberToText(Number) {
            var Number = CheckNumber(Number);
            var NumberArray = new Array("ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ");
            var DigitArray = new Array("", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน");
            var BahtText = "";
            if (isNaN(Number)) {
                return "ข้อมูลนำเข้าไม่ถูกต้อง";
            } else {
                if ((Number - 0) > 9999999.9999) {
                    return "ข้อมูลนำเข้าเกินขอบเขตที่ตั้งไว้";
                } else {
                    Number = Number.split(".");
                    if (Number[1].length > 0) {
                        Number[1] = Number[1].substring(0, 2);
                    }
                    var NumberLen = Number[0].length - 0;
                    for (var i = 0; i < NumberLen; i++) {
                        var tmp = Number[0].substring(i, i + 1) - 0;
                        if (tmp != 0) {
                            if ((i == (NumberLen - 1)) && (tmp == 1)) {
                                BahtText += "เอ็ด";
                            } else
                                if ((i == (NumberLen - 2)) && (tmp == 2)) {
                                    BahtText += "ยี่";
                                } else
                                    if ((i == (NumberLen - 2)) && (tmp == 1)) {
                                        BahtText += "";
                                    } else {
                                        BahtText += NumberArray[tmp];
                                    }
                            BahtText += DigitArray[NumberLen - i - 1];
                        }
                    }
                    BahtText += "บาท";
                    if ((Number[1] == "0") || (Number[1] == "00")) {
                        BahtText += "ถ้วน";
                    } else {
                        DecimalLen = Number[1].length - 0;
                        for (var i = 0; i < DecimalLen; i++) {
                            var tmp = Number[1].substring(i, i + 1) - 0;
                            if (tmp != 0) {
                                if ((i == (DecimalLen - 1)) && (tmp == 1)) {
                                    BahtText += "เอ็ด";
                                } else
                                    if ((i == (DecimalLen - 2)) && (tmp == 2)) {
                                        BahtText += "ยี่";
                                    } else
                                        if ((i == (DecimalLen - 2)) && (tmp == 1)) {
                                            BahtText += "";
                                        } else {
                                            BahtText += NumberArray[tmp];
                                        }
                                BahtText += DigitArray[DecimalLen - i - 1];
                            }
                        }
                        BahtText += "สตางค์";
                    }
                    return BahtText;
                }
            }
        }

        function CheckNumber(Number) {
            var decimal = false;
            Number = Number.toString();
            Number = Number.replace(/ |,|บาท|฿/gi, '');
            for (var i = 0; i < Number.length; i++) {
                if (Number[i] == '.') {
                    decimal = true;
                }
            }
            if (decimal == false) {
                Number = Number + '.00';
            }
            return Number
        }
    </script>
</asp:Content>
