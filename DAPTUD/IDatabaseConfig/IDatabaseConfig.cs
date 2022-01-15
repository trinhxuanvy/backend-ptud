﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.IDbConfig
{
    public interface IDatabaseConfig
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
        string SanPhamCollectionName { get; set; }
        string DonHangCollectionName { get; set; }
        string ChiTietDonHangCollectionName { get; set; }
        string NguoiDungCollectionName { get; set; }
    }
}
