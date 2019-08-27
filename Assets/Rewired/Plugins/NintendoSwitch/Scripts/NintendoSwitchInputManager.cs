// Copyright (c) 2017 Augie R. Maddox, Guavaman Enterprises. All rights reserved.
using UnityEngine;
using System;
using System.Collections.Generic;

#pragma warning disable 0219
#pragma warning disable 0618
#pragma warning disable 0649
#pragma warning disable 0414

namespace Rewired.Platforms.Switch {
    using Rewired.Utils.Interfaces;

    /// <summary>
    /// The Nintendo Switch input manager. This component must be added to the
    /// Rewired Input Manager GameObject to enable native input on the Nintendo Switch platform.
    /// </summary>
    [AddComponentMenu("Rewired/Nintendo Switch Input Manager")]
    [RequireComponent(typeof(InputManager))]
    public sealed class NintendoSwitchInputManager : MonoBehaviour, IExternalInputManager {

        [SerializeField]
        private UserData _userData = new UserData();

#if UNITY_SWITCH
        /// <summary>
        /// Determines which Npad styles are supported.
        /// </summary>
        public NpadStyle allowedNpadStyles {
            get {
                if(SwitchInput.isReady) return SwitchInput.Config.allowedNpadStyles;
                else return (NpadStyle)_userData.allowedNpadStyles;
            }
            set {
                if(SwitchInput.isReady) SwitchInput.Config.allowedNpadStyles = value;
                else _userData.allowedNpadStyles = (int)value;
            }
        }

        /// <summary>
        /// Determines how the user should hold individual Joy-Cons.
        /// Vertical: Joy-Con held like a remote control using one hand with L/R and ZL/ZR facing forward/up.
        /// Horizontal: Joy-Con held like a gamepad with SL and SR facing forward/up.
        /// </summary>
        public JoyConGripStyle joyConGripStyle {
            get {
                return (JoyConGripStyle)_userData.joyConGripStyle;
            }
            set {
                if(SwitchInput.isReady) SwitchInput.Config.joyConGripStyle = value;
                else _userData.joyConGripStyle = (int)value;
            }
        }

        /// <summary>
        /// If enabled, returned values from six-axis sensors
        /// will be modified to reflect the Joy-Con grip style for single Joy-Cons.
        /// When using a horizontal grip style, +Z points out from the SL/SR buttons.
        /// When using a vertical grip style, +Z points out from the L/ZL/R/ZR buttons.
        /// </summary>
        public bool adjustIMUsForGripStyle {
            get {
                if(SwitchInput.isReady) return SwitchInput.Config.adjustIMUsForGripStyle;
                else return _userData.adjustIMUsForGripStyle;
            }
            set {
                if(SwitchInput.isReady) SwitchInput.Config.adjustIMUsForGripStyle = value;
                else _userData.adjustIMUsForGripStyle = value;
            }
        }

        /// <summary>
        /// Determines how many Joy-Cons must be attached for the Handheld mode to become active.
        /// </summary>
        public HandheldActivationMode handheldActivationMode {
            get {
                return (HandheldActivationMode)_userData.handheldActivationMode;
            }
            set {
                if(SwitchInput.isReady) SwitchInput.Config.handheldActivationMode = value;
                else _userData.handheldActivationMode = (int)value;
            }
        }

        /// <summary>
        /// If enabled, Joysticks will be assigned to Players based on the npad id of the controller.
        /// Otherwise, the standard Rewired Joystick auto-assignment system will be used.
        /// Enable this to keep the Switch npad id aligned to the Rewired Player id when Joysticks are assigned.
        /// </summary>
        public bool assignJoysticksByNpadId {
            get {
                if(SwitchInput.isReady) return SwitchInput.Config.assignJoysticksByNpadId;
                else return _userData.assignJoysticksByNpadId;
            }
            set {
                if(SwitchInput.isReady) SwitchInput.Config.assignJoysticksByNpadId = value;
                _userData.assignJoysticksByNpadId = value;
            }
        }

        /// <summary>
        /// If enabled, controller vibration will updated updated on a separate thread.
        /// Otherwise, controller vibration will be updated on the main thread.
        /// </summary>
        public bool useVibrationThread {
            get {
                if (SwitchInput.isReady) return SwitchInput.Config.useVibrationThread;
                else return _userData.useVibrationThread;
            }
            set {
                if (SwitchInput.isReady) SwitchInput.Config.useVibrationThread = value;
                _userData.useVibrationThread = value;
            }
        }

