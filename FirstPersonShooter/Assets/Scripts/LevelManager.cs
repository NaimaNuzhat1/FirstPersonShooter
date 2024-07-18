using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Singleton
    public static LevelManager instance;
    public List<GameObject> levelPrefabs;
    private void Awake()
    {
        instance = this;
    }
    #endregion Singleton
    public void InstantiateLevel(int levelNum)
    {
        if(levelNum<2)
        {
            Instantiate(levelPrefabs[levelNum], levelPrefabs[levelNum].transform.position, Quaternion.identity);
        }

    }

}
