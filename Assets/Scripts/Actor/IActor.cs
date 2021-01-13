using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public interface IActor : IResettable
    {
        IActor Spawner { get; set; }
        Transform Transform { get; }
        List<IActorAbility> Abilities { get; set; }
    }
}