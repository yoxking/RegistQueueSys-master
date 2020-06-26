using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class DeviceInfoCollections:CollectionBase
  {

      public DeviceInfo this[int index]
      {
         get { return (DeviceInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(DeviceInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(DeviceInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, DeviceInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(DeviceInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(DeviceInfo value)
     {
       return (List.Contains(value));
     }

     public DeviceInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

