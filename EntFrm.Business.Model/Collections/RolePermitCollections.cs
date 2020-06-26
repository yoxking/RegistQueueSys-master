using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class RolePermitCollections:CollectionBase
  {

      public RolePermit this[int index]
      {
         get { return (RolePermit)List[index]; }
         set { List[index] = value; }
      }

      public int Add(RolePermit value)
      {
          return (List.Add(value));
     }

     public int IndexOf(RolePermit value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, RolePermit value)
     {
         List.Insert(index, value);
      }

     public void Remove(RolePermit value)
    {
         List.Remove(value);
     }

    public bool Contains(RolePermit value)
     {
       return (List.Contains(value));
     }

     public RolePermit GetFirstOne()
     {
         return this[0];
     }

    }
  }

