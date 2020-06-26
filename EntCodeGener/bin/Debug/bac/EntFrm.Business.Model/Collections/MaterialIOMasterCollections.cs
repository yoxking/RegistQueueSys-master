using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class MaterialIOMasterCollections:CollectionBase
  {

      public MaterialIOMaster this[int index]
      {
         get { return (MaterialIOMaster)List[index]; }
         set { List[index] = value; }
      }

      public int Add(MaterialIOMaster value)
      {
          return (List.Add(value));
     }

     public int IndexOf(MaterialIOMaster value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, MaterialIOMaster value)
     {
         List.Insert(index, value);
      }

     public void Remove(MaterialIOMaster value)
    {
         List.Remove(value);
     }

    public bool Contains(MaterialIOMaster value)
     {
       return (List.Contains(value));
     }

     public MaterialIOMaster GetFirstOne()
     {
         return this[0];
     }

    }
  }

