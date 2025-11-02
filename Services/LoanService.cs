using APIBiblioteca.Data;
using APIBiblioteca.DTO;
using APIBiblioteca.Errors;
using APIBiblioteca.Models;
using APIBiblioteca.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using OneOf;
using System;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;

namespace APIBiblioteca.Services
{
    public class LoanService(AppDbContext context) : ILoanService
    {
        private readonly AppDbContext _context = context;

        public async Task<object> GetAllAsync(int pageNumber, int pageQuantity, string bookName)
        {
            var query = _context.Loans.AsQueryable();

            if (!string.IsNullOrWhiteSpace(bookName))
            {
                query = query.Where(r => r.BookName.Contains(bookName));
            }

            var total = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(total / (double)pageQuantity);

            var loans = await query
                .Skip(pageNumber * pageQuantity)
                .Take(pageQuantity)
                .Select(l => new LoanDTO(l.Id, l.BookName, l.ReturnDate, l.Returned, l.ReaderId))
                .ToListAsync();

            return new 
            { 
                total,
                totalPages,
                pageNumber,
                loans
            };
        }

        public async Task<object> GetAllByReaderIdAsync(Guid id, int pageNumber, int pageQuantity, string bookName)
        {
            var query = _context.Loans
                .Where(l => l.ReaderId == id);

            if (!string.IsNullOrWhiteSpace(bookName))
            {
                query = query.Where(l => l.BookName.Contains(bookName));
            }

            var total = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(total / (double)pageQuantity);

            var loans = await query
                .Skip(pageNumber * pageQuantity)
                .Take(pageQuantity)
                .Select(l => new LoanDTO(l.Id, l.BookName, l.ReturnDate, l.Returned, l.ReaderId))
                .ToListAsync();

            return new
            {
                total,
                totalPages,
                pageNumber,
                loans
            };
        }

        public async Task<LoanDTO?> GetByIdAsync(Guid id)
        {
            var loan = await _context.Loans.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);

            if (loan == null)
                return null;

            var dto = new LoanDTO(loan.Id, loan.BookName, loan.ReturnDate, loan.Returned, loan.ReaderId);

            return dto;
        }
        public async Task<OneOf<LoanDTO, AppError>> SaveAsync(LoanDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.BookName) || dto.BookName.Trim().Length > 100)
                return new InvalidBookNameError();

            var newLoan = new Loan(dto.BookName.Trim(), dto.ReaderId);

            await _context.AddAsync(newLoan);
            await _context.SaveChangesAsync();

            var dtoReturn = new LoanDTO(
                newLoan.Id, 
                newLoan.BookName,
                newLoan.ReturnDate,
                newLoan.Returned, 
                newLoan.ReaderId
            );

            return dtoReturn;
        }

        public async Task<DateTime?> UpdateReturnDateAsync(Guid id)
        {
            var loan = await _context.Loans.FindAsync(id);

            if (loan == null)
                return null;

            loan.ExtendDate();
            await _context.SaveChangesAsync();

            return loan.ReturnDate;
        }

        public async Task<bool?> MarkAsReturnedAsync(Guid id)
        {
            var loan = await _context.Loans.FindAsync(id);

            if (loan == null)
                return null;

            loan.MarkAsReturned();
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Loans.Where(b => b.Id == id).ExecuteDeleteAsync();
        }
    }
}
