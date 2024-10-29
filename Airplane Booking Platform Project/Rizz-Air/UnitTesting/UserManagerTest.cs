using DomainLibrary.Domains;
using LogicLibrary.Managers;
using System.Security.Cryptography.X509Certificates;
using DomainLibrary.Domains;
using DomainLibrary.Enumerations;
using LogicLibrary.Managers;
using UnitTesting.FakeDALS;

namespace UnitTesting
{
    [TestClass]
    public class UserManagerTest
    {
        private FakeUserDAL createTestRepository()
        {
            return new FakeUserDAL();
        }

        private User CreateTestUser()
        {
            return new User(
                email: "testuser@example.com",
                lastName: "Doe",
                firstName: "John",
                dateOfBirth: new DateTime(2002, 7, 1),
                password: "TestPassword123",
                license: true
            );
        }

        /// <summary>
        /// Tests start from here 
        /// </summary>


        [TestMethod]
        public void AddUser()
        {
            // Arrange
            UserManager userManager = new UserManager(createTestRepository());
            User newUser = CreateTestUser();

            // Act
            bool result = userManager.AddUser(newUser);
              
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteUser()
        {
            // Arrange
            UserManager userManager = new UserManager(createTestRepository());
            User newUser = CreateTestUser();
            userManager.AddUser(newUser);

            // Act
            bool result = userManager.DeleteUser(newUser.UserId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Login()
        {
            // Arrange
            FakeUserDAL userDAL = new FakeUserDAL();
            User newUser = CreateTestUser();
            userDAL.AddUser(newUser); // Add the user to the repository

            // Act
            User loggedInUser = userDAL.Login(newUser.Email, "TestPassword123");

            // Assert
            Assert.IsNotNull(loggedInUser);
            Assert.AreEqual(newUser.UserId, loggedInUser.UserId);
        }

        [TestMethod]
        public void GetUserById()
        {
            // Arrange
            FakeUserDAL userDAL = new FakeUserDAL();
            User newUser = CreateTestUser();
            userDAL.AddUser(newUser); // Add the user to the repository

            // Act
            User foundUser = userDAL.GetUserById(newUser.UserId);

            // Assert
            Assert.IsNotNull(foundUser);
            Assert.AreEqual(newUser.Email, foundUser.Email);
        }

        [TestMethod]
        public void GetUserByEmail()
        {
            // Arrange
            FakeUserDAL userDAL = new FakeUserDAL();
            User newUser = CreateTestUser();
            userDAL.AddUser(newUser); // Add the user to the repository

            // Act
            User foundUser = userDAL.GetUserByEmail(newUser.Email);

            // Assert
            Assert.IsNotNull(foundUser);
            Assert.AreEqual(newUser.UserId, foundUser.UserId);
        }

        [TestMethod]
        public void GetAllUsers()
        {
            // Arrange
            FakeUserDAL userDAL = new FakeUserDAL();
            User newUser = CreateTestUser();
            userDAL.AddUser(newUser); // Add the user to the repository

            // Act
            List<User> users = userDAL.GetAllUsers();

            // Assert
            Assert.IsNotNull(users);
            Assert.AreEqual(1, users.Count);
        }
    }
}