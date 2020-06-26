using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class SWorkFlowsCollections:CollectionBase
  {

      public SWorkFlows this[int index]
      {
         get { return (SWorkFlows)List[index]; }
         set { List[index] = value; }
      }

      public int Add(SWorkFlows value)
      {
          return (List.Add(value));
     }

     public int IndexOf(SWorkFlows value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, SWorkFlows value)
     {
         List.Insert(index, value);
      }

     public void Remove(SWorkFlows value)
    {
         List.Remove(value);
     }

    public bool Contains(SWorkFlows value)
     {
       return (List.Contains(value));
     }

     public SWorkFlows GetFirstOne()
     {
         return this[0];
     }

    }
  }

