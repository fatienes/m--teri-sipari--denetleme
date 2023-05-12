using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MusteriSiparis
{
    public partial class sf_Siparisler : Page
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

        public void getir()
        {
            DataTable dt_Siparisler = new DAL.DBOps("connstr").sc(@"select s.id, s.siparis_no, m.firma_adi, ut.tur_adi, 
u.urun_adi, s.adet, s.fiyat as toplam_fiyat, u.fiyat, s.siparis_tarihi, sd.durum, s.aktif_pasif, sd.durum_id
from Siparisler s
inner join Urunler u on s.urun_id = u.id
inner join UrunTuru ut on ut.id= s.urunturu_id and u.urunturu_id = ut.id
inner join Musteriler m on m.id= s.musteri_id
inner join SiparisAdimlari sd on s.id= sd.siparis_id
where 1=1 "
+ (String.IsNullOrEmpty(txt_SiparisNo.Text) ? "" : " and siparis_no like '%" + txt_SiparisNo.Text + "%'")
+ ((ddl_UrunTuru.SelectedValue == "0" || ddl_UrunTuru.SelectedValue == "") ? "" : " and s.urunturu_id=" + ddl_UrunTuru.SelectedValue + "")
+ ((ddl_Urun.SelectedValue == "0" || ddl_Urun.SelectedValue == "") ? "" : " and s.urun_id=" + ddl_Urun.SelectedValue + ""));


            rpt_Siparisler.DataSource = dt_Siparisler;
            rpt_Siparisler.DataBind();

            for (int i = 0; i < dt_Siparisler.Rows.Count; i++)
            {
                LinkButton btn_Onayla = rpt_Siparisler.Items[i].FindControl("btn_Onayla") as LinkButton;
                LinkButton btn_IptalEt = rpt_Siparisler.Items[i].FindControl("btn_IptalEt") as LinkButton;

                btn_Onayla.Visible = false;
                btn_IptalEt.Visible = false;

                if (dt_Siparisler.Rows[i]["durum_id"].ToString() == "0")
                {
                    btn_Onayla.Visible = true;
                    btn_IptalEt.Visible = true;
                }
            }
        }

        protected void btn_Ara_Click(object sender, EventArgs e)
        {
            getir();
        }

        protected void rpt_Siparisler_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "onayla")
            {
                int sonuc = new DAL.DBOps("connstr").scn(@"update Siparisler set aktif_pasif = 1 where id = @p1", Convert.ToInt32(e.CommandArgument.ToString()));

                int siparis_adimleri = new DAL.DBOps("connstr").scn(@"insert into SiparisAdimlari(kullanici_id,siparis_id,durum,durum_id,aciklama)
values(@p1,@p2,'Sipariş Onaylandı','1','Sipariş onaylandı, müşyeriye gönderilmeyi bekliyor')", ((KullaniciBilgisi)Session["kullanici_bilgisi"]).Kullanici_Id, Convert.ToInt32(e.CommandArgument.ToString()));

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
        }

        public void UrunTuruListesi()
        {
            ddl_UrunTuru.DataTextField = "NAME_";
            ddl_UrunTuru.DataValueField = "ID_";

            ddl_UrunTuru.DataSource = new BLL.Urun().UrunTuruListesi();
            ddl_UrunTuru.SelectedValue = "0";
            ddl_UrunTuru.DataBind();
        }

        public void UrunListesi(int urunturu_id)
        {

            ddl_Urun.DataTextField = "NAME_";
            ddl_Urun.DataValueField = "ID_";

            ddl_Urun.DataSource = new BLL.Urun().UrunListesi(urunturu_id);
            ddl_Urun.SelectedValue = "0";
            ddl_Urun.DataBind();
        }

        protected void ddl_UrunTuru_SelectedIndexChanged(object sender, EventArgs e)
        {
            int urunturu_id;
            Int32.TryParse(ddl_UrunTuru.SelectedValue, out urunturu_id);
            UrunListesi(urunturu_id);
        }

        public void ExcelOlarakIndir()
        {
            string fileName = "Müşteriler.xlsx";
            System.IO.FileStream fs = null;
            string path = System.Web.HttpContext.Current.Server.MapPath("/exceldosyasi/") + fileName;
            ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();

            DataTable data = new DAL.DBOps("connstr").sc(@"select s.siparis_no as 'Sipariş No' , m.firma_adi as 'Firma Adı', 
ut.tur_adi as 'Ürün Türü', u.urun_adi as 'Ürün Adı', s.adet as Adet, u.fiyat as Fiyat, s.fiyat as 'Toplam Fiyat', s.siparis_tarihi as 'Sipariş Tarihi', 
s.durum_adi as 'Sipariş Durumu'
from Siparisler s
inner join Urunler u on s.urun_id = u.id
inner join UrunTuru ut on ut.id= s.urunturu_id and u.urun_turu = ut.id
inner join Musteriler m on m.id= m.musteri_id
inner join SiparisDurumlari sd on sd.id= s.siparisdurum_id
where 1=1 "
+ (String.IsNullOrEmpty(txt_SiparisNo.Text) ? "" : " and siparis_no like '%" + txt_SiparisNo.Text + "%'")
+ ((ddl_UrunTuru.SelectedValue == "0" || ddl_UrunTuru.SelectedValue == "") ? "" : " and s.urunturu_id=" + ddl_UrunTuru.SelectedValue + "")
+ ((ddl_Urun.SelectedValue == "0" || ddl_Urun.SelectedValue == "") ? "" : " and s.urun_id=" + ddl_Urun.SelectedValue + ""));

            var ws_AnlasmaSablonu = wbook.Worksheets.Add(data, "Sipariş Listesi");
            wbook.Table("Sipariş Listesi").ShowAutoFilter = false;

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