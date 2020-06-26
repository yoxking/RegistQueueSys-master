using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class DeviceUnitInfoCollections:CollectionBase
  {

      public DeviceUnitInfo this[int index]
      {
         get { return (DeviceUnitInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(DeviceUnitInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(DeviceUnitInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, DeviceUnitInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(DeviceUnitInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(DeviceUnitInfo value)
     {
       return (List.Contains(value));
     }

     public DeviceUnitInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

