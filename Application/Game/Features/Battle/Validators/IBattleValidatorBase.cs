using Application.Interfaces;
using FluentValidation;

namespace Application.Game.Features.Battle.Validators;

public interface IBattleValidatorBase : IValidator<IBattleCommand>;
