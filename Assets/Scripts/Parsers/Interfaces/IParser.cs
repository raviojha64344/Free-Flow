
namespace FreeFlow.Parsers.Interfaces
{ 
    public interface IParser
    {
        string To(object obj);
        T From<T>(string str);
    }
}