        /// <summary>
        /// Gets the settings for a specific Npad id.
        /// </summary>
        /// <param name="npadId">The Npad id.</param>
        /// <returns>Npad settings.</returns>
        public Rewired.Platforms.Switch.Config.INpadSettings GetNpadSettings(NpadId npadId) {
            NpadSettings_Internal settings;
            if(!_userData.npadSettings.TryGetValue(npadId, out settings)) return null;
            return new NpadSettings(npadId, settings);
        }

        /// <summary>
        /// Gets the settings for the Debug Pad.
        /// </summary>
        /// <returns>Debug Pad settings.</returns>
        public Rewired.Platforms.Switch.Config.IDebugPadSettings GetDebugPadSettings() {
            return new DebugPadSettings(_userData.debugPad);
        }
#endif

        #region IExternalInputManager implementation

        /// <exclude></exclude>
        /// <summary>
        /// Initializes the system. Do not call this method. For internal use only.
        /// </summary>
        /// <returns>Input manager</returns>
        object IExternalInputManager.Initialize(Platform platform, Rewired.Data.ConfigVars configVars) {
#if UNITY_SWITCH
            if(platform != Platform.Switch) return null;
            if(Application.isEditor) return null;
            if(configVars == null) return null;
            return Rewired.Utils.Platforms.Switch.Main.Initialize(this, _userData);
#else
            return null;
#endif
        }

        /// <exclude></exclude>
        /// <summary>
        /// Deinitializes the system. Do not call this method. For internal use only.
        /// </summary>
        void IExternalInputManager.Deinitialize() {
#if UNITY_SWITCH
            Rewired.Utils.Platforms.Switch.Main.Deinitialize(this);
#endif
        }

#endregion

        [Serializable]
        private class UserData : IKeyedData<int> {

            [SerializeField]
            private int _allowedNpadStyles = -1; // All
            [SerializeField]
            private int _joyConGripStyle = 1; // Horizontal
            [SerializeField]
            private bool _adjustIMUsForGripStyle = true;
            [SerializeField]
            private int _handheldActivationMode = 0; // Dual
            [SerializeField]
            private bool _assignJoysticksByNpadId = true;
            [SerializeField]
            private bool _useVibrationThread = true;

            // Npad settings -- one for each Npad Id

            [SerializeField]
            private NpadSettings_Internal _npadNo1 = new NpadSettings_Internal(0);
            [SerializeField]
            private NpadSettings_Internal _npadNo2 = new NpadSettings_Internal(1);
            [SerializeField]
            private NpadSettings_Internal _npadNo3 = new NpadSettings_Internal(2);
            [SerializeField]
            private NpadSettings_Internal _npadNo4 = new NpadSettings_Internal(3);
            [SerializeField]
            private NpadSettings_Internal _npadNo5 = new NpadSettings_Internal(4);
            [SerializeField]
            private NpadSettings_Internal _npadNo6 = new NpadSettings_Internal(5);
            [SerializeField]
            private NpadSettings_Internal _npadNo7 = new NpadSettings_Internal(6);
            [SerializeField]
            private NpadSettings_Internal _npadNo8 = new NpadSettings_Internal(7);
            [SerializeField]
            private NpadSettings_Internal _npadHandheld = new NpadSettings_Internal(0);
            [SerializeField]
            private DebugPadSettings_Internal _debugPad = new DebugPadSettings_Internal(0);

