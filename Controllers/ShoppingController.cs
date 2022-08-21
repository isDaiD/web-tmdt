using LeDaiDuong_151901766.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Windows;

namespace LeDaiDuong_151901766.Controllers
{
    public class ShoppingController : Controller
    {
        DBWebEntities1 db = new DBWebEntities1();
        // GET: Shopping

        public ActionResult Cart()
        {
            if (Session["USER"] == null)
                return RedirectToAction("Account", "Account");
            var giohang = Session["CART"] as List<SanPhamModels>;
            ViewBag.GioHang = giohang;
            var total = Session["TOTAL"];
            if (total == null)
            {
                total = 0;
            }    
            ViewBag.Total = int.Parse(total.ToString());
            ViewBag.Total = Math.Round(double.Parse(total.ToString()),0);
            return View();
        }

        public ActionResult PNum()
        {
            var giohang = Session["CART"] as List<SanPhamModels>;
            if(giohang != null)
            {
                var pnum = giohang.Count();
                ViewBag.PNum = pnum;
            }
            else ViewBag.PNum = 0;

            return PartialView("PNum");
        }

        public ActionResult Cart_Box()
        {

            var giohang = Session["CART"] as List<SanPhamModels>;
            if (giohang != null)
            {
                var Product_number = giohang.Count();
                Session["PNUM"] = Product_number;
            }
            else
            { Session["PNUM"] = 0; }



            ViewBag.GioHang = giohang;
            if (Session["TOTAL"] != null)
            {
                var total = Session["TOTAL"];
                ViewBag.Total = int.Parse(total.ToString());
            }    
            
           

            return PartialView("Cart_Box");
        }


        public ActionResult AddToCart(int id)
        {
            if (Session["USER"] == null)
                return RedirectToAction("Account", "Account");
            else
            {
                var giohang = Session["CART"] as List<SanPhamModels>;
                if (giohang == null)
                {
                    giohang = new List<SanPhamModels>();
                    Session["CART"] = giohang;
                }
                var temp = db.SANPHAMs.Where(x => x.MaSanPham == id).FirstOrDefault();
                if (temp.Soluongton == 0)
                {
                    return PartialView("_AddFail");
                }
                var timkiem = giohang.Find(x => x.MaSanPham == id);
                if (timkiem == null)
                {
                    var sanpham = new SanPhamModels();
                    sanpham.TenSanPham = temp.TenSanPham;
                    sanpham.Sale = temp.Sale;
                    sanpham.Hinh1 = temp.Hinh1;
                    sanpham.Giaban = temp.Giaban;
                    sanpham.MaSanPham = temp.MaSanPham;
                    sanpham.SoLuong = 1;
                    sanpham.Total = 0;
                    giohang.Add(sanpham);

                }
                else
                {
                    timkiem.SoLuong += 1;
                }

                if (Session["TOTAL"] == null)
                    Session["TOTAL"] = 0;

                
                double tmp = double.Parse(temp.Giaban.ToString()) * double.Parse((1 - temp.Sale).ToString());
                int price = int.Parse((Math.Round(tmp, 0)).ToString());
                var total = double.Parse((Session["TOTAL"]).ToString()) + price;
                Session["TOTAL"] = total.ToString();
                ViewBag.Total = Session["TOTAL"];

                //return RedirectToAction("Cart","Shopping");

                return PartialView("_AddToCart"); 
            }


        }
        public ActionResult RemoveFormCart(int id)
        {
            var giohang = Session["CART"] as List<SanPhamModels>;

            var timkiem = giohang.Find(x => x.MaSanPham == id);
            if (timkiem != null)
            {
                var temp = db.SANPHAMs.Where(x => x.MaSanPham == id).FirstOrDefault();
                double tmp = double.Parse(temp.Giaban.ToString()) * double.Parse((1 - temp.Sale).ToString());
                int price = int.Parse((Math.Round(tmp, 0)).ToString());
                var total = double.Parse((Session["TOTAL"]).ToString()) - (price * int.Parse(timkiem.SoLuong.ToString()));
                Session["TOTAL"] = total.ToString();

                giohang.RemoveAll(x => x.MaSanPham == id);
            }    
            return RedirectToAction("Cart", "Shopping");
        }

