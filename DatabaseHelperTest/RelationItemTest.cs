using DatabaseHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DatabaseHelperTest
{
    
    
    /// <summary>
    ///This is a test class for RelationItemTest and is intended
    ///to contain all RelationItemTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RelationItemTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for RelationItem Constructor
        ///</summary>
        [TestMethod()]
        public void RelationItemConstructorTest()
        {
            RelationItem target = new RelationItem();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for CreateRelationItem
        ///</summary>
        [TestMethod()]
        public void CreateRelationItemTest()
        {
            int parentId = 0; // TODO: Initialize to an appropriate value
            int childId = 0; // TODO: Initialize to an appropriate value
            bool isAuto = false; // TODO: Initialize to an appropriate value
            RelationItem expected = null; // TODO: Initialize to an appropriate value
            RelationItem actual;
            actual = RelationItem.CreateRelationItem(parentId, childId, isAuto, (short)RelationType.Normal, 0);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ChildId
        ///</summary>
        [TestMethod()]
        public void ChildIdTest()
        {
            RelationItem target = new RelationItem(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.ChildId = expected;
            actual = target.ChildId;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsAuto
        ///</summary>
        [TestMethod()]
        public void IsAutoTest()
        {
            RelationItem target = new RelationItem(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            target.IsAuto = expected;
            actual = target.IsAuto;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OriginalRelationType
        ///</summary>
        [TestMethod()]
        public void RelationTypeGroupTest()
        {
            RelationItem target = new RelationItem(); // TODO: Initialize to an appropriate value
            Nullable<short> expected = new Nullable<short>(); // TODO: Initialize to an appropriate value
            Nullable<short> actual;
            target.RelationTypeGroup = expected;
            actual = target.RelationTypeGroup;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ParentId
        ///</summary>
        [TestMethod()]
        public void ParentIdTest()
        {
            RelationItem target = new RelationItem(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.ParentId = expected;
            actual = target.ParentId;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for RelationType
        ///</summary>
        [TestMethod()]
        public void RelationTypeTest()
        {
            RelationItem target = new RelationItem(); // TODO: Initialize to an appropriate value
            short expected = 0; // TODO: Initialize to an appropriate value
            short actual;
            target.RelationType = expected;
            actual = target.RelationType;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
