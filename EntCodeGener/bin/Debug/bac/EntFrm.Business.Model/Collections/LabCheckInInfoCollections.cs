using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class LabCheckInInfoCollections:CollectionBase
  {

      public LabCheckInInfo this[int index]
      {
         get { return (LabCheckInInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(LabCheckInInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(LabCheckInInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, LabCheckInInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(LabCheckInInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(LabCheckInInfo value)
     {
       return (List.Contains(value));
     }

     public LabCheckInInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

