using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class PlanTypeCollections:CollectionBase
  {

      public PlanType this[int index]
      {
         get { return (PlanType)List[index]; }
         set { List[index] = value; }
      }

      public int Add(PlanType value)
      {
          return (List.Add(value));
     }

     public int IndexOf(PlanType value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, PlanType value)
     {
         List.Insert(index, value);
      }

     public void Remove(PlanType value)
    {
         List.Remove(value);
     }

    public bool Contains(PlanType value)
     {
       return (List.Contains(value));
     }

     public PlanType GetFirstOne()
     {
         return this[0];
     }

    }
  }

