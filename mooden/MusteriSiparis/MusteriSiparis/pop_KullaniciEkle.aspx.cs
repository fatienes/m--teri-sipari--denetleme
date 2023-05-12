using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MusteriSiparis
{
    public partial class pop_KullaniciEkle : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["kullanici_bilgisi"] == null) { Session.Clear(); Response.Redirect("login.aspx"); return; }

            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request["id"]))
                {
                    DataTable dt_Kullanici = new DAL.DBOps("connstr").sc(@"select top 1 adi, soyadi, kullanici_adi, sifre, aktif_pasif from Kullanicilar where id = @p1", Request["id"]);

                    txt_Adi.Text = dt_Kullanici.Rows[0]["adi"].ToString();
                    txt_Soyadi.Text = dt_Kullanici.Rows[0]["soyadi"].ToString();
                    txt_KullaniciAdi.Text = dt_Kullanici.Rows[0]["kullanici_adi"].ToString();
                    txt_Sifre.Attributes["value"] = dt_Kullanici.Rows[0]["sifre"].ToString();
                    txt_Sifre.Text = dt_Kullanici.Rows[0]["sifre"].ToString();
                }
            }
        }

        protected void btn_kaydet_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txt_Adi.Text) || String.IsNullOrEmpty(txt_Soyadi.Text) || String.IsNullOrEmpty(txt_KullaniciAdi.Text) || String.IsNullOrEmpty(txt_Sifre.Text))
            {
                ClientScript.RegisterStartupScript(GetType(), "YeniPencere", @"<script language='javascript'>toastr.warning('Lütfen * ile belirtilen alanları boş bırakmayınız.');</script>");
                return;
            }
            else
            {
                if (String.IsNullOrEmpty(Request["id"]))
                {
                    int sonuc = new DAL.DBOps("connstr").scn(@"insert into Kullanicilar(adi, soyadi, kullanici_adi, sifre) values(@p1,@p2,@p3,@p4)", txt_Adi.Text, txt_Soyadi.Text, txt_KullaniciAdi.Text, txt_Sifre.Text);

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
                    int sonuc = new DAL.DBOps("connstr").scn(@"update Kullanicilar set adi = @p1, soyadi = @p2, kullanici_adi = @p3, sifre = @p4, guncelleme_tarihi = @p5 where id = @p6", txt_Adi.Text, txt_Soyadi.Text, txt_KullaniciAdi.Text, txt_Sifre.Text, DateTime.Now, Request["id"]);

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