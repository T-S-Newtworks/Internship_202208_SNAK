using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Internship_Template.Models.Entity;
using Internship_Template.Models.VM;


namespace Internship_Template.Controllers
{
    public class ImageController : _BaseController
    {
        /// <summary>
        /// 画像表示用のアクション
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Show(string id)
        {
            T_USER target = _db.T_USER.Where(e => e.ID == id).FirstOrDefault();
            string mimetype = target.MIMETYPE;

            return File(target.ICON, mimetype);
        }
    }
}