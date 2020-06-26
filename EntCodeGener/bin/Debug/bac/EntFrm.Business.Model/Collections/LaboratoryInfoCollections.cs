using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class LaboratoryInfoCollections:CollectionBase
  {

      public LaboratoryInfo this[int index]
      {
         get { return (LaboratoryInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(LaboratoryInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(LaboratoryInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, LaboratoryInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(LaboratoryInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(LaboratoryInfo value)
     {
       return (List.Contains(value));
     }

     public LaboratoryInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

