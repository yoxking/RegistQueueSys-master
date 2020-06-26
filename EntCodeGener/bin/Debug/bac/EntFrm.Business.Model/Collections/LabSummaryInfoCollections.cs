using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class LabSummaryInfoCollections:CollectionBase
  {

      public LabSummaryInfo this[int index]
      {
         get { return (LabSummaryInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(LabSummaryInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(LabSummaryInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, LabSummaryInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(LabSummaryInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(LabSummaryInfo value)
     {
       return (List.Contains(value));
     }

     public LabSummaryInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

