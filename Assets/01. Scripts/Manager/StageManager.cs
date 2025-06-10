using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage",menuName = "StageData")]
public class StageData : ScriptableObject
{
    public int stageNum;
    public List<KeyValuePair<EnemySO,int>> EnemyList;
    public Vector3 playerRespawnPosition;
}

public class StageManager : MonoBehaviour
{
    private static StageManager instance;

    public static StageManager Instance { get => instance; }
    private int curStageNum = 1;
    private StageData[] stageData;
    
    private List<Enemy> enemies;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void InitStage(int index)
    {
        if (index >= stageData.Length)
        {
            return;
        }
        PlayerManager.Instance.player.transform.position = stageData[index].playerRespawnPosition;
        SpawnEnemyInStage(index);
    }

    private void SpawnEnemyInStage(int index)
    {
        foreach (KeyValuePair<EnemySO, int> enemy in stageData[index].EnemyList)
        {
            for (int i = 0; i < enemy.Value; ++i)
            {
                enemies.Add(Instantiate());
            }
        }
    }
}
