using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class DsQuartzInfoCollections:CollectionBase
  {

      public DsQuartzInfo this[int index]
      {
         get { return (DsQuartzInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(DsQuartzInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(DsQuartzInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, DsQuartzInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(DsQuartzInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(DsQuartzInfo value)
     {
       return (List.Contains(value));
     }

     public DsQuartzInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

