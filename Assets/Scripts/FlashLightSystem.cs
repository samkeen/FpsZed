using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightSystem : MonoBehaviour
{

    [SerializeField] private float lightDecay = .1f;
    [SerializeField] private float angleDecay = 1f;
    [SerializeField] private float minAngle = 40f;

    private Light _myLight;
    // Start is called before the first frame update
    void Start()
    {
        _myLight = GetComponent<Light>();
        
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseLightIntensity();
        DecreaseLightAngle();
    }

    private void DecreaseLightAngle()
    {
        if (_myLight.spotAngle>=minAngle)
        {
            _myLight.spotAngle -= _myLight.spotAngle * Time.deltaTime;
        }
    }

    private void DecreaseLightIntensity()
    {
        _myLight.intensity -= _myLight.intensity * Time.deltaTime;
    }
}
