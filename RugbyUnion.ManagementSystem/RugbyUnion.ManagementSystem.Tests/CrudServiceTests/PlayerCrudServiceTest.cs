using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RugbyUnion.ManagementSystem.Data;
using RugbyUnion.ManagementSystem.Data.Models;
using RugbyUnion.ManagementSystem.Models;
using RugbyUnion.ManagementSystem.Services;
using System;
using System.Linq;

namespace RugbyUnion.ManagementSystem.Tests
{
    [TestClass]
    public class PlayerCrudServiceTest
    {
        private RugbyUnionContext _context;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<RugbyUnionContext>().UseInMemoryDatabase("RubgyUnionManagementDatabase").UseLazyLoadingProxies().Options;
            _context = new RugbyUnionContext(options);
            _context.Database.EnsureCreated();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public void Can_Get_Player()
        {
            var sut = GetSut();
            var player = sut.Get<PlayerDto, Player>(1).GetAwaiter().GetResult();
            Assert.AreEqual(player.Id, 1);
        }

        [TestMethod]
        public void Get_Player_Throws_Exception_If_Id_Not_Found()
        {
            var sut = GetSut();
            Assert.ThrowsException<Exception>(() => sut.Get<PlayerDto, Player>(100).GetAwaiter().GetResult());
        }

        [TestMethod]
        public void Can_Get_Paged_Players()
        {
            var sut = GetSut();
            var players = sut.GetAll<PlayerDto, Player>(0, 5).GetAwaiter().GetResult();
            Assert.AreEqual(players.Items.Count(), 5);
            Assert.AreEqual(players.TotalItems, 8);
        }

        [TestMethod]
        public void Can_Create_Player()
        {
            var sut = GetSut();

            var newPlayer = new PlayerDto();
            newPlayer.FirstName = "John";
            newPlayer.LastName = "Doe";
            newPlayer.DateOfBirth = new System.DateTime(1992, 1, 27);
            newPlayer.HeightCentimeters = 172;
            newPlayer.WeightKilograms = 95;
            newPlayer.CurrentPositionId = 1;

            var createdPlayer = sut.Create<PlayerDto, Player>(newPlayer).GetAwaiter().GetResult();

            Assert.AreEqual(createdPlayer.Id, 9);
        }

        [TestMethod]
        public void Create_Player_Throws_Exception_If_Duplicate_Player_Created()
        {
            var sut = GetSut();

            var newPlayer = new PlayerDto();
            newPlayer.FirstName = "John";
            newPlayer.LastName = "Doe";
            newPlayer.DateOfBirth = new System.DateTime(1992, 1, 27);
            newPlayer.HeightCentimeters = 172;
            newPlayer.WeightKilograms = 95;
            newPlayer.CurrentPositionId = 1;

            var createdPlayer = sut.Create<PlayerDto, Player>(newPlayer).GetAwaiter().GetResult();
            Assert.ThrowsException<Exception>(() => sut.Create<PlayerDto, Player>(newPlayer).GetAwaiter().GetResult());
        }


        [TestMethod]
        public void Can_Update_Player()
        {
            var sut = GetSut();

            var newPlayer = new PlayerDto();
            newPlayer.FirstName = "John";
            newPlayer.LastName = "Doe";
            newPlayer.DateOfBirth = new System.DateTime(1992, 1, 27);
            newPlayer.HeightCentimeters = 172;
            newPlayer.WeightKilograms = 95;
            newPlayer.CurrentPositionId = 1;
            newPlayer = sut.Create<PlayerDto, Player>(newPlayer).GetAwaiter().GetResult();

            newPlayer.FirstName = "Jane";
            newPlayer = sut.Update<PlayerDto, Player>(newPlayer).GetAwaiter().GetResult();

            Assert.AreEqual(newPlayer.FirstName, "Jane");
        }

        [TestMethod]
        public void Update_Player_Throws_Exception_If_Duplicate_Player_Created()
        {
            var sut = GetSut();

            var newPlayer1 = new PlayerDto();
            newPlayer1.FirstName = "John";
            newPlayer1.LastName = "Doe";
            newPlayer1.DateOfBirth = new System.DateTime(1992, 1, 27);
            newPlayer1.HeightCentimeters = 172;
            newPlayer1.WeightKilograms = 95;
            newPlayer1.CurrentPositionId = 1;
            newPlayer1 = sut.Create<PlayerDto, Player>(newPlayer1).GetAwaiter().GetResult();

            var newPlayer2 = new PlayerDto();
            newPlayer2.FirstName = "Jane";
            newPlayer2.LastName = "Doe";
            newPlayer2.DateOfBirth = new System.DateTime(1992, 1, 27);
            newPlayer2.HeightCentimeters = 172;
            newPlayer2.WeightKilograms = 95;
            newPlayer2.CurrentPositionId = 1;
            newPlayer2 = sut.Create<PlayerDto, Player>(newPlayer2).GetAwaiter().GetResult();

            newPlayer1.FirstName = "Jane";
            Assert.ThrowsException<Exception>(() => sut.Update<PlayerDto, Player>(newPlayer1).GetAwaiter().GetResult());
        }

        [TestMethod]
        public void Update_Player_Throws_Exception_If_Id_Not_Found()
        {
            var sut = GetSut();

            var newPlayer1 = new PlayerDto();
            newPlayer1.FirstName = "John";
            newPlayer1.LastName = "Doe";
            newPlayer1.DateOfBirth = new System.DateTime(1992, 1, 27);
            newPlayer1.HeightCentimeters = 172;
            newPlayer1.WeightKilograms = 95;
            newPlayer1.CurrentPositionId = 1;
            newPlayer1 = sut.Create<PlayerDto, Player>(newPlayer1).GetAwaiter().GetResult();

            newPlayer1.FirstName = "Jane";
            newPlayer1.Id = null;

            Assert.ThrowsException<Exception>(() => sut.Update<PlayerDto, Player>(newPlayer1).GetAwaiter().GetResult());
        }

        [TestMethod]
        public void Can_Delete_Player()
        {
            var sut = GetSut();
            var deletedPlayer = sut.Delete<PlayerDto, Player>(1).GetAwaiter().GetResult();
            Assert.AreEqual(deletedPlayer.Id, 1);
            Assert.ThrowsException<Exception>(() => sut.Get<PlayerDto, Player>(1).GetAwaiter().GetResult());
        }

        [TestMethod]
        public void Delete_Player_Throws_Exception_If_Id_Not_Found()
        {
            var sut = GetSut();
            Assert.ThrowsException<Exception>(() => sut.Delete<PlayerDto, Player>(100).GetAwaiter().GetResult());
        }

        public ICrudService GetSut()
        {
            return new CrudService(_context);
        }
    }
}
