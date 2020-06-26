using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class DsVersionInfoCollections:CollectionBase
  {

      public DsVersionInfo this[int index]
      {
         get { return (DsVersionInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(DsVersionInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(DsVersionInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, DsVersionInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(DsVersionInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(DsVersionInfo value)
     {
       return (List.Contains(value));
     }

     public DsVersionInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

