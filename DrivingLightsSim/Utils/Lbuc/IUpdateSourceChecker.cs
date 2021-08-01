using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingLightsSim.Utils.Lbuc
{
    public interface IUpdateSourceChecker
    {
        protected void LoadSourceList(IEnumerable<UpdateSource> sourceList, bool append = false);

        UpdateSource Select();
    }
}
