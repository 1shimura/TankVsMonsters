using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class RandomActorFactory : IFactory<IActor>
    {
        private readonly List<IActor> _actors;

        public RandomActorFactory(List<IActor> actors)
        {
            _actors = actors;
        }

        public IActor Create()
        {
            var randIndex = Random.Range(0, _actors.Count);
            var randItem = _actors[randIndex];

            var tempGameObject = Object.Instantiate((MonoBehaviour) randItem) as IActor;
            return tempGameObject;
        }
    }
}