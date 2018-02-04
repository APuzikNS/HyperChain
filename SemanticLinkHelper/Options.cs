using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemanticLinkHelper
{
    public class Options
    {
        bool m_bForceSeacrh = false;
        int m_nDeepSearch = 1;

        public bool ForceSearch
        {
            get { return m_bForceSeacrh; }
            set { m_bForceSeacrh = value; }
        }

        public int DeepSearch
        {
            get { return m_nDeepSearch; }
            set { m_nDeepSearch = value; }
        }
    }
}
