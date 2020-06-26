using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class TicketFlowsCollections:CollectionBase
  {

      public TicketFlows this[int index]
      {
         get { return (TicketFlows)List[index]; }
         set { List[index] = value; }
      }

      public int Add(TicketFlows value)
      {
          return (List.Add(value));
     }

     public int IndexOf(TicketFlows value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, TicketFlows value)
     {
         List.Insert(index, value);
      }

     public void Remove(TicketFlows value)
    {
         List.Remove(value);
     }

    public bool Contains(TicketFlows value)
     {
       return (List.Contains(value));
     }

     public TicketFlows GetFirstOne()
     {
         return this[0];
     }

    }
  }

