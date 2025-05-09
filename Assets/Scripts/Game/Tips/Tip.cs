using System;

namespace Game.Tips
{
    public class Tip
    {
        private readonly float _startIn;
        private readonly Action _tip;
        private readonly float _tipTimeDifference;

        public Tip(float startIn, float tipTimeDifference, Action tip)
        {
            _startIn = startIn;
            _tipTimeDifference = tipTimeDifference;
            _tip = tip;
        }

        public bool StartTip(float inactiveTime)
        {
            if (CanStartTip(inactiveTime))
            {
                _tip?.Invoke();
                return true;
            }

            return false;
        }

        private bool CanStartTip(float inactiveTime)
        {
            float difference = inactiveTime - _startIn;
            return difference >= 0 && difference <= _tipTimeDifference;
        }
    }
}