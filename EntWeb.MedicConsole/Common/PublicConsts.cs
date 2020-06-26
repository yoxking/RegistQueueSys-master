using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntWeb.MedicConsole.Common
{
    public class PublicConsts
    {
        public const int WORKMODE_MAKERECIPE = 1;  //配药
        public const int WORKMODE_TAKERECIPE =2;  //发药

        //RecipeState
        public const int RECIPESTATE1 = 1;  //等候中
        public const int RECIPESTATE2 = 2;  //配药中
        public const int RECIPESTATE3 = 3;  //已完成 

        //ProcessState
        public const int RROCESSSTATE1 = 1;  //等候中
        public const int RROCESSSTATE2 = 2;  //叫号中
        public const int RROCESSSTATE3 = 3;  //已过号
        public const int RROCESSSTATE4 = 4;  //已完成

    }
}