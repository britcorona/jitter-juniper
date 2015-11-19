﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jitter.Models; //nov_19 branch

namespace Jitter.Tests.Models
{
    [TestClass]
    public class JitterUserTests
    {
        [TestMethod]
        public void JitterUserEnsureICanCreateInstance() //nov_19 branch
        {
            JitterUser a_user = new JitterUser();
            Assert.IsNotNull(a_user);
        }

        [TestMethod]
        public void JitterUserEnsureJitterUserHasAllTheThings()
        {
            JitterUser a_user = new JitterUser();

            a_user.Handle = "adam1";
            a_user.FirstName = "Adam";
            a_user.LastName = "Sandler";
            a_user.Picture = "https://google.com";
            a_user.Description = "I am Awesome!";

            Assert.AreEqual("adam1", a_user.Handle);
            Assert.AreEqual("Adam", a_user.FirstName);
            Assert.AreEqual("Sandler", a_user.LastName);
            Assert.AreEqual("https://google.com", a_user.Picture);
            Assert.AreEqual("I am Awesome!", a_user.Description);
        }
    }
}