            public int allowedNpadStyles { get { return _allowedNpadStyles; } set { _allowedNpadStyles = value; } }
            public int joyConGripStyle { get { return _joyConGripStyle; } set { _joyConGripStyle = value; } }
            public bool adjustIMUsForGripStyle { get { return _adjustIMUsForGripStyle; } set { _adjustIMUsForGripStyle = value; } }
            public int handheldActivationMode { get { return _handheldActivationMode; } set { _handheldActivationMode = value; } }
            public bool assignJoysticksByNpadId { get { return _assignJoysticksByNpadId; } set { _assignJoysticksByNpadId = value; } }
            public bool useVibrationThread { get { return _useVibrationThread; } set { _useVibrationThread = value; } }
            private NpadSettings_Internal npadNo1 { get { return _npadNo1; } }
            private NpadSettings_Internal npadNo2 { get { return _npadNo2; } }
            private NpadSettings_Internal npadNo3 { get { return _npadNo3; } }
            private NpadSettings_Internal npadNo4 { get { return _npadNo4; } }
            private NpadSettings_Internal npadNo5 { get { return _npadNo5; } }
            private NpadSettings_Internal npadNo6 { get { return _npadNo6; } }
            private NpadSettings_Internal npadNo7 { get { return _npadNo7; } }
            private NpadSettings_Internal npadNo8 { get { return _npadNo8; } }
            private NpadSettings_Internal npadHandheld { get { return _npadHandheld; } }
            public DebugPadSettings_Internal debugPad { get { return _debugPad; } }

#if UNITY_SWITCH
            [NonSerialized]
            private Dictionary<NpadId, NpadSettings_Internal> __npadSettings;
            public Dictionary<NpadId, NpadSettings_Internal> npadSettings {
                get {
                    if(__npadSettings != null) return __npadSettings;
                    return (__npadSettings = new Dictionary<NpadId, NpadSettings_Internal>() {
                        { NpadId.No1, _npadNo1 },
                        { NpadId.No2, _npadNo2 },
                        { NpadId.No3, _npadNo3 },
                        { NpadId.No4, _npadNo4 },
                        { NpadId.No5, _npadNo5 },
                        { NpadId.No6, _npadNo6 },
                        { NpadId.No7, _npadNo7 },
                        { NpadId.No8, _npadNo8 },
                        { NpadId.Handheld, _npadHandheld }
                    });
                }
            }
#endif

            #region IKeyedData implementation

            private Dictionary<int, object[]> __delegates;
            private Dictionary<int, object[]> delegates {
                get {
                    if(__delegates != null) return __delegates;
                    return __delegates = new Dictionary<int, object[]>()
                    {
                        { 0, new object[] { new Func<int>(() => allowedNpadStyles), new Action<int>(x => allowedNpadStyles = x) } },
                        { 1, new object[] { new Func<int>(() => joyConGripStyle), new Action<int>(x => joyConGripStyle = x) } },
                        { 2, new object[] { new Func<bool>(() => adjustIMUsForGripStyle), new Action<bool>(x => adjustIMUsForGripStyle = x) } },
                        { 3, new object[] { new Func<int>(() => handheldActivationMode), new Action<int>(x => handheldActivationMode = x) } },
                        { 4, new object[] { new Func<bool>(() => assignJoysticksByNpadId), new Action<bool>(x => assignJoysticksByNpadId = x) } },
                        { 5, new object[] { new Func<object>(() => npadNo1), null } },
                        { 6, new object[] { new Func<object>(() => npadNo2), null } },
                        { 7, new object[] { new Func<object>(() => npadNo3), null } },
                        { 8, new object[] { new Func<object>(() => npadNo4), null } },
                        { 9, new object[] { new Func<object>(() => npadNo5), null } },
                        { 10, new object[] { new Func<object>(() => npadNo6), null } },
                        { 11, new object[] { new Func<object>(() => npadNo7), null } },
                        { 12, new object[] { new Func<object>(() => npadNo8), null } },
                        { 13, new object[] { new Func<object>(() => npadHandheld), null } },
                        { 14, new object[] { new Func<object>(() => debugPad), null } },
                        { 15, new object[] { new Func<bool>(() => useVibrationThread), new Action<bool>(x => useVibrationThread = x) } },
                    };
                }
            }

            bool IKeyedData<int>.TryGetValue<T>(int key, out T value) {
                object[] dels;
                if(!delegates.TryGetValue(key, out dels)) {
                    value = default(T);
                    return false;
                }
                Func<T> func = dels[0] as Func<T>;
                if(func == null) {
                    value = default(T);
                    return false;
                }
                value = func();
                return true;
            }

            bool IKeyedData<int>.TrySetValue<T>(int key, T value) {
                object[] v;
                if(!delegates.TryGetValue(key, out v)) return false;
                Action<T> action = v[1] as Action<T>;
                if(action == null) return false;
                action(value);
                return true;
            }

            #endregion
        }

        [Serializable]
        private sealed class NpadSettings_Internal : IKeyedData<int> {

