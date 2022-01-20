using DAPTUD.IDbConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.DbConfig
{
    public class DatabaseConfig : IDatabaseConfig
    {
        public string SanPhamCollectionName { get; set; }
        public string DonHangCollectionName { get; set; }
        public string ChiTietDonHangCollectionName { get; set; }
        public string NguoiDungCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string VanDonCollectionName { get; set; }
        public string ShipperCollectionName { get; set; }

        public string CuaHangCollectionName { get; set; }

        public string ViTriCuaHangCollectionName { get; set; }

        public string ViTriKhachHangCollectionName { get; set; }

        public string ViTriShipperCollectionName { get; set; }
    }
}
