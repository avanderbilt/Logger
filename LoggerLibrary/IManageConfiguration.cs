namespace LoggerLibrary
{
    public interface IManageConfiguration
    {
        string FileName { get; set; }
        double WindowOpacity { get; set; }
        int DefaultInterval { get; set; }
    }
}