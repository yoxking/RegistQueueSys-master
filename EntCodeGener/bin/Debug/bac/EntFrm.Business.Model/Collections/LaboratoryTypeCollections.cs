using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class LaboratoryTypeCollections:CollectionBase
  {

      public LaboratoryType this[int index]
      {
         get { return (LaboratoryType)List[index]; }
         set { List[index] = value; }
      }

      public int Add(LaboratoryType value)
      {
          return (List.Add(value));
     }

     public int IndexOf(LaboratoryType value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, LaboratoryType value)
     {
         List.Insert(index, value);
      }

     public void Remove(LaboratoryType value)
    {
         List.Remove(value);
     }

    public bool Contains(LaboratoryType value)
     {
       return (List.Contains(value));
     }

     public LaboratoryType GetFirstOne()
     {
         return this[0];
     }

    }
  }

