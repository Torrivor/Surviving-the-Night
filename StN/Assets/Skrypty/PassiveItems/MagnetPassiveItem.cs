using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.currentMagnet *= 1 + passiveItemData.Multipler / 100f;
    }
}
