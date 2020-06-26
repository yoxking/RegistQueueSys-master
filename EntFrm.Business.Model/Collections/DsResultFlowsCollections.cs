using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class DsResultFlowsCollections:CollectionBase
  {

      public DsResultFlows this[int index]
      {
         get { return (DsResultFlows)List[index]; }
         set { List[index] = value; }
      }

      public int Add(DsResultFlows value)
      {
          return (List.Add(value));
     }

     public int IndexOf(DsResultFlows value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, DsResultFlows value)
     {
         List.Insert(index, value);
      }

     public void Remove(DsResultFlows value)
    {
         List.Remove(value);
     }

    public bool Contains(DsResultFlows value)
     {
       return (List.Contains(value));
     }

     public DsResultFlows GetFirstOne()
     {
         return this[0];
     }

    }
  }

