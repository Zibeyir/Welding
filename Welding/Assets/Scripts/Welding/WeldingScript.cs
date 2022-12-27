using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeldingScript : MonoBehaviour
{
    public float zIncrementer;

    public Transform parent;
    public Light directinalLight;
    public GameObject vfx;
    public GameObject[] welds;
    float times = 0.2f;
    public GameObject SpotLight;
    void Start()
    {
        
    }

    public float sphereRadius;
    public float maxDistance;
    public LayerMask layerMask;
    public Transform PointLight;
    public Light light;
    RaycastHit hit;
    float LightTimeFloat;
    public Transform VFX;
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
    void FixedUpdate()
    {
        zIncrementer += 1;

        if (zIncrementer >= 4)
        {
            zIncrementer = 0;


            // if (Physics.SphereCast(transform.position, sphereRadius, transform.forward, out hit, maxDistance,layerMask,QueryTriggerInteraction.UseGlobal))
            // {
            //
            // }
           

            if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if(hit.transform.tag == "weld")
                {                
                        

                    directinalLight.intensity = 0;
                    int randomNumber = Random.Range(0, welds.Length);
                    PointLight.position = hit.point;
                        VFX.position = hit.point;
                    //Instantiate(vfx, hit.point, Quaternion.identity);
                    Instantiate(welds[randomNumber], hit.point, Quaternion.identity, parent);

                }
                else
                {
                    if (hit.transform.tag != "Stone")
                        {
                            Instantiate(vfx, hit.point, Quaternion.identity);

                            directinalLight.intensity = 1;
                    }
                    else
                    {
                        Instantiate(vfx, hit.point, Quaternion.identity);

                    }

                }
            }
        }
        else
        {
            directinalLight.intensity = 1;
        }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}
