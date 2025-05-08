using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Game config", menuName = "Game/Config")]
    public class GameConfig : ScriptableObject
    {
        [Header("Path animation")] 
        public float delayToShowObjects;
        
        [Header("Figure path")]
        public float MinDistToSegmentPoint = 0.2f;
        public float clickTolerance = 0.2f;
        public float evaluateStep = 0.1f;
        public Color toleranceRadiusColor = new Color(1f, 0f, 0f, 0.3f);
        
        [Header("Show figure path")]
        public float splineSegmentNumber = 50f;
        public float splineSegmentNumberInOneFrame = 10f;
        public float distanceBetweenSprites = 1f;
    }
}