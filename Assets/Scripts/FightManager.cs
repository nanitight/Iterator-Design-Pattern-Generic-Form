using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    public Camera activeCamera;
    public GameObject customFighterPrefab, playerFighterGObj;
    public Transform[] startingPos;
    public Transform ringCentre;
    public List<GameObject> dbz = new List<GameObject>(), naru = new List<GameObject>(); // the transffered fighters
    private BaseFighter playerFighterSObj;
    private List<FighterPlayer> matchFighters = new List<FighterPlayer>();
    void Start()
    {
        if (StateManager.userFighterSelection != null)
        {
            for (int i = 0; i < startingPos.Length; i++)
            {
                startingPos[i].LookAt(ringCentre);
                if (i % 2 == 0) //dbz
                {
                    var o = Instantiate(customFighterPrefab, startingPos[i].position,Quaternion.Euler(startingPos[i].eulerAngles));
                    //o.transform.LookAt(ringCentre);
                    FighterPlayer fp = o.GetComponent<FighterPlayer>(); 
                    fp.SetFighterInfo(StateManager.userFighterSelection.dbzSelected[dbz.Count]);
                    matchFighters.Add(fp);
                    dbz.Add(o);
                
                }
                else //naru
                {
                    var o = Instantiate(customFighterPrefab, startingPos[i].position, Quaternion.Euler(startingPos[i].eulerAngles));// transform.LookAt(ringCentre).eulers;
                    //o.transform .LookAt(ringCentre);
                    FighterPlayer fp = o.GetComponent<FighterPlayer>(); 
                    fp.SetFighterInfo(StateManager.userFighterSelection.narutoSelected[naru.Count]);
                    matchFighters.Add(fp);
                    naru.Add(o);

                }
            }


            //choose randomly between contexts, and players
            //change camera parent to the spehere object of chosen
            //start fighting
            int randomContext = UnityEngine.Random.Range(0,Enum.GetNames(typeof(GameRunner.context)).Length);
            int randomPlayer = UnityEngine.Random.Range(0,dbz.Count);

            //default will be dbz
            if (randomContext == 0) //chosen dbz
            {
                playerFighterSObj = StateManager.userFighterSelection.dbzSelected[randomPlayer];
                playerFighterGObj = dbz[randomPlayer];
            }
            else if (randomContext == 1) //chosen naruto
            {
                playerFighterSObj = StateManager.userFighterSelection.narutoSelected[randomPlayer];
                playerFighterGObj = naru[randomPlayer]; 
            }

            activeCamera.transform.SetParent(playerFighterGObj.GetComponentInChildren<SphereCollider>().gameObject.transform,false);
            //apply movement to player and opponents
            playerFighterGObj.AddComponent<PlayerMovement>();
            //int naruInd = 0, dbzInd = 0;


            foreach (FighterPlayer fp in matchFighters)
            {
                //fp.SetAllFighterOpponents(matchFighters);
            }
        }//end of if
   

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
