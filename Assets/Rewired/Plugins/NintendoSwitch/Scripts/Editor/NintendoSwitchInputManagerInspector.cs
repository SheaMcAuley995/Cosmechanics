// Copyright (c) 2017 Augie R. Maddox, Guavaman Enterprises. All rights reserved.
#pragma warning disable 0219
#pragma warning disable 0618
#pragma warning disable 0649

namespace Rewired.Editor.Platforms.Switch {
    using UnityEditor;
    using Rewired.Platforms.Switch;

    [System.ComponentModel.Browsable(false)]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    [CustomEditor(typeof(NintendoSwitchInputManager))]
    public sealed class NintendoSwitchInputManagerInspector : CustomInspector_External {

        private void OnEnable() {
            internalEditor = new NintendoSwitchInputManagerInspector_Internal(this);
            internalEditor.OnEnable();
        }
    }
}