using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class DeviceClassInfoCollections:CollectionBase
  {

      public DeviceClassInfo this[int index]
      {
         get { return (DeviceClassInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(DeviceClassInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(DeviceClassInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, DeviceClassInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(DeviceClassInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(DeviceClassInfo value)
     {
       return (List.Contains(value));
     }

     public DeviceClassInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

