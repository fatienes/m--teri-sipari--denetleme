<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="MusteriSiparis.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1"/>
    <meta name="description" content="" />
    <meta name="author" content="" />

    <title>MOODEN</title>

    <link rel="stylesheet" href="font/iconsmind-s/css/iconsminds.css" />
    <link rel="stylesheet" href="font/simple-line-icons/css/simple-line-icons.css" />

    <link rel="stylesheet" href="css/vendor/bootstrap.min.css" />
    <link rel="stylesheet" href="css/vendor/bootstrap.rtl.only.min.css" />
    <link rel="stylesheet" href="css/vendor/bootstrap-float-label.min.css" />
    <link href="css/toastr.css" rel="stylesheet" />
    <link rel="stylesheet" href="css/main.css" />
    <script src="js/vendor/jquery-3.3.1.min.js"></script>
    <script src="js/vendor/bootstrap.bundle.min.js"></script>
</head>
    <body class="background show-spinner no-footer">
    <div class="fixed-background"></div>
    <main>
        <div class="container">
            <div class="row h-100">
                <div class="col-12 col-md-10 mx-auto my-auto">
                    <div class="card auth-card">
                        <div class="position-relative image-side ">

                            <p class=" text-white h2">MOODEN</p>

                            <p class="white mb-0">
                                
                            </p>
                        </div>
                        <div class="form-side">
                            <h1 class="text-center"><a href="#">
                                <span class="text-center">MOODEN</span>
                            </a></h1>
                            <h6 class="mb-4 d-none">Login</h6>
                            <form id="frm_Giris" runat="server">
                                <label class="form-group has-float-label mb-4">
                                    <asp:TextBox ID="txt_Tc" CssClass="form-control" runat="server"></asp:TextBox>
                                    <span>Kullanıcı Adı</span>
                                </label>

                                <label class="form-group has-float-label mb-4">
                                    <asp:TextBox ID="txt_Sifre" CssClass="form-control" type="password" runat="server"></asp:TextBox>
                                    <span>Şifre</span>
                                </label>
                                <div class="d-flex justify-content-between align-items-center">
                                    <a href="#"></a>
                                    <asp:Button ID="btn_Giris" OnClick="btn_Giris_Click" CssClass="btn btn-primary btn-lg btn-shadow" runat="server" Text="Giriş" />
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
        <script src="js/vendor/jquery-3.3.1.min.js"></script>
    <script src="js/vendor/bootstrap.bundle.min.js"></script>
        
    <script src="js/dore.script.js"></script>
    <script src="js/scripts.js"></script>
        <script src="js/toastr.min.js"></script>
 </body>
</html>