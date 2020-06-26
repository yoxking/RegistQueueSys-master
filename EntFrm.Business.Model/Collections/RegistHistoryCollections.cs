using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class RegistHistoryCollections:CollectionBase
  {

      public RegistHistory this[int index]
      {
         get { return (RegistHistory)List[index]; }
         set { List[index] = value; }
      }

      public int Add(RegistHistory value)
      {
          return (List.Add(value));
     }

     public int IndexOf(RegistHistory value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, RegistHistory value)
     {
         List.Insert(index, value);
      }

     public void Remove(RegistHistory value)
    {
         List.Remove(value);
     }

    public bool Contains(RegistHistory value)
     {
       return (List.Contains(value));
     }

     public RegistHistory GetFirstOne()
     {
         return this[0];
     }

    }
  }

