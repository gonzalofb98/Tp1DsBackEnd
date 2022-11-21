namespace InfaestructuraTransversal.IoC
{
    public interface IContenedor
    {
        T Resolver<T>();
        void RegistrarTipo(Type tipo);
    }
}