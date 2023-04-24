using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Aggregate<T>  where T : class
{
    abstract public Iterator<T> CreateIterator();

    abstract public int Count();

    abstract public T this[int index] { get; set; } 

}
