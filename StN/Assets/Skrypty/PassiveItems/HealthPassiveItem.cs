using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.currentHP *= 1 + passiveItemData.Multipler / 100f;
    }
}
