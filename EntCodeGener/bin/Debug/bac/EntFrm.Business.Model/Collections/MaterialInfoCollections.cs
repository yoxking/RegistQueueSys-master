using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class MaterialInfoCollections:CollectionBase
  {

      public MaterialInfo this[int index]
      {
         get { return (MaterialInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(MaterialInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(MaterialInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, MaterialInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(MaterialInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(MaterialInfo value)
     {
       return (List.Contains(value));
     }

     public MaterialInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

