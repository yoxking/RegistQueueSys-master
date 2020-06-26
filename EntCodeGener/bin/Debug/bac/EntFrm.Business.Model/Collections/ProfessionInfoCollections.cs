using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class ProfessionInfoCollections:CollectionBase
  {

      public ProfessionInfo this[int index]
      {
         get { return (ProfessionInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(ProfessionInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(ProfessionInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, ProfessionInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(ProfessionInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(ProfessionInfo value)
     {
       return (List.Contains(value));
     }

     public ProfessionInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

