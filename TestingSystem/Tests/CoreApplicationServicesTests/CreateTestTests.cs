﻿
using Core.ApplicationServices;
using Core.Domain.Entites;
using Core.Domain.Repositories;
using Core.Infrastructure.DataAccess.EfCoreDataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tests.CoreApplicationServicesTests
{
    [TestClass]
    public class CreateTestTests
    {
        private ICoreUnitOfWork CoreUnitOfWork;
        private CoreEfCoreDbContext DbContext;
        private GroupService GroupService;
        private ExaminerService ExaminerService;
        private StudentService StudentService;
        private TestService TestService;

        [TestInitialize]
        public void Setup()
        {
            var dbContextFactory = new SampleDbContextFactory();
            DbContext = dbContextFactory.CreateDbContext(new string[] { });
            CoreUnitOfWork = new CoreEfCoreUnitOfWork(DbContext);
            GroupService = new GroupService(CoreUnitOfWork);
            ExaminerService = new ExaminerService(CoreUnitOfWork);
            StudentService = new StudentService(CoreUnitOfWork);
            TestService = new TestService(CoreUnitOfWork);
        }

        [TestCleanup()]
        public async Task Cleanup()
        {
            await DbContext.DisposeAsync();
            DbContext = null;

        }

        [TestMethod]
        public async Task CreateTestSuccess()
        {
            string externalEId = Guid.NewGuid().ToString();

            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", externalEId);
            DateTime dateTime = DateTime.Now;
            short testId = await TestService.CreateTest(externalEId, "Test1", dateTime);

            Test test = await CoreUnitOfWork.TestRepository.GetById(testId);

            Assert.AreEqual(testId, test.Id);
            Assert.AreEqual("Test1", test.Title);
            Assert.AreEqual(examinerId, test.ExaminerId);
            Assert.AreEqual(dateTime, test.StartDate);
        }

        [TestMethod]
        public async Task CreateTestFail()
        {
            DateTime dateTime = DateTime.Now;

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await TestService.CreateTest("100", "Test", dateTime), $"Examiner does not exist");



        }
    }
}