using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDamage : MonoBehaviour
{

    [SerializeField] private Canvas impactCanvas;
    [SerializeField] private float impactTime = .3f;
    
    // Start is called before the first frame update
    void Start()
    {
        this.impactCanvas.enabled = false;
    }

    public void SowDamageImpact()
    {
        StartCoroutine(ShowSplatter());
    }

    private IEnumerator ShowSplatter()
    {
        impactCanvas.enabled = true;
        yield return new WaitForSeconds(this.impactTime);
        impactCanvas.enabled = false;
    }
}
