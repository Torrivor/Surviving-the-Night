using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MightPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.currentMight *= 1 + passiveItemData.Multipler / 100f;
    }
}
