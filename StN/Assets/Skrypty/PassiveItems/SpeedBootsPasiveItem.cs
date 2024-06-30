using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBootsPasiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.currentMoveSpeed *= 1 + passiveItemData.Multipler / 100f;
    }
}