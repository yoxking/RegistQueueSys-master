using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class ProcessHistoryCollections:CollectionBase
  {

      public ProcessHistory this[int index]
      {
         get { return (ProcessHistory)List[index]; }
         set { List[index] = value; }
      }

      public int Add(ProcessHistory value)
      {
          return (List.Add(value));
     }

     public int IndexOf(ProcessHistory value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, ProcessHistory value)
     {
         List.Insert(index, value);
      }

     public void Remove(ProcessHistory value)
    {
         List.Remove(value);
     }

    public bool Contains(ProcessHistory value)
     {
       return (List.Contains(value));
     }

     public ProcessHistory GetFirstOne()
     {
         return this[0];
     }

    }
  }

