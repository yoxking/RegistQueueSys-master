using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class RecipeHistoryCollections:CollectionBase
  {

      public RecipeHistory this[int index]
      {
         get { return (RecipeHistory)List[index]; }
         set { List[index] = value; }
      }

      public int Add(RecipeHistory value)
      {
          return (List.Add(value));
     }

     public int IndexOf(RecipeHistory value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, RecipeHistory value)
     {
         List.Insert(index, value);
      }

     public void Remove(RecipeHistory value)
    {
         List.Remove(value);
     }

    public bool Contains(RecipeHistory value)
     {
       return (List.Contains(value));
     }

     public RecipeHistory GetFirstOne()
     {
         return this[0];
     }

    }
  }

