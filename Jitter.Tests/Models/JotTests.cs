using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jitter.Models;

namespace Jitter.Tests.Models
{
    [TestClass]
    public class JotTests
    {
        [TestMethod]
        public void JotEnsureICanCreateInstance() //Nov_18 branch
        {
            //bootstraping test.
            Jot a_jot = new Jot(); //Jot Class is located in the Models folder under the Jitter file.
            //To use the Jot Class, put "using Jitter.Models" at the top. It means it has looked at the Jitter file and then went to the Models folder.
            Assert.IsNotNull(a_jot);
        }

        [TestMethod]
        public void JotEnsureJotHasAllTheThings() //Nov_18 branch
        {
            //Arrange
            Jot a_jot = new Jot();

            //Act
            DateTime expected_time = DateTime.Now; //Have to create a Variable for the datetime, and set the a_jot.Date to expected_time

            a_jot.JotId = 1; //To make the red squillies go away for JotId, I selected the already give option and it went into the Jot file under the Models folder in the Jitter file.
            a_jot.Content = "My Content";
            a_jot.Date = expected_time;
            a_jot.Author = null; //Will need to define this later
            a_jot.Picture = "https://google.com";

            //Assert
            Assert.AreEqual(1, a_jot.JotId);
            Assert.AreEqual("My Content", a_jot.Content);
            Assert.AreEqual(expected_time, a_jot.Date);
            Assert.AreEqual(null, a_jot.Author);
            Assert.AreEqual("https://google.com", a_jot.Picture);
        }

        [TestMethod]
        public void JotEnsureICanUseObjectInitializerSyntax() //Nov_18 branch
        {
            //Arrange
            DateTime expected_time = DateTime.Now;

            //Act
            Jot a_jot = new Jot { JotId = 1, Content = "My Content", Date = expected_time, Author = null, Picture = "https://google.com" }; //Here we will use the setters in the Jot class

            //Assert
            Assert.AreEqual(1, a_jot.JotId);
            Assert.AreEqual("My Content", a_jot.Content);
            Assert.AreEqual(expected_time, a_jot.Date);
            Assert.AreEqual(null, a_jot.Author);
            Assert.AreEqual("https://google.com", a_jot.Picture);
        }
    }
}
