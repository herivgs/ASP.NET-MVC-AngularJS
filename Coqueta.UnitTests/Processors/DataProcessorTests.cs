using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coqueta.UnitTests
{
    using BusinessInterfaces;
    using DataInterfaces;
    using BusinessLayer;
    using Types;

    [TestClass]
    public class DataProcessorTests
    {
        User _userGood;
        User _userBad;
        IEnumerable<User> _users;

        [TestInitialize]
        public void Initialize()
        {
            _users = Enumerable.Range(1, 3).Select(e => new User
            {
                Username = $"herivgs{e}",
                Email = "herivgs@hotmail.co",
                Password = "password",
                ConfirmationPassword = "password"
            });

            _userGood = new User() {
                Username = "herivgs", 
                Email = "herivgs@hotmail.co",
                Password = "password",
                ConfirmationPassword = "password"
            };

            _userBad = new User()
            {
                Username = "herivgs",
                Email = "herivgs@hotmail.co",
                Password = "password",
                ConfirmationPassword = "password2"
            };
        }


        [TestMethod]
        public async Task ProcessorGetAllShouldSucceed()
        {
            // Arrange
            var mockDataRepository = new Mock<IDataRepository>();
            mockDataRepository
                .Setup(r => r.RetrieveAll())
                .ReturnsAsync(_users.ToArray());

            // Act
            var process = (IDataProcessor)new DataProcessor(mockDataRepository.Object);
            var actual = await process.GetAll();

            // Assert
            Assert.IsTrue(actual.Any());
            mockDataRepository.Verify(r => r.RetrieveAll(), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task ProcessorGetAllShouldFail()
        {
            // Arrange
            var mockDataRepository = new Mock<IDataRepository>();
            mockDataRepository
                .Setup(r => r.RetrieveAll())
                .Throws(new InvalidOperationException());

            // Act
            var process = (IDataProcessor)new DataProcessor(mockDataRepository.Object);
            var actual = await process.GetAll();

            // Assert
            Assert.IsTrue(actual.Any());
            mockDataRepository.Verify(r => r.RetrieveAll(), Times.Once);
        }

        [TestMethod]
        public async Task GetByIdShouldSucceed()
        {
            // Arrange
            var DATAID = 1;
            var mockDataRepository = new Mock<IDataRepository>();
            mockDataRepository
                .Setup(r => r.RetrieveById(It.IsAny<int>()))
                .ReturnsAsync(_userGood);

            // Act
            var process = (IDataProcessor)new DataProcessor(mockDataRepository.Object);
            var actual = await process.GetById(DATAID);

            // Asert
            mockDataRepository.Verify(r => r.RetrieveById(It.IsAny<int>()));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task GetByIdShouldFail()
        {
            // Arrange
            var DATAID = 1;
            var mockDataRepository = new Mock<IDataRepository>();
            mockDataRepository
                .Setup(r => r.RetrieveById(It.IsAny<int>()))
                .Throws(new InvalidOperationException());

            try
            {
                // Act
                var process = (IDataProcessor)new DataProcessor(mockDataRepository.Object);
                await process.GetById(DATAID);
            }
            catch (InvalidOperationException)
            {
                // Asert
                mockDataRepository.Verify(r => r.RetrieveById(It.IsAny<int>()));
                throw;
            }


        }

        [TestMethod]
        public async Task AddUserShouldSucceed()
        {
            // Arrange
            var mockDataRepository = new Mock<IDataRepository>();

            //Act
            var process = (IDataProcessor)new DataProcessor(mockDataRepository.Object);
            await process.AddUser(_userGood);
            
            //Assert
            mockDataRepository.Verify(r => r.Save(_userGood));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task AddUserShouldFail()
        {
            // Arrange
            var mockDataRepository = new Mock<IDataRepository>();
            mockDataRepository
                .Setup(r => r.Save(It.IsAny<User>()))
                .Throws(new InvalidOperationException());
            try
            {
                //Act
                var process = (IDataProcessor)new DataProcessor(mockDataRepository.Object);
                await process.AddUser(_userGood);
            }
            catch (InvalidOperationException)
            {
                //Assert
                mockDataRepository.Verify(r => r.IsUserRegistered(_userGood.Email));
                mockDataRepository.Verify(r => r.Save(_userGood));
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(UserExistsExeption))]
        public async Task AddUserReturnUserExist()
        {
            // Arrange
            var mockDataRepository = new Mock<IDataRepository>();
            mockDataRepository
                .Setup(r => r.IsUserRegistered(It.IsAny<string>()))
                .ReturnsAsync(true);
            
            // Act
            var process = (IDataProcessor)new DataProcessor(mockDataRepository.Object);
            await process.AddUser(_userGood);
            
            // Assert
            mockDataRepository.Verify(r => r.Save(_userGood));
            mockDataRepository.Verify(r => r.IsUserRegistered(_userGood.Email));
        }

        [TestMethod]
        public async Task RemoveUserShouldSucceed()
        {
            // Arrange
            var mockDataRepository = new Mock<IDataRepository>();

            // Act
            var process = (IDataProcessor)new DataProcessor(mockDataRepository.Object);
            await process.RemoveUser(It.IsAny<int>());

            // Assert
            mockDataRepository.Verify(r => r.Delete(It.IsAny<int>()));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task RemoveUserShouldFail()
        {
            // Arrange
            var mockDataRepository = new Mock<IDataRepository>();
            mockDataRepository
                .Setup(r => r.Delete(It.IsAny<int>()))
                .Throws(new InvalidOperationException());
            try
            {
                //Act
                var process = (IDataProcessor)new DataProcessor(mockDataRepository.Object);
                await process.RemoveUser(It.IsAny<int>());
            }
            catch (InvalidOperationException)
            {
                //Assert
                mockDataRepository.Verify(r => r.Delete(It.IsAny<int>()));
                throw;
            }
        }

        [TestMethod]
        public async Task UpdateUserShouldSucceed()
        {
            // Arrange
            var mockDataRepository = new Mock<IDataRepository>();

            // Act
            var process = (IDataProcessor)new DataProcessor(mockDataRepository.Object);
            await process.UpdateUser(_userGood);

            // Assert
            mockDataRepository.Verify(r => r.Update(_userGood));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task UpdateUserShouldFail()
        {
            // Arrange
            var mockDataRepository = new Mock<IDataRepository>();
            mockDataRepository
                .Setup(r => r.Update(It.IsAny<User>()))
                .Throws(new InvalidOperationException());
            try
            {
                //Act
                var process = (IDataProcessor)new DataProcessor(mockDataRepository.Object);
                await process.UpdateUser(_userGood);
            }
            catch (InvalidOperationException)
            {
                //Assert
                mockDataRepository.Verify(r => r.Update(_userGood));
                throw;
            }
        }
    }
}
