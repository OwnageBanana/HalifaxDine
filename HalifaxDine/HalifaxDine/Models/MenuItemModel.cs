﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HalifaxDine.Models
{
    public class MenuItemModel
    {
        public int Menu_Id { get; set; }
        [Required]
        public string Menu_Name { get; set; }
        [Required]
        public string Menu_Desc { get; set; }
        [Required]
        public float Menu_Price { get; set; }
    }
}