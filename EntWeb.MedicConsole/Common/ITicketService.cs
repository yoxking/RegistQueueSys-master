using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntWeb.MedicConsole.Common
{
    public class ITicketService
    {
        private volatile static ITicketService _instance = null;
        private static readonly object lockHelper = new object();

        public static ITicketService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new ITicketService();
                }
            }
            return _instance;
        }

        public bool FindBoxInfo(string patId, string windowNo)
        {
            try
            {
                RecipeService.HospitalServiceSoapClient recipeService = new RecipeService.HospitalServiceSoapClient();
                string s = recipeService.GetBoxInfo(patId, windowNo);

                return s.Equals("1");
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool FinishRecipe(string patId, string windowNo)
        {
            try
            {
                RecipeService.HospitalServiceSoapClient recipeService = new RecipeService.HospitalServiceSoapClient();
                string s = recipeService.GetFirmpres(patId, windowNo);

                return s.Equals("1");
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}