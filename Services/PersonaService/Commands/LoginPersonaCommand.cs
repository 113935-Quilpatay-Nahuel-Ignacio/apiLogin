using ApiLogin.Data;
using ApiLogin.DTOs;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApiLogin.Services.PersonaService.Commands
{
    public class LoginPersonaCommand : IRequest<PersonaDTO>
    {
        public string Nombre { get; set; } = null!;

        public string? Password { get; set; }
    }

    public class LoginPersonaCommandHandler : IRequestHandler<LoginPersonaCommand, PersonaDTO>
    {
        private readonly ApplicationContext _context;
        private readonly IValidator<LoginPersonaCommand> _validator;
        private readonly IMapper _mapper;

        public LoginPersonaCommandHandler(ApplicationContext context, IValidator<LoginPersonaCommand> validator, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<PersonaDTO> Handle(LoginPersonaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var personaResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!personaResult.IsValid)
                {
                    throw new ValidationException(personaResult.Errors);
                }

                var persona = await _context.Personas.FirstOrDefaultAsync(x => x.Nombre == request.Nombre && x.Password == request.Password, cancellationToken);

                if (persona == null)
                {
                    throw new Exception("Usuario/contraseña incorrecto/s.");
                }

                var personaDto = _mapper.Map<PersonaDTO>(persona);
                return personaDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public class LoginPersonaCommandValidation : AbstractValidator<LoginPersonaCommand>
        {
            private readonly ApplicationContext _context;

            public LoginPersonaCommandValidation(ApplicationContext context)
            {
                RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre no debe estar vacío.");
                RuleFor(x => x.Password).NotEmpty().WithMessage("La contraseña no debe estar vacía.");
                RuleFor(x => x).MustAsync(UsuarioExiste).WithMessage("El usuario no existe.");
                _context = context;
            }

            private async Task<bool> UsuarioExiste(LoginPersonaCommand command, CancellationToken arg2)
            {
                bool existe = await _context.Personas.AnyAsync(x => x.Nombre != command.Nombre && x.Password != command.Password);
                return existe;
            }
        }
    }
}
