using System;

namespace EnroladorStandAloneV2.Herramientas.Huellero {
    public class FingerFeatureEventArgs : EventArgs
    {
        protected int _count;
        public int Count { get { return _count; } }
        public FingerFeatureEventArgs(int count)
        {
            _count = count;
        }
    }
}