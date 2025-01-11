using Store.Domain.Entities;
using Store.Domain.Enums;
namespace Store.Tests.Entities
{
    [TestClass]
    public class DiscountTests
    {
        private readonly Customer _customer = new Customer("Ana", "ana@email.com");
        private readonly Product _product = new Product("Produto 1", 10, true);
        private readonly Discount _discount = new Discount(10, DateTime.Now.AddDays(5));

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_desconto_expirado_o_valor_do_pedido_deve_ser_60()
        {
            var expiredDiscount = new Discount(10, DateTime.Now.AddDays(-1));
            var order = new Order(_customer, 0, expiredDiscount);
            order.AddItem(_product, 6);
            Assert.AreEqual(order.Total(), 60);
        }
        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_desconto_invalido_o_valor_do_pedido_deve_ser_60()
        {
            var invalidDiscount = new Discount(-10, DateTime.Now.AddDays(5));
            var order = new Order(_customer, 0, invalidDiscount);
            order.AddItem(_product, 5);
            Assert.AreEqual(order.Total(), 60);
        }
        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_desconto_de_10_o_valor_do_pedido_deve_ser_50()
        {
            var order = new Order(_customer, 0, _discount);
            order.AddItem(_product, 6);
            Assert.AreEqual(order.Total(), 50);
        }
    }
}