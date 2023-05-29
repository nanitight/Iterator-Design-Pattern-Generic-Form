using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameRunner : MonoBehaviour
{
    private Aggregate<DBZFighter> dbzFighters;
    private Iterator<DBZFighter> dbzIterator;

    private Aggregate<NarutoFighter> narutoFighters;
    private Iterator<NarutoFighter> narutoIterator;

    public GameObject naruLogo, dbzLogo, defaultLogo;
    public Image avatarImg;
    public TextMesh words, fighterName;
    

    public enum context
    {
        DBZ , Naru
    };
    public context currentContext = context.DBZ;
    public Object currentFighter = null;
    
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("STARTED!");

        dbzFighters = new ConcreteAggregate<DBZFighter>();
        if (dbzFighters != null)
        {
            foreach (DBZFighter f in Resources.LoadAll<DBZFighter>("DBZ"))
            {
                dbzFighters[-1] = f;
            }
            //Debug.Log("This is safe "+dbzFighters.ToString());
            dbzIterator = dbzFighters.CreateIterator();
            dbzIterator.First();

            //for (DBZFighter fighter = dbzIterator.First(); dbzIterator.IsDone() == false && fighter != null; fighter = dbzIterator.Next())
            //{
            //    Debug.Log(fighter.ToString());
            //}
        }
        else
        {
            Debug.Log("Nothing is working out this route");
        }

        narutoFighters = new ConcreteAggregate<NarutoFighter>();
        if (narutoFighters != null)
        {
            foreach (NarutoFighter f in Resources.LoadAll<NarutoFighter>("Naruto"))
            {
                narutoFighters[-1] = f;
            }
            narutoIterator = narutoFighters.CreateIterator();
            narutoIterator.First();
            //for (NarutoFighter fighter = narutoIterator.First(); narutoIterator.IsDone() == false && fighter != null; fighter = narutoIterator.Next())
            //{
            //    Debug.Log(fighter.ToString());
            //}
        }

        ChangeContext(-1); // choose random context
        InitializeFighter();
    }

    public void ChangeContext(int choosen)
    {
        if (choosen == 1)
        {
            currentContext = context.DBZ;
        }
        else if (choosen == 2)
        {
            currentContext = context.Naru;
        }
        else
        {
            //randomly choose context
            int choose1 = Random.Range(1, 3);
            ChangeContext(choose1);
        }

        GetCurrentContextsItem();
        ToggleLogosArcodingToContext();
    }

    public void NextOnTheList()
    {
        SwitchOffImage();
        switch(currentContext)
        {
            case context.Naru:
                currentFighter = narutoIterator.Next();
                break;
            case context.DBZ:
                currentFighter = dbzIterator.Next();
                break;
            default: break;
        }
        
        //went over the top, so we take it one stepback
        if(currentFighter == null)
        {
            PrevOnTheList();
        }
    }

    public void PrevOnTheList()
    {
        SwitchOffImage();
        switch (currentContext)
        {
            case context.Naru:
                currentFighter = narutoIterator.Prev();
                break;
            case context.DBZ:
                currentFighter = dbzIterator.Prev();
                break;
            default: break;
        }
        if (currentFighter == null)
        {
            NextOnTheList();
        }
    }

    private void InitializeFighter()
    {
        switch (currentContext)
        {
            case context.Naru:
                currentFighter = narutoIterator.First();
                break;
            case context.DBZ:
                currentFighter = dbzIterator.First();
                break;
            default: break;
        }
    }

    private void ToggleLogosArcodingToContext()
    {
        switch (currentContext)
        {
            case context.Naru:
                defaultLogo.SetActive(false);
                dbzLogo.SetActive(false);
                naruLogo.SetActive(true);
                break;
            case context.DBZ:
                defaultLogo.SetActive(false);
                dbzLogo.SetActive(true);
                naruLogo.SetActive(false);
                break;
            default:
                defaultLogo.SetActive(true);
                dbzLogo.SetActive(false);
                naruLogo.SetActive(false);
                break;
        }
    }

    private void GetCurrentContextsItem()
    {
        switch (currentContext)
        {
            case context.Naru:
                currentFighter = narutoIterator.CurrentItem;
                break;
            case context.DBZ:
                currentFighter= dbzIterator.CurrentItem;
                break;
            default: break;
        }
    }

    private void ChangeImageMade(Sprite sprite)
    {
        if (sprite == null) return;
        avatarImg.sprite = sprite;
        avatarImg.enabled = true;
    }

    private void SwitchOffImage()
    {
        avatarImg.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentFighter!= null)
        {
            if (currentContext==context.Naru) {
                NarutoFighter curr  = currentFighter as NarutoFighter;
                fighterName.text = curr.fighterName;
            }
            else if (currentContext==context.DBZ)
            {
                DBZFighter curr = currentFighter as DBZFighter; 
                fighterName.text = curr.fighterName;
            }
            else
            {
                fighterName.text = "Fighter Name";
            }
            words.text = currentFighter.ToString();

            BaseFighter fighter = currentFighter as BaseFighter; 
            if (fighter.avatar != null)
            {
                ChangeImageMade(fighter.avatar);
            }
            else
            {
                SwitchOffImage();
            }
        }
    }
}
