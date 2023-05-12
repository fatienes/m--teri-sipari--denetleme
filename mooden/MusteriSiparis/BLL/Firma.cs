using System.Data;

namespace BLL
{
    public class Firma
    {
        public DataTable FirmaListesi()
        {
            return new DAL.DBOps("connstr").sc(@"select 0 as ID_, ' Seçiniz' as NAME_ UNION ALL
select id as ID_, CASE WHEN aktif_pasif = 1 THEN '' ELSE '! ' END  + firma_adi 
as NAME_ from Musteriler order by NAME_");
        }
    }
}
