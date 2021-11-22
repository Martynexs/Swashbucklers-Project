﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EncounterAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Contracts;

namespace EncounterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public UsersController(IRepositoryWrapper repositoryWrapper)
        {
            _repository = repositoryWrapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
        {
            var users = await _repository.User.GetAllUsers();
            return users.ToList();
        }

        // GET: api/Users/username
        [HttpGet("{username}")]
        public async Task<ActionResult<UserModel>> GetUser(string username)
        {
            var currentUser = User.Claims.Where(c => c.Type == ClaimTypes.Name).First().Value;

            var user = await _repository.User.GetUserByUsername(username);

            if (user == default)
            {
                return NotFound();
            }

            if(currentUser != username)
            {
                return Forbid();
            }

            return user;
        }

        // PUT: api/Users/username
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{userId}")]
        public async Task<IActionResult> PutUser(long userId, UserModel user)
        {
            if (userId != user.ID)
            {
                return BadRequest();
            }

            _repository.User.UpdateUser(user);

            try
            {
                await _repository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(userId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserModel>> PostUser(UserModel user)
        {
            var hashedPassword = PasswordHasher.HashPassword(user.Password);
            user.Password = Convert.ToBase64String(hashedPassword);

            _repository.User.CreateUser(user);
            await _repository.SaveAsync();

            return CreatedAtAction("GetUser", new { username = user.Username }, user);
        }

        private bool UserExists(long userId)
        {
            return _repository.User.GetUserById(userId) == default;
        }
    }
}
