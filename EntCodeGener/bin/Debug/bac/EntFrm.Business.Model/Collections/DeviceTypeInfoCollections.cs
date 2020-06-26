using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class DeviceTypeInfoCollections:CollectionBase
  {

      public DeviceTypeInfo this[int index]
      {
         get { return (DeviceTypeInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(DeviceTypeInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(DeviceTypeInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, DeviceTypeInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(DeviceTypeInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(DeviceTypeInfo value)
     {
       return (List.Contains(value));
     }

     public DeviceTypeInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

