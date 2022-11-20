using TMPro;
using UnityEngine;
using AVG.Runtime.Controller;

public class Test : MonoBehaviour
{
    public TMP_Text charName;
    public TMP_Text text;

    public void Next()
    {
        EngineCore.Player.info(out var a, out var t);
        charName.SetText(a);
        text.SetText(t);
        EngineCore.Player.UpdateThis();
    }
}