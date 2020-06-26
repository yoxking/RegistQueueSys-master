using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class ViewRegistFlowsCollections:CollectionBase
  {

      public ViewRegistFlows this[int index]
      {
         get { return (ViewRegistFlows)List[index]; }
         set { List[index] = value; }
      }

      public int Add(ViewRegistFlows value)
      {
          return (List.Add(value));
     }

     public int IndexOf(ViewRegistFlows value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, ViewRegistFlows value)
     {
         List.Insert(index, value);
      }

     public void Remove(ViewRegistFlows value)
    {
         List.Remove(value);
     }

    public bool Contains(ViewRegistFlows value)
     {
       return (List.Contains(value));
     }

     public ViewRegistFlows GetFirstOne()
     {
         return this[0];
     }

    }
  }

