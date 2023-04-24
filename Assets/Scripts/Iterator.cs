using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Iterator<T>  where T : class
{
    //protected static GameObject objIterator;
    //[SerializeField] protected Aggregate<T> myCollection;
    protected int stepCount ;

    //protected static GameObject IteratorConcObj
    //{
    //    get
    //    {
    //        if (objIterator == null)
    //        {
    //            objIterator = new GameObject("Iterator with Aggregate");
    //        }
    //        return objIterator;
    //    }
    //}


    //protected void SetCollection(ConcreteAggregate<T> myCollection)
    //{
    //    this.myCollection = myCollection;
    //}

    //public static Iterator<T> MakeNewIteratorForThisAggregate(ConcreteAggregate<T> collection, int step = 1)
    //{
        //var thisIterator = IteratorConcObj.AddComponent<ConcreteIterator<T>>();
        //thisIterator.GetComponent<ConcreteIterator<T>>().SetCollection(collection);
        //thisIterator.GetComponent<ConcreteIterator<T>>().stepCount = step;
    //    return thisIterator;
    //}


    public abstract bool IsDone();
    public abstract T CurrentItem { get; }
    public abstract T Next();
    public abstract T First();
    public abstract T Prev();

}
