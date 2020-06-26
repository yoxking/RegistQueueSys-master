using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class ProviderInfoCollections:CollectionBase
  {

      public ProviderInfo this[int index]
      {
         get { return (ProviderInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(ProviderInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(ProviderInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, ProviderInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(ProviderInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(ProviderInfo value)
     {
       return (List.Contains(value));
     }

     public ProviderInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

