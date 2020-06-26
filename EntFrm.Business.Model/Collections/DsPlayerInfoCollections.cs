using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class DsPlayerInfoCollections:CollectionBase
  {

      public DsPlayerInfo this[int index]
      {
         get { return (DsPlayerInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(DsPlayerInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(DsPlayerInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, DsPlayerInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(DsPlayerInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(DsPlayerInfo value)
     {
       return (List.Contains(value));
     }

     public DsPlayerInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

