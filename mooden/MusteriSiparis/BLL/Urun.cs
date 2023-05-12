using System.Data;

namespace BLL
{
    public class Urun
    {
        public DataTable UrunTuruListesi()
        {
            return new DAL.DBOps("connstr").sc(@"select 0 as ID_, ' Seçiniz' as NAME_ UNION ALL
select id as ID_, CASE WHEN aktif_pasif = 1 THEN '' ELSE '! ' END  + tur_adi 
as NAME_ from UrunTuru order by NAME_");
        }

        public DataTable UrunListesi(int urunturu_id)
        {
            return new DAL.DBOps("connstr").sc(@"select 0 as ID_, ' Seçiniz' as NAME_ UNION ALL
select u.id as ID_, CASE WHEN u.aktif_pasif = 1 THEN '' ELSE '! ' END  + u.urun_adi 
as NAME_ from Urunler u
inner join UrunTuru ut on u.urunturu_id = ut.id
where u.urunturu_id =  " + urunturu_id + " order by NAME_");
        }
    }
}
