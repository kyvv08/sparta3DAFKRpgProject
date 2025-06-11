using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class StageManager : MonoBehaviour
{
    private static StageManager instance;

    public static StageManager Instance { get => instance; }
    private int curStageNum = 1;
    [SerializeField] private StageData[] stageData;
    [SerializeField] private BoxCollider[] spawnArea;
    
    private List<GameObject> enemies = new List<GameObject>();

    private Vector3 centerPosition;
    private float range = 20f;
    private float navMaxDistance = 5f;

    
    
    public bool IsInitDone = false;
    
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
        InitStage(curStageNum-1);
    }

    public void InitStage(int index)
    {
        IsInitDone = false;
        //Debug.Log(stageData.Length);
        while (index >= stageData.Length)
        {
            --index;
        }
        PlayerManager.Instance.player.transform.position = stageData[index].playerRespawnPosition;
        PlayerManager.Instance.player.StageRestart();
        curStageNum = stageData[index].stageNum;
        SpawnEnemyInStage(index);
        UIManager.Instance.gameInfoUI.SetCurStage(curStageNum);
        IsInitDone = true;
    }

    private void SpawnEnemyInStage(int index)
    {
        GameObject temp;
        if (enemies.Count > 0)
        {
            for (int i = enemies.Count - 1; i >= 0; --i)
            {
                temp = enemies[i];
                enemies.RemoveAt(i);
                Destroy(temp);
            }
        }
        Vector3 randomPos;
        for(int i = 0; i < stageData[index].enemies.Count;++i) 
        {
            for (int j = 0; j < stageData[index].enemyNum[i]; ++j)
            {
                GetRandomPoint(out randomPos);
                temp = Instantiate(stageData[index].enemies[i].EnemyPrefab, randomPos,Quaternion.identity);
                enemies.Add(temp);
            }
        }
    }
    private void GetRandomPoint(out Vector3 pos)
    {
        BoxCollider temp = spawnArea[Random.Range(0, spawnArea.Length)];
        Vector3 center = temp.bounds.center;
        Vector3 size = temp.bounds.size;

        float x = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float y = center.y;
        float z = Random.Range(center.z - size.z / 2, center.z + size.z / 2);
        pos = new Vector3(x, y, z);
    }

    public void DestroyEnemy(GameObject obj)
    {
        foreach (GameObject enemy in enemies)
        {
            if (obj == enemy)
            {
                enemies.Remove(enemy);
                Destroy(enemy);
                break;
            }
        }

        if (enemies.Count == 0)
        {
            InitStage(curStageNum);
        }
    }

    public List<GameObject> GetEnemyList()
    {
        return enemies;
    }

    public void PlayerDead()
    {
        InitStage(curStageNum-1);
    }
}
