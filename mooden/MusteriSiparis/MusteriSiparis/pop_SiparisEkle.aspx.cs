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
    public partial class pop_SiparisEkle : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["kullanici_bilgisi"] == null) { Session.Clear(); Response.Redirect("login.aspx"); return; }

            if (!IsPostBack)
            {
                FirmaListesi();
                UrunTuruListesi();
                if (!String.IsNullOrEmpty(Request["id"]))
                {
                    DateTime? _SiparisTarihi = null;
                    DateTime __SiparisTarihi;

                    DataTable dt_Siparisler = new DAL.DBOps("connstr").sc(@"select siparis_no, musteri_id, 
s.adet, s.fiyat, siparis_tarihi, s.urunturu_id, urun_id, u.adet as stok_miktari
from Siparisler s
inner join Urunler u on u.id = urun_id
where 1=1 and s.id = @p1", Request["id"]);

                    ddl_Firma.SelectedValue = dt_Siparisler.Rows[0]["musteri_id"].ToString();
                    txt_SiparisNo.Text = dt_Siparisler.Rows[0]["siparis_no"].ToString();
                    ddl_UrunTuru.SelectedValue = dt_Siparisler.Rows[0]["urunturu_id"].ToString();
                    txt_Adet.Text = dt_Siparisler.Rows[0]["adet"].ToString();
                    txt_Fiyat.Text = dt_Siparisler.Rows[0]["fiyat"].ToString();

                    DateTime.TryParse(dt_Siparisler.Rows[0]["siparis_tarihi"].ToString(), out __SiparisTarihi);
                    _SiparisTarihi = __SiparisTarihi;
                    txt_SiparisTarihi.Text = _SiparisTarihi.Value.ToString("dd.MM.yyyy") ?? "";

                    txt_StokMiktari.Text = dt_Siparisler.Rows[0]["stok_miktari"].ToString();
                    int urunturu_id;
                    Int32.TryParse(ddl_UrunTuru.SelectedValue, out urunturu_id);
                    UrunListesi(urunturu_id, dt_Siparisler.Rows[0]["urun_id"].ToString());
                }
            }
        }

        public void FirmaListesi()
        {
            ddl_Firma.DataTextField = "NAME_";
            ddl_Firma.DataValueField = "ID_";

            ddl_Firma.DataSource = new BLL.Firma().FirmaListesi();
            ddl_Firma.SelectedValue = "0";
            ddl_Firma.DataBind();
        }

        public void UrunTuruListesi()
        {
            ddl_UrunTuru.DataTextField = "NAME_";
            ddl_UrunTuru.DataValueField = "ID_";

            ddl_UrunTuru.DataSource = new BLL.Urun().UrunTuruListesi();
            ddl_UrunTuru.SelectedValue = "0";
            ddl_UrunTuru.DataBind();
        }
        
        public void UrunListesi(int urunturu_id, string ilce_id)
        {
            ddl_Urun.DataTextField = "NAME_";
            ddl_Urun.DataValueField = "ID_";

            ddl_Urun.DataSource = new BLL.Urun().UrunListesi(urunturu_id);
            ddl_Urun.SelectedValue = ilce_id;
            ddl_Urun.DataBind();
        }

        protected void btn_kaydet_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txt_Adet.Text) || String.IsNullOrEmpty(txt_SiparisTarihi.Text) || String.IsNullOrEmpty(ddl_Firma.SelectedValue) || String.IsNullOrEmpty(ddl_Urun.SelectedValue) || String.IsNullOrEmpty(ddl_UrunTuru.SelectedValue))
            {
                ClientScript.RegisterStartupScript(GetType(), "YeniPencere", @"<script language='javascript'>toastr.warning('Lütfen * ile belirtilen alanları boş bırakmayınız.');</script>");
                return;
            }
            else
            {
                if (int.Parse(txt_StokMiktari.Text) >= int.Parse(txt_Adet.Text))
                {
                    DateTime? _SiparisTarihi = null;
                    DateTime __SiparisTarihi;
                    bool success = DateTime.TryParse(txt_SiparisTarihi.Text, out __SiparisTarihi);
                    if (success) _SiparisTarihi = __SiparisTarihi;

                    Decimal fiyat;
                    Decimal.TryParse(txt_Fiyat.Text, out fiyat);

                    if (String.IsNullOrEmpty(Request["id"]))
                    {
                        int sonuc = new DAL.DBOps("connstr").scn(@"insert into Siparisler(kullanici_id, musteri_id, siparis_no, urunturu_id, urun_id, adet, fiyat, siparis_tarihi) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", ((KullaniciBilgisi)Session["kullanici_bilgisi"]).Kullanici_Id, ddl_Firma.SelectedValue, txt_SiparisNo.Text, ddl_UrunTuru.SelectedValue, ddl_Urun.SelectedValue, txt_Adet.Text, fiyat, _SiparisTarihi);

                        string siparis_id = new DAL.DBOps("connstr").scs(@"select top 1 id from Siparisler where siparis_no = @p1", txt_SiparisNo.Text);

                        int siparis_adimlari = new DAL.DBOps("connstr").scn(@"insert into SiparisAdimlari(kullanici_id,siparis_id,durum,durum_id,aciklama)
values(@p1,@p2,'Sipariş Bekliyor','0','Sipariş alındı. Onaylanması veya iptal edilmesi bekleniyor.')", ((KullaniciBilgisi)Session["kullanici_bilgisi"]).Kullanici_Id, siparis_id);

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
                        int sonuc = new DAL.DBOps("connstr").scn(@"update Siparisler set kullanici_id = @p1, urunturu_id = @p2, urun_id = @p3, adet = @p4, fiyat = @p5, siparis_tarihi = @p6, guncelleme_tarihi = @p7 where id = @p8", ((KullaniciBilgisi)Session["kullanici_bilgisi"]).Kullanici_Id, ddl_UrunTuru.SelectedValue, ddl_Urun.SelectedValue, txt_Adet.Text, fiyat, _SiparisTarihi, DateTime.Now, Request["id"]);

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
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "YeniPencere", @"<script language='javascript'>toastr.warning('Lütfen * ile belirtilen alanları boş bırakmayınız.');</script>");
                    return;
                }
            }
        }

        protected void ddl_UrunTuru_SelectedIndexChanged(object sender, EventArgs e)
        {
            int urunturu_id;
            Int32.TryParse(ddl_UrunTuru.SelectedValue, out urunturu_id);
            UrunListesi(urunturu_id, "0");
            //txt_SiparisNo.Text = Session["SiparisNo"].ToString();
        }

        protected void ddl_Firma_SelectedIndexChanged(object sender, EventArgs e)
        {
            string siparis_sayisi = new DAL.DBOps("connstr").scs(@"select count(id)
from Siparisler 
where 1=1 and musteri_id = @p1", ddl_Firma.SelectedValue);
            txt_SiparisNo.Text = ddl_Firma.SelectedItem.ToString().Split(' ').First() + siparis_sayisi;
            //Session["SiparisNo"] = txt_SiparisNo.Text;
        }

        protected void ddl_Urun_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fiyat = new DAL.DBOps("connstr").scs(@"select top 1 fiyat
from Urunler 
where 1=1 and id = @p1", ddl_Urun.SelectedValue);
                
            string stok_Miktari = new DAL.DBOps("connstr").scs(@"select top 1 adet
from Urunler 
where 1=1 and id = @p1", ddl_Urun.SelectedValue);

            txt_StokMiktari.Text = stok_Miktari;
        }

        protected void txt_Adet_TextChanged(object sender, EventArgs e)
        {
            string fiyat = new DAL.DBOps("connstr").scs(@"select top 1 fiyat
from Urunler 
where 1=1 and id = @p1", ddl_Urun.SelectedValue);

            Decimal fiyatt = Decimal.Parse(fiyat) * int.Parse(txt_Adet.Text);

            txt_Fiyat.Text = fiyatt.ToString();
        }
    }
}