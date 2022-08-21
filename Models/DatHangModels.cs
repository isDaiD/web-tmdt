using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeDaiDuong_151901766.Models
{
    public class DatHangModels
    {
        public string HoTen { get; set; }
        public int MaDonHang { get; set; }
        public DateTime? Ngaydat { get; set; }
        public int? MaKH { get; set; }
        public int SoDT { get; set; }
        public string DiaChi { get; set; }
        public int MaSanPham { get; set; }
        public int? Soluong { get; set; }
        public string Dongia { get; set; }
    }
}