using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntWeb.MedicConsole.Models
{
    public class RecipeData
    {
        public string RecipeNo { set; get; }
        public string RecipeName { set; get; }
        public string RecipeSpec { set; get; } 
        public string Standard { set; get; }
        public double Price { set; get; }
        public double Amount { set; get; }
        public string SQuantity { set; get; }
        public string TQuantity { set; get; }
        public string Direction { set; get; }
        public string Frequency { set; get; }

    }
}