using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CrudDenemeleri.Models;
using CrudDenemeleri.Services.Interfaces;

namespace CrudDenemeleri.Controllers;

public class HomeController : Controller
{
    private readonly IPersonService personService;
    public HomeController(IPersonService personService)
    {
        this.personService = personService;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        List<Person> people = personService.GetAll();
        return View(people);
    }

    [HttpGet]
    public IActionResult GetById(int id)
    {
        Person person = personService.GetById(id);
        return View(person);
    }
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Add(Person person)
    {
        personService.Add(person);
        return RedirectToAction("GetAll");
    }
    [HttpGet]
    public IActionResult Update(int id)
    {
        Person person = personService.GetById(id);
        return View(person);
    }
    [HttpPost]
    public IActionResult Update(Person person)
    {
        personService.Update(person);
        return RedirectToAction("GetAll");
    }
    [HttpPost]
    public IActionResult Delete(int id)
    {
        personService.Delete(id);
        return RedirectToAction("GetAll");
    }


}
