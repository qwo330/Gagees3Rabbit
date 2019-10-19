using System.Collections;
using UnityEngine;

public enum ZoombieType
{
    Normal,
    Range,
}

public class MonsterSpawn : MonoBehaviour
{
    Vector2 offset = new Vector2(200, 40);

    void Start()
    {
        StartSpawn();
    }

    public void StartSpawn()
    {
        StartCoroutine(IE_Spawn());
        //StartCoroutine(IE_Spawn2());
    }

    IEnumerator IE_Spawn()
    {
        while (GameManager.Get.gameState == GameState.Play)
        {
            yield return new WaitForSeconds(2f);

            GameObject go = ObjectPool.Get.GetObject("Zombie");
            Monster zombie = go.GetComponent<Monster>();
            zombie.Initialize();
            //int dir = Random.Range(0, 1);
            zombie.transform.position = GetSpawnPoint();
        }
    }

    IEnumerator IE_Spawn2()
    {
        while (GameManager.Get.gameState == GameState.Play)
        {
            yield return new WaitForSeconds(8f);

            GameObject go = ObjectPool.Get.GetObject("Zombie2");
            Monster zombie = go.GetComponent<Monster>();
            zombie.Initialize();
            //int dir = Random.Range(0, 1);
            zombie.transform.position = GetSpawnPoint();
        }
    }

    Vector2 GetSpawnPoint()
    {
        Vector2 playerPos = GameManager.Get.player.transform.position;
        Vector2 point = new Vector2(playerPos.x, playerPos.y) + offset;
        //if (dir == 1)
        //    point *= Vector2.left;

        GameManager.WriteLog(point);
        return point;
    }
}