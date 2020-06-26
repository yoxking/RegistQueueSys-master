using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class RegistFlowsCollections:CollectionBase
  {

      public RegistFlows this[int index]
      {
         get { return (RegistFlows)List[index]; }
         set { List[index] = value; }
      }

      public int Add(RegistFlows value)
      {
          return (List.Add(value));
     }

     public int IndexOf(RegistFlows value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, RegistFlows value)
     {
         List.Insert(index, value);
      }

     public void Remove(RegistFlows value)
    {
         List.Remove(value);
     }

    public bool Contains(RegistFlows value)
     {
       return (List.Contains(value));
     }

     public RegistFlows GetFirstOne()
     {
         return this[0];
     }

    }
  }

