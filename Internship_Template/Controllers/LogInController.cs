using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Internship_Template.Models.Entity;
using Internship_Template.Models.VM;


namespace Internship_Template.Controllers
{
    public class LogInController : _BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index(ログイン画面 model = null)
        {
            //認証済みなら表示しない。
            if(User != null)
            {
                //return this.Redirect("../Home/Index");
            }

            return View("Index",model?? new ログイン画面());
        }

        /// <summary>
        /// ログインの実行
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ExecuteLogin(ログイン画面 model)
        {
           if(model.Login.logintype == "1")
            {
                if (string.IsNullOrEmpty(model.Login.ID) || string.IsNullOrEmpty(model.Login.PASSWORD))
                {
                    // ユーザー認証 失敗
                    this.ModelState.AddModelError("LoginError", "IDおよびパスワードは必須です。");
                    return Index(model);

                }

                T_LOGIN loginInfo = _db.T_LOGIN.Find(model.Login.ID);
                if (loginInfo != null && loginInfo.PASSWORD == model.Login.PASSWORD)
                {
                    // ユーザー認証 成功
                    //LoginInfoをもとにユーザー情報を取得
                    T_USER loginUser = _db.T_USER.Find(loginInfo.ID);
                    HttpContext.Session[M_SESSION.SessionKey] = loginUser;
                    return this.Redirect("/");
                }
                else
                {
                    // ユーザー認証 失敗
                    this.ModelState.AddModelError("LoginError", "指定されたユーザー名またはパスワードが正しくありません。");
                    return Index(model);
                }

            }
            else
            {
                //T_USER loginUser = _db.T_USER.Find("zzzzz"); 

                T_USER loginUser=new T_USER();
                loginUser.ID = "zzzzzz";
                loginUser.FULLNAME = "ゲスト";
                HttpContext.Session[M_SESSION.SessionKey] = loginUser;
                return this.Redirect("/");

            }



        }

        /// <summary>
        /// ログアウト実行
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExecuteLogout()
        {
            //セッション情報を破棄する。
            HttpContext.Session.RemoveAll();

            ログイン画面 model = new ログイン画面();
            model.Messages.Add("Logout", "ログアウトしました。利用する際は再度ログインしてください。");
            return Index(model);
        }

    }

}