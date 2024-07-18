using System.Collections;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
     //Start is called before the first frame update
    Rigidbody rb;
    float force = 1000f;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
       rb.AddForce(rb.transform.forward* force);
        StartCoroutine(timeDeath());
    }
    IEnumerator timeDeath()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

     //Update is called once per frame

    private void OnCollisionEnter(Collision collision)
    {
        if (this.name == "PlayerBullet(Clone)")
        {
            if(!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("MainCamera") )
            { 
                Destroy(this.gameObject); 
            }
        }


        if (this.name == "EnemyBullet(Clone)")
        {
            if (!collision.gameObject.CompareTag("Enemy") && !collision.gameObject.CompareTag("Enemy2"))
            {
                
                Destroy(this.gameObject);
            }
        }

    }
}
