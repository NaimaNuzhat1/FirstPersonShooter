using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class healthPowerUp : MonoBehaviour
{
    [SerializeField] Canvas powerUpWindow;
    [SerializeField] TMP_Text Text;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
        Debug.Log("PowerUP!");
            StartCoroutine(setWindow());
            PlayerControls.instance.slider.value = 10;

        }

    }

    IEnumerator setWindow()
    {

        powerUpWindow.gameObject.SetActive(true);
        Text.text = "Health Restored!";
        yield return new WaitForSeconds(1f);
        powerUpWindow.gameObject.SetActive(false);
        Destroy(gameObject);

    }
}
