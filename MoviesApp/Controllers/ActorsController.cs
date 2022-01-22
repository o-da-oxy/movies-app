using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesApp.Data;
using MoviesApp.Filters;
using MoviesApp.Models;
using MoviesApp.Services.ActorServices;
using MoviesApp.Services.Dto;
using MoviesApp.ViewModels;
using MoviesApp.ViewModels.ActorViewModels;

namespace MoviesApp.Controllers
{
    public class ActorsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MoviesContext _context;
        private readonly IMapper _mapper;
        private readonly IActorService _service;

        public ActorsController(ILogger<HomeController> logger, MoviesContext context, IMapper mapper, IActorService service)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _service = service;
        }
        
        //get Actors
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            var movies = _service.GetAllActors();
            return View(movies);
        }
        
        [HttpGet]
        [Authorize]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<ActorViewModel>(_service.GetActor((int) id));
            
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }
        
        //гет запрос, когда только нажимаем на кнопку Create New
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        //пост запрос, когда ввели данные и нажимаем Create
        [HttpPost]
        [AgeRange]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken] //чтобы пост запрос был идемпотентным (уникальным)
        public IActionResult Create([Bind("FirstName,LastName,BirthDate")] InputActorViewModel inputModel)
        //bind - привязка модели
        {
            if (ModelState.IsValid)
            {
                _service.AddActor(_mapper.Map<ActorDto>(inputModel));
                return RedirectToAction(nameof(Index));
            }
            return View(inputModel);

        }
        
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editModel = _mapper.Map<EditActorViewModel>(_service.GetActor((int) id));
            
            if (editModel == null)
            {
                return NotFound();
            }

            return View(editModel);
        }
        
        [HttpPost]
        [AgeRange]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("FirstName,LastName,BirthDate")] EditActorViewModel editModel)
        {
            if (ModelState.IsValid)
            {
                var actor = _mapper.Map<ActorDto>(editModel);
                actor.Id = id;
                
                var result = _service.UpdateActor(actor);

                if (result == null)
                {
                    return NotFound();
                }
                
                return RedirectToAction(nameof(Index));
            }

            return View(editModel);
            
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deleteModel = _mapper.Map<DeleteActorViewModel>(_service.GetActor((int) id));
            
            if (deleteModel == null)
            {
                return NotFound();
            }

            return View(deleteModel);
        }
        
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var actor = _service.DeleteActor(id);
            if (actor==null)
            {
                return NotFound();
            }
            _logger.LogError($"Actor with id {actor.Id} has been deleted!");
            return RedirectToAction(nameof(Index)); //пререадресация
        }
        
        private bool ActorExists(int id)
        {
            return _context.Actors.Any(a => a.Id == id);
        }
        
    }
}