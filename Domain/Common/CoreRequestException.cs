using System.Net;

namespace Domain.Common;

public class CoreRequestException : Exception
{
    private readonly Dictionary<string, List<string>> _keyAndMessagesPairs = [];

    public HttpStatusCode StatusCode { get; private set; } = HttpStatusCode.BadRequest;
    public IReadOnlyDictionary<string, IReadOnlyCollection<string>> KeyAndMessagesPairs =>
        _keyAndMessagesPairs.ToDictionary(
            x => x.Key,
            x => x.Value.ToList().AsReadOnly() as IReadOnlyCollection<string>);

    public CoreRequestException SetStatusCode(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;

        return this;
    }

    public CoreRequestException AddMessages(IEnumerable<string> messages)
    {
        return AddKeyMessages("Common", messages);
    }

    public CoreRequestException AddKeyMessages(string key, IEnumerable<string> messages)
    {
        if (_keyAndMessagesPairs.TryAdd(key, [.. messages]) == false)
        {
            _keyAndMessagesPairs[key].AddRange(messages);
        }

        return this;
    }
}
