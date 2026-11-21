namespace labASP.Models;

public interface IBookService
{
    IEnumerable<Book> findAll();
    Book? findByID(int id);
    void Add(Book book);
    void Edit(Book book);
    void Delete(int id);
}