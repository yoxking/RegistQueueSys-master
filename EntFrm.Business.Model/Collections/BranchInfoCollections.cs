using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class BranchInfoCollections:CollectionBase
  {

      public BranchInfo this[int index]
      {
         get { return (BranchInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(BranchInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(BranchInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, BranchInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(BranchInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(BranchInfo value)
     {
       return (List.Contains(value));
     }

     public BranchInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

