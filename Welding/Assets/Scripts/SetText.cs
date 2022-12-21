using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetText : MonoBehaviour
{
    Text myText;

    void Start()
    {
        myText = GetComponent<Text>();
    }

    public void SetMyText(float value)
    {
        myText.text = ((int)(value*759)).ToString();
    }
    public void SetMyText2(float value)
    {
        myText.text = ((value )).ToString();
    }
}
