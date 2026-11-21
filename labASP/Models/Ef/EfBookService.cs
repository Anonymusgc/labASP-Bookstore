namespace labASP.Models.Ef;

public class EfBookService(BookDbContext context) : IBookService
{
    public IEnumerable<Book> findAll()
    {
        return context.Books;
    }

    public Book? findByID(int id)
    {
        return context.Books.FirstOrDefault(b => b.Id == id);
    }

    public void Add(Book book)
    {
        context.Books.Add(book);
        context.SaveChanges();
    }

    public void Edit(Book book)
    {
        context.Books.Update(book); // może dodać nieistniejącą pole
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        var book = context.Books.FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            return;
        }
        context.Books.Remove(book);
        context.SaveChanges();
        
    }
}