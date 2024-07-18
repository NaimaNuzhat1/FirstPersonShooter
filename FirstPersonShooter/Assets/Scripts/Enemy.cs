using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public float lookRadius=5f;
    [SerializeField] float shootSpeed;
    Transform target;
    NavMeshAgent agent;

    [SerializeField]
    Transform[] points;
    [SerializeField] GameObject bullet;
    int targetPoint=0;
    Animator animation;
    bool isSpawning = false;
    [SerializeField] GameObject gun;
    [SerializeField] Slider slider;
    // Start is called before the first frame update
    void Start()
    {

        animation = gameObject.GetComponent<Animator>();
        target = PlayerControls.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();



        GotoNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

       // agent.Warp(transform.position);
        if(distance<=lookRadius)
        {
            animation.SetBool("isWalking", false);
            agent.isStopped = true;
            transform.LookAt(target);
            agent.SetDestination(target.transform.position);
            if(!isSpawning)
            {
                StartCoroutine(shoot());
            }
        }
        else
        {
            if(agent.isStopped)
            {
                agent.isStopped = false;
            }
            else if(agent.destination==target.transform.position)
            {
                GotoNextPoint();
            }
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }
    
    }

    IEnumerator shoot()
    {

        isSpawning = true;
        //Vector3 position = transform.position;
        animation.SetBool("isShooting", true);


        Debug.Log("HERE");
        //muzzleFire.SetActive(true);
        yield return new WaitForSeconds(shootSpeed);
        //muzzleFire.SetActive(false);
        isSpawning = false;
        Instantiate(bullet, gun.transform.position, transform.rotation);
        animation.SetBool("isShooting", false);
    }
    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;
        animation.SetBool("isWalking", true);

        // Set the agent to go to the currently selected destination.
        agent.destination = points[targetPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        targetPoint = (targetPoint + 1) % points.Length;
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name=="PlayerBullet(Clone)")
        {
            slider.value -= 10;

            if (slider.value <= 0)
            {
                StartCoroutine(die());
            }
        }


    }



    IEnumerator die()
    {
        
        animation.SetBool("isDead", true);
        yield return new WaitForSeconds(0.8f);
        animation.SetBool("isDead", false);
        Destroy(gameObject);
        Debug.Log("HERe");
    }
}
