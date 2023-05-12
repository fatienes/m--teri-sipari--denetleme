using System.Data;

namespace BLL
{
    public class SehirIlce
    {
        public DataTable SehirListesi()
        {
            return new DAL.DBOps("connstr").sc(@"select 0 as ID_, ' Seçiniz' as NAME_ UNION ALL
select id as ID_, CASE WHEN aktif_pasif = 1 THEN '' ELSE '! ' END  + sehir_adi 
as NAME_ from Sehirler order by NAME_");
        }

        public DataTable IlceListesi(int sehir_id)
        {
            return new DAL.DBOps("connstr").sc(@"select 0 as ID_, ' Seçiniz' as NAME_ UNION ALL
select i.id as ID_, CASE WHEN i.aktif_pasif = 1 THEN '' ELSE '! ' END  + i.ilce_adi 
as NAME_ from Sehirler s
inner join Ilceler i on i.sehir_id = s.id
where i.sehir_id =  " + sehir_id + " order by NAME_");
        }

    }
}
