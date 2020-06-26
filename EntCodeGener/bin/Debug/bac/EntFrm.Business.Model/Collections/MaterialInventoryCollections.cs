using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class MaterialInventoryCollections:CollectionBase
  {

      public MaterialInventory this[int index]
      {
         get { return (MaterialInventory)List[index]; }
         set { List[index] = value; }
      }

      public int Add(MaterialInventory value)
      {
          return (List.Add(value));
     }

     public int IndexOf(MaterialInventory value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, MaterialInventory value)
     {
         List.Insert(index, value);
      }

     public void Remove(MaterialInventory value)
    {
         List.Remove(value);
     }

    public bool Contains(MaterialInventory value)
     {
       return (List.Contains(value));
     }

     public MaterialInventory GetFirstOne()
     {
         return this[0];
     }

    }
  }

