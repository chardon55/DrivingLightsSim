using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingLightsSim.Utils.Lbuc
{
    public abstract class AbstractUpdateSourceChecker : IUpdateSourceChecker
    {
        private readonly List<UpdateSource> sourceList = new List<UpdateSource>();

        public IReadOnlyList<UpdateSource> SourceList { get; protected set; }

        public abstract UpdateSource Select();

        void IUpdateSourceChecker.LoadSourceList(IEnumerable<UpdateSource> sourceList, bool append)
        {
            if (!append)
            {
                this.sourceList.Clear();
            }

            this.sourceList.AddRange(sourceList);
        }
    }
}
