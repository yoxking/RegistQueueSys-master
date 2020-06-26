using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class CategoriesCollections:CollectionBase
  {

      public Categories this[int index]
      {
         get { return (Categories)List[index]; }
         set { List[index] = value; }
      }

      public int Add(Categories value)
      {
          return (List.Add(value));
     }

     public int IndexOf(Categories value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, Categories value)
     {
         List.Insert(index, value);
      }

     public void Remove(Categories value)
    {
         List.Remove(value);
     }

    public bool Contains(Categories value)
     {
       return (List.Contains(value));
     }

     public Categories GetFirstOne()
     {
         return this[0];
     }

    }
  }

