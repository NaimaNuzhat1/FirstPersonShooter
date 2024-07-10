using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    float force = 1000f;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        //rb.AddForce(transform.forward * force);
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        //if(!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("MainCamera") && !collision.gameObject.CompareTag("Enemy"))
        //{ 
        //    //Destroy(this.gameObject); 
        //}

    }
}
