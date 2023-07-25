using System.IO;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace Chatlyst.Editor
{
    [ScriptedImporter(1, "nvp")]
    public class NexusFileImporter : ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            /*
            var txt = File.ReadAllText(ctx.assetPath);
            var assetText = new TextAsset(txt);
            ctx.AddObjectToAsset("assetText", assetText);
            ctx.SetMainObject(assetText);
            */
        }
    }
}
