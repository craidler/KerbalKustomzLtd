namespace KerbalKustomzLtd
{
    public static class Tool
    {
        
        /**
         * Sets the orbit of the active vessel to the one of a target vessel with a given lead time
         */
        public static void Rendezvous(Vessel vesselAct, Vessel vesselTgt, double leadtime)
        {
            var orbit = vesselTgt.GetOrbit();
            
            vesselAct.GetCurrentOrbit().SetOrbit(
                orbit.inclination,
                orbit.eccentricity,
                orbit.semiMajorAxis,
                orbit.LAN,
                orbit.argumentOfPeriapsis,
                orbit.meanAnomalyAtEpoch,
                orbit.epoch - leadtime,
                orbit.referenceBody
            );
        }

        /**
         * Resets all resettable science experiments on a vessel
         */
        public static void Reset(Vessel vesselAct)
        {
            foreach (var module in vesselAct.FindPartModulesImplementing<ModuleScienceExperiment>())
            {
                if (module.IsRerunnable())
                {
                    module.ResetExperiment();
                } 
            }
        }
        
        /**
         * Sets rigid attachment throughout the vessel
         */
        public static void Rigid(Vessel vesselAct)
        {
            foreach (var part in vesselAct.parts)
            {
                part.ApplyRigidAttachment();
            }
        }

        /**
         * Shutdown all enabled engines
         */
        public static void Shutdown(Vessel vesselAct)
        {
            foreach (var module in vesselAct.FindPartModulesImplementing<ModuleEngines>())
            {
                if (module.isEnabled)
                {
                    module.Shutdown();
                }
            }
        }
        
        /**
         * Teleports an active vessel on an orbit around a celestial body on a given altitude
         */
        public static void Teleport(Vessel vesselAct, CelestialBody bodyTgt, double inclination, double altitude)
        {
            altitude = bodyTgt.Radius >= altitude ? bodyTgt.Radius * .1 : altitude;
            
            vesselAct.GetCurrentOrbit().SetOrbit(
                inclination,
                0,
                altitude + bodyTgt.Radius,
                0,
                0,
                0,
                0,
                bodyTgt
            );
        }
    }
}