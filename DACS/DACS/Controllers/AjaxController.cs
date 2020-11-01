using DACS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DACS.Helper;

namespace DACS.Controllers
{
    public class AjaxController : Controller
    {
        // GET: Ajax


        public JsonResult GetPosition(int id)
        {
            //if (Session["AdminLogin"] is null) return null;

            using (DACSEntities db = new DACSEntities())
            {
                var role = db.ROLEs.Where(x => x.IDROLE == id).FirstOrDefault();
                if (role is null) return null;
                return new JsonResult()
                {
                    Data = new { NAME = role.NAMEROLE },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
        [HttpPost]
        public JsonResult EditPosition(ROLE role)
        {
            //if (Session["AdminLogin"] is null) return null;
            ApiResult result = new ApiResult();
            if (role is null)
            {
                result.Message = "Vui lòng điền đủ thông tin";
                return Json(result.Data);
            }
            using (DACSEntities db = new DACSEntities())
            {
                ROLE r = new ROLE();
                if (role.IDROLE > 0) r = db.ROLEs.Where(x => x.IDROLE == role.IDROLE).FirstOrDefault();
                if (r is null)
                {
                    result.Message = "Vui lòng thử lại";
                    return Json(result);
                }
                r.IDROLE = role.IDROLE;
                r.NAMEROLE = role.NAMEROLE;
                if (role.IDROLE == 0) db.ROLEs.Add(r);
                try
                {
                    db.SaveChanges();
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }

            return Json(result);
        }
        [HttpPost]
        public string DeleteRole(int id)
        {
            //if (Session["AdminLogin"] is null) return null;

            using (DACSEntities db = new DACSEntities())
            {
                var role = db.ROLEs.Where(x => x.IDROLE == id).FirstOrDefault();
                if (role is null) return "Không tìm thấy đối tượng này";
                db.ROLEs.Remove(role);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return "ok";
        }
        public JsonResult GetUser(int id)
        {
            using (DACSEntities db = new DACSEntities())
            {
                var user = db.USERs.Where(x => x.IDUSER == id).FirstOrDefault();
                if (user is null) return null;
                return new JsonResult()
                {
                    Data = new { FULLNAME = user.FULLNAME, EMAIL = user.EMAIL, PHONE = user.PHONE, USERNAME = user.USERNAME, ROLE = user.IDROLE },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        [HttpPost]
        public JsonResult PostUser(USER user, string finalString)
        {
            SendMail.EmailUser = "namnguyenhoang2404@gmail.com";
            SendMail.EmailPassword = "nhok_1998";
            SendMail.EmailName = "Cấp thông tin tài khoản cho nhân viên";

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            finalString = new String(stringChars);


            ApiResult result = new ApiResult();
            if (user is null)
            {
                result.Message = "Vui lòng điền đầy đủ thông tin bên trên";
                return Json(result.Data);
            }
            using (DACSEntities db = new DACSEntities())
            {
                USER u = new USER();
                if (user.IDUSER > 0) u = db.USERs.Where(x => x.IDUSER == user.IDUSER).FirstOrDefault();
                if (u is null)
                {
                    result.Message = "Vui lòng thử lại";
                    return Json(result);
                }
                u.FULLNAME = user.FULLNAME;
                u.USERNAME = user.USERNAME;
                u.PHONE = user.PHONE;
                u.EMAIL = user.EMAIL;
                u.IDROLE = user.IDROLE;
                u.PASSWORD = finalString;
                u.IDPERMISSION = 1;
                if (user.IDUSER == 0) db.USERs.Add(u);
                try
                {
                    db.SaveChanges();
                    System.Net.Mail.MailAddress m = new System.Net.Mail.MailAddress(u.EMAIL);
                    string body = @" <div style='width:600px;border: 1px solid black;'>
                                                <div style='background-color:rgb(13, 213, 240);padding: 10px;'>
                                                    <center><h2 style='margin:auto;'>THÔNG TIN TÀI KHOẢN CỦA BẠN</h2></center>
                                                </div>
                                                <div style='padding: 20px;'>
                                                    <p>Thông tin tài khoản: {0}</p>
                                                    <p>Mật khẩu của bạn là: <b>{1}</b></p>
                                                    <p>Vui lòng đăng nhập và đổi mật khẩu mới</p>
                                                </div>

                                            </div>";
                    body = String.Format(body, u.USERNAME, finalString);
                    SendMail.Send(user.EMAIL, "Cấp thông tin tài khoản", body);
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }

            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult DeleteUser(int id)
        {
            ApiResult result = new ApiResult();
            using (DACSEntities db = new DACSEntities())
            {
                var user = db.USERs.Where(x => x.IDUSER == id).FirstOrDefault();
                if (user is null)
                {
                    result.Message = "Vui lòng thử lại";
                    return Json(result);
                }
                try
                {
                    db.USERs.Remove(user);
                    db.SaveChanges();
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return Json(result);
        }
        public JsonResult GetNganh(int id)
        {
            using (DACSEntities db = new DACSEntities())
            {
                var nganh = db.NGANHs.Where(x => x.IDNGANH == id).FirstOrDefault();
                if (nganh is null) return null;
                return new JsonResult()
                {
                    Data = new { NAMENGANH = nganh.NAMENGANH },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
        [HttpPost]
        public JsonResult PostNganh(NGANH nganh)
        {
            ApiResult result = new ApiResult();
            if (nganh is null)
            {
                result.Message = "Vui lòng điền đủ thông tin";
                return Json(result.Data);
            }
            using (DACSEntities db = new DACSEntities())
            {
                NGANH n = new NGANH();
                if (nganh.IDNGANH > 0) n = db.NGANHs.Where(x => x.IDNGANH == nganh.IDNGANH).FirstOrDefault();
                if (n is null)
                {
                    result.Message = "Vui lòng thử lại";
                    return Json(result);
                }
                n.NAMENGANH = nganh.NAMENGANH;
                n.IDNIENKHOA = nganh.IDNIENKHOA;

                if (nganh.IDNGANH == 0) db.NGANHs.Add(n);
                try
                {
                    db.SaveChanges();
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult DeleteNganh(int id)
        {
            ApiResult result = new ApiResult();
            using (DACSEntities db = new DACSEntities())
            {
                var nganh = db.NGANHs.Where(x => x.IDNGANH == id).FirstOrDefault();
                if (nganh is null)
                {
                    result.Message = "Vui lòng thử lại";
                    return Json(result);
                }
                try
                {
                    db.NGANHs.Remove(nganh);
                    db.SaveChanges();
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult PostNienKhoa(NIENKHOA nienkhoa)
        {
            ApiResult result = new ApiResult();
            if (nienkhoa is null)
            {
                result.Message = "Vui lòng điền đủ thông tin";
                return Json(result.Data);
            }
            using (DACSEntities db = new DACSEntities())
            {
                NIENKHOA n = new NIENKHOA();
                if (nienkhoa.IDNIENKHOA > 0) n = db.NIENKHOAs.Where(x => x.IDNIENKHOA == nienkhoa.IDNIENKHOA).FirstOrDefault();
                if (n is null)
                {
                    result.Message = "Vui lòng thử lại";
                    return Json(result);
                }
                n.IDNIENKHOA = nienkhoa.IDNIENKHOA;
                n.NAMENIENKHOA = nienkhoa.NAMENIENKHOA;

                if (nienkhoa.IDNIENKHOA == 0) db.NIENKHOAs.Add(n);
                try
                {
                    db.SaveChanges();
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return Json(result);
        }

        public JsonResult PostMonHoc(MONHOC monhoc)
        {
            ApiResult result = new ApiResult();
            if (monhoc is null)
            {
                result.Message = "Vui lòng điền đủ thông tin";
                return Json(result.Data);
            }
            using (DACSEntities db = new DACSEntities())
            {
                MONHOC n = new MONHOC();
                if (monhoc.IDMONHOC > 0) n = db.MONHOCs.Where(x => x.IDMONHOC == monhoc.IDMONHOC).FirstOrDefault();
                if (n is null)
                {
                    result.Message = "Vui lòng thử lại";
                    return Json(result);
                }
                
                n.TENMONHOC = monhoc.TENMONHOC;
                n.IDTINCHI = monhoc.IDTINCHI;
             
                if (monhoc.IDMONHOC == 0) db.MONHOCs.Add(n);
                try
                {
                    db.SaveChanges();
                    result.Success = true;
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return Json(result);
        }
     
    }
}