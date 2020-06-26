using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class ViewRecipeFlowsCollections:CollectionBase
  {

      public ViewRecipeFlows this[int index]
      {
         get { return (ViewRecipeFlows)List[index]; }
         set { List[index] = value; }
      }

      public int Add(ViewRecipeFlows value)
      {
          return (List.Add(value));
     }

     public int IndexOf(ViewRecipeFlows value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, ViewRecipeFlows value)
     {
         List.Insert(index, value);
      }

     public void Remove(ViewRecipeFlows value)
    {
         List.Remove(value);
     }

    public bool Contains(ViewRecipeFlows value)
     {
       return (List.Contains(value));
     }

     public ViewRecipeFlows GetFirstOne()
     {
         return this[0];
     }

    }
  }

