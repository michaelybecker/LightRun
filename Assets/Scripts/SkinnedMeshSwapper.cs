using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinnedMeshSwapper : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer[] SkinnedMeshes;

    [SerializeField] Transform LocusFocus;

    [SerializeField] float[] LocusHeights;
    int SMCounter = 0;

    Smrvfx.SkinnedMeshBaker _smb;
    // Start is called before the first frame update
    void Start()
    {
        _smb = GetComponent<Smrvfx.SkinnedMeshBaker>();
        _smb.source = SkinnedMeshes[0];
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)) SwitchMesh();
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

}
