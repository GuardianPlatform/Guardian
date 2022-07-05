namespace Guardian.Domain.Models
{
    public class PagiantionModel
    {
        private const int MaxItemsPerPage = 50;

        public int pageSize { get; set; } = 20;
        public int page { get; set; } = 1;

        public PagiantionModel(int page, int pageSize)
        {
            this.page = page;
            this.pageSize = pageSize;
        }

        public PagiantionModel()
        {
            
        }

        public int ItemsPerPage
        {
            get => pageSize;
            set => pageSize = value > MaxItemsPerPage ? MaxItemsPerPage : value;
        }
    }
}