using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zen.EveCalc.Controls;
using Zen.EveCalc.Controls.Models;
using Zen.EveCalc.DataModel;

namespace Zen.EveCalc.Tests.Core
{
    [TestClass]
    public class BlueprintTests
    {
        [TestMethod]
        public void BasicTest()
        {
            var material = new Material()
                {
                    Name = "M1",
                    Price = 0.5f,
                    Ammount = 2
                };
            var material1 = new Material()
                {
                    Name = "M2",
                    Price = 1f,
                    Ammount = 1
                };

            var bpo = new Blueprint()
                {
                    SellPrice = 4,
                    Runs = 10
                };
            bpo.Materials = new Materials(new List<Material>()
                {
                    material,
                    material1
                }, bpo);

            Assert.AreEqual(bpo.ItemProductionPrice, 2, "Неверно вычисляется цена запуска");
            Assert.AreEqual(bpo.TotalPrice, 2*10, "Неверно вычисляется общая цена");
            Assert.AreEqual(bpo.Income, 4*10 - 2*10, "Неверно вычисляется доход");
        }
    }
}