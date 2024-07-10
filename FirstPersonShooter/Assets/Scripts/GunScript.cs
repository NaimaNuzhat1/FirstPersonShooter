using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class GunScript : MonoBehaviour
{
    Camera camera;
    float damage=10f;
    float range=10f;
    [SerializeField]
    Image crosshair; 
    [SerializeField] GameObject bullet;
    bool isSpawning = false;
    string target;
    [SerializeField] GameObject muzzleFire;

    //[SerializeField] GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        //Player= GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        RaycastHit hit;
        if(Physics.Raycast(camera.transform.position,camera.transform.forward,out hit,range))
        {
            target = hit.transform.tag;
            if (target == "Enemy")
            { 
                crosshair.color = Color.red;
                if (!isSpawning)
                {
                    StartCoroutine(shoot());
                }
            }
            else
            {
                crosshair.color = Color.black;
            }

        }


    }

    IEnumerator shoot()
    {
        isSpawning = true;
        Vector3 position = transform.position;

        Instantiate(bullet, position, camera.transform.rotation);
        
        
        muzzleFire.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        muzzleFire.SetActive(false);
        isSpawning = false;
    }
}
