using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageWindow : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _text;

    List<string> _textList;

    int index;

    public void InitText(List<string> textList)
    {
        gameObject.SetActive(true);
        _textList = textList;
        index = 0;
        SetText();
    }

    public void SetText()
    {
        _text.text = _textList[index];
    }

    public void ExitText()
    {
        gameObject.SetActive(false);
    }

    public void TapWindow()
    {
        index += 1;
        if(index > _textList.Count -1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            SetText();
        }
    }
}
