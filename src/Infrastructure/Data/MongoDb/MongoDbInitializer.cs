public static class MongoDbInitializer
{
    public static async Task CreateIndexes(MongoDbContext context)
    {
        //TODO: Índice para búsqueda de hoteles por nombre
        var indexKeysDefinition = Builders<Hotel>.IndexKeys.Ascending(h => h.Name);
        await context.Hotels.Indexes.CreateOneAsync(new CreateIndexModel<Hotel>(indexKeysDefinition));

        //TODO: Índice para búsqueda de reservaciones por fecha
        var reservationIndexKeys = Builders<Reservation>.IndexKeys
            .Ascending(r => r.CheckInDate)
            .Ascending(r => r.CheckOutDate);
        await context.Reservations.Indexes.CreateOneAsync(new CreateIndexModel<Reservation>(reservationIndexKeys));
    }
}
