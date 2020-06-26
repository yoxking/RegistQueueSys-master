using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class TeachResourceInfoCollections:CollectionBase
  {

      public TeachResourceInfo this[int index]
      {
         get { return (TeachResourceInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(TeachResourceInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(TeachResourceInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, TeachResourceInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(TeachResourceInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(TeachResourceInfo value)
     {
       return (List.Contains(value));
     }

     public TeachResourceInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

