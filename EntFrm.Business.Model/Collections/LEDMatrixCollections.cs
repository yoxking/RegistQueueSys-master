using System.Collections;

namespace EntFrm.Business.Model.Collections
{

  public class LEDMatrixCollections:CollectionBase
  {

      public LEDMatrix this[int index]
      {
         get { return (LEDMatrix)List[index]; }
         set { List[index] = value; }
      }

      public int Add(LEDMatrix value)
      {
          return (List.Add(value));
     }

     public int IndexOf(LEDMatrix value)
     {
         return (List.IndexOf(value));
     }

      public void Insert(int index, LEDMatrix value)
     {
         List.Insert(index, value);
      }

     public void Remove(LEDMatrix value)
    {
         List.Remove(value);
     }

    public bool Contains(LEDMatrix value)
     {
       return (List.Contains(value));
     }

     public LEDMatrix GetFirstOne()
     {
         return this[0];
     }

    }
  }

