using System;

using UnityEngine;

#if MELONLOADER
using MelonLoader;

using Il2CppInterop.Runtime.InteropTypes.Fields;
#endif

namespace FluidLab
{
#if MELONLOADER
    [RegisterTypeInIl2Cpp]
#endif
    public class LiquidVolume : MonoBehaviour
    {
#if MELONLOADER
        public LiquidVolume(IntPtr intPtr) : base(intPtr) { }

        public Il2CppValueField<float> density;

        public Il2CppValueField<Vector3> center;

        public Il2CppValueField<Vector3> size;

        public Il2CppValueField<Vector3> flow;
#else
        [Min(0.001f)]
        [Tooltip("The density of the liquid. Defaults to 1000, the density of water. Measured in kg/m^3.")]
        public float density = 1000f;

        [Tooltip("The center of the liquid bounds.")]
        public Vector3 center = Vector3.zero;

        [Tooltip("The size of the liquid bounds.")]
        public Vector3 size = Vector3.one;

        [Tooltip("The local default amount of water flow, in m/s.")]
        public Vector3 flow = Vector3.zero;
#endif

        public float Density => density;

        private Vector3 _worldFlow = Vector3.zero;

        public Vector3 Velocity
        {
            get
            {
                return _worldFlow;
            }
        }

        private Vector3 _worldCenter = Vector3.zero;
        private Vector3 _worldSize = Vector3.zero;

        private float _height = 0f;

        public Vector3 WorldCenter => _worldCenter;

        public Vector3 WorldSize => _worldSize;

        public float Height => _height;

        private void LateUpdate()
        {
            var matrix = transform.localToWorldMatrix;

            _worldCenter = matrix.MultiplyPoint3x4(center);
            _worldSize = Vector3.Scale(matrix.lossyScale, size);

            _height = WorldCenter.y + WorldSize.y * 0.5f;

            _worldFlow = transform.rotation * flow;
        }

#if UNITY_EDITOR
        public void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;

            Gizmos.color = Color.blue;

            Gizmos.DrawWireCube(center, size);
        }
#endif
    }
}