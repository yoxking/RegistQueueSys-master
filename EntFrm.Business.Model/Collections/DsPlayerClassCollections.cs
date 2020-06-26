using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class DsPlayerClassCollections:CollectionBase
  {

      public DsPlayerClass this[int index]
      {
         get { return (DsPlayerClass)List[index]; }
         set { List[index] = value; }
      }

      public int Add(DsPlayerClass value)
      {
          return (List.Add(value));
     }

     public int IndexOf(DsPlayerClass value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, DsPlayerClass value)
     {
         List.Insert(index, value);
      }

     public void Remove(DsPlayerClass value)
    {
         List.Remove(value);
     }

    public bool Contains(DsPlayerClass value)
     {
       return (List.Contains(value));
     }

     public DsPlayerClass GetFirstOne()
     {
         return this[0];
     }

    }
  }

