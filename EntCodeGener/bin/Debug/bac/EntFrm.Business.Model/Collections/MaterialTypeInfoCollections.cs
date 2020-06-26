using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class MaterialTypeInfoCollections:CollectionBase
  {

      public MaterialTypeInfo this[int index]
      {
         get { return (MaterialTypeInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(MaterialTypeInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(MaterialTypeInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, MaterialTypeInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(MaterialTypeInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(MaterialTypeInfo value)
     {
       return (List.Contains(value));
     }

     public MaterialTypeInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

