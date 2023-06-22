using ApiLogin.Data;
using ApiLogin.DTOs;
using ApiLogin.Models;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApiLogin.Services.PersonaService.Commands
{
    public class PostPersonaCommand : IRequest<PersonaDTO>
    {
        public string Nombre { get; set; } = null!;

        public string? Password { get; set; }
    }

    public class PostPersonaCommandHandler : IRequestHandler<PostPersonaCommand, PersonaDTO>
    {
        private readonly ApplicationContext _context;
        private readonly IValidator<PostPersonaCommand> _validator;
        private readonly IMapper _mapper;

        public PostPersonaCommandHandler(ApplicationContext context, IValidator<PostPersonaCommand> validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<PersonaDTO> Handle(PostPersonaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var personaResult = await _validator.ValidateAsync(request);
                if (!personaResult.IsValid)
                {
                    throw new ValidationException(personaResult.Errors);
                }
                else
                {
                    var persona = _mapper.Map<Persona>(request);
                    await _context.Personas.AddAsync(persona);
                    await _context.SaveChangesAsync();
                    var personaDto = _mapper.Map<PersonaDTO>(persona);
                    return personaDto;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public class PostPersonaCommandValidation : AbstractValidator<PostPersonaCommand>
    {
        private readonly ApplicationContext _context;

        public PostPersonaCommandValidation(ApplicationContext context)
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre no debe estar vacío.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("La contraseña no debe estar vacía.");
            RuleFor(x => x).MustAsync(PersonaExiste).WithMessage("El usuario ya existe.");
            _context = context;
        }

        private async Task<bool> PersonaExiste(PostPersonaCommand command, CancellationToken arg2)
        {
            bool existe = await _context.Personas.AnyAsync(x => x.Nombre == command.Nombre);
            return !existe;
        }
    }
}
