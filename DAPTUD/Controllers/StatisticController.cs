using DAPTUD.Models;
using DAPTUD.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Controllers
{
    public class Data
    {
        public double units
        {
            get; set;
        } 

        public DateTime date
        {
            get; set;
        }

        public Data(double _units, DateTime _date)
        {
            this.units = _units;
            this.date = _date;
        }
    }

    public class FinalData
    {
        public double units
        {
            get; set;
        }

        public string date
        {
            get; set;
        }

        public FinalData(double _units, string _date)
        {
            this.units = _units;
            this.date = _date;
        }
    }

    public class Statistic
    {
         public List<FinalData> data
        {
            get; set;
        }

        public string titleX
        {
            get; set;
        } 

        public string titleY
        {
            get; set;
        }
     }

    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        public readonly DonHangService donHangService;

        public StatisticController(DonHangService _donhangService)
        {
            donHangService = _donhangService;
        }

        [HttpGet("day/{storeId}/{startDate}/{endDate}")]
        public async Task<ActionResult<Statistic>> GetStatisticWithDay(
            string storeId,
            DateTime startDate,
            DateTime endDate
            )
        {
            Statistic statistic = new Statistic();
            List<Data> data = new List<Data>();
            List<FinalData> listFinalData = new List<FinalData>();
            DateTime dateTemp = new DateTime();
            Data dataTemp = null;
            FinalData finalDataTemp = null;
            double units = 0;
            bool flag = false;
            
            List<DonHang> invoices = await donHangService.GetAllDonHangForStatistic(storeId);

            invoices = invoices.FindAll(i => 
            i.thoiGianDat.Date >= startDate.Date && 
            i.thoiGianDat.Date <= endDate.Date
            ).OrderBy(i => i.thoiGianDat.Date).ToList();

            if (invoices.Count != 0)
            {
                dateTemp = (DateTime)(invoices?[0].thoiGianDat);

                foreach (DonHang invoice in invoices)
                {
                    if (invoice.thoiGianDat.Date == dateTemp.Date)
                    {
                        units += invoice.tongTien;
                    }
                    else
                    {
                        dataTemp = new Data(units, dateTemp);
                        data.Add(dataTemp);
                        units = invoice.tongTien;
                    }
                    dateTemp = invoice.thoiGianDat;
                    flag = true;
                }
            }
                    
            if (invoices.Count != 0 && flag)
            {
                dataTemp = new Data(units, dateTemp);
                data.Add(dataTemp);
            }

            if (data.Count != 0)
            {
                dateTemp = startDate;
                int i = 0;
                while (true)
                {
                    if (dateTemp.Date == endDate.Date.AddDays(1))
                    {
                        break;
                    }
                    if (i < data.Count)
                    {
                        if (dateTemp.Date == data[i].date.Date)
                        {
                            units = data[i].units;
                            i++;
                        }
                        else
                        {
                            units = 0;
                        }
                    }
                    else
                    {
                        units = 0;
                    }

                    string temp = dateTemp.Day.ToString() + "-" + dateTemp.Month.ToString() + "-" + dateTemp.Year.ToString();
                    finalDataTemp = new FinalData(units, temp);
                    listFinalData.Add(finalDataTemp);
                    dateTemp = dateTemp.AddDays(1);
                }    
            }
                      
            statistic.titleX = startDate.Day.ToString() + "-" + 
                startDate.Month.ToString() + "-" +
                startDate.Year.ToString() + " - " +
                endDate.Day.ToString() + "-" +
                endDate.Month.ToString() + "-" +
                endDate.Year.ToString();   
            
            statistic.data = listFinalData;
            statistic.titleY = "VNĐ";
            return Ok(statistic);
        }

        [HttpGet("month/{storeId}/{startMonth}/{startYear}/{endMonth}/{endYear}")]
        public async Task<ActionResult<Statistic>> GetStatisticWithMonth(
            string storeId,
            int startMonth,
            int startYear,
            int endMonth,
            int endYear
            )
        {
            Statistic statistic = new Statistic();
            List<Data> data = new List<Data>();
            List<FinalData> listFinalData = new List<FinalData>();
            DateTime dateTemp = new DateTime();
            Data dataTemp = null;
            FinalData finalDataTemp = null;
            double units = 0;
            bool flag = false;

            List<DonHang> invoices = await donHangService.GetAllDonHangForStatistic(storeId);

            DateTime a,
                        b = new DateTime(startYear, startMonth, 1),
                        c = new DateTime(endYear, endMonth, 1),
                        d = c.AddMonths(1).AddDays(-1);
            invoices = invoices.FindAll(i =>
                i.thoiGianDat >= b.Date &&
                i.thoiGianDat <= d.Date
                ).OrderBy(i => i.thoiGianDat.Date).ToList();
            if (invoices.Count != 0)
            {
                dateTemp = (DateTime)(invoices?[0].thoiGianDat);
                dateTemp = new DateTime(dateTemp.Year, dateTemp.Month, 1);

                foreach (DonHang invoice in invoices)
                {
                    a = new DateTime(invoice.thoiGianDat.Year, invoice.thoiGianDat.Month, 1);

                    if (a == dateTemp.Date)
                    {
                        units += invoice.tongTien;
                    }
                    else
                    {
                        dataTemp = new Data(units, dateTemp);
                        data.Add(dataTemp);
                        units = invoice.tongTien;
                    }
                    dateTemp = a;
                    flag = true;
                }
            }

            if (invoices.Count != 0 && flag)
            {
                dataTemp = new Data(units, dateTemp.Date);
                data.Add(dataTemp);
            }

            if (data.Count != 0)
            {
                dateTemp = b;
                int i = 0;
                while (true)
                {
                    if (dateTemp.Date == d.AddDays(1))
                    {
                        break;
                    }
                    if (i < data.Count)
                    {
                        DateTime j = new DateTime(data[i].date.Year, data[i].date.Month, data[i].date.Day);
                        if (dateTemp.Date == j)
                        {
                            units = data[i].units;
                            i++;
                        }
                        else
                        {
                            units = 0;
                        }
                    }
                    else
                    {
                        units = 0;
                    }


                    string temp = dateTemp.Month.ToString() + "-" + dateTemp.Year.ToString();
                    finalDataTemp = new FinalData(units, temp);
                    listFinalData.Add(finalDataTemp);
                    dateTemp = dateTemp.AddMonths(1);
                }
            }

            statistic.titleX = startMonth.ToString() + "-" +
                startYear.ToString() + " - " +
                endMonth.ToString() + "-" +
                endYear.ToString();
            statistic.data = listFinalData;
            statistic.titleY = "VNĐ";

            return Ok(statistic);
        }

        [HttpGet("year/{storeId}/{startYear}/{endYear}")]
        public async Task<ActionResult<Statistic>> GetStatisticWithYear(
            string storeId,
            int startYear,
            int endYear
            )
        {
            Statistic statistic = new Statistic();
            List<Data> data = new List<Data>();
            List<FinalData> listFinalData = new List<FinalData>();
            DateTime dateTemp = new DateTime();
            Data dataTemp = null;
            FinalData finalDataTemp = null;
            double units = 0;
            bool flag = false;

            List<DonHang> invoices = await donHangService.GetAllDonHangForStatistic(storeId);

            DateTime e, f = new DateTime(startYear, 1, 1), g = new DateTime(endYear, 12, 31);
            invoices = invoices.FindAll(i =>
                i.thoiGianDat >= f.Date &&
                i.thoiGianDat <= g.Date
                ).OrderBy(i => i.thoiGianDat.Date).ToList();
            if (invoices.Count != 0)
            {
                dateTemp = (DateTime)(invoices?[0].thoiGianDat);
                dateTemp = new DateTime(dateTemp.Year, 1, 1);

                foreach (DonHang invoice in invoices)
                {
                    e = new DateTime(invoice.thoiGianDat.Year, 1, 1);
                    if (e == dateTemp.Date)
                    {
                        units += invoice.tongTien;
                    }
                    else
                    {
                        dataTemp = new Data(units, dateTemp.Date);
                        data.Add(dataTemp);
                        units = invoice.tongTien;
                    }
                    dateTemp = e;
                    flag = true;
                }
            }

            if (invoices.Count != 0 && flag)
            {
                dataTemp = new Data(units, dateTemp.Date);
                data.Add(dataTemp);
            }

            if (data.Count != 0)
            {
                dateTemp = f;
                int i = 0;
                while (true)
                {
                    if (dateTemp.Date == g.AddDays(1))
                    {
                        break;
                    }
                    if (i < data.Count)
                    {
                        DateTime j = new DateTime(data[i].date.Year, data[i].date.Month, data[i].date.Day);
                        if (dateTemp.Date == j)
                        {
                            units = data[i].units;
                            i++;
                        }
                        else
                        {
                            units = 0;
                        }
                    }
                    else
                    {
                        units = 0;
                    }


                    string temp = dateTemp.Year.ToString();
                    finalDataTemp = new FinalData(units, temp);
                    listFinalData.Add(finalDataTemp);
                    dateTemp = dateTemp.AddYears(1);
                }
            }
            statistic.titleX = startYear.ToString() + " - " + endYear.ToString();
            statistic.data = listFinalData;
            statistic.titleY = "VNĐ";

            return Ok(statistic);
        }
    }
}
