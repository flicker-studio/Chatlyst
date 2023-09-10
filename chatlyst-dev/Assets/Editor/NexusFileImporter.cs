using UnityEditor.AssetImporters;
namespace Chatlyst.Editor
{
    [ScriptedImporter(0, NexusMacros.FilenameExtension)]
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
