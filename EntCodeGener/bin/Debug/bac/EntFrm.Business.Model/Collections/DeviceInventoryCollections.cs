using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class DeviceInventoryCollections:CollectionBase
  {

      public DeviceInventory this[int index]
      {
         get { return (DeviceInventory)List[index]; }
         set { List[index] = value; }
      }

      public int Add(DeviceInventory value)
      {
          return (List.Add(value));
     }

     public int IndexOf(DeviceInventory value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, DeviceInventory value)
     {
         List.Insert(index, value);
      }

     public void Remove(DeviceInventory value)
    {
         List.Remove(value);
     }

    public bool Contains(DeviceInventory value)
     {
       return (List.Contains(value));
     }

     public DeviceInventory GetFirstOne()
     {
         return this[0];
     }

    }
  }

