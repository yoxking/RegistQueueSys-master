using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class DsProgramClassCollections:CollectionBase
  {

      public DsProgramClass this[int index]
      {
         get { return (DsProgramClass)List[index]; }
         set { List[index] = value; }
      }

      public int Add(DsProgramClass value)
      {
          return (List.Add(value));
     }

     public int IndexOf(DsProgramClass value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, DsProgramClass value)
     {
         List.Insert(index, value);
      }

     public void Remove(DsProgramClass value)
    {
         List.Remove(value);
     }

    public bool Contains(DsProgramClass value)
     {
       return (List.Contains(value));
     }

     public DsProgramClass GetFirstOne()
     {
         return this[0];
     }

    }
  }

