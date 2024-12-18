public class RoomFilterDto : PaginationParams
{
    public RoomType? Type { get; set; }
    public bool? IsAvailable { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}