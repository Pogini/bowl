using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] props;

    [SerializeField]
    Transform positions;

    // Start is called before the first frame update
    void Start()
    {
        SetRondomProp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetRondomProp()
    {
        foreach (Transform set_postion in positions)
        {
            //インスタンスを生成
            Instantiate(props[Random.Range(0, props.Length)], set_postion.position, Quaternion.identity);
        }
    }
}
