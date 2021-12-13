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
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
