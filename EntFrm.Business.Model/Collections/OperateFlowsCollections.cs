using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class OperateFlowsCollections:CollectionBase
  {

      public OperateFlows this[int index]
      {
         get { return (OperateFlows)List[index]; }
         set { List[index] = value; }
      }

      public int Add(OperateFlows value)
      {
          return (List.Add(value));
     }

     public int IndexOf(OperateFlows value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, OperateFlows value)
     {
         List.Insert(index, value);
      }

     public void Remove(OperateFlows value)
    {
         List.Remove(value);
     }

    public bool Contains(OperateFlows value)
     {
       return (List.Contains(value));
     }

     public OperateFlows GetFirstOne()
     {
         return this[0];
     }

    }
  }

