using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class CounterInfoCollections:CollectionBase
  {

      public CounterInfo this[int index]
      {
         get { return (CounterInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(CounterInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(CounterInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, CounterInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(CounterInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(CounterInfo value)
     {
       return (List.Contains(value));
     }

     public CounterInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

