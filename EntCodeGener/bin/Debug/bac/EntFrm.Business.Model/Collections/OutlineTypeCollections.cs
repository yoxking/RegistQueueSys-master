using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class OutlineTypeCollections:CollectionBase
  {

      public OutlineType this[int index]
      {
         get { return (OutlineType)List[index]; }
         set { List[index] = value; }
      }

      public int Add(OutlineType value)
      {
          return (List.Add(value));
     }

     public int IndexOf(OutlineType value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, OutlineType value)
     {
         List.Insert(index, value);
      }

     public void Remove(OutlineType value)
    {
         List.Remove(value);
     }

    public bool Contains(OutlineType value)
     {
       return (List.Contains(value));
     }

     public OutlineType GetFirstOne()
     {
         return this[0];
     }

    }
  }

