using Microsoft.EntityFrameworkCore;

namespace DigitalLibary
{
    public class UserRepository
    {

        public List<User> SelectAll()
        {
            List<User> users = new List<User>();
            using (var db = new AppContext())
            {
                users = db.User.ToList();
                foreach (var user in users)
                {
                    Console.WriteLine(user.Id + " " + user.Name + " " + user.Email);
                }
            }
            return users;
        }

        public User SelectFromId(int idUser)
        {
            User user = new User();
            using (var db = new AppContext())
            {
                user = db.User.Find(idUser);
            }
            if (user == null)
            {
                Console.WriteLine("Пользователь не найден");
            }
            else
            {
                Console.WriteLine(user.Name + " " + user.Id + " " + user.Email);
            }
            return user;
        }
        public void InsertInDb(User user)
        {
            if (user != null)
            {
                using (var db = new AppContext())
                {
                    db.User.Add(user);
                    db.SaveChanges();
                }
            }
            else
            {
                Console.WriteLine("Данные указаны некорректно");
            }
        }
        public void DeleteFromDb(User user)
        {
            try
            {
                using (var db = new AppContext())
                {
                    db.Remove(user);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Пользователь не удален");
                Console.WriteLine(ex.Message);
            }

        }

        public void UpdateFromId(User user)
        {
            try
            {
                using (var db = new AppContext())
                {
                    var userFromDb = db.User.Find(user.Id);
                    if (userFromDb == null)
                    {
                        Console.WriteLine("Не удалось найти пользователя");
                    }
                    else
                    {
                        userFromDb.Name = user.Name;
                        userFromDb.Email = user.Email;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Не удалось обновить пользователя\n" + ex.Message);
            }
        }
        public int GetCountBooksFromUser(string name)
        {
            int countBooks = 0;
            try
            {
                if (name != null)
                {
                    using (var db = new AppContext())
                    {
                        countBooks = db.User.Include(x => x.Books).Where(u => u.Name.ToLower() == name.ToLower()).Count();

                    }
                }
                else
                {
                    Console.WriteLine("Не указано обязательное поле");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Некорректные данные");
            }
            return countBooks;
        }

    }
}
