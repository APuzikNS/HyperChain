﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
namespace DatabaseHelper
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class SemanticDBEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new SemanticDBEntities object using the connection string found in the 'SemanticDBEntities' section of the application configuration file.
        /// </summary>
        public SemanticDBEntities() : base("name=SemanticDBEntities", "SemanticDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new SemanticDBEntities object.
        /// </summary>
        public SemanticDBEntities(string connectionString) : base(connectionString, "SemanticDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new SemanticDBEntities object.
        /// </summary>
        public SemanticDBEntities(EntityConnection connection) : base(connection, "SemanticDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<EditorChoiceItem> tbl_EditorChoiceItems
        {
            get
            {
                if ((_tbl_EditorChoiceItems == null))
                {
                    _tbl_EditorChoiceItems = base.CreateObjectSet<EditorChoiceItem>("tbl_EditorChoiceItems");
                }
                return _tbl_EditorChoiceItems;
            }
        }
        private ObjectSet<EditorChoiceItem> _tbl_EditorChoiceItems;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<RelationItem> tbl_RelationItems
        {
            get
            {
                if ((_tbl_RelationItems == null))
                {
                    _tbl_RelationItems = base.CreateObjectSet<RelationItem>("tbl_RelationItems");
                }
                return _tbl_RelationItems;
            }
        }
        private ObjectSet<RelationItem> _tbl_RelationItems;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<WordItem> tbl_WordItems
        {
            get
            {
                if ((_tbl_WordItems == null))
                {
                    _tbl_WordItems = base.CreateObjectSet<WordItem>("tbl_WordItems");
                }
                return _tbl_WordItems;
            }
        }
        private ObjectSet<WordItem> _tbl_WordItems;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the tbl_EditorChoiceItems EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddTotbl_EditorChoiceItems(EditorChoiceItem editorChoiceItem)
        {
            base.AddObject("tbl_EditorChoiceItems", editorChoiceItem);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the tbl_RelationItems EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddTotbl_RelationItems(RelationItem relationItem)
        {
            base.AddObject("tbl_RelationItems", relationItem);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the tbl_WordItems EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddTotbl_WordItems(WordItem wordItem)
        {
            base.AddObject("tbl_WordItems", wordItem);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="SemanticDBModel", Name="EditorChoiceItem")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class EditorChoiceItem : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new EditorChoiceItem object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="realtionItemId">Initial value of the RealtionItemId property.</param>
        /// <param name="editorId">Initial value of the EditorId property.</param>
        public static EditorChoiceItem CreateEditorChoiceItem(global::System.Int32 id, global::System.Int32 realtionItemId, global::System.Int32 editorId)
        {
            EditorChoiceItem editorChoiceItem = new EditorChoiceItem();
            editorChoiceItem.Id = id;
            editorChoiceItem.RealtionItemId = realtionItemId;
            editorChoiceItem.EditorId = editorId;
            return editorChoiceItem;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 RealtionItemId
        {
            get
            {
                return _RealtionItemId;
            }
            set
            {
                OnRealtionItemIdChanging(value);
                ReportPropertyChanging("RealtionItemId");
                _RealtionItemId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("RealtionItemId");
                OnRealtionItemIdChanged();
            }
        }
        private global::System.Int32 _RealtionItemId;
        partial void OnRealtionItemIdChanging(global::System.Int32 value);
        partial void OnRealtionItemIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 EditorId
        {
            get
            {
                return _EditorId;
            }
            set
            {
                OnEditorIdChanging(value);
                ReportPropertyChanging("EditorId");
                _EditorId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("EditorId");
                OnEditorIdChanged();
            }
        }
        private global::System.Int32 _EditorId;
        partial void OnEditorIdChanging(global::System.Int32 value);
        partial void OnEditorIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int16> RelationType
        {
            get
            {
                return _RelationType;
            }
            set
            {
                OnRelationTypeChanging(value);
                ReportPropertyChanging("RelationType");
                _RelationType = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("RelationType");
                OnRelationTypeChanged();
            }
        }
        private Nullable<global::System.Int16> _RelationType;
        partial void OnRelationTypeChanging(Nullable<global::System.Int16> value);
        partial void OnRelationTypeChanged();

        #endregion

    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="SemanticDBModel", Name="RelationItem")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class RelationItem : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new RelationItem object.
        /// </summary>
        /// <param name="parentId">Initial value of the ParentId property.</param>
        /// <param name="childId">Initial value of the ChildId property.</param>
        /// <param name="isAuto">Initial value of the IsAuto property.</param>
        /// <param name="relationType">Initial value of the RelationType property.</param>
        /// <param name="id">Initial value of the Id property.</param>
        public static RelationItem CreateRelationItem(global::System.Int32 parentId, global::System.Int32 childId, global::System.Boolean isAuto, global::System.Int16 relationType, global::System.Int32 id)
        {
            RelationItem relationItem = new RelationItem();
            relationItem.ParentId = parentId;
            relationItem.ChildId = childId;
            relationItem.IsAuto = isAuto;
            relationItem.RelationType = relationType;
            relationItem.Id = id;
            return relationItem;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ParentId
        {
            get
            {
                return _ParentId;
            }
            set
            {
                if (_ParentId != value)
                {
                    OnParentIdChanging(value);
                    ReportPropertyChanging("ParentId");
                    _ParentId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ParentId");
                    OnParentIdChanged();
                }
            }
        }
        private global::System.Int32 _ParentId;
        partial void OnParentIdChanging(global::System.Int32 value);
        partial void OnParentIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ChildId
        {
            get
            {
                return _ChildId;
            }
            set
            {
                if (_ChildId != value)
                {
                    OnChildIdChanging(value);
                    ReportPropertyChanging("ChildId");
                    _ChildId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ChildId");
                    OnChildIdChanged();
                }
            }
        }
        private global::System.Int32 _ChildId;
        partial void OnChildIdChanging(global::System.Int32 value);
        partial void OnChildIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Boolean IsAuto
        {
            get
            {
                return _IsAuto;
            }
            set
            {
                OnIsAutoChanging(value);
                ReportPropertyChanging("IsAuto");
                _IsAuto = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("IsAuto");
                OnIsAutoChanged();
            }
        }
        private global::System.Boolean _IsAuto;
        partial void OnIsAutoChanging(global::System.Boolean value);
        partial void OnIsAutoChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int16 RelationType
        {
            get
            {
                return _RelationType;
            }
            set
            {
                OnRelationTypeChanging(value);
                ReportPropertyChanging("RelationType");
                _RelationType = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("RelationType");
                OnRelationTypeChanged();
            }
        }
        private global::System.Int16 _RelationType;
        partial void OnRelationTypeChanging(global::System.Int16 value);
        partial void OnRelationTypeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int16> RelationTypeGroup
        {
            get
            {
                return _RelationTypeGroup;
            }
            set
            {
                OnRelationTypeGroupChanging(value);
                ReportPropertyChanging("RelationTypeGroup");
                _RelationTypeGroup = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("RelationTypeGroup");
                OnRelationTypeGroupChanged();
            }
        }
        private Nullable<global::System.Int16> _RelationTypeGroup;
        partial void OnRelationTypeGroupChanging(Nullable<global::System.Int16> value);
        partial void OnRelationTypeGroupChanged();

        #endregion

    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="SemanticDBModel", Name="WordItem")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class WordItem : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new WordItem object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="word">Initial value of the Word property.</param>
        /// <param name="status">Initial value of the Status property.</param>
        /// <param name="inMainDB">Initial value of the InMainDB property.</param>
        public static WordItem CreateWordItem(global::System.Int32 id, global::System.String word, global::System.Int16 status, global::System.Boolean inMainDB)
        {
            WordItem wordItem = new WordItem();
            wordItem.Id = id;
            wordItem.Word = word;
            wordItem.Status = status;
            wordItem.InMainDB = inMainDB;
            return wordItem;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Word
        {
            get
            {
                return _Word;
            }
            set
            {
                if (_Word != value)
                {
                    OnWordChanging(value);
                    ReportPropertyChanging("Word");
                    _Word = StructuralObject.SetValidValue(value, false);
                    ReportPropertyChanged("Word");
                    OnWordChanged();
                }
            }
        }
        private global::System.String _Word;
        partial void OnWordChanging(global::System.String value);
        partial void OnWordChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int16 Status
        {
            get
            {
                return _Status;
            }
            set
            {
                OnStatusChanging(value);
                ReportPropertyChanging("Status");
                _Status = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Status");
                OnStatusChanged();
            }
        }
        private global::System.Int16 _Status;
        partial void OnStatusChanging(global::System.Int16 value);
        partial void OnStatusChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Boolean InMainDB
        {
            get
            {
                return _InMainDB;
            }
            set
            {
                OnInMainDBChanging(value);
                ReportPropertyChanging("InMainDB");
                _InMainDB = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("InMainDB");
                OnInMainDBChanged();
            }
        }
        private global::System.Boolean _InMainDB;
        partial void OnInMainDBChanging(global::System.Boolean value);
        partial void OnInMainDBChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Boolean> Reviewed
        {
            get
            {
                return _Reviewed;
            }
            set
            {
                OnReviewedChanging(value);
                ReportPropertyChanging("Reviewed");
                _Reviewed = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Reviewed");
                OnReviewedChanged();
            }
        }
        private Nullable<global::System.Boolean> _Reviewed;
        partial void OnReviewedChanging(Nullable<global::System.Boolean> value);
        partial void OnReviewedChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int16> Homonym
        {
            get
            {
                return _Homonym;
            }
            set
            {
                OnHomonymChanging(value);
                ReportPropertyChanging("Homonym");
                _Homonym = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Homonym");
                OnHomonymChanged();
            }
        }
        private Nullable<global::System.Int16> _Homonym;
        partial void OnHomonymChanging(Nullable<global::System.Int16> value);
        partial void OnHomonymChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> StatusLastTimeUpdate
        {
            get
            {
                return _StatusLastTimeUpdate;
            }
            set
            {
                OnStatusLastTimeUpdateChanging(value);
                ReportPropertyChanging("StatusLastTimeUpdate");
                _StatusLastTimeUpdate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("StatusLastTimeUpdate");
                OnStatusLastTimeUpdateChanged();
            }
        }
        private Nullable<global::System.DateTime> _StatusLastTimeUpdate;
        partial void OnStatusLastTimeUpdateChanging(Nullable<global::System.DateTime> value);
        partial void OnStatusLastTimeUpdateChanged();

        #endregion

    
    }

    #endregion

    
}
