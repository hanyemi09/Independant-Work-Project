using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupText : MonoBehaviour
{

    [SerializeField] float lifetime = 3f;
    float maxLifetime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
        maxLifetime = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        //if(lifetime > maxLifetime * .5f)
        //{
        //    float increaseScaleAmount = 0.05f;
        //    transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;

        //}
        //else
        //{
        //    float decreaseScaleAmount = 0.05f;
        //    transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;

        //}
    }
}
