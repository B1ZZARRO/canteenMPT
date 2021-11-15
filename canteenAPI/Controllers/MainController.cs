using canteenAPI.Models;
using canteenAPI.Models.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace canteenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private canteenMPTContext _context;

        public MainController(canteenMPTContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Авторизация пользователя по логину и паролю
        /// </summary>
        [HttpGet]
        [Route("[action]")]
        public ActionResult<UserView> GetUser(string email, string password)
        {
            var result = (from user in _context.Users
                          where user.Email == email && user.Password == password
                          select new UserView()
                          {
                              UserId = user.UserId,
                              LastName = user.LastName,
                              FirstName = user.FirstName,
                              MiddleName = user.MiddleName,
                              Email = user.Email,
                              Password = user.Password,
                              Role = user.Role.Name
                          }).FirstOrDefault();
            return new ObjectResult(result);
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        [HttpPost]
        [Route("[action]")]
        public ActionResult<UserView> RegUser(UserView newUser)
        {
            User user = new User()
            {
                LastName = newUser.LastName,
                FirstName = newUser.FirstName,
                MiddleName = newUser.MiddleName,
                Email = newUser.Email,
                Password = newUser.Password,
                RoleId = _context.Roles.Where(r => r.Name == "Пользователь").Select(r => r.RoleId).FirstOrDefault()
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            var result = (from usr in _context.Users
                          where user.Email == newUser.Email
                          join role in _context.Roles on user.RoleId equals role.RoleId
                          select new UserView()
                          {
                              UserId = user.UserId,
                              LastName = user.LastName,
                              FirstName = user.FirstName,
                              MiddleName = user.MiddleName,
                              Email = user.Email,
                              Password = user.Password,
                              Role = role.Name
                          }).ToList();

            if (result.Count == 0)
            {
                return NotFound();
            }

            return new ObjectResult(result[0]);
        }

        /// <summary>
        /// Вывод всех товаров
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<object> GetAvaibilities(int categoryID, int buildingID)
        {
            var result = (from Avaibility in _context.Avaibilities
                          where Avaibility.Product.CategoryId == categoryID && Avaibility.BuildingId == buildingID
                          select new
                          {
                              Count = Avaibility.Count,
                              Name = Avaibility.Product.Name,
                              Cost = Avaibility.Product.Cost,
                              Image = Avaibility.Product.Image
                          }).ToList();
            return result;
        }

        /// <summary>
        /// Вывод списка товаров в одном заказе
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<object> GetOrder(int orderID)
        {
            var result = (from OrderList in _context.OrderLists
                          where OrderList.OrderId == orderID
                          select new
                          {
                              Name = OrderList.Avaibility.Product.Name,
                              Cost = OrderList.Avaibility.Product.Cost,
                              Count = OrderList.Count
                          }).ToList();
            return result;
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<OrderView> NewOrder(OrderView _order)
        {
            Order order = new Order()
            {
                UserId = _order.UserId,
                Cost = _order.Cost,
                PayStatus = _order.PayStatus,
                OrderStatus = _order.OrderStatus
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
            foreach (var i in _order.Avaibility)
            {
                OrderList orderList = new OrderList()
                {
                    Count = i.Count,
                    AvaibilityId = i.AvaibilityId,
                    Order = order
                };
                _context.OrderLists.Add(orderList);
            }
            _context.SaveChanges();
            return _order;
        }

        /// <summary>
        /// Вывод ролей пользователей
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<object> GetRoles()
        {
            var result = (from Role in _context.Roles
                          select new
                          {
                              Name = Role.Name
                          }).ToList();
            return result;
        }

        /// <summary>
        /// Вывод пользователей
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<object> GetUsers()
        {
            var result = (from User in _context.Users
                          select new
                          {
                              Email = User.Email,
                              Password = User.Password,
                              LastName = User.LastName,
                              FirstName = User.FirstName,
                              MiddleName = User.MiddleName
                          }).ToList();
            return result;
        }
    }
}
