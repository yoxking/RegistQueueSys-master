using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class OutlineInfoCollections:CollectionBase
  {

      public OutlineInfo this[int index]
      {
         get { return (OutlineInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(OutlineInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(OutlineInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, OutlineInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(OutlineInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(OutlineInfo value)
     {
       return (List.Contains(value));
     }

     public OutlineInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

