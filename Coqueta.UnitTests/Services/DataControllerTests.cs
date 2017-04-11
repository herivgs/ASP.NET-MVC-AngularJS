using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace Coqueta.UnitTests.Services
{
    using BusinessInterfaces;
    using Mvc.Services;
    using Types;

    [TestClass]
    public class DataControllerTests
    {
        [TestMethod]
        public async Task ControllerGetAllShouldBeOK()
        {
            // Arrange
            var _users = Enumerable.Range(1, 3).Select(e => new User
            {
                Username = $"herivgs{e}",
                Email = "herivgs@hotmail.com",
                Password = "password",
                ConfirmationPassword = "password"
            });
            var mockDataProcessor = new Mock<IDataProcessor>();
            mockDataProcessor
                .Setup(p => p.GetAll())
                .ReturnsAsync(_users.ToArray());

            // Act
            var controller = new DataController(mockDataProcessor.Object);
            var actual = await controller.GetAll();

            // Arrange
            var result = actual as OkResult;
            mockDataProcessor.Verify(p => p.GetAll(), Times.Once);
        }

        [TestMethod]
        public async Task ControllerGetAllShouldBeNotFound()
        {
            // Arrange
            var _users = Enumerable.Range(1, 3).Select(e => new User { });
            var mockDataProcessor = new Mock<IDataProcessor>();
            mockDataProcessor
                .Setup(p => p.GetAll())
                .ReturnsAsync(_users.ToArray());
           
            // Act
            var controller = new DataController(mockDataProcessor.Object);
            var actual = await controller.GetAll();
           
            // Arrange
            var result = actual as NotFoundResult;
            mockDataProcessor.Verify(p => p.GetAll(), Times.Once);
        }

        [TestMethod]
        public async Task ControllerCreateShouldBeOK()
        {
            // Arrange
            var _user =  new User
            {
                Username = $"herivgs",
                Email = "herivgs@hotmail.com",
                Password = "password",
                ConfirmationPassword = "password"
            };
            var mockDataProcessor = new Mock<IDataProcessor>();

            // Act
            var controller = new DataController(mockDataProcessor.Object);
            var actual = await controller.Create(_user);

            // Arrange
            var result = actual as OkResult;
            mockDataProcessor.Verify(p => p.AddUser(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        public async Task ControllerCreateShouldBeBadRequest()
        {
            // Arrange
            // Arrange
            var _user = new User
            {
                Username = $"hervgs",
                Email = "herivgshotmail.com",
                Password = "password1",
                ConfirmationPassword = "1password"
            };
            var mockDataProcessor = new Mock<IDataProcessor>();
            mockDataProcessor.Setup(p => p.AddUser(It.IsAny<User>()))
                .Throws(new BusinessLayerException("Generic Error", new[] {"Test Error"}));

            // Act
            var controller = new DataController(mockDataProcessor.Object);
            var actual = await controller.Create(_user);

            // Arrange
            var result = actual as BadRequestResult;
            mockDataProcessor.Verify(p => p.AddUser(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        public async Task ControllerGetByIdShouldBeOK()
        {
            // Arrange
            var _user = new User
            {
                Username = $"herivgs",
                Email = "herivgs@hotmail.com",
                Password = "password",
                ConfirmationPassword = "password"
            };
            var mockDataProcessor = new Mock<IDataProcessor>();
            mockDataProcessor
                .Setup(p => p.GetById(It.IsAny<int>()))
                .ReturnsAsync(_user);

            // Act
            var controller = new DataController(mockDataProcessor.Object);
            var actual = await controller.GetById(It.IsAny<int>());

            // Arrange
            var result = actual as OkResult;
            mockDataProcessor.Verify(p => p.GetById(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public async Task ControllerGetByIdShouldBeNotFound()
        {
            // Arrange
            var mockDataProcessor = new Mock<IDataProcessor>();

            // Act
            var controller = new DataController(mockDataProcessor.Object);
            var actual = await controller.GetById(It.IsAny<int>());

            // Arrange
            var result = actual as NotFoundResult;
            mockDataProcessor.Verify(p => p.GetById(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public async Task ControllerGetByIdShouldBeBadRequest()
        {
            // Arrange
            var mockDataProcessor = new Mock<IDataProcessor>();
            mockDataProcessor.Setup(p => p.GetById(It.IsAny<int>()))
                .Throws(new BusinessLayerException("Generic Error", new[] { "Test Error" }));

            // Act
            var controller = new DataController(mockDataProcessor.Object);
            var actual = await controller.GetById(It.IsAny<int>());

            // Arrange
            var result = actual as BadRequestResult;
            mockDataProcessor.Verify(p => p.GetById(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public async Task ControllerDeleteShouldBeOk()
        {
            // Arrange
            var mockDataProcessor = new Mock<IDataProcessor>();

            // Act
            var controller = new DataController(mockDataProcessor.Object);
            var actual = await controller.Delete(It.IsAny<int>());

            // Arrange
            var result = actual as OkResult;
            mockDataProcessor.Verify(p => p.RemoveUser(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public async Task ControllerDeleteShouldBeBadRequest()
        {
            // Arrange
            var mockDataProcessor = new Mock<IDataProcessor>();
            mockDataProcessor.Setup(p => p.RemoveUser(It.IsAny<int>()))
                .Throws(new BusinessLayerException("Generic Error", new[] { "Test Error" }));

            // Act
            var controller = new DataController(mockDataProcessor.Object);
            var actual = await controller.Delete(It.IsAny<int>());

            // Arrange
            var result = actual as BadRequestResult;
            mockDataProcessor.Verify(p => p.RemoveUser(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public async Task ControllerEditShouldBeOK()
        {
            // Arrange
            var _user = new User
            {
                Username = $"herivgs",
                Email = "herivgs@hotmail.com",
                Password = "password",
                ConfirmationPassword = "password"
            };
            var mockDataProcessor = new Mock<IDataProcessor>();

            // Act
            var controller = new DataController(mockDataProcessor.Object);
            var actual = await controller.Edit(_user);

            // Arrange
            var result = actual as OkResult;
            mockDataProcessor.Verify(p => p.UpdateUser(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        public async Task ControllerEdithouldBeBadRequest()
        {
            // Arrange
            // Arrange
            var _user = new User
            {
                Username = $"hervgs",
                Email = "herivgshotmail.com",
                Password = "password1",
                ConfirmationPassword = "1password"
            };
            var mockDataProcessor = new Mock<IDataProcessor>();
            mockDataProcessor.Setup(p => p.UpdateUser(It.IsAny<User>()))
                .Throws(new BusinessLayerException("Generic Error", new[] { "Test Error" }));

            // Act
            var controller = new DataController(mockDataProcessor.Object);
            var actual = await controller.Edit(_user);

            // Arrange
            var result = actual as BadRequestResult;
            mockDataProcessor.Verify(p => p.UpdateUser(It.IsAny<User>()), Times.Once);
        }


    }
}
