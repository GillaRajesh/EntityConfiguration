using EntityConfiguration.Controllers;
using EntityConfigurationBLL;
using NSubstitute;
using NUnit.Framework;
using System.Net;
using System.Net.Http;

namespace EntityConfiguration.Tests.Controllers
{
    [TestFixture]
    public class EntityConfigControllerTest
    {
        IEntityBLL entityBLL;


        [SetUp]
        public void MockSetup()
        {
            entityBLL = Substitute.For<IEntityBLL>();           
        }

        [Test]
        public void GetFields_Default_Test()
        {
            string info = "[{\"EntityName\":\"Product\",\"Fields\":[{\"FieldName\":\"Field1\",\"IsRequired\":true,\"MaxLength\":10,\"IsCustom\":false},{\"FieldName\":\"Field2\",\"IsRequired\":false,\"MaxLength\":15,\"IsCustom\":false},{\"FieldName\":\"Field3\",\"IsRequired\":true,\"MaxLength\":10,\"IsCustom\":false}]}]";
            // Arrange           
            entityBLL.GetFields("Product", true).ReturnsForAnyArgs(info);

            // Act
            EntityConfigController controller = new EntityConfigController(entityBLL);
            HttpResponseMessage httpResponseMessage = controller.GetFields("Product",true);

            // Assert
            Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void GetFields_Custom_Test()
        {
            string info = "[{\"EntityName\":\"Product\",\"Fields\":[{\"FieldName\":\"Field1\",\"IsRequired\":true,\"MaxLength\":10,\"IsCustom\":false},{\"FieldName\":\"Field2\",\"IsRequired\":false,\"MaxLength\":15,\"IsCustom\":false},{\"FieldName\":\"Field3\",\"IsRequired\":true,\"MaxLength\":10,\"IsCustom\":false}]}]";
            // Arrange           
            entityBLL.GetFields("Product", true).ReturnsForAnyArgs(info);

            // Act
            EntityConfigController controller = new EntityConfigController(entityBLL);
            HttpResponseMessage httpResponseMessage = controller.GetFields("Product", false);

            // Assert
            Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void GetDefaultFields_Empty_Test()
        {
           
            // Arrange           
            
            // Act
            EntityConfigController controller = new EntityConfigController();
            HttpResponseMessage httpResponseMessage = controller.GetDefaultFields("");

            // Assert
            Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }


        [Test]
        public void GetCustomFields_Empty_Test()
        {

            // Arrange           

            // Act
            EntityConfigController controller = new EntityConfigController();
            HttpResponseMessage httpResponseMessage = controller.GetCustomFields("");

            // Assert
            Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public void GetFields_InvalidEntity_Test()
        {
           string info = "";
            // Arrange           
            entityBLL.GetFields("Product1", true).ReturnsForAnyArgs(info);

            // Act
            EntityConfigController controller = new EntityConfigController(entityBLL);
            HttpResponseMessage httpResponseMessage = controller.GetFields("Product1", true);

            // Assert
            Assert.That(httpResponseMessage.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
