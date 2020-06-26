using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class CallerInfoCollections:CollectionBase
  {

      public CallerInfo this[int index]
      {
         get { return (CallerInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(CallerInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(CallerInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, CallerInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(CallerInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(CallerInfo value)
     {
       return (List.Contains(value));
     }

     public CallerInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

