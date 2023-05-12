using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MusteriSiparis
{
    public partial class pop_SiparisIptalNedeni : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["kullanici_bilgisi"] == null) { Session.Clear(); Response.Redirect("login.aspx"); return; }

            if (!IsPostBack)
            {
            }
        }

       
        protected void btn_kaydet_Click(object sender, EventArgs e)
        {
            int son = new DAL.DBOps("connstr").scn(@"update SiparisAdimlari set guncelleme_tarihi = @p1, aktif_pasif = 0 where siparis_id = @p2", DateTime.Now, Request["id"]);
            if (son > 0)
            {
                int sonuc = new DAL.DBOps("connstr").scn(@"insert into SiparisAdimlari(kullanici_id,siparis_id,durum_id,durum,aciklama,olusturma_tarihi) values(@p1,@p2,2,'Sipariş İptal Edildi','" + txt_Aciklama.Value + "',@p3)", ((KullaniciBilgisi)Session["kullanici_bilgisi"]).Kullanici_Id, Request["id"], DateTime.Now);

                if (sonuc > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "YeniPencere", @"<script language='javascript'>toastr.success('Başvuru iptal edildi.');</script>");
                    pnl_KayitMesaj.Visible = true;
                    return;
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "YeniPencere", @"<script language='javascript'>toastr.warning('Başvuru iptal edilmedi.');</script>");
                    return;
                }
            }
        }
    }
}