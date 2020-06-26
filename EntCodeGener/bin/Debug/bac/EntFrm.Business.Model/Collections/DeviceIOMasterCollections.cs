using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class DeviceIOMasterCollections:CollectionBase
  {

      public DeviceIOMaster this[int index]
      {
         get { return (DeviceIOMaster)List[index]; }
         set { List[index] = value; }
      }

      public int Add(DeviceIOMaster value)
      {
          return (List.Add(value));
     }

     public int IndexOf(DeviceIOMaster value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, DeviceIOMaster value)
     {
         List.Insert(index, value);
      }

     public void Remove(DeviceIOMaster value)
    {
         List.Remove(value);
     }

    public bool Contains(DeviceIOMaster value)
     {
       return (List.Contains(value));
     }

     public DeviceIOMaster GetFirstOne()
     {
         return this[0];
     }

    }
  }

