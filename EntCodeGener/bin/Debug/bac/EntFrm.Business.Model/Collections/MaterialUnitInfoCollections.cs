using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class MaterialUnitInfoCollections:CollectionBase
  {

      public MaterialUnitInfo this[int index]
      {
         get { return (MaterialUnitInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(MaterialUnitInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(MaterialUnitInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, MaterialUnitInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(MaterialUnitInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(MaterialUnitInfo value)
     {
       return (List.Contains(value));
     }

     public MaterialUnitInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

