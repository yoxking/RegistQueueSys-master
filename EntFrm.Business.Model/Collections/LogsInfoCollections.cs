using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class LogsInfoCollections:CollectionBase
  {

      public LogsInfo this[int index]
      {
         get { return (LogsInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(LogsInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(LogsInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, LogsInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(LogsInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(LogsInfo value)
     {
       return (List.Contains(value));
     }

     public LogsInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

