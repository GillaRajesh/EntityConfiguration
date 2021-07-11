using EntityConfiguration.Controllers;
using NUnit.Framework;

namespace EntityConfiguration.Tests.Controllers
{
    [TestFixture]
    public class ValuesControllerTest
    {


        [Test]
        public void GetById()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            string result = controller.Get(5);

            // Assert
            Assert.AreEqual("value", result);
        }

       
    }
}
