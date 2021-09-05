using AutoShop.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShop.Test.ValueObjects
{
    [TestClass]
    public class CNPJTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhencnpjIsInvalid()
        {
            var cnpj = new CNPJ("123");
            Assert.IsFalse(cnpj.IsValid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhencnpjIsNull()
        {
            var cnpj = new CNPJ(null);
            Assert.IsFalse(cnpj.IsValid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("01.743.848/0001-73")]
        [DataRow("91814739000192")]
        [DataRow("25.353.506/0001-05")]
        public void ShouldReturnSucessWhencnpjIsValid(string cnpj)
        {
            var cnpjObj = new CNPJ(cnpj);
            Assert.IsTrue(cnpjObj.IsValid);
        }
    }
}
