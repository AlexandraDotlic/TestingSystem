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
    public class DeleteStudentTests
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
            StudentService = new StudentService(CoreUnitOfWork);
        }

        [TestCleanup()]
        public async Task Cleanup()
        {
            CoreUnitOfWork.ClearTracker();
            IReadOnlyCollection<Student> students = await CoreUnitOfWork.StudentRepository.GetAllList();

            if (students != null && students.Count != 0)
            {
                foreach (var item in students)
                {
                    await CoreUnitOfWork.StudentRepository.Delete(item);
                    await CoreUnitOfWork.SaveChangesAsync();
                }

            }

            await DbContext.DisposeAsync();
            DbContext = null;
            
        }

        [TestMethod]
        public async Task TestDeleteStudentSuccess()
        {
            int id  = await StudentService.CreateStudent("Ime", "Prezime", "123");

            await StudentService.DeleteStudent(id);
            var student = await CoreUnitOfWork.ExaminerRepository.GetById(id);

            Assert.AreEqual(student, null);

        }

        [TestMethod]
        public async Task TestDeleteStudentFail()
        {
            var examiner = await CoreUnitOfWork.ExaminerRepository.GetById(100);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await StudentService.DeleteStudent(100), $"Student with Id={100} doesn't exist");



        }

    }
}
