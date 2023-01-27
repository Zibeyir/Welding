using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeldingScript : MonoBehaviour
{
    public float zIncrementer;
    public WeldingSpeed weldingSpeed;
    public Transform parent;
    public Light directinalLight;
    public GameObject vfx;
    public GameObject[] welds;
    public GameObject SpotLight;



    public float sphereRadius;
    public float maxDistance;
    public LayerMask layerMask;
    public LayerMask layerMaskW;
    public LayerMask layerMaskScore;

    public Transform PointLight;
    public Light light;
    float LightTimeFloat;
    public Transform VFX;
   
    Vector2 PassVector = new Vector2();
    int randomNumber;
    GameObject w;
    public float WeldingUpSize;
    GameObject passObject=null;
    public bool weldingBool = true;
    public bool weldingScriptBool=false;
    IEnumerator LightTime()
    {
        //print("LightTime");
        LightTimeFloat = 1;
        while (LightTimeFloat>0)
        {
            //print("LightTime"+ LightTimeFloat);

            yield return new WaitForSeconds(0.01f);
            LightTimeFloat -= .005f;
            light.intensity = LightTimeFloat;
        }

    }
    private void Update()
    {
        if (weldingScriptBool)
        {
            if (Input.GetMouseButtonDown(0))
            {
                light.intensity = 1;
                VFX.gameObject.SetActive(true);
                SpotLight.SetActive(true);
                //PointLight.gameObject.SetActive(true);

            }
            if (Input.GetMouseButtonUp(0))
            {
                VFX.gameObject.SetActive(false);
                SpotLight.SetActive(false);

                StartCoroutine(LightTime());
                //PointLight.gameObject.SetActive(false);


            }
        }
        
    }

    public void StopWelding()
    {
        VFX.gameObject.SetActive(false);
        SpotLight.SetActive(false);

        StartCoroutine(LightTime());
        directinalLight.intensity = 1;

    }

    void FixedUpdate()
    {
        if (weldingScriptBool)
        {
            zIncrementer += 1;

            if (zIncrementer >= 4)
            {
                zIncrementer = 0;





                if (Input.GetMouseButton(0))

                {


                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100,layerMaskW))
                    {
                        if (hit.collider.CompareTag("Stone"))
                        {
                            if (hit.collider.gameObject.transform.localScale.x < 10)
                            {
                                hit.collider.gameObject.transform.localScale += new Vector3(WeldingUpSize, WeldingUpSize, WeldingUpSize);

                            }

                            if (hit.collider.gameObject == passObject)
                            {
                                weldingBool = false;

                            }
                            else
                            {
                                passObject = hit.collider.gameObject;
                                weldingBool = true;
                            }
                        }
                        if (hit.collider.CompareTag("weld"))
                        {
                            weldingBool = true;

                        }
                    }

                    if (Physics.Raycast(ray, out hit, 100, layerMask))
                    {

                        if (weldingBool)
                        {
                            directinalLight.intensity = 0.2f;
                            randomNumber = Random.Range(0, welds.Length);
                            PointLight.position = hit.point;
                            VFX.position = hit.point;
                            w = Instantiate(welds[randomNumber], hit.point, RotateWeldingInstantiate(hit.point), parent);
                            w.transform.Rotate(new Vector3(0, 0, 90));
                        }

                    }
                    if (Physics.Raycast(ray, out hit, 100, layerMaskScore))
                    {
                        weldingSpeed.WeldingScoreBool = true;

                        //if (hit.collider.CompareTag("Score"))
                        //{
                        //    weldingSpeed.WeldingScoreBool = true;

                        //}
                        //else
                        //{
                        //    weldingSpeed.WeldingScoreBool = false;

                        //}

                    }
                    else
                    {
                        weldingSpeed.WeldingScoreBool = false;

                    }



                }
                else
                {
                    directinalLight.intensity = 1;
                }

            }
        }
       
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
    public Quaternion RotateWeldingInstantiate(Vector2 V2)
    {
       
        Vector2 movementDirection = V2-PassVector;
        //float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        //movementDirection.Normalize();
        //Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
        PassVector = V2;
        //print(Quaternion.LookRotation(Vector3.forward, movementDirection));
        //quaternionWelding = Quaternion.LookRotation(Vector3.forward, movementDirection) + new Vector3(0, 0, 90);
        return Quaternion.LookRotation(Vector3.forward, movementDirection);
        //transform.Translate(movementDirection *speed *inputMagnitude * Time.deltaTime, Space.World);

    }
}
