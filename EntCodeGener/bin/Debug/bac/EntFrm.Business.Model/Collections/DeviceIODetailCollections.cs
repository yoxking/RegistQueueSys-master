using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class DeviceIODetailCollections:CollectionBase
  {

      public DeviceIODetail this[int index]
      {
         get { return (DeviceIODetail)List[index]; }
         set { List[index] = value; }
      }

      public int Add(DeviceIODetail value)
      {
          return (List.Add(value));
     }

     public int IndexOf(DeviceIODetail value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, DeviceIODetail value)
     {
         List.Insert(index, value);
      }

     public void Remove(DeviceIODetail value)
    {
         List.Remove(value);
     }

    public bool Contains(DeviceIODetail value)
     {
       return (List.Contains(value));
     }

     public DeviceIODetail GetFirstOne()
     {
         return this[0];
     }

    }
  }

