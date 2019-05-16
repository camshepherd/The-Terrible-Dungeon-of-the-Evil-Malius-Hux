using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float minPos;
    public float maxPos;

    [System.Serializable]
    public struct ShooterRisk
    {
        public GameObject prefab;
        public int risk;
    }

    [System.Serializable]
    public struct BulletRisk
    {
        public GameObject prefab;
        public int risk;
    }

    public ShooterRisk[] shooters;
    public BulletRisk[] bullets;

    public GameObject[] drops;
    public float dropChance = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        Camera mainCamera = Camera.main;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<ShooterRisk> shooterList()
    {
        List<ShooterRisk> output = new List<ShooterRisk>();
        foreach(ShooterRisk g in shooters)
        {
            output.Add(g);
        };
        return output;
    }

    private ShooterRisk PickShooter(int capacity)
    {
        List<ShooterRisk> shooters = shooterList();
        while (shooters.Count > 0)
        {
            int choice = Random.Range(0, shooters.Count);
            if(shooters[choice].risk <= capacity)
            {
                return shooters[choice];
            }
            else
            {
                shooters.RemoveAt(choice);
            }
        }
        Debug.Log("No Valid Enemies for capacity + " + capacity);
        ShooterRisk debug = new ShooterRisk
        {
            risk = -1
        };
        return debug;

    }

    private List<BulletRisk> BulletList()
    {
        List<BulletRisk> output = new List<BulletRisk>();
        foreach (BulletRisk br in bullets)
        {
            output.Add(br);
        }
        return output;
    }

    private BulletRisk PickBullet(int capacity)
    {
        List<BulletRisk> bs = BulletList();
        while (bs.Count > 0)
        {
            int choice = Random.Range(0, bs.Count);
            if (bs[choice].risk <= capacity)
            {
                return bs[choice];
            }
            else
            {
                bs.RemoveAt(choice);
            }
        }

        BulletRisk debug = new BulletRisk
        {
            risk = -1
        };
        return debug;
    }

    public void SpawnShooters(int capacity)
    {
        if(capacity == 0)
        {
            Debug.Log("No Risk");
            return;
        }
        while(capacity > 0)
        {
            ShooterRisk sr = PickShooter(capacity);
            if(sr.risk == -1)
            {
                // No valid choices
                return;
            }

            BulletRisk br = PickBullet(capacity);
            if (br.risk == -1)
            {
                // No valid choices
                return;
            }
            float xpos = Random.Range(minPos, maxPos);
            float ypos = Random.Range(minPos, maxPos);

            Vector3 screenPosition = Camera.main.ViewportToWorldPoint(new Vector3(xpos, ypos, 0));

            GameObject g = Instantiate(sr.prefab, new Vector3(screenPosition.x, screenPosition.y, 0), Quaternion.identity);

            StaticShooter ss = g.GetComponent<StaticShooter>();

            List<Vector3> playerColours = GameStores.GetPlayerColours();
            Vector3 col = playerColours[Random.Range(0, playerColours.Count)];
            ss.setColour(col);
            ss.missile = br.prefab;
            ss.dropOnDeath = drops;
            ss.dropChance = dropChance;

            capacity -= Mathf.Max(sr.risk, br.risk);
        }
    }


}
