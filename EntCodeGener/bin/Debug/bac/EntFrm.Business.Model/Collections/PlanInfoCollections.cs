using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class PlanInfoCollections:CollectionBase
  {

      public PlanInfo this[int index]
      {
         get { return (PlanInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(PlanInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(PlanInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, PlanInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(PlanInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(PlanInfo value)
     {
       return (List.Contains(value));
     }

     public PlanInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

