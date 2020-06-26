using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class AbstractCollections:CollectionBase
  {

      public Abstract this[int index]
      {
         get { return (Abstract)List[index]; }
         set { List[index] = value; }
      }

      public int Add(Abstract value)
      {
          return (List.Add(value));
     }

     public int IndexOf(Abstract value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, Abstract value)
     {
         List.Insert(index, value);
      }

     public void Remove(Abstract value)
    {
         List.Remove(value);
     }

    public bool Contains(Abstract value)
     {
       return (List.Contains(value));
     }

     public Abstract GetFirstOne()
     {
         return this[0];
     }

    }
  }

