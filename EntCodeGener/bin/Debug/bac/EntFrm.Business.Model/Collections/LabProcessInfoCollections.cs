using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class LabProcessInfoCollections:CollectionBase
  {

      public LabProcessInfo this[int index]
      {
         get { return (LabProcessInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(LabProcessInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(LabProcessInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, LabProcessInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(LabProcessInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(LabProcessInfo value)
     {
       return (List.Contains(value));
     }

     public LabProcessInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

