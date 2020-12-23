//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Web.Mvc;
//using Elearning;
//using Elearning.Data;
//using Elearning.Controllers;

//namespace Elearning.Tests
//{
//    [TestClass]
//    public class UsersControllerTests
//    {
//        private ElearningDbContext db;
//        private UsersController usersController;
//        private ViewResult result;

//        [TestInitialize]
//        public async System.Threading.Tasks.Task SetupContextAsync()
//        {
//            db = new ElearningDbContext();
//            usersController = new UsersController();
//            result = await usersController.Index() as ViewResult;
//        }

//        [TestMethod]
//        public async System.Threading.Tasks.Task IndexAsync()
//        {
//            // Arrange
//            UsersController usersController = new UsersController();

//            // Act
//            ViewResult result = await usersController.Index() as ViewResult;

//            // Assert
//            Assert.IsNotNull(result);
//        }

//        [TestMethod]
//        public async System.Threading.Tasks.Task IndexViewEqualIndexCshtmlAsync()
//        {
//            UsersController usersController = new UsersController();

//            ViewResult result = await usersController.Index() as ViewResult;

//            Assert.AreEqual("Index", result.ViewName);
//        }

//        [TestMethod]
//        public async System.Threading.Tasks.Task IndexStringInViewbagAsync()
//        {
//            UsersController usersController = new UsersController();

//            ViewResult result = await usersController.Index() as ViewResult;

//            Assert.AreEqual("Index", result.ViewBag.Title);
//        }

//        [TestMethod]
//        public void Details()
//        {
//            // Arrange
//            UsersController usersController = new UsersController();

//            // Act
//            ViewResult result = usersController.Details(1) as ViewResult;

//            // Assert
//            Assert.IsNotNull(result);
//        }

//        [TestMethod]
//        public void Create()
//        {
//            // Arrange
//            UsersController usersController = new UsersController();

//            // Act
//            ViewResult result = usersController.Create() as ViewResult;

//            // Assert
//            Assert.IsNotNull(result);
//        }

//        [TestMethod]
//        public void Edit()
//        {
//            // Arrange
//            UsersController usersController = new UsersController();

//            // Act
//            ViewResult result = usersController.Edit(1) as ViewResult;

//            // Assert
//            Assert.IsNotNull(result);
//        }

//        [TestMethod]
//        public void Delete()
//        {
//            // Arrange
//            UsersController usersController = new UsersController();

//            // Act
//            ViewResult result = usersController.Delete(1) as ViewResult;

//            // Assert
//            Assert.IsNotNull(result);
//        }
//    }
//}
