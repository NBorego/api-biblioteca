using APIBiblioteca.Data;
using APIBiblioteca.DTO;
using APIBiblioteca.Errors;
using APIBiblioteca.Models;
using APIBiblioteca.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using OneOf;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace APIBiblioteca.Services
{
    public class ReaderService(AppDbContext context) : IReaderService
    {
        private readonly AppDbContext _context = context;

        public async Task<object> GetAllAsync(int pageNumber, int pageQuantity, string name)
        {
            var query = _context.Readers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(r => r.Name.Contains(name));
            }

            var total = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(total / (double)pageQuantity);

            var readers = await query
                .Skip(pageNumber * pageQuantity)
                .Take(pageQuantity)
                .Select(r => new ReaderDTO(r.Id, r.Name, r.Address, r.Phone))
                .ToListAsync();

            return new
            {
                total,
                totalPages,
                pageNumber,
                readers
            };
        }

        public async Task<ReaderDTO?> GetByIdAsync(Guid id)
        {
            var reader = await _context.Readers.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);

            if (reader == null)
                return null;

            var dto = new ReaderDTO(reader.Id, reader.Name, reader.Address, reader.Phone);

            return dto;
        }

        public async Task<OneOf<ReaderDTO, AppError>> SaveAsync(ReaderDTO dto)
        {
            if (dto.Phone.Trim().Length != 11)
                return new InvalidReaderPhoneError();

            if (dto.Address.Trim().Length > 150)
                return new InvalidReaderAddressError();

            var isInvalidReaderName = Regex.IsMatch(dto.Name, @"[^\p{L}\s]", RegexOptions.Compiled);

            if (string.IsNullOrWhiteSpace(dto.Name) || isInvalidReaderName || dto.Name.Trim().Length > 100)
                return new InvalidReaderNameError();

            var newReader = new Reader(dto.Name.Trim(), dto.Address.Trim(), dto.Phone.Trim());

            await _context.AddAsync(newReader);
            await _context.SaveChangesAsync();

            var dtoReturn = new ReaderDTO(newReader.Id, newReader.Name, newReader.Address, newReader.Phone);

            return dtoReturn;
        }

        public async Task<OneOf<ReaderDTO, AppError>> UpdateAsync(Guid id, ReaderDTO dto)
        {
            if (dto.Phone.Trim().Length != 11)
                return new InvalidReaderPhoneError();

            if (dto.Address.Trim().Length > 150)
                return new InvalidReaderAddressError();

            var isInvalidReaderName = Regex.IsMatch(dto.Name, @"[^\p{L}\s]", RegexOptions.Compiled);

            if (isInvalidReaderName || dto.Name.Trim().Length > 100)
                return new InvalidReaderNameError();

            var reader = new Reader(dto.Name.Trim(), dto.Address.Trim(), dto.Phone.Trim());

            await _context.Readers.Where(r => r.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(p => p.Name, reader.Name)
                    .SetProperty(p => p.Address, reader.Address)
                    .SetProperty(p => p.Phone, reader.Phone)
                );

            var dtoReturn = new ReaderDTO(reader.Id, reader.Name, reader.Address, reader.Phone);

            return dtoReturn;
        }
        public async Task DeleteAsync(Guid id)
        {
            await _context.Readers.Where(r => r.Id == id).ExecuteDeleteAsync();
        }
    }
}
