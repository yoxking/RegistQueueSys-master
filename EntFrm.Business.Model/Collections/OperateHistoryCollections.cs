using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class OperateHistoryCollections:CollectionBase
  {

      public OperateHistory this[int index]
      {
         get { return (OperateHistory)List[index]; }
         set { List[index] = value; }
      }

      public int Add(OperateHistory value)
      {
          return (List.Add(value));
     }

     public int IndexOf(OperateHistory value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, OperateHistory value)
     {
         List.Insert(index, value);
      }

     public void Remove(OperateHistory value)
    {
         List.Remove(value);
     }

    public bool Contains(OperateHistory value)
     {
       return (List.Contains(value));
     }

     public OperateHistory GetFirstOne()
     {
         return this[0];
     }

    }
  }

