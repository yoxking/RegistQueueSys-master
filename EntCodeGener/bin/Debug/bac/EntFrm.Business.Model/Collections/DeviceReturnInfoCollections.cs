using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class DeviceReturnInfoCollections:CollectionBase
  {

      public DeviceReturnInfo this[int index]
      {
         get { return (DeviceReturnInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(DeviceReturnInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(DeviceReturnInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, DeviceReturnInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(DeviceReturnInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(DeviceReturnInfo value)
     {
       return (List.Contains(value));
     }

     public DeviceReturnInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

