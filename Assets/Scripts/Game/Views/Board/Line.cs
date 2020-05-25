using System;
using System.Collections;
using System.Collections.Generic;
using FreeFlow.Game.Views;
using UnityEngine;

namespace FreeFlow.Game.Views.Board
{
    [RequireComponent(typeof(LineRenderer))]
    public class Line : View
    {
        private LineRenderer _lineRenderer;
        private int index = 1;

        #region Properties

        private Color _lineColor;
        public Color LineColor
        {
            get
            {
                return _lineColor;
            }
            set
            {
                SetLineColor(value);
            }
        }

        #endregion

        private void Awake()
        {
            Collect();
        }

        private void Collect()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            _lineRenderer.startColor = _lineColor;
            _lineRenderer.endColor = _lineColor;
        }

        #region Mutators

        public void SetPosition(int index, Vector3 position)
        {
            if(_lineRenderer != null)
            {
                _lineRenderer.SetPosition(index, position * 0.2f);
            }
        }

        public void SetLineColor(Color value)
        {
            _lineColor = value;
        }

        #endregion

        public void ClearLine()
        {
            index = 0;
            _lineRenderer.SetPosition(0, Vector3.zero);
        }

    }
}
