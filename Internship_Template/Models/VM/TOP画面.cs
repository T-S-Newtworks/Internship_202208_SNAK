using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Internship_Template.Models.Entity;


namespace Internship_Template.Models.VM
{
    public class TOP画面 :_BaseViewModel
    {
        /// <summary>
        /// 市民の声
        /// </summary>
        public List<T_OPINION> Opinions { get; set; }
        public T_OPINION InputOpinions { get; set; }

        public List<T_QUESTIONNAIRE> Questionnaire { get; set; }

    }
}