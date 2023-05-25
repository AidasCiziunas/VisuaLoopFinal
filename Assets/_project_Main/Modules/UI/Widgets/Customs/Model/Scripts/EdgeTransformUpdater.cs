using Doozy.Runtime.Reactor.Animators;
using SyedAli.Main.UIModule.Widgets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace SyedAli.Main.UIModule.Widgets
{
    public class EdgeTransformUpdater : MonoBehaviour
    {
        [Space(10)]
        [SerializeField] Transform _thisT;

        [Space(10)]
        [SerializeField] float flexX;
        [SerializeField] float flexY;
        [SerializeField] float flexZ;

        private Vector3 _initPos;

        private void Start()
        {
            _initPos = _thisT.localPosition;
        }
        
        public void UpdateInitPos()
        {
            _initPos = this.transform.localPosition;
        }

        public bool UpdatePosWithLimit(Vector3 val)
        {
            return HandlePosWithLimit(val);
        }

        private bool HandlePosWithLimit(Vector3 val)
        {
            if (val.x >= (_initPos.x - flexX) && val.x <= (_initPos.x + flexX) && 
                val.y >= (_initPos.y - flexY) && val.y <= (_initPos.y + flexY) &&
                val.z >= (_initPos.z - flexZ) && val.z <= (_initPos.z + flexZ))
            {
                _thisT.localPosition = val;
                return true;
            }
            return false;
        }
    }
}
