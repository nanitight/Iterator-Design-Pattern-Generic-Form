using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    public Camera activeCamera;
    public GameObject customFighterPrefab, playerFighterGObj;
    public Transform[] startingPos;
    public Transform ringCentre, loserPOV;
    public TextMeshPro countDownTime, winnerName;
    public List<GameObject> dbz = new(), naru = new(); // the transffered fighters
    private BaseFighter playerFighterSObj;
    private List<FighterPlayer> matchFighters = new(), chosenAlready = new();
    public bool fightStarted = false; 
    private bool startCountDown = false ;
    private int delayAmount = 1; 
    private float timer = 0 ;
    private int downCount = 3;
    [SerializeField, RequiredMember] private List<GameObject> fireworksPrefab;

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
                    fp.SetFightManager(this);
                    fp.SetFighterInfo(StateManager.userFighterSelection.dbzSelected[dbz.Count]);
                    matchFighters.Add(fp);
                    dbz.Add(o);
                
                }
                else //naru
                {
                    var o = Instantiate(customFighterPrefab, startingPos[i].position, Quaternion.Euler(startingPos[i].eulerAngles));// transform.LookAt(ringCentre).eulers;
                    //o.transform .LookAt(ringCentre);
                    FighterPlayer fp = o.GetComponent<FighterPlayer>();
                    fp.SetFightManager(this);
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
            playerFighterGObj.GetComponent<PlayerMovement>().enabled = false;
            chosenAlready.Add(playerFighterGObj.GetComponent<FighterPlayer>());
            //int naruInd = 0, dbzInd = 0;


            foreach (FighterPlayer fp in matchFighters)
            {
                fp.SetAllFighterOpponents(matchFighters);
            }

            //start the countdown and allowEachToPlay
            //StartCoroutine(CountDown()); 
            ToogleTimerUIVisibility(true);
            SetTimerUI();
            startCountDown = true;
        }//end of if
   
        //ensuring what needs to be off is off
        winnerName.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (startCountDown)
        {
            timer += Time.deltaTime; 

            if (timer >= delayAmount) { 
                timer = 0;
                downCount--;
                SetTimerUI();
                if (downCount <= 0)
                {
                    ToogleTimerUIVisibility(false);
                    startCountDown=false;
                    fightStarted = true;
                    downCount = 3;
                }
            }
        }

        if (winnerName != null && winnerName.gameObject.activeSelf)
        {
            StartNameRotation();
        }
    }

    private void SetTimerUI()
    {
        countDownTime.text = downCount.ToString();
    }

    private void ToogleTimerUIVisibility(bool visible)
    {
        countDownTime.gameObject.SetActive(visible);
        if (playerFighterGObj != null)
        {
            countDownTime.transform.LookAt(playerFighterGObj.transform);
        }
    }

    private void StartNameRotation()
    {
        Vector3 rot = winnerName.rectTransform.rotation.eulerAngles;
        rot.z += Time.deltaTime * PlayerMovement.movementSpeed;
        winnerName.rectTransform.rotation = Quaternion.Euler(rot);
    }

    public void LastManStanding(FighterPlayer winner)
    {
        winnerName.text = "WINNER: "+winner.name;
        winnerName.gameObject.SetActive(true);
        //display that this fighter is the winner
        foreach(GameObject obj in fireworksPrefab)
        {
            obj.SetActive(true);
        }

        StartCoroutine(ResetToStartCondition());
    }

    private IEnumerator ResetToStartCondition()
    {
        yield return new WaitForSecondsRealtime(5f);


        StateManager.userFighterSelection = null;
        //StateManager.depedency = betDependency;
        SceneManager.LoadScene("Intro");
    }
    
    

}
