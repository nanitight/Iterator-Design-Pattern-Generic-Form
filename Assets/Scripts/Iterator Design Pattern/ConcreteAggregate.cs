using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConcreteAggregate<T> : Aggregate<T> where T : class
{
    [SerializeField]private List<T> dBZFighters= new();
    private ConcreteIterator<T> iterator;
    public override Iterator<T> CreateIterator()
    {
        iterator ??= new ConcreteIterator<T>(this); // if iterator is equal to null, then assign
        return iterator;
        //return Iterator<T>.MakeNewIteratorForThisAggregate(this);
    }

    public override int Count()
    {
        return dBZFighters.Count;
    }

    public override T this[int index]
    {
        get
        {
            int size = dBZFighters.Count;
            if (size > 0)
            {
                if (index >= size) //wanted to go over, so we give the last
                {
                    return  dBZFighters[size-1] ;
                }
                else if(index < 0)  //wanted to go under so we give the first
                {
                    return dBZFighters[0];
                }
                else
                {
                    return dBZFighters[index];
                }
            }
            return null;
        }
        set
        {
            dBZFighters.Add(value);
        }
    }

    public override string ToString()
    {
        string text = ""; 
        foreach(var t in dBZFighters)
        {
            text += t.ToString() + " ";
        }
        text += " count: " + dBZFighters.Count;
        return text;
    }
}
