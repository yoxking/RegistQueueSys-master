using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class MaterialClassInfoCollections:CollectionBase
  {

      public MaterialClassInfo this[int index]
      {
         get { return (MaterialClassInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(MaterialClassInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(MaterialClassInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, MaterialClassInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(MaterialClassInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(MaterialClassInfo value)
     {
       return (List.Contains(value));
     }

     public MaterialClassInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

