using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jitter.Models;
using System.Collections.Generic;
using Moq;
using System.Data.Entity;
using System.Linq;

namespace Jitter.Tests.Models
{
    [TestClass]
    public class JitterRepositoryTests
    {
        private Mock<JitterContext> mock_context;
        private Mock<DbSet<JitterUser>> mock_set;
        private JitterRepository repository;

        private void ConnectMocksToDataStore(IEnumerable<JitterUser> data_store)
        {
            var data_source = data_store.AsQueryable<JitterUser>();
            // HINT HINT: var data_source = (data_store as IEnumerable<JitterUser>).AsQueryable();
            // Convince LINQ that our Mock DbSet is a (relational) Data store.
            mock_set.As<IQueryable<JitterUser>>().Setup(data => data.Provider).Returns(data_source.Provider);
            mock_set.As<IQueryable<JitterUser>>().Setup(data => data.Expression).Returns(data_source.Expression);
            mock_set.As<IQueryable<JitterUser>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            mock_set.As<IQueryable<JitterUser>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());
            
            // This is Stubbing the JitterUsers property getter
            mock_context.Setup(a => a.JitterUsers).Returns(mock_set.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<JitterContext>();
            mock_set = new Mock<DbSet<JitterUser>>();
            repository = new JitterRepository(mock_context.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            mock_context = null;
            mock_set = null;
            repository = null;
        }

        [TestMethod]
        public void JitterContextEnsureICanCreateInstance()
        {
            JitterContext context = mock_context.Object;
            Assert.IsNotNull(context);
        }

        [TestMethod]
        public void JitterRepositoryEnsureICanCreatInstance()
        {
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void JitterRepositoryEnsureICanGetAllUsers()
        {
            // Arrange
            var expected = new List<JitterUser>
            {
                new JitterUser {Handle = "adam1" },
                new JitterUser { Handle = "rumbadancer2"}
            };
            mock_set.Object.AddRange(expected);

            ConnectMocksToDataStore(expected);

            // Act
            var actual = repository.GetAllUsers();
            // Assert

            Assert.AreEqual("adam1", actual.First().Handle);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void JitterRepositoryEnsureIHaveAContext()
        {
            // Arrange
            // Act
            var actual = repository.Context;
            // Assert
            Assert.IsInstanceOfType(actual, typeof(JitterContext));
        }

        [TestMethod]
        public void JitterRepositoryEnsureICanGetUserByHandle() //nov_24 branch
        {
            var expected = new List<JitterUser>
            {
                new JitterUser {Handle = "adam1" },
                new JitterUser { Handle = "rumbadancer2"}
            };
            mock_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            string handle = "rumbadancer2";
            JitterUser actual_user = repository.GetUserByHandle(handle);
            Assert.AreEqual("rumbadancer2", actual_user.Handle);
        }

        [TestMethod]
        public void JitterRepositoryGetUserByHandleUserDoesNotExist()
        {
            var expected = new List<JitterUser>
            {
                new JitterUser {Handle = "adam1" },
                new JitterUser { Handle = "rumbadancer2"}
            };
            mock_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            string handle = "bogus"; //What was changed from the test above
            JitterUser actual_user = repository.GetUserByHandle(handle);
            Assert.IsNull(actual_user); //What was changed from the test above
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))] //What was changed from the test above. This is saying this test will work succesfully if user by handle fails properly.
        public void JitterRepositoryGetUserByHandleFailsMulitpleUsers()
        {
            var expected = new List<JitterUser>
            {
                new JitterUser {Handle = "adam1" }, //What was changed from the test above
                new JitterUser { Handle = "adam1"} //What was changed from the test above
            };
            mock_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            string handle = "adam1"; //What was changed from the test above
            JitterUser actual_user = repository.GetUserByHandle(handle);
            //Assert.IsNull(actual_user);
        }

        [TestMethod]
        public void JitterRepositoryEnsureHandleIsAvailable()
        {
            var expected = new List<JitterUser>
            {
                new JitterUser {Handle = "adam1" },
                new JitterUser { Handle = "rumbadancer2"}
            };
            mock_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            string handle = "bogus"; //What was changed
            bool is_available = repository.IsHandleAvailable(handle); //What was changed
            Assert.IsTrue(is_available); //What was changed
        }

        [TestMethod]
        public void JitteRepositoryEnsureHandleIsNotAvailable()
        {
            var expected = new List<JitterUser>
            {
                new JitterUser {Handle = "adam1" },
                new JitterUser { Handle = "rumbadancer2"}
            };
            mock_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            string handle = "adam1"; //What was changed
            bool is_available = repository.IsHandleAvailable(handle);
            Assert.IsFalse(is_available); //What was changed
        }

        [TestMethod]
        public void JitterRepositoryEnsureHandleIsNotAvailableMultipleUsers()
        {
            var expected = new List<JitterUser>
            {
                new JitterUser {Handle = "adam1" },
                new JitterUser { Handle = "adam1"} //What was changed
            };
            mock_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            string handle = "adam1";
            bool is_available = repository.IsHandleAvailable(handle);
            Assert.IsFalse(is_available);
        }

        [TestMethod]
        public void JitterRepositoryEnsureICanSearchByHandle()
        {
            var expected = new List<JitterUser>
            {
                new JitterUser { Handle = "adam1" },
                new JitterUser { Handle = "rumbadancer2"}, //What was changed
                new JitterUser { Handle = "treehugger" }, //What was changed
                new JitterUser { Handle = "treedancer" } //What was changed
                
            };
            mock_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            string handle = "tree"; //What was changed
            List<JitterUser> expected_users = new List<JitterUser> //What was changed
            {
                new JitterUser { Handle = "treedancer" },
                new JitterUser { Handle = "treehugger" }
            };
            List<JitterUser> acutal_users = repository.SearchByHandle(handle); //What was changed
            //CollectionAssert.AreEqual(expected_users, acutal_users); //What was changed
            Assert.AreEqual(expected_users[0].Handle, acutal_users[0].Handle);
            Assert.AreEqual(expected_users[1].Handle, acutal_users[1].Handle);
        }
    }
}
