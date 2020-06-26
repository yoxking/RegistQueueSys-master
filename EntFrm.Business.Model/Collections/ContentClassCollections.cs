using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class ContentClassCollections:CollectionBase
  {

      public ContentClass this[int index]
      {
         get { return (ContentClass)List[index]; }
         set { List[index] = value; }
      }

      public int Add(ContentClass value)
      {
          return (List.Add(value));
     }

     public int IndexOf(ContentClass value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, ContentClass value)
     {
         List.Insert(index, value);
      }

     public void Remove(ContentClass value)
    {
         List.Remove(value);
     }

    public bool Contains(ContentClass value)
     {
       return (List.Contains(value));
     }

     public ContentClass GetFirstOne()
     {
         return this[0];
     }

    }
  }

