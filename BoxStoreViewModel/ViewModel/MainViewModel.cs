using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using BoxStoreModels;
using BoxStoreBLL;
using System.Collections.ObjectModel;

namespace BoxStoreViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public Box NewBox { get => newBox; set { Set(ref newBox , value); } }
        public SearchTicket SearchTicket { get => searchTicket; set { Set(ref searchTicket, value); } }
        public ObservableCollection<Box> Boxes { get => boxes; set => Set(ref boxes, value); }
        public ObservableCollection<Box> RemovedBoxes { get => removedBoxes; set => Set(ref removedBoxes, value); }
        public ObservableCollection<BoxOrder> Orders { get => orders; set => Set(ref orders, value); }        
        public bool SearchSuccess { get => searchSuccess; set => Set(ref searchSuccess, value); }
        public bool NoMatches { get => noMatches; set => Set(ref noMatches, value); }
        public bool AddSuccess { get => addSuccess; set => Set(ref addSuccess, value); }
        public bool PopupOpen { get => popupOpen; set => Set(ref popupOpen, value); }
        public RelayCommand AddBoxCommand { get; set; }
        public RelayCommand SearchBoxesCommand { get; set; }
        public RelayCommand BuyBoxesCommand { get; set; }
        public RelayCommand ClosePopupCommand { get; set; }

        public MainViewModel()
        {
            AddBoxCommand = new RelayCommand(AddBox);
            SearchBoxesCommand = new RelayCommand(SearchForBoxes);
            BuyBoxesCommand = new RelayCommand(BuyBoxes);
            ClosePopupCommand = new RelayCommand(ClosePopup);
            NewBox = new Box();
            Boxes = new ObservableCollection<Box>(bSA.AllBoxes);
            SearchTicket = new SearchTicket();
            SearchSuccess = true;
            AddSuccess = true;
            bSA.BoxCleanUp += BSA_BoxCleanUp;
        }

        private BoxStoreApi bSA = new BoxStoreApi();
        private Box newBox;
        private ObservableCollection<Box> boxes;
        private ObservableCollection<BoxOrder> orders;
        private SearchTicket searchTicket;
        private bool searchSuccess;
        private bool addSuccess;
        private bool noMatches;
        private ObservableCollection<Box> removedBoxes;
        private bool popupOpen;

        private void BSA_BoxCleanUp(object sender, System.Collections.Generic.IEnumerable<Box> e)
        {
            RemovedBoxes = new ObservableCollection<Box>(e);
            Boxes = new ObservableCollection<Box>(bSA.AllBoxes);
            Orders = new ObservableCollection<BoxOrder>();
            PopupOpen = true;
        }
        private void ClosePopup()
        {
            PopupOpen = false;
        }
        private void AddBox()
        {
            if (NewBox.X <= 0 || NewBox.Y <= 0 || NewBox.Quantity <= 0) { AddSuccess = false; return; }
            bSA.AddBox(NewBox);
            AddSuccess = true;
            NewBox = new Box();
            Boxes = new ObservableCollection<Box>(bSA.AllBoxes);
        }
        private void SearchForBoxes()
        {
            bool succes;
            Orders = new ObservableCollection<BoxOrder>(bSA.Search(SearchTicket, out succes));
            if (Orders.Count == 0) { NoMatches = true; SearchSuccess = true; }
            else { NoMatches = false; SearchSuccess = succes; }                   
        }
        private void BuyBoxes()
        {
            bSA.BuyBoxes(Orders);
            Orders = new ObservableCollection<BoxOrder>();
            Boxes = new ObservableCollection<Box>(bSA.AllBoxes);
            NoMatches = false; SearchSuccess = true;
        }
    }
}