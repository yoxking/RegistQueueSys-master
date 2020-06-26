using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class TicketStyleCollections:CollectionBase
  {

      public TicketStyle this[int index]
      {
         get { return (TicketStyle)List[index]; }
         set { List[index] = value; }
      }

      public int Add(TicketStyle value)
      {
          return (List.Add(value));
     }

     public int IndexOf(TicketStyle value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, TicketStyle value)
     {
         List.Insert(index, value);
      }

     public void Remove(TicketStyle value)
    {
         List.Remove(value);
     }

    public bool Contains(TicketStyle value)
     {
       return (List.Contains(value));
     }

     public TicketStyle GetFirstOne()
     {
         return this[0];
     }

    }
  }

