using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public Player player;

    [SerializeField]
    TextMeshProUGUI[] statusText;

    public MessageWindow messageWindow;

    // Start is called before the first frame update
    void Awake()
    {

        // データロード
        //string dataPath = Application.persistentDataPath + "/player.json";
        //Debug.Log("dataPath:" + Application.persistentDataPath);

        player.charactor.status = PlayerPrefsUtils.GetObject<Status>("player");
        //　ファイルが存在しなければ作成
        //if (player == null)
        //{
        var init_status = new Status();
        init_status.name = "チャワンコ";
        init_status.lv = 1;
        //init_status.atk = Random.Range(2, 10);
        //init_status.speed = Random.Range(3, 10);
        //init_status.cute = Random.Range(1, 10);
        init_status.atk = 5;
        init_status.speed = 5;
        init_status.cute = 5;
        init_status.color = new Color(0.9f, 0.80f, 0.80f, 1.0f);
        //PlayerPrefsUtils.SetObject("player", init_status);
        init_status.hp = 100;
        player.charactor.status = init_status;
        //}
        player.charactor.status.color = new Color(0.9f, 0.80f, 0.80f, 1.0f);
        SetStatus();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetStatus()
    {
        statusText[0].text = player.charactor.status.name;
        statusText[1].text = player.charactor.status.atk.ToString(); ;
        statusText[2].text = player.charactor.status.speed.ToString(); ;
        statusText[3].text = player.charactor.status.cute.ToString();
        player.GetComponentInChildren<Renderer>().material.color = player.charactor.status.color;
    }
}
