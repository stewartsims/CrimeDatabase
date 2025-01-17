using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrimeDatabase.Data;
using CrimeDatabase.Models;

namespace CrimeDatabase.Controllers
{
    // Crime events controller with list, search, create, update and delete functionality
    public class CrimeEventsController : Controller
    {
        private readonly ICrimeEventRepository _crimeEventRepository;

        public CrimeEventsController(ICrimeEventRepository crimeEventRepository)
        {
            _crimeEventRepository = crimeEventRepository;
        }

        // List all events
        // if search parameter provided, filter by: Area, Town & Victim Name
        public async Task<IActionResult> Index(string searchString)
        {
            TempData["SearchString"] = searchString;
            var crimesToReturn = _crimeEventRepository.Search(searchString);
            return View(crimesToReturn);
        }


        // Get an individual crime event to display details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crimeEvent = _crimeEventRepository.GetById(id.Value);
            if (crimeEvent == null)
            {
                return NotFound();
            }

            return View(crimeEvent);
        }

        // Prepare to create new crime event
        public IActionResult Create()
        {

            return View();
        }

        // Create a new crime event, saving to the database
        // also record audit log of event creation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CrimeDate,LocationArea,LocationTown,VictimName,CrimeType,Notes")] CrimeEvent crimeEvent)
        {
            if (ModelState.IsValid)
            {
                _crimeEventRepository.Create(crimeEvent);
                return RedirectToAction(nameof(Index));
            }
            return View(crimeEvent);
        }

        // Prepare to edit crime event
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crimeEvent = _crimeEventRepository.GetById(id.Value);
            if (crimeEvent == null)
            {
                return NotFound();
            }
            return View(crimeEvent);
        }

        // Save crime event to database
        // also record an audit log of the action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CrimeDate,LocationArea,LocationTown,VictimName,CrimeType,Notes")] CrimeEvent crimeEvent)
        {
            if (id != crimeEvent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _crimeEventRepository.Update(crimeEvent);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_crimeEventRepository.GetById(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(crimeEvent);
        }

        // Prepare to delete crime event
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crimeEvent = _crimeEventRepository.GetById(id.Value);
            if (crimeEvent == null)
            {
                return NotFound();
            }

            return View(crimeEvent);
        }

        // Delete crime event from database
        // also record an audit log of the event deletion
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var crimeEvent = _crimeEventRepository.GetById(id);
            if (crimeEvent != null)
            {
                _crimeEventRepository.DeleteById(crimeEvent.Id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
