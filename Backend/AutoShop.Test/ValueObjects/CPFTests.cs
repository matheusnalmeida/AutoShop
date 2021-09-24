using AutoShop.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoShop.Test.ValueObjects
{
    [TestClass]
    public class CPFTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenCPFIsInvalid()
        {
            var cpf = new CPF("123");
            Assert.IsFalse(cpf.IsValid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenCPFIsNull()
        {
            var cpf = new CPF(null);
            Assert.IsFalse(cpf.IsValid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("547.439.970-70")]
        [DataRow("83805688008")]
        [DataRow("182.982.600-07")]
        public void ShouldReturnSucessWhenCPFIsValid(string cpf)
        {
            var cpfObj = new CPF(cpf);
            Assert.IsTrue(cpfObj.IsValid);
        }
    }
}
