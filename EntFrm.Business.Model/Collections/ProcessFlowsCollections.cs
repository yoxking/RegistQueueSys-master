using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class ProcessFlowsCollections:CollectionBase
  {

      public ProcessFlows this[int index]
      {
         get { return (ProcessFlows)List[index]; }
         set { List[index] = value; }
      }

      public int Add(ProcessFlows value)
      {
          return (List.Add(value));
     }

     public int IndexOf(ProcessFlows value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, ProcessFlows value)
     {
         List.Insert(index, value);
      }

     public void Remove(ProcessFlows value)
    {
         List.Remove(value);
     }

    public bool Contains(ProcessFlows value)
     {
       return (List.Contains(value));
     }

     public ProcessFlows GetFirstOne()
     {
         return this[0];
     }

    }
  }

