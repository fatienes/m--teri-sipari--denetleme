using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MusteriSiparis
{
    public partial class sf_Urunler : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["kullanici_bilgisi"] == null) { Session.Clear(); Response.Redirect("login.aspx"); return; }

            if (!IsPostBack)
            {
                UrunTuruListesi();
                getir();
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

        public void getir()
        {
            DataTable dt_Urunler = new DAL.DBOps("connstr").sc(@"select u.id, u.urun_adi, ut.tur_adi, u.adet, u.fiyat, ut.aktif_pasif
from Urunler u 
inner join UrunTuru ut on ut.id = u.urunturu_id
where 1=1"
+ (String.IsNullOrEmpty(txt_UrunAdi.Text) ? "" : " and u.urun_adi like '%" + txt_UrunAdi.Text + "%'")
+ ((ddl_UrunTuru.SelectedValue == "0" || ddl_UrunTuru.SelectedValue == "") ? "" : " and u.urunturu_id=" + ddl_UrunTuru.SelectedValue + ""));

            rpt_Urunler.DataSource = dt_Urunler;
            rpt_Urunler.DataBind();

            for (int i = 0; i < dt_Urunler.Rows.Count; i++)
            {
                LinkButton btn_Aktif = rpt_Urunler.Items[i].FindControl("btn_Aktif") as LinkButton;
                LinkButton btn_Pasif = rpt_Urunler.Items[i].FindControl("btn_Pasif") as LinkButton;

                if (dt_Urunler.Rows[i]["aktif_pasif"].ToString() == "1")
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

        protected void rpt_Urunler_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "aktif")
            {
                int sonuc = new DAL.DBOps("connstr").scn(@"update Urunler set aktif_pasif = 1 where id = @p1", Convert.ToInt32(e.CommandArgument.ToString()));

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
                int sonuc = new DAL.DBOps("connstr").scn(@"update Urunler set aktif_pasif = 0 where id = @p1", Convert.ToInt32(e.CommandArgument.ToString()));

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

        public void ExcelOlarakIndir()
        {
            string fileName = "Ürünler.xlsx";
            System.IO.FileStream fs = null;
            string path = System.Web.HttpContext.Current.Server.MapPath("/exceldosyasi/") + fileName;
            ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();

            DataTable data = new DataTable();
            data = new DAL.DBOps("connstr").sc(@"select u.urun_adi as 'Ürün Adı', ut.tur_adi as 'Ürün Türü', u.adet as Adet, u.fiyat as Fiyat
from Urunler u 
inner join UrunTuru ut on ut.id = u.urunturu_id
where 1=1"
+ (String.IsNullOrEmpty(txt_UrunAdi.Text) ? "" : " and u.urun_adi like '%" + txt_UrunAdi.Text + "%'")
+ ((ddl_UrunTuru.SelectedValue == "0" || ddl_UrunTuru.SelectedValue == "") ? "" : " and u.urunturu_id=" + ddl_UrunTuru.SelectedValue + ""));

            var ws_AnlasmaSablonu = wbook.Worksheets.Add(data, "Ürünler Listesi");
            wbook.Table("Ürünler Listesi").ShowAutoFilter = false;

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