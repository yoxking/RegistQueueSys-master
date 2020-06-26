using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class EvaluatorInfoCollections:CollectionBase
  {

      public EvaluatorInfo this[int index]
      {
         get { return (EvaluatorInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(EvaluatorInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(EvaluatorInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, EvaluatorInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(EvaluatorInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(EvaluatorInfo value)
     {
       return (List.Contains(value));
     }

     public EvaluatorInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

