using TD.AppInfra.ViewModels;
using TD.Extensions;
using TD.Models;
using static TD.Extensions.EnumExtension;

namespace TD.WPF.ViewModels.Trades
{
    public partial class TakeProfitOrderCreateVM : AbstractCreateEditVM
    {
        private TakeProfitOrder _takeProfitOrder;
        public TakeProfitOrder TakeProfitOrder => _takeProfitOrder;

        public TakeProfitOrderCreateVM()
        {
            _takeProfitOrder = new TakeProfitOrder();
            ExitOrderTypeValues = GetValues<ExitOrderType>().Where(v => v.Value <= ExitOrderType.TrailingStop)
                                                            .ToList();
            OnInitialized();
        }
        partial void OnInitialized();

        /// <summary>
        /// Gets the collection of available ExitOrderType enum members for selection or display.
        /// </summary>
        public IEnumerable<EnumValueItem<ExitOrderType>> ExitOrderTypeValues { get; }

        public int TradeId
        {
            get { return _takeProfitOrder.TradeId; }
            set
            {
                if (_takeProfitOrder.TradeId != value)
                {
                    _takeProfitOrder.TradeId = value;
                    RaisePropertyChanged(nameof(TradeId));
                    ValidateProperty(_takeProfitOrder, nameof(TradeId));
                }
            }
        }

        public Trade Trade
        {
            get { return _takeProfitOrder.Trade; }
            set
            {
                if (_takeProfitOrder.Trade != value)
                {
                    _takeProfitOrder.Trade = value;
                    RaisePropertyChanged(nameof(Trade));
                    ValidateProperty(_takeProfitOrder, nameof(Trade));
                }
            }
        }

        public ExitOrderType OrderType
        {
            get { return _takeProfitOrder.OrderType; }
            set
            {
                if (_takeProfitOrder.OrderType != value)
                {
                    _takeProfitOrder.OrderType = value;
                    RaisePropertyChanged(nameof(OrderType));
                    ValidateProperty(_takeProfitOrder, nameof(OrderType));
                }
            }
        }

        public decimal OrderPrice
        {
            get { return _takeProfitOrder.OrderPrice; }
            set
            {
                if (_takeProfitOrder.OrderPrice != value)
                {
                    _takeProfitOrder.OrderPrice = value;
                    RaisePropertyChanged(nameof(OrderPrice));
                    ValidateProperty(_takeProfitOrder, nameof(OrderPrice));
                }
            }
        }

        public decimal? OrderQuantity
        {
            get { return _takeProfitOrder.OrderQuantity; }
            set
            {
                if (_takeProfitOrder.OrderQuantity != value)
                {
                    _takeProfitOrder.OrderQuantity = value;
                    RaisePropertyChanged(nameof(OrderQuantity));
                    ValidateProperty(_takeProfitOrder, nameof(OrderQuantity));
                }
            }
        }

        public decimal? OrderValue
        {
            get { return _takeProfitOrder.OrderValue; }
            set
            {
                if (_takeProfitOrder.OrderValue != value)
                {
                    _takeProfitOrder.OrderValue = value;
                    RaisePropertyChanged(nameof(OrderValue));
                    ValidateProperty(_takeProfitOrder, nameof(OrderValue));
                }
            }
        }

        public string? Notes
        {
            get { return _takeProfitOrder.Notes; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && _takeProfitOrder.Notes != value)
                {
                    _takeProfitOrder.Notes = value;
                    RaisePropertyChanged(nameof(Notes));
                    ValidateProperty(_takeProfitOrder, nameof(Notes));
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    _takeProfitOrder.Notes = null;
                    RaisePropertyChanged(nameof(Notes));
                    ValidateProperty(_takeProfitOrder, nameof(Notes));
                }
            }
        }

        public bool ValidateModel()
        {
            return ValidateModel(_takeProfitOrder);
        }
    }
}
