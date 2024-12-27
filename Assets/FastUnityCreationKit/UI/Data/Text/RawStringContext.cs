using Sirenix.OdinInspector;
using UnityEngine;

namespace FastUnityCreationKit.UI.Data.Text
{
    public sealed class RawStringContext : StringContextBase<RawStringContext>
    {
        [TabGroup("Configuration")] [ShowInInspector] [SerializeField]
        private string text;

        public override string LocalizedText => text;
    }
}