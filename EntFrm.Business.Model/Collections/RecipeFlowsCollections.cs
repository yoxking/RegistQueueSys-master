using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class RecipeFlowsCollections:CollectionBase
  {

      public RecipeFlows this[int index]
      {
         get { return (RecipeFlows)List[index]; }
         set { List[index] = value; }
      }

      public int Add(RecipeFlows value)
      {
          return (List.Add(value));
     }

     public int IndexOf(RecipeFlows value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, RecipeFlows value)
     {
         List.Insert(index, value);
      }

     public void Remove(RecipeFlows value)
    {
         List.Remove(value);
     }

    public bool Contains(RecipeFlows value)
     {
       return (List.Contains(value));
     }

     public RecipeFlows GetFirstOne()
     {
         return this[0];
     }

    }
  }

