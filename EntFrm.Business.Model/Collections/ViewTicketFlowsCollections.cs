using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class ViewTicketFlowsCollections:CollectionBase
  {

      public ViewTicketFlows this[int index]
      {
         get { return (ViewTicketFlows)List[index]; }
         set { List[index] = value; }
      }

      public int Add(ViewTicketFlows value)
      {
          return (List.Add(value));
     }

     public int IndexOf(ViewTicketFlows value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, ViewTicketFlows value)
     {
         List.Insert(index, value);
      }

     public void Remove(ViewTicketFlows value)
    {
         List.Remove(value);
     }

    public bool Contains(ViewTicketFlows value)
     {
       return (List.Contains(value));
     }

     public ViewTicketFlows GetFirstOne()
     {
         return this[0];
     }

    }
  }

