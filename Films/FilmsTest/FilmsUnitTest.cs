using Films.Core.Mappers;
using Kirel.Repositories.Core.Models;
using Assert = NUnit.Framework.Assert;

namespace FilmsTest
{
    [TestClass]
    public class FilmServiceTests
    {
        private IMapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FilmMapper>();
                cfg.AddProfile<GenreMapper>(); // Регистрация вашего профиля маппинга
                // Добавьте другие профили маппинга, если они есть
            });

            _mapper = configuration.CreateMapper();
        }

        [TestMethod]
        public async Task CreateFilm_ValidModel_CallsRepositoriesAndService()
        {
            // Arrange
            var mockFilmRepository = new Mock<IKirelGenericEntityRepository<int, Film>>();
            var mockGenreRepository = new Mock<IKirelGenericEntityRepository<int, Genre>>();

            var filmService = new FilmService(
                mockFilmRepository.Object,
                _mapper, // Используем зарегистрированный маппер
                mockGenreRepository.Object
            );

            var filmCreateDto = new FilmCreateDto
            {
                Name = "Test Film",
                Rating = 5,
                Description = "Test Description",
                Genres = new List<GenreCreateDto>
                {
                    new() { Name = "Action" },
                    new() { Name = "Drama" }
                },
                PosterUrl = "http://example.com/test-poster.jpg"
            };

            // Assume that genre "Action" and "Drama" do not exist yet
            mockGenreRepository.Setup(repo => repo.GetList(
                    It.IsAny<Expression<Func<Genre, bool>>>(),
                    It.IsAny<Func<IQueryable<Genre>, IOrderedQueryable<Genre>>>(),
                    It.IsAny<Func<IQueryable<Genre>, IQueryable<Genre>>>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()))
                .ReturnsAsync(new List<Genre>());

            // Act
            await filmService.CreateFilm(filmCreateDto);

            // Assert
            mockGenreRepository.Verify(m => m.Insert(It.IsAny<Genre>()),
                Times.Exactly(2)); // Verify that Insert is called for each new genre
            mockFilmRepository.Verify(m => m.Insert(It.IsAny<Film>()), Times.Once);
        }

        [TestMethod]
        public async Task SearchFilms_FoundFilms_ReturnsFilmDtos()
        {
            // Arrange
            var mockFilmRepository = new Mock<IKirelGenericEntityRepository<int, Film>>();
            var mockGenreRepository = new Mock<IKirelGenericEntityRepository<int, Genre>>();
            var mockMapper = new Mock<IMapper>();

            var filmService = new FilmService(
                mockFilmRepository.Object,
                _mapper, // Используем зарегистрированный маппер
                mockGenreRepository.Object
            );

            // Создаем поддельные данные для поиска
            var filmName = "Test Film";
            var existingFilms = new List<Film>
            {
                new Film { Name = "Test Film 1" },
                new Film { Name = "Test Film 2" }
            };

            // Ожидаем, что метод GetList будет вызван с правильными параметрами
            mockFilmRepository.Setup(repo => repo.GetList(
                    It.IsAny<Expression<Func<Film, bool>>>(),
                    It.IsAny<Func<IQueryable<Film>, IOrderedQueryable<Film>>>(),
                    It.IsAny<Func<IQueryable<Film>, IQueryable<Film>>>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()))
                .ReturnsAsync(existingFilms);

            // Ожидаем, что маппер будет вызван для маппинга Film в FilmDto
            mockMapper.Setup(m => m.Map<List<FilmDto>>(existingFilms))
                .Returns(new List<FilmDto>
                {
                    new FilmDto { Name = "Test Film 1" },
                    new FilmDto { Name = "Test Film 2" }
                });

            // Act
            var result = await filmService.SearchFilms(filmName);

            // Output результатов в консоль
            Console.WriteLine("Search Films Result:");
            foreach (var filmDto in result)
            {
                Console.WriteLine($"Film Name: {filmDto.Name}");
                // Другие поля
            }

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count); // Проверяем, что количество Dto соответствует ожидаемому
            // Другие проверки по вашим ожиданиям могут быть добавлены здесь
        }

        [TestMethod]
        public async Task GetAllFilmsPaginated_ReturnsPaginatedResultWithGenres()
        {
            // Arrange
            var mockFilmRepository = new Mock<IKirelGenericEntityRepository<int, Film>>();
            var mockGenreRepository = new Mock<IKirelGenericEntityRepository<int, Genre>>();

            var filmService = new FilmService(
                mockFilmRepository.Object,
                _mapper, // Используем зарегистрированный маппер
                mockGenreRepository.Object
            );

            // Создаем поддельные данные для пагинации
            var pageNumber = 1;
            var pageSize = 10;
            var orderBy = "Name";
            var orderDirection = SortDirection.Asc;
            var search = "Test";

            var totalCount = 20; // Общее количество фильмов
            
            
            
            var paginatedFilms = new List<Film>
            {
                new Film
                {
                    Name = "Test Film 1",
                    Genres = new List<Genre>
                    {
                        new Genre { Name = "Action" },
                        new Genre { Name = "Drama" }
                    }
                },
                new Film
                {
                    Name = "Test Film 2",
                    Genres = new List<Genre>
                    {
                        new Genre { Name = "Comedy" }
                    }
                }
                // Другие фильмы
            };

            // Ожидаем, что метод Count будет вызван с правильными параметрами и вернет общее количество фильмов
            mockFilmRepository.Setup(repo => repo.Count(search))
                .ReturnsAsync(totalCount);

            // Ожидаем, что метод GetList будет вызван с правильными параметрами и вернет пагинированный список фильмов
            mockFilmRepository.Setup(repo => repo.GetList(
                    It.IsAny<string>(),
                    orderBy,
                    orderDirection,
                    pageNumber,
                    pageSize))
                .ReturnsAsync(paginatedFilms);

            // Act
            var result = await filmService.GetAllFilmsPaginated(pageNumber, pageSize, orderBy, orderDirection, search);
            Console.WriteLine("Pagination:");
            Console.WriteLine($"  Current Page: {result.Pagination.CurrentPage}");
            Console.WriteLine($"  Page Size: {result.Pagination.PageSize}");
            Console.WriteLine($"  Total Count: {result.Pagination.TotalCount}");

            foreach (var filmDto in result.Data)
            {
                Console.WriteLine("Film:");
                Console.WriteLine($"  Name: {filmDto.Name}");
                Console.WriteLine($"  Genres: {string.Join(", ", filmDto.Genres)}");
                // Другие поля
            }

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(pageNumber, result.Pagination.CurrentPage);
            Assert.AreEqual(pageSize, result.Pagination.PageSize);
            Assert.AreEqual(totalCount, result.Pagination.TotalCount);
            Assert.AreEqual(paginatedFilms.Count, result.Data.Count);

            // Проверяем, что жанры также корректно маппируются
            Assert.AreEqual(paginatedFilms[0].Genres.Count, result.Data[0].Genres.Count);
            Assert.AreEqual(paginatedFilms[1].Genres.Count, result.Data[1].Genres.Count);
            // Другие проверки по вашим ожиданиям могут быть добавлены здесь
        }
    }
}
