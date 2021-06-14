using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MessageProp : MonoBehaviour
{
    public List<string> messageList;

    void OnTriggerEnter(Collider hit)
    {
        //プレイヤーの場合
        if (hit.transform.gameObject.tag == "Player" && !hit.transform.gameObject.GetComponent<Bullet>())
        {
            //会話開始
            GameManager.I.messageWindow.InitText(messageList);
        }
    }

    void OnTriggerExit(Collider hit)
    {
        //プレイヤーの場合
        if (hit.transform.gameObject.tag == "Player" && !hit.transform.gameObject.GetComponent<Bullet>())
        {
            //会話終了
            GameManager.I.messageWindow.ExitText();
        }
    }
}
