using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    [CreateAssetMenu(fileName = "Game config", menuName = "Game/Config")]
    public class GameConfig : ScriptableObject
    {
        [Header("Path animation")] 
        public float delayToShowObjects;
        
        [Header("Figure path")]
        public float minDistToSegmentPoint = 0.2f;
        [FormerlySerializedAs("distBetweenSegments")] public float distanceBetweenSegments = 0.1f;
        public float splineSegmentNumberInOneFrame = 10f;
        public float distanceBetweenSprites = 1f;
        public int maxSkipSegments = 8;

        [Header("Tip settings")] 
        public float playStartSoundAfter = 7f;
        public float showArrowTipAfter = 14f;
        public float timerStep = 0.2f;
    }
}