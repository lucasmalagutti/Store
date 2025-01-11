using Store.Domain.Entities;
using Store.Domain.Enums;
namespace Store.Tests.Entities
{
    [TestClass]
    public class ProductTests
    {
        private readonly Customer _customer = new Customer("Ana", "ana@email.com");
        private readonly Product _product = new Product("Produto 1", 10, true);
        private readonly Discount _discount = new Discount(10, DateTime.Now.AddDays(5));
        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_item_sem_produto_o_mesmo_nao_deve_ser_adicionado()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(null, 10);
            Assert.AreEqual(order.Items.Count, 0);
        }
        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_item_com_quantidade_zero_ou_menor_nao_deve_ser_adicionado()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(_product, -1);
            Assert.AreEqual(order.Items.Count, 0);
        }
    }
}