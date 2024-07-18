
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int levelNo = 0;

    public Canvas levelUpWind;
    public Canvas gameOverWind;
    public Canvas gameStartWind;
    public Canvas gameEndWind;
    public Camera cam;


    #region Singleton
    public static GameManager instance;
    LevelManager levelManager;
    private void Awake()
    {
        instance = this;
        levelManager = LevelManager.instance;
        PlayerPrefs.SetInt("Level", levelNo);
        levelManager.InstantiateLevel(0);

    }
    #endregion Singleton

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerControls.instance != null)
        {
            int isDead = PlayerPrefs.GetInt("isDead", 1);
            if (isDead == 0)
            {
                gameOverWind.gameObject.SetActive(true);
                setSceneInteractableOff();
                PlayerPrefs.DeleteKey("isDead");
            }
            else if (gameOverWind.isActiveAndEnabled == false)
            {

                GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy");
                if (objs.Length == 0 && !levelUpWind.isActiveAndEnabled && !gameStartWind.isActiveAndEnabled && !gameEndWind.isActiveAndEnabled)
                {
                    levelNo++;
                    levelUpWind.gameObject.SetActive(true);
                    setSceneInteractableOff();
                    PlayerPrefs.SetInt("Level", levelNo);

                }
            }
            if (gameEndWind.isActiveAndEnabled)
            {
                setSceneInteractableOff();
            }

        }
    }

    public void onRetryClicked()
    {
        SceneManager.LoadScene("FinalGame1");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public void onContinueClicked()
    {
        if (levelNo < 2)
        {
            setSceneInteractableON();
            levelUpWind.gameObject.SetActive(false);
            levelManager.InstantiateLevel(levelNo);
        }
        else
        {
            setSceneInteractableOff();
            gameEndWind.gameObject.SetActive(true);
            levelUpWind.gameObject.SetActive(false);
        }



    }

    void setSceneInteractableOff()
    {

        GameObject[] finish = GameObject.FindGameObjectsWithTag("Finish");
        foreach (var obj in finish)
        {
            Debug.Log(obj.name);
            obj.gameObject.SetActive(false);
        }
        cam.gameObject.SetActive(true);
    }

    void setSceneInteractableON()
    {

        GameObject[] finish = GameObject.FindGameObjectsWithTag("Finish");
        foreach (var obj in finish)
        {

            obj.gameObject.SetActive(true);
        }
        cam.gameObject.SetActive(false);
    }


}
