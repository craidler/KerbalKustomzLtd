using System.Collections.Generic;
using UnityEngine;

namespace KKL
{
    public class LightManager : MonoBehaviour
    {
        private readonly List<Part> _ports = new List<Part>();
        private bool _active;

        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        private void FixedUpdate()
        {
            if (!_active) return;
            
            var vessel = FlightGlobals.ActiveVessel;
            var target = FlightGlobals.activeTarget;
            
            if (!vessel || !target || !target.FindModuleImplementing<ModuleDockingNode>())
            {
                foreach (var port in _ports) SwitchLight(port, false);
                _ports.Clear();
                return;
            }

            if (!_ports.Contains(target) && SwitchLight(target, true)) _ports.Add(target);
        }

        private void OnLevelWasLoaded(int level)
        {
            _active = GameScenes.FLIGHT.GetHashCode() == level;
        }

        private bool SwitchLight(Part port, bool state)
        {
            var changers = port.FindModulesImplementing<ModuleColorChanger>();

            if (0 == changers.Count) return false;

            foreach (var changer in changers) changer.ToggleEvent();

            return true;
        }
    }
}
