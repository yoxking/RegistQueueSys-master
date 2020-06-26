using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class ViewStafferRotaCollections:CollectionBase
  {

      public ViewStafferRota this[int index]
      {
         get { return (ViewStafferRota)List[index]; }
         set { List[index] = value; }
      }

      public int Add(ViewStafferRota value)
      {
          return (List.Add(value));
     }

     public int IndexOf(ViewStafferRota value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, ViewStafferRota value)
     {
         List.Insert(index, value);
      }

     public void Remove(ViewStafferRota value)
    {
         List.Remove(value);
     }

    public bool Contains(ViewStafferRota value)
     {
       return (List.Contains(value));
     }

     public ViewStafferRota GetFirstOne()
     {
         return this[0];
     }

    }
  }

