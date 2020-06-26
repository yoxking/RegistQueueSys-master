using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class LEDDisplayCollections:CollectionBase
  {

      public LEDDisplay this[int index]
      {
         get { return (LEDDisplay)List[index]; }
         set { List[index] = value; }
      }

      public int Add(LEDDisplay value)
      {
          return (List.Add(value));
     }

     public int IndexOf(LEDDisplay value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, LEDDisplay value)
     {
         List.Insert(index, value);
      }

     public void Remove(LEDDisplay value)
    {
         List.Remove(value);
     }

    public bool Contains(LEDDisplay value)
     {
       return (List.Contains(value));
     }

     public LEDDisplay GetFirstOne()
     {
         return this[0];
     }

    }
  }

