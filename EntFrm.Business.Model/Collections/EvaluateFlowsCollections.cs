using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class EvaluateFlowsCollections:CollectionBase
  {

      public EvaluateFlows this[int index]
      {
         get { return (EvaluateFlows)List[index]; }
         set { List[index] = value; }
      }

      public int Add(EvaluateFlows value)
      {
          return (List.Add(value));
     }

     public int IndexOf(EvaluateFlows value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, EvaluateFlows value)
     {
         List.Insert(index, value);
      }

     public void Remove(EvaluateFlows value)
    {
         List.Remove(value);
     }

    public bool Contains(EvaluateFlows value)
     {
       return (List.Contains(value));
     }

     public EvaluateFlows GetFirstOne()
     {
         return this[0];
     }

    }
  }

