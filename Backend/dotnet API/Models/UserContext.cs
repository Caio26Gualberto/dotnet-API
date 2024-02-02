public static class UserContext
{
    private static readonly AsyncLocal<int?> userIdLocal = new AsyncLocal<int?>();

    public static int? UserId
    {
        get { return userIdLocal.Value; }
        set { userIdLocal.Value = value; }
    }
}