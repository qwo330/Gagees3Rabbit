using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTimer : MonoBehaviour
{
    [SerializeField] float delay = 1;

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(IE_Timer());
    }

    IEnumerator IE_Timer()
    {
        yield return new WaitForSeconds(delay);

        ObjectPool.Get.ReturnObject(gameObject);
        gameObject.SetActive(false);
    }

}
