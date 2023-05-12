using System.Data;

namespace BLL
{
    public class Yoksis
    {
        public DataTable UniversiteListesi()
        {
            return new DAL.DBOps("connstr").sc(@"select 0 as ID_, ' Seçiniz' as NAME_ UNION ALL
select universite_id as ID_, adi 
as NAME_ from Universiteler where aktif_pasif = 1 order by NAME_");
        }

        public DataTable FakulteListesi(int universite_id)
        {
            return new DAL.DBOps("connstr").sc(@"select 0 as ID_, ' Seçiniz' as NAME_ UNION ALL
select fakulte_id as ID_, adi 
as NAME_ from Fakulteler where universite_id=@p1 and aktif_pasif = 1 order by NAME_", universite_id);
        }

        public DataTable BolumListesi(int fakulte_id)
        {
            return new DAL.DBOps("connstr").sc(@"select 0 as ID_, ' Seçiniz' as NAME_ UNION ALL
select bolum_id as ID_, adi 
as NAME_ from Bolumler where fakulte_id =@p1 and aktif_pasif = 1 order by NAME_", fakulte_id);
        }
    }
}
