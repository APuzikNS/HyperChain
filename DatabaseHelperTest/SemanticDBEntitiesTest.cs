using DatabaseHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.EntityClient;
using System.Data.Objects;

namespace DatabaseHelperTest
{
    
    
    /// <summary>
    ///This is a test class for SemanticDBEntitiesTest and is intended
    ///to contain all SemanticDBEntitiesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SemanticDBEntitiesTest
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
        ///A test for SemanticDBEntities Constructor
        ///</summary>
        [TestMethod()]
        public void SemanticDBEntitiesConstructorTest()
        {
            EntityConnection connection = null; // TODO: Initialize to an appropriate value
            SemanticDBEntities target = new SemanticDBEntities(connection);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for SemanticDBEntities Constructor
        ///</summary>
        [TestMethod()]
        public void SemanticDBEntitiesConstructorTest1()
        {
            string connectionString = string.Empty; // TODO: Initialize to an appropriate value
            SemanticDBEntities target = new SemanticDBEntities(connectionString);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for SemanticDBEntities Constructor
        ///</summary>
        [TestMethod()]
        public void SemanticDBEntitiesConstructorTest2()
        {
            SemanticDBEntities target = new SemanticDBEntities();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for AddTotbl_Relations
        ///</summary>
        [TestMethod()]
        public void AddTotbl_RelationsTest()
        {
            SemanticDBEntities target = new SemanticDBEntities(); // TODO: Initialize to an appropriate value
            RelationItem relationItem = null; // TODO: Initialize to an appropriate value
            target.AddTotbl_RelationItems(relationItem);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddTotbl_Words
        ///</summary>
        [TestMethod()]
        public void AddTotbl_WordsTest()
        {
            SemanticDBEntities target = new SemanticDBEntities(); // TODO: Initialize to an appropriate value
            WordItem wordItem = null; // TODO: Initialize to an appropriate value
            target.AddTotbl_WordItems(wordItem);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for tbl_RelationItems
        ///</summary>
        [TestMethod()]
        public void tbl_RelationsTest()
        {
            SemanticDBEntities target = new SemanticDBEntities(); // TODO: Initialize to an appropriate value
            ObjectSet<RelationItem> actual;
            actual = target.tbl_RelationItems;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for tbl_WordItems
        ///</summary>
        [TestMethod()]
        public void tbl_WordsTest()
        {
            SemanticDBEntities target = new SemanticDBEntities(); // TODO: Initialize to an appropriate value
            ObjectSet<WordItem> actual;
            actual = target.tbl_WordItems;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
