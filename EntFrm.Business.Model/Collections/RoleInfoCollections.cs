using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class RoleInfoCollections:CollectionBase
  {

      public RoleInfo this[int index]
      {
         get { return (RoleInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(RoleInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(RoleInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, RoleInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(RoleInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(RoleInfo value)
     {
       return (List.Contains(value));
     }

     public RoleInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

