using LeDaiDuong_151901766.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LeDaiDuong_151901766.Controllers
{
    public class AccountController : Controller
    {
        DBWebEntities1 db = new DBWebEntities1();
        // GET: Account
        [ValidateInput(false)]
        [HttpGet]
        public ActionResult Account()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(TaiKhoanModels model)
        {
            if (model.NhapLaiMatKhau == model.MatKhau)
            {
                    KHACHHANG taikhoan = new KHACHHANG();
                    taikhoan.MaKH = model.MaKH;
                    taikhoan.HoTen = model.HoTen;
                    taikhoan.TaiKhoan = model.TaiKhoan;
                    taikhoan.MatKhau = model.MatKhau;
                    taikhoan.NgaySinh = model.NgaySinh;
                    taikhoan.DiaChi = model.DiaChi;
                    taikhoan.DienThoai = model.DienThoai;
                    taikhoan.Email = model.Email;

                    db.KHACHHANGs.Add(taikhoan);
                    db.SaveChanges();

                    var login = db.KHACHHANGs
                    .Where(x => x.TaiKhoan == model.TaiKhoan && x.MatKhau == model.MatKhau)
                    .FirstOrDefault();
                    if (taikhoan != null)
                    {
                        Session["USER"] = taikhoan;
                        return RedirectToAction("Index", "Home");
                    }
                    return RedirectToAction("Index", "Home");   
            }

            return RedirectToAction("Account", "Account");
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Login(TaiKhoanModels model)
        {

            var taikhoan = db.KHACHHANGs
             .Where(x => x.TaiKhoan == model.TaiKhoan && x.MatKhau == model.MatKhau)
             .FirstOrDefault();
            if (taikhoan != null)
            {
                Session["USER"] = taikhoan;
                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Account", "Account");
            
        }

        public ActionResult _User()
        {
            var user = Session["USER"];
            ViewBag.User = user;
            return PartialView();
        }

        public ActionResult _User2()
        {
            var user = Session["USER"];
            ViewBag.User = user;
            return PartialView();
        }

        public ActionResult _SignOut()
        {
            Session["USER"] = null;
            Session["PNUM"] = null;
            Session["TOTAL"] = null;
            Session["CART"] = null;
            return RedirectToAction("Index","Home");
        }
    }
}