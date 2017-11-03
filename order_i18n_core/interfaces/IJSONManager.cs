namespace order_i18n_core.interfaces
{
    public interface IJSONManager
    {
        string BuildOrder();
        void ClearObject();

        void GenerateFile(string destination);
    }
}
