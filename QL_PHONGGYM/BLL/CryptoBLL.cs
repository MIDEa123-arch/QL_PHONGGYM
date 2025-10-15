using Oracle.ManagedDataAccess.Client;
using QL_PHONGGYM.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_PHONGGYM.BLL
{
    public class CryptoBLL
    {
        private readonly CryptoDAL cryptoDAL;

        public CryptoBLL(OracleConnection conn)
        {
            cryptoDAL = new CryptoDAL(conn);
        }

        public string MaHoaCong(string input, int key) => cryptoDAL.MaHoaCong(input, key);
        public string GiaiMaCong(string input, int key) => cryptoDAL.GiaiMaCong(input, key);
        public string MaHoaNhan(string input, int key) => cryptoDAL.MaHoaNhan(input, key);
        public string GiaiMaNhan(string input, int key) => cryptoDAL.GiaiMaNhan(input, key);
        public System.Data.DataTable XuatLopHoc() => cryptoDAL.XuatLopHoc();
    }
}
