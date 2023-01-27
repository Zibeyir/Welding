using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class UIScript : MonoBehaviour
{

    public GameObject BGOptions;
    public GameObject BGFigures;
    public GameObject Logo;
    public GameObject Final;
    public TextMeshProUGUI FinalScore;
    public WeldingScript weldingScript;
    public WeldingSpeed weldingSpeed;
    public List<GameObject> Figures;
    void Awake()
    {
        Final.SetActive(false);

        BGOptions.SetActive(true);
        BGFigures.SetActive(false);
        //weldingScript.enabled = false;
        Figures[0].SetActive(true);
        weldingSpeed.WeldingSpeedBool = false;
        weldingScript.weldingScriptBool = false;
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
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
        //weldingScript.enabled = true;
        weldingScript.weldingScriptBool = true;
        weldingSpeed.WeldingSpeedBool = true;
        StartCoroutine(weldingSpeed.Timer());

    }
    public void FiguresFunc(int num)
    {
        for (int i = 0; i < Figures.Count; i++)
        {
            Figures[i].SetActive(i == num ? true : false);
        }
    }
    public void FinalFunc()
    {
        weldingScript.weldingScriptBool = false;
        weldingScript.StopWelding();
        //weldingScript.enabled = false;

        FinalScore.text = ((int)weldingSpeed.ResultScore).ToString();
        Final.SetActive(true);
    }


}
