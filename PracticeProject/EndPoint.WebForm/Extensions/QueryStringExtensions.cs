using System.Collections.Specialized;
using System.IO;

namespace EndPoint.WebForm.Extensions;

public static class QueryStringExtensions
{
    public static int GetId(this NameValueCollection request)
    {
        if (!int.TryParse(request["id"], out var userId))
            throw new InvalidDataException("Invalid user id");

        return userId;
    }
}