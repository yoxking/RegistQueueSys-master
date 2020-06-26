using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntFrm.DataAdapter.HisData
{
    public class HisRecipeDetail
    {
        public string Guid { set; get; }  //no 
        public string RegistId { set; get; }  //门诊号
        public string HospitId { set; get; }  //住院号
        public string PatId { set; get; }  //病人ID

        public string RecipeName { set; get; }  //药品名称
        public string RecipeSpec { set; get; }  //规格

        public string SeriNumber { set; get; }  //批号

        public string Standard { set; get; }//单位

        public float Price { set; get; }  //单价
        public float Amount { set; get; }  //金额

        public string SQuantity { set; get; }  //付数
        public string TQuantity { set; get; }  //数次
        public string Direction { set; get; }  //用法
        public string ExpiryDate { set; get; }//有效期
        public string Frequency { set; get; }//频次
        public string Remark { set; get; }   //备注
    }
}
