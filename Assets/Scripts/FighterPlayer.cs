using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class FighterPlayer : MonoBehaviour
{
    [SerializeField,RequiredMember]
    BaseFighter fighterInfo;
    [SerializeField, RequiredMember]
    Slider healthBar; 
    [SerializeField] private float health, percentageOfAttack = 0.1f;
    [SerializeField, RequiredMember] GameObject explosionPrefab;
    [SerializeField, RequiredMember] List<FighterPlayer> allFightersOpp;
    


    private void Start()
    {
        if (fighterInfo != null)
        {
            SetUpUIElements();
            health = fighterInfo.healthPoints;
            UpdateHealthBar();
        }
        //explosionPrefab.SetActive(false);
    }

    public void SetAllFighterOpponents(List<FighterPlayer> allFighters)
    {
        FighterPlayer[] list = new FighterPlayer[allFighters.Count];  
        allFighters.CopyTo(list);
        int myself = allFightersOpp.FindIndex((x) => x == this);
        if (myself == -1)
        {
            Debug.LogError("You are not found!"); 
        }
        else
        {
            allFightersOpp.RemoveAt(myself);
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

    private void FixedUpdate()
    {
        if (health <= 0)
        {
            Debug.Log(name + " is dead");
            Instantiate(explosionPrefab,this.transform);
            Destroy(this.gameObject, 3);
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

                    TakeDamage(opp.GetPowerOfAttack());
                }
                else
                {
                    //not the right collision or structure of object
                }
            }
            //else if (collision.collider.CompareTag("Body"))
        }
    }
}
