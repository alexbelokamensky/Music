using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using Music.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Music.Controllers
{
    public class UsersContoller
    {
        public Users GetUserByName(string username)
        {
            using (var dataBaseContext = new DataBaseContext())
            {
                return dataBaseContext.Users.FirstOrDefault(u => u.Nume == username);
            }
        }
        public Users GetUserById(int id)
        {
            using (var dataBaseContext = new DataBaseContext())
            {
                return dataBaseContext.Users.FirstOrDefault(u => u.Id == id);
            }
        }
        public List<Users> GetAllUsers()
        {
            using (var dataBaseContext = new DataBaseContext())
            {
                return dataBaseContext.Users.FromSql($"SELECT * FROM Users").ToList();
            }
        }
        public Users GetLastUser()
        {
            using (var dataBaseContext = new DataBaseContext())
            {
                return dataBaseContext.Users.OrderBy(u => u.Id).Last();
            }
        }
        public void AddUser(Users user)
        {
            using (var dataBaseContext = new DataBaseContext())
            {
                dataBaseContext.Users.Add(user);
                dataBaseContext.SaveChanges();
            }
        }
        public void DeleteUser(Users user)
        {
            using (var dataBaseContext = new DataBaseContext())
            {
                dataBaseContext.Users.Remove(user);
                dataBaseContext.SaveChanges();
            }
        }
        public void ModifyUser(int id, string nume, string parola, string rol)
        {
            using (var dataBaseContext = new DataBaseContext())
            {
                Users user = GetUserById(id);
                if (user != null)
                {
                    user.Nume = nume;
                    user.Parola = parola;
                    user.Rol = rol;
                    dataBaseContext.SaveChanges();
                }
            }
        }
    }
}
