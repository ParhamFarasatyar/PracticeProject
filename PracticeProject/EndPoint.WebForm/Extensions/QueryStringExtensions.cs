using System.Collections.Specialized;
using System.IO;
using PracticeProject.Domain.BaseInfo.Exceptions;

namespace EndPoint.WebForm.Extensions;

public static class QueryStringExtensions
{
    public static int GetId(this NameValueCollection request)
    {
        if (!int.TryParse(request["id"], out int userId))
            throw new InvalidDataException("Invalid user id");

        return userId;
    }
}