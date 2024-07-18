using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class GunScript : MonoBehaviour
{

    Camera camera;
    float damage=10f;
    float range=40f;
    [SerializeField]
    Image crosshair; 
    [SerializeField] GameObject bullet;
    bool isSpawning = false;
    string target;
    [SerializeField] GameObject muzzleFire;
    string mode;
     //Animator Player;
    void Start()
    {
        camera = Camera.main;
        //Player = PlayerControls.instance.GetComponent<Animation>();
        //Player= GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        mode = PlayerPrefs.GetString("AutoShoot","false");
        Debug.Log(mode);
    }

    // Update is called once per frame
    private void LateUpdate()
    {

        //transform.localRotation = Quaternion.Euler(camera.transform.rotation.x, 0f, 0f);
        RaycastHit hit;
        if(Physics.Raycast(camera.transform.position,camera.transform.forward,out hit,range))
        {

            target = hit.transform.tag;
            if (target == "Enemy")
            {

                if (mode=="true")
                {
                    onShootClicked();
                }
                crosshair.color = Color.red;
                
            }
            else
            {
                crosshair.color = Color.black;
            }

        }


    }
    public void onShootClicked()
    {
        if(!isSpawning)
        {
            StartCoroutine(shoot());
        }
    }

    IEnumerator shoot()
    {
        //Player.SetBool("isShooting", true);
        isSpawning = true;
        Vector3 position = transform.position;

        Instantiate(bullet, position, camera.transform.rotation);
        
        
        muzzleFire.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        muzzleFire.SetActive(false);
        isSpawning = false;
        //Player.SetBool("isShooting", false);

    }
}
