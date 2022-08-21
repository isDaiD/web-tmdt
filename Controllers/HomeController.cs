using LeDaiDuong_151901766.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeDaiDuong_151901766.Controllers
{
    public class HomeController : Controller
    {
        DBWebEntities1 db = new DBWebEntities1();
        public ActionResult Index()
        {
            ViewBag.Category = "";
            var dssanpham = db.SANPHAMs.Where(s => s.DaXoa == null)
                .Select(sp=>new SanPhamModels 
                { 
                    MaSanPham = sp.MaSanPham,
                    TenSanPham = sp.TenSanPham,
                    Giaban = sp.Giaban,
                    Mota = sp.Mota,
                    GioiTinh = sp.GioiTinh,
                    MaCD = sp.MaCD,
                    SoLuongAnh = sp.SoLuongAnh,
                    Hinh1 = sp.Hinh1,
                    Hinh2 = sp.Hinh2,
                    Hinh3 = sp.Hinh3,
                    Hinh4 = sp.Hinh4,
                    Hinh5 = sp.Hinh5,
                    Hinh6 = sp.Hinh6,
                    Hinh7 = sp.Hinh7,
                    Hinh8 = sp.Hinh8,
                    Hinh9 = sp.Hinh9,
                    Hinh10 = sp.Hinh10,
                    Hinh11 = sp.Hinh11,
                    Hinh12 = sp.Hinh12,
                    Hinh13 = sp.Hinh13,
                    Hinh14 = sp.Hinh14,
                    Hinh15 = sp.Hinh15,
                    Sale = sp.Sale,
                    GiaSale = (double)sp.Giaban * (1 - sp.Sale),
                    KhuyenMai = sp.Sale * 100,
                    Ngaycapnhat = sp.Ngaycapnhat,
                    Soluongton = sp.Soluongton,
                    DaXoa = sp.DaXoa,
                })
                .ToList();
            DateTime today = DateTime.Today;
            ViewBag.HomNay = today;
            ViewBag.SanPham = dssanpham;

            var dsMen = db.SANPHAMs.Where(s => (s.GioiTinh == "Male" || s.GioiTinh == "All") && s.DaXoa == null)
                .Select(sp => new SanPhamModels
                {
                    MaSanPham = sp.MaSanPham,
                    TenSanPham = sp.TenSanPham,
                    Giaban = sp.Giaban,
                    Mota = sp.Mota,
                    GioiTinh = sp.GioiTinh,
                    MaCD = sp.MaCD,
                    SoLuongAnh = sp.SoLuongAnh,
                    Hinh1 = sp.Hinh1,
                    Hinh2 = sp.Hinh2,
                    Hinh3 = sp.Hinh3,
                    Hinh4 = sp.Hinh4,
                    Hinh5 = sp.Hinh5,
                    Hinh6 = sp.Hinh6,
                    Hinh7 = sp.Hinh7,
                    Hinh8 = sp.Hinh8,
                    Hinh9 = sp.Hinh9,
                    Hinh10 = sp.Hinh10,
                    Hinh11 = sp.Hinh11,
                    Hinh12 = sp.Hinh12,
                    Hinh13 = sp.Hinh13,
                    Hinh14 = sp.Hinh14,
                    Hinh15 = sp.Hinh15,
                    Sale = sp.Sale,
                    GiaSale = (double)sp.Giaban * (1 - sp.Sale),
                    KhuyenMai = sp.Sale * 100,
                    Ngaycapnhat = sp.Ngaycapnhat,
                    Soluongton = sp.Soluongton,
                    DaXoa = sp.DaXoa,
                })
                .Take(8)
                .ToList();
            ViewBag.SanPhamMen = dsMen;

            var dsWomen = db.SANPHAMs.Where(s => (s.GioiTinh == "Female" || s.GioiTinh == "All") && s.DaXoa == null)
                .Select(sp => new SanPhamModels
                {
                    MaSanPham = sp.MaSanPham,
                    TenSanPham = sp.TenSanPham,
                    Giaban = sp.Giaban,
                    Mota = sp.Mota,
                    GioiTinh = sp.GioiTinh,
                    MaCD = sp.MaCD,
                    SoLuongAnh = sp.SoLuongAnh,
                    Hinh1 = sp.Hinh1,
                    Hinh2 = sp.Hinh2,
                    Hinh3 = sp.Hinh3,
                    Hinh4 = sp.Hinh4,
                    Hinh5 = sp.Hinh5,
                    Hinh6 = sp.Hinh6,
                    Hinh7 = sp.Hinh7,
                    Hinh8 = sp.Hinh8,
                    Hinh9 = sp.Hinh9,
                    Hinh10 = sp.Hinh10,
                    Hinh11 = sp.Hinh11,
                    Hinh12 = sp.Hinh12,
                    Hinh13 = sp.Hinh13,
                    Hinh14 = sp.Hinh14,
                    Hinh15 = sp.Hinh15,
                    Sale = sp.Sale,
                    GiaSale = (double)sp.Giaban * (1 - sp.Sale),
                    KhuyenMai = sp.Sale * 100,
                    Ngaycapnhat = sp.Ngaycapnhat,
                    Soluongton = sp.Soluongton,
                    DaXoa = sp.DaXoa,
                })
                .Take(8)
                .ToList();
            ViewBag.SanPhamWomen = dsWomen;

            var dsSport = db.SANPHAMs.Where(s => (s.CHUDE.TenChuDe == "Sport") && s.DaXoa == null)
                .Select(sp => new SanPhamModels
                {
                    MaSanPham = sp.MaSanPham,
                    TenSanPham = sp.TenSanPham,
                    Giaban = sp.Giaban,
                    Mota = sp.Mota,
                    GioiTinh = sp.GioiTinh,
                    MaCD = sp.MaCD,
                    SoLuongAnh = sp.SoLuongAnh,
                    Hinh1 = sp.Hinh1,
                    Hinh2 = sp.Hinh2,
                    Hinh3 = sp.Hinh3,
                    Hinh4 = sp.Hinh4,
                    Hinh5 = sp.Hinh5,
                    Hinh6 = sp.Hinh6,
                    Hinh7 = sp.Hinh7,
                    Hinh8 = sp.Hinh8,
                    Hinh9 = sp.Hinh9,
                    Hinh10 = sp.Hinh10,
                    Hinh11 = sp.Hinh11,
                    Hinh12 = sp.Hinh12,
                    Hinh13 = sp.Hinh13,
                    Hinh14 = sp.Hinh14,
                    Hinh15 = sp.Hinh15,
                    Sale = sp.Sale,
                    GiaSale = (double)sp.Giaban * (1 - sp.Sale),
                    KhuyenMai = sp.Sale * 100,
                    Ngaycapnhat = sp.Ngaycapnhat,
                    Soluongton = sp.Soluongton,
                    DaXoa = sp.DaXoa,
                })
                .Take(8)
                .ToList();
            ViewBag.SanPhamSport = dsSport;

            var dsWatch = db.SANPHAMs.Where(s => (s.CHUDE.TenChuDe == "Watch") && s.DaXoa == null)
                .Select(sp => new SanPhamModels
                {
                    MaSanPham = sp.MaSanPham,
                    TenSanPham = sp.TenSanPham,
                    Giaban = sp.Giaban,
                    Mota = sp.Mota,
                    GioiTinh = sp.GioiTinh,
                    MaCD = sp.MaCD,
                    SoLuongAnh = sp.SoLuongAnh,
                    Hinh1 = sp.Hinh1,
                    Hinh2 = sp.Hinh2,
                    Hinh3 = sp.Hinh3,
                    Hinh4 = sp.Hinh4,
                    Hinh5 = sp.Hinh5,
                    Hinh6 = sp.Hinh6,
                    Hinh7 = sp.Hinh7,
                    Hinh8 = sp.Hinh8,
                    Hinh9 = sp.Hinh9,
                    Hinh10 = sp.Hinh10,
                    Hinh11 = sp.Hinh11,
                    Hinh12 = sp.Hinh12,
                    Hinh13 = sp.Hinh13,
                    Hinh14 = sp.Hinh14,
                    Hinh15 = sp.Hinh15,
                    Sale = sp.Sale,
                    GiaSale = (double)sp.Giaban * (1 - sp.Sale),
                    KhuyenMai = sp.Sale * 100,
                    Ngaycapnhat = sp.Ngaycapnhat,
                    Soluongton = sp.Soluongton,
                    DaXoa = sp.DaXoa,
                })
                .Take(8)
                .ToList();
            ViewBag.SanPhamWatch = dsWatch;

            var user = Session["USER"];
            ViewBag.User = user;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult CategoryList()
        {
            var gioitinh = db.SANPHAMs.ToList();
            ViewBag.Gender = gioitinh;

            var theloai = db.CHUDEs.ToList();


            ViewBag.TheLoai = theloai;
            return PartialView("CategoryList");
        }
    }
}