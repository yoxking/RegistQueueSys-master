using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class SysParamsCollections:CollectionBase
  {

      public SysParams this[int index]
      {
         get { return (SysParams)List[index]; }
         set { List[index] = value; }
      }

      public int Add(SysParams value)
      {
          return (List.Add(value));
     }

     public int IndexOf(SysParams value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, SysParams value)
     {
         List.Insert(index, value);
      }

     public void Remove(SysParams value)
    {
         List.Remove(value);
     }

    public bool Contains(SysParams value)
     {
       return (List.Contains(value));
     }

     public SysParams GetFirstOne()
     {
         return this[0];
     }

    }
  }

