using Core.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Base
{
    public class BaseWindow<T> : MonobehSingleton<T> where T : BaseWindow<T>
    {
        [SerializeField] protected GameObject panel;

        protected bool _isVisiable;

        public bool IsVisiable 
        {
            get => _isVisiable;
            protected set
            {
                _isVisiable = value;
                panel.SetActive(value);
            }
        }

        public virtual void Show() => 
            IsVisiable = true;

        public virtual void Hide() =>
            IsVisiable = false;
    }
}
