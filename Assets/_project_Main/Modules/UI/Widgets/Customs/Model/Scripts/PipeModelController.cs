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
    public class PipeModelController : MonoBehaviour
    {
        public PipeModelControllerProperties Props { get => _props; }
        public float Multiplier { get => _props.Multiplier; }
        public AxisType AxisType { get => _props.ModelCurrentAxisType; }
        public Vector3 Pos { get => _thisTransform.position; }
        public Vector3 EulerAngle { get => _thisTransform.eulerAngles; }
        public Vector3 LocalScale { get => _thisTransform.localScale; }
        public bool ApplyLimOnEdges { get => _applyLimOnEdges; }
        public PipeEdgeType CurrentGrabbedEdge { get => _currentGrabbedEdge; set => _currentGrabbedEdge = value; }

        [SerializeField] protected bool _applyLimOnEdges;

        [SerializeField] protected Transform _thisTransform;
        [SerializeField] protected GameObject _thisGo;
        [SerializeField] protected Vector3Animator _localEulerAngleAnim;

        [Space(10)]
        [SerializeField] protected List<PipeEdgeData> _pipeEdgeDataLs;
        [SerializeField] protected List<PipeHoseData> _pipeHoseDataLs;
        [SerializeField] protected Transform _edgeParentT;

        protected PipeModelControllerProperties _props;
        private PipeEdgeType _currentGrabbedEdge;

        public virtual void Setup(PipeModelControllerProperties props)
        {

        }

        public virtual void Refresh(PipeModelControllerProperties props)
        {
            Setup(props);
        }

        public virtual void ResetModel()
        {
            foreach(var entry in _pipeEdgeDataLs)
            {
                entry.PipeEdgeController.EdgeResetter.Reset();
            }

            foreach(var entry in _pipeHoseDataLs)
            {
                entry.HoseResetter.Reset();
            }
        }

        public virtual void ChangeVisibilityStatus(bool status)
        {
            _thisGo.SetActive(status);
        }

        public virtual void DoRotationAnim(Vector3 targetAngle, UnityAction afterAnimAct)
        {
            _localEulerAngleAnim.animation.animation.fromCustomValue = _thisTransform.localEulerAngles;
            _localEulerAngleAnim.animation.animation.toCustomValue = targetAngle;
            _localEulerAngleAnim.animation.OnFinishCallback.AddListener(() => afterAnimAct?.Invoke());
            _localEulerAngleAnim.Play();
        }

        public virtual void DoRotateAroundInstant(Vector3 point, Vector3 axis, float angle)
        {
            _thisTransform.RotateAround(point, axis, angle);
        }

        public virtual void ChangeLocalScaleInstant(Vector3 localScale)
        {
            _thisTransform.localScale = localScale;
        }

        public virtual void ChangeHosesInteractibility(bool status)
        {
            foreach (var entry in _pipeHoseDataLs)
            {
                entry.Hose.enabled = status;
            }
        }

        public virtual void ChangeParents(ParentChangeBeh pCB, PipeEdgeType edgeToBecomeChild, PipeHoseType hoseToBecomeChild, PipeEdgeType edgeToBecomeParent)
        {
            Transform edgeParentT = _pipeEdgeDataLs.Find(x => x.Type == edgeToBecomeParent)?.PipeEdgeController.EdgePivotDataLs.Find(
                x => x.Type == (edgeToBecomeParent == PipeEdgeType.C ? EdgePivotType.A : EdgePivotType.None)).EdgePivotT;

            Transform edgeChildT = _pipeEdgeDataLs.Find(x => x.Type == edgeToBecomeChild)?.PipeEdgeController.EdgePivotDataLs.Find(
                x => x.Type == (edgeToBecomeChild == PipeEdgeType.D ? EdgePivotType.A : EdgePivotType.None)).EdgePivotT;

            Transform hoseChildT = _pipeHoseDataLs.Find(x => x.Type == hoseToBecomeChild)?.HoseT;

            if (pCB == ParentChangeBeh.Do)
            {
                if (edgeParentT != null)
                {
                    if (edgeChildT != null)
                        edgeChildT.SetParent(edgeParentT);
                    if (hoseChildT != null)
                        hoseChildT.SetParent(edgeParentT);
                }
            }
            else if (pCB == ParentChangeBeh.Reset)
            {
                if (edgeChildT != null)
                    edgeChildT.SetParent(_edgeParentT);
                if (hoseChildT != null)
                    hoseChildT.SetParent(_edgeParentT);
            }
        }
    }
}
