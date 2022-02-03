using FrooxEngine;
using BaseX;

using HarmonyLib;
using NeosModLoader;
using System.Threading.Tasks;

namespace PrintImportReport
{
    public class PrintImportReport : NeosMod
    {
        public override string Name => "PrintImportReport";
        public override string Author => "badhaloninja";
        public override string Version => "1.0.0";
        public override string Link => "https://github.com/badhaloninja/PrintImportReport";
        public override void OnEngineInit()
        {
            Harmony harmony = new Harmony("me.badhaloninja.PrintImportReport");
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(UniversalImporter), "SpawnText")]
        class SpawnText_Patch
        {
			public static bool Prefix(Slot root, string text, color background, float textSize)
            {
				if(background == new color(1f, 0.8f, 0.8f, 0.5f) && textSize == 8f)
                {
                    root.Destroy();
                    Warn($"{root.Name}:\n {text}");

                    StringToken stringToken = StringTokenizer.Tokenize(text, TextRenderer.RichTextTags);

                    Task.Run(() =>
                    {
                        var printer = new PrintingExample();
                        printer.Print(stringToken.GetRawString());
                    });
                    return false;
                }
                return true;
			}
        }

    }
}