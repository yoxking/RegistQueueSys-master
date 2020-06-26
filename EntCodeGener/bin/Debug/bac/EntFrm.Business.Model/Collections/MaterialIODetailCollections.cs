using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class MaterialIODetailCollections:CollectionBase
  {

      public MaterialIODetail this[int index]
      {
         get { return (MaterialIODetail)List[index]; }
         set { List[index] = value; }
      }

      public int Add(MaterialIODetail value)
      {
          return (List.Add(value));
     }

     public int IndexOf(MaterialIODetail value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, MaterialIODetail value)
     {
         List.Insert(index, value);
      }

     public void Remove(MaterialIODetail value)
    {
         List.Remove(value);
     }

    public bool Contains(MaterialIODetail value)
     {
       return (List.Contains(value));
     }

     public MaterialIODetail GetFirstOne()
     {
         return this[0];
     }

    }
  }

