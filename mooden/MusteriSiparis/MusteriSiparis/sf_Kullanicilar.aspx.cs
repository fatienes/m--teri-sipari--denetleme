using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MusteriSiparis
{
    public partial class sf_Kullanicilar : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["kullanici_bilgisi"] == null) { Session.Clear(); Response.Redirect("login.aspx"); return; }

            if (!IsPostBack)
            {
                getir();
            }
        }

        public void getir()
        {
            DataTable dt_Kullanicilar = new DAL.DBOps("connstr").sc(@"select id, adi, soyadi, kullanici_adi, aktif_pasif
from Kullanicilar"
+ (String.IsNullOrEmpty(txt_Kullanici.Text) ? "" : " where ( adi like '%" + txt_Kullanici.Text + "%' or soyadi like '%" + txt_Kullanici.Text + "%' )"));

            rpt_Kullanicilar.DataSource = dt_Kullanicilar;
            rpt_Kullanicilar.DataBind();

            for (int i = 0; i < dt_Kullanicilar.Rows.Count; i++)
            {
                LinkButton btn_Aktif = rpt_Kullanicilar.Items[i].FindControl("btn_Aktif") as LinkButton;
                LinkButton btn_Pasif = rpt_Kullanicilar.Items[i].FindControl("btn_Pasif") as LinkButton;

                if (dt_Kullanicilar.Rows[i]["aktif_pasif"].ToString() == "1")
                {
                    btn_Pasif.Visible = true;
                }
                else
                {
                    btn_Aktif.Visible = true;
                }
            }
        }

        protected void btn_Ara_Click(object sender, EventArgs e)
        {
            getir();
        }

        protected void rpt_Kullanicilar_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "aktif")
            {
                int sonuc = new DAL.DBOps("connstr").scn(@"update Kullanicilar set aktif_pasif = 1 where id = @p1", Convert.ToInt32(e.CommandArgument.ToString()));

                if (sonuc > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "YeniPencere", @"<script language='javascript'>toastr.success('Personel durumu aktif olarak güncellenmiştir.');</script>");
                    getir();
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "YeniPencere", @"<script language='javascript'>toastr.warning('Personel durumu aktif olarak güncellenmemiştir.');</script>");
                    return;
                }
            }

            if (e.CommandName == "pasif")
            {
                int sonuc = new DAL.DBOps("connstr").scn(@"update Kullanicilar set aktif_pasif = 0 where id = @p1", Convert.ToInt32(e.CommandArgument.ToString()));

                if (sonuc > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "YeniPencere", @"<script language='javascript'>toastr.success('Personel durumu pasif olarak güncellenmiştir.');</script>");
                    getir();
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "YeniPencere", @"<script language='javascript'>toastr.warning('Personel durumu pasif olarak güncellenmemiştir.');</script>");
                    return;
                }
            }
        }
    }
}