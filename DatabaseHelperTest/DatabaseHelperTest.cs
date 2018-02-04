using DatabaseHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DatabaseHelperTest
{
    
    
    /// <summary>
    ///This is a test class for DatabaseHelperTest and is intended
    ///to contain all DatabaseHelperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DatabaseHelperTest
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
        ///A test for DatabaseHelper Constructor
        ///</summary>
        [TestMethod()]
        public void DatabaseHelperConstructorTest()
        {
            string sConnectionString = string.Empty; // TODO: Initialize to an appropriate value
            DatabaseHelper.DatabaseHelper target = new DatabaseHelper.DatabaseHelper(sConnectionString);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for AddRelation
        ///</summary>
        [TestMethod()]
        public void AddRelationTest()
        {
            string sConnectionString = string.Empty; // TODO: Initialize to an appropriate value
            DatabaseHelper.DatabaseHelper target = new DatabaseHelper.DatabaseHelper(sConnectionString); // TODO: Initialize to an appropriate value
            string sParent = string.Empty; // TODO: Initialize to an appropriate value
            string sChild = string.Empty; // TODO: Initialize to an appropriate value
            bool isTentative = false; // TODO: Initialize to an appropriate value
            RelationItem expected = null; // TODO: Initialize to an appropriate value
            RelationItem actual;
            actual = target.AddRelation(sParent, sChild, RelationType.Normal, 0);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AddWord
        ///</summary>
        [TestMethod()]
        public void AddWordTest()
        {
            string sConnectionString = string.Empty; 
            DatabaseHelper.DatabaseHelper target = new DatabaseHelper.DatabaseHelper(sConnectionString);
            string sWord = "TestWord1";
            target.RemoveWord(sWord);
            bool bSave = false; 
            
            WordItem actual;
            actual = target.AddWord(sWord, bSave);
            WordItem expected = target.GetWord(sWord);
            Assert.IsNull(expected);
            
            bSave = true;
            actual = target.AddWord(sWord, bSave);
            expected = target.GetWord("тест1");
            Assert.AreNotEqual(expected, actual);
            expected = target.GetWord(sWord);
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///A test for GetItem
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DatabaseHelper.dll")]
        public void GetItemTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            //DatabaseHelper_Accessor target = new DatabaseHelper_Accessor(param0); // TODO: Initialize to an appropriate value
            string sParent = string.Empty; // TODO: Initialize to an appropriate value
            string sChild = string.Empty; // TODO: Initialize to an appropriate value
            RelationItem expected = null; // TODO: Initialize to an appropriate value
            RelationItem actual;
            //actual = target.GetRelationItem(sParent, sChild);
            //Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetWord
        ///</summary>
        [TestMethod()]
        public void GetWordTest()
        {
            string sConnectionString = string.Empty; // TODO: Initialize to an appropriate value
            DatabaseHelper.DatabaseHelper target = new DatabaseHelper.DatabaseHelper(sConnectionString); // TODO: Initialize to an appropriate value
            string sWord = string.Empty; // TODO: Initialize to an appropriate value
            WordItem expected = null; // TODO: Initialize to an appropriate value
            WordItem actual;
            actual = target.GetWord(sWord);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetWordById
        ///</summary>
        [TestMethod()]
        public void GetWordByIdTest()
        {
            string sConnectionString = string.Empty; 
            DatabaseHelper.DatabaseHelper target = new DatabaseHelper.DatabaseHelper(sConnectionString); 

            int nId = 1; 
            WordItem expected = target.AddWord("тест1", false); 
            WordItem expected2 = target.AddWord("тест2", false); 
            
            WordItem actual;
            actual = target.GetWordById(nId);
            Assert.AreEqual(expected, actual);
            Assert.AreNotEqual(expected2, actual);
        }

        /// <summary>
        ///A test for GetWordId
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DatabaseHelper.dll")]
        public void GetWordIdTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            //DatabaseHelper_Accessor target = new DatabaseHelper_Accessor(param0); // TODO: Initialize to an appropriate value
            string sWord = string.Empty; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            //actual = target.GetWordId(sWord);
            //Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SetStatus
        ///</summary>
        [TestMethod()]
        public void SetStatusTest()
        {
            string sConnectionString = string.Empty; // TODO: Initialize to an appropriate value
            DatabaseHelper.DatabaseHelper target = new DatabaseHelper.DatabaseHelper(sConnectionString); // TODO: Initialize to an appropriate value
            string sWord = "test4"; 

            WordItem expected = new WordItem(); 
            expected.Status = (short)WordStatus.InProgress;
            expected.Reviewed = false;
            expected.InMainDB = true;
            DateTime now= DateTime.UtcNow;
            expected.StatusLastTimeUpdate = now;
            expected.Word = "test4";
            WordItem actual;
            actual = target.SetStatus(sWord, (WordStatus)expected.Status, (short)0, false);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateRelation
        ///</summary>
        [TestMethod()]
        public void UpdateRelationTest()
        {
            string sConnectionString = string.Empty; // TODO: Initialize to an appropriate value
            DatabaseHelper.DatabaseHelper target = new DatabaseHelper.DatabaseHelper(sConnectionString); // TODO: Initialize to an appropriate value
            string sWord = string.Empty; // TODO: Initialize to an appropriate value
            string sChild = string.Empty; // TODO: Initialize to an appropriate value
            target.UpdateRelation(sWord, sChild);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for UpdateWord
        ///</summary>
        [TestMethod()]
        public void UpdateWordTest()
        {
            string sConnectionString = string.Empty; // TODO: Initialize to an appropriate value
            DatabaseHelper.DatabaseHelper target = new DatabaseHelper.DatabaseHelper(sConnectionString); // TODO: Initialize to an appropriate value
            string sWord = string.Empty; // TODO: Initialize to an appropriate value
            target.UpdateWord(sWord);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
