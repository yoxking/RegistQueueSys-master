using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class DsDwloadFlowsCollections:CollectionBase
  {

      public DsDwloadFlows this[int index]
      {
         get { return (DsDwloadFlows)List[index]; }
         set { List[index] = value; }
      }

      public int Add(DsDwloadFlows value)
      {
          return (List.Add(value));
     }

     public int IndexOf(DsDwloadFlows value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, DsDwloadFlows value)
     {
         List.Insert(index, value);
      }

     public void Remove(DsDwloadFlows value)
    {
         List.Remove(value);
     }

    public bool Contains(DsDwloadFlows value)
     {
       return (List.Contains(value));
     }

     public DsDwloadFlows GetFirstOne()
     {
         return this[0];
     }

    }
  }

