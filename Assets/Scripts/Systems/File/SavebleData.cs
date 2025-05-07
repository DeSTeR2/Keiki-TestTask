using System;

namespace Utils
{
    public abstract class SavebleData : IData
    {
        public virtual void Copy(IData data) { }

        public static TSave ConvertTo<TSave>(IData savebleData) where TSave : IData
        {
            if (savebleData is not null)
            {
                return (TSave)savebleData;
            }

            throw new Exception("Cannot convert " + typeof(TSave).Name + " to " + null);
        }
    }
}