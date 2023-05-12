﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pop_UrunTuruEkle.aspx.cs" Inherits="MusteriSiparis.pop_UrunTuruEkle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link rel="stylesheet" href="font/iconsmind-s/css/iconsminds.css" />
    <link rel="stylesheet" href="font/simple-line-icons/css/simple-line-icons.css" />
    
    <link rel="stylesheet" href="css/vendor/bootstrap.min.css" />
    <link rel="stylesheet" href="css/vendor/bootstrap.rtl.only.min.css" />
    <link rel="stylesheet" href="css/vendor/component-custom-switch.min.css" />
    <link rel="stylesheet" href="css/vendor/perfect-scrollbar.css" />
    <link href="css/toastr.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="css/main.css" />
    <link rel="stylesheet" href="css/dore.light.bluenavy.css" />

    <script src="js/vendor/jquery-3.3.1.min.js"></script>
    <script src="js/vendor/bootstrap.bundle.min.js"></script>
</head>
<body">
    <form id="form1" runat="server">
        <asp:Panel ID="pnl_yeni" runat="server">
        <div class="container-fluid">
            <div class="col-lg-12 mt-3 text-center">
                        <h6 class="m-0 font-weight-bold" style="color:#900604">Ürün Türü Bilgileri</h6>
                    </div>
                <div class="row mt-3">
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Adı<span style="color: red"><b>*</b></span></label>
                                <asp:TextBox ID="txt_UrunTuruAdi" runat="server" CssClass="form-control" placeholder="" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>&nbsp;</label>
                                <asp:Button ID="btn_kaydet" runat="server" Text="Kaydet" CssClass="btn btn-danger btn-block" style="background-color:#900604" OnClick="btn_kaydet_Click" OnClientClick="return KontrolEt();" />
                            </div>
                        </div>
                    </div>
                   
        </div>
    </asp:Panel>

        <asp:Panel ID="pnl_KayitMesaj" runat="server" Visible="false">
            <div class="row">
                <div class="col-lg-3"></div>
                <div class="col-lg-6" style="text-align: center">
                    <br />
                    <br />
                    <img src="img/ok.png" />
                    <br />
                    <br />
                    <h2>Ürün türü başarıyla kaydedilmiştir.</h2>
                </div>
                <div class="col-lg-3"></div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnl_GuncellemeMesaj" runat="server" Visible="false">
            <div class="row">
                <div class="col-lg-3"></div>
                <div class="col-lg-6" style="text-align: center">
                    <br />
                    <br />
                    <img src="img/ok.png" />
                    <br />
                    <br />
                    <h2>Ürün türü başarıyla güncellenmiştir.</h2>
                </div>
                <div class="col-lg-3"></div>
            </div>
        </asp:Panel>
            </form>
    <script src="js/vendor/bootstrap-datepicker.js"></script>
    <script src="js/vendor/perfect-scrollbar.min.js"></script>
    <script src="js/vendor/mousetrap.min.js"></script>
    <script src="js/dore.script.js"></script>
    <script src="js/scripts.single.theme.js"></script>
    <script src="js/toastr.min.js"></script>

</body>
</html>
