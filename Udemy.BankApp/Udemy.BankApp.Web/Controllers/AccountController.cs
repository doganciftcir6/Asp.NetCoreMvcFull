using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Udemy.BankApp.Web.Data.Entities;
using Udemy.BankApp.Web.Data.Interfaces;
using Udemy.BankApp.Web.Data.UnitOfWork;
using Udemy.BankApp.Web.Mapping;
using Udemy.BankApp.Web.Models;

namespace Udemy.BankApp.Web.Controllers
{
    public class AccountController : Controller
    {
        //dependency injection
        //private readonly IApplicationUserRepository _applicationUserRepository;
        //private readonly IAccountRepository _accountRepository;
        //private readonly IUserMapper _userMapper;
        //private readonly IAccountMapper _accountMapper;

        //public AccountController(IApplicationUserRepository applicationUserRepository, IUserMapper userMapper, IAccountRepository accountRepository, IAccountMapper accountMapper)
        //{
        //    _applicationUserRepository = applicationUserRepository;
        //    _userMapper = userMapper;
        //    _accountRepository = accountRepository;
        //    _accountMapper = accountMapper;
        //}

        //GENERİC REPOSİTORY'DEN SONRA DEPENDECY İNJECTİON
        //private readonly IGenericRepository<Account> _accountRepository;
        //private readonly IGenericRepository<ApplicationUser> _userRepository;
        //public AccountController(IGenericRepository<Account> accountRepository, IGenericRepository<ApplicationUser> userRepository)
        //{
        //    _accountRepository = accountRepository;
        //    _userRepository = userRepository;
        //}

        //UNİT OF WORK'TEN SONRA DEPENDECY İNJECTİON
        private readonly IUow _uow;

        public AccountController(IUow uow)
        {
            _uow = uow;
        }



        //create
        //entity'i Mapping classı ile modele çevirelim View'e model olarak gönderelim bu listeyi.
        //parametre içindeki id route yani url ' ye düşen ıd oluyor. Url deki ıd ile veritabanındaki ıd biribirne eşitliyoruz ki kullanıcı hangi kayıtı seçtiğini bilsin. O kayıdın bilgileri gelsin.
        public IActionResult Create(int id)
        {
            //var userInfo = _userMapper.MapToUserList(_applicationUserRepository.GetById(id));
            //GENERİC REPOSİTORY'DEN SONRA
            var userInfo = _uow.GetRepository<ApplicationUser>().GetById(id);
            return View(new UserListModel
            {
                Id=userInfo.Id,
                Name=userInfo.Name,
                Surname=userInfo.Surname,
            });
        }
        //artık modelimiz olduğu için entity ile değil model ile model binding yapıcaz view sayfasında da oluşturduğumuz modeli kullanıcaz.
        //bu model ile databinding yapıcaz ilgili inputların name propu ile modelin propları aynı isimde olmak zorundadır.Kullanıcı inputa bilgi girdiğinde o name propu modelin proplarına değer olarak setlenir. parametre içindeki model'e aktarılır bilgiler yani.
        [HttpPost]
        public IActionResult Create(AccountCreateModel model)
        {
            //Db'ye ekleme yaparken modeli Entity'e çevirmem gerekiyor db entity'den anlar. Bunun için mapper kullanıcaz.
            //_accountRepository.Create(_accountMapper.Map(model));
            //GENERİC REPOSİTORY'DEN SONRA
            _uow.GetRepository<Account>().Create(new Account
            {
                AccountNumber=model.AccountNumber,
                Balance = model.Balance,
                ApplicationUserId = model.ApplicationUserId
            });
            _uow.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        //ilgili kullanıcının bilgisini getirmek
        [HttpGet]
        public IActionResult GetByUserId(int userId)
        {
            var query = _uow.GetRepository<Account>().GetQueryable();
            var accountList = query.Where(x => x.ApplicationUserId == userId).ToList();
            var user = _uow.GetRepository<ApplicationUser>().GetById(userId);
            ViewBag.Fullname = user.Name + " " + user.Surname;
            var list = new List<AccountListModel>();
            foreach (var item in accountList)
            {
                list.Add(new()
                {
                    AccountNumber = item.AccountNumber,
                    ApplicationUserId=item.ApplicationUserId,
                    Balance = item.Balance,
                    Id = item.Id
                });
            }
            return View(list);
        }
        //para göndermek
        [HttpGet]
        public IActionResult SendMoney(int accountId)
        {
            var query = _uow.GetRepository<Account>().GetQueryable();
            var accounts = query.Where(x => x.Id != accountId).ToList();
            var list = new List<AccountListModel>();
            ViewBag.Sender = accountId; 
            foreach (var account in accounts)
            {
                list.Add(new()
                {
                    AccountNumber=account.AccountNumber,
                    ApplicationUserId=account.ApplicationUserId,
                    Balance=account.Balance,
                    Id=account.Id
                });
            }
            return View(new SelectList(list,"Id","AccountNumber"));
        }
        [HttpPost]
        public IActionResult SendMoney(SendMoneyModel model)
        {
            var senderAccount = _uow.GetRepository<Account>().GetById(model.SenderId);
            senderAccount.Balance -= model.Amount;
            _uow.GetRepository<Account>().Update(senderAccount);
            var account = _uow.GetRepository<Account>().GetById(model.AccountId);
            account.Balance += model.Amount;
            _uow.GetRepository<Account>().Update(account);
            _uow.SaveChanges();
            return RedirectToAction("Index","Home");
        }
    }
}
