using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class TeachResourceTypeCollections:CollectionBase
  {

      public TeachResourceType this[int index]
      {
         get { return (TeachResourceType)List[index]; }
         set { List[index] = value; }
      }

      public int Add(TeachResourceType value)
      {
          return (List.Add(value));
     }

     public int IndexOf(TeachResourceType value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, TeachResourceType value)
     {
         List.Insert(index, value);
      }

     public void Remove(TeachResourceType value)
    {
         List.Remove(value);
     }

    public bool Contains(TeachResourceType value)
     {
       return (List.Contains(value));
     }

     public TeachResourceType GetFirstOne()
     {
         return this[0];
     }

    }
  }

