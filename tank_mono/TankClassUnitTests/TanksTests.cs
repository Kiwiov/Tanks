using System;
using tank_mono;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace TankClassUnitTests
{
    public class TanksTests
    {
        [TestClass]
        public class TankTest
        {
            [TestMethod]
            public void ConstuctorTest1()
            {
                //Arrange
                var tank = new Tank(new Vector2(1, 1), "Heavy", Color.OliveDrab, false);

                //Act

                //Assert
                Assert.AreEqual(new Vector2(1, 1), tank.Position);
            }

            [TestMethod]
            public void ConstuctorTest2()
            {
                //Arrange
                var tank = new Tank(new Vector2(1, 1), "Heavy", Color.OliveDrab, false);

                //Act

                //Assert
                Assert.AreEqual("Heavy", tank.TankType);
            }

            [TestMethod]
            public void ConstuctorTest3()
            {
                //Arrange
                var tank = new Tank(new Vector2(1, 1), "Heavy", Color.OliveDrab, false);

                //Act

                //Assert
                Assert.AreEqual(Color.OliveDrab, tank.Colour);
            }

            [TestMethod]
            public void ConstuctorTest4()
            {
                //Arrange
                var tank = new Tank(new Vector2(1, 1), "Heavy", Color.OliveDrab, false);

                //Act

                //Assert
                Assert.AreEqual(false, tank.IsBot);
            }
        }

        [TestClass]
        public class TankManagerTests
        {
            [TestMethod]
            public void CrateTankTest1()
            {
                //Arrange
                var tankManager = new TankManager(null,null,null,null,null,null,null);
                
                //Act
                tankManager.CreateTank(new Vector2(1, 1), "Heavy", Color.OliveDrab, false);

                //Assert
                Assert.IsNotNull(tankManager.Tanks);
            }

            [TestMethod]
            public void CrateTankTest2()
            {
                //Arrange
                var tankManager = new TankManager(null, null, null, null, null, null,null);

                //Act
                tankManager.CreateTank(new Vector2(1, 1), "Heavy", Color.OliveDrab, false);

                //Assert
                Assert.AreEqual(new Vector2(1,1),tankManager.Tanks[0].Position);
            }

            [TestMethod]
            public void CrateTankTest3()
            {
                //Arrange
                var tankManager = new TankManager(null, null, null, null, null, null,null);

                //Act
                tankManager.CreateTank(new Vector2(1, 1), "Heavy", Color.OliveDrab, false);

                //Assert
                Assert.AreEqual("Heavy", tankManager.Tanks[0].TankType);
            }

            [TestMethod]
            public void CrateTankTest4()
            {
                //Arrange
                var tankManager = new TankManager(null, null, null, null, null, null,null);

                //Act
                tankManager.CreateTank(new Vector2(1, 1), "Heavy", Color.OliveDrab, false);

                //Assert
                Assert.AreEqual(Color.OliveDrab, tankManager.Tanks[0].Colour);
            }

            [TestMethod]
            public void CrateTankTest5()
            {
                //Arrange
                var tankManager = new TankManager(null, null, null, null, null, null,null);

                //Act
                tankManager.CreateTank(new Vector2(1, 1), "Heavy", Color.OliveDrab, false);

                //Assert
                Assert.AreEqual(false, tankManager.Tanks[0].IsBot);
            }

            [TestMethod]
            public void SetStatsTestHeavy()
            {
                //Arrange
                var tankManager = new TankManager(null, null, null, null, null, null, null);

                //Act
                tankManager.CreateTank(new Vector2(1, 1), "Heavy", Color.OliveDrab, false);
                tankManager.SetStats();

                //Assert
                Assert.AreEqual(200, tankManager.Tanks[0].Health);
                Assert.AreEqual(20, tankManager.Tanks[0].Speed);
                Assert.AreEqual(400, tankManager.Tanks[0].Fuel);
                Assert.AreEqual(300, tankManager.Tanks[0].Armour);
            }

            [TestMethod]
            public void SetStatsTestStandard()
            {
                //Arrange
                var tankManager = new TankManager(null, null, null, null, null, null, null);

                //Act
                tankManager.CreateTank(new Vector2(1, 1), "Standard", Color.OliveDrab, false);
                tankManager.SetStats();

                //Assert
                Assert.AreEqual(150, tankManager.Tanks[0].Health);
                Assert.AreEqual(40, tankManager.Tanks[0].Speed);
                Assert.AreEqual(400, tankManager.Tanks[0].Fuel);
                Assert.AreEqual(150, tankManager.Tanks[0].Armour);
            }

            [TestMethod]
            public void SetStatsTestLight()
            {
                //Arrange
                var tankManager = new TankManager(null, null, null, null, null, null, null);

                //Act
                tankManager.CreateTank(new Vector2(1, 1), "Light", Color.OliveDrab, false);
                tankManager.SetStats();

                //Assert
                Assert.AreEqual(100, tankManager.Tanks[0].Health);
                Assert.AreEqual(60, tankManager.Tanks[0].Speed);
                Assert.AreEqual(400, tankManager.Tanks[0].Fuel);
                Assert.AreEqual(100, tankManager.Tanks[0].Armour);
            }

        }
    }
}
