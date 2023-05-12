using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MusteriSiparis
{
    public partial class sf_Musteriler : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["kullanici_bilgisi"] == null) { Session.Clear(); Response.Redirect("login.aspx"); return; }

            if (!IsPostBack)
            {
                SehirListesi();
                getir();
            }
        }

        public void getir()
        {
            DataTable dt_Musteriler = new DAL.DBOps("connstr").sc(@"select m.id, m.firma_adi, m.musteri_soyadi, m.musteri_adi, m.telefon, m.mail, s.sehir_adi, i.ilce_adi, m.aktif_pasif
from Musteriler m
inner join Sehirler s on s.id = m.sehir_id
inner join Ilceler i on i.sehir_id = s.id and i.id = m.ilce_id where 1=1"
+ (String.IsNullOrEmpty(txt_MusteriAdi.Text) ? "" : " and ( m.musteri_adi like '%" + txt_MusteriAdi.Text + "%' or musteri_soyadi like '%" + txt_MusteriAdi.Text + "%' or firma_adi like '%" + txt_MusteriAdi.Text + "%' )")
+ ((ddl_Sehir.SelectedValue == "0" || ddl_Sehir.SelectedValue == "") ? "" : " and m.sehir_id=" + ddl_Sehir.SelectedValue + "")
+ ((ddl_Ilce.SelectedValue == "0" || ddl_Ilce.SelectedValue == "") ? "" : " and m.ilce_id=" + ddl_Ilce.SelectedValue + ""));

            rpt_Musteriler.DataSource = dt_Musteriler;
            rpt_Musteriler.DataBind();

            for (int i = 0; i < dt_Musteriler.Rows.Count; i++)
            {
                LinkButton btn_Aktif = rpt_Musteriler.Items[i].FindControl("btn_Aktif") as LinkButton;
                LinkButton btn_Pasif = rpt_Musteriler.Items[i].FindControl("btn_Pasif") as LinkButton;

                if (dt_Musteriler.Rows[i]["aktif_pasif"].ToString() == "1")
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

        protected void rpt_Musteriler_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "aktif")
            {
                int sonuc = new DAL.DBOps("connstr").scn(@"update Musteriler set aktif_pasif = 1 where id = @p1", Convert.ToInt32(e.CommandArgument.ToString()));

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
                int sonuc = new DAL.DBOps("connstr").scn(@"update Musteriler set aktif_pasif = 0 where id = @p1", Convert.ToInt32(e.CommandArgument.ToString()));

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

        public void SehirListesi()
        {
            ddl_Sehir.DataTextField = "NAME_";
            ddl_Sehir.DataValueField = "ID_";

            ddl_Sehir.DataSource = new BLL.SehirIlce().SehirListesi();
            ddl_Sehir.SelectedValue = "0";
            ddl_Sehir.DataBind();
        }

        public void IlceListesi(int sehir_id)
        {
            
            ddl_Ilce.DataTextField = "NAME_";
            ddl_Ilce.DataValueField = "ID_";

            ddl_Ilce.DataSource = new BLL.SehirIlce().IlceListesi(sehir_id);
            ddl_Ilce.SelectedValue = "0";
            ddl_Ilce.DataBind();
        }

        protected void ddl_Sehir_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sehir_id;
            Int32.TryParse(ddl_Sehir.SelectedValue, out sehir_id);
            IlceListesi(sehir_id);
        }

        public void ExcelOlarakIndir()
        {
            string fileName = "Müşteriler.xlsx";
            System.IO.FileStream fs = null;
            string path = System.Web.HttpContext.Current.Server.MapPath("/exceldosyasi/") + fileName;
            ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();

            DataTable data = new DataTable();
            data = new DAL.DBOps("connstr").sc(@"select m.firma_adi as 'Firma', m.musteri_adi as 'Adı', m.musteri_soyadi as Soyadı, m.telefon as 'Telefon Numarası', m.mail as 'E-Mail', s.sehir_adi as Şehir, i.ilce_adi as İlçe
from Musteriler m
inner join Sehirler s on s.id = m.sehir_id
inner join Ilceler i on i.sehir_id = s.id and i.id = m.ilce_id where 1=1"
+ (String.IsNullOrEmpty(txt_MusteriAdi.Text) ? "" : " and ( m.musteri_adi like '%" + txt_MusteriAdi.Text + "%' or musteri_soyadi like '%" + txt_MusteriAdi.Text + "%' or firma_adi like '%" + txt_MusteriAdi.Text + "%' )")
+ ((ddl_Sehir.SelectedValue == "0" || ddl_Sehir.SelectedValue == "") ? "" : " and m.sehir_id=" + ddl_Sehir.SelectedValue + "")
+ ((ddl_Ilce.SelectedValue == "0" || ddl_Ilce.SelectedValue == "") ? "" : " and m.ilce_id=" + ddl_Ilce.SelectedValue + ""));

            var ws_AnlasmaSablonu = wbook.Worksheets.Add(data, "Müşteri Listesi");
            wbook.Table("Müşteri Listesi").ShowAutoFilter = false;

            wbook.SaveAs(path);

            System.IO.FileInfo file = new System.IO.FileInfo(path);
            string Outgoingfile = fileName;

            if (file.Exists)
            {
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Outgoingfile);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.WriteFile(file.FullName);
                Response.Flush();
                Response.Close();
            }
        }
        protected void btn_excelolarakver_Click(object sender, EventArgs e)
        {
            ExcelOlarakIndir();
        }
    }
}