using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HHHKN.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public int CatId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Detail { get; set; }
        public string Metakey { get; set; }
        public string Metadesc { get; set; }
        public string Img { get; set; }
        public int Number { get; set; }
        public double Price { get; set; }
        public double Pricesale { get; set; }
        public int? Created_By { get; set; }
        public DateTime? Created_At { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Update_At { get; set; }
        public int Status { get; set; }
        public string CatName { get; set; }
    }
}