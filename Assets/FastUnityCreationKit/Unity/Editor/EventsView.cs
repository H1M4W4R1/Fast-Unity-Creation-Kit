using System;
using System.Collections.Generic;
using System.Text;
using FastUnityCreationKit.Core.Extensions;
using FastUnityCreationKit.Unity.Interfaces.Callbacks;
using FastUnityCreationKit.Unity.Interfaces.Callbacks.Global;
using FastUnityCreationKit.Unity.Interfaces.Callbacks.Local;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Unity.Editor
{
    /// <summary>
    /// This struct is a utility used to display <see cref="CKMonoBehaviour"/> events
    /// within the Unity Editor.
    /// </summary>
#if UNITY_EDITOR
    public struct EventsView
    {
        private const string GENERIC_TYPE_SELF = "TSelf";
        private const string GENERIC_TYPE_OBJECT = "TObject";

        /// <summary>
        /// Builds a tooltip for the given <see cref="CKMonoBehaviour"/>.
        /// </summary>
        public (string, string countTooltip) BuildTooltip([NotNull] CKMonoBehaviour behaviour)
        {
            Type behaviourType = behaviour.GetType();

            StringBuilder sb0 = new("<size=16><color=#00FFFF><b>Events</b></color></size>");
            sb0.AppendLine();
            sb0.AppendLine("<b>Local events</b>");

            int localEventsCount = 0;

            // --------------------------------
            //
            // Handle drawing all local events
            //
            // --------------------------------

            List<Type> knownLocalEvents =
                behaviourType.GetInterfacesByType(typeof(ILocalCallback));
            for (int index = 0; index < knownLocalEvents.Count; index++)
            {
                Type knownLocalEvent = knownLocalEvents[index];
                if (knownLocalEvent.IsGenericType) continue;

                sb0.AppendLine($"<color=green>{knownLocalEvent.GetLabel()}</color>");
                localEventsCount++;
            }

            if (localEventsCount == 0) sb0.AppendLine("<color=white>No local events found</color>");

            // --------------------------------
            //
            // Handle drawing all global events
            //
            // --------------------------------

            int globalEventsCount = 0;

            sb0.AppendLine();
            sb0.AppendLine("<b>Global events</b>");
            List<Type> knownGlobalEvents =
                behaviourType.GetInterfacesByType(typeof(IGlobalCallback));
            for (int index = 0; index < knownGlobalEvents.Count; index++)
            {
                Type knownGlobalEvent = knownGlobalEvents[index];
                if (knownGlobalEvent.IsGenericType) continue;

                sb0.AppendLine($"<color=green>{knownGlobalEvent.GetLabel()}</color>");
                globalEventsCount++;
            }

            if (globalEventsCount == 0) sb0.AppendLine("<color=white>No global events found</color>");

            // --------------------------------
            //
            // Handle drawing all custom events
            //
            // --------------------------------

            int customEventsCount = 0;

            sb0.AppendLine();
            sb0.AppendLine("<b>Custom events</b>");
            List<Type> knownCustomEvents = behaviourType.GetInterfacesByType(typeof(ICustomCallback));
            for (int index = 0; index < knownCustomEvents.Count; index++)
            {
                Type knownCustomEvent = knownCustomEvents[index];
                if (knownCustomEvent.IsGenericType) continue;

                sb0.AppendLine($"{knownCustomEvent.GetLabel()}");
                customEventsCount++;
            }

            if (customEventsCount == 0) sb0.Append("<color=white>No custom events found</color>");

            // --------------------------------
            //
            // Handle drawing event count tooltip
            //
            // --------------------------------

            StringBuilder sb1 = new("<size=16><color=#00FFFF><b>Events</b></color></size>");
            sb1.AppendLine();
            sb1.AppendLine(
                $"<b>Local events</b>: {(localEventsCount > 0 ? localEventsCount : "<color=white>None</color>")}");
            sb1.AppendLine(
                $"<b>Global events</b>: {(globalEventsCount > 0 ? globalEventsCount : "<color=white>None</color>")}");
            sb1.Append(
                $"<b>Custom events</b>: {(customEventsCount > 0 ? customEventsCount : "<color=white>None</color>")}");

            return (sb0.ToString(), sb1.ToString());
        }
    }
#endif
}