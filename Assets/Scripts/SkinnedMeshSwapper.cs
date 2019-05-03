using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class SkinnedMeshSwapper : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer[] SkinnedMeshes;

    [SerializeField] Transform LocusFocus;

    [SerializeField] float[] LocusHeights;

    [SerializeField] float TransitionSpeed;
    int SMCounter = 0;

    bool _lerpForwards = true;

    Smrvfx.SkinnedMeshBaker _smb;

    [SerializeField]
    VisualEffect _vfx;

    // Start is called before the first frame update
    void Start()
    {
        _smb = GetComponent<Smrvfx.SkinnedMeshBaker>();
        _smb.source = SkinnedMeshes[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine(Transition());
        ;
    }

    void SwitchMesh()
    {
        if (SMCounter < SkinnedMeshes.Length - 1)
        {
            SMCounter += 1;
        }
        else
        {
            SMCounter = 0;
        }

        _smb.source = SkinnedMeshes[SMCounter];
        LocusFocus.transform.position = new Vector3(LocusFocus.transform.position.x, LocusHeights[SMCounter], LocusFocus.transform.position.z);
    }

    IEnumerator Transition()
    {
        float i = 0;

        while (i <= 1)
        {
            i += Time.deltaTime / TransitionSpeed;
            _vfx.SetFloat("LerpToTargetMap", i);
            yield return new WaitForEndOfFrame();
        }

        SwitchMesh();
        while (i >= 0)
        {
            i -= Time.deltaTime / TransitionSpeed;
            _vfx.SetFloat("LerpToTargetMap", i);
            yield return new WaitForEndOfFrame();
        }


        yield return null;
    }

}
