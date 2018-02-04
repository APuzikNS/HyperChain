using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SemanticLinkHelper;

namespace HyperChain
{
    public class ShowHyperRun
    {
        WordSemanticBranch m_Link = null;


        public ShowHyperRun(WordSemanticBranch aLink, int nDeep, int nCurDeep, bool bNewBranch)
        {
            DeepSearch = nDeep;
            CurDeep = nCurDeep;
            m_Link = aLink;
            NewBranch = bNewBranch;
        }

        public int DeepSearch { get; set; }
        public int CurDeep { get; set; }
        public bool NewBranch{ get; set; }
        
        public WordSemanticBranch WordLink
        { 
            get{ return m_Link; }
            set{ m_Link = value; }
        }
    }
}
