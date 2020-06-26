using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class VoiceInfoCollections:CollectionBase
  {

      public VoiceInfo this[int index]
      {
         get { return (VoiceInfo)List[index]; }
         set { List[index] = value; }
      }

      public int Add(VoiceInfo value)
      {
          return (List.Add(value));
     }

     public int IndexOf(VoiceInfo value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, VoiceInfo value)
     {
         List.Insert(index, value);
      }

     public void Remove(VoiceInfo value)
    {
         List.Remove(value);
     }

    public bool Contains(VoiceInfo value)
     {
       return (List.Contains(value));
     }

     public VoiceInfo GetFirstOne()
     {
         return this[0];
     }

    }
  }

