using AutoShop.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoShop.Test.ValueObjects
{
    [TestClass]
    public class TelefoneTest
    {

        [TestMethod]
        [DataTestMethod]
        [DataRow("(68) 70127-3739")]
        [DataRow("682222222")]
        [DataRow("68202222222")]
        public void ShouldReturnErrorWhenTelefoneIsInvalid(string telefone)
        {
            var telefoneVo = new Telefone(telefone);
            Assert.IsFalse(telefoneVo.IsValid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenTelefoneIsNull()
        {
            var telefone = new Telefone(null);
            Assert.IsFalse(telefone.IsValid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("4827886506")]
        [DataRow("48981371600")]
        [DataRow("6929862104")]
        public void ShouldReturnSucessWhenTelefoneIsValid(string telefone)
        {
            var telefoneVo = new Telefone(telefone);
            Assert.IsTrue(telefoneVo.IsValid);
        }
    }
}
