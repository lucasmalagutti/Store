using Store.Domain.Repositories;
using Store.Tests.Repositories;
using Store.Domain.Commands;
using Store.Domain.Handlers;

namespace Store.Tests.Handlers
{
    [TestClass]
    public class OrderHandlerTests
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly CreateOrderCommand _command;

        public OrderHandlerTests()
        {
            _customerRepository = new FakeCustomerRepository();
            _deliveryFeeRepository = new FakeDeliveryFeeRepository();
            _discountRepository = new FakeDiscountRepository();
            _orderRepository = new FakeOrderRepository();
            _productRepository = new FakeProductRepository();

            _command = new CreateOrderCommand
            {
                Customer = "Lucas",
                ZipCode = "12345678",
                PromoCode = "87654321"
            };
            _command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            _command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        }
        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_cliente_inexistente_o_pedido_nao_deve_ser_gerado()
        {
            _command.Customer = "";
            _command.Validate();

            Assert.AreEqual(_command.IsValid, false);
        }
        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_cep_invalido_o_pedido_deve_ser_gerado_normalmente()
        {
            _command.ZipCode = null;

            Assert.AreEqual(_command.IsValid, true);
        }
        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_promocode_inexistente_o_pedido_deve_ser_gerado_normalmente()
        {
            _command.PromoCode = null;

            Assert.AreEqual(_command.IsValid, true);
        }
        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_pedido_sem_items_o_mesmo_nao_deve_ser_gerado()
        {
            var command = new CreateOrderCommand
            {
                Customer = "Lucas",
                ZipCode = "12345678",
                PromoCode = "87654321"
            };
            command.Validate();
            Assert.AreEqual(command.IsValid, false);
        }
        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_comando_invalido_o_pedido_nao_deve_ser_gerado()
        {
            _command.Customer = "";
            _command.Validate();

            Assert.AreEqual(_command.IsValid, false);
        }
        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_comando_valido_o_pedido_deve_ser_gerado()
        {
            var handler = new OrderHandler(
                _customerRepository,
                _deliveryFeeRepository,
                _discountRepository,
                _productRepository,
                _orderRepository
                );

            handler.Handle(_command);
            Assert.AreEqual(handler.IsValid, true);
        }
    }
}