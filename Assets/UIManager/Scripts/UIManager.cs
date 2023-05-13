using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UIManagerLibrary.Scripts
{
    public class UIManager : MonoBehaviour
    {
        [HideInInspector]
        public int _activeFlags; //Used to track flags with DataContext

        public static UIManager current;

        public UIContextDataObject UIDataContext;

        public List<SerializedPair<string, UIContextObject>> UIContexts = new List<SerializedPair<string, UIContextObject>>();

        private Dictionary<string, List<UIContextObject>> _UIContexts = new Dictionary<string, List<UIContextObject>>();

        #region Unity Lifecycle

        private void Awake()
        {
            if (current != null) Debug.LogWarning("Oops! it looks like there might already be a " + GetType().Name + " in this scene!");
            current = this;
        }

        private void Start()
        {
            _InitializeContexts();
        }

        private void OnEnable()
        {
            _UpdateUIContexts();
        }

        //Editor based
        private void OnValidate()
        {
            _UpdateUIContexts();
        }

        private void Reset()
        {
            _ClearUIContexts();
        }

        //Helper methods
        private void _InitializeContexts()
        {
            foreach (var (kVP, i) in _UIContexts.Select((value, i) => (value, i)))
            {
                int bitMask = 1 << i;
                bool contextEnabled = (_activeFlags & bitMask) != 0;
                foreach (UIContextObject contextObject in kVP.Value) contextObject.SetDisplayAndActive(contextEnabled, true);
            }
        }

        private void _ClearUIContexts()
        {
            UIContexts.Clear();
            _UIContexts.Clear();
        }

        private void _UpdateUIContexts()
        {
            _UIContexts.Clear();

            foreach (SerializedPair<string, UIContextObject> sKVP in UIContexts)
            {
                if (sKVP.Value == null) continue;
                if (_UIContexts.ContainsKey(sKVP.Key)) //If key already exists then add the value into the existing dictionary list
                {
                    _UIContexts[sKVP.Key].Add(sKVP.Value);
                }
                else //If not then create a whole new object in the dictionary and then assign the current canvas group into that context
                {
                    _UIContexts.Add(sKVP.Key, new List<UIContextObject> { sKVP.Value });
                }
            }
        }

        #endregion

        public void SetActiveContexts(bool active, params string[] contexts)
        {
            foreach (string context in contexts)
            {
                SetAllContextsOfType(context, active);
            }
        }

        public void SetActiveContexts(bool active, bool immediate, params string[] contexts)
        {
            foreach (string context in contexts)
            {
                SetAllContextsOfType(context, active, immediate);
            }
        }

        public void ToggleActiveContexts(params string[] contexts)
        {
            foreach (string context in contexts)
            {
                ToggleContext(context);
            }
        }

        //These are incase we want to directly refer to the UIManager in buttons and call this in onclick event for the UI
        public void ToggleContext(string context)
        {
            ToggleAllContextsOfType(context);
        }

        public void EnableContext(string context)
        {
            SetAllContextsOfType(context, true);
        }

        public void DisableContext(string context)
        {
            SetAllContextsOfType(context, false);
        }

        //Helper methods
        private void SetAllContextsOfType(string context, bool active, bool immediate = false)
        {
            if (_UIContexts.ContainsKey(context) == false)
            {
                Debug.LogWarning($"Setting Context For: \"{context}\" could not be performed. Have you properly configured this context?");
                return;
            }

            foreach (UIContextObject contextObject in _UIContexts[context]) 
            { 
                contextObject.SetDisplayAndActive(active, immediate); 
            }
        }

        private void ToggleAllContextsOfType(string context)
        {
            if (_UIContexts.ContainsKey(context) == false)
            {
                Debug.LogWarning($"Toggle Context For: \"{context}\" could not be performed. Have you properly configured this context?");
                return;
            }

            foreach (UIContextObject contextObject in _UIContexts[context]) 
            { 
                contextObject.SetDisplayAndActive(!contextObject.Display); 
            }
        }
    }
}