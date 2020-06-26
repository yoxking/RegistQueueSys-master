using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class ExptProjectInfoCollections:CollectionBase
  {

      public ExptProjectInfo this[int index]
      {
         get { return (ExptProjectInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(ExptProjectInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(ExptProjectInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, ExptProjectInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(ExptProjectInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(ExptProjectInfo value)
     {
       return (List.Contains(value));
     }

     public ExptProjectInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

