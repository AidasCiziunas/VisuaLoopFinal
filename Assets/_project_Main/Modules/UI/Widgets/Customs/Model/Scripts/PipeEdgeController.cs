using Doozy.Runtime.Reactor.Animators;
using MegaFiers;
using SyedAli.Main.UIModule.Widgets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace SyedAli.Main.UIModule.Widgets
{
    //Remeber A Pivot is from origin model left most, B Pivot is from origin mode right most
    public class PipeEdgeController : MonoBehaviour
    {
        public Transform EdgeCenPivT { get => _edgeCenPivT; }
        public List<EdgePivotData> EdgePivotDataLs { get => _edgePivotDataLs; }
        public EdgeTransformUpdater EdgeTUpdater { get => _edgeTUpdater; }
        public EdgeResetter EdgeResetter { get => edgeResetter; }

        [SerializeField] protected PipeEdgeType _pipeEdgeType;
        [SerializeField] protected Transform _edgeCenPivT;
        [SerializeField] protected EdgeTransformUpdater _edgeTUpdater;
        [SerializeField] private EdgeResetter edgeResetter;
        [SerializeField] protected List<EdgePivotData> _edgePivotDataLs;

        protected TouchControls _touchControls;
        protected MeshMovementType _lockRotation = MeshMovementType.None;

        protected virtual void Awake()
        {
            _touchControls = new TouchControls();
        }

        protected void OnEnable()
        {
            _touchControls.Enable();
        }

        protected void OnDisable()
        {
            _touchControls.Disable();
        }

        public virtual void OnTouchEnter()
        {
            //Debug.Log("OnTouchEnter");
        }

        public virtual void OnTouchDrag()
        {

        }

        public virtual void OnTouchExit()
        {
            //Debug.Log("OnTouchExit");
        }

        protected float CalculateAngle(float val, float rotatePer001 = 1.4f)
        {
            float constF = 0.01f;
            float angle = (val / constF) * rotatePer001;
            return angle;
        }
    }
}
