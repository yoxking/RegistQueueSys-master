using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class UserRoleCollections:CollectionBase
  {

      public UserRole this[int index]
      {
         get { return (UserRole)List[index]; }
         set { List[index] = value; }
      }

      public int Add(UserRole value)
      {
          return (List.Add(value));
     }

     public int IndexOf(UserRole value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, UserRole value)
     {
         List.Insert(index, value);
      }

     public void Remove(UserRole value)
    {
         List.Remove(value);
     }

    public bool Contains(UserRole value)
     {
       return (List.Contains(value));
     }

     public UserRole GetFirstOne()
     {
         return this[0];
     }

    }
  }

