using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class RankTypeCollections:CollectionBase
  {

      public RankType this[int index]
      {
         get { return (RankType)List[index]; }
         set { List[index] = value; }
      }

      public int Add(RankType value)
      {
          return (List.Add(value));
     }

     public int IndexOf(RankType value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, RankType value)
     {
         List.Insert(index, value);
      }

     public void Remove(RankType value)
    {
         List.Remove(value);
     }

    public bool Contains(RankType value)
     {
       return (List.Contains(value));
     }

     public RankType GetFirstOne()
     {
         return this[0];
     }

    }
  }

