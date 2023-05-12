using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MusteriSiparis
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Giris_Click(object sender, EventArgs e)
        {
            Session["kb"] = new List<KullaniciBilgisi> { };
            Session["kullanici_bilgisi"] = null;

            if (String.IsNullOrEmpty(txt_Tc.Text) || String.IsNullOrEmpty(txt_Sifre.Text))
            {
                ClientScript.RegisterStartupScript(GetType(), "YeniPencere", @"<script language='javascript'>toastr.warning('Lütfen tc veya şifrenizi giriniz.');</script>");
                return;
            }
            else
            {

                DataTable dt = new DAL.DBOps("ConnStr").sc(@"select top 1 adi, soyadi, id from Kullanicilar where aktif_pasif=1 and kullanici_adi=@p1 and sifre=@p2", txt_Tc.Text, txt_Sifre.Text);

                if (dt.Rows.Count <= 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "YeniPencere", @"<script language='javascript'>toastr.warning('Lütfen tc veya şifrenizi kontrol ediniz.');</script>");
                    return;
                }
                else
                {
                    KullaniciBilgisi kb = new KullaniciBilgisi();
                    kb.AdSoyad = dt.Rows[0]["adi"].ToString() + " " + dt.Rows[0]["soyadi"].ToString();
                    kb.Kullanici_Id = Int32.Parse(dt.Rows[0]["id"].ToString());

                    ((List<KullaniciBilgisi>)Session["kb"]).Add(kb);

                    if (((List<KullaniciBilgisi>)Session["kb"]).Count > 0)
                    {
                        Session["kullanici_bilgisi"] = ((List<KullaniciBilgisi>)Session["kb"])[0];

                        Response.Redirect("inside.aspx");
                        return;

                    }
                }

            }
        }
    }
}