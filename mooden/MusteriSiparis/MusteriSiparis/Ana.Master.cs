using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MusteriSiparis
{
    public partial class Ana : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["kullanici_bilgisi"] == null) { Session.Clear(); Response.Redirect("login.aspx"); return; }

            if (!IsPostBack)
            {
                menugetir();
            }
        }
        public void menugetir()
        {
            ltr_menu.Text = "";

            ltr_menu.Text += @"   
        <div class=""main-menu"">
            <div class=""scroll"">
                <ul class=""list-unstyled"">
                    <li id=""nav-log-dashboard"">
                        <a href=""inside.aspx"">
                            <i class=""iconsminds-optimization""></i>
                            <span>Anasayfa</span>
                        </a>
                    </li>
                    <li id=""nav-log-Siparisler"">
                        <a href=""sf_Siparisler.aspx"">
                            <i class=""iconsminds-user""></i> Siparişler
                        </a>
                    </li>
                    <li id=""nav-log-Musteriler"">
                        <a href=""sf_Musteriler.aspx"">
                            <i class=""iconsminds-building""></i> Müşteriler
                        </a>
                    </li>
                    <li id=""nav-log-Urunler"">
                        <a href=""sf_Urunler.aspx"">
                            <i class=""iconsminds-notepad""></i> Ürünler
                        </a>
                    </li>
                    <li id=""nav-log-UrunTuru"">
                        <a href=""sf_UrunTuru.aspx"">
                            <i class=""iconsminds-user""></i> Ürün Türü
                        </a>
                    </li>
                    <li id=""nav-log-Kullanicilar"">
                        <a href=""sf_Kullanicilar.aspx"" >
                            <i class=""iconsminds-calendar-4""></i> Kullanıcılar
                        </a>
                    </li>
                </ul>
            </div>
        </div>
 ";

            lbl_adisoyadi.Text = ((KullaniciBilgisi)Session["kullanici_bilgisi"]).AdSoyad;

            //}

        }
    }
}