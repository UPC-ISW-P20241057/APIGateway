namespace JwtAuthenticationManager.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}