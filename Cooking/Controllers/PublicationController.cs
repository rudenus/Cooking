using BusinessLogic.PublicationLogic;
using Cooking.Infrastructure.Validator.Publication;
using Microsoft.AspNetCore.Mvc;

namespace Cooking.Controllers
{
    public class PublicationController : Controller
    {
        private readonly PublicationLogic publicationLogic;
        private readonly PublicationValidator publicationValidator;

        public PublicationController(PublicationLogic publicationLogic, PublicationValidator publicationValidator)
        {
            this.publicationLogic = publicationLogic;
            this.publicationValidator = publicationValidator;
        }

        public IActionResult Get()
        {
            publicationLogic.AddComment().GetAwaiter();
            return View();
        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult Update()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult AddLike()
        {
            return View();
        }

        public IActionResult DeleteLike()
        {
            return View();
        }

        public IActionResult AddComment()
        {
            return View();
        }

        public IActionResult DeleteComment()
        {
            return View();
        }
    }
}
