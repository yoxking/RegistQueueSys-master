using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class ServiceInfoCollections:CollectionBase
  {

      public ServiceInfo this[int index]
      {
         get { return (ServiceInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(ServiceInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(ServiceInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, ServiceInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(ServiceInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(ServiceInfo value)
     {
       return (List.Contains(value));
     }

     public ServiceInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

