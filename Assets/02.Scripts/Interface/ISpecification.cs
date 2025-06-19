using UnityEngine;

public interface ISpecification<T>
{
    public string ErrorMassage { get; }
    public bool IsSatisfiedBy(T value);
}
