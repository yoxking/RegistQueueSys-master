using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class DsProgramInfoCollections:CollectionBase
  {

      public DsProgramInfo this[int index]
      {
         get { return (DsProgramInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(DsProgramInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(DsProgramInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, DsProgramInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(DsProgramInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(DsProgramInfo value)
     {
       return (List.Contains(value));
     }

     public DsProgramInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

