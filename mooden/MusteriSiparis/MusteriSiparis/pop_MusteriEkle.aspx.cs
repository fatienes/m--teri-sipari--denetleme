using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MusteriSiparis
{
    public partial class pop_MusteriEkle : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["kullanici_bilgisi"] == null) { Session.Clear(); Response.Redirect("login.aspx"); return; }

            if (!IsPostBack)
            {
                SehirListesi();
                if (!String.IsNullOrEmpty(Request["id"]))
                {
                    DataTable dt_Kullanici = new DAL.DBOps("connstr").sc(@"select top 1 firma_adi, musteri_adi, musteri_soyadi, telefon, mail, adres, sehir_id, ilce_id, aktif_pasif from Musteriler where id = @p1", Request["id"]);

                    txt_MusteriAdi.Text = dt_Kullanici.Rows[0]["musteri_adi"].ToString();
                    txt_FirmaAdi.Text = dt_Kullanici.Rows[0]["firma_adi"].ToString();
                    txt_MusteriSoyadi.Text= dt_Kullanici.Rows[0]["musteri_soyadi"].ToString();
                    txt_Mail.Text= dt_Kullanici.Rows[0]["mail"].ToString();
                    txt_Telefon.Text = dt_Kullanici.Rows[0]["telefon"].ToString();
                    txt_Adres.Value = dt_Kullanici.Rows[0]["adres"].ToString();
                    ddl_Sehir.SelectedValue = dt_Kullanici.Rows[0]["sehir_id"].ToString();
                    int sehir_id;
                    Int32.TryParse(ddl_Sehir.SelectedValue, out sehir_id);
                    IlceListesi(sehir_id, dt_Kullanici.Rows[0]["ilce_id"].ToString());
                }
            }
        }

        public void SehirListesi()
        {
            ddl_Sehir.DataTextField = "NAME_";
            ddl_Sehir.DataValueField = "ID_";

            ddl_Sehir.DataSource = new BLL.SehirIlce().SehirListesi();
            ddl_Sehir.SelectedValue = "0";
            ddl_Sehir.DataBind();
        }
        
        public void IlceListesi(int sehir_id, string ilce_id)
        {
            ddl_Ilce.DataTextField = "NAME_";
            ddl_Ilce.DataValueField = "ID_";

            ddl_Ilce.DataSource = new BLL.SehirIlce().IlceListesi(sehir_id);
            ddl_Ilce.SelectedValue = ilce_id;
            ddl_Ilce.DataBind();
        }

        protected void btn_kaydet_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txt_Telefon.Text) || String.IsNullOrEmpty(txt_Mail.Text) || String.IsNullOrEmpty(txt_MusteriAdi.Text))
            {
                ClientScript.RegisterStartupScript(GetType(), "YeniPencere", @"<script language='javascript'>toastr.warning('Lütfen * ile belirtilen alanları boş bırakmayınız.');</script>");
                return;
            }
            else
            {
                if (String.IsNullOrEmpty(Request["id"]))
                {
                    int sonuc = new DAL.DBOps("connstr").scn(@"insert into Musteriler(kullanici_id, firma_adi, musteri_adi, musteri_soyadi, telefon, mail, adres, sehir_id, ilce_id) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", ((KullaniciBilgisi)Session["kullanici_bilgisi"]).Kullanici_Id, txt_FirmaAdi.Text, txt_MusteriAdi.Text, txt_MusteriSoyadi.Text, txt_Telefon.Text, txt_Mail.Text, txt_Adres.Value, ddl_Sehir.SelectedValue, ddl_Ilce.SelectedValue);

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
                    int sonuc = new DAL.DBOps("connstr").scn(@"update Musteriler set kullanici_id = @p1, firma_adi = @p2, musteri_adi = @p3, musteri_soyadi = @p4, telefon = @p5, mail = @p6, adres = @p7, sehir_id = @p8, ilce_id = @p9, guncelleme_tarihi = @p10 where id = @p11", ((KullaniciBilgisi)Session["kullanici_bilgisi"]).Kullanici_Id, txt_FirmaAdi.Text, txt_MusteriAdi.Text, txt_MusteriSoyadi.Text, txt_Telefon.Text, txt_Mail.Text, txt_Adres.Value, ddl_Sehir.SelectedValue, ddl_Ilce.SelectedValue, DateTime.Now, Request["id"]);

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

        protected void ddl_Sehir_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sehir_id;
            Int32.TryParse(ddl_Sehir.SelectedValue, out sehir_id);
            IlceListesi(sehir_id, "0");
            
        }

    }
}