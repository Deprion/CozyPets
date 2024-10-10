public static class PetHolder
{
    public static Dictionary<string, Pet> Pets = new Dictionary<string, Pet>()
    {
        ["Cat"] = new Pet(1000, "Кот", 100, 10, 15, 10, 15, "Коты проворны и скрытны - отличные хищники из засады", Global.Photos["Cat"]),
        ["Dog"] = new Pet(1001, "Собака", 125, 13, 10, 13, 12, "Собаки сильны и выносливы", Global.Photos["Dog"]),
        ["Parrot"] = new Pet(1002, "Попугай", 90, 12, 13, 9, 15, "Попугаи умны и внимательны. А еще умеют летать!", Global.Photos["Parrot"]),
        ["Hamster"] = new Pet(1003, "Хомяк", 85, 10, 12, 8, 13, "Хомяки не такие сильные, как другие, но они милые", Global.Photos["Hamster"]),
    };
}