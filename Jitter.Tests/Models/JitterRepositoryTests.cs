﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jitter.Models; //nov_20 branch
using System.Collections.Generic;
using Moq;
using System.Data.Entity; //Access to DbSet

namespace Jitter.Tests.Models
{
    [TestClass]
    public class JitterRepositoryTests
    {
        [TestMethod]
        public void JitterContextEnsureICanCreateInstance()
        {
            JitterContext context = new JitterContext();
            Assert.IsNotNull(context);
        }

        [TestMethod]
        public void JitterRepositoryEnsureICanCreateInstance()
        {
            JitterRepository repository = new JitterRepository();
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void JItterRepositoryEnsureICanGetAllUsers()
        {
            var expected = new List<JitterUser>
            {
                new JitterUser {Handle = "adam1" },
                new JitterUser {Handle = "rumbadancer2" }
            };
            Mock<JitterContext> mock_context = new Mock<JitterContext>();
            Mock<DbSet<JitterUser>> mock_set = new Mock<DbSet<JitterUser>>();

            mock_set.Object.AddRange(expected);

            //This is Stubbing the JitterUsers property getter
            mock_context.Setup(a => a.JitterUsers).Returns(mock_set.Object);
            JitterRepository repository = new JitterRepository(mock_context.Object);
            var actual = repository.GetAllUsers(); //Call one method and it give me everything I want.
            //Assert.AreEqual("adam1", actual.First().Handle);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void JitterRepositoryEnsureIHaveAContext()
        {
            JitterRepository repository = new JitterRepository();
            var actual = repository.Context;
            Assert.IsInstanceOfType(actual, typeof(JitterContext));
        }
    }
}
