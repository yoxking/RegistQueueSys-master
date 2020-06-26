using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class DsMaterialClassCollections:CollectionBase
  {

      public DsMaterialClass this[int index]
      {
         get { return (DsMaterialClass)List[index]; }
         set { List[index] = value; }
      }

      public int Add(DsMaterialClass value)
      {
          return (List.Add(value));
     }

     public int IndexOf(DsMaterialClass value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, DsMaterialClass value)
     {
         List.Insert(index, value);
      }

     public void Remove(DsMaterialClass value)
    {
         List.Remove(value);
     }

    public bool Contains(DsMaterialClass value)
     {
       return (List.Contains(value));
     }

     public DsMaterialClass GetFirstOne()
     {
         return this[0];
     }

    }
  }

