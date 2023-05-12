using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MusteriSiparis
{
    public partial class pop_UrunTuruEkle : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["kullanici_bilgisi"] == null) { Session.Clear(); Response.Redirect("login.aspx"); return; }

            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request["id"]))
                {
                    DataTable dt_Kullanici = new DAL.DBOps("connstr").sc(@"select top 1 tur_adi from UrunTuru where id = @p1", Request["id"]);

                    txt_UrunTuruAdi.Text = dt_Kullanici.Rows[0]["tur_adi"].ToString();
                }
            }
        }

        protected void btn_kaydet_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txt_UrunTuruAdi.Text))
            {
                ClientScript.RegisterStartupScript(GetType(), "YeniPencere", @"<script language='javascript'>toastr.warning('Lütfen * ile belirtilen alanları boş bırakmayınız.');</script>");
                return;
            }
            else
            {
                if (String.IsNullOrEmpty(Request["id"]))
                {
                    int sonuc = new DAL.DBOps("connstr").scn(@"insert into UrunTuru(tur_adi, kullanici_id) values(@p1,@p2)", txt_UrunTuruAdi.Text, ((KullaniciBilgisi)Session["kullanici_bilgisi"]).Kullanici_Id);

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
                    int sonuc = new DAL.DBOps("connstr").scn(@"update UrunTuru set tur_adi = @p1, kullanici_id = @p2, guncelleme_tarihi = @p3 where id = @p4", txt_UrunTuruAdi.Text, ((KullaniciBilgisi)Session["kullanici_bilgisi"]).Kullanici_Id, DateTime.Now, Request["id"]);

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