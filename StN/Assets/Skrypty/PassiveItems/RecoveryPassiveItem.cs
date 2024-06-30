using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryPasiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.currentRecovery *= 1 + passiveItemData.Multipler / 100f;
    }
}