            [Tooltip("Determines whether this Npad id is allowed to be used by the system.")]
            [SerializeField]
            private bool _isAllowed = true;

            [Tooltip("The Rewired Player Id assigned to this Npad id.")]
            [SerializeField]
            private int _rewiredPlayerId;

            [Tooltip(
                "Determines how Joy-Cons should be handled.\n\n" +
                "Unmodified: Joy-Con assignment mode will be left at the system default.\n" +
                "Dual: Joy-Cons pairs are handled as a single controller.\n" +
                "Single: Joy-Cons are handled as individual controllers."
            )]
            [SerializeField]
            private int _joyConAssignmentMode = -1; // Unmodified

            private bool isAllowed { get { return _isAllowed; } set { _isAllowed = value; } }
            private int rewiredPlayerId { get { return _rewiredPlayerId; } set { _rewiredPlayerId = value; } }
            private int joyConAssignmentMode { get { return _joyConAssignmentMode; } set { _joyConAssignmentMode = value; } }

            internal NpadSettings_Internal(int playerId) {
                this._rewiredPlayerId = playerId;
            }

            #region IKeyedData implementation

            private Dictionary<int, object[]> __delegates;
            private Dictionary<int, object[]> delegates {
                get {
                    if(__delegates != null) return __delegates;
                    return __delegates = new Dictionary<int, object[]>()
                    {
                        { 0, new object[] { new Func<bool>(() => isAllowed), new Action<bool>(x => isAllowed = x) } },
                        { 1, new object[] { new Func<int>(() => rewiredPlayerId), new Action<int>(x => rewiredPlayerId = x) } },
                        { 2, new object[] { new Func<int>(() => joyConAssignmentMode), new Action<int>(x => joyConAssignmentMode = x) } },
                    };
                }
            }

            bool IKeyedData<int>.TryGetValue<T>(int key, out T value) {
                object[] dels;
                if(!delegates.TryGetValue(key, out dels)) {
                    value = default(T);
                    return false;
                }
                Func<T> func = dels[0] as Func<T>;
                if(func == null) {
                    value = default(T);
                    return false;
                }
                value = func();
                return true;
            }

            bool IKeyedData<int>.TrySetValue<T>(int key, T value) {
                object[] v;
                if(!delegates.TryGetValue(key, out v)) return false;
                Action<T> action = v[1] as Action<T>;
                if(action == null) return false;
                action(value);
                return true;
            }

            #endregion
        }

        [Serializable]
        private sealed class DebugPadSettings_Internal : IKeyedData<int> {

            [Tooltip("Determines whether the Debug Pad will be enabled.")]
            [SerializeField]
            private bool _enabled = false;

            [Tooltip("The Rewired Player Id to which the Debug Pad will be assigned.")]
            [SerializeField]
            private int _rewiredPlayerId = 0;

            private int rewiredPlayerId { get { return _rewiredPlayerId; } set { _rewiredPlayerId = value; } }
            private bool enabled { get { return _enabled; } set { _enabled = value; } }

            internal DebugPadSettings_Internal(int playerId) {
                this._rewiredPlayerId = playerId;
            }

            #region IKeyedData implementation

            private Dictionary<int, object[]> __delegates;
            private Dictionary<int, object[]> delegates {
                get {
                    if(__delegates != null) return __delegates;
                    return __delegates = new Dictionary<int, object[]>()
                    {
                        { 0, new object[] { new Func<bool>(() => enabled), new Action<bool>(x => enabled = x) } },
                        { 1, new object[] { new Func<int>(() => rewiredPlayerId), new Action<int>(x => rewiredPlayerId = x) } }
                    };
                }
            }

            bool IKeyedData<int>.TryGetValue<T>(int key, out T value) {
                object[] dels;
                if(!delegates.TryGetValue(key, out dels)) {
                    value = default(T);
                    return false;
                }
                Func<T> func = dels[0] as Func<T>;
                if(func == null) {
                    value = default(T);
                    return false;
                }
                value = func();
                return true;
            }

            bool IKeyedData<int>.TrySetValue<T>(int key, T value) {
                object[] v;
                if(!delegates.TryGetValue(key, out v)) return false;
                Action<T> action = v[1] as Action<T>;
                if(action == null) return false;
                action(value);
                return true;
            }

            #endregion
        }
    }
}