using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    Transform[] points;
    

    [SerializeField]
    float speed = 2f;
    [SerializeField] GameObject bullet;
    int targetPoint;
    public float health = 50f;
    public float lookRadius = 5f;
    Animator animation;
    bool isWalking = false;
    bool isSpawning=false;
    string target;


    private void Start()
    {
        animation = gameObject.GetComponent<Animator>();
        targetPoint = 1;
    }
    private void Update()
    {
            transform.position = Vector3.MoveTowards(transform.position, points[targetPoint].position, speed * Time.deltaTime);
       
        if (isWalking == false &&isSpawning==false)
        {
            isWalking = true;

            animation.SetBool("isWalking", true);
        

                float rotate = (transform.rotation.y - 90) % 360;

                transform.Rotate(0f, rotate, 0f);
            StartCoroutine(shoot());   


        }
        else if (transform.position == points[targetPoint].position)
        {
            isWalking = false;
            animation.SetBool("isWalking", false);
            targetPoint = (targetPoint + 1) % points.Length;

        }

    }
    private void LateUpdate()
    {
        
    }
    IEnumerator shoot()
    {

        isSpawning = true;
        Vector3 position = transform.position;

        Instantiate(bullet, position, gameObject.transform.rotation);

        Debug.Log("HERE");
        //muzzleFire.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        //muzzleFire.SetActive(false);
        isSpawning = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= 10;
            if (health <= 0)
            {
                StartCoroutine(die());
            }
        }


    }



    IEnumerator die()
    {
        //animation.SetBool("isDead", true);

        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
        Debug.Log("HERe");
    }

    private void OnDrawGizmos()
    {
       Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
