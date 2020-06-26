using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class UploadFileInfoCollections:CollectionBase
  {

      public UploadFileInfo this[int index]
      {
         get { return (UploadFileInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(UploadFileInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(UploadFileInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, UploadFileInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(UploadFileInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(UploadFileInfo value)
     {
       return (List.Contains(value));
     }

     public UploadFileInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

