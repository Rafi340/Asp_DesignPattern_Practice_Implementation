using Autofac.Extras.Moq;
using Demo.Application.Services;
using Demo.Domain.Entities;
using Demo.Domain.Repository;
using Demo.Domain.Services;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Shouldly;
using System.Data;
using Demo.Application.Exceptions;

namespace Demo.Application.Tests
{
    [ExcludeFromCodeCoverage]
    public class AuthorServiceTest
    {
        private AutoMock _moq;
        private IAuthorService _authorService;
        private Mock<IApplicationUnitOfWork> _applicationUnitOfWorkMock;
        private Mock<IAuthorRepository> _authorRepositoryMock;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _moq = AutoMock.GetLoose(); // Initialize AutoMock Object
        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _moq?.Dispose(); // Dispose AutoMock Object
        }
        [SetUp]
        public void Setup()
        {
            _authorService = _moq.Create<AuthorService>();
            _applicationUnitOfWorkMock =  _moq.Mock<IApplicationUnitOfWork>();
            _authorRepositoryMock = _moq.Mock<IAuthorRepository>();
        }

        [TearDown]
        public void TearDown()
        {
            _applicationUnitOfWorkMock?.Reset();
            _authorRepositoryMock?.Reset();
        }
        [Test]
        public void AddAuthor_UniqueName_AddsAuthor()
        {
            // Arrage
            Author author = new Author
            {
                Name = "Unique Author",
                Biography = "This is a unique author biography.",
                Rating = 4.5,
            };
            _applicationUnitOfWorkMock.SetupGet(x => x.AuthorRepository)
                .Returns(_authorRepositoryMock.Object);

            _authorRepositoryMock.Setup(x => x.IsNameDuplicate(author.Name, null))
                .Returns(false)
                .Verifiable();
            _authorRepositoryMock.Setup(x => x.Add(author))
                .Verifiable();
            _applicationUnitOfWorkMock.Setup(x => x.Save())
                .Verifiable();
            // Act
            _authorService.AddAuthor(author);
            // Assert
            this.ShouldSatisfyAllConditions(
            _applicationUnitOfWorkMock.VerifyAll,
            _authorRepositoryMock.VerifyAll
            );

        }
        [Test]
        public void AddAuthor_DuplicateName_ThrowsExceptions()
        {
            // Arrange

            Author author = new Author
            {
                Name = "Duplicate Author",
                Biography = "This is a duplicate author biography.",
                Rating = 4.5,
            };
            // Act & Assert
            _applicationUnitOfWorkMock.SetupGet(x => x.AuthorRepository)
                .Returns(_authorRepositoryMock.Object);
            _authorRepositoryMock.Setup(x => x.IsNameDuplicate(author.Name, null))
                .Returns(true)
                .Verifiable();

            Should.Throw<DuplicateAuthorNameException>(
                () =>  _authorService.AddAuthor(author)
            );
        }
    }
}
