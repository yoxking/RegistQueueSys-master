using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class DsMaterialInfoCollections:CollectionBase
  {

      public DsMaterialInfo this[int index]
      {
         get { return (DsMaterialInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(DsMaterialInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(DsMaterialInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, DsMaterialInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(DsMaterialInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(DsMaterialInfo value)
     {
       return (List.Contains(value));
     }

     public DsMaterialInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