        [ValidateInput(false)]
        [HttpGet]
        public ActionResult Checkout()
        {
            if (Session["USER"] == null)
                return RedirectToAction("Account", "Account");
            var giohang = Session["CART"] as List<SanPhamModels>;
            ViewBag.GioHang = giohang;
            var total = Session["TOTAL"];
            if (total == null)
            {
                total = 0;
            }
            ViewBag.Ten = Session["USER"];
            ViewBag.Total = int.Parse(total.ToString());
            ViewBag.Total = Math.Round(double.Parse(total.ToString()), 0);
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Order(DatHangModels model)
        {    
            if (Session["USER"] == null)
                return RedirectToAction("Account", "Account");
            var taikhoan = Session["USER"];
            var giohang = Session["CART"] as List<SanPhamModels>;

            var total = Session["TOTAL"];
            if (total == null)
            {
                total = 0;
            }
            total = int.Parse(total.ToString());
            total = Math.Round(double.Parse(total.ToString()), 0);

            int mahoadon = 0;
            if (mahoadon == 0)
            {
                Random rnd = new Random();
                mahoadon = rnd.Next(10000, 99999);
            }

            DateTime today = DateTime.Today;
            DONDATHANG hoadon = new DONDATHANG();
            hoadon.MaDonHang = mahoadon;
            hoadon.DiaChi = model.DiaChi;
            hoadon.MaKH = model.MaKH;
            hoadon.TongTien = int.Parse(total.ToString());
            hoadon.Ngaydat = today;
            hoadon.SDT = int.Parse(model.SoDT.ToString());
            db.DONDATHANGs.Add(hoadon);
            db.SaveChanges();

            CHITIETDONTHANG chitiethoadon = new CHITIETDONTHANG();
            foreach(var item in giohang)
            {
                double temp = Math.Round(double.Parse(item.Giaban.ToString()) * double.Parse((1 - item.Sale).ToString()), 0);
                //string price = int.Parse(temp.ToString());
                chitiethoadon.MaDonHang = mahoadon;
                chitiethoadon.MaSanPham = item.MaSanPham;
                chitiethoadon.Soluong = item.SoLuong;
                chitiethoadon.Dongia = temp.ToString();
                db.CHITIETDONTHANGs.Add(chitiethoadon);
                db.SaveChanges();
            }

            Session["CART"] = null;
            Session["TOTAL"] = null;
            return PartialView("_Order");
        }

        public ActionResult Product(int id)
        {
            var sanpham = db.SANPHAMs
                .Where(s => s.MaSanPham == id && s.DaXoa == null)
                .Select(sp => sp)
                .FirstOrDefault();
            Session["CATEGORY"] = sanpham.CHUDE.MaCD;
            ViewBag.Category = Session["CATEGORY"];
            ViewBag.SanPham = sanpham;

            if (id == 40)
            {
                var theloai = db.SANPHAMs
                .Where(s => s.DaXoa == null)
                .Select(sp => new SanPhamModels
                {
                    MaSanPham = sp.MaSanPham,
                    TenSanPham = sp.TenSanPham,
                    Giaban = sp.Giaban,
                    Mota = sp.Mota,
                    GioiTinh = sp.GioiTinh,
                    MaCD = sp.MaCD,
                    TheLoai = sp.CHUDE.TenChuDe,
                    SoLuongAnh = sp.SoLuongAnh,
                    Soluongton = sp.Soluongton,
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
                    Ngaycapnhat = sp.Ngaycapnhat,
                    GiaSale = (double)sp.Giaban * (1 - sp.Sale),
                    KhuyenMai = sp.Sale * 100,
                    DaXoa = sp.DaXoa,
                })
                .ToList();
                ViewBag.TheLoai = theloai;
            }
            else if (id == 36)
            {
                var theloai = db.SANPHAMs
                .Where(s =>s.GioiTinh == "Male" || s.GioiTinh == "All" && s.DaXoa == null)
                .Select(sp => new SanPhamModels
                {
                    MaSanPham = sp.MaSanPham,
                    TenSanPham = sp.TenSanPham,
                    Giaban = sp.Giaban,
                    Mota = sp.Mota,
                    GioiTinh = sp.GioiTinh,
                    MaCD = sp.MaCD,
                    TheLoai = sp.CHUDE.TenChuDe,
                    SoLuongAnh = sp.SoLuongAnh,
                    Soluongton = sp.Soluongton,
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
                    Ngaycapnhat = sp.Ngaycapnhat,
                    GiaSale = (double)sp.Giaban * (1 - sp.Sale),
                    KhuyenMai = sp.Sale * 100,
                    DaXoa = sp.DaXoa,
                })
                .ToList();
                ViewBag.TheLoai = theloai;
            }

            else if (id == 33)
            {
                var theloai = db.SANPHAMs
                .Where(s => s.GioiTinh == "Female" || s.GioiTinh == "All" && s.DaXoa == null)
                .Select(sp => new SanPhamModels
                {
                    MaSanPham = sp.MaSanPham,
                    TenSanPham = sp.TenSanPham,
                    Giaban = sp.Giaban,
                    Mota = sp.Mota,
                    GioiTinh = sp.GioiTinh,
                    MaCD = sp.MaCD,
                    TheLoai = sp.CHUDE.TenChuDe,
                    SoLuongAnh = sp.SoLuongAnh,
                    Soluongton = sp.Soluongton,
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
                    Ngaycapnhat = sp.Ngaycapnhat,
                    GiaSale = (double)sp.Giaban * (1 - sp.Sale),
                    KhuyenMai = sp.Sale * 100,
                    DaXoa = sp.DaXoa,
                })
                .ToList();
                ViewBag.TheLoai = theloai;
            }
            else
            {
                var theloai = db.SANPHAMs
                .Where(s => sanpham.MaCD == s.CHUDE.MaCD && s.DaXoa == null)
                .Select(sp => new SanPhamModels
                {
                    MaSanPham = sp.MaSanPham,
                    TenSanPham = sp.TenSanPham,
                    Giaban = sp.Giaban,
                    Mota = sp.Mota,
                    GioiTinh = sp.GioiTinh,
                    MaCD = sp.MaCD,
                    TheLoai = sp.CHUDE.TenChuDe,
                    SoLuongAnh = sp.SoLuongAnh,
                    Soluongton = sp.Soluongton,
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
                    Ngaycapnhat = sp.Ngaycapnhat,
                    GiaSale = (double)sp.Giaban * (1 - sp.Sale),
                    KhuyenMai = sp.Sale * 100,
                    DaXoa = sp.DaXoa,
                })
                .ToList();
                ViewBag.TheLoai = theloai;
            }
            

            DateTime today = DateTime.Today;
            ViewBag.HomNay = today;
            return View();
        }

        public ActionResult Product_detail(int id)
        {
            var sanpham = db.SANPHAMs
                .Where(s => s.MaSanPham == id && s.DaXoa == null )
                .Select(sp => new SanPhamModels
                {
                    MaSanPham = sp.MaSanPham,
                    TenSanPham = sp.TenSanPham,
                    Giaban = sp.Giaban,
                    Mota = sp.Mota,
                    GioiTinh = sp.GioiTinh,
                    MaCD = sp.MaCD,
                    TheLoai = sp.CHUDE.TenChuDe,
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
                    Ngaycapnhat = sp.Ngaycapnhat,
                    Soluongton = sp.Soluongton,
                    Sale = sp.Sale,
                    GiaSale = (double)sp.Giaban * (1 - sp.Sale),
                    KhuyenMai = sp.Sale * 100,
                    DaXoa = sp.DaXoa,
                })
                .FirstOrDefault();
            ViewBag.SanPham = sanpham;

            var theloai = db.SANPHAMs
                .Where(s => sanpham.MaCD == s.CHUDE.MaCD && s.DaXoa == null && (sanpham.GioiTinh == "All" ? (s.GioiTinh == "Male" || s.GioiTinh == "Female" || s.GioiTinh == "All") : (sanpham.GioiTinh == s.GioiTinh || s.GioiTinh == "All")))
                .Select(sp => new SanPhamModels
                {
                    MaSanPham = sp.MaSanPham,
                    TenSanPham = sp.TenSanPham,
                    Giaban = sp.Giaban,
                    Mota = sp.Mota,
                    GioiTinh = sp.GioiTinh,
                    MaCD = sp.MaCD,
                    TheLoai = sp.CHUDE.TenChuDe,
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
                    Ngaycapnhat = sp.Ngaycapnhat,
                    GiaSale = (double)sp.Giaban * (1 - sp.Sale),
                    KhuyenMai = sp.Sale * 100,
                    DaXoa = sp.DaXoa,
                })
                .ToList();
            ViewBag.TheLoai = theloai;

            DateTime today = DateTime.Today;
            ViewBag.HomNay = today;
            return View();
        }

        
        public ActionResult Wishlist()
        {
            if (Session["USER"] == null)
                return RedirectToAction("Account", "Account");
            var yeuthich = Session["WISHLIST"] as List<SanPhamModels>;
            ViewBag.YeuThich = yeuthich;
           
            return View();
        }

        public ActionResult AddToWishlist(int id)
        {
            if (Session["USER"] == null)
                return RedirectToAction("Account", "Account");
            else
            {
                var yeuthich = Session["WISHLIST"] as List<SanPhamModels>;
                if (yeuthich == null)
                {
                    yeuthich = new List<SanPhamModels>();
                    Session["WISHLIST"] = yeuthich;
                }
                var temp = db.SANPHAMs.Where(x => x.MaSanPham == id).FirstOrDefault();

                var timkiem = yeuthich.Find(x => x.MaSanPham == id);
                if (timkiem == null)
                {
                    var sanpham = new SanPhamModels();
                    sanpham.TenSanPham = temp.TenSanPham;
                    sanpham.Sale = temp.Sale;
                    sanpham.Hinh1 = temp.Hinh1;
                    sanpham.Giaban = temp.Giaban;
                    sanpham.MaSanPham = temp.MaSanPham;
                    if (temp.Soluongton == 0)
                    {
                        sanpham.TinhTrang = "Out of Stock";
                    }
                    else { sanpham.TinhTrang = "In Stock"; }

                    yeuthich.Add(sanpham);

                }
                else
                {
                    
                }

                //return RedirectToAction("Cart","Shopping");

                return PartialView("_AddToWishList");
            }
        }

        public ActionResult RemoveFormWishList(int id)
        {
            var yeuthich = Session["WISHLIST"] as List<SanPhamModels>;

            var timkiem = yeuthich.Find(x => x.MaSanPham == id);
            if (timkiem != null)
            {
                var temp = db.SANPHAMs.Where(x => x.MaSanPham == id).FirstOrDefault();

                yeuthich.RemoveAll(x => x.MaSanPham == id);
            }
            return RedirectToAction("WishList", "Shopping");
        }
    }
}