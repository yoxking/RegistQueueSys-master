using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class TeachResourceDetailCollections:CollectionBase
  {

      public TeachResourceDetail this[int index]
      {
         get { return (TeachResourceDetail)List[index]; }
         set { List[index] = value; }
      }

      public int Add(TeachResourceDetail value)
      {
          return (List.Add(value));
     }

     public int IndexOf(TeachResourceDetail value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, TeachResourceDetail value)
     {
         List.Insert(index, value);
      }

     public void Remove(TeachResourceDetail value)
    {
         List.Remove(value);
     }

    public bool Contains(TeachResourceDetail value)
     {
       return (List.Contains(value));
     }

     public TeachResourceDetail GetFirstOne()
     {
         return this[0];
     }

    }
  }

