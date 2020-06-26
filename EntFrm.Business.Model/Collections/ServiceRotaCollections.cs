using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class ServiceRotaCollections:CollectionBase
  {

      public ServiceRota this[int index]
      {
         get { return (ServiceRota)List[index]; }
         set { List[index] = value; }
      }

      public int Add(ServiceRota value)
      {
          return (List.Add(value));
     }

     public int IndexOf(ServiceRota value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, ServiceRota value)
     {
         List.Insert(index, value);
      }

     public void Remove(ServiceRota value)
    {
         List.Remove(value);
     }

    public bool Contains(ServiceRota value)
     {
       return (List.Contains(value));
     }

     public ServiceRota GetFirstOne()
     {
         return this[0];
     }

    }
  }

