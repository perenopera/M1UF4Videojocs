using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotarhastaangulo : MonoBehaviour
{
    float rotar;
    // Start is called before the first frame update
    void Start()
    {
        rotar = 40;
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(new Vector3(0, 0, rotar) * Time.deltaTime);
    }
}
