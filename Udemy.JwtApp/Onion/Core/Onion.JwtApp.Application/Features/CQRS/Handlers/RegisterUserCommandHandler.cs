using AutoMapper;
using MediatR;
using Onion.JwtApp.Application.Dtos;
using Onion.JwtApp.Application.Enums;
using Onion.JwtApp.Application.Features.CQRS.Commands;
using Onion.JwtApp.Application.Interfaces;
using Onion.JwtApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.JwtApp.Application.Features.CQRS.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, CreatedUserDto?>
    {
        private readonly IRepository<AppUser> _userRepository;
        private readonly IMapper _mapper;
        public RegisterUserCommandHandler(IRepository<AppUser> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CreatedUserDto?> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            var data = await _userRepository.CreateAsync(new AppUser
            {
                AppRoleId = (int)RoleType.Member,
                Username = request.Username,
                Password = request.Password,
            });
            return _mapper.Map<CreatedUserDto>(data);
        }
    }
}
