using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{

    public GameObject BGOptions;
    public GameObject BGFigures;
    public GameObject Logo;
    public WeldingScript weldingScript;
    public List<GameObject> Figures;
    void Start()
    {
        BGOptions.SetActive(true);
        BGFigures.SetActive(false);
        weldingScript.enabled = false;
    }

    public void OptionsCanvasFunc()
    {
        BGOptions.SetActive(false);
        BGFigures.SetActive(true);
    }
    public void FiguresCanvasFunc()
    {
        BGFigures.SetActive(false);
        Logo.SetActive(false);
        weldingScript.enabled = true;

    }
    public void FiguresFunc(int num)
    {
        for (int i = 0; i < Figures.Count; i++)
        {
            Figures[i].SetActive(i == num ? true : false);
        }
    }
}
