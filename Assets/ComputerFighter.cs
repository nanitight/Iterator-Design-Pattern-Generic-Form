using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public class ComputerFighter : MonoBehaviour
{
    [SerializeField, RequiredMember] private Transform closestOppAttackPosition;
    [SerializeField, RequiredMember] private List<Transform> allFighterOppsBestAttackPosition;
    private Rigidbody rb; 
    private float minDistance = 0.2f, timer = 0f, levelMovement = 0.01f;
    private int delayAmount = 1, levelMax = 10; 
    private bool collisionWithTarget = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (enabled)
        {
            if (closestOppAttackPosition != null )
            {
                //start attack procedure 
                AttackTheClosestOpp(closestOppAttackPosition);
                timer += Time.deltaTime;
                if (timer > delayAmount)
                {
                    timer = 0f;
                }
            }
            else
            {
                closestOppAttackPosition = FindClosestOpp();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
           if(collision.collider.gameObject.TryGetComponent<FighterPlayer>(out FighterPlayer player))
            {
                collisionWithTarget = true;
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision != null)
        {
            if (collision.collider.gameObject.TryGetComponent<FighterPlayer>(out FighterPlayer player))
            {
                collisionWithTarget = false;
            }
        }
    }

    private bool AttackTheClosestOpp(Transform target)
    {
        //Debug.Log(name+ "moving towarsd "+target.name);
        transform.LookAt(target); 
        //rb.MoveRotation(Quaternion.Euler(target.position));
        Vector3 targetVector = transform.position  + levelMovement * PlayerMovement.movementSpeed * Time.deltaTime * transform.forward;
        transform.position = Vector3.MoveTowards(transform.position,targetVector,minDistance);

        //StartCoroutine(ChargeTowardsEnemy());
        return false;
    }

    private IEnumerator ChargeTowardsEnemy()
    {
        yield return new WaitUntil(() => collisionWithTarget == true);

    }

    private Transform FindClosestOpp()
    {
        Transform closest = allFighterOppsBestAttackPosition[0]; 
        foreach (Transform t in allFighterOppsBestAttackPosition)
        {
            if (t != null && t != closest)
            {
                if (closest == null)
                {
                    closest = t;
                }
                else if (Vector3.Distance(t.position, closest.position) < minDistance)
                {
                    closest = t;
                }
            }
        }
        return closest;
    }
        
    public void SetOppsBestAttackPostions(List<Transform> opponents)
    {
        allFighterOppsBestAttackPosition = opponents;
        closestOppAttackPosition = FindClosestOpp();

    }

    public void RunAwayToTheCenter(Transform destination)
    {
            AttackTheClosestOpp(destination);
        //while(Vector3.Distance(transform.position,destination.position) > minDistance) { 
        //} //hang
        //recalculate target , 
        //closestOppAttackPosition = FindClosestOpp();
    }


}
