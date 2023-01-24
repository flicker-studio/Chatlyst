using System.IO;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace NexusVisual.Editor
{
    [ScriptedImporter(1, "nvp")]
    public class NexusPlotImporter : ScriptedImporter
    {
        public const string Extension = "nvp";

        public override void OnImportAsset(AssetImportContext ctx)
        {
            var txt = File.ReadAllText(ctx.assetPath);
            var assetText = new TextAsset(txt);
            ctx.AddObjectToAsset("assetText", assetText);
            ctx.SetMainObject(assetText);
        }
    }
}