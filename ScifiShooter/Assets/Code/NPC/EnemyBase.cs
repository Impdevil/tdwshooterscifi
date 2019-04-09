using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{

    public bool GS_TargetFound
    {
        get { return targetFound; }
        set { targetFound = value; }
    }
    GameManager GM;

    public int health;
    public float Attacktimer;
    public int basicDamage;
    
    float btwnTmr;
    NavMeshAgent agent;
    bool targetFound;
    bool barked;

    AudioClip alertSound, attackSound, damageSound;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.Locked)
        {
            FindTarget();
            Bark();
        }
    }

    /// <summary>
    /// tells local enemies that this enemy has found the player.
    /// </summary>
    private void Bark()
    {
        if(targetFound && !barked)
        {
            for (int i = 0; i < GM.enemies.Count; i ++)
            {
                if(GM.enemies[i] != null && Vector3.Distance(this.transform.position,GM.enemies[i].transform.position) < 5f)
                {
                    GM.enemies[i].GetComponent<EnemyBase>().GS_TargetFound = true;
                }
            }
            barked = true;
        }
    }

    private void FindTarget()
    {
        if (!targetFound)
        {
            Ray visionRay = new Ray(new Vector3(transform.position.x, 1, transform.position.z), Vector3.Normalize(GM.player.transform.position - transform.position));
            RaycastHit rayVisionHit;
            if (Physics.Raycast(visionRay, out rayVisionHit, 15))
            {
                Debug.Log(rayVisionHit.transform.tag + " object between player and " + transform.name);
                if (rayVisionHit.transform.tag != "ENV")
                {

                    visionRay = new Ray(new Vector3(transform.position.x, 1, transform.position.z), transform.forward);

                    if (Physics.Raycast(visionRay, out rayVisionHit, 15))
                    {
                        if (rayVisionHit.transform.tag == "Player")
                        {
                            targetFound = true;
                        }
                    }
                    if (Vector3.Distance(GM.player.transform.position, transform.position) <= 10)
                    {
                        Vector3 direction = Vector3.Normalize(GM.player.transform.position - transform.position);
                        float dot = Vector3.Dot(direction, transform.forward);
                        Debug.Log(this.name + " dot value: " + dot + " | | " + (dot * 180 / Mathf.PI));
                        if ((dot * 180 / Mathf.PI) > 20)
                        {
                            targetFound = true;
                        }
                    }
                }
                else if (Vector3.Distance(GM.player.transform.position, this.transform.position) < 4)
                {
                    targetFound = true;
                }
            }
        }
        else if (targetFound)
        {
            transform.LookAt(new Vector3(GM.player.transform.position.x, this.transform.position.y, GM.player.transform.position.z), transform.up);
            if (Vector3.Distance(GM.player.transform.position, this.transform.position) < 1.5)
            {
                AttackTarget(GM.player);
                agent.SetDestination(transform.position);

            }
            else
            {
                agent.destination = GM.player.transform.position;
                btwnTmr = 0;
            }
        }
    }

    /// <summary>
    /// used to damage a enemy
    /// </summary>
    /// <param name="damage"></param>
    public void DamageSelf(int damage)
    {
        targetFound = true;
        health -= damage;
        if (health <= 0)
        {
            Debug.Log(this.name + " is destroyed");
            Destroy(this.gameObject);
        }
    }

    public void AttackTarget(GameObject target)
    {
        btwnTmr -= Time.deltaTime;
        if (btwnTmr <= 0)
        {
            Debug.Log("attacking " + target.name);
            target.GetComponent<PlayerController>().DamagePlayer(basicDamage, null);
            btwnTmr = Attacktimer;
        }
    }
}
