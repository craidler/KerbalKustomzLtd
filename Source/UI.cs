using System;
using ClickThroughFix;
using UnityEngine;

namespace KerbalKustomzLtd
{
    public class UI : MonoBehaviour
    {
        private readonly Setting _setting = new Setting();
        private Rect _position = new Rect(100, 100, 200, 100);
        
        private void Start()
        {
            DontDestroyOnLoad(this);
        }
        
        private void Awake()
        {
            _setting.Load();
        }

        private void OnGUI()
        {
            GUI.skin = HighLogic.Skin;

            _position = ClickThruBlocker.GUILayoutWindow(568, _position, UiContent, Setting.Id + " Control Panel " + Setting.Version, GUILayout.Width(150), GUILayout.Height(150));
        }
        
        private void UiContent(int windowId) {
            GUILayout.BeginVertical();
            
            if (GUILayout.Button("Reset experiments", GUILayout.Width(125)))
            {
                print("well, reset experiments button has been pressed!");
                var vesselAct = FlightGlobals.ActiveVessel;

                if (vesselAct)
                {
                    Tool.Reset(vesselAct);
                }
            }
            
            if (GUILayout.Button("Shutdown engines", GUILayout.Width(125)))
            {
                print("well, the shutdown button has been pressed!");
                var vesselAct = FlightGlobals.ActiveVessel;

                if (vesselAct)
                {
                    Tool.Shutdown(vesselAct);
                }
            }

            GUILayout.Label("Separation:", GUILayout.Width(125));

            var altitude = GUILayout.TextField(_setting.GetValue("altitude"), GUILayout.Width(125));
            var inclination = GUILayout.TextField(_setting.GetValue("inclination"), GUILayout.Width(125));
            
            if (GUILayout.Button("Teleport", GUILayout.Width(125)))
            {
                print("well, the teleport button has been pressed!");
                var vesselAct = FlightGlobals.ActiveVessel;

                if (vesselAct)
                {
                    Tool.Teleport(vesselAct, vesselAct.GetOrbit().referenceBody, double.Parse(inclination), double.Parse(altitude));
                    _setting.SetValue("altitude", altitude).SetValue("inclination", inclination).Save();
                }
            }
            
            GUILayout.EndVertical();
        }
    }
}
