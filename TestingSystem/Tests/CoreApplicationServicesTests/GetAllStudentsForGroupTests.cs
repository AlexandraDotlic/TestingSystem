using Core.ApplicationServices;
using Core.Domain.Entites;
using Core.Domain.Repositories;
using Core.Infrastructure.DataAccess.EfCoreDataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.CoreApplicationServicesTests
{
    [TestClass]
    public class GetAllStudentsForGroupTests
    {
        private ICoreUnitOfWork CoreUnitOfWork;
        private CoreEfCoreDbContext DbContext;
        private GroupService GroupService;
        private ExaminerService ExaminerService;
        private StudentService StudentService;
        private TestService TestService;
        private QuestionService QuestionService;

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
            QuestionService = new QuestionService(CoreUnitOfWork);
        }

        [TestCleanup()]
        public async Task Cleanup()
        {
            await DbContext.DisposeAsync();
            DbContext = null;

        }

        [TestMethod]
        public async Task GetAllStudentsForGroupSuccess()
        {
            int examinerId = await ExaminerService.CreateExaminer("Ime", "Prezime", "123");
            int studentId1 = await StudentService.CreateStudent("Marko","Markovic","01");
            int studentId2 = await StudentService.CreateStudent("Petar", "Petrovic", "02");

            short groupId = await GroupService.CreateGroup("Grupa",examinerId);
            Group group = await CoreUnitOfWork.GroupRepository.GetFirstOrDefaultWithIncludes(g => g.Id == groupId, g => g.Students);

            await GroupService.AddStudentToGroup(groupId, studentId1);
            await GroupService.AddStudentToGroup(groupId, studentId2);

            ICollection<Core.ApplicationServices.DTOs.StudentDTO> studentDtos = await StudentService.GetAllStudentsForGroup(groupId);

            Assert.AreEqual(group.Students.Count, studentDtos.Count);
            Assert.AreEqual(group.Students.FirstOrDefault().FirstName, studentDtos.FirstOrDefault().FirstName);
            Assert.AreEqual(group.Students.FirstOrDefault().LastName, studentDtos.FirstOrDefault().LastName);

        }

    }
}