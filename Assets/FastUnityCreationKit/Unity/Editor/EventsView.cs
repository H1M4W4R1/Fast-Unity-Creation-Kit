using System;
using System.Collections.Generic;
using System.Text;
using FastUnityCreationKit.Core.Extensions;
using FastUnityCreationKit.Unity.Interfaces.Callbacks;
using FastUnityCreationKit.Unity.Interfaces.Callbacks.Global;
using FastUnityCreationKit.Unity.Interfaces.Callbacks.Local;
using JetBrains.Annotations;
using Sirenix.Utilities;

namespace FastUnityCreationKit.Unity.Editor
{
    /// <summary>
    /// This struct is a utility used to display <see cref="CKMonoBehaviour"/> events
    /// within the Unity Editor.
    /// </summary>
#if UNITY_EDITOR
    public struct EventsView
    {
        /// <summary>
        /// Builds a tooltip for the given <see cref="CKMonoBehaviour"/>.
        /// </summary>
        [NotNull] public string BuildTooltip([NotNull] CKMonoBehaviour behaviour)
        {
            Type behaviourType = behaviour.GetType();

            StringBuilder sb = new StringBuilder("<size=16><color=#00FFFF><b>Events</b></color></size>");
            sb.AppendLine();
            sb.AppendLine("<b>Local events</b>");

            int localEventsCount = 0;

            // --------------------------------
            //
            // Handle drawing all local events
            //
            // --------------------------------
            
            List<Type> knownLocalEvents = typeof(ILocalCallback).GetSameAssemblyInterfacesByRootInterface();
            for (int index = 0; index < knownLocalEvents.Count; index++)
            {
                Type knownLocalEvent = knownLocalEvents[index];
                if (!behaviourType.ImplementsOrInherits(knownLocalEvent)) continue;

                sb.AppendLine($"<color=green>{knownLocalEvent.GetLabel()}</color>");
                localEventsCount++;
            }

            if (localEventsCount == 0) sb.AppendLine("<color=white>No local events found</color>");

            // --------------------------------
            //
            // Handle drawing all global events
            //
            // --------------------------------
            
            int globalEventsCount = 0;

            sb.AppendLine();
            sb.AppendLine("<b>Global events</b>");
            List<Type> knownGlobalEvents = typeof(IGlobalCallback).GetSameAssemblyInterfacesByRootInterface();
            for (int index = 0; index < knownGlobalEvents.Count; index++)
            {
                Type knownGlobalEvent = knownGlobalEvents[index];
                if (!behaviourType.ImplementsOrInherits(knownGlobalEvent)) continue;

                sb.AppendLine($"{knownGlobalEvent.GetLabel()}");
                globalEventsCount++;
            }

            if (globalEventsCount == 0) sb.AppendLine("<color=white>No global events found</color>");
            
            // --------------------------------
            //
            // Handle drawing all custom events
            //
            // --------------------------------
            
            int customEventsCount = 0;
            
            sb.AppendLine();
            sb.AppendLine("<b>Custom events</b>");
            List<Type> knownCustomEvents = typeof(ICustomCallback).GetSameAssemblyInterfacesByRootInterface();
            for (int index = 0; index < knownCustomEvents.Count; index++)
            {
                Type knownCustomEvent = knownCustomEvents[index];
                if (!behaviourType.ImplementsOrInherits(knownCustomEvent)) continue;

                sb.AppendLine($"{knownCustomEvent.GetLabel()}");
                customEventsCount++;
            }

            if (customEventsCount == 0) sb.AppendLine("<color=white>No custom events found</color>");

            return sb.ToString();
        }
    }
#endif
}