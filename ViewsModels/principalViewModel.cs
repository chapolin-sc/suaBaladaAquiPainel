using suaBaladaAqui2.ViewsModels;

namespace suaBaladaAqui2.ViewsModels;
    public partial class principalViewModel
    {
        public principalViewModel()
        {}

        public principalViewModel(IEnumerable<carouselViewModel> carouselPVM, IEnumerable<boxBaladaViewsModels> boxPVM)
        {
            this.carouselPVM = carouselPVM;
            this.boxPVM = boxPVM;
        }

        public IEnumerable<carouselViewModel> carouselPVM { get; set; }
        public IEnumerable<boxBaladaViewsModels> boxPVM { get; set; }
        
    }
