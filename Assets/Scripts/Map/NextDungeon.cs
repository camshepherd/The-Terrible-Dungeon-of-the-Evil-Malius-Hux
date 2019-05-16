using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextDungeon : MonoBehaviour
{
    public int firstBoss;
    public int secondBoss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("TODO: GO TO NEXT DUNGEON!");
        SimplePlayer p = collision.gameObject.GetComponent<SimplePlayer>();
        //Debug.Log(p.health);
        GameStores.SetHealth(p.health);
        GameStores.SetDefMod(p.shieldModifier);
        GameStores.SetAtkMod(p.fireDelay);
        //Debug.Log(p.shieldModifier);
        GameStores.IncrementLength();
        GameStores.IncrementDepth();
        if (GameStores.GetDepth() == firstBoss)
        {
            SceneManager.LoadScene("BossWall");
        }
        else if(GameStores.GetDepth() == secondBoss)
        {
            SceneManager.LoadScene("BossHux");
        }
        else
        {
            SceneManager.LoadScene("Dungeon");
        }
    }
}
