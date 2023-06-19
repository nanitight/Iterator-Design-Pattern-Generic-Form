using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class FighterPlayer : MonoBehaviour
{
    [SerializeField,RequiredMember] private FightManager fightManager;
    [SerializeField,RequiredMember] private BaseFighter fighterInfo;
    [SerializeField, RequiredMember] private Slider healthBar; 
    [SerializeField] private float health, percentageOfAttack = 0.1f;
    [SerializeField, RequiredMember] private GameObject explosionPrefab;
    [SerializeField, RequiredMember] private List<FighterPlayer> allFightersOpp;
    [SerializeField, RequiredMember] private List<Transform> allFighterOppsBestAttackPosition;



    private void Start()
    {
        if (fighterInfo != null)
        {
            SetUpUIElements();
            health = fighterInfo.healthPoints;
            gameObject.name = fighterInfo.fighterName;
            UpdateHealthBar();
        }
        //explosionPrefab.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            Debug.Log(name + " is dead");
            Instantiate(explosionPrefab, this.transform);
            ToggleObjectMovementComponent(false);
            enabled = false;
            Destroy(this.gameObject, 1.5f);
        }

        if (fightManager != null)
        {
            if (fightManager.fightStarted)
            {
                ToggleObjectMovementComponent(true);
            }
            else
            {
                ToggleObjectMovementComponent(false);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("I am " + name + " collision detected.");
        //Debug.Log("collider as of "+name+" "+collision.collider.tag);

        if (collision.collider != null)
        {
            if (collision.collider.CompareTag("Weapon"))
            {
                // I am taking damage from the weapon
                FighterPlayer opp = collision.collider.gameObject.GetComponentInParent<FighterPlayer>();
                if (opp != null)
                {
                    //Debug.Log("YAAA");
                    if (opp.enabled)
                    {
                        TakeDamage(opp.GetPowerOfAttack());
                        //run away, 
                        if (TryGetComponent<ComputerFighter>(out ComputerFighter comp)){
                            comp.RunAwayToTheCenter(fightManager.ringCentre.transform); 
                        }
                    }
                }
                else
                {
                    //not the right collision or structure of object
                }
            }
            //else if (collision.collider.CompareTag("Body"))
        }
    }

    private void OnDestroy()
    {
        Camera camera = GetComponentInChildren<Camera>();
        if (camera != null)
        {
            camera.transform.SetParent(fightManager.loserPOV, false);
        }
    }

    public void SetAllFighterOpponents(List<FighterPlayer> allFighters)
    {
        int myself = allFighters.FindIndex((x) => x == this);
        if (myself == -1)
        {
            Debug.LogError("You are not found!"); 
        }
        else
        {
            for (int i = 0; i < allFighters.Count; i++)
            {
                if (i != myself)
                {
                    allFightersOpp.Add(allFighters[i]);
                }
            }
            SetUpBestAttackPositions();
            Debug.Log("Self removed!");
        }
    }
    
    public void SetFighterInfo(BaseFighter fighterInfo)
    {
        if (this.fighterInfo == null)
        {
            this.fighterInfo = fighterInfo;
        }
        SetUpUIElements();
        health = fighterInfo.healthPoints;
        gameObject.name = fighterInfo.fighterName;
        UpdateHealthBar();
    }

    public int GetPowerOfAttack()
    {
        return fighterInfo.GetPowerValue(); 
    }

    public void AttackOpp(FighterPlayer opp)
    {
        opp.TakeDamage(fighterInfo.GetPowerValue());
    }

    public void SetFightManager(FightManager manager)
    {
        fightManager = manager;
    }

    private void SetUpUIElements()
    {
        GetComponentInChildren<Image>().sprite = fighterInfo.avatar;
    }

    private void UpdateHealthBar()
    {
        healthBar.value = (health / fighterInfo.healthPoints) * healthBar.maxValue;
    }

    private void TakeDamage(int punch)
    {
        if (health > 0)
        {
            health -= (float) ( punch * percentageOfAttack);
            UpdateHealthBar();
        }
        
    }

    private void ToggleObjectMovementComponent(bool onOff)
    {
        if (TryGetComponent<ComputerFighter>(out ComputerFighter computerFighter))
        {
            computerFighter.enabled = onOff;
        }
        else if (TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            playerMovement.enabled = onOff;
        }
    }

    private void SetUpBestAttackPositions()
    {
        foreach(FighterPlayer fighter in allFightersOpp)
        {
            COMFighter com = fighter.gameObject.GetComponentInChildren<COMFighter>();
            if (com != null)
            {
                allFighterOppsBestAttackPosition.Add(com.gameObject.transform);
                //error handling?
            }
        }
        // attach computerfighter to own script if not player
        if (!gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            gameObject.AddComponent<ComputerFighter>();
            ComputerFighter computer =  gameObject.GetComponent<ComputerFighter>();
            computer.enabled = false;
            computer.SetOppsBestAttackPostions(allFighterOppsBestAttackPosition);
        }
    }


}
