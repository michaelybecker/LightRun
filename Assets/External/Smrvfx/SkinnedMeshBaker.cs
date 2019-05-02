using UnityEngine;
using System.Collections.Generic;

namespace Smrvfx
{
    public sealed class SkinnedMeshBaker : MonoBehaviour
    {
        #region Editable attributes

        public SkinnedMeshRenderer source = null;
        [SerializeField] RenderTexture _positionMap = null;
        [SerializeField] ComputeShader _compute = null;

        #endregion

        #region Temporary objects

        Mesh _mesh;
        Matrix4x4 _previousTransform = Matrix4x4.identity;

        int[] _dimensions = new int[2];

        List<Vector3> _positionList = new List<Vector3>();

        ComputeBuffer _positionBuffer1;

        RenderTexture _tempPositionMap;

        #endregion

        #region MonoBehaviour implementation

        void Start()
        {
            _mesh = new Mesh();
        }

        void OnDestroy()
        {
            Destroy(_mesh);
            _mesh = null;

            Utility.TryDispose(_positionBuffer1);

            Utility.TryDestroy(_tempPositionMap);

            _positionBuffer1 = null;

            _tempPositionMap = null;
        }

        void Update()
        {
            if (source == null) return;


            source.BakeMesh(_mesh);
            _mesh.GetVertices(_positionList);

            if (!CheckConsistency()) return;

            TransferData();
            _previousTransform = source.transform.localToWorldMatrix;
        }

        #endregion

        #region Private methods


        void TransferData()
        {
            var mapWidth = _positionMap.width;
            var mapHeight = _positionMap.height;

            var vcount = _positionList.Count;
            var vcount_x3 = vcount * 3;

            // Release the temporary objects when the size of them don't match
            // the input.

            if (_positionBuffer1 != null &&
                _positionBuffer1.count != vcount_x3)
            {
                _positionBuffer1.Dispose();
                _positionBuffer1 = null;
            }

            if (_tempPositionMap != null &&
                (_tempPositionMap.width != mapWidth ||
                 _tempPositionMap.height != mapHeight))
            {
                Destroy(_tempPositionMap);

                _tempPositionMap = null;
            }

            // Lazy initialization of temporary objects

            if (_positionBuffer1 == null)
            {
                _positionBuffer1 = new ComputeBuffer(vcount_x3, sizeof(float));
            }

            if (_tempPositionMap == null)
            {
                _tempPositionMap = Utility.CreateRenderTexture(_positionMap);
            }

            // Set data and execute the transfer task.

            _compute.SetInt("VertexCount", vcount);
            _compute.SetMatrix("Transform", source.transform.localToWorldMatrix);

            _positionBuffer1.SetData(_positionList);

            _compute.SetBuffer(0, "PositionBuffer", _positionBuffer1);

            _compute.SetTexture(0, "PositionMap", _tempPositionMap);

            _compute.Dispatch(0, mapWidth / 8, mapHeight / 8, 1);

            Graphics.CopyTexture(_tempPositionMap, _positionMap);
        }

        bool _warned;

        bool CheckConsistency()
        {
            if (_warned) return false;

            if (_positionMap.width % 8 != 0 || _positionMap.height % 8 != 0)
            {
                Debug.LogError("Position map dimensions should be a multiple of 8.");
                _warned = true;
            }

            if (_positionMap.format != RenderTextureFormat.ARGBHalf &&
                _positionMap.format != RenderTextureFormat.ARGBFloat)
            {
                Debug.LogError("Position map format should be ARGBHalf or ARGBFloat.");
                _warned = true;
            }
            return !_warned;
        }

        #endregion
    }
}
