using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class DeviceFailedInfoCollections:CollectionBase
  {

      public DeviceFailedInfo this[int index]
      {
         get { return (DeviceFailedInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(DeviceFailedInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(DeviceFailedInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, DeviceFailedInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(DeviceFailedInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(DeviceFailedInfo value)
     {
       return (List.Contains(value));
     }

     public DeviceFailedInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

