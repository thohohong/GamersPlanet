using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLightBall : MonoBehaviour
{
    int maxNum = 1000;
    GameObject[] list = new GameObject[1000];

    public GameObject LightBall;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxNum; i++)
        {
            float x = UnityEngine.Random.Range(-1000, 1000);
            float y = UnityEngine.Random.Range(-1000, 1000);
            float z = UnityEngine.Random.Range(-1000, 1000);

            /*
            x = x < 0 ? x - 200 : x + 200;
            y = y < 0 ? y - 200 : y + 200;
            z = z < 0 ? z - 200 : z + 200;
            */
            Instantiate(LightBall, new Vector3(x, y, z), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
