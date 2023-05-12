using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MusteriSiparis
{
    public partial class pop_UrunEkle : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["kullanici_bilgisi"] == null) { Session.Clear(); Response.Redirect("login.aspx"); return; }

            if (!IsPostBack)
            {
                UrunTuruListesi();
                if (!String.IsNullOrEmpty(Request["id"]))
                {
                    DataTable dt_Urunler = new DAL.DBOps("connstr").sc(@"select top 1 select u.id, u.urun_adi, ut.tur_adi, ut.adet, ut.fiyat, ut.aktif_pasif
from Kullanicilar 
inner join UrunTuru ut on ut.id = u.urun_turu
where 1=1 and id = @p1", Request["id"]);

                    ddl_UrunTuru.SelectedValue = dt_Urunler.Rows[0]["tur_adi"].ToString();
                    txt_UrunAdi.Text = dt_Urunler.Rows[0]["urun_adi"].ToString();
                    txt_Fiyat.Text = dt_Urunler.Rows[0]["fiyat"].ToString();
                    txt_Adet.Text = dt_Urunler.Rows[0]["adet"].ToString();
                }
            }
        }

        public void UrunTuruListesi()
        {
            ddl_UrunTuru.DataTextField = "NAME_";
            ddl_UrunTuru.DataValueField = "ID_";

            ddl_UrunTuru.DataSource = new BLL.Urun().UrunTuruListesi();
            ddl_UrunTuru.SelectedValue = "0";
            ddl_UrunTuru.DataBind();
        }

        protected void btn_kaydet_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txt_UrunAdi.Text) || String.IsNullOrEmpty(txt_Fiyat.Text) || String.IsNullOrEmpty(txt_Adet.Text) || String.IsNullOrEmpty(ddl_UrunTuru.SelectedValue))
            {
                ClientScript.RegisterStartupScript(GetType(), "YeniPencere", @"<script language='javascript'>toastr.warning('Lütfen * ile belirtilen alanları boş bırakmayınız.');</script>");
                return;
            }
            else
            {
                Decimal fiyat;
                Decimal.TryParse(txt_Fiyat.Text, out fiyat);

                if (String.IsNullOrEmpty(Request["id"]))
                {
                    int sonuc = new DAL.DBOps("connstr").scn(@"insert into Urunler(urun_adi, urunturu_id, adet, fiyat, kullanici_id) values(@p1,@p2,@p3,@p4,@p5)", txt_UrunAdi.Text, ddl_UrunTuru.SelectedValue, txt_Adet.Text, fiyat, ((KullaniciBilgisi)Session["kullanici_bilgisi"]).Kullanici_Id);

                    if (sonuc > 0)
                    {
                        pnl_KayitMesaj.Visible = true;
                        pnl_yeni.Visible = false;
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "YeniPencere", @"<script language='javascript'>toastr.warning('Departman ekleme başarısız');</script>");
                        return;
                    }
                }
                else
                {
                    int sonuc = new DAL.DBOps("connstr").scn(@"update Urunler set urun_adi = @p1, tur_adi = @p2, adet = @p3, fiyat = @p4, kullanici_id = @p5, guncelleme_tarihi = @p6 where id = @p7", txt_UrunAdi.Text, ddl_UrunTuru.SelectedValue, txt_Adet.Text, fiyat, ((KullaniciBilgisi)Session["kullanici_bilgisi"]).Kullanici_Id, DateTime.Now, Request["id"]);

                    if (sonuc > 0)
                    {
                        pnl_GuncellemeMesaj.Visible = true;
                        pnl_yeni.Visible = false;
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "YeniPencere", @"<script language='javascript'>toastr.error('Departman güncelleme başarısız');</script>");
                        return;
                    }
                }

            }
        }
    }
}