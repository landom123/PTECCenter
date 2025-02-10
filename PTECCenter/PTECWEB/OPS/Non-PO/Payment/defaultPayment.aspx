<%@ Page Title="Payment" Language="vb" AutoEventWireup="false" MasterPageFile="~/site.Master" CodeBehind="defaultPayment.aspx.vb" Inherits="PTECCENTER.defaultPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-color: #f0f2f5;
        }
        .container{
            font-family:thonburi;
        }
        .slide-up {
            display: flex;
            justify-content: center;
            align-items: center;
            position: relative;
            font-size: 2.5rem;
            font-weight: bold;
            transition: .2s;
            overflow: hidden;
        }

        .slide-up i {
            
            font-size: 3rem;
            position: absolute;
            transform: translateY(300%);
            transition: .2s;
        }

        .slide-up span {
            display: flex;
            transition: .2s;
        }

        .slide-up:hover i {
            transform: translateY(0);
        }

        .slide-up:hover span {
            transform: translateY(-300%);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="wrapper" class="h-100">

        <div id="content-wrapper">

            <div class="container">
                <div class="row">
                    <div class="col">
                        <a href="../NonPO.aspx" class="btn  back mb-4" style="color: red; font-size: 2rem; font-weight: bold;">
                            <i class="fas fa-arrow-circle-left"></i>
                        </a>
                    </div>
                </div>

                <div class="row mb-2">
                    <div class="col" style="height: 150px;">
                        <a href="Payment.aspx" class="btn btn-primary slide-up  shadow w-100 h-100 ">
                            <i class="fas fa-plus-square"></i>
                            <span>สร้างรายการ Payment
                            </span>
                        </a>
                    </div>
                </div>
                <div class="row mb-2">
                    <div class="col" style="height: 150px;">
                        <a href="Payment.aspx" class="btn slide-up  shadow w-100 h-100 " style="background-color: #F1FAEE; color: #000;">
                            <i class="fas fa-list-alt"></i>
                            <span>รายการของฉัน
                            </span>
                        </a>
                    </div>
                </div>
                <div class="row mb-2">
                    <div class="col" style="height: 150px;">
                        <a href="Payment.aspx" class="btn btn-info slide-up  shadow w-100 h-100 " style="background-color: #E63946; color: #000;">
                            <i class="fas fa-users"></i>
                            <span>สำหรับผู้ปฏิบัติงาน
                            </span>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
