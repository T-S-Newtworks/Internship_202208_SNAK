//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはテンプレートから生成されました。
//
//     このファイルを手動で変更すると、アプリケーションで予期しない動作が発生する可能性があります。
//     このファイルに対する手動の変更は、コードが再生成されると上書きされます。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Internship_Template.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class T_QUESTION
    {
        public string ID { get; set; }
        public string QUESTIONNAIREID { get; set; }
        public string CONTENT { get; set; }
        public string OPTION1 { get; set; }
        public string OPTION2 { get; set; }
        public string OPTION3 { get; set; }
        public string OPTION4 { get; set; }
        public string OPTION5 { get; set; }
        [NotMapped]
        public string OPTION { get; set; }
        public byte[] IMAGE1 { get; set; }
        public byte[] IMAGE2 { get; set; }
        public byte[] IMAGE3 { get; set; }
        public byte[] IMAGE4 { get; set; }
        public byte[] IMAGE5 { get; set; }
    }
}
