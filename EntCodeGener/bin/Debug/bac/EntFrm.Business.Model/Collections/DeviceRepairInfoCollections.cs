using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class DeviceRepairInfoCollections:CollectionBase
  {

      public DeviceRepairInfo this[int index]
      {
         get { return (DeviceRepairInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(DeviceRepairInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(DeviceRepairInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, DeviceRepairInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(DeviceRepairInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(DeviceRepairInfo value)
     {
       return (List.Contains(value));
     }

     public DeviceRepairInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

