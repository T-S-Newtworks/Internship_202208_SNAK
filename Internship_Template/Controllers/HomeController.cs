using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Text;
using Internship_Template.Models.Entity;
using Internship_Template.Models.VM;

namespace Internship_Template.Controllers
{
    /// <summary>
    /// Top画面のコントローラです。
    /// </summary>
    /// <remarks>Top画面に機能を追加したい場合はここに処理を追加しましょう。</remarks>
    public class HomeController : _BaseController
    {
        public ActionResult Index()
        {
            TempData.Remove("model");
            TOP画面 model = new TOP画面();
            model.Opinions = _db.T_OPINION.ToList() ?? new List<T_OPINION>();
            model.Questionnaire = _db.T_QUESTIONNAIRE.ToList() ?? new List<T_QUESTIONNAIRE>();
            List<T_ANSWER> answers = new List<T_ANSWER>();
            foreach(var questionaire in model.Questionnaire)
            {
                if (User.ID == "zzzzzz")
                {
                    questionaire.ANSWER = "未回答";
                }
                else
                {
                    answers = _db.T_ANSWER.Where(e => e.QUESTIONNAIREID == questionaire.ID).ToList();
                    answers = answers.Where(e => e.USERID == User.ID).ToList();
                    if(answers.Count > 0)
                    {
                        questionaire.ANSWER = "回答済";

                    }
                    else
                    {
                        questionaire.ANSWER = "未回答";

                    }
                }

            }
            model.LoginedUser = User;

            return View(model);

            //TempData.Remove("model");
            //TOP画面 model = new TOP画面();
            //model.Users = _db.T_USER.ToList() ?? new List<T_USER>();
            //model.LoginedUser = User;

            //return View(model);

            TempData.Remove("qestion");
            TOP画面 qestion = new TOP画面();
            qestion.Questionnaire = _db.T_QUESTIONNAIRE.ToList() ?? new List<T_QUESTIONNAIRE>();
            qestion.LoginedUser = User;

            return View(qestion);


        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Regist(TOP画面 model)
        {
            T_OPINION opi = new T_OPINION();
            opi.ID = (_db.T_OPINION.Count() + 1).ToString();
            T_USER loginUser = (T_USER)this.Session[M_SESSION.SessionKey];
            opi.USERID = loginUser.ID;
            opi.TEXT = model.InputOpinions.TEXT;
            _db.T_OPINION.Add(opi);
            _db.SaveChanges();
            return this.Redirect("/");

        }

    }
}