using System;

namespace EntFrm.Business.Model
{
  public class RecipeDetails
  {
     public RecipeDetails(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string RecipeNo;
       public string sRecipeNo
       {
           set { this.RecipeNo =value;}
           get { return this.RecipeNo;}
        }

       private string RFlowNo;
       public string sRFlowNo
       {
           set { this.RFlowNo =value;}
           get { return this.RFlowNo;}
        }

       private string RUserNo;
       public string sRUserNo
       {
           set { this.RUserNo =value;}
           get { return this.RUserNo;}
        }

       private string RecipeName;
       public string sRecipeName
       {
           set { this.RecipeName =value;}
           get { return this.RecipeName;}
        }

       private string RecipeSpec;
       public string sRecipeSpec
       {
           set { this.RecipeSpec =value;}
           get { return this.RecipeSpec;}
        }

       private string SeriNumber;
       public string sSeriNumber
       {
           set { this.SeriNumber =value;}
           get { return this.SeriNumber;}
        }

       private string Standard;
       public string sStandard
       {
           set { this.Standard =value;}
           get { return this.Standard;}
        }

       private double Price;
       public double dPrice
       {
           set { this.Price =value;}
           get { return this.Price;}
        }

       private double Amount;
       public double dAmount
       {
           set { this.Amount =value;}
           get { return this.Amount;}
        }

       private string SQuantity;
       public string sSQuantity
       {
           set { this.SQuantity =value;}
           get { return this.SQuantity;}
        }

       private string TQuantity;
       public string sTQuantity
       {
           set { this.TQuantity =value;}
           get { return this.TQuantity;}
        }

       private string Direction;
       public string sDirection
       {
           set { this.Direction =value;}
           get { return this.Direction;}
        }

       private DateTime ExpiryDate;
       public DateTime dExpiryDate
       {
           set { this.ExpiryDate =value;}
           get { return this.ExpiryDate;}
        }

       private string Frequency;
       public string sFrequency
       {
           set { this.Frequency =value;}
           get { return this.Frequency;}
        }

       private string AddOptor;
       public string sAddOptor
       {
           set { this.AddOptor =value;}
           get { return this.AddOptor;}
        }

       private DateTime AddDate;
       public DateTime dAddDate
       {
           set { this.AddDate =value;}
           get { return this.AddDate;}
        }

       private string ModOptor;
       public string sModOptor
       {
           set { this.ModOptor =value;}
           get { return this.ModOptor;}
        }

       private DateTime ModDate;
       public DateTime dModDate
       {
           set { this.ModDate =value;}
           get { return this.ModDate;}
        }

       private int ValidityState;
       public int iValidityState
       {
           set { this.ValidityState =value;}
           get { return this.ValidityState;}
        }

       private string Comments;
       public string sComments
       {
           set { this.Comments =value;}
           get { return this.Comments;}
        }

       private string AppCode;
       public string sAppCode
       {
           set { this.AppCode =value;}
           get { return this.AppCode;}
        }

       private string Version;
       public string sVersion
       {
           set { this.Version =value;}
           get { return this.Version;}
        }

    }
  }

