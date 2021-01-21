namespace App.SharedKernel.Domain
{
    public interface Handles<T>
       where T : DomainEvent
    {
        void Handle(T args);
    }
}
