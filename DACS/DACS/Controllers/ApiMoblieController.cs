using DACS.Models;
using DACS.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;


namespace DACS.Controllers
{
    public class ApiMoblieController : ApiController
    {
        [HttpGet]
        public string TestApi()
        {
            return "ok";
        }
        [HttpPost]
        public JsonResult<ApiMobile> Login(USER login)
        {
            ApiMobile result = new ApiMobile();

            try
            {

                if (String.IsNullOrEmpty(login.USERNAME) || String.IsNullOrEmpty(login.PASSWORD)) throw new Exception("Vui lòng nhập đầy đủ thông tin đăng nhập");

                using (DACSEntities db = new DACSEntities())
                {
                    login.PASSWORD = db.proc_CryptData(login.PASSWORD).FirstOrDefault();

                 
                    if (db.USERs.Any(x => x.USERNAME.Equals(login.USERNAME)) == false)
                    {
                        result.Message = "Tài khoản không tồn tại!";
                        return Json(result);

                    }
                    else
                    {
                        if (!db.USERs.Any(x => x.USERNAME.Equals(login.USERNAME) && x.PASSWORD.Equals(login.PASSWORD)))
                        {
                            result.Message = "Tài khoản hoặc mật khẩu không chính xác, vui lòng kiểm tra lại";
                            return Json(result);
                        }
                        else
                        {
                            Login data = new Login();
                            var kq = db.USERs.Where(x => x.USERNAME.Equals(login.USERNAME.ToLower().Trim()) && x.PASSWORD.Equals(login.PASSWORD)).FirstOrDefault();
                            var student = db.STUDENTs.Where(x => x.IDUSER == kq.IDUSER).FirstOrDefault();
                            var idRole = kq.IDROLE;
                            if (student == null && idRole != 3)
                            {
                                result.Message = "Tài khoản sinh viên chưa được đăng ký";
                                return Json(result);
                            }
                            data.IDUSER = kq.IDUSER;
                            data.USERNAME = kq.USERNAME;
                            data.FULLNAME = kq.FULLNAME;
                            data.PHONE = kq.PHONE;
                            data.EMAIL = kq.EMAIL;
                            data.ADDRESS = kq.ADDRESS;
                            data.IDROLE = kq.IDROLE;
                            
                            result.Data = data;
                            result.Success = true;
                            result.Message = "Đăng nhập thành công!";
                          
                        }

                    }
                    return Json(result);

                }

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult<ApiMobile> SignUp(USER user)
        {
            ApiMobile result = new ApiMobile();
            if (user is null)
            {
                result.Message = "Vui lòng điền đầy đủ thông tin bên trên";
                return Json(result);
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
                u.IDROLE = 3;
                u.IDPERMISSION = 1;
                u.PASSWORD = user.PASSWORD;
                if (user.IDUSER == 0) db.USERs.Add(u);
                try
                {
                    db.SaveChanges();
                    result.Success = true;
                    result.Message = "Đăng ký thành công!";
                }
                catch(Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
                return Json(result);
        }
    }
}
