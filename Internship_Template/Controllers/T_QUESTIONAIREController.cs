using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Internship_Template.Models.Entity;
using Internship_Template.Models.VM;


namespace Internship_Template.Controllers
{
    public class T_QUESTIONAIREController : _BaseController
    {
        private Internship_Context db = new Internship_Context();

        // GET: T_QUESTION
        public ActionResult Index()
        {
            質問画面 model = new 質問画面();
            model.LoginedUser = User;
            model.Qestions = db.T_QUESTION.ToList() ?? new List<T_QUESTION>();
            return View(model);
        }

        // GET: T_QUESTION/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_QUESTION t_QUESTION = db.T_QUESTION.Find(id);
            if (t_QUESTION == null)
            {
                return HttpNotFound();
            }
            return View(t_QUESTION);
        }

        // GET: T_QUESTION/Create
        public ActionResult Create(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            質問画面 model = new 質問画面();
            model.LoginedUser = User;
            model.Questionaire = db.T_QUESTIONNAIRE.Where(e => e.ID == id).FirstOrDefault();//選択されたアンケート
            model.Qestions = db.T_QUESTION.Where(e => e.QUESTIONNAIREID == model.Questionaire.ID).ToList() ?? new List<T_QUESTION>();
            return View(model);
        }

        // POST: T_QUESTION/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*Bind(Include = "ID,QUESTIONNAIREID,CONTENT,OPTION1,OPTION2,OPTION3,OPTION4,OPTION5")]*/ 質問画面 model)
        {
            if (ModelState.IsValid)
            {
                //質問の回答をDBにとうろく
                List<T_ANSWER> answer = new List<T_ANSWER>(); 

                int count =  db.T_ANSWER.ToList().Count();
                for (int i = 0; i < model.Qestions.Count; i++)
                {
                    answer.Add(new T_ANSWER {
                        QUESTIONNAIREID = model.Questionaire.ID,
                        USERID = User.ID,
                        QUESTIONID = model.Qestions[i].ID,
                    OPTION = model.Qestions[i].OPTION,

                    ID = (count + i + 1).ToString(),
                });

                }
                //答えを登録
                db.T_ANSWER.AddRange(answer);
                //保存
                db.SaveChanges();

                return RedirectToAction("../Home/Index");
            }

            return View(model);
        }

        // GET: T_QUESTION/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_QUESTION t_QUESTION = db.T_QUESTION.Find(id);
            if (t_QUESTION == null)
            {
                return HttpNotFound();
            }
            return View(t_QUESTION);
        }

        // POST: T_QUESTION/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,QUESTIONNAIREID,CONTENT,OPTION1,OPTION2,OPTION3,OPTION4,OPTION5")] T_QUESTION t_QUESTION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_QUESTION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_QUESTION);
        }

        // GET: T_QUESTION/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_QUESTION t_QUESTION = db.T_QUESTION.Find(id);
            if (t_QUESTION == null)
            {
                return HttpNotFound();
            }
            return View(t_QUESTION);
        }

        // POST: T_QUESTION/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            T_QUESTION t_QUESTION = db.T_QUESTION.Find(id);
            db.T_QUESTION.Remove(t_QUESTION);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
