using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class LessonTimesInfoCollections:CollectionBase
  {

      public LessonTimesInfo this[int index]
      {
         get { return (LessonTimesInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(LessonTimesInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(LessonTimesInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, LessonTimesInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(LessonTimesInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(LessonTimesInfo value)
     {
       return (List.Contains(value));
     }

     public LessonTimesInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

