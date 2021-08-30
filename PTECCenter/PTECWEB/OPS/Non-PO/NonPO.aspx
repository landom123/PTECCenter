<%@ Page Title="NonPO" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="NonPO.aspx.vb" Inherits="PTECCENTER.NonPO1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-color: #f0f2f5;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="wrapper">

        <div id="content-wrapper">

            <div class="container">
                <div class="row">
                    <div class="col-lg-6">
                        <div class="card mb-3  shadow-sm" style=" cursor: pointer;" onclick="window.location='../Non-PO/PettyCash/defaultPettyCash.aspx';">
                            <div class="row no-gutters">
                                <div class="col-sm-4">
                                    <img src="http://vpnptec.dyndns.org:10280/OPS_Fileupload/ATT_210800096.jpg" class="card-img h-100" alt="...">
                                </div>
                                <div class="col-sm-8">
                                    <div class="card-body">
                                        <h2 class="card-title">Petty Cash</h2>
                                        <p class="card-text">เงินสดย่อย</p>
                                        <p class="card-text"><small class="text-muted">สร้างรายการ ดูรายงาน หักยอดขาย</small></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-6">
                        <div class="card mb-3  shadow-sm" style=" cursor: pointer;" onclick="window.location='../Non-PO/Payment/defaultPayment.aspx';">
                            <div class="row no-gutters">
                                <div class="col-sm-4">
                                    <img src="http://vpnptec.dyndns.org:10280/OPS_Fileupload/ATT_210800095.jpg" class="card-img h-100" alt="...">
                                </div>
                                <div class="col-sm-8">
                                    <div class="card-body">
                                        <h2 class="card-title">Payment</h2>
                                        <p class="card-text">ใบสำคัญจ่าย</p>
                                        <p class="card-text"><small class="text-muted">สร้างใบ ดูรายงาน</small></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6">
                        <div class="card mb-3  shadow-sm" style=" cursor: pointer;" onclick="window.location='../Non-PO/Advance/defaultAdvance.aspx';">
                            <div class="row no-gutters">
                                <div class="col-sm-4">
                                    <img src="http://vpnptec.dyndns.org:10280/OPS_Fileupload/ATT_210800097.jpg" class="card-img h-100" alt="...">
                                </div>
                                <div class="col-sm-8">
                                    <div class="card-body">
                                        <h2 class="card-title">Advance</h2>
                                        <p class="card-text">ขอเงินออกไปใช้ก่อน</p>
                                        <p class="card-text"><small class="text-muted">เบิก เคลียร์ ดูรายงาน หักยอดขาย</small></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                        <div class="col-xl-3 col-sm-6 mb-3">
                            <div class="card text-white bg-success o-hidden h-100">
                                <div class="card-body">
                                    <div class="card-body-icon">
                                        <i class="fas fa-list-alt"></i>
                                    </div>
                                    <div class="mr-5">Payment</div>
                                </div>
                                <a class="card-footer text-white clearfix small z-1" href="#">
                                    <span class="float-left">View Details</span>
                                    <span class="float-right">
                                        <i class="fas fa-angle-right"></i>
                                    </span>
                                </a>
                            </div>
                        </div>
                </div>


            </div>
        </div>
    </div>
</asp:Content>
