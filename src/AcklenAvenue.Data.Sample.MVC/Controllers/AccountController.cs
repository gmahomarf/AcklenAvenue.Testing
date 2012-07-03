using System.Web.Mvc;
using AcklenAvenue.Data.Sample.Domain;
using AcklenAvenue.Data.Sample.MVC.Models;
using AutoMapper;

namespace AcklenAvenue.Data.Sample.MVC.Controllers
{
    public class AccountController : Controller
    {
        readonly IAccountFetcher _accountFetcher;
        readonly IMappingEngine _mappingEngine;

        public AccountController(IAccountFetcher accountFetcher, IMappingEngine mappingEngine)
        {
            _accountFetcher = accountFetcher;
            _mappingEngine = mappingEngine;
        }

        public ActionResult View(long? id)
        {
            //pull the account from the fetcher
            Account account = _accountFetcher.Get(id.Value);

            //always map domain objects to models
            AccountModel mappedAccount = _mappingEngine.Map<Account, AccountModel>(account);

            //no view right now... it's just a sample
            return View(mappedAccount);
        }
    }
}