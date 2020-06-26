using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class StafferRotaCollections:CollectionBase
  {

      public StafferRota this[int index]
      {
         get { return (StafferRota)List[index]; }
         set { List[index] = value; }
      }

      public int Add(StafferRota value)
      {
          return (List.Add(value));
     }

     public int IndexOf(StafferRota value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, StafferRota value)
     {
         List.Insert(index, value);
      }

     public void Remove(StafferRota value)
    {
         List.Remove(value);
     }

    public bool Contains(StafferRota value)
     {
       return (List.Contains(value));
     }

     public StafferRota GetFirstOne()
     {
         return this[0];
     }

    }
  }

