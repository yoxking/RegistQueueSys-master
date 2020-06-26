using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class ContentInfoCollections:CollectionBase
  {

      public ContentInfo this[int index]
      {
         get { return (ContentInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(ContentInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(ContentInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, ContentInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(ContentInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(ContentInfo value)
     {
       return (List.Contains(value));
     }

     public ContentInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

