using Core.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Base
{
    public class BaseWindow : MonobehSingleton<BaseWindow>
    {
        [SerializeField] protected GameObject panel;

        public virtual void Show() =>
            panel.SetActive(true);

        public virtual void Hide() =>
            panel.SetActive(false);
    }
}
