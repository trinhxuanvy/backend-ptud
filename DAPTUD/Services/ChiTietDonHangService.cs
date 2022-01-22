﻿using DAPTUD.IDbConfig;
using DAPTUD.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Services
{
    public class ChiTietDonHangService
    {
        private readonly IMongoCollection<DonHang> invoices;
        private readonly IMongoCollection<ChiTietDonHang> invoiceDetails;
        private readonly IMongoCollection<SanPham> product;
        public ChiTietDonHangService(IDatabaseConfig dbConfig)
        {
            var client = new MongoClient(dbConfig.ConnectionString);
            var database = client.GetDatabase(dbConfig.DatabaseName);
            invoices = database.GetCollection<DonHang>(dbConfig.DonHangCollectionName);
            product = database.GetCollection<SanPham>(dbConfig.SanPhamCollectionName);
            invoiceDetails = database.GetCollection<ChiTietDonHang>(dbConfig.ChiTietDonHangCollectionName);
        }

        public async Task<List<ChiTietDonHang>> GetAll()
        {
            return await invoiceDetails.Find(c => true).ToListAsync();
        }

        public async Task<List<ChiTietDonHang>> Get(string donHang)
        {
            return await invoiceDetails.Find(c => c.donHang == donHang).ToListAsync();
        }

        public async Task<ChiTietDonHang> CreateAsync(ChiTietDonHang chiTietDonHang)
        {
            await invoiceDetails.InsertOneAsync(chiTietDonHang).ConfigureAwait(false);
            return chiTietDonHang;
        }
        public async Task<List<ChiTietDonHang>> CreateInvoiceDetails(List<ChiTietDonHang> chiTietDonHang)
        {
            await invoiceDetails.InsertManyAsync(chiTietDonHang);
            return chiTietDonHang;
        }

        public async Task<Invoice> GetOneHaveNameProduct(string donHang)
        {
            Invoice result = new Invoice();

            DonHang invs = await invoices.Find<DonHang>(s => s.id == donHang).FirstOrDefaultAsync();

            List<ChiTietDonHang> invdetails = await invoiceDetails.Find<ChiTietDonHang>(s => s.donHang == donHang).ToListAsync();

            List<InvoiceDetail> listInvoiceDetails = new List<InvoiceDetail>();

            foreach (ChiTietDonHang invdetail in invdetails)
            {
                List<SanPham> prods = await product.Find<SanPham>(s => s.id == invdetail.sanPham).ToListAsync();

                foreach (SanPham product in prods)
                {
                    InvoiceDetail tmpInvoiceDetail = new InvoiceDetail();
                    tmpInvoiceDetail.product = product.tenSanPham;
                    tmpInvoiceDetail.price = product.giaTien;
                    tmpInvoiceDetail.numOfElement = invdetail.soLuong;
                    tmpInvoiceDetail.unit = product.donViTinh;
                    tmpInvoiceDetail.idInvoiceDetail = invdetail.id;
                    listInvoiceDetails.Add(tmpInvoiceDetail);
                }
            }
            result.invoiceID = invs.id;
            result.timeOrder = invs.thoiGianDat.GetDateTimeFormats('d')[0];
            result.status = invs.tinhTrang;
            result.oldStatus = invs.tinhTrangCu;
            result.payment = invs.phuongThucThanhToan;
            result.invoiceDetail = listInvoiceDetails;
            return result;
        }
    }
}
