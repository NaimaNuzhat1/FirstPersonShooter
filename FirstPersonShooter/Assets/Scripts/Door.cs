using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public int doorId;
    public Animator doorAnim;
    bool isOpened = false;
    // Start is called before the first frame update
    void Start()
    {
        doorAnim = gameObject.GetComponent<Animator>();   
    }

    private void Update()
    {

        int levelId = PlayerPrefs.GetInt("Level");

        if (levelId==doorId && !isOpened)
        {
            OpenDoor(levelId);
        }
        else
        {

        }
    }

    private void OpenDoor(int levelId)
    {

        doorAnim.SetBool("isOpened", true);
        isOpened = true;
        //GetComponent<BoxCollider>().enabled = false;
    }
}
