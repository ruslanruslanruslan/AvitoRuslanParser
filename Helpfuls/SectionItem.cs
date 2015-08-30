using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AvitoRuslanParser.Helpfuls
{
  public struct SectionItem
  {
    public enum Site { Avito, Ebay, UnTyped };
    public string Link { get; set; }
    public Site site { get; set; }
    public string CategoryName { get; set; }
  }

  class Helpful
  {
    public static IEnumerable<IEnumerable<T>> Partition<T>(IEnumerable<T> source, int size)
    {
      T[] array = null;
      int count = 0;
      foreach (T item in source)
      {
        if (array == null)
        {
          array = new T[size];
        }
        array[count] = item;
        count++;
        if (count == size)
        {
          yield return new ReadOnlyCollection<T>(array);
          array = null;
          count = 0;
        }
      }
      if (array != null)
      {
        Array.Resize(ref array, count);
        yield return new ReadOnlyCollection<T>(array);
      }
    }
  }
}
