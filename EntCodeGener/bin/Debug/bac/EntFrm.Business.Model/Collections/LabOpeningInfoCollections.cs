using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class LabOpeningInfoCollections:CollectionBase
  {

      public LabOpeningInfo this[int index]
      {
         get { return (LabOpeningInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(LabOpeningInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(LabOpeningInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, LabOpeningInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(LabOpeningInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(LabOpeningInfo value)
     {
       return (List.Contains(value));
     }

     public LabOpeningInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

