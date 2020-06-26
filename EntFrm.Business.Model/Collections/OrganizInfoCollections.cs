using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class OrganizInfoCollections:CollectionBase
  {

      public OrganizInfo this[int index]
      {
         get { return (OrganizInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(OrganizInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(OrganizInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, OrganizInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(OrganizInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(OrganizInfo value)
     {
       return (List.Contains(value));
     }

     public OrganizInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

