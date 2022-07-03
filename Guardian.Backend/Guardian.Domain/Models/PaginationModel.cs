namespace Guardian.Domain.Models
{
    public class PagiantionModel
    {
        private const int _maxItemsPerPage = 50;
        private int itemsPerPage = 20;

        public int page { get; set; } = 1;
        public int ItemsPerPage
        {
            get => itemsPerPage;
            set => itemsPerPage = value > _maxItemsPerPage ? _maxItemsPerPage : value;
        }
    }
}