﻿using Hangfire.Logging;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using HangFireDemo.crud;
using HangFireDemo.Models;

namespace HangFireDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DiaryController : Controller
    {


        public static List<Diary> records = new List<Diary>();
        private readonly icrud _icrud;

        public DiaryController(icrud Icrud)
        {
            _icrud = Icrud;
        }
        [HttpPost]
        public IActionResult AddRecord(Diary diary)
        {
            if (ModelState.IsValid)
            {
                records.Add(diary);
                _icrud.InsertRecords(diary);
                BackgroundJob.Enqueue<icrud>(x => x.SendEmail());
                return CreatedAtAction("GetDetails", new { diary.Id }, diary);
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetRecord(int id)
        {
            var records = _icrud.GetAllRecords();

            var diary = records.FirstOrDefault(x => x.Id == id);
            if (diary == null)
                return NotFound();
            BackgroundJob.Enqueue<icrud>(x => x.SyncData());
            return Ok(diary);
        }
  
        [HttpDelete]
        public IActionResult Remove(int BookID)
        {
            bool k = _icrud.DeleteRecords(BookID);
            if (k)
            {
                return Ok(k);
            }
            return NoContent();
        }

    }
}


