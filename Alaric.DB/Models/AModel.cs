using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaric.DB.Models
{
    public class AModel: Identifiable
    {
        [Attr(PublicName ="sym")]
        public string Sym { get; set; }

        [Attr(PublicName = "tradeprice")]
        public decimal tradePrice { get; set; }

        [Attr(PublicName = "tradesize")]
        public int tradeSize { get; set; }

        [Attr(PublicName = "partid")]
        public char partId { get; set; }

        [Attr(PublicName = "modifydt")]
        public DateTime modifyDt { get; set; }

    }
}
