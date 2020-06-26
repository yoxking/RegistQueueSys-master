using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class SUsersInfoCollections:CollectionBase
  {

      public SUsersInfo this[int index]
      {
         get { return (SUsersInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(SUsersInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(SUsersInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, SUsersInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(SUsersInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(SUsersInfo value)
     {
       return (List.Contains(value));
     }

     public SUsersInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

