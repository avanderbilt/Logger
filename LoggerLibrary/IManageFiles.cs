namespace LoggerLibrary
{
    public interface IManageFiles
    {
        string ProgramDirectory { get; }

        bool Exists(string fileName);
        void Delete(string fileName);
        void Move(string fileName, string destination);
    }
}
