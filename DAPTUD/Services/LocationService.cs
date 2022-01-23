using DAPTUD.IDbConfig;
using DAPTUD.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace DAPTUD.Services
{
    public class LocationService
    {
        private readonly IMongoCollection<ViTriKhachHang> lctKhachHang;
        private readonly IMongoCollection<ViTriCuaHang> lctCuaHang;
        private readonly IMongoCollection<ViTriShipper> lctShipper;
        private readonly IMongoCollection<VungMien> vungMiens;
        private readonly IMongoCollection<CuaHang> cuaHangs;

        public LocationService(IDatabaseConfig dbConfig)
        {
            var client = new MongoClient(dbConfig.ConnectionString);
            var database = client.GetDatabase(dbConfig.DatabaseName);
            lctCuaHang = database.GetCollection<ViTriCuaHang>(dbConfig.ViTriCuaHangCollectionName);
            lctKhachHang = database.GetCollection<ViTriKhachHang>(dbConfig.ViTriKhachHangCollectionName);
            lctShipper = database.GetCollection<ViTriShipper>(dbConfig.ViTriShipperCollectionName);
            vungMiens = database.GetCollection<VungMien>(dbConfig.VungMienCollectionName);
            cuaHangs = database.GetCollection<CuaHang>(dbConfig.CuaHangCollectionName);
        }

        public async Task<List<ViTriCuaHang>> GetAllViTriCuaHang()
        {
            return await lctCuaHang.Find(l => true).ToListAsync();
        }

        public async Task<List<ViTriKhachHang>> GetAllViTriKhachHang()
        {
            return await lctKhachHang.Find(l => true).ToListAsync();
        }

        public async Task<List<ViTriShipper>> GetAllViTriShipper()
        {
            return await lctShipper.Find(l => true).ToListAsync();
        }

        public async Task<ViTriCuaHang> GetMotViTriCuaHang(string id)
        {
            return await lctCuaHang.Find<ViTriCuaHang>(l => l.objectId == id).FirstOrDefaultAsync();
        }

        public async Task<List<VungMien>> GetAllVungMien()
        {
            return await vungMiens.Find(l => true).ToListAsync();
        }
        public static RootObject getAddress(double lat, double lon)
        {
            WebClient webClient = new WebClient();

            webClient.Headers.Add("user-agent", "Mozilla/4.0(compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

            webClient.Headers.Add("Referer", "http://www.microsoft.com");

            var jsonData = webClient.DownloadData("http://nominatim.openstreetmap.org/reverse?format=json&lat=" + lat + "&lon=" + lon);

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(RootObject));

            RootObject rootObject = (RootObject)ser.ReadObject(new MemoryStream(jsonData));

            return rootObject;
        }
        public async Task<List<CuaHang>> GetAllCuaHang()
        {
            return await cuaHangs.Find(s => true).ToListAsync();
        }
        public async Task<List<CuaHang>> GetStoreByDiseaseLevel(int level)
        {
            List<ViTriCuaHang> storeLocations = await GetAllViTriCuaHang();
            List<CuaHang> listGetStore = await GetAllCuaHang();
            List<CuaHang> stores_new = new List<CuaHang>();
            List<VungMien> regions = await GetAllVungMien();
            foreach (ViTriCuaHang storeLocation in storeLocations)
            {
                RootObject rootObject = getAddress(storeLocation.latitude, storeLocation.longtitude);
                foreach (VungMien region in regions)
                {
                    if (region.huyen ==getDistrict(rootObject.display_name) && region.thanhPho==getCity(rootObject.display_name) && region.capDoDich==level)
                    {
                        foreach(CuaHang store in listGetStore)
                        {
                            if (store.id==storeLocation.objectId)
                            {
                                stores_new.Add(store);
                            }    
                        }    
                    }    
                }
            }
            return stores_new;
        }
        public string getDistrict(string addr)
        {
            string[] arr = addr.Split(',');
            return  arr[arr.Length-4].Trim();
        }
        public string getCity(string addr)
        {
            string[] arr = addr.Split(',');
            return arr[arr.Length - 3].Trim();
        }
    }
}
