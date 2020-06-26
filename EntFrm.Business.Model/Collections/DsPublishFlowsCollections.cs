using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class DsPublishFlowsCollections:CollectionBase
  {

      public DsPublishFlows this[int index]
      {
         get { return (DsPublishFlows)List[index]; }
         set { List[index] = value; }
      }

      public int Add(DsPublishFlows value)
      {
          return (List.Add(value));
     }

     public int IndexOf(DsPublishFlows value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, DsPublishFlows value)
     {
         List.Insert(index, value);
      }

     public void Remove(DsPublishFlows value)
    {
         List.Remove(value);
     }

    public bool Contains(DsPublishFlows value)
     {
       return (List.Contains(value));
     }

     public DsPublishFlows GetFirstOne()
     {
         return this[0];
     }

    }
  }

