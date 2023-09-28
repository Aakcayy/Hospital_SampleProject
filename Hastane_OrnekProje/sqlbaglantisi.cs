using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Hastane_OrnekProje
{
    class sqlbaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=KARTAL\\SQLEXPRESS02;Initial Catalog=HastaneProje;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
        /*Büyük bir proje olduğu için bilemedin belki 20 tane sqlconnection kullanmak zorunda kalacağız.
Hem her seferinden bağlantıyı yazmak yerine hem de olaki başka pc ye proje taşınırsa teker teker 
bağlantı adresini değiştirmek ile uğraşmayım diye bağlantı adresini class içerisinde metot a tanımlarsak 
istediğimiz yerde çağılarak kullanırız veya başka bir pc de tek yerden adresi değiştirerek büyük bir zahmetten kurtulmuş oluruz.*/
    }
}
