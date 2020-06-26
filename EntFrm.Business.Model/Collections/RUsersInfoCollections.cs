using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class RUsersInfoCollections:CollectionBase
  {

      public RUsersInfo this[int index]
      {
         get { return (RUsersInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(RUsersInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(RUsersInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, RUsersInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(RUsersInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(RUsersInfo value)
     {
       return (List.Contains(value));
     }

     public RUsersInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

