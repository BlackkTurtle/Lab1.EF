using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab1.API.DBContext;
using Lab1.API.Entities;

namespace Lab1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Table1Controller : ControllerBase
    {
        private readonly ILogger<Table1Controller> _logger;
        private DataBaseContext dbcontext;
        public Table1Controller(ILogger<Table1Controller> logger,
            DataBaseContext ado_unitofwork)
        {
            _logger = logger;
            dbcontext = ado_unitofwork;
        }
        [HttpGet]
        public async Task<ActionResult<Table1>> GetAllTable1()
        {
            try
            {
                var results = await dbcontext.table1.ToListAsync();
                _logger.LogInformation($"Отримали всі Table1 з бази даних!");
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetAllTable1() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
        //GET: api/events/Id
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Table1>> GetTable1ByIdAsync(int id)
        {
            try
            {
                var result = await dbcontext.table1.Where(t=>t.Id==id).SingleOrDefaultAsync();
                if (result == null)
                {
                    _logger.LogInformation($"Table1 із Id: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Отримали Table1 з бази даних!");
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі GetUserByIdAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        //POST: api/events
        [HttpPost]
        public async Task<ActionResult> PostTable1Async([FromBody] Table1 fulltable1)
        {
            try
            {
                if (fulltable1 == null)
                {
                    _logger.LogInformation($"Ми отримали пустий json зі сторони клієнта");
                    return BadRequest("Обєкт Table1 є null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Ми отримали некоректний json зі сторони клієнта");
                    return BadRequest("Обєкт Table1 є некоректним");
                }
                var table1 = new Table1()
                {
                    Name=fulltable1.Name
                };
                await dbcontext.AddAsync(table1);
                await dbcontext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі PostTable1Async - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }

        //POST: api/events/id
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTable1Async(int id, [FromBody] Table1 updatedtable1)
        {
            try
            {
                if (updatedtable1 == null)
                {
                    _logger.LogInformation($"Empty JSON received from the client");
                    return BadRequest("Table1 object is null");
                }

                var table1Entity = await dbcontext.table1.Where(t => t.Id == id).SingleOrDefaultAsync();
                if (table1Entity == null)
                {
                    _logger.LogInformation($"Table1 with ID: {id} was not found in the database");
                    return NotFound();
                }
                table1Entity.Name = updatedtable1.Name;

                await dbcontext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Transaction failed! Something went wrong in UpdateTable1Async() method - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error occurred.");
            }
        }

        //GET: api/events/Id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTable1ByIdAsync(int id)
        {
            try
            {
                var table1Entity = await dbcontext.table1.Where(t => t.Id == id).SingleOrDefaultAsync();
                if (table1Entity == null)
                {
                    _logger.LogInformation($"Table1 із Id: {id}, не був знайдейний у базі даних");
                    return NotFound();
                }

                dbcontext.table1.Remove(table1Entity);
                await dbcontext.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Транзакція сфейлилась! Щось пішло не так у методі DeleteTable1ByIdAsync() - {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "вот так вот!");
            }
        }
    }
}
