using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class StafferInfoCollections:CollectionBase
  {

      public StafferInfo this[int index]
      {
         get { return (StafferInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(StafferInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(StafferInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, StafferInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(StafferInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(StafferInfo value)
     {
       return (List.Contains(value));
     }

     public StafferInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

