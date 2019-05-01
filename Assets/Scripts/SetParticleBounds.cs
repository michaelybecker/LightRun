using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class SetParticleBounds : MonoBehaviour
{
    [SerializeField]
    private VisualEffect _vfx;
    private Camera _mainCam;
    // Start is called before the first frame update
    void Start()
    {
        _mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        _vfx.SetVector3("Bounds", _mainCam.transform.position);

    }
}
