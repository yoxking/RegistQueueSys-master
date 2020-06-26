using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class LaboratoryTableCollections:CollectionBase
  {

      public LaboratoryTable this[int index]
      {
         get { return (LaboratoryTable)List[index]; }
         set { List[index] = value; }
      }

      public int Add(LaboratoryTable value)
      {
          return (List.Add(value));
     }

     public int IndexOf(LaboratoryTable value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, LaboratoryTable value)
     {
         List.Insert(index, value);
      }

     public void Remove(LaboratoryTable value)
    {
         List.Remove(value);
     }

    public bool Contains(LaboratoryTable value)
     {
       return (List.Contains(value));
     }

     public LaboratoryTable GetFirstOne()
     {
         return this[0];
     }

    }
  }

