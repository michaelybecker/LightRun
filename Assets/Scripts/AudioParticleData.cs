using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class AudioParticleData : MonoBehaviour
{
    [SerializeField]
    VisualEffect _pty;
    AudioSpectrum _as;
    // Start is called before the first frame update
    void Start()
    {
        _as = GetComponent<AudioSpectrum>();

    }

    // Update is called once per frame
    void Update()
    {
        // print(_as.MeanLevels.Length);
        // for (var i = 0; i < _as.MeanLevels.Length; i++)
        // {
        // print(_as.MeanLevels[1]);
        _pty.SetFloat("MusicFreq", _as.MeanLevels[1]);

        // }
    }
}
