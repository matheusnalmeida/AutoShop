using AutoShop.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoShop.Test.ValueObjects
{
    [TestClass]
    public class SenhaTests
    {

        [TestMethod]
        public void ShouldReturnErrorWhenSenhaIsInvalid()
        {
            var senha = new Senha("     ");
            Assert.IsFalse(senha.IsValid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenSenhaIsNull()
        {
            var senha = new Senha(null);
            Assert.IsFalse(senha.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSucessWhenSenhaIsValid()
        {
            var senha = new Senha("12345");
            Assert.AreEqual(senha.Valor, "5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5");
        }
    }
}
