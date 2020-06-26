using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class LabLessonInfoCollections:CollectionBase
  {

      public LabLessonInfo this[int index]
      {
         get { return (LabLessonInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(LabLessonInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(LabLessonInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, LabLessonInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(LabLessonInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(LabLessonInfo value)
     {
       return (List.Contains(value));
     }

     public LabLessonInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

