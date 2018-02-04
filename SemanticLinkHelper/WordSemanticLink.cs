using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseHelper;
using System.ServiceModel.Description;
using System.Runtime.Serialization;
using ExplEntities;
using System.ComponentModel;

namespace SemanticLinkHelper
{
    [DataContract(Name = "WordLinkType")]
    public enum WordLinkType
    {
        [EnumMember(Value = "Normal")]
        [Description("Дійсне")]
        eNormal = 0,
        [EnumMember(Value = "Ignored")]
        [Description("Ігноровано")]
        eIgnored,
        [EnumMember(Value = "Tentative")]
        [Description("Невизначено")]
        eTentative,
        [EnumMember(Value = "Absent")]
        [Description("Відсутнє")]
        eAbsent
    }


    /// <summary>
    /// Relation Parent
    /// </summary>
    [DataContract]
    public class WordLink : IEquatable<WordLink>
    {
        RegistryWord    m_sChild;
        bool            m_bAuto;
        WordLinkType    m_eLinkType;
        short?           m_nRelGroupId;

        public WordLink(RegistryWord sChild, bool bAuto, WordLinkType eType, short relGroupId)
        {
            m_sChild = sChild;
            m_bAuto = bAuto;
            m_eLinkType = eType;
            m_nRelGroupId = relGroupId;
        }

        public WordLink(RelationItem anItem, WordItem sChild)
        {
            m_sChild = new RegistryWord(sChild);
            m_bAuto = anItem.IsAuto;
            m_eLinkType = (WordLinkType)anItem.RelationType;
            m_nRelGroupId = (short?)anItem.RelationTypeGroup;
        }

        [DataMember]
        public WordLinkType RelationType
        {
            get { return m_eLinkType; }
            set { m_eLinkType = value; }
        }

        [DataMember]
        public short? RelationGroupId
        {
            get { return m_nRelGroupId; }
            set { m_nRelGroupId = value; }
        }

        [DataMember]
        public bool AutoLink
        {
            get { return m_bAuto; }
            set { m_bAuto = value; }
        }

        [DataMember]
        public RegistryWord Child
        {
            get { return m_sChild; }
            set { m_sChild = value; }
        }

        public bool Equals(WordLink other)
        {
            return Child.Equals(other.Child);
            //return Child.Word.Equals(other.Child.Word, StringComparison.OrdinalIgnoreCase) && Child.Homonym == other.Child.Homonym;
        }

    }

    [DataContract]
    public class WordSemanticBranch : IEquatable<WordSemanticBranch>, INotifyPropertyChanged
    {
        List<WordLink> m_Children = new List<WordLink>();
        RegistryWord m_wordParent = new RegistryWord();

        [DataMember]
        public List<WordLink> Children
        {
            get { return m_Children; }
            set { m_Children = value; }
        }

        public int Ignored
        {
            get { return m_Children.Sum(aLink =>
                (aLink.RelationType == WordLinkType.eIgnored ||
                aLink.RelationType == WordLinkType.eAbsent) ? 1 : 0); }
        }

        public int Tentative
        {
            get
            {
                return m_Children.Sum(aLink =>
                    (aLink.RelationType == WordLinkType.eTentative) ? 1 : 0);
            }
        }

        [DataMember]
        public RegistryWord ParentWord
        {
            get { return m_wordParent; }
            set { m_wordParent = value; OnPropertyChanged("ParentWord"); }
        }

        public bool Equals(WordSemanticBranch other)
        {
            return ParentWord.Equals(other.ParentWord);
            //return ParentWord.Word.Equals(other.ParentWord.Word, StringComparison.OrdinalIgnoreCase) && ParentWord.Homonym == other.ParentWord.Homonym;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    [DataContract(Name = "WordStatus")]
    public enum WordStatus
    {
        [EnumMember(Value = "NotProcessed")]
        [Description("Не оброблено")]
        eNotProcessed,
        [EnumMember(Value = "InProgress")]
        [Description("Обробляється")]
        eInProgress,
        [EnumMember(Value = "Completed")]
        [Description("Завершено")]
        eCompleted,
    }

    [DataContract]
    public class RegistryWord : IEquatable<RegistryWord>
    {
        string m_sWord = "";
        short? m_nOmon = 0;
        WordStatus m_eStatus = WordStatus.eNotProcessed;
        bool m_bReviewed = false;

        public RegistryWord()
        {
        }

        public RegistryWord(WordItem anItem)
        {
            m_sWord = anItem.Word;
            m_eStatus = (WordStatus)anItem.Status;
            m_nOmon = anItem.Homonym == null ? 0 : anItem.Homonym;
            m_bReviewed = anItem.Reviewed == null ? false : (bool)anItem.Reviewed;
        }

        internal RegistryWord(SemanticLinkHelper.ExplDictService.elList elem)
        {
            m_sWord = CreateNameWithHomonym(elem.word, elem.omon);
            m_eStatus = WordStatus.eNotProcessed;
            m_nOmon = elem.omon.Length == 0 ?(short) 0 : Convert.ToInt16(elem.omon);
        }

        [DataMember]
        public WordStatus Status
        {
            get { return m_eStatus; }
            set { m_eStatus = value; }
        }

        [DataMember]
        public string Word
        {
            get { return m_sWord; }
            set { m_sWord = value; }
        }

        [DataMember]
        public bool Reviewed
        {
            get { return m_bReviewed; }
            set { m_bReviewed = value; }
        }

        [DataMember]
        public short? Homonym
        {
            get { return m_nOmon; }
            set { m_nOmon = value; }
        }

        static public string CreateNameWithHomonym(string sName, string sHomonym)
        {
            return string.Format("{0}{1}", sName, sHomonym);
        }

        public bool Equals(RegistryWord other)
        {
            return Word.Equals(other.Word, StringComparison.OrdinalIgnoreCase) && Homonym == other.Homonym;
        }


    }

}
