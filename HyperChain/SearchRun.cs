using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SemanticLinkHelper;

namespace HyperChain
{
    public class SearchRun : IEquatable<SearchRun>
    {
        WordSemanticBranch m_Link = null;

        public SearchRun(WordSemanticBranch aLink, int nDeep, bool bForce, int nCurDeep)
        {
            WordToSearch = aLink.ParentWord.Word;
            DeepSearch = nDeep;
            ForceSearch = bForce;
            CurDeep = nCurDeep;
            MaxDeep = nDeep;
            m_Link = aLink;
        }

        public int MaxDeep { get; set; }        
        public int CurDeep { get; set; }
        
        public WordSemanticBranch WordLink
        { 
            get{ return m_Link; }
            set{ m_Link = value; }
        }
        
        public string WordToSearch { get; set; }
        public int DeepSearch { get; set; }
        public bool ForceSearch { get; set; }

        public bool Equals(SearchRun other)
        {
            if (WordLink != null && other.WordLink != null)
                return WordLink.ParentWord.Equals(other.WordLink.ParentWord);
            else
                return false;
        }
    }
}
