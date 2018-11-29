using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class MenuItemModel
    {
        [DisplayName("Menu Id")]
        public int Menu_Id { get; set; }
        [Required]
        [DisplayName("Name")]
        public string Menu_Name { get; set; }
        [Required]
        [DisplayName("Description")]
        public string Menu_Desc { get; set; }
        [Required]
        [DisplayName("Price")]
        public float Menu_Price { get; set; }
    }
}