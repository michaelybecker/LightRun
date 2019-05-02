using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;
public class GetCharPos : MonoBehaviour
{

    VisualEffect _vfx;

    [SerializeField]
    Transform CharPos;
    // Start is called before the first frame update
    void Start()
    {
        _vfx = GetComponent<VisualEffect>();

    }

    // Update is called once per frame
    void Update()
    {
        _vfx.SetVector3("CharacterPosition", CharPos.position);

    }
}
