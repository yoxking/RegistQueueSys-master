using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class WareHouseCollections:CollectionBase
  {

      public WareHouse this[int index]
      {
         get { return (WareHouse)List[index]; }
         set { List[index] = value; }
      }

      public int Add(WareHouse value)
      {
          return (List.Add(value));
     }

     public int IndexOf(WareHouse value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, WareHouse value)
     {
         List.Insert(index, value);
      }

     public void Remove(WareHouse value)
    {
         List.Remove(value);
     }

    public bool Contains(WareHouse value)
     {
       return (List.Contains(value));
     }

     public WareHouse GetFirstOne()
     {
         return this[0];
     }

    }
  }

