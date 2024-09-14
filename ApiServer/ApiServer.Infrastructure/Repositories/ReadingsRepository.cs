using ApiServer.Core.Entities;
using ApiServer.Core.Interfaces;
using ApiServer.Infrastructure.Database;

namespace ApiServer.Infrastructure.Repositories
{
    public class ReadingsRepository : IReadingsRepository
    {
        private readonly ApiServerContext _context;

        public ReadingsRepository(ApiServerContext context)
        {
            _context = context;
        }

        public IEnumerable<ReadingEntity> GetAll()
        {
            var values = _context.Reading.ToList();
            return values;
        }
        //public ReadingEntity AddReading(ReadingEntity readingEntity)
        //{
        //    _context.Reading.Add(readingEntity);
        //    _context.SaveChanges();
        //    return readingEntity;
        //}

        // Metoda dodająca pojedynczy odczyt do bazy danych
        public ReadingEntity AddReading(ReadingEntity readingEntity)
        {
            _context.Reading.Add(readingEntity);
            _context.SaveChanges();
            return readingEntity;
        }

        // Metoda dodająca paczkę odczytów do bazy danych
        public void AddReadingsBatch(IEnumerable<ReadingEntity> readings)
        {
            _context.Reading.AddRange(readings);
            _context.SaveChanges(); // Zapisanie wszystkich zmian do bazy danych naraz
        }
    }
}
