using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Elearning;
using Elearning.Controllers;

namespace Elearning.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController homeController = new HomeController();

            // Act
            ViewResult result = homeController.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController homeController = new HomeController();

            // Act
            ViewResult result = homeController.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController homeController = new HomeController();

            // Act
            ViewResult result = homeController.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
