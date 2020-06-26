using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class DsHrtbeatFlowsCollections:CollectionBase
  {

      public DsHrtbeatFlows this[int index]
      {
         get { return (DsHrtbeatFlows)List[index]; }
         set { List[index] = value; }
      }

      public int Add(DsHrtbeatFlows value)
      {
          return (List.Add(value));
     }

     public int IndexOf(DsHrtbeatFlows value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, DsHrtbeatFlows value)
     {
         List.Insert(index, value);
      }

     public void Remove(DsHrtbeatFlows value)
    {
         List.Remove(value);
     }

    public bool Contains(DsHrtbeatFlows value)
     {
       return (List.Contains(value));
     }

     public DsHrtbeatFlows GetFirstOne()
     {
         return this[0];
     }

    }
  }

