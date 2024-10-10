public static class Global
{
    public static Random random = new Random();

    public static Dictionary<string, string> Photos = new Dictionary<string, string>()
    {
        ["Cat"] = "AgACAgIAAxkDAAMNZuspCZowgTyqpfELhzfFylpqivMAAsLhMRu-_VlL5mZlAgODaYcBAAMCAAN5AAM2BA",
        ["Dog"] = "AgACAgIAAxkDAAM5Zu7vMIl5ftwQ6540dAENr0zQ3UgAAj_qMRvq0XhLk12bXZrXWCgBAAMCAAN5AAM2BA",
        ["Parrot"] = "AgACAgIAAxkDAAM8Zu7vUGCjPPONju73Q-xVQhDSEVwAAkLqMRvq0XhLhRq-BdfnIL4BAAMCAAN5AAM2BA",
        ["Hamster"] = "AgACAgIAAxkDAAM_Zu7vaGgHq4iu6y2CzZdnRE9jB7QAAkXqMRvq0XhLTD1-os397wMBAAMCAAN5AAM2BA",
        ["Collar"] = "AgACAgIAAxkDAANTZu_zPj5ZOzk4xxSVAaGFobLaa7wAAjDgMRu5OYBLAAE1NOxPeViMAQADAgADeQADNgQ"
    };

    public static int MultiFloat(int num, float fnum)
    {
        return (int) (num * fnum);
    }
}