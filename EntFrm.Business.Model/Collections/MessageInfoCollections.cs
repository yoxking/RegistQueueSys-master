using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class MessageInfoCollections:CollectionBase
  {

      public MessageInfo this[int index]
      {
         get { return (MessageInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(MessageInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(MessageInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, MessageInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(MessageInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(MessageInfo value)
     {
       return (List.Contains(value));
     }

     public MessageInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

