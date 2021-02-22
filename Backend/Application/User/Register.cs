using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Application.Validators;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.User
{
    public class Register
    {
        public class Command : IRequest<User>
        {
            public string Email { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.Password).Password();
            }
        }

        public class Handler : IRequestHandler<Command, User>
        {
            private readonly UserManager<Admin> _userManager;
            private readonly SignInManager<Admin> _signInManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly DataContext _context;

            public Handler(UserManager<Admin> userManager, SignInManager<Admin> signInManager, 
                IJwtGenerator jwtGenerator, DataContext context)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _jwtGenerator = jwtGenerator;
                _context = context;
            }

            public async Task<User> Handle(Command command, CancellationToken cancellationToken)
            {
                if (await _context.Users.AnyAsync(x => x.Email == command.Email))
                {
                    throw new RestException(HttpStatusCode.BadRequest, new {Email = "Email already exists"});
                }
                
                if (await _context.Users.AnyAsync(x => x.UserName == command.UserName))
                {
                    throw new RestException(HttpStatusCode.BadRequest, new {UserName = "UserName already exists"});
                }

                var user = new Admin
                {
                    Email = command.Email,
                    UserName = command.UserName
                };

                var result = await _userManager.CreateAsync(user, command.Password);

                if (result.Succeeded)
                {
                    return new User
                    {
                        UserName = user.UserName,
                        Token = _jwtGenerator.CreateToken(user)
                    };
                }

                throw new Exception("Problem occurred");
            }
        }
    }
}