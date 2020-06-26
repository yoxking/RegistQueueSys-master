using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class TermInfoCollections:CollectionBase
  {

      public TermInfo this[int index]
      {
         get { return (TermInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(TermInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(TermInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, TermInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(TermInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(TermInfo value)
     {
       return (List.Contains(value));
     }

     public TermInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

