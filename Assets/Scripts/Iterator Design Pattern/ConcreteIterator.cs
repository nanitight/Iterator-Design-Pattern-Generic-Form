using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConcreteIterator<T> : Iterator<T> where T : class
{
    int current = 0;
    [SerializeField] private Aggregate<T> myCollection;

    public ConcreteIterator(ConcreteAggregate<T> coll){
        myCollection= coll;
    }

    public override T CurrentItem
    {
        get{
            return myCollection[current];
        }
    }

    public override T First()
    {
        current = 0;
        return myCollection.Count() > 0 ? myCollection[current] : null ;
    }

    public override bool IsDone()
    {
        return current >= myCollection.Count();
    }

    public override T Next()
    {
        current++;
        if (current >= myCollection.Count())
        {
            //current--;
            return null;
        }
        return myCollection[current];
    }

    public override T Prev()
    {
        current--;
        if (current < 0)
        {
            //current = 0 ;
            return null;
        }
        return  myCollection[current];
    }
}
