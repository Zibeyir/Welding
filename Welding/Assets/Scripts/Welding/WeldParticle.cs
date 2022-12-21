using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeldParticle : MonoBehaviour
{
    public float pos = .4f;
    IEnumerator Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + pos);
        yield return new WaitForSeconds(.7f);
        Destroy(this.gameObject);
    }

}
