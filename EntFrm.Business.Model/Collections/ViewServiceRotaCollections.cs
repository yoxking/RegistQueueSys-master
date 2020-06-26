using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class ViewServiceRotaCollections:CollectionBase
  {

      public ViewServiceRota this[int index]
      {
         get { return (ViewServiceRota)List[index]; }
         set { List[index] = value; }
      }

      public int Add(ViewServiceRota value)
      {
          return (List.Add(value));
     }

     public int IndexOf(ViewServiceRota value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, ViewServiceRota value)
     {
         List.Insert(index, value);
      }

     public void Remove(ViewServiceRota value)
    {
         List.Remove(value);
     }

    public bool Contains(ViewServiceRota value)
     {
       return (List.Contains(value));
     }

     public ViewServiceRota GetFirstOne()
     {
         return this[0];
     }

    }
  }

