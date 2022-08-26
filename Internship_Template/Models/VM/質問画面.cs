using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Internship_Template.Models.Entity;


namespace Internship_Template.Models.VM
{
    public class 質問画面 :_BaseViewModel
    {


        public T_QUESTIONNAIRE Questionaire { get; set; }
        /// <summary>
        /// メンバー一覧
        /// </summary>
        public List<T_QUESTION> Qestions { get; set; }
    }
}