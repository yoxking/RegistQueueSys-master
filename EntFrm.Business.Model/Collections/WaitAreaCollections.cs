using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class WaitAreaCollections:CollectionBase
  {

      public WaitArea this[int index]
      {
         get { return (WaitArea)List[index]; }
         set { List[index] = value; }
      }

      public int Add(WaitArea value)
      {
          return (List.Add(value));
     }

     public int IndexOf(WaitArea value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, WaitArea value)
     {
         List.Insert(index, value);
      }

     public void Remove(WaitArea value)
    {
         List.Remove(value);
     }

    public bool Contains(WaitArea value)
     {
       return (List.Contains(value));
     }

     public WaitArea GetFirstOne()
     {
         return this[0];
     }

    }
  }

