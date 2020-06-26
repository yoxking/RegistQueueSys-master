using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class DeviceGuaranteeInfoCollections:CollectionBase
  {

      public DeviceGuaranteeInfo this[int index]
      {
         get { return (DeviceGuaranteeInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(DeviceGuaranteeInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(DeviceGuaranteeInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, DeviceGuaranteeInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(DeviceGuaranteeInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(DeviceGuaranteeInfo value)
     {
       return (List.Contains(value));
     }

     public DeviceGuaranteeInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

