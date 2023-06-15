using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class GameDependent //: MonoBehaviour
{

    [Serializable]
    public class DeleteRequest 
    {
        public GameRunner.context context;
        public int index;

        public DeleteRequest(GameRunner.context context, int index)
        {
            this.context = context;
            this.index = index;
        }
    }


    [SerializeField]private BaseFighter[] selectedDBZ = new BaseFighter[2], selectedNaruto = new BaseFighter[2];
    private Image[] dbzImg, naruImg; // = new Image[2];
    [SerializeField] private UserFighterSelection userFighterSelection; 

    public GameDependent(Image[] dbz, Image[] naru ) {
        dbzImg = dbz;
        naruImg = naru;
        userFighterSelection = new UserFighterSelection();
        UpdateUIMethod();
    }

    public void AddFighterToContextList(GameRunner.context context, BaseFighter fighter)
    {
        if (ExistsInArray(context, fighter))
        {
            return;
        }

        if (context == GameRunner.context.DBZ)
        {
            if (selectedDBZ[0] == null)
            {
                selectedDBZ[0] = fighter;
            }
            else
            {
                selectedDBZ[1] = fighter;
            }
        }
        else
        {
            if (selectedNaruto[0] == null)
            {
                selectedNaruto[0] = fighter;
            }
            else
            {
                selectedNaruto[1] = fighter;
            }
        }
        UpdateUIMethod();
        UpdateUserSelection(context);
    }

    private bool ExistsInArray(GameRunner.context context, BaseFighter fighter)
    {
        if (context == GameRunner.context.DBZ)
        {
            foreach (BaseFighter f in selectedDBZ)
                if (f == fighter)
                    return true;
        }
        else
        {
            foreach (BaseFighter f in selectedNaruto)
                if (f == fighter)
                    return true;
        }
        return false;
    }

    public void DeleteSelectedFighter(DeleteRequest req) 
    {
        //delete either o or 1. if it is 0, remove and replace with elem in 1. 
        int i = req.index;
        if(i <=1 && i >= 0)//[0,1]
        {
            GameRunner.context context = req.context;
            if (i == 0)
            {
                if (context == GameRunner.context.DBZ)
                {
                    selectedDBZ[0] = selectedDBZ[1];
                    selectedDBZ[1] = null;
                }
                else //if (context == GameRunner.context.Naru) //limit to 2 only
                {
                    selectedNaruto[0] = selectedNaruto[1];
                    selectedNaruto[1] = null;
                }

            }
            else
            {
                if (context == GameRunner.context.DBZ)
                {
                    selectedDBZ[i] = null;
                }
                else
                {
                    selectedNaruto[1] = null;
                }
            }
            UpdateUIMethod();
            UpdateUserSelection(context);
        }
        else
        {
            throw new Exception("OUT OF INDEX"); 
        }
    }

    //SO is null, the Go is off
    public void UpdateUIMethod()
    {
        for (int i = 0; i < selectedNaruto.Length; i++)
        {
            if (selectedNaruto[i] == null) {
                naruImg[i].gameObject.SetActive(false);
            }
            else
            {
                naruImg[i].gameObject.SetActive(true);
                naruImg[i].sprite = selectedNaruto[i].avatar; 
            }
        }

        for(int i = 0;i < selectedDBZ.Length; i++)
        {
            if (selectedDBZ[i] == null)
            {
                dbzImg[i].gameObject.SetActive(false);
            }
            else
            {
                dbzImg[i].gameObject.SetActive(true);
                dbzImg[i].sprite = selectedDBZ[i].avatar;
            }
        }
    }

    public bool EverythingFilled()
    {
        for(int i = 0;i < 2; i++)
        {
            if (selectedDBZ[i] == null)
                return false;
            if (selectedNaruto[i]==null)
                return false;
        }

        return true;
    }

    private void UpdateUserSelection(GameRunner.context con)
    {
        switch (con)
        {
            case GameRunner.context.Naru:
                userFighterSelection.narutoSelected = selectedNaruto;
                break;
            case GameRunner.context.DBZ:
                userFighterSelection.dbzSelected = selectedDBZ;
                break;
            default:
                break;
        }
    }
    public UserFighterSelection GetUserSelection()
    {
        return userFighterSelection;
    }
}


