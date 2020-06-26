using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class PermitInfoCollections:CollectionBase
  {

      public PermitInfo this[int index]
      {
         get { return (PermitInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(PermitInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(PermitInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, PermitInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(PermitInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(PermitInfo value)
     {
       return (List.Contains(value));
     }

     public PermitInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

