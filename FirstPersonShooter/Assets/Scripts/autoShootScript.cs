using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class autoShootScript : MonoBehaviour
{
    [SerializeField] GameObject bullets;
    [SerializeField] Canvas powerUpWindow;
    [SerializeField] TMP_Text Text;
    // Start is called before the first frame update

    private void Start()
    {
        PlayerPrefs.SetString("AutoShoot", "false");
    }
    private void OnTriggerEnter(Collider other)
    {
        
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        bullets.gameObject.SetActive(false);
        StartCoroutine(setWindow());
      
    }
    IEnumerator setWindow()
    {

        powerUpWindow.gameObject.SetActive(true);
        Text.text = "Auto Shoot for 20 Seconds!";
        yield return new WaitForSeconds(1f);
        powerUpWindow.gameObject.SetActive(false);
        StartCoroutine(setAutoShoot());

    }
    IEnumerator setAutoShoot()
    {

        PlayerPrefs.SetString("AutoShoot", "true");
        yield return new WaitForSeconds(20f);
        PlayerPrefs.SetString("autoShoot", "false");
        Destroy(gameObject);
        
    }
}
