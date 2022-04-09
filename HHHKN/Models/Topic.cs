using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HHHKN.Models
{
    [Table("Topics")]
    public class Topic
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Slug { get; set; }
        public int ParentId { get; set; }
        public int Orders { get; set; }
        [Required]
        public string Metakey { get; set; }
        [Required]
        public string Metadesc { get; set; }

        public int? Created_By { get; set; }
        public DateTime? Created_At { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Update_At { get; set; }
        public int Status { get; set; }
    }
}