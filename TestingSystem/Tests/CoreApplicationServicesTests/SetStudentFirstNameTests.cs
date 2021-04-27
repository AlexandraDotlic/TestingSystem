using Core.ApplicationServices;
using Core.Domain.Repositories;
using Core.Domain.Entites;
using Core.Infrastructure.DataAccess.EfCoreDataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tests.CoreApplicationServicesTests
{

    [TestClass]
    public class SetStudentFirstNameTests
    {
        private ICoreUnitOfWork CoreUnitOfWork;
        private CoreEfCoreDbContext DbContext;
        private StudentService StudentService;

        [TestInitialize]
        public void Setup()
        {
            var dbContextFactory = new SampleDbContextFactory();
            DbContext = dbContextFactory.CreateDbContext(new string[] { });
            CoreUnitOfWork = new CoreEfCoreUnitOfWork(DbContext);

        }

        [TestCleanup]
        public async Task Cleanup()
        {
            await DbContext.DisposeAsync();
            DbContext = null;
        }

        [TestMethod]
        public async Task SetStudentFirstNameSuccess()
        {
            int studentId = await StudentService.CreateStudent("Ime", "Prezime", "123");
            var student = await CoreUnitOfWork.StudentRepository.GetById(studentId);
            await StudentService.SetStudentFirstName(studentId, "Marko");

            Assert.AreEqual(student.FirstName, "Marko");
        }

        [TestMethod]
        public async Task SetStudentFirstNameFail()
        {
            var student = await CoreUnitOfWork.StudentRepository.GetById(100);
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await StudentService.SetStudentFirstName(1000, "Marko"), $"Student with with Id={100} doesn't existt");
        }
    }
}