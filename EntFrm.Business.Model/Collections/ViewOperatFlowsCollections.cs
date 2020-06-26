using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class ViewOperatFlowsCollections:CollectionBase
  {

      public ViewOperatFlows this[int index]
      {
         get { return (ViewOperatFlows)List[index]; }
         set { List[index] = value; }
      }

      public int Add(ViewOperatFlows value)
      {
          return (List.Add(value));
     }

     public int IndexOf(ViewOperatFlows value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, ViewOperatFlows value)
     {
         List.Insert(index, value);
      }

     public void Remove(ViewOperatFlows value)
    {
         List.Remove(value);
     }

    public bool Contains(ViewOperatFlows value)
     {
       return (List.Contains(value));
     }

     public ViewOperatFlows GetFirstOne()
     {
         return this[0];
     }

    }
  }

