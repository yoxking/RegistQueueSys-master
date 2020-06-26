using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class RecipeDetailsCollections:CollectionBase
  {

      public RecipeDetails this[int index]
      {
         get { return (RecipeDetails)List[index]; }
         set { List[index] = value; }
      }

      public int Add(RecipeDetails value)
      {
          return (List.Add(value));
     }

     public int IndexOf(RecipeDetails value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, RecipeDetails value)
     {
         List.Insert(index, value);
      }

     public void Remove(RecipeDetails value)
    {
         List.Remove(value);
     }

    public bool Contains(RecipeDetails value)
     {
       return (List.Contains(value));
     }

     public RecipeDetails GetFirstOne()
     {
         return this[0];
     }

    }
  }

